using System.ComponentModel.DataAnnotations;

namespace IzinTakipProject.Data.Entities
{
    public class KullaniciCreateModel
    {
        [Required(ErrorMessage = "Ad Soyad gereklidir")]
        public string AdSoyad { get; set; }

        [Required(ErrorMessage = "Email gereklidir")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalı")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Rol seçmelisiniz")]
        public string Rol { get; set; }

        [Required(ErrorMessage = "Departman seçmelisiniz")]
        public string DepartmanId { get; set; }
    }
}
