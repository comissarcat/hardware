using QRCoder;
using System.Drawing.Drawing2D;
using Path = System.IO.Path;

namespace Hardware
{
    internal class QrManager
    {
        public async void CreateQrImages(IProgress<int> progress)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            path = Path.Combine(path, "Hardware", "QR");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var devices = ApplicationContext.Instance().Devices.Select(d => new
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
                double percent = (double)++i / (double)devices.Count * 100;
                progress.Report((int)percent);
            }

            //MakePdf(path);
            MakeHtml(path);

            MessageBox.Show($"Выгрузка завершена по пути {path}");
        }

        private async void MakeHtml(string path)
        {
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
            var devices = ApplicationContext.Instance().Devices.Select(d => new
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
            }

            if (isRowOpen)
                await writer.WriteLineAsync("</tr>");

            await writer.WriteLineAsync("</tbody>");
            await writer.WriteLineAsync("</table>");
            await writer.WriteLineAsync("</body>");
            await writer.WriteLineAsync("</html>");
        }

        //private void MakePdf(string path)
        //{
        //    int cols = 4;

        //    string filename = Path.Combine(path, "qr.pdf");
        //    if (File.Exists(filename))
        //        File.Delete(filename);
        //    using PdfWriter writer = new(filename);
        //    using PdfDocument pdf = new(writer);
        //    using Document document = new(pdf, PageSize.A4);

        //    float margin = 36;
        //    document.SetMargins(margin, margin, margin, margin);

        //    PdfFont font;
        //    try
        //    {
        //        string fontFile = Directory.GetFiles(AppContext.BaseDirectory, "*.ttf").First();
        //        font = PdfFontFactory.CreateFont(fontFile, PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
        //    }
        //    catch
        //    {
        //        font = PdfFontFactory.CreateFont("Times-Roman", "Cp1251", PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
        //    }


        //    var devices = ApplicationContext.Instance().Devices.Select(d => new
        //    {
        //        d.Complect.Cabinet.Building,
        //        d.Complect.Cabinet,
        //        d.Complect,
        //        d.Serial
        //    }).OrderBy(d => d.Building.Name)
        //    .ThenBy(d => d.Cabinet.Name)
        //    .ThenBy(d => d.Complect.Name)
        //    .ToList();

        //    Table table = new Table(UnitValue.CreatePercentArray(cols)).UseAllAvailableWidth().SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(Border.NO_BORDER);

        //    foreach (var device in devices)
        //    {
        //        string image = Path.Combine(path, $"{device.Serial}.jpg");
        //        if (!File.Exists(image))
        //            continue;

        //        Div cellContent = new Div().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetPadding(5);

        //        try
        //        {
        //            ImageData imageData = ImageDataFactory.Create(image);
        //            Image pdfImage = new Image(imageData).SetWidth(75).SetHeight(75);

        //            cellContent.Add(pdfImage);
        //        }
        //        catch
        //        {

        //        }

        //        string sign = $"{device.Complect.Cabinet.Building.Name}\n{device.Complect.Cabinet.Name}\n{device.Complect.Name}\n{device.Serial}";

        //        Paragraph paragraph = new Paragraph(sign).SetFontSize(8).SetMarginTop(3).SetTextAlignment(TextAlignment.CENTER).SetFont(font);

        //        cellContent.Add(paragraph);

        //        Cell cell = new Cell().Add(cellContent).SetBorder(Border.NO_BORDER).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetPadding(0);

        //        table.AddCell(cell);
        //    }

        //    document.Add(table);
        //    document.Close();
        //}
    }
}
