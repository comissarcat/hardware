using Hardware.Forms;
using Hardware.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Text.RegularExpressions;

namespace Hardware
{
    public partial class MainForm : Form
    {
        private System.Windows.Forms.Timer searchTimerLeft;
        private System.Windows.Forms.Timer searchTimerRight;
        private const int searchTimerDelayMS = 500;
        private readonly ConfigManager configManager = new();

        public MainForm()
        {
            InitializeComponent();
            Load += OnLoad;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            EnterPasswordForm form = new();
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                Init();
            }
            else
                Close();
        }

        private void Init()
        {
            InitializeButtons();
            RefreshBuildings();
            InitializeHistoryDGW();
            InitializeFullListDGW();
            searchTimerLeft = new()
            {
                Interval = searchTimerDelayMS
            };
            searchTimerLeft.Tick += SearchTimerTick;
            searchTimerRight = new()
            {
                Interval = searchTimerDelayMS
            };
            searchTimerRight.Tick += SearchTimerTick;
            progressBar1.Visible = false;
            progressBar1.Value = 0;
        }

        private void InitializeButtons()
        {
            editBuildingBtnLeft.Text = "Изменить";
            addBuildingBtnLeft.Text = "Добавить";

            editBuildingBtnRight.Text = "Изменить";
            addBuildingBtnRight.Text = "Добавить";

            editCabinetBtnLeft.Text = "Изменить";
            addCabinetBtnLeft.Text = "Добавить";

            editCabinetBtnRight.Text = "Изменить";
            addCabinetBtnRight.Text = "Добавить";

            editComplectBtnLeft.Text = "Изменить";
            addComplectBtnLeft.Text = "Добавить";

            editComplectBtnRight.Text = "Изменить";
            addComplectBtnRight.Text = "Добавить";

            editDeviceBtnLeft.Text = "Изменить";
            addDeviceBtnLeft.Text = "Добавить";

            editDeviceBtnRight.Text = "Изменить";
            addDeviceBtnRight.Text = "Добавить";
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshAll();
        }

        private void searchTBox_TextChanged(object sender, EventArgs e)
        {
            if (sender as TextBox == searchTBoxLeft)
            {
                searchTimerLeft.Stop();
                searchTimerLeft.Start();
            }
            else if (sender as TextBox == searchTBoxRight)
            {
                searchTimerRight.Stop();
                searchTimerRight.Start();
            }
        }

        private void SearchTimerTick(object sender, EventArgs e)
        {
            (sender as System.Windows.Forms.Timer).Stop();
            if (sender as System.Windows.Forms.Timer == searchTimerLeft)
                RefreshBuildingsLBoxLeft();
            else if (sender as System.Windows.Forms.Timer == searchTimerRight)
                RefreshBuildingsLBoxRight();
        }

        private void RefreshAll()
        {
            RefreshBuildings();
            RefreshDeviceTypes();
            RefreshDeviceProviders();
            RefreshHistoryDGW();
            RefreshFullListDGW();
        }

        private void RestoreSelectedItem(ListBox lBox, object? selectedItem)
        {
            if (selectedItem is not null)
                if (lBox.Items.Contains(selectedItem))
                    lBox.SelectedItem = selectedItem;
        }

        #region Блок работы со списком зданий		
        private void RefreshBuildings()
        {
            RefreshBuildingsLBoxLeft();
            RefreshBuildingsLBoxRight();
        }

