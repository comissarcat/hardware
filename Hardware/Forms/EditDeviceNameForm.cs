using Hardware.Models;

namespace Hardware.Forms
{
	public partial class EditDeviceNameForm : Form
	{
		readonly ApplicationContext context;
		private DeviceName? deviceName;
		public EditDeviceNameForm(DeviceName? deviceName, DeviceType deviceType)
		{
			InitializeComponent();
			context = ApplicationContext.Instanse();
			DialogResult = DialogResult.Cancel;
			typeCBox.DataSource = context.DeviceTypes.OrderBy(d => d.Name)
													 .ToList();
			this.deviceName = deviceName;
			if (this.deviceName is not null)
			{
				idTBox.Text = this.deviceName.Id.ToString();
				nameTBox.Text = this.deviceName.Name;
				typeCBox.SelectedItem = this.deviceName.DeviceType;
				editBtn.Enabled = true;
				SwitchRemoveBtn();
			}
			typeCBox.SelectedItem = deviceType;
		}

		private void SwitchRemoveBtn()
		{
			var nameHasDevices = context.Devices.Where(d => d.DeviceName == deviceName)
												.Any();
			removeBtn.Enabled = !nameHasDevices;
		}

		private void RefreshTypes()
		{
			var selectedItem = typeCBox.SelectedItem;

			typeCBox.DataSource = context.DeviceTypes.OrderBy(d => d.Name)
													 .ToList();

			if (typeCBox.Items.Count == 0)
				typeCBox.Text = string.Empty;
			if (selectedItem is not null)
				if (typeCBox.Items.Contains(selectedItem))
					typeCBox.SelectedItem = selectedItem;
		}

		private async Task<string?> AddDeviceName()
		{
			deviceName = new() { Name = nameTBox.Text, DeviceType = (DeviceType)typeCBox.SelectedItem };
			await context.DeviceNames.AddAsync(deviceName);
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

		private async Task<string?> EditDeviceName()
		{
			deviceName.Name = nameTBox.Text;
			deviceName.DeviceType = (DeviceType)typeCBox.SelectedItem;
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

		private async Task<string?> RemoveDeviceName()
		{
			context.DeviceNames.Remove(deviceName);
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
			string? result = await AddDeviceName();
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
			string? result = await EditDeviceName();
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
			string? result = await RemoveDeviceName();
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

		private void editDeviceTypeBtn_Click(object sender, EventArgs e)
		{

			var deviceType = (DeviceType?)typeCBox.SelectedItem;
			var form = new EditDeviceTypeForm(deviceType);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshTypes();
		}
	}
}
