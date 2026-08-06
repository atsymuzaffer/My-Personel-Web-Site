using System.ComponentModel.DataAnnotations;

namespace PortfolioSite.ViewModels;

public class ContactViewModel
{
    [Required(ErrorMessage = "Ad Soyad gereklidir."), MaxLength(100)]
    [Display(Name = "Ad Soyad")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta gereklidir."), EmailAddress, MaxLength(200)]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [MaxLength(300)]
    [Display(Name = "Konu")]
    public string? Subject { get; set; }

    [Required(ErrorMessage = "Mesaj gereklidir."), MaxLength(5000), MinLength(10, ErrorMessage = "Mesaj en az 10 karakter olmalıdır.")]
    [Display(Name = "Mesaj")]
    public string Message { get; set; } = string.Empty;

    [Range(typeof(bool), "true", "true", ErrorMessage = "Kişisel verilerin işlenmesini onaylamanız gerekir.")]
    [Display(Name = "Kişisel veri işlemeyi onaylıyorum")]
    public bool ConsentGiven { get; set; }

    // Honeypot anti-spam field - must be empty
    public string? Website { get; set; }
}
