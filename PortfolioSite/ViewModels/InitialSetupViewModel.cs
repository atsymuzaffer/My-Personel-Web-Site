using System.ComponentModel.DataAnnotations;

namespace PortfolioSite.ViewModels;

public class InitialSetupViewModel
{
    [Required(ErrorMessage = "Yeni e-posta adresi gereklidir.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    [Display(Name = "Yeni E-posta")]
    public string NewEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta tekrarı gereklidir.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    [Compare("NewEmail", ErrorMessage = "E-posta adresleri birbiriyle eşleşmiyor.")]
    [Display(Name = "Yeni E-posta Tekrarı")]
    public string ConfirmEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Yeni şifre gereklidir.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Şifre en az {2} karakter uzunluğunda olmalıdır.")]
    [DataType(DataType.Password)]
    [Display(Name = "Yeni Şifre")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre tekrarı gereklidir.")]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "Şifreler birbiriyle eşleşmiyor.")]
    [Display(Name = "Yeni Şifre Tekrarı")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
