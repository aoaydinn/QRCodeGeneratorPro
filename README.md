# QR Kod Oluşturucu Pro (QR Code Generator Pro)

**Versiyon:** v1.0.0  
**Geliştirici:** aoaydin  
**Platform:** .NET 10 (Windows Forms Uygulaması)  

QR Kod Oluşturucu Pro, gelişmiş özelliklere sahip, hem tekil hem de toplu (batch) barkod ve QR kod oluşturmanıza olanak tanıyan profesyonel bir masaüstü uygulamasıdır. Modern mimarisi, zengin özelleştirme seçenekleri ve yerleşik API sunucusu ile kişisel veya kurumsal projelerinizde kullanılmak üzere tasarlanmıştır.

## 🚀 Temel Özellikler

- **Çoklu Barkod Desteği:** Yalnızca QR Kod değil; `Code128`, `EAN13`, `UPCA`, `Aztec`, `DataMatrix` ve `PDF417` formatlarında da barkodlar oluşturun.
- **Gelişmiş Arayüz ve Tasarım:**
  - **Modül Şekilleri:** Kare, Daire ve Yuvarlak Dikdörtgen.
  - **Göz (Eye) Şekilleri:** Klasik Kare, Oval/Daire, Çember Çerçeveli vb. özel desenler.
  - **Dış Çerçeveler:** Barkodlarınızın etrafına Düz, Çift veya Kalın özel çerçeveler ekleme imkanı.
  - **Renkler ve Gradyanlar:** Ön plan ve arka plan renklerini özelleştirebilir veya arka planda pürüzsüz gradyan (renk geçişi) kullanabilirsiniz.
- **Dinamik Logo Ekleme:** QR kodlarınızın ortasına şirket logonuzu veya ikonunuzu ekleyin. Logo boyutu, seçtiğiniz Hata Düzeltme Seviyesine (L, M, Q, H) göre otomatik ve güvenli bir şekilde ölçeklenir.
- **Şifreleme (Encryption):** Oluşturulan içeriklere gizlilik katmak isterseniz "Şifrele" seçeneği ile verilerinizi AES standartlarında koruma altına alabilirsiniz.
- **Toplu Üretim (Batch Processing):** Yüzlerce veya binlerce satırlık CSV dosyalarınızı sisteme yükleyerek, tüm kodları saniyeler içerisinde tek bir tıkla belirlediğiniz klasöre çıkartabilirsiniz.
- **Dahili API Sunucusu:** Uygulama içerisindeki API butonunu aktif hale getirerek, uygulamanızı yerel bir sunucuya (localhost:5000) dönüştürebilir, dış platformlardan (web veya diğer otomasyon sistemlerinden) HTTP istekleriyle barkod ürettirebilirsiniz.
- **Çoklu Dil Desteği:** Türkçe (TR) ve İngilizce (EN) dillerini anlık olarak tek tıkla değiştirebilirsiniz.
- **Dışa Aktarma Formatları:** Ürettiğiniz kodları yüksek kalitede `PNG`, `JPEG`, `SVG` veya `PDF` olarak kaydedin.

## 🛠 Kullanım Adımları

### 1. Tekil QR Kod Üretimi
1. Programı açın ve **İçerik** kutusuna metninizi, linkinizi veya verinizi girin.
2. (Opsiyonel) Eğer veriyi gizlemek isterseniz **Şifrele** kutusunu işaretleyip bir şifre belirleyin.
3. **Barkod Yapısı** kısmından barkod tipini ve Hata Düzeltme oranını (ECC) belirleyin.
4. **Görünüm** menüsü altından göz şeklini, modül şeklini ve renkleri (gradyan dahil) dilediğiniz gibi tasarlayın. Logo eklemek isterseniz `Logo Ekle` butonunu kullanın.
5. **Oluştur** butonuna tıklayarak önizlemeyi görün ve **Kaydet** ile istediğiniz formatta dışa aktarın.

### 2. Toplu Kod Üretimi (Batch)
1. Ana ekrandaki **Toplu İşlem** butonuna tıklayarak Toplu İşlem Modülünü açın.
2. **CSV Yükle** butonuna basarak, içeriğinde "İçerik, DosyaAdı" sütunları bulunan bir CSV dosyasını projeye dahil edin.
3. Barkod özelliklerini ayarlayın ve çıktıların toplanacağı bir klasör seçin.
4. **Toplu Oluştur** butonuna basıp işlemin otomatik olarak bitmesini bekleyin.

### 3. API Kullanımı
1. Ana ekrandaki **API Başlat** butonuna basın. (Buton metni "API Durdur" olarak değişecek ve sağ altta API'nin hangi adreste aktif olduğu yazacaktır).
2. Sistem, varsayılan olarak `5000` portunu deneyecektir. Eğer bu port doluysa veya işletim sisteminiz engelliyorsa otomatik olarak bir sonraki boş portu (`5001`, `5002` vb.) bularak başlatacaktır. Lütfen **sağ alttaki yeşil bildirimde yazan port numarasını** dikkate alın.
3. Herhangi bir tarayıcıdan, Postman veya benzeri bir HTTP istemcisinden uygulamanıza POST isteği atarak QR kod üretebilirsiniz.  
   **Örnek Kullanım (POST `http://localhost:<PORT_NUMARASI>/api/qr`):**  
   JSON Body:
   ```json
   {
       "content": "Hello World",
       "barcodeType": "QRCode",
       "ecc": "Q",
       "moduleShape": "RoundRectangle",
       "eyeShape": "Circle",
       "frameStyle": "DoubleBorder",
       "useGradient": true
   }
   ```

## 💻 Sistem Gereksinimleri

- İşletim Sistemi: Windows 10 veya üzeri
- Çalışma Zamanı (Runtime): .NET 10 Desktop Runtime

## 📝 Teknik Mimarisi

- **Model-View-Controller/Designer Deseni:** UI mantığı (Form1, BatchForm) ve UI Tasarımı (Designer.cs dosyaları) katı bir şekilde birbirinden ayrılmıştır.
- **Core Kütüphanesi:** Barkod üretim mantığı (`BarcodeGenerator`), Dil Yönetimi (`LanguageManager`) ve Yerleşik Sunucu (`ApiServer`) bağımsız sınıflara bölünmüş, modüler ve yüksek performanslı bir altyapı oluşturulmuştur.

---

*Geliştirilmiş ve modernize edilmiştir. | 2026*
