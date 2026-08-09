using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioSite.Areas.Admin.Controllers;

[Area("Admin")]
public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signIn;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(SignInManager<IdentityUser> signIn, UserManager<IdentityUser> userManager)
    {
        _signIn = signIn;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
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

        var model = new PortfolioSite.ViewModels.ChangeCredentialsViewModel
        {
            CurrentEmail = user.Email ?? string.Empty,
            NewEmail = user.Email ?? string.Empty
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ChangeCredentials(PortfolioSite.ViewModels.ChangeCredentialsViewModel model)
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

        // 4. Enforce single admin user rule: purge any other users in database
        var otherUsers = _userManager.Users.Where(u => u.Id != user.Id).ToList();
        foreach (var otherUser in otherUsers)
        {
            await _userManager.DeleteAsync(otherUser);
        }

        if (updatedAny)
        {
            await _signIn.RefreshSignInAsync(user);
            TempData["Success"] = "Hesap bilgileriniz (E-Posta / Şifre) başarıyla güncellendi ve tek yönetici olarak kaydedildi!";
        }
        else
        {
            TempData["Success"] = "Herhangi bir değişiklik yapılmadı.";
        }

        return RedirectToAction(nameof(ChangeCredentials));
    }
}
