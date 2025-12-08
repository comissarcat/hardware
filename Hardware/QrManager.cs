using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using QRCoder;
using System.Drawing.Drawing2D;
using HorizontalAlignment = iText.Layout.Properties.HorizontalAlignment;
using Image = iText.Layout.Element.Image;
using Path = System.IO.Path;

namespace Hardware
{
	internal class QrManager
	{
		long _imageIdCounter = 1;

		public void CreateQrImages()
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
			}

			MakePdf(path);

			MessageBox.Show($"Выгрузка завершена по пути {path}");
		}

		private void MakePdf(string path)
		{
			int cols = 4;

			string filename = Path.Combine(path, "qr.pdf");
			if (File.Exists(filename))
				File.Delete(filename);
			using PdfWriter writer = new(filename);
			using PdfDocument pdf = new(writer);
			using Document document = new(pdf, PageSize.A4);

			float margin = 36;
			document.SetMargins(margin, margin, margin, margin);

			PdfFont font;
			try
			{
				string fontFile = Directory.GetFiles(AppContext.BaseDirectory, "*.ttf").First();
				font = PdfFontFactory.CreateFont(fontFile, PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
			}
			catch
			{
				font = PdfFontFactory.CreateFont("Times-Roman", "Cp1251", PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
			}


			var devices = ApplicationContext.Instance().Devices.Select(d => new
			{
				d.Complect.Cabinet.Building,
				d.Complect.Cabinet,
				d.Complect,
				d.Serial
			}).OrderBy(d => d.Building.Name)
			.ThenBy(d => d.Cabinet.Name)
			.ThenBy(d => d.Complect.Name)
			.ToList();

			Table table = new Table(UnitValue.CreatePercentArray(cols)).UseAllAvailableWidth().SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(Border.NO_BORDER);

			foreach (var device in devices)
			{
				string image = Path.Combine(path, $"{device.Serial}.jpg");
				if (!File.Exists(image))
					continue;

				Div cellContent = new Div().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetPadding(5);

				try
				{
					ImageData imageData = ImageDataFactory.Create(image);
					Image pdfImage = new Image(imageData).SetWidth(75).SetHeight(75);

					cellContent.Add(pdfImage);
				}
				catch
				{

				}

				string sign = $"{device.Complect.Cabinet.Building.Name}\n{device.Complect.Cabinet.Name}\n{device.Complect.Name}\n{device.Serial}";

				Paragraph paragraph = new Paragraph(sign).SetFontSize(8).SetMarginTop(3).SetTextAlignment(TextAlignment.CENTER).SetFont(font);

				cellContent.Add(paragraph);

				Cell cell = new Cell().Add(cellContent).SetBorder(Border.NO_BORDER).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetPadding(0);

				table.AddCell(cell);
			}

			document.Add(table);
			document.Close();
		}
	}
}
