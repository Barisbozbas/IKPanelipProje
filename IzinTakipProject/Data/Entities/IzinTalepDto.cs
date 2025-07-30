namespace IzinTakipProject.Data.Entities
{
    public class IzinTalepDto
    {
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public int IzinTuruId { get; set; }

        public string UserId { get; set; } 
    }
}
