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
				SwitchRemoveBtn();
			}
		}

		private void SwitchRemoveBtn()
		{
			var buildingHasDevices = context.Devices.Include(d => d.Complect.Cabinet.Building)
													.Where(d => d.Complect.Cabinet.Building == building)
													.Any();
			removeBtn.Enabled = !buildingHasDevices;
		}

		private async Task<string?> AddBuilding()
		{
			building = new() { Name = nameTBox.Text };
			await context.Buildings.AddAsync(building);
			try
			{
				await context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return null;
		}

		private async Task<string?> EditBuilding()
		{
			building.Name = nameTBox.Text;
			try
			{
				await context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return null;
		}

		private async Task<string?> RemoveBuilding()
		{
			context.Buildings.Remove(building);
			try
			{
				await context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return null;
		}

		private async void addBtn_Click(object sender, EventArgs e)
		{
			string? result = await AddBuilding();
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
			string? result = await EditBuilding();
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
			string? result = await RemoveBuilding();
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
	}
}
