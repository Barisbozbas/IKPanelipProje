using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace IzinTakipProject.Data.Entities
{

    public class Kullanici : IdentityUser
    {
        public string AdSoyad { get; set; }

        [ForeignKey("Departman")]
        public int? DepartmanId { get; set; }
        public virtual Departman? Departman { get; set; }

        public string? YoneticiId { get; set; }

        [ForeignKey("YoneticiId")]
        public virtual Kullanici? Yonetici { get; set; }

        public virtual ICollection<Kullanici>? BagliCalisanlar { get; set; }

        public virtual ICollection<Izin>? Izinler { get; set; }
    }


}
