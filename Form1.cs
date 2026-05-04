using System.Drawing.Imaging;
using QrCodeApp.Core;

namespace QrCodeApp;

public partial class Form1 : Form
{
    private Bitmap? logoImage = null;
    private Color fgColor = Color.Black;
    private Color bgColor = Color.White;
    private Color gradientEndColor = Color.Gray;
    private string currentContent = "";
    private Core.BarcodeType currentBarcodeType = Core.BarcodeType.QRCode;
    private string currentEcc = "M";

    public Form1()
    {
        InitializeComponent();
        
    }

    
    private void UpdateVisibility()
    {
        bool isQR = cmbBarcodeType.SelectedIndex == 0;
        cmbModuleShape.Visible = isQR;
        cmbEyeShape.Visible = isQR;
        cmbFrame.Visible = isQR;

        foreach (Control c in Controls)
        {
            if (c is Label lbl && (lbl.Text == "Modül Şekli:" || lbl.Text == "Göz Şekli:" || lbl.Text == "Çerçeve:"))
                lbl.Visible = isQR;
        }
    }

    private void BtnLogo_Click(object? sender, EventArgs e)
    {
        using OpenFileDialog dlg = new();
        dlg.Filter = "Resim Dosyaları|*.png;*.jpg;*.jpeg;*.bmp|All Files|*.*";
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            try
            {
                logoImage = new Bitmap(dlg.FileName);
                lblLogo.Text = $"Logo: {Path.GetFileName(dlg.FileName)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Logo yükleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void BtnGenerate_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtInput.Text))
        {
            MessageBox.Show(Core.LanguageManager.GetString("InputWarning"), Core.LanguageManager.GetString("WarningTitle"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            var barcodeType = (BarcodeType)cmbBarcodeType.SelectedIndex;
            string ecc = cmbECC.SelectedItem?.ToString() ?? "M";
            string content = txtInput.Text;

            if (chkEncrypt.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show(Core.LanguageManager.GetString("PasswordWarning"), Core.LanguageManager.GetString("WarningTitle"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                content = Core.EncryptionHelper.WrapInJson(Core.EncryptionHelper.Encrypt(content, txtPassword.Text));
            }

            if (!Core.BarcodeGenerator.ValidateContent(content, barcodeType))
            {
                MessageBox.Show(Core.LanguageManager.GetString("InvalidContentWarning"), Core.LanguageManager.GetString("WarningTitle"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            currentContent = content;
            currentBarcodeType = barcodeType;
            currentEcc = ecc;

            var moduleShape = (ModuleShape)cmbModuleShape.SelectedIndex;
            var eyeShape = (EyeShape)cmbEyeShape.SelectedIndex;
            var frameStyle = (Core.FrameStyle)cmbFrame.SelectedIndex;
            bool useGradient = chkGradient.Checked;

            Bitmap barcode = Core.BarcodeGenerator.GenerateBarcode(
                content,
                barcodeType,
                ecc,
                fgColor,
                bgColor,
                550,
                550,
                moduleShape,
                eyeShape,
                frameStyle,
                useGradient,
                gradientEndColor);

            if (logoImage != null && barcodeType == BarcodeType.QRCode)
            {
                barcode = AddLogoToQR(barcode, logoImage, ecc); // ecc = "L", "M", "Q", "H"
            }

            picQR.Image = barcode;
            btnSave.Enabled = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static Bitmap AddLogoToQR(Bitmap qrImage, Bitmap logo, string eccLevel)
    {
        using Graphics g = Graphics.FromImage(qrImage);

        // Logo boyutu ECC seviyesine göre: L:1/7, M:1/5, Q:1/4, H:1/3
        float ratio = eccLevel.ToUpper() switch
        {
            "L" => 7f,
            "M" => 5f,
            "Q" => 4f,
            "H" => 3f,
            _ => 5f
        };

        int logoSize = (int)(qrImage.Width / ratio);
        int x = (qrImage.Width - logoSize) / 2;
        int y = (qrImage.Height - logoSize) / 2;

        // Beyaz arka plan (hata düzeltme için maskeleme)
        using Brush whiteBrush = new SolidBrush(Color.White);
        g.FillRectangle(whiteBrush, x - 2, y - 2, logoSize + 4, logoSize + 4);

        g.DrawImage(logo, x, y, logoSize, logoSize);
        return qrImage;
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        if (picQR.Image == null) return;

        using SaveFileDialog saveDialog = new();
        string filter = "PNG Dosyası|*.png|JPEG Dosyası|*.jpg|SVG Dosyası|*.svg|PDF Dosyası|*.pdf|Tüm Dosyalar|*.*";
        saveDialog.Filter = filter;
        saveDialog.Title = "Barkod Kaydet";
        saveDialog.FileName = "barcode";

        if (saveDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                string ext = Path.GetExtension(saveDialog.FileName).ToLower();
                if (ext == ".svg")
                {
                    string svg = Core.BarcodeGenerator.GenerateSvgString(currentContent, currentBarcodeType, currentEcc, 550);
                    File.WriteAllText(saveDialog.FileName, svg);
                }
                else if (ext == ".pdf")
                {
                    Core.BarcodeGenerator.SaveAsPDF((Bitmap)picQR.Image, saveDialog.FileName);
                }
                else
                {
                    ImageFormat format = ext == ".jpg" ? ImageFormat.Jpeg : ImageFormat.Png;
                    picQR.Image.Save(saveDialog.FileName, format);
                }
                MessageBox.Show(Core.LanguageManager.GetString("SaveSuccess"), Core.LanguageManager.GetString("SuccessTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
             {
                MessageBox.Show($"{Core.LanguageManager.GetString("SaveError")} {ex.Message}", Core.LanguageManager.GetString("ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void ChangeLanguage()
    {
        if (cmbLanguage == null) return;

        bool isEnglish = cmbLanguage.SelectedIndex == 1;
        Core.LanguageManager.IsEnglish = isEnglish;

        Text = Core.LanguageManager.GetString("FormTitle");
        lblTitle.Text = Core.LanguageManager.GetString("AppTitle");
        txtInput.PlaceholderText = Core.LanguageManager.GetString("InputPlaceholder");
        txtPassword.PlaceholderText = Core.LanguageManager.GetString("PasswordPlaceholder");
        toolTip.SetToolTip(chkEncrypt, Core.LanguageManager.GetString("EncryptTooltip"));
        
        grpContent.Text = Core.LanguageManager.GetString("ContentGroup");
        grpBarcode.Text = Core.LanguageManager.GetString("BarcodeGroup");
        grpAppearance.Text = Core.LanguageManager.GetString("AppearanceGroup");
        grpExport.Text = Core.LanguageManager.GetString("ExportGroup");

        int currentModule = cmbModuleShape.SelectedIndex;
        int currentEye = cmbEyeShape.SelectedIndex;
        int currentFrame = cmbFrame.SelectedIndex;

        cmbModuleShape.Items.Clear();
        cmbModuleShape.Items.AddRange(Core.BarcodeGenerator.GetModuleShapeNames());
        if (currentModule >= 0 && currentModule < cmbModuleShape.Items.Count) cmbModuleShape.SelectedIndex = currentModule;

        cmbEyeShape.Items.Clear();
        cmbEyeShape.Items.AddRange(Core.BarcodeGenerator.GetEyeShapeNames());
        if (currentEye >= 0 && currentEye < cmbEyeShape.Items.Count) cmbEyeShape.SelectedIndex = currentEye;

        cmbFrame.Items.Clear();
        cmbFrame.Items.AddRange(Core.BarcodeGenerator.GetFrameStyles());
        if (currentFrame >= 0 && currentFrame < cmbFrame.Items.Count) cmbFrame.SelectedIndex = currentFrame;

        UpdateLanguageForControls(Controls);
    }

    private void UpdateLanguageForControls(Control.ControlCollection controls)
    {
        foreach (Control c in controls)
        {
            if (c is Label lbl)
            {
                if (lbl.Name == "lblApiStatus")
                {
                    lbl.Text = Core.ApiServer.IsRunning ? Core.LanguageManager.GetString("ApiRunning") : Core.LanguageManager.GetString("ApiOff");
                }
                else if (lbl.Text.Contains(":") && lbl.Name != "lblLogo")
                {
                    // Re-assign texts via matching old tags or just check location isn't as reliable anymore with GroupBoxes,
                    // but we can just identify by their previous state, or better yet, since they are in GroupBoxes, 
                    // we can just re-assign. Actually let's use their locations in the groupbox or their names if possible.
                    // Wait, I didn't assign names. Let's just do a Contains match to avoid resetting user data:
                    if (lbl.Location.Y == 25)
                    {
                        if (c.Parent == grpBarcode && lbl.Location.X == 15) lbl.Text = Core.LanguageManager.GetString("BarcodeType");
                        if (c.Parent == grpBarcode && lbl.Location.X == 205) lbl.Text = Core.LanguageManager.GetString("ErrorCorrection");
                        if (c.Parent == grpAppearance && lbl.Location.X == 15) lbl.Text = Core.LanguageManager.GetString("ModuleShape");
                        if (c.Parent == grpAppearance && lbl.Location.X == 140) lbl.Text = Core.LanguageManager.GetString("EyeShape");
                        if (c.Parent == grpAppearance && lbl.Location.X == 265) lbl.Text = Core.LanguageManager.GetString("Frame");
                        if (c.Parent == grpExport && lbl.Location.X == 15) lbl.Text = Core.LanguageManager.GetString("ExportFormat");
                    }
                }
            }
            else if (c is Button btn)
            {
                if (btn == btnGenerate) btn.Text = Core.LanguageManager.GetString("GenerateBtn");
                else if (btn == btnSave) btn.Text = Core.LanguageManager.GetString("SaveBtn");
                else if (btn.Name == "btnApi") btn.Text = Core.ApiServer.IsRunning ? Core.LanguageManager.GetString("StopApiBtn") : Core.LanguageManager.GetString("StartApiBtn");
                else if (btn == btnLogo) btn.Text = Core.LanguageManager.GetString("AddLogoBtn");
                else if (btn == btnFgColor) btn.Text = Core.LanguageManager.GetString("ForeColorBtn");
                else if (btn == btnBgColor) btn.Text = Core.LanguageManager.GetString("BackColorBtn");
                else if (btn == btnGradientEndColor) btn.Text = Core.LanguageManager.GetString("GradientEndBtn");
                else if (btn.Location.X == 15 && btn.Location.Y == 140) btn.Text = Core.LanguageManager.GetString("BatchProcessBtn");
            }
            else if (c is CheckBox chk)
            {
                if (chk == chkEncrypt) chk.Text = Core.LanguageManager.GetString("EncryptBtn");
                else if (chk == chkGradient) chk.Text = Core.LanguageManager.GetString("GradientChk");
            }

            if (c.HasChildren)
            {
                UpdateLanguageForControls(c.Controls);
            }
        }
    }

    private void BtnApi_Click(object? sender, EventArgs e)
    {
        var btnApi = grpExport.Controls.OfType<Button>().FirstOrDefault(c => c.Name == "btnApi");
        var lblApiStatus = Controls.OfType<Label>().FirstOrDefault(c => c.Name == "lblApiStatus");

        if (Core.ApiServer.IsRunning)
        {
            Core.ApiServer.Stop();
            if (btnApi != null) btnApi.Text = Core.LanguageManager.GetString("StartApiBtn");
            if (lblApiStatus != null) { lblApiStatus.Text = Core.LanguageManager.GetString("ApiOff"); lblApiStatus.ForeColor = Color.Red; }
        }
        else
        {
            try
            {
                Core.ApiServer.Start();
                if (btnApi != null) btnApi.Text = Core.LanguageManager.GetString("StopApiBtn");
                if (lblApiStatus != null) { lblApiStatus.Text = $"{Core.LanguageManager.GetString("ApiRunning")} (localhost:{Core.ApiServer.Port})"; lblApiStatus.ForeColor = Color.Green; }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"API başlatılamadı: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
