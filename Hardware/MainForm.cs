using Hardware.Forms;
using Hardware.Models;
using Hardware.UserControls;

namespace Hardware
{
    public partial class MainForm : Form
    {
        private Repairman? repairman;

        public MainForm()
        {
            InitializeComponent();
            Load += (sender, e) => OnLoad();
        }

        private void OnLoad()
        {
            EnterPasswordForm form = new();
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                repairman = form.Repairman;
                if (repairman != null)
                    Text = $"Инвентаризация - {repairman.Name}";
                Init();
            }
            else
                Close();
        }

        private void Init()
        {
            tabPage1.Controls.Add(new DevicesTabPage(repairman) { Dock = DockStyle.Fill });
            tabPage2.Controls.Add(new DeviceTypesTabPage() { Dock = DockStyle.Fill });
            tabPage3.Controls.Add(new DeviceProvidersTabPage() { Dock = DockStyle.Fill });
            tabPage4.Controls.Add(new HistoryTabPage() { Dock = DockStyle.Fill });
            tabPage5.Controls.Add(new DevicesDataGridTabPage() { Dock = DockStyle.Fill });
            tabPage6.Controls.Add(new RepairmenAndOperationsTabPage() { Dock = DockStyle.Fill });
            tabPage7.Controls.Add(new RepairsTabPage() { Dock = DockStyle.Fill });

            Icon = Resources.inventarisation;
        }

        private async void DownloadToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog dialog = new();
            dialog.Description = "Выберите папку для сохранения";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.SelectedPath;
                DownloadForm form = new(path, DownloadForm.ExportType.ALL_DEVICES_TO_EXCEL);
                form.ShowDialog();
            }
        }

        private async void DownloadQRsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog dialog = new();
            dialog.Description = "Выберите папку для сохранения";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.SelectedPath;
                DownloadForm form = new(path, DownloadForm.ExportType.QRs);
                form.ShowDialog();
            }
        }

        private async void DownloadInventoryCardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog dialog = new();
            dialog.Description = "Выберите папку для сохранения";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.SelectedPath;
                DownloadForm form = new(path, DownloadForm.ExportType.INVENTORY_CARDS);
                form.ShowDialog();
            }
        }
    }
}
