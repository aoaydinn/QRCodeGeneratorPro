using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QrCodeApp;

public partial class BatchForm : Form
{
    public BatchForm()
    {
        InitializeComponent();
    }

    
    private void BtnLoadCSV_Click(object? sender, EventArgs e)
    {
        using OpenFileDialog dlg = new();
        dlg.Filter = "CSV Dosyası|*.csv|Tüm Dosyalar|*.*";
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            try
            {
                dgvData.Rows.Clear();
                var lines = File.ReadAllLines(dlg.FileName);
                int loaded = 0;
                foreach (var line in lines)
                {
                    var parts = ParseCsvLine(line);
                    if (parts.Length >= 2)
                    {
                        dgvData.Rows.Add(parts[0], parts[1]);
                        loaded++;
                    }
                    else if (parts.Length == 1 && !string.IsNullOrWhiteSpace(parts[0]))
                    {
                        dgvData.Rows.Add(parts[0], $"qr_{dgvData.Rows.Count + 1}");
                        loaded++;
                    }
                }
                lblStatus.Text = $"{loaded} kayıt yüklendi";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{Core.LanguageManager.GetString("ErrorTitle")}: {ex.Message}", Core.LanguageManager.GetString("ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private static string[] ParseCsvLine(string line)
    {
        // Simple CSV parser that handles quoted fields
        var result = new List<string>();
        bool inQuotes = false;
        var field = new StringBuilder();

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(field.ToString());
                field.Clear();
            }
            else
            {
                field.Append(c);
            }
        }
        result.Add(field.ToString());
        return result.ToArray();
    }

    private void BtnBrowseOutput_Click(object? sender, EventArgs e)
    {
        using FolderBrowserDialog dlg = new();
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            txtOutputDir.Text = dlg.SelectedPath;
        }
    }

    private void BtnStart_Click(object? sender, EventArgs e)
    {
        if (dgvData.Rows.Count == 0)
        {
            MessageBox.Show(Core.LanguageManager.GetString("CsvWarning"), Core.LanguageManager.GetString("WarningTitle"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrEmpty(txtOutputDir.Text) || !Directory.Exists(txtOutputDir.Text))
        {
            MessageBox.Show(Core.LanguageManager.GetString("OutputDirWarning"), Core.LanguageManager.GetString("WarningTitle"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var barcodeType = (Core.BarcodeType)cmbBarcodeType.SelectedIndex;
        string ecc = cmbECC.SelectedItem?.ToString() ?? "M";
        
        // Get customization parameters
        var cmbModule = Controls.OfType<ComboBox>().FirstOrDefault(c => c.Name == "cmbModuleShape");
        var cmbEye = Controls.OfType<ComboBox>().FirstOrDefault(c => c.Name == "cmbEyeShape");
        var cmbFrame = Controls.OfType<ComboBox>().FirstOrDefault(c => c.Name == "cmbFrame");
        var chkGrad = Controls.OfType<CheckBox>().FirstOrDefault(c => c.Name == "chkGradient");

        var moduleShape = cmbModule != null && cmbModule.SelectedIndex >= 0 ? (Core.ModuleShape)cmbModule.SelectedIndex : Core.ModuleShape.Square;
        var eyeShape = cmbEye != null && cmbEye.SelectedIndex >= 0 ? (Core.EyeShape)cmbEye.SelectedIndex : Core.EyeShape.Square;
        var frameStyle = cmbFrame != null && cmbFrame.SelectedIndex >= 0 ? (Core.FrameStyle)cmbFrame.SelectedIndex : Core.FrameStyle.None;
        bool useGradient = chkGrad?.Checked ?? false;

        int totalRows = dgvData.Rows.Cast<DataGridViewRow>().Count(r => !r.IsNewRow);
        int processed = 0;

        progressBar.Value = 0;
        lblStatus.Text = Core.LanguageManager.GetString("Processing");

        for (int i = 0; i < dgvData.Rows.Count; i++)
        {
            var row = dgvData.Rows[i];
            if (row.IsNewRow) continue;

            string? content = row.Cells[0].Value?.ToString();
            string? fileName = row.Cells[1].Value?.ToString();

            if (string.IsNullOrEmpty(content)) continue;

            try
            {
                Bitmap barcode = Core.BarcodeGenerator.GenerateBarcode(
                    content,
                    barcodeType,
                    ecc,
                    Color.Black,
                    Color.White,
                    400,
                    400,
                    moduleShape,
                    eyeShape,
                    frameStyle,
                    useGradient);

                string fullPath = Path.Combine(txtOutputDir.Text, $"{fileName ?? $"qr_{processed + 1}"}.png");
                barcode.Save(fullPath, ImageFormat.Png);
                barcode.Dispose();

                processed++;
                if (totalRows > 0)
                {
                    progressBar.Value = (int)((float)processed / totalRows * 100);
                }
                lblStatus.Text = $"{processed}/{totalRows} işlendi";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{Core.LanguageManager.GetString("ErrorTitle")} ({i + 1}): {ex.Message}", Core.LanguageManager.GetString("ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        lblStatus.Text = Core.LanguageManager.GetString("Completed");
        MessageBox.Show(string.Format(Core.LanguageManager.GetString("BatchSuccess"), processed), Core.LanguageManager.GetString("SuccessTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
