using QRCoder;
using System.Drawing.Drawing2D;
using Path = System.IO.Path;

namespace Hardware
{
    internal class QrManager
    {
        private readonly ConfigManager configManager = new();
        public async void CreateQrImages(IProgress<(int percent, string message)> progress)
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            path = Path.Combine(path, "Hardware", "QR");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var devices = context.Devices.Select(d => new
            {
                d.DeviceName.DeviceType,
                d.DeviceName,
                d.Serial,
                d.Inventory
            }).ToList();

            using QRCodeGenerator qrGenerator = new();
            int i = 0;
            foreach (var device in devices)
            {
                using QRCodeData qrCodeData = qrGenerator.CreateQrCode($"Тип: {device.DeviceType.Name}\nНазвание: {device.DeviceName.Name}\nС/н: {device.Serial}\nИ/н: {device.Inventory}", QRCodeGenerator.ECCLevel.H);
                using QRCode qrCode = new(qrCodeData);

                Bitmap originalQrCode = qrCode.GetGraphic(25, Color.Black, Color.White, true);

                // Изменяем размер под нужный
                Bitmap resizedBitmap = new(300, 300);
                using (Graphics graphics = Graphics.FromImage(resizedBitmap))
                {
                    // Настройки для качественного масштабирования
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.DrawImage(originalQrCode, 0, 0, 300, 300);
                }

                // Сохраняем в JPG
                string fileName = Path.Combine(path, $"{device.Serial}.jpg");
                if (File.Exists(fileName))
                    File.Delete(fileName);
                try
                {
                    resizedBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch { }
                double percent = ++i / (double)devices.Count * 100;
                string message = $"Создание qr-кода {i} из {devices.Count}";
                progress.Report(((int)percent, message));
            }

            MakeHtml(path, progress);

            MessageBox.Show($"Выгрузка завершена по пути {path}");
        }

        private async void MakeHtml(string path, IProgress<(int percent, string message)> progress)
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

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
            var devices = context.Devices.Select(d => new
            {
                d.Complect.Cabinet.Building,
                d.Complect.Cabinet,
                d.Complect,
                d.Serial
            }).OrderBy(d => d.Building.Name).ThenBy(d => d.Cabinet.Name).ThenBy(d => d.Complect.Name).ToList();

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
                await writer.WriteLineAsync($"<p>{devices[i].Building.Name}<br/>{devices[i].Cabinet.Name}<br/>{devices[i].Complect.Name}<br/>{devices[i].Serial}</p>");
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
        }
    }
}
