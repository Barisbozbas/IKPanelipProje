using IzinTakipProject.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly SignInManager<Kullanici> _signInManager;
    private readonly UserManager<Kullanici> _userManager;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        SignInManager<Kullanici> signInManager,
        UserManager<Kullanici> userManager,
        ILogger<AuthController> logger)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] GirisModel model)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(model.Eposta);
            if (user == null)
                return Unauthorized(new { Error = "Geçersiz kullanıcı adı veya şifre" });

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName,
                model.Sifre,
                isPersistent: false,
                lockoutOnFailure: false);

            if (!result.Succeeded)
                return Unauthorized(new { Error = "Geçersiz kullanıcı adı veya şifre" });

            // Kullanıcı bilgilerini döndür
            return Ok(new
            {
                user.Id,
                user.Email,
                user.UserName,
                Roles = await _userManager.GetRolesAsync(user)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login error");
            return StatusCode(500, new { Error = "Bir hata oluştu" });
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation("Kullanıcı çıkış yaptı");
        return Ok(new { Message = "Başarıyla çıkış yapıldı" });
    }
}

public class GirisModel
{
    public string Eposta { get; set; }
    public string Sifre { get; set; }
}