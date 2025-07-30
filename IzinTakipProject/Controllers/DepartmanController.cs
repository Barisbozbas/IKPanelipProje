using IzinTakipProject.Data;
using IzinTakipProject.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class DepartmanController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public DepartmanController(ApplicationDbContext db)
    {
        _db = db;
    }

    // Tüm departmanları getir
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var departmanlar = await _db.Departmanlar
            .Select(d => new { d.Id, d.Ad })
            .ToListAsync();
        return Ok(departmanlar);
    }

    // Yeni departman ekle
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Departman model)
    {
        if (model == null)
            return BadRequest("Model NULL geldi! JSON doğru mu?");
        if (string.IsNullOrWhiteSpace(model.Ad))
            return BadRequest("Departman adı boş olamaz.");

        var entity = new Departman { Ad = model.Ad };
        _db.Departmanlar.Add(entity);
        await _db.SaveChangesAsync();
        return Ok(entity);
    }

    // Departman sil
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var dep = await _db.Departmanlar
            .Include(d => d.Calisanlar)
            .FirstOrDefaultAsync(d => d.Id == id);
        if (dep == null)
            return NotFound();

        if (dep.Calisanlar != null && dep.Calisanlar.Any())
            return BadRequest("Bu departmanda çalışanlar bulunduğu için silinemez!");

        _db.Departmanlar.Remove(dep);
        await _db.SaveChangesAsync();
        return Ok();
    }
}
