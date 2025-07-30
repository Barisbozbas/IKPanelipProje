# 🚀 İzin Takip Sistemi

İzin Takip Sistemi, şirketlerin ve çalışanların izin süreçlerini basitleştiren kullanıcı dostu bir İnsan Kaynakları (IK) uygulamasıdır. Çalışanlar izin taleplerini kolayca oluşturabilir, Admin yetkisine sahip kullanıcılar ise bu talepleri yönetip onaylayabilirler.

## 🛠 Kullanılan Teknolojiler

- 🌐 **Frontend:** HTML, CSS, JavaScript, Bootstrap (Bazı sayfalar mobil uyumludur)
- 🖥 **Backend:** ASP.NET Core MVC, C#, Entity Framework Core
- 💾 **Veritabanı:** SQL Server (Database bağlantısı `appsettings.json` üzerinden yapılandırılabilir)

## ✨ Özellikler

- ✅ Çalışanların izin taleplerini oluşturabilmesi ve kendi izin listelerini görüntüleyebilmesi.
- ✅ Yönetici panelinden izin taleplerinin onaylanması .
- ✅ İzin türü seçimi (Yıllık, Hastalık, Ücretsiz).
- ✅ Başlangıç ve bitiş tarihleri ile otomatik iş günü hesaplama.
- ✅ Kullanıcı yönetimi ve yetki kontrol sistemi.
- ✅ Departman ve izin türü tanımlama.
- ✅ Yıllık maksimum izin gün sayısını ayarlayabilme.


## 🚩 Projeyi Nasıl Çalıştırırım?

1. Projeyi klonlayın:

```bash
git clone https://github.com/Barisbozbas/IKPanelipProje.git
```

2. Proje klasörüne gidin:

```bash
cd IKPanelipProje/IzinTakipProject
```

3. `appsettings.json` içindeki veritabanı ayarlarını kendi ortamınıza uygun olarak güncelleyin.

4. Entity Framework ile veritabanınızı oluşturun:

```bash
update-database
```

5. Uygulamayı başlatın ve kullanmaya başlayın!
