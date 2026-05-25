using Hardware.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using QRCoder;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace Hardware
{
    public static partial class ExportManager
    {
        public static async Task DownloadFullTable(IProgress<(int percent, string message)> progress, string path, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, $"Вся техника {DateTime.Now.ToShortDateString().Replace('.', '-')}.xlsx");
            if (File.Exists(path))
                File.Delete(path);

            using ApplicationContext context = new ApplicationContextFactory(new ConfigManager()).CreateDbContext();

            List<Device> devices = await context.Devices.Include(d => d.DeviceProvider)
                                                        .Include(d => d.DeviceName)
                                                        .ThenInclude(dn => dn.DeviceType)
                                                        .Include(d => d.Complect)
                                                        .ThenInclude(c => c.Cabinet)
                                                        .ThenInclude(c => c.Building)
                                                        .OrderBy(d => d.Complect.Cabinet.Building.Name)
                                                        .ThenBy(d => d.Complect.Cabinet.Name)
                                                        .ThenBy(d => d.Complect.Name)
                                                        .ThenBy(d => d.Inventory)
                                                        .ThenBy(d => d.Serial)
                                                        .AsNoTracking()
                                                        .AsSplitQuery()
                                                        .ToListAsync(token);

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
            int devicesCount = await context.Devices.CountAsync(token);

            foreach (Device device in devices)
            {
                token.ThrowIfCancellationRequested();

                worksheet.Cells[row, 1].Value = device.Complect.Cabinet.Building.Name;
                worksheet.Cells[row, 2].Value = device.Complect.Cabinet.Name;
                worksheet.Cells[row, 3].Value = device.Complect.Name;
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

            using ExcelRange range = worksheet.Cells[1, 1, row - 1, 9];
            ExcelTable table = worksheet.Tables.Add(range, "Техника");
            table.TableStyle = TableStyles.Light16;
            range.AutoFitColumns();
            await package.SaveAsync(token);
        }

        public static async Task DownloadQRs(IProgress<(int percent, string message)> progress, string path, CancellationToken token)
        {
            using ApplicationContext context = new ApplicationContextFactory(new ConfigManager()).CreateDbContext();

            path = Path.Combine(path, "QR");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            List<QrData> devices = await context.Devices.Select(d => new QrData
            {
                DeviceType = d.DeviceName.DeviceType.Name,
                DeviceName = d.DeviceName.Name,
                Serial = d.Serial,
                Inventory = d.Inventory
            }).ToListAsync(token);

            int iterator = 0;
            const int batchSize = 50;
            for (int i = 0; i < devices.Count; i += batchSize)
            {
                IEnumerable<QrData> batch = devices.Skip(i).Take(batchSize);
                foreach (QrData device in batch)
                {
                    await Task.Run(() => GenerateSingleQrImage(device, path), token);
                    double percent = ++iterator / (double)devices.Count * 100;
                    string message = $"Создание qr-кода {iterator} из {devices.Count}";
                    progress.Report(((int)percent, message));
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                await Task.Delay(100, token);
            }

            await MakeHtml(path, progress);
        }

        private static async Task GenerateSingleQrImage(QrData qrData, string path)
        {
            using QRCodeGenerator qrGenerator = new();
            using QRCodeData qrCodeData = qrGenerator.CreateQrCode($"Тип: {qrData.DeviceType}\nНазвание: {qrData.DeviceName}\nС/н: {qrData.Serial}\nИ/н: {qrData.Inventory}", QRCodeGenerator.ECCLevel.H);
            using QRCode qrCode = new(qrCodeData);

            Bitmap originalQrCode = qrCode.GetGraphic(25, Color.Black, Color.White, true);

            Bitmap resizedBitmap = new(300, 300);
            using (Graphics graphics = Graphics.FromImage(resizedBitmap))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(originalQrCode, 0, 0, 300, 300);
            }

            string fileName = Path.Combine(path, $"{qrData.Serial}.jpg");
            if (File.Exists(fileName))
                File.Delete(fileName);
            try
            {
                resizedBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch { }
        }

        private static async Task<bool> MakeHtml(string path, IProgress<(int percent, string message)> progress)
        {
            using ApplicationContext context = new ApplicationContextFactory(new ConfigManager()).CreateDbContext();

            string filename = Path.Combine(path, "qr.html");
            if (File.Exists(filename))
                File.Delete(filename);

            using StreamWriter writer = new(filename, false, System.Text.Encoding.UTF8);
            await writer.WriteLineAsync("<html>");
            await writer.WriteLineAsync("<head>");
            await writer.WriteLineAsync("<style>");
            await writer.WriteLineAsync("@media print {" +
                "tr {" +
                "page-break-inside: avoid;" +
                "break-inside: avoid;" +
                "page-break-after: auto;" +
                "}\n" +
                "td {" +
                "page-break-inside: avoid;" +
                "break-inside: avoid" +
                "}\n" +
                "}");
            await writer.WriteLineAsync("</style>");
            await writer.WriteLineAsync("</head>");
            await writer.WriteLineAsync("<body style=\"font-family:'Courier New';font-size:8pt\">");
            await writer.WriteLineAsync("<table style=\"border:1px solid black; border-collapse:collapse; margin: 0 auto;\">");
            await writer.WriteLineAsync("<tbody>");

            int col = 0;
            int row = 0;
            bool isRowOpen = false;
            var devices = await context.Devices.Select(d => new
            {
                BuildingName = d.Complect.Cabinet.Building.Name,
                CabinetName = d.Complect.Cabinet.Name,
                ComplectName = d.Complect.Name,
                d.Serial
            }).OrderBy(d => d.BuildingName).ThenBy(d => d.CabinetName).ThenBy(d => d.ComplectName).ToListAsync();

            for (int i = 0; i < devices.Count; i++)
            {
                if (col == 0)
                {
                    await writer.WriteLineAsync("<tr>");
                    await writer.WriteLineAsync($"<td style=\"border:1px solid black; padding:3px\">{++row}</td>");
                    isRowOpen = true;
                }

                await writer.WriteLineAsync("<td style=\"border:1px solid black; padding:3px\">");
                await writer.WriteLineAsync($"<img src=\"{Path.Combine(path, $"{devices[i].Serial}.jpg")}\" width=150px height=150px>");
                await writer.WriteLineAsync($"<p>{devices[i].BuildingName}<br/>{devices[i].CabinetName}<br/>{devices[i].ComplectName}<br/>{devices[i].Serial}</p>");
                await writer.WriteLineAsync("</td>");

                if (col == 4)
                {
                    await writer.WriteLineAsync("</tr>");
                    isRowOpen = false;
                }

                col++;
                if (col > 4)
                    col = 0;

                double percent = (i + 1) / (double)devices.Count * 100;
                string message = $"Формирование таблицы {i + 1} из {devices.Count}";
                progress.Report(((int)percent, message));
            }

            if (isRowOpen)
                await writer.WriteLineAsync("</tr>");

            await writer.WriteLineAsync("</tbody>");
            await writer.WriteLineAsync("</table>");
            await writer.WriteLineAsync("</body>");
            await writer.WriteLineAsync("</html>");

            return true;
        }

        struct QrData
        {
            public string DeviceType;
            public string DeviceName;
            public string Serial;
            public string? Inventory;
        }

        public static async Task DownloadInventoryCards(IProgress<(int percent, string message)> progress, string path, CancellationToken token)
        {
            using ApplicationContext context = new ApplicationContextFactory(new ConfigManager()).CreateDbContext();

            path = Path.Combine(path, "Инвентарные карточки");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            int files = 1;
            string fileName = Path.Combine(path, $"Инвентарные карточки {DateTime.Now.ToShortDateString().Replace('.', '-')} ({files++}).xlsx");
            if (File.Exists(fileName))
                File.Delete(fileName);

            List<Building> buildings = await context.Buildings.Include(b => b.Cabinets)
                                                              .ThenInclude(c => c.Complects)
                                                              .ThenInclude(c => c.Devices)
                                                              .ThenInclude(d => d.DeviceName)
                                                              .ThenInclude(dn => dn.DeviceType)
                                                              .OrderBy(b => b.Name)
                                                              .AsSplitQuery()
                                                              .ToListAsync(token);
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
            int devicesCount = await context.Devices.CountAsync(token);

            foreach (Building building in buildings)
                foreach (Cabinet cabinet in building.Cabinets)
                {
                    token.ThrowIfCancellationRequested();

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
                        package.Save();

                        workbook.Dispose();
                        package.Dispose();

                        fileName = Path.Combine(path, $"Инвентарные карточки {DateTime.Now.ToShortDateString().Replace('.', '-')} ({files++}).xlsx");

                        if (File.Exists(fileName))
                            File.Delete(fileName);

                        package = new(fileName);
                        workbook = package.Workbook;
                    }
                }

            package.Save();
        }

        [GeneratedRegex(@"[^\p{L}\p{N}\s]")]
        private static partial Regex NonAlphabetNoNumberNoSpace();
    }
}
