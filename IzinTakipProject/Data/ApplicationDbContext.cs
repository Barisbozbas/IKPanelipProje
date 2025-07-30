using IzinTakipProject.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IzinTakipProject.Data.Yetkilendirme;

namespace IzinTakipProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<Kullanici, Rol, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

      
        public DbSet<Departman> Departmanlar { get; set; }
        public DbSet<Izin> Izinler { get; set; }
        public DbSet<IzinAyarModel> IzinAyarlar { get; set; }

        public DbSet<IzinTuru> IzinTurleri { get; set; }

        public DbSet<Kullanici> Kullanicilar => Users;
        public DbSet<KullaniciDetay> KullaniciDetaylar { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Gerekirse özel konfigürasyonlar buraya
        }
    }
}

