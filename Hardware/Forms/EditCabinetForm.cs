using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware.Forms
{
	public partial class EditCabinetForm : Form
	{
		readonly ApplicationContext context;
		private Cabinet? cabinet;
		public EditCabinetForm(Cabinet? cabinet, Building building)
		{
			InitializeComponent();
			context = ApplicationContext.Instanse();
			DialogResult = DialogResult.Cancel;
			buildingCBox.DataSource = context.Buildings.ToList();
			this.cabinet = cabinet;
			if (this.cabinet is not null)
			{
				idTBox.Text = this.cabinet.Id.ToString();
				nameTBox.Text = this.cabinet.Name;
				buildingCBox.SelectedItem = this.cabinet.Building;
				editBtn.Enabled = true;
				SwitchRemoveBtn();
			}
			buildingCBox.SelectedItem = building;
		}

		private void SwitchRemoveBtn()
		{
			var cabinetHasDevices = context.Devices.Include(d => d.Complect.Cabinet).Where(d => d.Complect.Cabinet == cabinet).Any();
			removeBtn.Enabled = !cabinetHasDevices;
		}

		private void RefreshBuildings()
		{
			var selectedItem = buildingCBox.SelectedItem;

			buildingCBox.DataSource = context.Buildings.ToList();

			if (selectedItem is not null)
				if (buildingCBox.Items.Contains(selectedItem))
					buildingCBox.SelectedItem = selectedItem;
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
			List<string> before = [];
			List<string> after = [];
			var cabinetDevices = await context.Devices.Include(d => d.Complect.Cabinet.Building).Where(d => d.Complect.Cabinet == cabinet).ToListAsync();
			if (cabinetDevices.Count > 0)
			{
				foreach (var device in cabinetDevices)
					before.Add($"{device.Complect.Cabinet.Building.Name} -> {device.Complect.Cabinet.Name} -> {device.Complect.Name} -> {device.DeviceName} {device.Serial} {device.Inventory}");
				cabinet.Name = nameTBox.Text;
				cabinet.Building = (Building)buildingCBox.SelectedItem;
				foreach (var device in cabinetDevices)
					after.Add($"{device.Complect.Cabinet.Building.Name} -> {device.Complect.Cabinet.Name} -> {device.Complect.Name} -> {device.DeviceName} {device.Serial} {device.Inventory}");
				for (int i = 0; i < cabinetDevices.Count; i++)
					await context.History.AddAsync(new History() { Before = before[i], After = after[i] });
			}
			else
			{
				cabinet.Name = nameTBox.Text;
				cabinet.Building = (Building)buildingCBox.SelectedItem;
			}
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

		private void editBuildingBtn_Click(object sender, EventArgs e)
		{

			var building = (Building?)buildingCBox.SelectedItem;
			var form = new EditBuildingForm(building);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshBuildings();
		}
	}
}
