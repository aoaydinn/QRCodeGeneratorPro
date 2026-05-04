using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Image;

namespace QrCodeApp.Core;

public enum BarcodeType
{
    QRCode,
    Code128,
    Code39,
    EAN13,
    EAN8,
    UPCA,
    UPCE,
    ISBN,
    DataMatrix,
    PDF417,
    Aztec
}

public enum ModuleShape
{
    Square,
    Circle,
    RoundedRectangle
}

public enum EyeShape
{
    Square,
    Circle,
    Rounded
}

public enum FrameStyle
{
    None,
    Standard,
    Rounded
}

public class BarcodeGenerator
{
    public static Bitmap GenerateBarcode(
        string content,
        BarcodeType barcodeType = BarcodeType.QRCode,
        string eccLevel = "M",
        Color? foreground = null,
        Color? background = null,
        int width = 400,
        int height = 400,
        ModuleShape moduleShape = ModuleShape.Square,
        EyeShape eyeShape = EyeShape.Square,
        FrameStyle frameStyle = FrameStyle.None,
        bool useGradient = false,
        Color? gradientEnd = null)
    {
        foreground ??= Color.Black;
        background ??= Color.White;

        if (barcodeType == BarcodeType.ISBN && content.Length == 10)
        {
            content = ConvertToIsbn13(content);
        }

        if (barcodeType == BarcodeType.QRCode)
        {
            return GenerateCustomQR(content, eccLevel, foreground.Value, background.Value, width, height, moduleShape, eyeShape, frameStyle, useGradient, gradientEnd ?? Color.Gray);
        }

        return GenerateStandardBarcode(content, barcodeType, eccLevel, foreground.Value, background.Value, width, height);
    }

    private static string ConvertToIsbn13(string isbn10)
    {
        string prefix = "978" + isbn10.Substring(0, 9);
        int sum = 0;
        for (int i = 0; i < 12; i++)
        {
            int digit = prefix[i] - '0';
            sum += (i % 2 == 0) ? digit : digit * 3;
        }
        int checkDigit = (10 - (sum % 10)) % 10;
        return prefix + checkDigit.ToString();
    }

    private static Bitmap GenerateCustomQR(
        string content,
        string eccLevel,
        Color fg,
        Color bg,
        int width,
        int height,
        ModuleShape moduleShape,
        EyeShape eyeShape,
        FrameStyle frameStyle,
        bool useGradient,
        Color gradientEnd)
    {
        var writer = new ZXing.QrCode.QRCodeWriter();
        var bitMatrix = writer.encode(content, BarcodeFormat.QR_CODE, 0, 0);

        int matrixWidth = bitMatrix.Width;
        float moduleSize = (float)width / matrixWidth;

        Bitmap bitmap = new(width, height);
        using Graphics g = Graphics.FromImage(bitmap);
        
        g.Clear(bg);
        g.SmoothingMode = SmoothingMode.AntiAlias;

        using Brush fgBrush = useGradient 
            ? new LinearGradientBrush(new Rectangle(0, 0, width, height), fg, gradientEnd, LinearGradientMode.ForwardDiagonal)
            : new SolidBrush(fg);

        for (int y = 0; y < matrixWidth; y++)
        {
            for (int x = 0; x < matrixWidth; x++)
            {
                if (!bitMatrix[x, y]) continue;

                if (IsEyeArea(x, y, matrixWidth)) continue;

                float xPos = x * moduleSize;
                float yPos = y * moduleSize;

                DrawModule(g, xPos, yPos, moduleSize, fgBrush, moduleShape);
            }
        }

        // Gözleri (Eyes) özel olarak çiziyoruz
        float eyeOuterSize = moduleSize * 7;
        DrawEyeModule(g, 0, 0, eyeOuterSize, fgBrush, eyeShape); // Sol üst
        DrawEyeModule(g, (matrixWidth - 7) * moduleSize, 0, eyeOuterSize, fgBrush, eyeShape); // Sağ üst
        DrawEyeModule(g, 0, (matrixWidth - 7) * moduleSize, eyeOuterSize, fgBrush, eyeShape); // Sol alt

        if (frameStyle != FrameStyle.None)
        {
            DrawFrame(g, width, height, frameStyle, fg);
        }

        return bitmap;
    }

    private static void DrawModule(Graphics g, float x, float y, float size, Brush brush, ModuleShape shape)
    {
        switch (shape)
        {
            case ModuleShape.Circle:
                g.FillEllipse(brush, x, y, size, size);
                break;
            case ModuleShape.RoundedRectangle:
                using (GraphicsPath path = new GraphicsPath())
                {
                    float radius = size * 0.2f;
                    path.AddArc(x, y, radius, radius, 180, 90);
                    path.AddArc(x + size - radius, y, radius, radius, 270, 90);
                    path.AddArc(x + size - radius, y + size - radius, radius, radius, 0, 90);
                    path.AddArc(x, y + size - radius, radius, radius, 90, 90);
                    path.CloseFigure();
                    g.FillPath(brush, path);
                }
                break;
            default:
                g.FillRectangle(brush, x, y, size, size);
                break;
        }
    }

