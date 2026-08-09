using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PortfolioSite.Entities;

namespace PortfolioSite.Filters;

public class MustChangeCredentialsFilter : IAsyncActionFilter
{
    private readonly UserManager<ApplicationUser> _userManager;

    public MustChangeCredentialsFilter(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var httpContext = context.HttpContext;
        var user = httpContext.User;

        if (user.Identity?.IsAuthenticated == true)
        {
            var area = context.RouteData.Values["area"]?.ToString();
            if (string.Equals(area, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                var controller = context.RouteData.Values["controller"]?.ToString();
                var action = context.RouteData.Values["action"]?.ToString();

                // Allow InitialSetup and Logout actions in AccountController
                bool isInitialSetup = string.Equals(controller, "Account", StringComparison.OrdinalIgnoreCase) &&
                                     string.Equals(action, "InitialSetup", StringComparison.OrdinalIgnoreCase);

                bool isLogout = string.Equals(controller, "Account", StringComparison.OrdinalIgnoreCase) &&
                               string.Equals(action, "Logout", StringComparison.OrdinalIgnoreCase);

                if (!isInitialSetup && !isLogout)
                {
                    var appUser = await _userManager.GetUserAsync(user);
                    if (appUser != null && appUser.MustChangeCredentials)
                    {
                        context.Result = new RedirectToActionResult("InitialSetup", "Account", new { area = "Admin" });
                        return;
                    }
                }
            }
        }

        await next();
    }
}
