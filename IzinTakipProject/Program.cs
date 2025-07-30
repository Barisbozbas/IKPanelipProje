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

// MVC Controller'lar� i�in gerekli servisler
builder.Services.AddControllers();

// Swagger servisini ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "�zin Takip API", Version = "v1" });
});

// Veritaban� ba�lant�s�
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity yap�land�rmas�
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

// Cookie ayarlar�
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

// HttpClient i�in base address ayar� ve API client olu�turma
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5009/");  // API adresin
});

// IHttpClientFactory'den ApiClient'� sa�layacak scoped servis
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));

var app = builder.Build();

// Geli�tirme ortam�nda Swagger'� etkinle�tir
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "�zin Takip API V1");
    });
}

// Varsay�lan kullan�c� ve roller olu�tur
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