        private async Task<List<Building>> LoadBuildings(string filter)
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            List<Building> buildings = await context.Buildings.OrderBy(b => b.Name)
                                                              .Include(b => b.Cabinets)
                                                              .Include(b => b.Cabinets)
                                                              .ThenInclude(c => c.Complects)
                                                              .ThenInclude(c => c.Devices)
                                                              .ThenInclude(d => d.DeviceName)
                                                              .AsSplitQuery()
                                                              .ToListAsync();
            if (filter.Length != 0)
                buildings = [.. buildings.Where(b => b.Cabinets.Any(c => c.Complects.Any(c => c.Devices.Any(d => d.ToString().ToLower().Contains(filter)))))];
            return buildings;
        }

        private async void RefreshBuildingsLBoxLeft()
        {
            object? selectedBuilding = buildingsLBoxLeft.SelectedItem;
            object? selectedCabinet = cabinetsLBoxLeft.SelectedItem;
            object? selectedComplect = complectsLBoxLeft.SelectedItem;
            object? selectedDevice = devicesLBoxLeft.SelectedItem;

            buildingsLBoxLeft.DataSource = await LoadBuildings(searchTBoxLeft.Text);

            if (buildingsLBoxLeft.Items.Count == 0)
                RefreshCabinetsLBoxLeft();

            RestoreSelectedItem(buildingsLBoxLeft, selectedBuilding);
            RestoreSelectedItem(cabinetsLBoxLeft, selectedCabinet);
            RestoreSelectedItem(complectsLBoxLeft, selectedComplect);
            RestoreSelectedItem(devicesLBoxLeft, selectedDevice);

            SwitchEditCabinetBtnLeft();
        }

        private async void RefreshBuildingsLBoxRight()
        {
            object? selectedBuilding = buildingsLBoxRight.SelectedItem;
            object? selectedCabinet = cabinetsLBoxRight.SelectedItem;
            object? selectedComplect = complectsLBoxRight.SelectedItem;
            object? selectedDevice = devicesLBoxRight.SelectedItem;

            buildingsLBoxRight.DataSource = await LoadBuildings(searchTBoxRight.Text);

            if (buildingsLBoxRight.Items.Count == 0)
                RefreshCabinetsLBoxRight();

            RestoreSelectedItem(buildingsLBoxRight, selectedBuilding);
            RestoreSelectedItem(cabinetsLBoxRight, selectedCabinet);
            RestoreSelectedItem(complectsLBoxRight, selectedComplect);
            RestoreSelectedItem(devicesLBoxRight, selectedDevice);

            SwitchEditCabinetBtnRight();
        }

        private void editBuildingBtn_Click(object sender, EventArgs e)
        {
            if (sender as Button == editBuildingBtnLeft)
            {
                Building? building = buildingsLBoxLeft.SelectedItem as Building;
                EditBuilding(building);
            }
            else if (sender as Button == editBuildingBtnRight)
            {
                Building? building = buildingsLBoxRight.SelectedItem as Building;
                EditBuilding(building);
            }
            else
                EditBuilding(null);
        }

        private void EditBuilding(Building? building)
        {
            EditBuildingForm form = new(building);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
                RefreshBuildings();
        }

        private void buildingsLBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (sender as ListBox == buildingsLBoxLeft)
                RefreshCabinetsLBoxLeft();
            else if (sender as ListBox == buildingsLBoxRight)
                RefreshCabinetsLBoxRight();
        }
        #endregion

        #region Блок работы со списком кабинетов
        private List<Cabinet> LoadCabinets(Building building, string filter)
        {
            List<Cabinet> cabinets = [.. building.Cabinets.OrderBy(c => c.Name)];
            if (filter.Length != 0)
                cabinets = cabinets.Where(c => c.Complects.Any(c => c.Devices.Any(d => d.ToString().ToLower().Contains(filter)))).ToList();
            return cabinets;
        }

        private void RefreshCabinetsLBoxLeft()
        {
            if (buildingsLBoxLeft.SelectedIndex == -1)
                cabinetsLBoxLeft.DataSource = new List<Cabinet>();
            else
                cabinetsLBoxLeft.DataSource = LoadCabinets(buildingsLBoxLeft.SelectedItem as Building, searchTBoxLeft.Text);

            if (cabinetsLBoxLeft.Items.Count == 0)
                RefreshComplectsLBoxLeft();

            SwitchEditComplectBtnLeft();
            SwitchMoveCabinetBtns();
        }

        private void RefreshCabinetsLBoxRight()
        {
            if (buildingsLBoxRight.SelectedIndex == -1)
                cabinetsLBoxRight.DataSource = new List<Cabinet>();
            else
                cabinetsLBoxRight.DataSource = LoadCabinets(buildingsLBoxRight.SelectedItem as Building, searchTBoxRight.Text);

            if (cabinetsLBoxRight.Items.Count == 0)
                RefreshComplectsLBoxRight();

            SwitchEditComplectBtnRight();
            SwitchMoveCabinetBtns();
        }

        private void SwitchEditCabinetBtnLeft()
        {
            if (buildingsLBoxLeft.SelectedIndex != -1)
            {
                editCabinetBtnLeft.Enabled = true;
                addCabinetBtnLeft.Enabled = true;
            }
            else
            {
                editCabinetBtnLeft.Enabled = false;
                addCabinetBtnLeft.Enabled = false;
            }
        }

        private void SwitchEditCabinetBtnRight()
        {
            if (buildingsLBoxRight.SelectedIndex != -1)
            {
                editCabinetBtnRight.Enabled = true;
                addCabinetBtnRight.Enabled = true;
            }
            else
            {
                editCabinetBtnRight.Enabled = false;
                addCabinetBtnRight.Enabled = false;
            }
        }

        private void SwitchMoveCabinetBtns()
        {
            SwitchMoveCabinetToLeftBtn();
            SwitchMoveCabinetToRightBtn();
        }

        private void SwitchMoveCabinetToRightBtn()
        {
            if (buildingsLBoxLeft.SelectedIndex == -1 || buildingsLBoxRight.SelectedIndex == -1)
            {
                moveCabinetToRightBtn.Enabled = false;
                return;
            }
            if (cabinetsLBoxLeft.SelectedIndex == -1)
            {
                moveCabinetToRightBtn.Enabled = false;
                return;
            }
            if (buildingsLBoxLeft.SelectedItem == buildingsLBoxRight.SelectedItem)
            {
                moveCabinetToRightBtn.Enabled = false;
                return;
            }
            moveCabinetToRightBtn.Enabled = true;
        }

        private void SwitchMoveCabinetToLeftBtn()
        {
            if (buildingsLBoxLeft.SelectedIndex == -1 || buildingsLBoxRight.SelectedIndex == -1)
            {
                moveCabinetToLeftBtn.Enabled = false;
                return;
            }
            if (cabinetsLBoxRight.SelectedIndex == -1)
            {
                moveCabinetToLeftBtn.Enabled = false;
                return;
            }
            if (buildingsLBoxLeft.SelectedItem == buildingsLBoxRight.SelectedItem)
            {
                moveCabinetToLeftBtn.Enabled = false;
                return;
            }
            moveCabinetToLeftBtn.Enabled = true;
        }

        private void editCabinetBtn_Click(object sender, EventArgs e)
        {
            if (sender as Button == editCabinetBtnLeft)
            {
                Cabinet? cabinet = cabinetsLBoxLeft.SelectedItem as Cabinet;
                Building? building = cabinet is null ? buildingsLBoxLeft.SelectedItem as Building : cabinet.Building;
                EditCabinet(cabinet, building);
            }
            else if (sender as Button == editCabinetBtnRight)
            {
                Cabinet? cabinet = cabinetsLBoxRight.SelectedItem as Cabinet;
                Building? building = cabinet is null ? buildingsLBoxRight.SelectedItem as Building : cabinet.Building;
                EditCabinet(cabinet, building);
            }
            else if (sender as Button == addCabinetBtnLeft)
            {
                Building? building = buildingsLBoxLeft.SelectedItem as Building;
                EditCabinet(null, building);
            }
            else
            {
                Building? building = buildingsLBoxRight.SelectedItem as Building;
                EditCabinet(null, building);
            }
        }

        private void EditCabinet(Cabinet? cabinet, Building building)
        {
            EditCabinetForm form = new(cabinet, building);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
                RefreshBuildings();
        }

        private void cabinetsLBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (sender as ListBox == cabinetsLBoxLeft)
                RefreshComplectsLBoxLeft();
            else if (sender as ListBox == cabinetsLBoxRight)
                RefreshComplectsLBoxRight();
        }

        private async void moveCabinetToRightBtn_Click(object sender, EventArgs e)
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            List<string> before = [];
            List<string> after = [];
            Cabinet? cabinet = cabinetsLBoxLeft.SelectedItem as Cabinet;
            List<Device> devicesInCabinet = cabinet?.Complects.SelectMany(c => c.Devices).ToList() ?? [];

            foreach (Device device in devicesInCabinet)
                before.Add(device.ToStringForHistory());

            cabinet.Building = buildingsLBoxRight.SelectedItem as Building;

            foreach (Device device in devicesInCabinet)
                after.Add(device.ToStringForHistory());

            for (int i = 0; i < devicesInCabinet.Count; i++)
                await context.History.AddAsync(new History()
                {
                    Before = before[i],
                    After = after[i]
                });

            bool success;
            try
            {
                await context.SaveChangesAsync();
                success = true;
            }
            catch
            {
                success = false;
            }
            if (success)
                RefreshBuildings();
        }

        private async void moveCabinetToLeftBtn_Click(object sender, EventArgs e)
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            List<string> before = [];
            List<string> after = [];
            Cabinet? cabinet = cabinetsLBoxRight.SelectedItem as Cabinet;
            List<Device> devicesInCabinet = cabinet?.Complects.SelectMany(c => c.Devices).ToList() ?? [];

            foreach (Device device in devicesInCabinet)
                before.Add(device.ToStringForHistory());

            cabinet.Building = buildingsLBoxLeft.SelectedItem as Building;

            foreach (Device device in devicesInCabinet)
                after.Add(device.ToStringForHistory());

            for (int i = 0; i < devicesInCabinet.Count; i++)
                await context.History.AddAsync(new History()
                {
                    Before = before[i],
                    After = after[i]
                });

            bool success;
            try
            {
                await context.SaveChangesAsync();
                success = true;
            }
            catch
            {
                success = false;
            }
            if (success)
                RefreshBuildings();
        }
        #endregion

        #region Блок работы со списком комплектов
        private List<Complect> LoadComplects(Cabinet cabinet, string filter)
        {
            List<Complect> complects = [.. cabinet.Complects.OrderBy(c => c.Name)];
            if (filter.Length != 0)
                complects = complects.Where(c => c.Devices.Any(d => d.ToString().ToLower().Contains(filter))).ToList();
            return complects;
        }

        private void RefreshComplectsLBoxLeft()
        {
            if (cabinetsLBoxLeft.SelectedIndex == -1)
                complectsLBoxLeft.DataSource = new List<Complect>();
            else
                complectsLBoxLeft.DataSource = LoadComplects(cabinetsLBoxLeft.SelectedItem as Cabinet, searchTBoxLeft.Text);

            if (complectsLBoxLeft.Items.Count == 0)
                RefreshDevicesLBoxLeft();

            SwitchEditDeviceBtnLeft();
            SwitchMoveComplectBtns();
        }

        private void RefreshComplectsLBoxRight()
        {
            if (cabinetsLBoxRight.SelectedIndex == -1)
                complectsLBoxRight.DataSource = new List<Complect>();
            else
                complectsLBoxRight.DataSource = LoadComplects(cabinetsLBoxRight.SelectedItem as Cabinet, searchTBoxRight.Text);

            if (complectsLBoxRight.Items.Count == 0)
                RefreshDevicesLBoxRight();

            SwitchEditDeviceBtnRight();
            SwitchMoveComplectBtns();
        }

        private void SwitchEditComplectBtnLeft()
        {
            if (cabinetsLBoxLeft.SelectedIndex != -1)
            {
                editComplectBtnLeft.Enabled = true;
                addComplectBtnLeft.Enabled = true;
            }
            else
            {
                editComplectBtnLeft.Enabled = false;
                addComplectBtnLeft.Enabled = false;
            }
        }

        private void SwitchEditComplectBtnRight()
        {
            if (cabinetsLBoxRight.SelectedIndex != -1)
            {
                editComplectBtnRight.Enabled = true;
                addComplectBtnRight.Enabled = true;
            }
            else
            {
                editComplectBtnRight.Enabled = false;
                addComplectBtnRight.Enabled = false;
            }
        }

        private void SwitchMoveComplectBtns()
        {
            SwitchMoveComplectToLeftBtn();
            SwitchMoveComplectToRightBtn();
        }

        private void SwitchMoveComplectToRightBtn()
        {
            if (cabinetsLBoxLeft.SelectedIndex == -1 || cabinetsLBoxRight.SelectedIndex == -1)
            {
                moveComplectToRightBtn.Enabled = false;
                return;
            }
            if (complectsLBoxLeft.SelectedIndex == -1)
            {
                moveComplectToRightBtn.Enabled = false;
                return;
            }
            if (cabinetsLBoxLeft.SelectedItem == cabinetsLBoxRight.SelectedItem)
            {
                moveComplectToRightBtn.Enabled = false;
                return;
            }
            moveComplectToRightBtn.Enabled = true;
        }

        private void SwitchMoveComplectToLeftBtn()
        {
            if (cabinetsLBoxLeft.SelectedIndex == -1 || cabinetsLBoxRight.SelectedIndex == -1)
            {
                moveComplectToLeftBtn.Enabled = false;
                return;
            }
            if (complectsLBoxRight.SelectedIndex == -1)
            {
                moveComplectToLeftBtn.Enabled = false;
                return;
            }
            if (cabinetsLBoxLeft.SelectedItem == cabinetsLBoxRight.SelectedItem)
            {
                moveComplectToLeftBtn.Enabled = false;
                return;
            }
            moveComplectToLeftBtn.Enabled = true;
        }

        private void editComplectBtn_Click(object sender, EventArgs e)
        {
            if (sender as Button == editComplectBtnLeft)
            {
                Complect? complect = complectsLBoxLeft.SelectedItem as Complect;
                Cabinet? cabinet = complect is null ? cabinetsLBoxLeft.SelectedItem as Cabinet : complect.Cabinet;
                EditComplect(complect, cabinet);
            }
            else if (sender as Button == editComplectBtnRight)
            {
                Complect? complect = complectsLBoxRight.SelectedItem as Complect;
                Cabinet? cabinet = complect is null ? cabinetsLBoxRight.SelectedItem as Cabinet : complect.Cabinet;
                EditComplect(complect, cabinet);
            }
            else if (sender as Button == addComplectBtnLeft)
            {
                Cabinet? cabinet = cabinetsLBoxLeft.SelectedItem as Cabinet;
                EditComplect(null, cabinet);
            }
            else
            {
                Cabinet? cabinet = cabinetsLBoxRight.SelectedItem as Cabinet;
                EditComplect(null, cabinet);
            }
        }

        private void EditComplect(Complect? complect, Cabinet cabinet)
        {
            EditComplectForm form = new(complect, cabinet);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
                RefreshBuildings();
        }

        private void complectsLBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (sender as ListBox == complectsLBoxLeft)
                RefreshDevicesLBoxLeft();
            else if (sender as ListBox == complectsLBoxRight)
                RefreshDevicesLBoxRight();
        }

        private async void moveComplectToRightBtn_Click(object sender, EventArgs e)
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            List<string> before = [];
            List<string> after = [];
            Complect? complect = complectsLBoxLeft.SelectedItem as Complect;
            List<Device> devicesInComplect = complect?.Devices.ToList() ?? [];

            foreach (Device device in devicesInComplect)
                before.Add(device.ToStringForHistory());

            complect.Cabinet = cabinetsLBoxRight.SelectedItem as Cabinet;

            foreach (Device device in devicesInComplect)
                after.Add(device.ToStringForHistory());

            for (int i = 0; i < devicesInComplect.Count; i++)
                await context.History.AddAsync(new History()
                {
                    Before = before[i],
                    After = after[i]
                });

            bool success;
            try
            {
                await context.SaveChangesAsync();
                success = true;
            }
            catch
            {
                success = false;
            }
            if (success)
                RefreshBuildings();
        }

        private async void moveComplectToLeftBtn_Click(object sender, EventArgs e)
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            List<string> before = [];
            List<string> after = [];
            Complect? complect = complectsLBoxRight.SelectedItem as Complect;
            List<Device> devicesInComplect = complect?.Devices.ToList() ?? [];

            foreach (Device device in devicesInComplect)
                before.Add(device.ToStringForHistory());

            complect.Cabinet = cabinetsLBoxLeft.SelectedItem as Cabinet;

            foreach (Device device in devicesInComplect)
                after.Add(device.ToStringForHistory());

            for (int i = 0; i < devicesInComplect.Count; i++)
                await context.History.AddAsync(new History()
                {
                    Before = before[i],
                    After = after[i]
                });

            bool success;
            try
            {
                await context.SaveChangesAsync();
                success = true;
            }
            catch
            {
                success = false;
            }
            if (success)
                RefreshBuildings();
        }
        #endregion

        #region Блок работы со списком единиц техники
        private List<Device> LoadDevices(Complect complect, string filter)
        {
            List<Device> devices = [.. complect.Devices.OrderBy(d => d.Serial)];
            if (filter.Length != 0)
                devices = [.. devices.Where(d => d.ToString().ToLower().Contains(filter))];
            return devices;
        }

        private void RefreshDevicesLBoxLeft()
        {
            if (complectsLBoxLeft.SelectedIndex == -1)
                devicesLBoxLeft.DataSource = new List<Device>();
            else
                devicesLBoxLeft.DataSource = LoadDevices(complectsLBoxLeft.SelectedItem as Complect, searchTBoxLeft.Text);

            SwitchMoveDeviceBtns();
        }

        private void RefreshDevicesLBoxRight()
        {
            if (complectsLBoxRight.SelectedIndex == -1)
                devicesLBoxRight.DataSource = new List<Device>();
            else
                devicesLBoxRight.DataSource = LoadDevices(complectsLBoxRight.SelectedItem as Complect, searchTBoxRight.Text);

            SwitchMoveDeviceBtns();
        }

        private void SwitchEditDeviceBtnLeft()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            if (complectsLBoxLeft.SelectedIndex != -1 && context.DeviceNames.Any() && context.DeviceProviders.Any())
            {
                editDeviceBtnLeft.Enabled = true;
                addDeviceBtnLeft.Enabled = true;
            }
            else
            {
                editDeviceBtnLeft.Enabled = false;
                addDeviceBtnLeft.Enabled = false;
            }
        }

        private void SwitchEditDeviceBtnRight()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            if (complectsLBoxRight.SelectedIndex != -1 && context.DeviceNames.Any() && context.DeviceProviders.Any())
            {
                editDeviceBtnRight.Enabled = true;
                addDeviceBtnRight.Enabled = true;
            }
            else
            {
                editDeviceBtnRight.Enabled = false;
                addDeviceBtnRight.Enabled = false;
            }
        }

        private void SwitchMoveDeviceBtns()
        {
            SwitchMoveDeviceToLeftBtn();
            SwitchMoveDeviceToRightBtn();
        }

        private void SwitchMoveDeviceToRightBtn()
        {
            if (complectsLBoxLeft.SelectedIndex == -1 || complectsLBoxRight.SelectedIndex == -1)
            {
                moveDeviceToRightBtn.Enabled = false;
                return;
            }
            if (devicesLBoxLeft.SelectedIndex == -1)
            {
                moveDeviceToRightBtn.Enabled = false;
                return;
            }
            if (complectsLBoxLeft.SelectedItem == complectsLBoxRight.SelectedItem)
            {
                moveDeviceToRightBtn.Enabled = false;
                return;
            }
            moveDeviceToRightBtn.Enabled = true;
        }

        private void SwitchMoveDeviceToLeftBtn()
        {
            if (complectsLBoxLeft.SelectedIndex == -1 || complectsLBoxRight.SelectedIndex == -1)
            {
                moveDeviceToLeftBtn.Enabled = false;
                return;
            }
            if (devicesLBoxRight.SelectedIndex == -1)
            {
                moveDeviceToLeftBtn.Enabled = false;
                return;
            }
            if (complectsLBoxLeft.SelectedItem == complectsLBoxRight.SelectedItem)
            {
                moveDeviceToLeftBtn.Enabled = false;
                return;
            }
            moveDeviceToLeftBtn.Enabled = true;
        }

        private void editDeviceBtn_Click(object sender, EventArgs e)
        {
            if (sender as Button == editDeviceBtnLeft)
            {
                Device? device = (Device?)devicesLBoxLeft.SelectedItem;
                Complect? complect = device is null ? complectsLBoxLeft.SelectedItem as Complect : device.Complect;
                EditDevice(device, complect);
            }
            else if (sender as Button == editDeviceBtnRight)
            {
                Device? device = (Device?)devicesLBoxRight.SelectedItem;
                Complect? complect = device is null ? complectsLBoxRight.SelectedItem as Complect : device.Complect;
                EditDevice(device, complect);
            }
            else if (sender as Button == addDeviceBtnLeft)
            {
                Complect? complect = complectsLBoxLeft.SelectedItem as Complect;
                EditDevice(null, complect);
            }
            else
            {
                Complect? complect = complectsLBoxRight.SelectedItem as Complect;
                EditDevice(null, complect);
            }
        }

        private void EditDevice(Device? device, Complect complect)
        {
            EditDeviceForm form = new(device, complect);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
                RefreshBuildings();
        }

        private void devicesLBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SwitchMoveDeviceBtns();
        }

        private async void moveDeviceToRightBtn_Click(object sender, EventArgs e)
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            Device? device = devicesLBoxLeft.SelectedItem as Device;
            string before = device.ToStringForHistory();
            device.Complect = complectsLBoxRight.SelectedItem as Complect;
            string after = device.ToStringForHistory();
            await context.History.AddAsync(new History()
            {
                Before = before,
                After = after
            });
            bool success;
            try
            {
                await context.SaveChangesAsync();
                success = true;
            }
            catch
            {
                success = false;
            }
            if (success)
                RefreshBuildings();
        }

        private async void moveDeviceToLeftBtn_Click(object sender, EventArgs e)
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            Device? device = devicesLBoxRight.SelectedItem as Device;
            string before = device.ToStringForHistory();
            device.Complect = complectsLBoxLeft.SelectedItem as Complect;
            string after = device.ToStringForHistory();
            await context.History.AddAsync(new History()
            {
                Before = before,
                After = after
            });
            bool success;
            try
            {
                await context.SaveChangesAsync();
                success = true;
            }
            catch
            {
                success = false;
            }
            if (success)
                RefreshBuildings();
        }
        #endregion

        #region Блок работы со списком типов техники
        private void deviceTypeEditBtn_Click(object sender, EventArgs e)
        {
            DeviceType? deviceType = deviceTypesLBox.SelectedItem as DeviceType;
            EditDeviceTypeForm form = new(deviceType);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
                RefreshDeviceTypes();
        }

        private void RefreshDeviceTypes()
        {
            object? selectedDeviceType = deviceTypesLBox.SelectedItem;
            object? selectedDeviceName = deviceNamesLBox.SelectedItem;

            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            deviceTypesLBox.DataSource = context.DeviceTypes.OrderBy(dt => dt.Name)
                                                            .Include(dt => dt.DeviceNames)
                                                            .ThenInclude(dn => dn.Devices)
                                                            .ToList();

            RefreshDeviceNames();

            RestoreSelectedItem(deviceTypesLBox, selectedDeviceType);
            RestoreSelectedItem(deviceNamesLBox, selectedDeviceName);

            SwitchEditDeviceNameBtn();
        }

        private void SwitchEditDeviceNameBtn()
        {
            if (deviceTypesLBox.SelectedIndex != -1)
                editDeviceNameBtn.Enabled = true;
            else
                editDeviceNameBtn.Enabled = false;
        }

        private void typesLBox_SelectedValueChanged(object sender, EventArgs e)
        {
            RefreshDeviceNames();
        }
        #endregion

        #region Блок работы со списком названий техники
        private void deviceNameEditBtn_Click(object sender, EventArgs e)
        {
            DeviceName? deviceName = deviceNamesLBox.SelectedItem as DeviceName;
            DeviceType? deviceType = deviceName is null ? deviceTypesLBox.SelectedItem as DeviceType : deviceName.DeviceType;
            EditDeviceNameForm form = new(deviceName, deviceType);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
                RefreshDeviceTypes();
        }

        private void RefreshDeviceNames()
        {
            if (deviceTypesLBox.SelectedIndex == -1)
                deviceNamesLBox.DataSource = new List<DeviceName>();
            else
                deviceNamesLBox.DataSource = (deviceTypesLBox.SelectedItem as DeviceType).DeviceNames.ToList();

            SwitchEditDeviceBtnLeft();
            SwitchEditDeviceBtnRight();
        }
        #endregion

        #region Блок работы со списком предоставителей техники
        private void deviceProviderEditBtn_Click(object sender, EventArgs e)
        {
            DeviceProvider? deviceProvider = deviceProvidersLBox.SelectedItem as DeviceProvider;
            EditDeviceProviderForm form = new(deviceProvider);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
                RefreshDeviceProviders();
        }

        private void RefreshDeviceProviders()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            object? selectedItem = deviceProvidersLBox.SelectedItem;
            deviceProvidersLBox.DataSource = context.DeviceProviders.OrderBy(dp => dp.Name)
                                                                    .Include(dp => dp.Devices)
                                                                    .ToList();

            RestoreSelectedItem(deviceProvidersLBox, selectedItem);

            SwitchEditDeviceBtnLeft();
            SwitchEditDeviceBtnRight();
        }
        #endregion

        #region Блок работы с историей перемещений
        private void InitializeHistoryDGW()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            historyDGW.DataSource = context.History.OrderByDescending(h => h.ChangedAt).ToList();
            historyDGW.Columns[0].Visible = false;
            historyDGW.Columns[1].HeaderText = "Было";
            historyDGW.Columns[2].HeaderText = "Стало";
            historyDGW.Columns[3].HeaderText = "Дата и время изменения";
        }

        private void RefreshHistoryDGW()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            if (historySearchTBox.Text.Length == 0)
                historyDGW.DataSource = context.History.OrderByDescending(h => h.ChangedAt).ToList();
            else
            {
                string text = historySearchTBox.Text.ToLower();
                historyDGW.DataSource = context.History.Where(h => h.Before.ToLower().Contains(text) || h.After.ToLower().Contains(text))
                    .OrderByDescending(h => h.ChangedAt)
                    .ToList();
            }
        }

        private void historySearchTBox_TextChanged(object sender, EventArgs e)
        {
            RefreshHistoryDGW();
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            RefreshHistoryDGW();
        }
        #endregion

        #region Блок работы с полным списком техники
        private void InitializeFullListDGW()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            fullListDGW.DataSource = context.Devices.Select(d => new
            {
                d.Complect.Cabinet.Building,
                d.Complect.Cabinet,
                d.Complect,
                d.DeviceName.DeviceType,
                d.DeviceName,
                d.DeviceProvider,
                d.Serial,
                d.Inventory,
                d.Notes
            }).OrderBy(d => d.Building.Name)
            .ThenBy(d => d.Cabinet.Name)
            .ThenBy(d => d.Complect.Name)
            .ThenBy(d => d.DeviceName.Name)
            .ToList();

            fullListDGW.Columns[0].HeaderText = "Здание";
            fullListDGW.Columns[1].HeaderText = "Кабинет";
            fullListDGW.Columns[2].HeaderText = "Комплект";
            fullListDGW.Columns[3].HeaderText = "Тип";
            fullListDGW.Columns[4].HeaderText = "Название";
            fullListDGW.Columns[5].HeaderText = "Получено от";
            fullListDGW.Columns[6].HeaderText = "Серийный номер";
            fullListDGW.Columns[7].HeaderText = "Инвентарный номер";
            fullListDGW.Columns[8].HeaderText = "Примечание";

            fullListNumberOfDevicesLabel.Text = $"Всего единиц техники: {fullListDGW.RowCount}";
        }

        private void RefreshFullListDGW()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            var list = context.Devices.Select(d => new
            {
                d.Complect.Cabinet.Building,
                d.Complect.Cabinet,
                d.Complect,
                d.DeviceName.DeviceType,
                d.DeviceName,
                d.DeviceProvider,
                d.Serial,
                d.Inventory,
                d.Notes
            }).OrderBy(d => d.Building.Name)
            .ThenBy(d => d.Cabinet.Name)
            .ThenBy(d => d.Complect.Name)
            .ThenBy(d => d.DeviceName.Name)
            .ToList();

            if (fullListBuildingTBox.Text.Length != 0)
                list = list.Where(d => d.Building.Name.ToLower().Contains(fullListBuildingTBox.Text.ToLower())).ToList();
            if (fullListCabinetTBox.Text.Length != 0)
                list = list.Where(d => d.Cabinet.Name.ToLower().Contains(fullListCabinetTBox.Text.ToLower())).ToList();
            if (fullListComplectTBox.Text.Length != 0)
                list = list.Where(d => d.Complect.Name.ToLower().Contains(fullListComplectTBox.Text.ToLower())).ToList();
            if (fullListDeviceTypeTBox.Text.Length != 0)
                list = list.Where(d => d.DeviceType.Name.ToLower().Contains(fullListDeviceTypeTBox.Text.ToLower())).ToList();
            if (fullListDeviceNameTBox.Text.Length != 0)
                list = list.Where(d => d.DeviceName.Name.ToLower().Contains(fullListDeviceNameTBox.Text.ToLower())).ToList();
            if (fullListDeviceProviderTBox.Text.Length != 0)
                list = list.Where(d => d.DeviceProvider.Name.ToLower().Contains(fullListDeviceProviderTBox.Text.ToLower())).ToList();
            if (fullListSerialTBox.Text.Length != 0)
                list = list.Where(d => d.Serial.ToLower().Contains(fullListSerialTBox.Text.ToLower())).ToList();
            if (fullListInventoryTBox.Text.Length != 0)
                list = list.Where(d => d.Inventory.ToLower().Contains(fullListInventoryTBox.Text.ToLower())).ToList();
            if (fullListNotesTBox.Text.Length != 0)
                list = list.Where(d => d.Notes.ToLower().Contains(fullListNotesTBox.Text.ToLower())).ToList();

            fullListDGW.DataSource = list;
            fullListNumberOfDevicesLabel.Text = $"Всего единиц техники: {fullListDGW.RowCount}";
        }

        private void fullListSearchTBox_TextChanged(object sender, EventArgs e)
        {
            RefreshFullListDGW();
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            RefreshFullListDGW();
        }
        #endregion

        private void передатьВExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            path = Path.Combine(path, "Hardware");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, $"{DateTime.Now.ToShortDateString().Replace('.', '-')}.xlsx");
            try
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
            catch
            {
                MessageBox.Show($"Файл {path} занят");
                return;
            }

            var devices = context.Devices.Select(d => new
            {
                d.Complect.Cabinet.Building,
                d.Complect.Cabinet,
                d.Complect,
                d.DeviceName.DeviceType,
                d.DeviceName,
                d.DeviceProvider,
                d.Serial,
                d.Inventory,
                d.Notes
            }).OrderBy(d => d.Building.Name)
            .ThenBy(d => d.Cabinet.Name)
            .ThenBy(d => d.Complect.Name)
            .ThenBy(d => d.DeviceName.Name)
            .ToList();

            using ExcelPackage package = new(path);
            using ExcelWorkbook workbook = package.Workbook;
            using ExcelWorksheet worksheet = workbook.Worksheets.Add("Техника");

            worksheet.Cells.Style.Font.Name = "Courier New";
            worksheet.Cells.Style.Font.Size = 12;
            worksheet.Cells.Style.WrapText = true;
            worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

            worksheet.Cells[1, 1].Value = "Здание";
            worksheet.Cells[1, 2].Value = "Кабинет";
            worksheet.Cells[1, 3].Value = "Комплект";
            worksheet.Cells[1, 4].Value = "Тип";
            worksheet.Cells[1, 5].Value = "Название";
            worksheet.Cells[1, 6].Value = "Получено от";
            worksheet.Cells[1, 7].Value = "С/н";
            worksheet.Cells[1, 8].Value = "И/н";
            worksheet.Cells[1, 9].Value = "Примечание";

            int row = 2;

            foreach (var device in devices)
            {
                worksheet.Cells[row, 1].Value = device.Complect.Cabinet.Building;
                worksheet.Cells[row, 2].Value = device.Complect.Cabinet;
                worksheet.Cells[row, 3].Value = device.Complect;
                worksheet.Cells[row, 4].Value = device.DeviceName.DeviceType;
                worksheet.Cells[row, 5].Value = device.DeviceName;
                worksheet.Cells[row, 6].Value = device.DeviceProvider;
                worksheet.Cells[row, 7].Value = device.Serial;
                worksheet.Cells[row, 8].Value = device.Inventory;
                worksheet.Cells[row, 9].Value = device.Notes;

                row++;
            }

            ExcelRange range = worksheet.Cells[1, 1, row - 1, 9];
            OfficeOpenXml.Table.ExcelTable table = worksheet.Tables.Add(range, "Техника");
            table.TableStyle = OfficeOpenXml.Table.TableStyles.Light16;
            range.AutoFitColumns();

            try
            {
                package.Save();
                MessageBox.Show($"Файл сохранён по пути {path}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private async void выгрузитьQRкодыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            выгрузитьИнвентарныеКарточкиToolStripMenuItem.Enabled = false;
            выгрузитьQRкодыToolStripMenuItem.Enabled = false;
            progressBar1.Visible = true;
            progressBar1.Value = 0;

            try
            {
                QrManager qrManager = new();
                Progress<int> progress = new(percent => progressBar1.Value = percent);
                await Task.Run(() => qrManager.CreateQrImages(progress));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            finally
            {
                выгрузитьИнвентарныеКарточкиToolStripMenuItem.Enabled = true;
                выгрузитьQRкодыToolStripMenuItem.Enabled = true;
                progressBar1.Visible = false;
            }
        }

        private async void выгрузитьИнвентарныеКарточкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            выгрузитьИнвентарныеКарточкиToolStripMenuItem.Enabled = false;
            выгрузитьQRкодыToolStripMenuItem.Enabled = false;
            progressBar1.Visible = true;
            progressBar1.Value = 0;

            try
            {
                Progress<int> progress = new(percent => progressBar1.Value = percent);
                await Task.Run(() => UploadInventoryCards(progress));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            finally
            {
                выгрузитьИнвентарныеКарточкиToolStripMenuItem.Enabled = true;
                выгрузитьQRкодыToolStripMenuItem.Enabled = true;
                progressBar1.Visible = false;
            }
        }

        private async void UploadInventoryCards(IProgress<int> progress)
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            path = Path.Combine(path, "Hardware");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            int files = 1;
            string fileName = Path.Combine(path, $"Инвентарные карточки {DateTime.Now.ToShortDateString().Replace('.', '-')} ({files++}).xlsx");
            try
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
            }
            catch
            {
                MessageBox.Show($"Файл {fileName} занят");
                return;
            }

            var devices = context.Devices.Select(d => new
            {
                d.Complect.Cabinet.Building,
                d.Complect.Cabinet,
                d.Complect,
                d.DeviceName.DeviceType,
                d.DeviceName,
                d.Serial,
                d.Inventory
            }).OrderBy(d => d.Building.Name)
            .ThenBy(d => d.Cabinet.Name)
            .ThenBy(d => d.Complect.Name)
            .ThenBy(d => d.DeviceName.Name)
            .ToList();

            ExcelPackage package = new(fileName);
            ExcelWorkbook workbook = package.Workbook;

            List<string> savedCabinets = [];
            int iterator = 0;
            foreach (var device in devices)
            {
                double percent = (double)++iterator / (double)devices.Count * 100;
                progress.Report((int)percent);

                string worksheetName = $"{device.Complect.Cabinet.Building.Name} {device.Complect.Cabinet.Name}";
                worksheetName = NonAlphabetNoNumberNoSpace().Replace(worksheetName, "_");
                if (savedCabinets.Any(item => item == worksheetName))
                    continue;

                var devicesInCabinet = (from d in devices where d.Building.Id == device.Building.Id && d.Cabinet.Id == device.Cabinet.Id select d).ToList();

                ExcelWorksheet worksheet = workbook.Worksheets.Add(worksheetName);

                worksheet.PrinterSettings.FitToPage = true;
                worksheet.PrinterSettings.FitToWidth = 1;
                worksheet.PrinterSettings.FitToHeight = 1;
                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
                worksheet.PrinterSettings.PaperSize = ePaperSize.A5;
                worksheet.PrinterSettings.HorizontalCentered = true;

                worksheet.Cells.Style.Font.Name = "Courier New";
                worksheet.Cells.Style.Font.Size = 8;
                worksheet.Cells.Style.WrapText = true;
                worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                worksheet.Columns[1].Width = 6.17;
                worksheet.Columns[2].Width = 18.67;
                worksheet.Columns[3].Width = 12.67;
                worksheet.Columns[4].Width = 12.17;
                worksheet.Columns[5].Width = 4.5;
                worksheet.Columns[6].Width = 7.67;
                worksheet.Columns[7].Width = 28.33;
                worksheet.Columns[8].Width = 2.67;
                worksheet.Columns[9].Width = 12.67;
                worksheet.Columns[10].Width = 7.67;
                worksheet.Columns[11].Width = 8.67;

                #region Шапка
                worksheet.Cells[1, 1, 1, 11].Merge = true;
                worksheet.Cells[1, 1].Value = "Инвентарный список нефинансовых активов";
                worksheet.Cells[1, 1].Style.Font.Size = 10;
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells[2, 1].Value = $"{device.Complect.Cabinet.Building.Name} {device.Complect.Cabinet.Name}";

                worksheet.Cells[4, 1].Value = "Учреждение";
                worksheet.Cells[4, 3, 4, 8].Merge = true;
                worksheet.Cells[4, 3].Value = "ЧЕТВЕРТЫЙ КАССАЦИОННЫЙ СУД ОБЩЕЙ ЮРИСДИКЦИИ";
                worksheet.Cells[4, 3, 4, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                worksheet.Cells[5, 4, 5, 8].Merge = true;
                worksheet.Cells[5, 1].Value = "Структурное подразделение";
                worksheet.Cells[5, 4, 5, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                worksheet.Cells[6, 4, 6, 8].Merge = true;
                worksheet.Cells[6, 1].Value = "Ответственное(-ые) лицо(-а)";
                worksheet.Cells[6, 4, 6, 8].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                worksheet.Cells[2, 10, 2, 11].Merge = true;
                worksheet.Cells[2, 10].Value = "КОДЫ";
                worksheet.Cells[2, 10, 2, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells[3, 10, 3, 11].Merge = true;
                worksheet.Cells[3, 10].Value = "0504034";
                worksheet.Cells[3, 10, 3, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells[4, 10, 4, 11].Merge = true;
                worksheet.Cells[4, 10].Value = "32717350";
                worksheet.Cells[4, 10, 4, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells[5, 10].Value = DateTime.Now.ToShortDateString();
                worksheet.Cells[5, 10, 5, 11].Merge = true;
                worksheet.Cells[5, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells[6, 10, 6, 11].Merge = true;

                worksheet.Cells[3, 9].Value = "Форма по ОКУД";
                worksheet.Cells[3, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                worksheet.Cells[4, 9].Value = "по ОКПО";
                worksheet.Cells[4, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                worksheet.Cells[5, 9].Value = "Дата";
                worksheet.Cells[5, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                for (int i = 2; i <= 6; i++)
                    for (int j = 10; j <= 11; j++)
                    {
                        var cell = worksheet.Cells[i, j];
                        cell.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        cell.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        cell.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        cell.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    }
                worksheet.Cells[3, 10, 6, 11].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);

                worksheet.Rows[1, 6].Style.WrapText = false;
                #endregion

                #region Шапка таблицы
                worksheet.Rows[8, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells[8, 1].Value = "Номер\nп/п";
                worksheet.Cells[8, 1, 10, 1].Merge = true;

                worksheet.Cells[8, 2].Value = "Инвентарная\nкарточка";
                worksheet.Cells[8, 2, 9, 3].Merge = true;

                worksheet.Cells[10, 2].Value = "номер";
                worksheet.Cells[10, 3].Value = "дата";

                worksheet.Cells[8, 4].Value = "Заводской\nномер";
                worksheet.Cells[8, 4, 10, 4].Merge = true;

                worksheet.Cells[8, 5].Value = "Инвентарный\nномер";
                worksheet.Cells[8, 5, 10, 6].Merge = true;

                worksheet.Cells[8, 7].Value = "Полное наименование объекта";
                worksheet.Cells[8, 7, 10, 8].Merge = true;

                worksheet.Cells[8, 9].Value = "Выбытие (перемещение)";
                worksheet.Cells[8, 9, 8, 11].Merge = true;

                worksheet.Cells[9, 9].Value = "документ";
                worksheet.Cells[9, 9, 9, 10].Merge = true;

                worksheet.Cells[10, 9].Value = "дата";
                worksheet.Cells[10, 10].Value = "номер";

                worksheet.Cells[9, 11].Value = "причина\nвыбытия";
                worksheet.Cells[9, 11, 10, 11].Merge = true;

                worksheet.Cells[11, 1].Value = "1а";
                worksheet.Cells[11, 2].Value = "1";
                worksheet.Cells[11, 3].Value = "2";
                worksheet.Cells[11, 4].Value = "3";

                worksheet.Cells[11, 5, 11, 6].Merge = true;
                worksheet.Cells[11, 5].Value = "4";

                worksheet.Cells[11, 7, 11, 8].Merge = true;
                worksheet.Cells[11, 7].Value = "5";

                worksheet.Cells[11, 9].Value = "6";
                worksheet.Cells[11, 10].Value = "7";
                worksheet.Cells[11, 11].Value = "8";
                #endregion

                #region Тело таблицы
                int row = 12;
                int count = 1;
                foreach (var d in devicesInCabinet)
                {
                    worksheet.Cells[row, 5, row, 6].Merge = true;
                    worksheet.Cells[row, 7, row, 8].Merge = true;

                    worksheet.Cells[row, 1].Value = count++;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    if (d.Inventory != null && d.Inventory.StartsWith("10134"))
                        worksheet.Cells[row, 2].Value = d.Inventory[^5..];

                    worksheet.Cells[row, 4].Value = d.Serial;
                    if (d.Inventory != null)
                        worksheet.Cells[row, 5].Value = d.Inventory;

                    worksheet.Cells[row, 7].Value = $"{d.DeviceType.Name} {d.DeviceName}";

                    row++;
                }
                row--;

                for (int i = 8; i <= row; i++)
                {
                    for (int j = 1; j <= 11; j++)
                    {
                        ExcelRange cell = worksheet.Cells[i, j];
                        cell.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        cell.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        cell.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        cell.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    }
                }
                #endregion

                #region Днище таблицы
                row += 2;

                worksheet.Rows[row, row + 5].Style.WrapText = false;

                worksheet.Cells[row, 1].Value = "Исполнитель";

                worksheet.Cells[row, 3, row, 5].Merge = true;
                worksheet.Cells[row, 3, row, 5].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                worksheet.Cells[row, 7].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                worksheet.Cells[row, 9, row, 11].Merge = true;
                worksheet.Cells[row, 9, row, 11].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                row++;

                worksheet.Cells[row, 3, row, 5].Merge = true;
                worksheet.Cells[row, 3].Value = "(должность)";
                worksheet.Cells[row, 3].Style.Font.Size = 7;
                worksheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells[row, 7].Value = "(подпись)";
                worksheet.Cells[row, 7].Style.Font.Size = 7;
                worksheet.Cells[row, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells[row, 9, row, 11].Merge = true;
                worksheet.Cells[row, 9].Value = "(расшифровка подписи)";
                worksheet.Cells[row, 9].Style.Font.Size = 7;
                worksheet.Cells[row, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                row++;

                worksheet.Cells[row, 7].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                worksheet.Cells[row, 9, row, 11].Merge = true;
                worksheet.Cells[row, 9, row, 11].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                row++;

                worksheet.Cells[row, 7].Value = "(номер контактного телефона)";
                worksheet.Cells[row, 7].Style.Font.Size = 7;
                worksheet.Cells[row, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells[row, 9, row, 11].Merge = true;
                worksheet.Cells[row, 9].Value = "(электронный адрес)";
                worksheet.Cells[row, 9].Style.Font.Size = 7;
                worksheet.Cells[row, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                row++;

                worksheet.Cells[row, 1].Value = "\"_______\"____________________ 20___ г.";
                #endregion

                if (workbook.Worksheets.Count > 24)
                {
                    try
                    {
                        package.Save();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }

                    workbook.Dispose();
                    package.Dispose();

                    fileName = Path.Combine(path, $"Инвентарные карточки {DateTime.Now.ToShortDateString().Replace('.', '-')} ({files++}).xlsx");
                    try
                    {
                        if (File.Exists(fileName))
                            File.Delete(fileName);
                    }
                    catch
                    {
                        MessageBox.Show($"Файл {fileName} занят");
                        return;
                    }

                    package = new(fileName);
                    workbook = package.Workbook;
                }

                savedCabinets.Add(worksheetName);
            }

            try
            {
                package.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            finally
            {
                MessageBox.Show("Выгрузка инвентарных карточек завершена");
            }
        }

        [GeneratedRegex(@"[^\p{L}\p{N}\s]")]
        private static partial Regex NonAlphabetNoNumberNoSpace();
    }
}
