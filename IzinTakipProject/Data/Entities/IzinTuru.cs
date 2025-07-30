namespace IzinTakipProject.Data.Entities
{
    public class IzinTuru
    {
        public int Id { get; set; }
        public string Ad { get; set; } // Yıllık, Hastalık, Ücretsiz

        public ICollection<Izin> Izinler { get; set; } = new List<Izin>();
    }
}
