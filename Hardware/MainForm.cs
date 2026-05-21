using Hardware.Forms;
using Hardware.Models;
using Hardware.UserControls;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using Timer = System.Windows.Forms.Timer;

namespace Hardware
{
    public partial class MainForm : Form
    {
        private Timer searchTimerHistory = new();
        private Timer searchTimerFullTable = new();
        private const int searchTimerDelayMS = 500;
        private readonly ConfigManager configManager = new();
        private List<ToolStripMenuItem> downloadButtons = [];

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
            InitializeHistoryDGW();
            InitializeFullListDGW();

            searchTimerHistory = new()
            {
                Interval = searchTimerDelayMS
            };
            searchTimerHistory.Tick += (sender, e) => { SearchTimerHistoryTick(searchTimerHistory); };

            searchTimerFullTable = new()
            {
                Interval = searchTimerDelayMS
            };
            searchTimerFullTable.Tick += (sender, e) => { SearchTimerFullTableTick(searchTimerFullTable); };

            ProgressOff();
            downloadButtons =
            [
                downloadToExcelToolStripMenuItem,
                downloadQRsToolStripMenuItem,
                downloadInventoryCardsToolStripMenuItem
            ];

            tabPage1.Controls.Add(new DevicesTabPage(repairman) { Dock = DockStyle.Fill });
            tabPage2.Controls.Add(new DeviceTypesTabPage() { Dock = DockStyle.Fill });
            tabPage3.Controls.Add(new DeviceProvidersTabPage() { Dock = DockStyle.Fill });
            tabPage6.Controls.Add(new RepairmenAndOperationsTabPage() { Dock = DockStyle.Fill });
            tabPage7.Controls.Add(new RepairsTabPage() { Dock = DockStyle.Fill });

            Icon = Resources.inventarisation;
        }
        
        private void SearchTimerHistoryTick(object sender)
        {
            ((Timer)sender).Stop();
            RefreshHistoryDGW();
        }

        private void SearchTimerFullTableTick(object sender)
        {
            ((Timer)sender).Stop();
            RefreshFullListDGW();
        }        

        private void SwitchDownloadButtons()
        {
            foreach (ToolStripMenuItem button in downloadButtons)
                button.Enabled = !button.Enabled;
        }

        private void ProgressOff()
        {
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            progressLabel.Text = string.Empty;
            progressLabel.Visible = false;
        }

        private void ProgressOn()
        {
            progressBar1.Visible = true;
            progressLabel.Visible = true;
        }

