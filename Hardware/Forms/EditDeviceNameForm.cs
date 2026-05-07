using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware.Forms
{
    public partial class EditDeviceNameForm : Form
    {
        private DeviceName? deviceName;
        private readonly ConfigManager configManager = new();

        public EditDeviceNameForm(DeviceName? deviceName, DeviceType deviceType)
        {
            InitializeComponent();
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            DialogResult = DialogResult.Cancel;
            typeCBox.DataSource = context.DeviceTypes.OrderBy(dt => dt.Name)
                                            .Include(dt => dt.DeviceNames)
                                            .ThenInclude(dn => dn.Devices)
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
            bool nameHasDevices = deviceName?.Devices.Count != 0;
            removeBtn.Enabled = !nameHasDevices;
        }

        private void RefreshTypes()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            object? selectedItem = typeCBox.SelectedItem;

            typeCBox.DataSource = context.DeviceTypes.OrderBy(dt => dt.Name)
                                            .Include(dt => dt.DeviceNames)
                                            .ThenInclude(dn => dn.Devices)
                                            .ToList();

            if (typeCBox.Items.Count == 0)
                typeCBox.Text = string.Empty;
            if (selectedItem is not null)
                if (typeCBox.Items.Contains(selectedItem))
                    typeCBox.SelectedItem = selectedItem;
        }

        private async Task<string?> AddDeviceName()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            deviceName = new() { Name = nameTBox.Text, DeviceType = (DeviceType)typeCBox.SelectedItem };
            await context.DeviceNames.AddAsync(deviceName);
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

        private async Task<string?> EditDeviceName()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            deviceName.Name = nameTBox.Text;
            deviceName.DeviceType = (DeviceType)typeCBox.SelectedItem;
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

        private async Task<string?> RemoveDeviceName()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            context.DeviceNames.Remove(deviceName);
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
            DeviceType? deviceType = (DeviceType?)typeCBox.SelectedItem;
            EditDeviceTypeForm form = new(deviceType);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
                RefreshTypes();
        }
    }
}
