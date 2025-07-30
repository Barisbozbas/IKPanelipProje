namespace IzinTakipProject.Data.Entities
{
    public class IzinDto
    {
        public int Id { get; set; }
        public string IzinTuruAd { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public int IsGunuSayisi { get; set; }
        public bool OnaylandiMi { get; set; }
    }
}
