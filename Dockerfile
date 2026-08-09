# DÜZENLENDİ: Uygulamayı derlemek için .NET 8 SDK kullanılıyor.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# DÜZENLENDİ: Önce proje dosyası kopyalanıyor.
# Böylece NuGet restore katmanı Docker cache'inden yararlanabilir.
COPY ["PortfolioSite/PortfolioSite.csproj", "PortfolioSite/"]
RUN dotnet restore "PortfolioSite/PortfolioSite.csproj"

# DÜZENLENDİ: Solution içerisindeki tüm kaynak kodlar build ortamına alınıyor.
COPY . .

WORKDIR "/src/PortfolioSite"

# DÜZENLENDİ: Production için Release publish oluşturuluyor.
RUN dotnet publish "PortfolioSite.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# DÜZENLENDİ: Canlı ortamda yalnızca ASP.NET Core 8 Runtime kullanılıyor.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# DÜZENLENDİ: Production ortamı ve .NET 8 container portu.
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_HTTP_PORTS=8080

EXPOSE 8080

# DÜZENLENDİ: Derlenmiş uygulama production image'ına aktarılıyor.
COPY --from=build /app/publish .

# DÜZENLENDİ: ASP.NET Core uygulaması başlatılıyor.
ENTRYPOINT ["dotnet", "PortfolioSite.dll"]