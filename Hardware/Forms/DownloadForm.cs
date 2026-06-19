namespace Hardware.Forms
{
    public partial class DownloadForm : Form
    {
        public enum ExportType { ALL_DEVICES, QRs, INVENTORY_CARDS, REPAIRS }

        private readonly ProgressBar progressBar;
        private readonly Label progressLabel;
        private readonly Button cancelButton;

        CancellationTokenSource cancellationTokenSource;

        public DownloadForm(string path, ExportType type)
        {
            InitializeComponent();
            Icon = Resources.savefile;
            Text = "Экспорт данных";

            TableLayoutPanel panel = new()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                ColumnCount = 1,
                ColumnStyles =
                {
                    new(SizeType.AutoSize)
                },
                RowCount = 3,
                RowStyles =
                {
                    new (SizeType.AutoSize),
                    new (SizeType.AutoSize),
                    new (SizeType.AutoSize)
                },
                Padding = new Padding(5)
            };
            Controls.Add(panel);
            progressBar = new() { Style = ProgressBarStyle.Blocks, Anchor = AnchorStyles.None, Width = 400, Margin = new(3) };
            panel.Controls.Add(progressBar, 0, 0);
            progressLabel = new() { Text = string.Empty, Anchor = AnchorStyles.None, AutoSize = true, Margin = new(3) };
            panel.Controls.Add(progressLabel, 0, 1);
            cancelButton = new() { Text = "Отмена", Anchor = AnchorStyles.None, AutoSize = true, Margin = new(3) };
            cancelButton.Click += (sender, e) => Cancel();
            panel.Controls.Add(cancelButton, 0, 2);
            Load += async (sender, e) => { await StartExport(path, type); };
        }

        private void Cancel()
        {
            if (cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested)
            {
                cancellationTokenSource.Cancel();
                cancelButton.Enabled = false;
            }
        }

        public async Task StartExport(string path, ExportType type)
        {
            Progress<(int percent, string message)> progress = new(report =>
                {
                    progressBar.Value = report.percent;
                    progressLabel.Text = report.message;
                });
            cancellationTokenSource = new();
            try
            {
                switch (type)
                {
                    case ExportType.ALL_DEVICES:
                        await ExportManager.DownloadFullTable(progress, path, cancellationTokenSource.Token);
                        break;
                    case ExportType.QRs:
                        await ExportManager.DownloadQRs(progress, path, cancellationTokenSource.Token);
                        break;
                    case ExportType.INVENTORY_CARDS:
                        await ExportManager.DownloadInventoryCards(progress, path, cancellationTokenSource.Token);
                        break;
                    case ExportType.REPAIRS:
                        await ExportManager.DownloadRepairs(progress, path, cancellationTokenSource.Token);
                        break;
                }
                ((IProgress<(int percent, string message)>)progress).Report((100, "Завершено"));
            }
            catch (OperationCanceledException)
            {
                ((IProgress<(int percent, string message)>)progress).Report((progressBar.Value, "Отменено пользователем"));
            }
            catch (Exception ex)
            {
                ((IProgress<(int percent, string message)>)progress).Report((progressBar.Value, $"{ex.Message}"));
            }
            finally
            {
                cancelButton.DialogResult = DialogResult.OK;
                cancelButton.Text = "ОК";
                cancelButton.Enabled = true;
                cancelButton.Click -= (sender, e) => Cancel();
                cancelButton.Click += (sender, e) => Close();
            }
        }
    }
}
