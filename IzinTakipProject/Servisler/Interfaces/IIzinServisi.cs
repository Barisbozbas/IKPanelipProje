using IzinTakipProject.Data.Entities;
using IzinTakipProject.Data.Yetkilendirme; // Kullanici sınıfı bu namespace altında

namespace IzinTakipProject.Servisler.Interfaces
{
    public interface IIzinServisi
    {
        Task<List<Izin>> IzinleriGetirAsync();
        Task<List<IzinTuru>> IzinTurleriniGetirAsync();
        Task<List<Izin>> BekleyenIzinleriGetirAsync();
        Task IzinEkleAsync(Izin izin);
        Task IzinGuncelleAsync(Izin izin);

        // ⬇️ Bu satırı ekle
        Task<List<Kullanici>> KullanicilariGetirAsync();
    }
}
