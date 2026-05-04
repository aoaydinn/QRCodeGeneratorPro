namespace QrCodeApp.Core;

public static class LanguageManager
{
    public static bool IsEnglish { get; set; } = false;

    public static string GetString(string key)
    {
        if (IsEnglish && StringsEn.TryGetValue(key, out string? enValue))
        {
            return enValue;
        }
        
        if (StringsTr.TryGetValue(key, out string? trValue))
        {
            return trValue;
        }

        return key; // Fallback
    }

    private static readonly Dictionary<string, string> StringsTr = new()
    {
        // UI
        { "FormTitle", "QR Kod Oluşturucu Pro" },
        { "AppTitle", "QR Kod Oluşturucu Pro" },
        { "BarcodeType", "Barkod Türü:" },
        { "ErrorCorrection", "Hata Düzeltme:" },
        { "ModuleShape", "Modül Şekli:" },
        { "EyeShape", "Göz Şekli:" },
        { "Frame", "Çerçeve:" },
        { "ApiStarted", "API Sunucusu {0} portunda dinliyor..." },
        { "ApiOff", "API Kapalı" },
        // Combo Box Items
        { "ModuleSquare", "Kare" },
        { "ModuleCircle", "Daire" },
        { "ModuleRoundedRect", "Yuvarlak Dikdörtgen" },
        { "EyeSquare", "Kare" },
        { "EyeCircle", "Daire" },
        { "EyeRounded", "Yuvarlak" },
        { "FrameNone", "Yok" },
        { "FrameStandard", "Standart Çerçeve" },
        { "FrameRounded", "Yuvarlak Çerçeve" },
        { "ApiRunning", "API: Çalışıyor (localhost:5000)" },
        { "GenerateBtn", "Oluştur" },
        { "SaveBtn", "Kaydet" },
        { "BatchProcessBtn", "Toplu İşleme" },
        { "StartApiBtn", "API Başlat" },
        { "StopApiBtn", "API Durdur" },
        { "AddLogoBtn", "Logo Ekle" },
        { "ForeColorBtn", "Ön Renk" },
        { "BackColorBtn", "Arka Renk" },
        { "GradientEndBtn", "Gradyan Son" },
        { "EncryptBtn", "Şifrele" },
        { "GradientChk", "Gradyan" },
        
        // Placeholders and Tooltips
        { "InputPlaceholder", "Metin veya URL girin..." },
        { "PasswordPlaceholder", "Şifre..." },
        { "EncryptTooltip", "Not: Şifreli QR kodlar yalnızca bu uygulamanın çözme özelliği ile okunabilir. Standart okuyucular ham JSON metnini gösterir." },
        
        // Messages
        { "InputWarning", "Lütfen bir metin veya URL girin!" },
        { "WarningTitle", "Uyarı" },
        { "PasswordWarning", "Şifreleme için şifre girin!" },
        { "InvalidContentWarning", "Girilen veri seçilen barkod türü için uygun değil!" },
        { "ErrorTitle", "Hata" },
        { "SaveSuccess", "Barkod başarıyla kaydedildi!" },
        { "SuccessTitle", "Başarılı" },
        { "ApiStartError", "API başlatılamadı:" },
        { "LogoLoadError", "Logo yükleme hatası:" },
        { "SaveError", "Kaydetme hatası:" },
        
        // BatchForm
        { "BatchFormTitle", "Toplu QR Kod Oluşturma" },
        { "LoadCsvBtn", "CSV Yükle" },
        { "OutputDirLabel", "Çıktı Klasörü:" },
        { "BrowseBtn", "Gözat..." },
        { "BatchGenerateBtn", "Toplu Oluştur" },
        { "CsvWarning", "Lütfen önce CSV yükleyin veya tabloya veri girin!" },
        { "OutputDirWarning", "Lütfen geçerli bir çıktı klasörü seçin!" },
        { "Completed", "Tamamlandı!" },
        { "BatchSuccess", "{0} QR kod başarıyla oluşturuldu!" },
        
        // GroupBoxes
        { "ContentGroup", "İçerik" },
        { "BarcodeGroup", "Barkod Yapısı" },
        { "AppearanceGroup", "Görünüm" },
        { "ExportGroup", "Dışa Aktar & İşlemler" }
    };

    private static readonly Dictionary<string, string> StringsEn = new()
    {
        // UI
        { "FormTitle", "QR Code Generator Pro" },
        { "AppTitle", "QR Code Generator Pro" },
        { "BarcodeType", "Barcode Type:" },
        { "ErrorCorrection", "Error Correction:" },
        { "ModuleShape", "Module Shape:" },
        { "EyeShape", "Eye Shape:" },
        { "Frame", "Frame:" },
        { "ApiStarted", "API Server listening on port {0}..." },
        { "ApiOff", "API is Offline" },
        // Combo Box Items
        { "ModuleSquare", "Square" },
        { "ModuleCircle", "Circle" },
        { "ModuleRoundedRect", "Rounded Rectangle" },
        { "EyeSquare", "Square" },
        { "EyeCircle", "Circle" },
        { "EyeRounded", "Rounded" },
        { "FrameNone", "None" },
        { "FrameStandard", "Standard Frame" },
        { "FrameRounded", "Rounded Frame" },
        { "ApiRunning", "API: Running (localhost:5000)" },
        { "GenerateBtn", "Generate" },
        { "SaveBtn", "Save" },
        { "BatchProcessBtn", "Batch Process" },
        { "StartApiBtn", "Start API" },
        { "StopApiBtn", "Stop API" },
        { "AddLogoBtn", "Add Logo" },
        { "ForeColorBtn", "Fore Color" },
        { "BackColorBtn", "Back Color" },
        { "GradientEndBtn", "Gradient End" },
        { "EncryptBtn", "Encrypt" },
        { "GradientChk", "Gradient" },
        
        // Placeholders and Tooltips
        { "InputPlaceholder", "Enter text or URL..." },
        { "PasswordPlaceholder", "Password..." },
        { "EncryptTooltip", "Note: Encrypted QR codes can only be read with this app. Standard readers will show raw JSON." },
        
        // Messages
        { "InputWarning", "Please enter text or a URL!" },
        { "WarningTitle", "Warning" },
        { "PasswordWarning", "Please enter a password for encryption!" },
        { "InvalidContentWarning", "The entered data is not valid for the selected barcode type!" },
        { "ErrorTitle", "Error" },
        { "SaveSuccess", "Barcode saved successfully!" },
        { "SuccessTitle", "Success" },
        { "ApiStartError", "Failed to start API:" },
        { "LogoLoadError", "Error loading logo:" },
        { "SaveError", "Save error:" },
        
        // BatchForm
        { "BatchFormTitle", "Batch QR Code Generation" },
        { "LoadCsvBtn", "Load CSV" },
        { "OutputDirLabel", "Output Folder:" },
        { "BrowseBtn", "Browse..." },
        { "BatchGenerateBtn", "Batch Generate" },
        { "CsvWarning", "Please load a CSV or enter data into the table first!" },
        { "OutputDirWarning", "Please select a valid output folder!" },
        { "Completed", "Completed!" },
        { "BatchSuccess", "{0} QR codes generated successfully!" },
        
        // GroupBoxes
        { "ContentGroup", "Content" },
        { "BarcodeGroup", "Barcode Structure" },
        { "AppearanceGroup", "Appearance" },
        { "ExportGroup", "Export & Actions" }
    };
}
