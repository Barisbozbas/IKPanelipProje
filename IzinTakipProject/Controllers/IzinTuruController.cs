using IzinTakipProject.Data;
using IzinTakipProject.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class IzinTuruController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public IzinTuruController(ApplicationDbContext db)
    {
        _db = db;
    }

    // Tüm izin türlerini getir
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var turler = await _db.IzinTurleri
            .AsNoTracking()
            .ToListAsync();
        return Ok(turler);
    }

    // Yeni izin türü ekle
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] IzinTuru model)
    {
        if (string.IsNullOrWhiteSpace(model.Ad))
            return BadRequest("İzin türü adı boş olamaz.");

        _db.IzinTurleri.Add(model);
        await _db.SaveChangesAsync();
        return Ok(model);
    }

    // Bir izin türünü sil
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var tur = await _db.IzinTurleri.FindAsync(id);
        if (tur == null)
            return NotFound();

        _db.IzinTurleri.Remove(tur);
        await _db.SaveChangesAsync();
        return Ok();
    }
}
