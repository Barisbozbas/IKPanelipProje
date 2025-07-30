namespace IzinTakipProject.Data.Entities
{
    public class KullaniciDetay
    {
        public int Id { get; set; }
        public string UserId { get; set; } // AspNetUsers.Id ile eşleşir
        public string AdSoyad { get; set; }
        public string DepartmanId { get; set; }
    }
}
