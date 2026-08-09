# 🚀 Muzaffer Atasoy — Kişisel Portföy & Yönetim Paneli (ASP.NET Core 8)

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=csharp&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/Microsoft%20SQL%20Server-2022-CC292B?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![EF Core](https://img.shields.io/badge/Entity%20Framework-Core%208-512BD4?style=for-the-badge)

Backend geliştirme ve veritabanı mimarisi odağında hazırlanan; dinamik içerik yönetimine sahip, ölçeklenebilir ve sürdürülebilir **ASP.NET Core 8 MVC** kişisel portföy projesidir.

---

## 📌 Özellikler

### 🌐 Public Portföy Sayfası
- **12 Kolonlu Editoryal Izgara (Editorial Grid):** Modern, teknik ve okunabilir arayüz tasarımı.
- **Dinamik İçerik:** Projeler, iş deneyimleri, yetenekler, sertifikalar ve özgeçmiş bilgileri veritabanından dinamik çekilir.
- **Credly Tarzı Rozet / Sertifika Görünümü:** Sertifika ve rozet görselleri için özel tasarım. Süresiz sertifikalar için otomatik sonsuzluk (`∞`) simgesi.
- **Karanlık & Aydınlık Tema (Light / Dark Mode):** CSS değişkenleri ve LocalStorage kalıcılığı ile anlık tema geçişi.
- **İnteraktif Ağ Arka Planı (tsParticles):** Fare hareketleriyle etkileşime giren matriks/ağ efekti.
- **Çoklu Dil Desteği (TR / EN):** Özel isim korumalı (`notranslate`) ve cookie tabanlı dinamik dil seçeneği.

### 🛡️ Yönetim Paneli (Admin Panel)
- **Güvenli Kimlik Doğrulama:** ASP.NET Core Identity altyapısı ile rol tabanlı (Admin) yetkilendirme.
- **Tam CRUD Yönetimi:** Proje, yetenek, deneyim ve sertifikalar için ekleme, düzenleme ve silme işlemleri.
- **Gerçek Dosya & Görsel Yükleme:** Sertifikalar ve projeler için yerel dosya depolama servisi (`IFileStorageService`).
- **Gelişmiş Form Güvenliği:** Anti-forgery token (XSRF/CSRF) doğrulaması ve iletişim formu için Honeypot anti-spam mekanizması.

---

## 🛠️ Teknoloji Yığını

- **Backend:** C# 12, ASP.NET Core 8 MVC
- **Mimari:** Clean Layered Architecture, Repository & Service Pattern
- **Veritabanı & ORM:** MS SQL Server, Entity Framework Core 8
- **Güvenlik:** ASP.NET Core Identity, Anti-Forgery Tokens, Honeypot Anti-Spam
- **Ön Yüz (Frontend):** Vanilla CSS3, HTML5, JavaScript (ES6+), tsParticles
- **Araçlar:** Visual Studio 2022 / Antigravity, Git, GitHub Desktop

---

## 📂 Proje Yapısı

```
My-Personel-Web-Site/
├── PortfolioSite/
│   ├── Areas/
│   │   └── Admin/            # Admin Paneli Controller & View bileşenleri
│   ├── Controllers/          # Public Ana Sayfa ve Proje detay Controller'ları
│   ├── Data/                 # ApplicationDbContext & Entity konfigürasyonları
│   ├── Entities/             # Veritabanı varlık modelleri (BaseEntity, Project, Skill...)
│   ├── Interfaces/           # Servis arayüzleri (IPortfolioService, IFileStorageService)
│   ├── Services/             # İş mantığı ve dosya depolama servisleri
│   ├── ViewComponents/       # Reusable View Component'ler (SocialLinks vb.)
│   ├── ViewModels/           # Sayfa modelleri (HomeViewModel, ContactViewModel vb.)
│   ├── Views/                # Public Razor View'ler ve Layout
│   └── wwwroot/              # CSS, JS, statik görseller ve dosya yükleme dizinleri
└── README.md
```

---

## 🚀 Kurulum ve Çalıştırma

### Gereksinimler
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Microsoft SQL Server](https://www.microsoft.com/sql-server/) (LocalDB veya Express)

### Adımlar

1. **Repoyu klonlayın:**
   ```bash
   git clone https://github.com/atsmuzaffer/My-Personel-Web-Site.git
   cd My-Personel-Web-Site/PortfolioSite
   ```

2. **Veritabanı Bağlantısını Yapılandırın:**
   `appsettings.json` içerisindeki bağlantı dizesini kendi SQL Server bilginize göre güncelleyin:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PortfolioDb;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

3. **Veritabanı Migrasyonunu Uygulayın:**
   ```bash
   dotnet ef database update
   ```

4. **Uygulamayı Çalıştırın:**
   ```bash
   dotnet run --urls "http://localhost:5145"
   ```

---

## 🔐 Varsayılan Admin İlk Kurulum Bilgileri

Projeyi yerel ortamda test etmek veya ilk kurulumu gerçekleştirmek için varsayılan demo giriş bilgileri:

- **Admin URL:** `http://localhost:5145/Admin`
- **Demo E-posta:** `admin@example.com`
- **Demo Şifre:** `Admin@2025!`

> [!IMPORTANT]
> **Zorunlu İlk Kurulum (Initial Setup):** Bu varsayılan bilgiler yalnızca projeyi ilk kez kuran kişilerin erişim sağlayabilmesi içindir. Sistem güvenliği gereği demo bilgilerle yapılan ilk girişte uygulama sizi otomatik olarak **Zorunlu İlk Kurulum (`/Admin/Account/InitialSetup`)** ekranına yönlendirir ve kendi kişisel e-posta ve şifrenizi belirlemenizi şart koşar. Bilgiler değiştirildikten sonra demo hesap kalıcı olarak kapatılır.

---

## 📬 İletişim

**Muzaffer Atasoy** — Backend & Veritabanı Geliştiricisi  
🌐 **Web:** [muzafferatasoy.com](https://muzafferatasoy.com)  
📂 **GitHub:** [@atsmuzaffer](https://github.com/atsymuzaffer)
