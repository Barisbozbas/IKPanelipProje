using IzinTakipProject.Data.Entities;
using IzinTakipProject.Servisler.Interfaces;
using Microsoft.EntityFrameworkCore;
using IzinTakipProject.Data;
using IzinTakipProject.Data.Yetkilendirme; // Kullanici class'ı burada

namespace IzinTakipProject.Servisler.Applications
{
    public class IzinServisi : IIzinServisi
    {
        private readonly ApplicationDbContext _db;

        public IzinServisi(ApplicationDbContext db) => _db = db;

        public async Task<List<Izin>> IzinleriGetirAsync() =>
            await _db.Izinler
                .Include(i => i.Calisan)
                .Include(i => i.IzinTuru)
                .ToListAsync();

        public async Task<List<Kullanici>> KullanicilariGetirAsync() =>
            await _db.Users.ToListAsync();

        public async Task<List<IzinTuru>> IzinTurleriniGetirAsync() =>
            await _db.IzinTurleri.ToListAsync();

        public async Task<List<Izin>> BekleyenIzinleriGetirAsync() =>
            await _db.Izinler
                .Where(i => !i.OnaylandiMi)
                .Include(i => i.Calisan)
                .Include(i => i.IzinTuru)
                .ToListAsync();

        public async Task IzinEkleAsync(Izin izin)
        {
            izin.IsGunuSayisi = TarihYardimcisi.IsGunuHesapla(izin.BaslangicTarihi, izin.BitisTarihi);
            _db.Izinler.Add(izin);
            await _db.SaveChangesAsync();
        }

        public async Task IzinGuncelleAsync(Izin izin)
        {
            _db.Izinler.Update(izin);
            await _db.SaveChangesAsync();
        }

       
    }
}
