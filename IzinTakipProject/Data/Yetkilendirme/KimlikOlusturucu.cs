using IzinTakipProject.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IzinTakipProject.Data.Yetkilendirme
{

    public static class KimlikOlusturucu
    {
        public static async Task VarsayilanKullanicilariVeRolleriEkle(IServiceProvider hizmetSaglayici)
        {
            var userManager = hizmetSaglayici.GetRequiredService<UserManager<Kullanici>>();
            var roleManager = hizmetSaglayici.GetRequiredService<RoleManager<Rol>>();
            var dbContext = hizmetSaglayici.GetRequiredService<ApplicationDbContext>();

            // 1. Departmanları seed et
            if (!dbContext.Departmanlar.Any())
            {
                dbContext.Departmanlar.AddRange(
                           new Departman { Ad = "Bilgi İşlem" },
                           new Departman { Ad = "İnsan Kaynakları" },
                           new Departman { Ad = "Muhasebe" },
                            new Departman { Ad = "Satış" }
                );
                dbContext.SaveChanges();
            }

            // 2. Roller varsa oluşturma
            string[] roller = { "Admin", "Yonetici", "User" };
            foreach (var rol in roller)
            {
                if (!await roleManager.RoleExistsAsync(rol))
                {
                    await roleManager.CreateAsync(new Rol { Name = rol });
                }
            }

            // 3. Admin kullanıcı oluştur
            var adminEmail = "BilgiAdmin@panel.com";
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                var yeniAdmin = new Kullanici
                {
                    UserName = "BilgiAdmin",
                    Email = adminEmail,
                    EmailConfirmed = true,
                    AdSoyad = "Sistem Yöneticisi",
                    DepartmanId = 1 // Örnek: Bilgi İşlem (varsa)
                };

                var sonuc = await userManager.CreateAsync(yeniAdmin, "Admin123!");

                if (sonuc.Succeeded)
                {
                    await userManager.AddToRoleAsync(yeniAdmin, "Admin");
                }
            }
        }
    }
}
