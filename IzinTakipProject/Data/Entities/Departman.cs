using System.ComponentModel.DataAnnotations;

namespace IzinTakipProject.Data.Entities
{
    public class Departman
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Departman adı zorunludur!")]
        public string Ad { get; set; }

        // Navigation property, asla [Required] OLMAYACAK ve new'lenmiş olacak:
        public ICollection<Kullanici> Calisanlar { get; set; } = new List<Kullanici>();
    }
}
