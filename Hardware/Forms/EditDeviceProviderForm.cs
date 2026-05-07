using Hardware.Models;

namespace Hardware.Forms
{
    public partial class EditDeviceProviderForm : Form
    {
        private DeviceProvider? deviceProvider;
        private readonly ConfigManager configManager = new();
        public EditDeviceProviderForm(DeviceProvider? deviceProvider)
        {
            InitializeComponent();
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
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            bool providerHasDevices = deviceProvider?.Devices.Count != 0;
            removeBtn.Enabled = !providerHasDevices;
        }

        private async Task<string?> AddDeviceProvider()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            deviceProvider = new() { Name = nameTBox.Text };
            await context.DeviceProviders.AddAsync(deviceProvider);
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

        private async Task<string?> EditDeviceProvider()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            deviceProvider.Name = nameTBox.Text;
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

        private async Task<string?> RemoveDeviceProvider()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            context.DeviceProviders.Remove(deviceProvider);
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
