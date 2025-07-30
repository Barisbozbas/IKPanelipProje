
using IzinTakipProject.Data;
using IzinTakipProject.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class IzinAyarController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public IzinAyarController(ApplicationDbContext db)
    {
        _db = db;
    }


    [HttpGet]
    public async Task<ActionResult<IzinAyarModel>> Get()
    {
        var ayar = await _db.IzinAyarlar.FirstOrDefaultAsync();
        if (ayar == null)
        {
            ayar = new IzinAyarModel { MaksimumIzinGun = 20 }; 
            _db.IzinAyarlar.Add(ayar);
            await _db.SaveChangesAsync();
        }
        return ayar;
    }

    //[HttpGet("kullanilan-gun/{userId}")]
    //public async Task<IActionResult> GetKullanilanIzinGunu(string userId)
    //{
    //    if (string.IsNullOrWhiteSpace(userId))
    //        return BadRequest("Kullanıcı ID gerekli.");

    //    // Onaylanan izinleri topla (istersen OnaylandiMi'yi kaldırabilirsin)
    //    var toplamIzin = await _db.Izinler
    //        .Where(i => i.KullaniciId == userId && i.OnaylandiMi)
    //        .SumAsync(i => (int?)i.IsGunuSayisi) ?? 0;

    //    return Ok(toplamIzin);
    //}

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] IzinAyarModel model)
    {
        if (model.MaksimumIzinGun < 1 || model.MaksimumIzinGun > 90)
            return BadRequest("İzin günü 1-90 arası olmalı!");

        var ayar = await _db.IzinAyarlar.FirstOrDefaultAsync();
        if (ayar == null)
        {
            _db.IzinAyarlar.Add(new IzinAyarModel { MaksimumIzinGun = model.MaksimumIzinGun });
        }
        else
        {
            ayar.MaksimumIzinGun = model.MaksimumIzinGun;
            _db.IzinAyarlar.Update(ayar);
        }
        await _db.SaveChangesAsync();
        return Ok(ayar ?? model);
    }
}
