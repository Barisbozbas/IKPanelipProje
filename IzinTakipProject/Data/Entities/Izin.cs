using System.ComponentModel.DataAnnotations.Schema;

namespace IzinTakipProject.Data.Entities
{
    public class Izin
    {
        public int Id { get; set; }

        public string KullaniciId { get; set; }

        [ForeignKey("KullaniciId")]
        public Kullanici Calisan { get; set; }

        public int IzinTuruId { get; set; }

        [ForeignKey("IzinTuruId")]
        public IzinTuru IzinTuru { get; set; }

        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public int IsGunuSayisi { get; set; }
        public bool OnaylandiMi { get; set; }
    }


}
