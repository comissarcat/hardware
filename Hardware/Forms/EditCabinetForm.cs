using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware.Forms
{
	public partial class EditCabinetForm : Form
	{
		private readonly ApplicationContext context;
		private Cabinet? cabinet;
		public EditCabinetForm(ApplicationContext context, Cabinet? cabinet, Building building)
		{
			InitializeComponent();
			this.context = context;
			DialogResult = DialogResult.Cancel;
			buildingCBox.DataSource = context.Buildings.ToList();
			buildingCBox.DisplayMember = "Name";
			this.cabinet = cabinet;
			if (this.cabinet is not null)
			{
				idTBox.Text = this.cabinet.Id.ToString();
				nameTBox.Text = this.cabinet.Name;
				editBtn.Enabled = true;
				building = cabinet.Building;
				var cabinetDevices = context.Devices.Include(d => d.Complect.Cabinet).Where(d => d.Complect.Cabinet == cabinet).ToList();
				if (cabinetDevices.Count == 0)
					removeBtn.Enabled = true;
			}
			buildingCBox.SelectedItem = building;
		}

		private async Task<bool> AddCabinet()
		{
			cabinet = new() { Name = nameTBox.Text, Building = (Building)buildingCBox.SelectedItem };
			await context.Cabinets.AddAsync(cabinet);
			try
			{
				await context.SaveChangesAsync();
			}
			catch
			{
				return false;
			}
			return true;
		}

		private async Task<bool> EditCabinet()
		{
			List<string> b = [];
			List<string> a = [];
			var cabinetDevices = await context.Devices.Include(d => d.Complect.Cabinet.Building).Where(d => d.Complect.Cabinet == cabinet).ToListAsync();
			if (cabinetDevices.Count > 0)
			{
				foreach (var device in cabinetDevices)
					b.Add($"{device.Complect.Cabinet.Building.Name} -> {device.Complect.Cabinet.Name} -> {device.Complect.Name} -> {device.DeviceName} {device.Serial} {device.Inventory}");
				cabinet.Name = nameTBox.Text;
				cabinet.Building = (Building)buildingCBox.SelectedItem;
				foreach (var device in cabinetDevices)
					a.Add($"{device.Complect.Cabinet.Building.Name} -> {device.Complect.Cabinet.Name} -> {device.Complect.Name} -> {device.DeviceName} {device.Serial} {device.Inventory}");
				for (int i = 0; i < cabinetDevices.Count; i++)
					await context.History.AddAsync(new History() { Before = b[i], After = a[i] });
				try
				{
					await context.SaveChangesAsync();
				}
				catch
				{
					return false;
				}
			}
			return true;
		}

		private async Task<bool> RemoveCabinet()
		{
			context.Cabinets.Remove(cabinet);
			try
			{
				await context.SaveChangesAsync();
			}
			catch
			{
				return false;
			}
			return true;
		}

		private async void addBtn_Click(object sender, EventArgs e)
		{
			if (!await AddCabinet())
				MessageBox.Show("А нихера", "Ошибка", MessageBoxButtons.OK);
			else
			{
				MessageBox.Show("Успешно добавлено", "Успех", MessageBoxButtons.OK);
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		private async void editBtn_Click(object sender, EventArgs e)
		{
			if (!await EditCabinet())
				MessageBox.Show("А нихера", "Ошибка", MessageBoxButtons.OK);
			else
			{
				MessageBox.Show("Успешно изменено", "Успех", MessageBoxButtons.OK);
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		private async void removeBtn_Click(object sender, EventArgs e)
		{
			if (!await RemoveCabinet())
				MessageBox.Show("А нихера", "Ошибка", MessageBoxButtons.OK);
			else
			{
				MessageBox.Show("Успешно удалено", "Успех", MessageBoxButtons.OK);
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void cancelBtn_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
