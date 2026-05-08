using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware.Forms
{
    public partial class EditDeviceForm : Form
    {
        Device? device;
        private readonly ConfigManager configManager = new();

        public EditDeviceForm(Device? device, Complect complect)
        {
            InitializeComponent();
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            DialogResult = DialogResult.Cancel;
            buildingCBox.DataSource = context.Buildings.OrderBy(b => b.Name)
                                                       .Include(b => b.Cabinets)
                                                       .ThenInclude(c => c.Complects)
                                                       .ThenInclude(c => c.Devices)
                                                       .AsSplitQuery()
                                                       .ToList();
            deviceNameCBox.DataSource = context.DeviceNames.Include(dn => dn.DeviceType)
                                                           .OrderBy(dn => dn.DeviceType.Name)
                                                           .ThenBy(dn => dn.Name)
                                                           .ToList();
            deviceProviderCBox.DataSource = context.DeviceProviders.OrderBy(d => d.Name)
                                                                   .ToList();
            this.device = device;
            if (this.device is not null)
            {
                idTBox.Text = this.device.Id.ToString();
                serialTBox.Text = this.device.Serial;
                inventoryTBox.Text = this.device.Inventory;
                notesTBox.Text = this.device.Notes;

                buildingCBox.SelectedItem = this.device.Complect.Cabinet.Building;

                cabinetCBox.DataSource = (buildingCBox.SelectedItem as Building).Cabinets.ToList();
                cabinetCBox.SelectedItem = this.device.Complect.Cabinet;

                complectCBox.DataSource = (cabinetCBox.SelectedItem as Cabinet).Complects.ToList();
                complectCBox.SelectedItem = this.device.Complect;

                deviceNameCBox.SelectedItem = this.device.DeviceName;
                deviceProviderCBox.SelectedItem = this.device.DeviceProvider;

                removeBtn.Enabled = true;
                editBtn.Enabled = true;
            }
            else
            {
                buildingCBox.SelectedItem = complect.Cabinet.Building;

                cabinetCBox.DataSource = (buildingCBox.SelectedItem as Building).Cabinets.ToList();
                cabinetCBox.SelectedItem = complect.Cabinet;

                complectCBox.DataSource = (cabinetCBox.SelectedItem as Cabinet).Complects.ToList();
                complectCBox.SelectedItem = complect;
            }
        }

        private void SwitchEditCabinetBtn()
        {
            if (buildingCBox.SelectedIndex != -1)
                editCabinetBtn.Enabled = true;
            else
                editCabinetBtn.Enabled = false;
        }

        private void SwitchEditComplectBtn()
        {
            if (cabinetCBox.SelectedIndex != -1)
                editComplectBtn.Enabled = true;
            else
                editComplectBtn.Enabled = false;
        }

        private void SwitchEditDeviceNameBtn()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            editDeviceNameBtn.Enabled = context.DeviceTypes.Any();
        }

        private void RefreshDeviceLocations()
        {
            RefreshBuildings();
            RefreshCabinets();
            RefreshComplects();
        }

        private void RefreshBuildings()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            object? selectedItem = buildingCBox.SelectedItem;

            buildingCBox.DataSource = context.Buildings.OrderBy(b => b.Name)
                                                       .Include(b => b.Cabinets)
                                                       .ThenInclude(c => c.Complects)
                                                       .ThenInclude(c => c.Devices)
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
            object? selectedItem = cabinetCBox.SelectedItem;

            cabinetCBox.DataSource = (buildingCBox.SelectedItem as Building).Cabinets.ToList();

            if (cabinetCBox.Items.Count == 0)
                cabinetCBox.Text = string.Empty;
            else if (selectedItem is not null)
                if (cabinetCBox.Items.Contains(selectedItem))
                    cabinetCBox.SelectedItem = selectedItem;
            SwitchEditComplectBtn();
        }

        private void RefreshComplects()
        {
            object? selectedItem = complectCBox.SelectedItem;

            complectCBox.DataSource = (cabinetCBox.SelectedItem as Cabinet).Complects.ToList();

            if (complectCBox.Items.Count == 0)
                complectCBox.Text = string.Empty;
            else if (selectedItem is not null)
                if (complectCBox.Items.Contains(selectedItem))
                    complectCBox.SelectedItem = selectedItem;
        }

        private void RefreshDeviceNames()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            object? selectedItem = deviceNameCBox.SelectedItem;

            deviceNameCBox.DataSource = context.DeviceNames.Include(dn => dn.DeviceType)
                                                           .OrderBy(dn => dn.DeviceType.Name)
                                                           .ThenBy(dn => dn.Name)
                                                           .ToList();

            if (deviceNameCBox.Items.Count == 0)
                deviceNameCBox.Text = string.Empty;
            else if (selectedItem is not null)
                if (deviceNameCBox.Items.Contains(selectedItem))
                    deviceNameCBox.SelectedItem = selectedItem;
            SwitchEditDeviceNameBtn();
        }

        private void RefreshDeviceProviders()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            object? selectedItem = deviceProviderCBox.SelectedItem;

            deviceProviderCBox.DataSource = context.DeviceProviders.OrderBy(d => d.Name)
                                                                   .ToList();

            if (deviceProviderCBox.Items.Count == 0)
                deviceProviderCBox.Text = string.Empty;
            else if (selectedItem is not null)
                if (deviceProviderCBox.Items.Contains(selectedItem))
                    deviceProviderCBox.SelectedItem = selectedItem;
        }

        private async Task<string?> AddDevice()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            string before = string.Empty;
            device = new()
            {
                Serial = serialTBox.Text,
                Inventory = inventoryTBox.Text,
                Complect = (Complect)complectCBox.SelectedItem,
                DeviceName = (DeviceName)deviceNameCBox.SelectedItem,
                DeviceProvider = (DeviceProvider)deviceProviderCBox.SelectedItem,
                Notes = notesTBox.Text
            };
            string after = device.ToStringForHistory();
            await context.Devices.AddAsync(device);
            await context.History.AddAsync(new History() { Before = before, After = after });
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

        private async Task<string?> EditDevice()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            string before = device.ToStringForHistory();
            device.Serial = serialTBox.Text;
            device.Inventory = inventoryTBox.Text;
            device.Complect = (Complect)complectCBox.SelectedItem;
            device.DeviceName = (DeviceName)deviceNameCBox.SelectedItem;
            device.DeviceProvider = (DeviceProvider)deviceProviderCBox.SelectedItem;
            device.Notes = notesTBox.Text;
            string after = device.ToStringForHistory();
            await context.History.AddAsync(new History() { Before = before, After = after });
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

        private async Task<string?> RemoveDevice()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            string before = device.ToStringForHistory();
            string after = string.Empty;
            context.Devices.Remove(device);
            await context.History.AddAsync(new History() { Before = before, After = after });
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
            string? result = await AddDevice();
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
            string? result = await EditDevice();
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
            string? result = await RemoveDevice();
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
            Building? building = (Building)buildingCBox.SelectedItem;
            EditBuildingForm form = new(building);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
                RefreshDeviceLocations();
        }

        private void editCabinetBtn_Click(object sender, EventArgs e)
        {
            Cabinet? cabinet = (Cabinet?)cabinetCBox.SelectedItem;
            Building? building = cabinet is null ? (Building)buildingCBox.SelectedItem : cabinet.Building;
            EditCabinetForm form = new(cabinet, building);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
                RefreshDeviceLocations();
        }

        private void editComplectBtn_Click(object sender, EventArgs e)
        {
            Complect? complect = (Complect?)complectCBox.SelectedItem;
            Cabinet? cabinet = complect is null ? (Cabinet)cabinetCBox.SelectedItem : complect.Cabinet;
            EditComplectForm form = new(complect, cabinet);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
                RefreshDeviceLocations();
        }

        private void editDeviceNameBtn_Click(object sender, EventArgs e)
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            DeviceName? deviceName = (DeviceName?)deviceNameCBox.SelectedItem;
            DeviceType deviceType = deviceName is null ? context.DeviceTypes.First() : deviceName.DeviceType;
            EditDeviceNameForm form = new(deviceName, deviceType);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
                RefreshDeviceNames();
        }

        private void editDeviceProviderBtn_Click(object sender, EventArgs e)
        {
            DeviceProvider? deviceProvider = (DeviceProvider?)deviceProviderCBox.SelectedItem;
            EditDeviceProviderForm form = new(deviceProvider);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
                RefreshDeviceProviders();
        }

        private void buildingCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshCabinets();
        }

        private void cabinetCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshComplects();
        }
    }
}
