using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware.Forms
{
    public partial class EditDeviceTypeForm : Form
    {
        private DeviceType? deviceType;
        private readonly ConfigManager configManager = new();
        public EditDeviceTypeForm(DeviceType? deviceType)
        {
            InitializeComponent();
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
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            bool typeHasDevices = deviceType.DeviceNames.Any(dn => dn.Devices.Count != 0);
            removeBtn.Enabled = !typeHasDevices;
        }

        private async Task<string?> AddDeviceType()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            deviceType = new() { Name = nameTBox.Text };
            await context.DeviceTypes.AddAsync(deviceType);
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

        private async Task<string?> EditDeviceType()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            deviceType = await context.DeviceTypes.FindAsync(deviceType.Id);
            deviceType.Name = nameTBox.Text;
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

        private async Task<string?> RemoveDeviceType()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            deviceType = await context.DeviceTypes.FindAsync(deviceType.Id);
            context.DeviceTypes.Remove(deviceType);
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
            string? result = await AddDeviceType();
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
            string? result = await EditDeviceType();
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
            string? result = await RemoveDeviceType();
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
