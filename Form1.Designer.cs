namespace QrCodeApp;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {

        Text = Core.LanguageManager.GetString("FormTitle");
        Size = new Size(920, 720);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = ColorTranslator.FromHtml("#F5F7FA");

        lblTitle.Text = Core.LanguageManager.GetString("AppTitle");
        lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(30, 40, 50);
        lblTitle.AutoSize = true;
        lblTitle.Location = new Point(20, 15);

        // Group 1: İçerik
        grpContent.Text = Core.LanguageManager.GetString("ContentGroup");
        grpContent.Location = new Point(20, 60);
        grpContent.Size = new Size(400, 120);
        grpContent.Font = new Font("Segoe UI", 9, FontStyle.Bold);

        txtInput.Location = new Point(15, 30);
        txtInput.Size = new Size(370, 30);
        txtInput.Font = new Font("Segoe UI", 11);
        txtInput.PlaceholderText = Core.LanguageManager.GetString("InputPlaceholder");

        chkEncrypt.Text = Core.LanguageManager.GetString("EncryptBtn");
        chkEncrypt.Location = new Point(15, 75);
        chkEncrypt.Size = new Size(80, 25);
        chkEncrypt.Font = new Font("Segoe UI", 9);
        chkEncrypt.CheckedChanged += (s, e) => txtPassword.Visible = chkEncrypt.Checked;
        toolTip.SetToolTip(chkEncrypt, Core.LanguageManager.GetString("EncryptTooltip"));

        txtPassword.Location = new Point(100, 73);
        txtPassword.Size = new Size(285, 25);
        txtPassword.Font = new Font("Segoe UI", 10);
        txtPassword.PlaceholderText = Core.LanguageManager.GetString("PasswordPlaceholder");
        txtPassword.Visible = false;
        txtPassword.UseSystemPasswordChar = true;

        grpContent.Controls.Add(txtInput);
        grpContent.Controls.Add(chkEncrypt);
        grpContent.Controls.Add(txtPassword);

        // Group 2: Barkod Yapısı
        grpBarcode.Text = Core.LanguageManager.GetString("BarcodeGroup");
        grpBarcode.Location = new Point(20, 190);
        grpBarcode.Size = new Size(400, 90);
        grpBarcode.Font = new Font("Segoe UI", 9, FontStyle.Bold);

        Label lblType = new() { Text = Core.LanguageManager.GetString("BarcodeType"), Location = new Point(15, 25), AutoSize = true, Font = new Font("Segoe UI", 9) };
        cmbBarcodeType.Location = new Point(15, 45);
        cmbBarcodeType.Size = new Size(180, 25);
        cmbBarcodeType.Font = new Font("Segoe UI", 9);
        cmbBarcodeType.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbBarcodeType.Items.AddRange(Core.BarcodeGenerator.GetBarcodeTypeNames());
        cmbBarcodeType.SelectedIndex = 0;
        cmbBarcodeType.SelectedIndexChanged += (s, e) => UpdateVisibility();

        Label lblECC = new() { Text = Core.LanguageManager.GetString("ErrorCorrection"), Location = new Point(205, 25), AutoSize = true, Font = new Font("Segoe UI", 9) };
        cmbECC.Location = new Point(205, 45);
        cmbECC.Size = new Size(180, 25);
        cmbECC.Font = new Font("Segoe UI", 9);
        cmbECC.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbECC.Items.AddRange(new[] { "L", "M", "Q", "H" });
        cmbECC.SelectedIndex = 1;

        grpBarcode.Controls.Add(lblType);
        grpBarcode.Controls.Add(cmbBarcodeType);
        grpBarcode.Controls.Add(lblECC);
        grpBarcode.Controls.Add(cmbECC);

        // Group 3: Görünüm
        grpAppearance.Text = Core.LanguageManager.GetString("AppearanceGroup");
        grpAppearance.Location = new Point(20, 290);
        grpAppearance.Size = new Size(400, 160);
        grpAppearance.Font = new Font("Segoe UI", 9, FontStyle.Bold);

        Label lblModule = new() { Text = Core.LanguageManager.GetString("ModuleShape"), Location = new Point(15, 25), AutoSize = true, Font = new Font("Segoe UI", 9) };
        cmbModuleShape.Location = new Point(15, 45);
        cmbModuleShape.Size = new Size(115, 25);
        cmbModuleShape.Font = new Font("Segoe UI", 9);
        cmbModuleShape.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbModuleShape.Items.AddRange(Core.BarcodeGenerator.GetModuleShapeNames());
        cmbModuleShape.SelectedIndex = 0;

        Label lblEye = new() { Text = Core.LanguageManager.GetString("EyeShape"), Location = new Point(140, 25), AutoSize = true, Font = new Font("Segoe UI", 9) };
        cmbEyeShape.Location = new Point(140, 45);
        cmbEyeShape.Size = new Size(115, 25);
        cmbEyeShape.Font = new Font("Segoe UI", 9);
        cmbEyeShape.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbEyeShape.Items.AddRange(Core.BarcodeGenerator.GetEyeShapeNames());
        cmbEyeShape.SelectedIndex = 0;

        Label lblFrame = new() { Text = Core.LanguageManager.GetString("Frame"), Location = new Point(265, 25), AutoSize = true, Font = new Font("Segoe UI", 9) };
        cmbFrame.Location = new Point(265, 45);
        cmbFrame.Size = new Size(120, 25);
        cmbFrame.Font = new Font("Segoe UI", 9);
        cmbFrame.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbFrame.Items.AddRange(Core.BarcodeGenerator.GetFrameStyles());
        cmbFrame.SelectedIndex = 0;

        btnFgColor.Text = Core.LanguageManager.GetString("ForeColorBtn");
        btnFgColor.Location = new Point(15, 85);
        btnFgColor.Size = new Size(80, 28);
        btnFgColor.Font = new Font("Segoe UI", 9);
        btnFgColor.Click += (s, e) => { using ColorDialog dlg = new(); if (dlg.ShowDialog() == DialogResult.OK) { fgColor = dlg.Color; pnlFgColor.BackColor = fgColor; } };

        pnlFgColor.Location = new Point(100, 85);
        pnlFgColor.Size = new Size(28, 28);
        pnlFgColor.BackColor = fgColor;
        pnlFgColor.BorderStyle = BorderStyle.FixedSingle;

        btnBgColor.Text = Core.LanguageManager.GetString("BackColorBtn");
        btnBgColor.Location = new Point(140, 85);
        btnBgColor.Size = new Size(80, 28);
        btnBgColor.Font = new Font("Segoe UI", 9);
        btnBgColor.Click += (s, e) => { using ColorDialog dlg = new(); if (dlg.ShowDialog() == DialogResult.OK) { bgColor = dlg.Color; pnlBgColor.BackColor = bgColor; } };

        pnlBgColor.Location = new Point(225, 85);
        pnlBgColor.Size = new Size(28, 28);
        pnlBgColor.BackColor = bgColor;
        pnlBgColor.BorderStyle = BorderStyle.FixedSingle;

        chkGradient.Text = Core.LanguageManager.GetString("GradientChk");
        chkGradient.Location = new Point(275, 85);
        chkGradient.Size = new Size(110, 28);
        chkGradient.Font = new Font("Segoe UI", 9);
        chkGradient.CheckedChanged += (s, e) => pnlGradientEnd.Visible = chkGradient.Checked;

        btnGradientEndColor.Text = Core.LanguageManager.GetString("GradientEndBtn");
        btnGradientEndColor.Location = new Point(15, 120);
        btnGradientEndColor.Size = new Size(80, 28);
        btnGradientEndColor.Font = new Font("Segoe UI", 9);
        btnGradientEndColor.Click += (s, e) => { using ColorDialog dlg = new(); if (dlg.ShowDialog() == DialogResult.OK) { gradientEndColor = dlg.Color; pnlGradientEnd.BackColor = gradientEndColor; } };

        pnlGradientEnd.Location = new Point(100, 120);
        pnlGradientEnd.Size = new Size(28, 28);
        pnlGradientEnd.BackColor = gradientEndColor;
        pnlGradientEnd.BorderStyle = BorderStyle.FixedSingle;
        pnlGradientEnd.Visible = false;

        btnLogo.Text = Core.LanguageManager.GetString("AddLogoBtn");
        btnLogo.Location = new Point(140, 120);
        btnLogo.Size = new Size(80, 28);
        btnLogo.Font = new Font("Segoe UI", 9);
        btnLogo.Click += BtnLogo_Click;

        lblLogo.Text = "Logo: Yok";
        lblLogo.Location = new Point(230, 125);
        lblLogo.AutoSize = true;
        lblLogo.Font = new Font("Segoe UI", 8);
        lblLogo.ForeColor = Color.Gray;

        grpAppearance.Controls.Add(lblModule);
        grpAppearance.Controls.Add(cmbModuleShape);
        grpAppearance.Controls.Add(lblEye);
        grpAppearance.Controls.Add(cmbEyeShape);
        grpAppearance.Controls.Add(lblFrame);
        grpAppearance.Controls.Add(cmbFrame);
        grpAppearance.Controls.Add(btnFgColor);
        grpAppearance.Controls.Add(pnlFgColor);
        grpAppearance.Controls.Add(btnBgColor);
        grpAppearance.Controls.Add(pnlBgColor);
        grpAppearance.Controls.Add(chkGradient);
        grpAppearance.Controls.Add(btnGradientEndColor);
        grpAppearance.Controls.Add(pnlGradientEnd);
        grpAppearance.Controls.Add(btnLogo);
        grpAppearance.Controls.Add(lblLogo);

        // Group 4: Dışa Aktar & İşlemler
        grpExport.Text = Core.LanguageManager.GetString("ExportGroup");
        grpExport.Location = new Point(20, 460);
        grpExport.Size = new Size(400, 200);
        grpExport.Font = new Font("Segoe UI", 9, FontStyle.Bold);

        Label lblExport = new() { Text = Core.LanguageManager.GetString("ExportFormat"), Location = new Point(15, 25), AutoSize = true, Font = new Font("Segoe UI", 9) };
        cmbExportFormat.Location = new Point(15, 45);
        cmbExportFormat.Size = new Size(180, 25);
        cmbExportFormat.Font = new Font("Segoe UI", 9);
        cmbExportFormat.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbExportFormat.Items.AddRange(new[] { "PNG", "JPEG", "SVG", "PDF" });
        cmbExportFormat.SelectedIndex = 0;

        Label lblLang = new() { Text = "Dil / Language:", Location = new Point(205, 25), AutoSize = true, Font = new Font("Segoe UI", 9) };
        cmbLanguage.Location = new Point(205, 45);
        cmbLanguage.Size = new Size(180, 25);
        cmbLanguage.Font = new Font("Segoe UI", 9);
        cmbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbLanguage.Items.AddRange(new[] { "TR", "EN" });
        cmbLanguage.SelectedIndex = 0;
        cmbLanguage.Name = "cmbLanguage";
        cmbLanguage.SelectedIndexChanged += (s, e) => ChangeLanguage();

        btnGenerate.Text = Core.LanguageManager.GetString("GenerateBtn");
        btnGenerate.Location = new Point(15, 85);
        btnGenerate.Size = new Size(180, 45);
        btnGenerate.BackColor = Color.FromArgb(0, 120, 215);
        btnGenerate.ForeColor = Color.White;
        btnGenerate.FlatStyle = FlatStyle.Flat;
        btnGenerate.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        btnGenerate.Click += BtnGenerate_Click;

        btnSave.Text = Core.LanguageManager.GetString("SaveBtn");
        btnSave.Location = new Point(205, 85);
        btnSave.Size = new Size(180, 45);
        btnSave.BackColor = Color.FromArgb(40, 167, 69);
        btnSave.ForeColor = Color.White;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        btnSave.Click += BtnSave_Click;
        btnSave.Enabled = false;

        Button btnBatch = new();
        btnBatch.Text = Core.LanguageManager.GetString("BatchProcessBtn");
        btnBatch.Location = new Point(15, 140);
        btnBatch.Size = new Size(180, 40);
        btnBatch.BackColor = Color.FromArgb(255, 150, 0);
        btnBatch.ForeColor = Color.White;
        btnBatch.FlatStyle = FlatStyle.Flat;
        btnBatch.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        btnBatch.Click += (s, e) => { var form = new BatchForm(); form.ShowDialog(); };

        Button btnApi = new();
        btnApi.Text = Core.LanguageManager.GetString("StartApiBtn");
        btnApi.Location = new Point(205, 140);
        btnApi.Size = new Size(180, 40);
        btnApi.BackColor = Color.FromArgb(128, 0, 128);
        btnApi.ForeColor = Color.White;
        btnApi.FlatStyle = FlatStyle.Flat;
        btnApi.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        btnApi.Name = "btnApi";
        btnApi.Click += BtnApi_Click;

        grpExport.Controls.Add(lblExport);
        grpExport.Controls.Add(cmbExportFormat);
        grpExport.Controls.Add(lblLang);
        grpExport.Controls.Add(cmbLanguage);
        grpExport.Controls.Add(btnGenerate);
        grpExport.Controls.Add(btnSave);
        grpExport.Controls.Add(btnBatch);
        grpExport.Controls.Add(btnApi);

        Label lblApiStatus = new() { Text = Core.LanguageManager.GetString("ApiOff"), Location = new Point(20, 665), AutoSize = true, ForeColor = Color.Red, Name = "lblApiStatus", Font = new Font("Segoe UI", 9) };

        // Sağ Kolon: QR Önizleme
        Panel pnlQR = new();
        pnlQR.Location = new Point(440, 60);
        pnlQR.Size = new Size(440, 600);
        pnlQR.BackColor = Color.White;
        pnlQR.BorderStyle = BorderStyle.FixedSingle;

        picQR.Location = new Point(20, 20);
        picQR.Size = new Size(400, 560);
        picQR.SizeMode = PictureBoxSizeMode.Zoom;
        picQR.BackColor = Color.White;

        pnlQR.Controls.Add(picQR);

        Controls.Add(lblTitle);
        Controls.Add(grpContent);
        Controls.Add(grpBarcode);
        Controls.Add(grpAppearance);
        Controls.Add(grpExport);
        Controls.Add(lblApiStatus);
        
        Label lblAbout = new() { Text = "v1.0.0 | Yapımcısı: aoaydin", Location = new Point(20, 685), AutoSize = true, ForeColor = Color.Gray, Font = new Font("Segoe UI", 8) };
        Controls.Add(lblAbout);

        Controls.Add(pnlQR);
        FormBorderStyle = FormBorderStyle.Sizable;
        MaximizeBox = true;
        MinimizeBox = true;
        components = new System.ComponentModel.Container();
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(900, 720);
        Text = "Form1";
    }

    #endregion

    // UI Controls
    private TextBox txtInput = new();
    private TextBox txtPassword = new();
    private Button btnGenerate = new();
    private Button btnSave = new();
    private PictureBox picQR = new();
    private Label lblTitle = new();
    private ComboBox cmbBarcodeType = new();
    private ComboBox cmbECC = new();
    private ComboBox cmbModuleShape = new();
    private ComboBox cmbEyeShape = new();
    private ComboBox cmbFrame = new();
    private Button btnFgColor = new();
    private Panel pnlFgColor = new();
    private Button btnBgColor = new();
    private Panel pnlBgColor = new();
    private Button btnGradientEndColor = new();
    private Panel pnlGradientEnd = new();
    private CheckBox chkGradient = new();
    private Button btnLogo = new();
    private Label lblLogo = new();
    private ComboBox cmbExportFormat = new();
    private CheckBox chkEncrypt = new();
    private ComboBox cmbLanguage = new();
    private ToolTip toolTip = new();
    private GroupBox grpContent = new();
    private GroupBox grpBarcode = new();
    private GroupBox grpAppearance = new();
    private GroupBox grpExport = new();
}