    private static void DrawEyeModule(Graphics g, float x, float y, float size, Brush brush, EyeShape shape)
    {
        float moduleSize = size / 7f;
        float outerSize = size;
        float innerSize = moduleSize * 5;
        float centerSize = moduleSize * 3;
        float innerOffset = moduleSize;
        float centerOffset = moduleSize * 2;

        DrawEyeLayer(g, brush, shape, x, y, outerSize);

        using (SolidBrush whiteBrush = new SolidBrush(Color.White))
        {
            DrawEyeLayer(g, whiteBrush, shape, x + innerOffset, y + innerOffset, innerSize);
        }

        DrawEyeLayer(g, brush, shape, x + centerOffset, y + centerOffset, centerSize);
    }

    private static void DrawEyeLayer(Graphics g, Brush brush, EyeShape shape, float x, float y, float size)
    {
        switch (shape)
        {
            case EyeShape.Circle:
                g.FillEllipse(brush, x, y, size, size);
                break;
            case EyeShape.Rounded:
                using (GraphicsPath path = new GraphicsPath())
                {
                    float radius = size * 0.2f;
                    path.AddArc(x, y, radius, radius, 180, 90);
                    path.AddArc(x + size - radius, y, radius, radius, 270, 90);
                    path.AddArc(x + size - radius, y + size - radius, radius, radius, 0, 90);
                    path.AddArc(x, y + size - radius, radius, radius, 90, 90);
                    path.CloseFigure();
                    g.FillPath(brush, path);
                }
                break;
            default:
                g.FillRectangle(brush, x, y, size, size);
                break;
        }
    }

    private static bool IsEyeArea(int x, int y, int modules)
    {
        int eyeSize = 7;
        return (x < eyeSize && y < eyeSize) ||
               (x < eyeSize && y >= modules - eyeSize) ||
               (x >= modules - eyeSize && y < eyeSize);
    }

    private static void DrawFrame(Graphics g, int width, int height, FrameStyle frameStyle, Color fg)
    {
        using Pen pen = new(fg, 3);
        int margin = 10;

        switch (frameStyle)
        {
            case FrameStyle.Standard:
                g.DrawRectangle(pen, margin, margin, width - margin * 2, height - margin * 2);
                break;
            case FrameStyle.Rounded:
                g.DrawEllipse(pen, margin, margin, width - margin * 2, height - margin * 2);
                break;
        }
    }

    private static Bitmap GenerateStandardBarcode(
        string content,
        BarcodeType barcodeType,
        string eccLevel,
        Color fg,
        Color bg,
        int width,
        int height)
    {
        try
        {
            var writer = new ZXing.MultiFormatWriter();
            var bitMatrix = writer.encode(content, GetBarcodeFormat(barcodeType), width, height);

            Bitmap bitmap = new(width, height);
            using Graphics g = Graphics.FromImage(bitmap);
            g.Clear(bg);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int matrixWidth = bitMatrix.Width;
            float moduleWidth = (float)width / matrixWidth;
            float moduleHeight = (float)height / bitMatrix.Height;

            for (int y = 0; y < bitMatrix.Height; y++)
            {
                for (int x = 0; x < matrixWidth; x++)
                {
                    if (bitMatrix[x, y])
                    {
                        using Brush brush = new SolidBrush(fg);
                        g.FillRectangle(brush, x * moduleWidth, y * moduleHeight, moduleWidth, moduleHeight);
                    }
                }
            }

            return bitmap;
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Barkod oluşturulamadı: {ex.Message}");
        }
    }

    public static string GenerateSvgString(string content, BarcodeType barcodeType, string eccLevel, int size)
    {
        if (barcodeType != BarcodeType.QRCode)
        {
                // For non-QR types, fall back to PNG embedding
            var bitmap = GenerateBarcode(content, barcodeType, eccLevel, Color.Black, Color.White, size, size);
            string base64;
            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                base64 = Convert.ToBase64String(ms.ToArray());
            }
            return $"<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"{size}\" height=\"{size}\"><image width=\"{size}\" height=\"{size}\" href=\"data:image/png;base64,{base64}\" /></svg>";
        }

        var writer = new ZXing.QrCode.QRCodeWriter();
        var bitMatrix = writer.encode(content, BarcodeFormat.QR_CODE, size, size);

        int matrixWidth = bitMatrix.Width;
        float moduleSize = (float)size / matrixWidth;

