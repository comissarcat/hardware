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
			buildingCBox.DataSource = context.Buildings.OrderBy(b => b.Name)
													   .ToList();
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
			var cabinetHasDevices = context.Devices.Include(d => d.Complect.Cabinet)
												   .Where(d => d.Complect.Cabinet == cabinet)
												   .Any();
			removeBtn.Enabled = !cabinetHasDevices;
		}

		private void RefreshBuildings()
		{
			var selectedItem = buildingCBox.SelectedItem;

			buildingCBox.DataSource = context.Buildings.OrderBy(b => b.Name)
													   .ToList();

			if (selectedItem is not null)
				if (buildingCBox.Items.Contains(selectedItem))
					buildingCBox.SelectedItem = selectedItem;
		}

		private async Task<string?> AddCabinet()
		{
			cabinet = new() { Name = nameTBox.Text, Building = (Building)buildingCBox.SelectedItem };
			await context.Cabinets.AddAsync(cabinet);
			try
			{
				await context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return $"{ex.Message}\n{ex.InnerException}";
			}
			return null;
		}

		private async Task<string?> EditCabinet()
		{
			List<string> before = [];
			List<string> after = [];
			var devicesInCabinet = context.Devices.Include(d => d.Complect.Cabinet.Building)
												  .Where(d => d.Complect.Cabinet == cabinet)
												  .ToList();
			if (devicesInCabinet.Count > 0)
			{
				foreach (var device in devicesInCabinet)
					before.Add(device.ToStringForHistory());

				cabinet.Name = nameTBox.Text;
				cabinet.Building = (Building)buildingCBox.SelectedItem;

				foreach (var device in devicesInCabinet)
					after.Add(device.ToStringForHistory());

				for (int i = 0; i < devicesInCabinet.Count; i++)
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
			catch (Exception ex)
			{
				return $"{ex.Message}\n{ex.InnerException}";
			}
			return null;
		}

		private async Task<string?> RemoveCabinet()
		{
			context.Cabinets.Remove(cabinet);
			try
			{
				await context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return $"{ex.Message}\n{ex.InnerException}";
			}
			return null;
		}

		private async void addBtn_Click(object sender, EventArgs e)
		{
			string? result = await AddCabinet();
			if (result != null)
				MessageBox.Show(result, "Ошибка", MessageBoxButtons.OK);
			else
			{
				MessageBox.Show("Успешно добавлено", "Успех", MessageBoxButtons.OK);
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		private async void editBtn_Click(object sender, EventArgs e)
		{
			string? result = await EditCabinet();
			if (result != null)
				MessageBox.Show(result, "Ошибка", MessageBoxButtons.OK);
			else
			{
				MessageBox.Show("Успешно изменено", "Успех", MessageBoxButtons.OK);
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		private async void removeBtn_Click(object sender, EventArgs e)
		{
			string? result = await RemoveCabinet();
			if (result != null)
				MessageBox.Show(result, "Ошибка", MessageBoxButtons.OK);
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
