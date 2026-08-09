using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortfolioSite.Entities;
using PortfolioSite.ViewModels;

namespace PortfolioSite.Areas.Admin.Controllers;

[Area("Admin")]
public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signIn;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(SignInManager<ApplicationUser> signIn, UserManager<ApplicationUser> userManager)
    {
        _signIn = signIn;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null && currentUser.MustChangeCredentials)
                return RedirectToAction(nameof(InitialSetup));

            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string email, string password, bool rememberMe = false, string? returnUrl = null)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            ModelState.AddModelError(string.Empty, "E-posta ve şifre gereklidir.");
            return View();
        }

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !await _userManager.IsInRoleAsync(user, "Admin"))
        {
            ModelState.AddModelError(string.Empty, "Geçersiz giriş bilgileri.");
            return View();
        }

        var result = await _signIn.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: true);
        if (result.Succeeded)
        {
            if (user.MustChangeCredentials)
                return RedirectToAction(nameof(InitialSetup));

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }

        if (result.IsLockedOut)
            ModelState.AddModelError(string.Empty, "Hesabınız geçici olarak kilitlendi. Lütfen daha sonra tekrar deneyin.");
        else
            ModelState.AddModelError(string.Empty, "Geçersiz e-posta veya şifre.");

        return View();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> InitialSetup()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToAction(nameof(Login));

        if (!user.MustChangeCredentials)
            return RedirectToAction("Index", "Dashboard");

        return View(new InitialSetupViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> InitialSetup(InitialSetupViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToAction(nameof(Login));

        if (!user.MustChangeCredentials)
            return RedirectToAction("Index", "Dashboard");

        // Check if new email is taken by another account
        if (!string.Equals(user.Email, model.NewEmail, StringComparison.OrdinalIgnoreCase))
        {
            var existing = await _userManager.FindByEmailAsync(model.NewEmail);
            if (existing != null && existing.Id != user.Id)
            {
                ModelState.AddModelError("NewEmail", "Bu e-posta adresi başka bir kullanıcı tarafından kullanılıyor.");
                return View(model);
            }
        }

        // 1. Change password from demo password "Admin@2025!" to new password
        var changePasswordResult = await _userManager.ChangePasswordAsync(user, "Admin@2025!", model.NewPassword);
        if (!changePasswordResult.Succeeded)
        {
            foreach (var err in changePasswordResult.Errors)
                ModelState.AddModelError(string.Empty, err.Description);
            return View(model);
        }

        // 2. Set new email and username
        var setEmailResult = await _userManager.SetEmailAsync(user, model.NewEmail);
        if (!setEmailResult.Succeeded)
        {
            foreach (var err in setEmailResult.Errors)
                ModelState.AddModelError(string.Empty, err.Description);
            return View(model);
        }

        var setUserNameResult = await _userManager.SetUserNameAsync(user, model.NewEmail);
        if (!setUserNameResult.Succeeded)
        {
            foreach (var err in setUserNameResult.Errors)
                ModelState.AddModelError(string.Empty, err.Description);
            return View(model);
        }

        // 3. Mark MustChangeCredentials = false and update security stamp
        user.MustChangeCredentials = false;
        await _userManager.UpdateAsync(user);
        await _userManager.UpdateSecurityStampAsync(user);

        // 4. Force sign out and redirect to login page
        await _signIn.SignOutAsync();
        TempData["Success"] = "İlk kurulum başarıyla tamamlandı! Lütfen yeni e-posta ve şifrenizle giriş yapınız.";
        return RedirectToAction(nameof(Login));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signIn.SignOutAsync();
        return RedirectToAction("Login");
    }

    public IActionResult AccessDenied() => View();

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ChangeCredentials()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToAction(nameof(Login));

        var model = new ChangeCredentialsViewModel
        {
            CurrentEmail = user.Email ?? string.Empty,
            NewEmail = user.Email ?? string.Empty
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ChangeCredentials(ChangeCredentialsViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToAction(nameof(Login));

        // 1. Verify current password
        var passwordCheck = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
        if (!passwordCheck)
        {
            ModelState.AddModelError("CurrentPassword", "Mevcut şifreniz hatalı.");
            return View(model);
        }

        bool updatedAny = false;

        // 2. Check if email is changing
        if (!string.Equals(user.Email, model.NewEmail, StringComparison.OrdinalIgnoreCase))
        {
            var existingUserWithEmail = await _userManager.FindByEmailAsync(model.NewEmail);
            if (existingUserWithEmail != null && existingUserWithEmail.Id != user.Id)
            {
                ModelState.AddModelError("NewEmail", "Bu e-posta adresi başka bir kullanıcı tarafından kullanılıyor.");
                return View(model);
            }

            var setEmailResult = await _userManager.SetEmailAsync(user, model.NewEmail);
            if (!setEmailResult.Succeeded)
            {
                foreach (var err in setEmailResult.Errors)
                    ModelState.AddModelError(string.Empty, err.Description);
                return View(model);
            }

            var setUserNameResult = await _userManager.SetUserNameAsync(user, model.NewEmail);
            if (!setUserNameResult.Succeeded)
            {
                foreach (var err in setUserNameResult.Errors)
                    ModelState.AddModelError(string.Empty, err.Description);
                return View(model);
            }

            updatedAny = true;
        }

        // 3. Check if password is changing
        if (!string.IsNullOrWhiteSpace(model.NewPassword))
        {
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var err in changePasswordResult.Errors)
                    ModelState.AddModelError(string.Empty, err.Description);
                return View(model);
            }

            updatedAny = true;
        }

        if (updatedAny)
        {
            await _signIn.RefreshSignInAsync(user);
            TempData["Success"] = "Hesap bilgileriniz (E-Posta / Şifre) başarıyla güncellendi!";
        }
        else
        {
            TempData["Success"] = "Herhangi bir değişiklik yapılmadı.";
        }

        return RedirectToAction(nameof(ChangeCredentials));
    }
}