        var svg = new StringBuilder();
        svg.AppendLine($"<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"{size}\" height=\"{size}\">");
        svg.AppendLine($"  <rect width=\"100%\" height=\"100%\" fill=\"white\"/>");

        for (int y = 0; y < matrixWidth; y++)
        {
            for (int x = 0; x < matrixWidth; x++)
            {
                if (bitMatrix[x, y])
                {
                    float xPos = x * moduleSize;
                    float yPos = y * moduleSize;
                    svg.AppendLine($"  <rect x=\"{xPos}\" y=\"{yPos}\" width=\"{moduleSize}\" height=\"{moduleSize}\" fill=\"black\"/>");
                }
            }
        }

        svg.AppendLine("</svg>");
        return svg.ToString();
    }

    public static void SaveAsSVG(Bitmap bitmap, string filePath, string? svgContent = null)
    {
        try
        {
            if (!string.IsNullOrEmpty(svgContent))
            {
                File.WriteAllText(filePath, svgContent);
            }
            else
            {
                // Fall back: save as PNG with Base64 embedding
                using var stream = new MemoryStream();
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                string base64 = Convert.ToBase64String(stream.ToArray());
                File.WriteAllText(filePath, $"<svg xmlns=\"http://www.w3.org/2000/svg\"><image width=\"{bitmap.Width}\" height=\"{bitmap.Height}\" href=\"data:image/png;base64,{base64}\" /></svg>");
            }
        }
        catch
        {
            // Fallback: save as PNG
            bitmap.Save(filePath.Replace(".svg", ".png"));
        }
    }

    public static void SaveAsPDF(Bitmap bitmap, string filePath)
    {
        using var writer = new PdfWriter(filePath);
        using var pdf = new PdfDocument(writer);
        
        // Setup document size according to image
        var pageSize = new iText.Kernel.Geom.PageSize(bitmap.Width, bitmap.Height);
        pdf.SetDefaultPageSize(pageSize);
        
        using var document = new Document(pdf);
        document.SetMargins(0, 0, 0, 0);

        using var stream = new MemoryStream();
        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        stream.Position = 0;

        var imageData = ImageDataFactory.Create(stream.ToArray());
        var image = new iText.Layout.Element.Image(imageData);
        image.SetAutoScale(false);

        document.Add(image);
    }

    private static BarcodeFormat GetBarcodeFormat(BarcodeType type)
    {
        return type switch
        {
            BarcodeType.QRCode => BarcodeFormat.QR_CODE,
            BarcodeType.Code128 => BarcodeFormat.CODE_128,
            BarcodeType.Code39 => BarcodeFormat.CODE_39,
            BarcodeType.EAN13 => BarcodeFormat.EAN_13,
            BarcodeType.EAN8 => BarcodeFormat.EAN_8,
            BarcodeType.UPCA => BarcodeFormat.UPC_A,
            BarcodeType.UPCE => BarcodeFormat.UPC_E,
            BarcodeType.ISBN => BarcodeFormat.EAN_13,
            BarcodeType.DataMatrix => BarcodeFormat.DATA_MATRIX,
            BarcodeType.PDF417 => BarcodeFormat.PDF_417,
            BarcodeType.Aztec => BarcodeFormat.AZTEC,
            _ => BarcodeFormat.QR_CODE
        };
    }

    public static string[] GetBarcodeTypeNames()
    {
        return Enum.GetNames(typeof(BarcodeType));
    }

    public static string[] GetModuleShapeNames()
    {
        return new[] 
        { 
            LanguageManager.GetString("ModuleSquare"), 
            LanguageManager.GetString("ModuleCircle"), 
            LanguageManager.GetString("ModuleRoundedRect") 
        };
    }

    public static string[] GetEyeShapeNames()
    {
        return new[] 
        { 
            LanguageManager.GetString("EyeSquare"), 
            LanguageManager.GetString("EyeCircle"), 
            LanguageManager.GetString("EyeRounded") 
        };
    }

    public static string[] GetFrameStyles()
    {
        return new[] 
        { 
            LanguageManager.GetString("FrameNone"), 
            LanguageManager.GetString("FrameStandard"), 
            LanguageManager.GetString("FrameRounded") 
        };
    }

    public static bool ValidateContent(string content, BarcodeType type)
    {
        if (string.IsNullOrWhiteSpace(content)) return false;

        return type switch
        {
            BarcodeType.EAN13 => content.Length == 13 && content.All(char.IsDigit),
            BarcodeType.ISBN => (content.Length == 13 || content.Length == 10) && content.All(char.IsDigit),
            BarcodeType.EAN8 => content.Length == 8 && content.All(char.IsDigit),
            BarcodeType.UPCA => content.Length == 12 && content.All(char.IsDigit),
            BarcodeType.UPCE => content.Length == 6 && content.All(char.IsDigit),
            _ => true
        };
    }
}
