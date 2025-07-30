using IzinTakipProject.Components;
using IzinTakipProject.Data;
using IzinTakipProject.Data.Entities;
using IzinTakipProject.Data.Yetkilendirme;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Microsoft.OpenApi.Models;
using Blazored.LocalStorage;
using IzinTakipProject.Servisler.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Razor Pages ve Blazor Server servisleri
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// MVC Controller'larý için gerekli servisler
builder.Services.AddControllers();

// Swagger servisini ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ýzin Takip API", Version = "v1" });
});

// Veritabaný baðlantýsý
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity yapýlandýrmasý
builder.Services.AddIdentity<Kullanici, Rol>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Cookie ayarlarý
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/giris";
    options.AccessDeniedPath = "/yetkisiz";
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
});

// HttpContextAccessor servisi
builder.Services.AddHttpContextAccessor();

// Custom AuthenticationStateProvider servisi
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

// Radzen servisleri
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<IIzinServisi, IzinTakipProject.Servisler.Applications.IzinServisi>();

builder.Services.AddBlazoredLocalStorage();

// HttpClient için base address ayarý ve API client oluþturma
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5009/");  // API adresin
});

// IHttpClientFactory'den ApiClient'ý saðlayacak scoped servis
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));

var app = builder.Build();

// Geliþtirme ortamýnda Swagger'ý etkinleþtir
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ýzin Takip API V1");
    });
}

// Varsayýlan kullanýcý ve roller oluþtur
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await KimlikOlusturucu.VarsayilanKullanicilariVeRolleriEkle(services);
}

// Middleware pipeline

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Hata");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
