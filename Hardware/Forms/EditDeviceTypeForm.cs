using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware.Forms
{
	public partial class EditDeviceTypeForm : Form
	{
		private readonly ApplicationContext context;
		private DeviceType? deviceType;
		public EditDeviceTypeForm(DeviceType? deviceType)
		{
			InitializeComponent();
			context = ApplicationContext.Instanse();
			DialogResult = DialogResult.Cancel;
			this.deviceType = deviceType;
			if (this.deviceType is not null)
			{
				idTBox.Text = this.deviceType.Id.ToString();
				nameTBox.Text = this.deviceType.Name;
				editBtn.Enabled = true;
				SwitchRemoveBtn();
			}
		}

		private void SwitchRemoveBtn()
		{
			var typeHasDevices = context.Devices.Include(d => d.DeviceName.DeviceType).Where(d => d.DeviceName.DeviceType == deviceType).Any();
			removeBtn.Enabled = !typeHasDevices;
		}

		private async Task<bool> AddDeviceType()
		{
			deviceType = new() { Name = nameTBox.Text };
			await context.DeviceTypes.AddAsync(deviceType);
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

		private async Task<bool> EditDeviceType()
		{
			deviceType.Name = nameTBox.Text;
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

		private async Task<bool> RemoveDeviceType()
		{
			context.DeviceTypes.Remove(deviceType);
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
			if (!await AddDeviceType())
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
			if (!await EditDeviceType())
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
			if (!await RemoveDeviceType())
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
