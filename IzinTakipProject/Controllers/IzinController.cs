using IzinTakipProject.Data.Entities;
using IzinTakipProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class IzinController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public IzinController(ApplicationDbContext db) => _db = db;

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] IzinTalepDto talep)
    {
        // Eksik veri kontrolü
        if (talep == null || talep.BaslangicTarihi == default || talep.BitisTarihi == default || talep.IzinTuruId == 0 || string.IsNullOrEmpty(talep.UserId))
            return BadRequest("Eksik veya hatalı veri!");

        var userId = talep.UserId;

        // Tarih çakışma kontrolü
        bool cakisanVar = await _db.Izinler
            .AnyAsync(i => i.KullaniciId == userId
                        && ((talep.BaslangicTarihi >= i.BaslangicTarihi && talep.BaslangicTarihi <= i.BitisTarihi)
                        || (talep.BitisTarihi >= i.BaslangicTarihi && talep.BitisTarihi <= i.BitisTarihi)
                        || (talep.BaslangicTarihi <= i.BaslangicTarihi && talep.BitisTarihi >= i.BitisTarihi)));
        if (cakisanVar)
            return BadRequest("Bu tarihlerde zaten bir izin talebiniz mevcut!");

        // Hafta sonu hariç gün sayısı
        int gunSayisi = GunSayisiIsGunu(talep.BaslangicTarihi, talep.BitisTarihi);

        // Kayıt
        var izin = new Izin
        {
            KullaniciId = userId,
            BaslangicTarihi = talep.BaslangicTarihi,
            BitisTarihi = talep.BitisTarihi,
            IzinTuruId = talep.IzinTuruId,
            IsGunuSayisi = gunSayisi,
            OnaylandiMi = false
        };
        _db.Izinler.Add(izin);
        await _db.SaveChangesAsync();

        return Ok(new { mesaj = "İzin talebiniz başarıyla alındı!" });
    }
    [HttpGet("kullanici/{userId}")]
    public async Task<IActionResult> GetKullaniciIzinleri(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return BadRequest("Kullanıcı ID gerekli!");

        var izinler = await _db.Izinler
            .Include(i => i.IzinTuru)
            .Where(i => i.KullaniciId == userId)
            .OrderByDescending(i => i.BaslangicTarihi)
            .Select(i => new KullaniciIzinDto
            {
                Id = i.Id,
                IzinTuruAd = i.IzinTuru.Ad,
                BaslangicTarihi = i.BaslangicTarihi,
                BitisTarihi = i.BitisTarihi,
                IsGunuSayisi = i.IsGunuSayisi,
                OnaylandiMi = i.OnaylandiMi
            })
            .ToListAsync();

        return Ok(izinler);
    }


    [HttpGet("kullanilan-gun/{userId}")]
    public async Task<IActionResult> GetKullanilanIzinGunu(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return BadRequest("Kullanıcı ID gerekli.");

        // Onaylanan izinleri topla (istersen OnaylandiMi'yi kaldırabilirsin)
        var toplamIzin = await _db.Izinler
            .Where(i => i.KullaniciId == userId && i.OnaylandiMi)
            .SumAsync(i => (int?)i.IsGunuSayisi) ?? 0;

        return Ok(toplamIzin);
    }

    private int GunSayisiIsGunu(DateTime baslangic, DateTime bitis)
    {
        int gun = 0;
        for (var dt = baslangic.Date; dt <= bitis.Date; dt = dt.AddDays(1))
            if (dt.DayOfWeek != DayOfWeek.Saturday && dt.DayOfWeek != DayOfWeek.Sunday)
                gun++;
        return gun;
    }
}
