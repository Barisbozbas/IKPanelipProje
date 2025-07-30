using IzinTakipProject.Data;
using IzinTakipProject.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IzinTakipProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        private readonly UserManager<Kullanici> _userManager;
        private readonly RoleManager<Rol> _roleManager; // BURAYA DİKKAT!
        private readonly ApplicationDbContext _db;

        public KullaniciController(
            UserManager<Kullanici> userManager,
            RoleManager<Rol> roleManager,      // BURAYA DİKKAT!
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }
        // KullaniciController.cs
        [HttpGet("profil")]
        public async Task<IActionResult> Profil([FromQuery] string email)
        {
            var user = await _userManager.Users
                .Include(k => k.Departman)
                .FirstOrDefaultAsync(k => k.Email == email);

            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                AdSoyad = user.AdSoyad,
                Email = user.Email,
                Rol = roles.FirstOrDefault() ?? "-",
                DepartmanAd = user.Departman?.Ad ?? "-"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] KullaniciCreateModel yeniKullanici)
        {
            if (yeniKullanici == null)
                return BadRequest("Kullanıcı bilgileri eksik!");

            var user = new Kullanici
            {
                UserName = yeniKullanici.Email,
                Email = yeniKullanici.Email,
                AdSoyad = yeniKullanici.AdSoyad,
                DepartmanId = int.TryParse(yeniKullanici.DepartmanId, out var depId) ? depId : (int?)null
            };
            var result = await _userManager.CreateAsync(user, yeniKullanici.Sifre);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Role ekle
            if (!string.IsNullOrEmpty(yeniKullanici.Rol))
            {
                if (!await _roleManager.RoleExistsAsync(yeniKullanici.Rol))
                {
                    await _roleManager.CreateAsync(new Rol { Name = yeniKullanici.Rol });  // DİKKAT: IdentityRole yerine Rol nesnesi!
                }
                await _userManager.AddToRoleAsync(user, yeniKullanici.Rol);
            }

            return Ok(new { mesaj = "Kullanıcı başarıyla eklendi!" });
        }
    }
}

    //public class KullaniciController : ControllerBase
    //{
    //    private readonly UserManager<IdentityUser> _userManager;
    //    private readonly RoleManager<IdentityRole> _roleManager;
    //    private readonly ApplicationDbContext _db; //

    //    public KullaniciController(
    //        UserManager<IdentityUser> userManager,
    //        RoleManager<IdentityRole> roleManager,
    //        ApplicationDbContext db)
    //    {
    //        _userManager = userManager;
    //        _roleManager = roleManager;
    //        _db = db;
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Post([FromBody] KullaniciCreateModel yeniKullanici)
    //    {
    //        if (yeniKullanici == null)
    //            return BadRequest("Kullanıcı bilgileri eksik!");

    //        //  AspNetUsers tablosuna ekliyoruz
    //        var user = new IdentityUser
    //        {
    //            UserName = yeniKullanici.Email,
    //            Email = yeniKullanici.Email
    //        };
    //        var result = await _userManager.CreateAsync(user, yeniKullanici.Sifre);

    //        if (!result.Succeeded)
    //            return BadRequest(result.Errors);

    //        //  Role ekle
    //        if (!string.IsNullOrEmpty(yeniKullanici.Rol))
    //        {
    //            if (!await _roleManager.RoleExistsAsync(yeniKullanici.Rol))
    //            {
    //                await _roleManager.CreateAsync(new IdentityRole(yeniKullanici.Rol));
    //            }

    //            await _userManager.AddToRoleAsync(user, yeniKullanici.Rol);
    //        }

    //         // Burada KullaniciDetay tablosuna ekliyoruz
    //        var detay = new KullaniciDetay
    //        {
    //            UserId = user.Id, 
    //            AdSoyad = yeniKullanici.AdSoyad,
    //            DepartmanId = yeniKullanici.DepartmanId
    //        };
    //        _db.KullaniciDetaylar.Add(detay);
    //        await _db.SaveChangesAsync();

    //        return Ok(new { mesaj = "Kullanıcı ve detayları başarıyla eklendi!" });
    //    }
    //}
