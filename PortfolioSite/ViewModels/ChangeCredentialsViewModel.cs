using System.ComponentModel.DataAnnotations;

namespace PortfolioSite.ViewModels;

public class ChangeCredentialsViewModel
{
    [Required(ErrorMessage = "Mevcut e-posta gereklidir.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    [Display(Name = "Mevcut E-Posta")]
    public string CurrentEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Yeni e-posta gereklidir.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    [Display(Name = "Yeni E-Posta")]
    public string NewEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mevcut şifreniz gereklidir.")]
    [DataType(DataType.Password)]
    [Display(Name = "Mevcut Şifre")]
    public string CurrentPassword { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Yeni şifre en az 8 karakter olmalıdır.")]
    [Display(Name = "Yeni Şifre (Değiştirmek istemiyorsanız boş bırakın)")]
    public string? NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "Yeni şifreler birbiriyle eşleşmiyor.")]
    [Display(Name = "Yeni Şifre Tekrar")]
    public string? ConfirmNewPassword { get; set; }
}