        #region Блок работы с историей перемещений
        private async void InitializeHistoryDGW()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            historyDGW.DataSource = await context.History.OrderByDescending(h => h.ChangedAt).ToListAsync();
            historyDGW.Columns[0].Visible = false;
            historyDGW.Columns[1].HeaderText = "Было";
            historyDGW.Columns[2].HeaderText = "Стало";
            historyDGW.Columns[3].HeaderText = "Дата и время изменения";
        }

        private async void RefreshHistoryDGW()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            if (historySearchTBox.Text.Length == 0)
                historyDGW.DataSource = await context.History.OrderByDescending(h => h.ChangedAt).ToListAsync();
            else
            {
                string text = historySearchTBox.Text.ToLower();
                historyDGW.DataSource = await context.History.Where(h => h.Before.ToLower().Contains(text) || h.After.ToLower().Contains(text))
                    .OrderByDescending(h => h.ChangedAt)
                    .ToListAsync();
            }
        }

        private void historySearchTBox_TextChanged(object sender, EventArgs e)
        {
            searchTimerHistory.Stop();
            searchTimerHistory.Start();
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            RefreshHistoryDGW();
        }
        #endregion

        #region Блок работы с полным списком техники
        private async void InitializeFullListDGW()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            fullListDGW.DataSource = await context.Devices.Select(d => new
            {
                BuildingName = d.Complect.Cabinet.Building.Name,
                CabinetName = d.Complect.Cabinet.Name,
                ComplectName = d.Complect.Name,
                DeviceTypeName = d.DeviceName.DeviceType.Name,
                DeviceName = d.DeviceName.Name,
                DeviceProviderName = d.DeviceProvider.Name,
                d.Serial,
                d.Inventory,
                d.Notes
            }).OrderBy(d => d.BuildingName)
            .ThenBy(d => d.CabinetName)
            .ThenBy(d => d.ComplectName)
            .ThenBy(d => d.Inventory)
            .ToListAsync();

            fullListDGW.Columns[0].HeaderText = "Здание";
            fullListDGW.Columns[1].HeaderText = "Кабинет";
            fullListDGW.Columns[2].HeaderText = "Комплект";
            fullListDGW.Columns[3].HeaderText = "Тип";
            fullListDGW.Columns[4].HeaderText = "Название";
            fullListDGW.Columns[5].HeaderText = "Получено от";
            fullListDGW.Columns[6].HeaderText = "Серийный номер";
            fullListDGW.Columns[7].HeaderText = "Инвентарный номер";
            fullListDGW.Columns[8].HeaderText = "Примечание";

            fullListNumberOfDevicesLabel.Text = $"Всего единиц техники: {fullListDGW.RowCount}";
        }

        private async void RefreshFullListDGW()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            var list = await context.Devices.Select(d => new
            {
                BuildingName = d.Complect.Cabinet.Building.Name,
                CabinetName = d.Complect.Cabinet.Name,
                ComplectName = d.Complect.Name,
                DeviceTypeName = d.DeviceName.DeviceType.Name,
                DeviceName = d.DeviceName.Name,
                DeviceProviderName = d.DeviceProvider.Name,
                d.Serial,
                d.Inventory,
                d.Notes
            }).OrderBy(d => d.BuildingName)
            .ThenBy(d => d.CabinetName)
            .ThenBy(d => d.ComplectName)
            .ThenBy(d => d.Inventory)
            .ToListAsync();

            if (fullListBuildingTBox.Text.Length != 0)
                list = [.. list.Where(d => d.BuildingName.ToLower().Contains(fullListBuildingTBox.Text.ToLower()))];
            if (fullListCabinetTBox.Text.Length != 0)
                list = [.. list.Where(d => d.CabinetName.ToLower().Contains(fullListCabinetTBox.Text.ToLower()))];
            if (fullListComplectTBox.Text.Length != 0)
                list = [.. list.Where(d => d.ComplectName.ToLower().Contains(fullListComplectTBox.Text.ToLower()))];
            if (fullListDeviceTypeTBox.Text.Length != 0)
                list = [.. list.Where(d => d.DeviceTypeName.ToLower().Contains(fullListDeviceTypeTBox.Text.ToLower()))];
            if (fullListDeviceNameTBox.Text.Length != 0)
                list = [.. list.Where(d => d.DeviceName.ToLower().Contains(fullListDeviceNameTBox.Text.ToLower()))];
            if (fullListDeviceProviderTBox.Text.Length != 0)
                list = [.. list.Where(d => d.DeviceProviderName.ToLower().Contains(fullListDeviceProviderTBox.Text.ToLower()))];
            if (fullListSerialTBox.Text.Length != 0)
                list = [.. list.Where(d => d.Serial.ToLower().Contains(fullListSerialTBox.Text.ToLower()))];
            if (fullListInventoryTBox.Text.Length != 0)
                list = [.. list.Where(d => d.Inventory.ToLower().Contains(fullListInventoryTBox.Text.ToLower()))];
            if (fullListNotesTBox.Text.Length != 0)
                list = [.. list.Where(d => d.Notes.ToLower().Contains(fullListNotesTBox.Text.ToLower()))];

            fullListDGW.DataSource = list;
            fullListNumberOfDevicesLabel.Text = $"Всего единиц техники: {fullListDGW.RowCount}";
        }

        private void fullListSearchTBox_TextChanged(object sender, EventArgs e)
        {
            searchTimerFullTable.Stop();
            searchTimerFullTable.Start();
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            RefreshFullListDGW();
        }
        #endregion

        private async void DownloadToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchDownloadButtons();
            ProgressOn();

            try
            {
                Progress<(int percent, string message)> progress = new(report =>
                {
                    progressBar1.Value = report.percent;
                    progressLabel.Text = report.message;
                });
                await Task.Run(() => DownloadFullTable(progress));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            finally
            {
                SwitchDownloadButtons();
                ProgressOff();
            }
        }

        private async Task<bool> DownloadFullTable(IProgress<(int percent, string message)> progress)
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            path = Path.Combine(path, "Hardware");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, $"{DateTime.Now.ToShortDateString().Replace('.', '-')}.xlsx");
            try
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
            catch
            {
                MessageBox.Show($"Файл {path} занят");
                return false;
            }

            List<Building> buildings = await context.Buildings.Include(b => b.Cabinets)
                                                              .ThenInclude(c => c.Complects)
                                                              .ThenInclude(c => c.Devices)
                                                              .ThenInclude(d => d.DeviceName)
                                                              .ThenInclude(dn => dn.DeviceType)
                                                              .Include(b => b.Cabinets)
                                                              .ThenInclude(c => c.Complects)
                                                              .ThenInclude(c => c.Devices)
                                                              .ThenInclude(d => d.DeviceProvider)
                                                              .OrderBy(b => b.Name)
                                                              .AsSplitQuery()
                                                              .ToListAsync();
            foreach (Building building in buildings)
            {
                building.Cabinets = [.. building.Cabinets.OrderBy(c => c.Name)];
                foreach (Cabinet cabinet in building.Cabinets)
                {
                    cabinet.Complects = [.. cabinet.Complects.OrderBy(c => c.Name)];
                    foreach (Complect complect in cabinet.Complects)
                        complect.Devices = [.. complect.Devices.OrderBy(d => d.Inventory).ThenBy(d => d.Serial)];
                }
            }

            using ExcelPackage package = new(path);
            using ExcelWorkbook workbook = package.Workbook;
            using ExcelWorksheet worksheet = workbook.Worksheets.Add("Техника");

            worksheet.Cells.Style.Font.Name = "Courier New";
            worksheet.Cells.Style.Font.Size = 12;
            worksheet.Cells.Style.WrapText = true;
            worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

            worksheet.Cells[1, 1].Value = "Здание";
            worksheet.Cells[1, 2].Value = "Кабинет";
            worksheet.Cells[1, 3].Value = "Комплект";
            worksheet.Cells[1, 4].Value = "Тип";
            worksheet.Cells[1, 5].Value = "Название";
            worksheet.Cells[1, 6].Value = "Получено от";
            worksheet.Cells[1, 7].Value = "С/н";
            worksheet.Cells[1, 8].Value = "И/н";
            worksheet.Cells[1, 9].Value = "Примечание";

            int row = 2;
            int iterator = 0;
            int devicesCount = await context.Devices.CountAsync();

            foreach (Building building in buildings)
                foreach (Cabinet cabinet in building.Cabinets)
                    foreach (Complect complect in cabinet.Complects)
                        foreach (Device device in complect.Devices)
                        {
                            worksheet.Cells[row, 1].Value = building.Name;
                            worksheet.Cells[row, 2].Value = cabinet.Name;
                            worksheet.Cells[row, 3].Value = complect.Name;
                            worksheet.Cells[row, 4].Value = device.DeviceName.DeviceType.Name;
                            worksheet.Cells[row, 5].Value = device.DeviceName.Name;
                            worksheet.Cells[row, 6].Value = device.DeviceProvider.Name;
                            worksheet.Cells[row, 7].Value = device.Serial;
                            worksheet.Cells[row, 8].Value = device.Inventory;
                            worksheet.Cells[row, 9].Value = device.Notes;

                            double percent = ++iterator / (double)devicesCount * 100;
                            string message = $"Формирование таблицы {iterator} из {devicesCount}";
                            progress.Report(((int)percent, message));

                            row++;
                        }

            ExcelRange range = worksheet.Cells[1, 1, row - 1, 9];
            OfficeOpenXml.Table.ExcelTable table = worksheet.Tables.Add(range, "Техника");
            table.TableStyle = OfficeOpenXml.Table.TableStyles.Light16;
            range.AutoFitColumns();

            try
            {
                package.Save();
                MessageBox.Show($"Файл сохранён по пути {path}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            return true;
        }

        private async void DownloadQRsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchDownloadButtons();
            ProgressOn();

            try
            {
                QrManager qrManager = new();
                Progress<(int percent, string message)> progress = new(report =>
                {
                    progressBar1.Value = report.percent;
                    progressLabel.Text = report.message;
                });
                await Task.Run(() => qrManager.CreateQrImages(progress));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            finally
            {
                SwitchDownloadButtons();
                ProgressOff();
            }
        }

        private async void DownloadInventoryCardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchDownloadButtons();
            ProgressOn();

            try
            {
                Progress<(int percent, string message)> progress = new(report =>
                {
                    progressBar1.Value = report.percent;
                    progressLabel.Text = report.message;
                });
                await Task.Run(() => DownloadInventoryCards(progress));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            finally
            {
                SwitchDownloadButtons();
                ProgressOff();
            }
        }

        private async Task<bool> DownloadInventoryCards(IProgress<(int percent, string message)> progress)
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            path = Path.Combine(path, "Hardware");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            int files = 1;
            string fileName = Path.Combine(path, $"Инвентарные карточки {DateTime.Now.ToShortDateString().Replace('.', '-')} ({files++}).xlsx");
            try
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
            }
            catch
            {
                MessageBox.Show($"Файл {fileName} занят");
                return false;
            }

            List<Building> buildings = await context.Buildings.Include(b => b.Cabinets)
                                                              .ThenInclude(c => c.Complects)
                                                              .ThenInclude(c => c.Devices)
                                                              .ThenInclude(d => d.DeviceName)
                                                              .ThenInclude(dn => dn.DeviceType)
                                                              .OrderBy(b => b.Name)
                                                              .AsSplitQuery()
                                                              .ToListAsync();
            foreach (Building building in buildings)
            {
                building.Cabinets = [.. building.Cabinets.OrderBy(c => c.Name)];
                foreach (Cabinet cabinet in building.Cabinets)
                {
                    cabinet.Complects = [.. cabinet.Complects.OrderBy(c => c.Name)];
                    foreach (Complect complect in cabinet.Complects)
                        complect.Devices = [.. complect.Devices.OrderBy(d => d.Inventory).ThenBy(d => d.Serial)];
                }
            }

            ExcelPackage package = new(fileName);
            ExcelWorkbook workbook = package.Workbook;
            int iterator = 0;
            int devicesCount = await context.Devices.CountAsync();

            foreach (Building building in buildings)
                foreach (Cabinet cabinet in building.Cabinets)
                {
                    string worksheetName = $"{building.Name} {cabinet.Name}";
                    worksheetName = NonAlphabetNoNumberNoSpace().Replace(worksheetName, "_");

                    ExcelWorksheet worksheet = workbook.Worksheets.Add(worksheetName);

                    worksheet.PrinterSettings.FitToPage = true;
                    worksheet.PrinterSettings.FitToWidth = 1;
                    worksheet.PrinterSettings.FitToHeight = 1;
                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
                    worksheet.PrinterSettings.PaperSize = ePaperSize.A5;
                    worksheet.PrinterSettings.HorizontalCentered = true;

                    worksheet.Cells.Style.Font.Name = "Courier New";
                    worksheet.Cells.Style.Font.Size = 8;
                    worksheet.Cells.Style.WrapText = true;
                    worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    worksheet.Columns[1].Width = 6.17;
                    worksheet.Columns[2].Width = 18.67;
                    worksheet.Columns[3].Width = 12.67;
                    worksheet.Columns[4].Width = 12.17;
                    worksheet.Columns[5].Width = 4.5;
                    worksheet.Columns[6].Width = 7.67;
                    worksheet.Columns[7].Width = 28.33;
                    worksheet.Columns[8].Width = 2.67;
                    worksheet.Columns[9].Width = 12.67;
                    worksheet.Columns[10].Width = 7.67;
                    worksheet.Columns[11].Width = 8.67;

                    #region Шапка
                    worksheet.Cells[1, 1, 1, 11].Merge = true;
                    worksheet.Cells[1, 1].Value = "Инвентарный список нефинансовых активов";
                    worksheet.Cells[1, 1].Style.Font.Size = 10;
                    worksheet.Cells[1, 1].Style.Font.Bold = true;
                    worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    worksheet.Cells[2, 1].Value = $"{building.Name} {cabinet.Name}";

                    worksheet.Cells[4, 1].Value = "Учреждение";
                    worksheet.Cells[4, 3, 4, 8].Merge = true;
                    worksheet.Cells[4, 3].Value = "ЧЕТВЕРТЫЙ КАССАЦИОННЫЙ СУД ОБЩЕЙ ЮРИСДИКЦИИ";
                    worksheet.Cells[4, 3, 4, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    worksheet.Cells[5, 4, 5, 8].Merge = true;
                    worksheet.Cells[5, 1].Value = "Структурное подразделение";
                    worksheet.Cells[5, 4, 5, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    worksheet.Cells[6, 4, 6, 8].Merge = true;
                    worksheet.Cells[6, 1].Value = "Ответственное(-ые) лицо(-а)";
                    worksheet.Cells[6, 4, 6, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    worksheet.Cells[2, 10, 2, 11].Merge = true;
                    worksheet.Cells[2, 10].Value = "КОДЫ";
                    worksheet.Cells[2, 10, 2, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    worksheet.Cells[3, 10, 3, 11].Merge = true;
                    worksheet.Cells[3, 10].Value = "0504034";
                    worksheet.Cells[3, 10, 3, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    worksheet.Cells[4, 10, 4, 11].Merge = true;
                    worksheet.Cells[4, 10].Value = "32717350";
                    worksheet.Cells[4, 10, 4, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    worksheet.Cells[5, 10].Value = DateTime.Now.ToShortDateString();
                    worksheet.Cells[5, 10, 5, 11].Merge = true;
                    worksheet.Cells[5, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    worksheet.Cells[6, 10, 6, 11].Merge = true;

                    worksheet.Cells[3, 9].Value = "Форма по ОКУД";
                    worksheet.Cells[3, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                    worksheet.Cells[4, 9].Value = "по ОКПО";
                    worksheet.Cells[4, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                    worksheet.Cells[5, 9].Value = "Дата";
                    worksheet.Cells[5, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                    for (int i = 2; i <= 6; i++)
                        for (int j = 10; j <= 11; j++)
                        {
                            ExcelRange cell = worksheet.Cells[i, j];
                            cell.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            cell.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            cell.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            cell.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        }
                    worksheet.Cells[3, 10, 6, 11].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);

                    worksheet.Rows[1, 6].Style.WrapText = false;
                    #endregion

                    #region Шапка таблицы
                    worksheet.Rows[8, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    worksheet.Cells[8, 1].Value = "Номер\nп/п";
                    worksheet.Cells[8, 1, 10, 1].Merge = true;

                    worksheet.Cells[8, 2].Value = "Инвентарная\nкарточка";
                    worksheet.Cells[8, 2, 9, 3].Merge = true;

                    worksheet.Cells[10, 2].Value = "номер";
                    worksheet.Cells[10, 3].Value = "дата";

                    worksheet.Cells[8, 4].Value = "Заводской\nномер";
                    worksheet.Cells[8, 4, 10, 4].Merge = true;

                    worksheet.Cells[8, 5].Value = "Инвентарный\nномер";
                    worksheet.Cells[8, 5, 10, 6].Merge = true;

                    worksheet.Cells[8, 7].Value = "Полное наименование объекта";
                    worksheet.Cells[8, 7, 10, 8].Merge = true;

                    worksheet.Cells[8, 9].Value = "Выбытие (перемещение)";
                    worksheet.Cells[8, 9, 8, 11].Merge = true;

                    worksheet.Cells[9, 9].Value = "документ";
                    worksheet.Cells[9, 9, 9, 10].Merge = true;

                    worksheet.Cells[10, 9].Value = "дата";
                    worksheet.Cells[10, 10].Value = "номер";

                    worksheet.Cells[9, 11].Value = "причина\nвыбытия";
                    worksheet.Cells[9, 11, 10, 11].Merge = true;

                    worksheet.Cells[11, 1].Value = "1а";
                    worksheet.Cells[11, 2].Value = "1";
                    worksheet.Cells[11, 3].Value = "2";
                    worksheet.Cells[11, 4].Value = "3";

                    worksheet.Cells[11, 5, 11, 6].Merge = true;
                    worksheet.Cells[11, 5].Value = "4";

                    worksheet.Cells[11, 7, 11, 8].Merge = true;
                    worksheet.Cells[11, 7].Value = "5";

                    worksheet.Cells[11, 9].Value = "6";
                    worksheet.Cells[11, 10].Value = "7";
                    worksheet.Cells[11, 11].Value = "8";
                    #endregion

                    #region Тело таблицы
                    int row = 12;
                    int count = 1;
                    foreach (Complect complect in cabinet.Complects)
                        foreach (Device d in complect.Devices)
                        {
                            worksheet.Cells[row, 5, row, 6].Merge = true;
                            worksheet.Cells[row, 7, row, 8].Merge = true;

                            worksheet.Cells[row, 1].Value = count++;
                            worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            if (d.Inventory != null && d.Inventory.StartsWith("10134"))
                                worksheet.Cells[row, 2].Value = d.Inventory[^5..];

                            worksheet.Cells[row, 4].Value = d.Serial;
                            if (d.Inventory != null)
                                worksheet.Cells[row, 5].Value = d.Inventory;

                            worksheet.Cells[row, 7].Value = $"{d.DeviceName.DeviceType.Name} {d.DeviceName.Name}";

                            if (d.Inventory != null && d.Inventory != string.Empty)
                                worksheet.Row(row).Height = 22.5;

                            row++;

                            double percent = ++iterator / (double)devicesCount * 100;
                            string message = $"Заполнение инвентарных карточек {iterator} из {devicesCount}";
                            progress.Report(((int)percent, message));
                        }
                    row--;

                    for (int i = 8; i <= row; i++)
                    {
                        for (int j = 1; j <= 11; j++)
                        {
                            ExcelRange cell = worksheet.Cells[i, j];
                            cell.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            cell.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            cell.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            cell.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        }
                    }
                    #endregion

                    #region Днище таблицы
                    row += 2;

                    worksheet.Rows[row, row + 5].Style.WrapText = false;

                    worksheet.Cells[row, 1].Value = "Исполнитель";

                    worksheet.Cells[row, 3, row, 5].Merge = true;
                    worksheet.Cells[row, 3, row, 5].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    worksheet.Cells[row, 7].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    worksheet.Cells[row, 9, row, 11].Merge = true;
                    worksheet.Cells[row, 9, row, 11].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    row++;

                    worksheet.Cells[row, 3, row, 5].Merge = true;
                    worksheet.Cells[row, 3].Value = "(должность)";
                    worksheet.Cells[row, 3].Style.Font.Size = 7;
                    worksheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    worksheet.Cells[row, 7].Value = "(подпись)";
                    worksheet.Cells[row, 7].Style.Font.Size = 7;
                    worksheet.Cells[row, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    worksheet.Cells[row, 9, row, 11].Merge = true;
                    worksheet.Cells[row, 9].Value = "(расшифровка подписи)";
                    worksheet.Cells[row, 9].Style.Font.Size = 7;
                    worksheet.Cells[row, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    row++;

                    worksheet.Cells[row, 7].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    worksheet.Cells[row, 9, row, 11].Merge = true;
                    worksheet.Cells[row, 9, row, 11].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    row++;

                    worksheet.Cells[row, 7].Value = "(номер контактного телефона)";
                    worksheet.Cells[row, 7].Style.Font.Size = 7;
                    worksheet.Cells[row, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    worksheet.Cells[row, 9, row, 11].Merge = true;
                    worksheet.Cells[row, 9].Value = "(электронный адрес)";
                    worksheet.Cells[row, 9].Style.Font.Size = 7;
                    worksheet.Cells[row, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    row++;

                    worksheet.Cells[row, 1].Value = "\"_______\"____________________ 20___ г.";
                    #endregion

                    if (workbook.Worksheets.Count > 24)
                    {
                        try
                        {
                            package.Save();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка: {ex.Message}");
                        }

                        workbook.Dispose();
                        package.Dispose();

                        fileName = Path.Combine(path, $"Инвентарные карточки {DateTime.Now.ToShortDateString().Replace('.', '-')} ({files++}).xlsx");
                        try
                        {
                            if (File.Exists(fileName))
                                File.Delete(fileName);
                        }
                        catch
                        {
                            MessageBox.Show($"Файл {fileName} занят");
                            return false;
                        }

                        package = new(fileName);
                        workbook = package.Workbook;
                    }
                }

            try
            {
                package.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            finally
            {
                MessageBox.Show("Выгрузка инвентарных карточек завершена");
            }
            return true;
        }

        [GeneratedRegex(@"[^\p{L}\p{N}\s]")]
        private static partial Regex NonAlphabetNoNumberNoSpace();
    }
}
