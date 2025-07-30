# \# İzin Takip Sistemi

# 

# İzin Takip Sistemi, çalışanların izin taleplerini kolayca yönetebilecekleri, yöneticilerin izin taleplerini onaylayabilecekleri, departman ve kullanıcı yönetimini gerçekleştirebilecekleri bir İnsan Kaynakları (IK) uygulamasıdır.

# 

# \## Kullanılan Teknolojiler

# 

# \- \*\*Frontend:\*\* HTML, CSS, JavaScript, Bootstrap (Bazı sayfalar mobil uyumludur)

# \- \*\*Backend:\*\* ASP.NET Core MVC, C#, Entity Framework Core

# \- \*\*Veritabanı:\*\* SQL Server (Database bağlantısı `appsettings.json` dosyasından ayarlanabilir)

# 

# \## Özellikler

# 

# \- Çalışanların izin taleplerini oluşturması ve kendi izin listelerini görüntüleyebilmesi.

# \- Yönetici panelinden izin taleplerini onaylama .

# &nbsp;-İzin türü seçimi (Yıllık, Hastalık, Ücretsiz)

# &nbsp;-Başlangıç ve bitiş tarihleri

# &nbsp;-Otomatik iş günü hesaplama

# \- Kullanıcı yönetimi ve yetki kontrolü.

# \- Departman ve izin türü tanımlama.

# \- Yıllık maksimum izin gün sayısını ayarlama.

# 

# \## Ekran Görüntüleri

# 

# \### Yıllık Maksimum İzin Gün Sayısı Ayarı

# 

# 

# 

# \### İzin Listesi (Yetki kontrolü uygulanmış sayfa)

# 

# 

# 

# \### Kullanıcı Profil Sayfası

# 

# 

# 

# \## Projeyi Çalıştırma Adımları

# 

# 1\. Repository'i klonlayın:

# 

# &nbsp;  ```bash

# &nbsp;  git clone https://github.com/Barisbozbas/IKPanelipProje.git

# &nbsp;  ```

# 

# 2\. Proje dizinine gidin:

# 

# &nbsp;  ```bash

# &nbsp;  cd IKPanelipProje/IzinTakipProject

# &nbsp;  ```

# 

# 3\. `appsettings.json` dosyasında veritabanı bağlantı bilgilerini kendi ortamınıza göre güncelleyin.

# 

# 4\. Entity Framework ile veritabanını oluşturun ve migrasyonları uygulayın:

# 

# &nbsp;  ```bash

# &nbsp;  update-database

# &nbsp;  ```

# 

# 5\. Uygulamayı başlatın.

# 

# 



