using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware.Forms
{
	public partial class EditComplectForm : Form
	{
		readonly ApplicationContext context;
		Complect? complect;
		public EditComplectForm(Complect? complect, Cabinet cabinet)
		{
			InitializeComponent();
			context = ApplicationContext.Instance();
			DialogResult = DialogResult.Cancel;
			buildingCBox.DataSource = context.Buildings.OrderBy(b => b.Name)
													   .ToList();
			this.complect = complect;
			if (this.complect is not null)
			{
				idTBox.Text = this.complect.Id.ToString();
				nameTBox.Text = this.complect.Name;

				buildingCBox.SelectedItem = this.complect.Cabinet.Building;

				cabinetCBox.DataSource = context.Cabinets.Where(c => c.Building == buildingCBox.SelectedItem)
														 .OrderBy(c => c.Name)
														 .ToList();
				cabinetCBox.SelectedItem = this.complect.Cabinet;

				editBtn.Enabled = true;
				SwitchRemoveBtn();
			}
			else
			{
				buildingCBox.SelectedItem = cabinet.Building;
				cabinetCBox.DataSource = context.Cabinets.Where(c => c.Building == buildingCBox.SelectedItem)
														 .OrderBy(c => c.Name)
														 .ToList();
				cabinetCBox.SelectedItem = cabinet;
			}
		}

		private void SwitchRemoveBtn()
		{
			var complectHasDevices = context.Devices.Where(d => d.Complect == complect)
													.Any();
			removeBtn.Enabled = !complectHasDevices;
		}

		private void SwitchEditCabinetBtn()
		{
			if (buildingCBox.SelectedIndex != -1)
				editCabinetBtn.Enabled = true;
			else
				editCabinetBtn.Enabled = false;
		}

		private void RefreshAll()
		{
			RefreshBuildings();
			RefreshCabinets();
		}

		private void RefreshBuildings()
		{
			var selectedItem = buildingCBox.SelectedItem;

			buildingCBox.DataSource = context.Buildings.OrderBy(b => b.Name)
													   .ToList();

			if (buildingCBox.Items.Count == 0)
				buildingCBox.Text = string.Empty;
			if (selectedItem is not null)
				if (buildingCBox.Items.Contains(selectedItem))
					buildingCBox.SelectedItem = selectedItem;
			SwitchEditCabinetBtn();
		}

		private void RefreshCabinets()
		{
			var selectedItem = cabinetCBox.SelectedItem;

			cabinetCBox.DataSource = context.Cabinets.Where(c => c.Building == buildingCBox.SelectedItem)
													 .OrderBy(c => c.Name)
													 .ToList();

			if (cabinetCBox.Items.Count == 0)
				cabinetCBox.Text = string.Empty;
			else if (selectedItem is not null)
				if (cabinetCBox.Items.Contains(selectedItem))
					cabinetCBox.SelectedItem = selectedItem;
		}

		private async Task<string?> AddComplect()
		{
			complect = new() { Name = nameTBox.Text, Cabinet = (Cabinet)cabinetCBox.SelectedItem };
			await context.Complects.AddAsync(complect);
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

		private async Task<string?> EditComplect()
		{
			List<string> before = [];
			List<string> after = [];
			var devicesInComplect = context.Devices.Include(d => d.Complect.Cabinet.Building)
												   .Where(d => d.Complect == complect)
												   .ToList();
			if (devicesInComplect.Count > 0)
			{
				foreach (var device in devicesInComplect)
					before.Add(device.ToStringForHistory());

				complect.Name = nameTBox.Text;
				complect.Cabinet = (Cabinet)cabinetCBox.SelectedItem;

				foreach (var device in devicesInComplect)
					after.Add(device.ToStringForHistory());

				for (int i = 0; i < devicesInComplect.Count; i++)
					await context.History.AddAsync(new History() { Before = before[i], After = after[i] });
			}
			else
			{
				complect.Name = nameTBox.Text;
				complect.Cabinet = (Cabinet)cabinetCBox.SelectedItem;
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

		private async Task<string?> RemoveComplect()
		{
			context.Complects.Remove(complect);
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
			string? result = await AddComplect();
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
			string? result = await EditComplect();
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
			string? result = await RemoveComplect();
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
			var building = (Building)buildingCBox.SelectedItem;
			var form = new EditBuildingForm(building);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshAll();
		}

		private void editCabinetBtn_Click(object sender, EventArgs e)
		{
			var cabinet = (Cabinet?)cabinetCBox.SelectedItem;
			var building = cabinet is null ? (Building)buildingCBox.SelectedItem : cabinet.Building;
			var form = new EditCabinetForm(cabinet, building);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshAll();
		}

		private void buildingCBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshCabinets();
		}
	}
}
