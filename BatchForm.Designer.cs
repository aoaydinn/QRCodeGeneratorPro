namespace QrCodeApp;

partial class BatchForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
        private void InitializeComponent()
    {
        Text = Core.LanguageManager.GetString("BatchFormTitle");
        Size = new Size(850, 750);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = ColorTranslator.FromHtml("#F5F7FA");

        // Veri Kaynağı Grubu
        grpSource.Text = "Veri Kaynağı";
        grpSource.Location = new Point(20, 20);
        grpSource.Size = new Size(790, 80);
        grpSource.Font = new Font("Segoe UI", 9, FontStyle.Bold);

        btnLoadCSV.Text = "CSV Yükle";
        btnLoadCSV.Location = new Point(15, 30);
        btnLoadCSV.Size = new Size(100, 30);
        btnLoadCSV.Font = new Font("Segoe UI", 9);
        btnLoadCSV.Click += BtnLoadCSV_Click;

        Label lblOutput = new() { Text = "Çıktı Klasörü:", Location = new Point(130, 35), AutoSize = true, Font = new Font("Segoe UI", 9) };
        
        txtOutputDir.Location = new Point(230, 32);
        txtOutputDir.Size = new Size(450, 25);
        txtOutputDir.Font = new Font("Segoe UI", 9);
        txtOutputDir.PlaceholderText = "Klasör seçin...";

        btnBrowseOutput.Text = "Gözat...";
        btnBrowseOutput.Location = new Point(690, 30);
        btnBrowseOutput.Size = new Size(80, 28);
        btnBrowseOutput.Font = new Font("Segoe UI", 9);
        btnBrowseOutput.Click += BtnBrowseOutput_Click;

        grpSource.Controls.Add(btnLoadCSV);
        grpSource.Controls.Add(lblOutput);
        grpSource.Controls.Add(txtOutputDir);
        grpSource.Controls.Add(btnBrowseOutput);

        // Barkod Ayarları Grubu
        grpSettings.Text = "Barkod Ayarları";
        grpSettings.Location = new Point(20, 110);
        grpSettings.Size = new Size(790, 140);
        grpSettings.Font = new Font("Segoe UI", 9, FontStyle.Bold);

        Label lblType = new() { Text = "Barkod Türü:", Location = new Point(15, 25), AutoSize = true, Font = new Font("Segoe UI", 9) };
        cmbBarcodeType.Location = new Point(15, 45);
        cmbBarcodeType.Size = new Size(180, 25);
        cmbBarcodeType.Font = new Font("Segoe UI", 9);
        cmbBarcodeType.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbBarcodeType.Items.AddRange(Core.BarcodeGenerator.GetBarcodeTypeNames());
        cmbBarcodeType.SelectedIndex = 0;

        Label lblECC = new() { Text = "Hata Düzeltme:", Location = new Point(215, 25), AutoSize = true, Font = new Font("Segoe UI", 9) };
        cmbECC.Location = new Point(215, 45);
        cmbECC.Size = new Size(180, 25);
        cmbECC.Font = new Font("Segoe UI", 9);
        cmbECC.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbECC.Items.AddRange(new[] { "L", "M", "Q", "H" });
        cmbECC.SelectedIndex = 1;

        Label lblModule = new() { Text = Core.LanguageManager.GetString("ModuleShape"), Location = new Point(415, 25), AutoSize = true, Font = new Font("Segoe UI", 9) };
        cmbModuleShape.Location = new Point(415, 45);
        cmbModuleShape.Size = new Size(180, 25);
        cmbModuleShape.Font = new Font("Segoe UI", 9);
        cmbModuleShape.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbModuleShape.Items.AddRange(Core.BarcodeGenerator.GetModuleShapeNames());
        cmbModuleShape.SelectedIndex = 0;
        cmbModuleShape.Name = "cmbModuleShape";

        Label lblEye = new() { Text = Core.LanguageManager.GetString("EyeShape"), Location = new Point(615, 25), AutoSize = true, Font = new Font("Segoe UI", 9) };
        cmbEyeShape.Location = new Point(615, 45);
        cmbEyeShape.Size = new Size(160, 25);
        cmbEyeShape.Font = new Font("Segoe UI", 9);
        cmbEyeShape.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbEyeShape.Items.AddRange(Core.BarcodeGenerator.GetEyeShapeNames());
        cmbEyeShape.SelectedIndex = 0;
        cmbEyeShape.Name = "cmbEyeShape";

        Label lblFrame = new() { Text = Core.LanguageManager.GetString("Frame"), Location = new Point(15, 80), AutoSize = true, Font = new Font("Segoe UI", 9) };
        cmbFrame.Location = new Point(15, 100);
        cmbFrame.Size = new Size(180, 25);
        cmbFrame.Font = new Font("Segoe UI", 9);
        cmbFrame.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbFrame.Items.AddRange(Core.BarcodeGenerator.GetFrameStyles());
        cmbFrame.SelectedIndex = 0;
        cmbFrame.Name = "cmbFrame";

        chkGradient.Text = Core.LanguageManager.GetString("GradientChk");
        chkGradient.Location = new Point(215, 100);
        chkGradient.AutoSize = true;
        chkGradient.Font = new Font("Segoe UI", 9);
        chkGradient.Name = "chkGradient";

        grpSettings.Controls.Add(lblType);
        grpSettings.Controls.Add(cmbBarcodeType);
        grpSettings.Controls.Add(lblECC);
        grpSettings.Controls.Add(cmbECC);
        grpSettings.Controls.Add(lblModule);
        grpSettings.Controls.Add(cmbModuleShape);
        grpSettings.Controls.Add(lblEye);
        grpSettings.Controls.Add(cmbEyeShape);
        grpSettings.Controls.Add(lblFrame);
        grpSettings.Controls.Add(cmbFrame);
        grpSettings.Controls.Add(chkGradient);

        // Önizleme / Liste Grubu
        grpPreview.Text = "Veri Listesi";
        grpPreview.Location = new Point(20, 260);
        grpPreview.Size = new Size(790, 310);
        grpPreview.Font = new Font("Segoe UI", 9, FontStyle.Bold);

        dgvData.Location = new Point(15, 30);
        dgvData.Size = new Size(760, 265);
        dgvData.Font = new Font("Segoe UI", 9);
        dgvData.AllowUserToAddRows = true;
        dgvData.AllowUserToDeleteRows = true;
        dgvData.BackgroundColor = Color.White;
        dgvData.Columns.Add("Content", "İçerik (Metin/URL)");
        dgvData.Columns.Add("FileName", "Dosya Adı");
        dgvData.Columns[0].Width = 430;
        dgvData.Columns[1].Width = 280;

        grpPreview.Controls.Add(dgvData);

        // İşlemler Grubu
        grpActions.Text = "İşlemler";
        grpActions.Location = new Point(20, 580);
        grpActions.Size = new Size(790, 90);
        grpActions.Font = new Font("Segoe UI", 9, FontStyle.Bold);

        btnStart.Text = Core.LanguageManager.GetString("BatchGenerateBtn");
        btnStart.Location = new Point(15, 30);
        btnStart.Size = new Size(150, 40);
        btnStart.BackColor = Color.FromArgb(0, 120, 215);
        btnStart.ForeColor = Color.White;
        btnStart.FlatStyle = FlatStyle.Flat;
        btnStart.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        btnStart.Click += BtnStart_Click;

        progressBar.Location = new Point(180, 38);
        progressBar.Size = new Size(400, 25);
        progressBar.Minimum = 0;
        progressBar.Maximum = 100;

        lblStatus.Location = new Point(600, 42);
        lblStatus.AutoSize = true;
        lblStatus.Text = "Hazır";
        lblStatus.Font = new Font("Segoe UI", 9);

        grpActions.Controls.Add(btnStart);
        grpActions.Controls.Add(progressBar);
        grpActions.Controls.Add(lblStatus);

        Controls.Add(grpSource);
        Controls.Add(grpSettings);
        Controls.Add(grpPreview);
        Controls.Add(grpActions);

        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
    }

    #endregion

        // UI Controls
    private DataGridView dgvData = new();
    private Button btnLoadCSV = new();
    private Button btnStart = new();
    private Button btnBrowseOutput = new();
    private TextBox txtOutputDir = new();
    private ProgressBar progressBar = new();
    private Label lblStatus = new();
    private ComboBox cmbBarcodeType = new();
    private ComboBox cmbECC = new();
    private ComboBox cmbModuleShape = new();
    private ComboBox cmbEyeShape = new();
    private ComboBox cmbFrame = new();
    private CheckBox chkGradient = new();
    private GroupBox grpSource = new();
    private GroupBox grpSettings = new();
    private GroupBox grpPreview = new();
    private GroupBox grpActions = new();
}