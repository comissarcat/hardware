using Hardware.Models;

namespace Hardware.Forms
{
	public partial class EditDeviceProviderForm : Form
	{
		private readonly ApplicationContext context;
		private DeviceProvider? deviceProvider;
		public EditDeviceProviderForm(DeviceProvider? deviceProvider)
		{
			InitializeComponent();
			context = ApplicationContext.Instanse();
			DialogResult = DialogResult.Cancel;
			this.deviceProvider = deviceProvider;
			if (this.deviceProvider is not null)
			{
				idTBox.Text = this.deviceProvider.Id.ToString();
				nameTBox.Text = this.deviceProvider.Name;
				editBtn.Enabled = true;
				SwitchRemoveBtn();
			}
		}

		private void SwitchRemoveBtn()
		{
			var providerHasDevices = context.Devices.Where(d => d.DeviceProvider == deviceProvider)
													.Any();
			removeBtn.Enabled = !providerHasDevices;
		}

		private async Task<string?> AddDeviceProvider()
		{
			deviceProvider = new() { Name = nameTBox.Text };
			await context.DeviceProviders.AddAsync(deviceProvider);
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

		private async Task<string?> EditDeviceProvider()
		{
			deviceProvider.Name = nameTBox.Text;
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

		private async Task<string?> RemoveDeviceProvider()
		{
			context.DeviceProviders.Remove(deviceProvider);
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
			string? result = await AddDeviceProvider();
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
			string? result = await EditDeviceProvider();
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
			string? result = await RemoveDeviceProvider();
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
