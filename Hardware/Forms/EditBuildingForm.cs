using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware.Forms
{
	public partial class EditBuildingForm : Form
	{
		private readonly ApplicationContext context;
		private Building? building;
		public EditBuildingForm(Building? building)
		{
			InitializeComponent();
			context = ApplicationContext.Instanse();
			DialogResult = DialogResult.Cancel;
			this.building = building;
			if (this.building is not null)
			{
				idTBox.Text = this.building.Id.ToString();
				nameTBox.Text = this.building.Name;
				editBtn.Enabled = true;
				var buildingDevices = context.Devices.Include(d => d.Complect.Cabinet.Building).ToList();
				if (buildingDevices.Count == 0)
					removeBtn.Enabled = true;
			}
		}

		private async Task<bool> AddBuilding()
		{
			building = new() { Name = nameTBox.Text };
			await context.Buildings.AddAsync(building);
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

		private async Task<bool> EditBuilding()
		{
			building.Name = nameTBox.Text;
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

		private async Task<bool> RemoveBuilding()
		{
			context.Buildings.Remove(building);
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
			if (!await AddBuilding())
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
			if (!await EditBuilding())
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
			if (!await RemoveBuilding())
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
