using Hardware.Forms;
using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware
{
	public partial class MainForm : Form
	{
		private readonly ApplicationContext context;
		public MainForm()
		{
			InitializeComponent();
			EnterPasswordForm form = new();
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
			{
				context = ApplicationContext.Instanse();
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
		}

		private void InitializeButtons()
		{
			editBuildingBtnLeft.Text = "Добавить\nизменить";
			editBuildingBtnRight.Text = "Добавить\nизменить";
			editCabinetBtnLeft.Text = "Добавить\nизменить";
			editCabinetBtnRight.Text = "Добавить\nизменить";
			editComplectBtnLeft.Text = "Добавить\nизменить";
			editComplectBtnRight.Text = "Добавить\nизменить";
			editDeviceBtnLeft.Text = "Добавить\nизменить";
			editDeviceBtnRight.Text = "Добавить\nизменить";
		}

		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RefreshAll();
		}

		private void searchTBox_TextChanged(object sender, EventArgs e)
		{
			if (sender as TextBox == searchTBoxLeft)
				RefreshBuildingsLBoxLeft();
			else if (sender as TextBox == searchTBoxRight)
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

		// Блок работы со списком зданий
		private void RefreshBuildings()
		{
			RefreshBuildingsLBoxLeft();
			RefreshBuildingsLBoxRight();
		}

		private void RefreshBuildingsLBoxLeft()
		{
			var selectedItem = buildingsLBoxLeft.SelectedItem;
			var selectedCabinet = cabinetsLBoxLeft.SelectedItem;
			var selectedComplect = complectsLBoxLeft.SelectedItem;
			var selectedDevice = devicesLBoxLeft.SelectedItem;
			if (searchTBoxLeft.Text.Length == 0)
				buildingsLBoxLeft.DataSource = context.Buildings.OrderBy(b => b.Name).ToList();
			else
				buildingsLBoxLeft.DataSource = SearchBuildingByDevice(searchTBoxLeft.Text);
			RestoreSelectedItem(buildingsLBoxLeft, selectedItem);
			RestoreSelectedItem(cabinetsLBoxLeft, selectedCabinet);
			RestoreSelectedItem(complectsLBoxLeft, selectedComplect);
			RestoreSelectedItem(devicesLBoxLeft, selectedDevice);
			SwitchEditCabinetBtnLeft();
		}

		private void RefreshBuildingsLBoxRight()
		{
			var selectedItem = buildingsLBoxRight.SelectedItem;
			var selectedCabinet = cabinetsLBoxRight.SelectedItem;
			var selectedComplect = complectsLBoxRight.SelectedItem;
			var selectedDevice = devicesLBoxRight.SelectedItem;
			if (searchTBoxRight.Text.Length == 0)
				buildingsLBoxRight.DataSource = context.Buildings.OrderBy(b => b.Name).ToList();
			else
				buildingsLBoxRight.DataSource = SearchBuildingByDevice(searchTBoxRight.Text);
			RestoreSelectedItem(buildingsLBoxRight, selectedItem);
			RestoreSelectedItem(cabinetsLBoxRight, selectedCabinet);
			RestoreSelectedItem(complectsLBoxRight, selectedComplect);
			RestoreSelectedItem(devicesLBoxRight, selectedDevice);
			SwitchEditCabinetBtnRight();
		}

		private List<Building> SearchBuildingByDevice(string text)
		{
			text = text.ToLower();
			return context.Devices.ToList()
				.Where(d => d.ToString().ToLower().Contains(text))
				.GroupBy(d => d.Complect.Cabinet.Building)
				.Select(d => d.First().Complect.Cabinet.Building)
				.OrderBy(b => b.Name)
				.ToList();
		}

		private void editBuildingBtn_Click(object sender, EventArgs e)
		{
			if (sender as Button == editBuildingBtnLeft)
			{
				var building = buildingsLBoxLeft.SelectedItem as Building;
				EditBuilding(building);
			}
			else if (sender as Button == editBuildingBtnRight)
			{
				var building = buildingsLBoxRight.SelectedItem as Building;
				EditBuilding(building);
			}
		}

		private void EditBuilding(Building? building)
		{
			var form = new EditBuildingForm(building);
			var result = form.ShowDialog();
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

		// Блок работы со списком кабинетов
		private void RefreshCabinetsLBoxLeft()
		{
			var selectedItem = cabinetsLBoxLeft.SelectedItem;
			var selectedComplect = complectsLBoxLeft.SelectedItem;
			var selectedDevice = devicesLBoxLeft.SelectedItem;
			if (searchTBoxLeft.Text.Length == 0)
				cabinetsLBoxLeft.DataSource = context.Cabinets.Where(c => c.Building == buildingsLBoxLeft.SelectedItem)
					.OrderBy(c => c.Name)
					.ToList();
			else
				cabinetsLBoxLeft.DataSource = SearchCabinetByDevice(searchTBoxLeft.Text, buildingsLBoxLeft.SelectedItem as Building);
			RestoreSelectedItem(cabinetsLBoxLeft, selectedItem);
			RestoreSelectedItem(complectsLBoxLeft, selectedComplect);
			RestoreSelectedItem(devicesLBoxLeft, selectedDevice);
			SwitchEditComplectBtnLeft();
			SwitchMoveCabinetBtns();
		}

		private void RefreshCabinetsLBoxRight()
		{
			var selectedItem = cabinetsLBoxRight.SelectedItem;
			var selectedComplect = complectsLBoxRight.SelectedItem;
			var selectedDevice = devicesLBoxLeft.SelectedItem;
			if (searchTBoxRight.Text.Length == 0)
				cabinetsLBoxRight.DataSource = context.Cabinets.Where(c => c.Building == buildingsLBoxRight.SelectedItem)
					.OrderBy(c => c.Name)
					.ToList();
			else
				cabinetsLBoxRight.DataSource = SearchCabinetByDevice(searchTBoxRight.Text, buildingsLBoxRight.SelectedItem as Building);
			RestoreSelectedItem(cabinetsLBoxRight, selectedItem);
			RestoreSelectedItem(complectsLBoxRight, selectedComplect);
			RestoreSelectedItem(devicesLBoxRight, selectedDevice);
			SwitchEditComplectBtnRight();
			SwitchMoveCabinetBtns();
		}

		private List<Cabinet> SearchCabinetByDevice(string text, Building? building)
		{
			text = text.ToLower();
			return context.Devices.ToList()
				.Where(d => d.ToString().ToLower().Contains(text))
				.GroupBy(d => d.Complect.Cabinet)
				.Select(d => d.First().Complect.Cabinet)
				.ToList()
				.Where(c => c.Building == building)
				.OrderBy(c => c.Name)
				.ToList();
		}

		private void SwitchEditCabinetBtnLeft()
		{
			if (buildingsLBoxLeft.SelectedIndex != -1)
				editCabinetBtnLeft.Enabled = true;
			else
				editCabinetBtnLeft.Enabled = false;
		}

		private void SwitchEditCabinetBtnRight()
		{
			if (buildingsLBoxRight.SelectedIndex != -1)
				editCabinetBtnRight.Enabled = true;
			else
				editCabinetBtnRight.Enabled = false;
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
				var cabinet = cabinetsLBoxLeft.SelectedItem as Cabinet;
				var building = cabinet is null ? buildingsLBoxLeft.SelectedItem as Building : cabinet.Building;
				EditCabinet(cabinet, building);
			}
			else if (sender as Button == editCabinetBtnRight)
			{
				var cabinet = cabinetsLBoxRight.SelectedItem as Cabinet;
				var building = cabinet is null ? buildingsLBoxRight.SelectedItem as Building : cabinet.Building;
				EditCabinet(cabinet, building);
			}
		}

		private void EditCabinet(Cabinet? cabinet, Building building)
		{
			var form = new EditCabinetForm(cabinet, building);
			var result = form.ShowDialog();
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
			var before = new List<string>();
			var after = new List<string>();
			var cabinet = cabinetsLBoxLeft.SelectedItem as Cabinet;
			var devicesInCabinet = context.Devices.Include(d => d.Complect.Cabinet.Building)
				.Where(d => d.Complect.Cabinet == cabinet)
				.ToList();

			foreach (var device in devicesInCabinet)
				before.Add(device.ToStringForHistory());

			cabinet.Building = buildingsLBoxRight.SelectedItem as Building;

			foreach (var device in devicesInCabinet)
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
			var before = new List<string>();
			var after = new List<string>();
			var cabinet = cabinetsLBoxRight.SelectedItem as Cabinet;
			var devicesInCabinet = context.Devices.Include(d => d.Complect.Cabinet.Building)
				.Where(d => d.Complect.Cabinet == cabinet)
				.ToList();

			foreach (var device in devicesInCabinet)
				before.Add(device.ToStringForHistory());

			cabinet.Building = buildingsLBoxLeft.SelectedItem as Building;

			foreach (var device in devicesInCabinet)
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

		// Блок работы со списком комплектов
		private void RefreshComplectsLBoxLeft()
		{
			var selectedItem = complectsLBoxLeft.SelectedItem;
			var selectedDevice = devicesLBoxLeft.SelectedItem;
			if (searchTBoxLeft.Text.Length == 0)
				complectsLBoxLeft.DataSource = context.Complects.Where(c => c.Cabinet == cabinetsLBoxLeft.SelectedItem)
					.OrderBy(c => c.Name)
					.ToList();
			else
				complectsLBoxLeft.DataSource = SearchComplectByDevice(searchTBoxLeft.Text, cabinetsLBoxLeft.SelectedItem as Cabinet);

			if (complectsLBoxLeft.Items.Count == 0)
				devicesLBoxLeft.DataSource = null;

			RestoreSelectedItem(complectsLBoxLeft, selectedItem);
			RestoreSelectedItem(devicesLBoxLeft, selectedDevice);
			SwitchEditDeviceBtnLeft();
			SwitchMoveComplectBtns();
		}

		private void RefreshComplectsLBoxRight()
		{
			var selectedItem = complectsLBoxRight.SelectedItem;
			var selectedDevice = devicesLBoxRight.SelectedItem;
			if (searchTBoxRight.Text.Length == 0)
				complectsLBoxRight.DataSource = context.Complects.Where(c => c.Cabinet == cabinetsLBoxRight.SelectedItem)
					.OrderBy(c => c.Name)
					.ToList();
			else
				complectsLBoxRight.DataSource = SearchComplectByDevice(searchTBoxRight.Text, cabinetsLBoxRight.SelectedItem as Cabinet);

			if (complectsLBoxRight.Items.Count == 0)
				devicesLBoxRight.DataSource = null;

			RestoreSelectedItem(complectsLBoxRight, selectedItem);
			RestoreSelectedItem(devicesLBoxRight, selectedDevice);
			SwitchEditDeviceBtnRight();
			SwitchMoveComplectBtns();
		}

		private List<Complect> SearchComplectByDevice(string text, Cabinet? cabinet)
		{
			text = text.ToLower();
			return context.Devices.ToList()
				.Where(d => d.ToString().ToLower().Contains(text))
				.GroupBy(d => d.Complect)
				.Select(d => d.First().Complect)
				.ToList()
				.Where(c => c.Cabinet == cabinet)
				.OrderBy(c => c.Name)
				.ToList();
		}

		private void SwitchEditComplectBtnLeft()
		{
			if (cabinetsLBoxLeft.SelectedIndex != -1)
				editComplectBtnLeft.Enabled = true;
			else
				editComplectBtnLeft.Enabled = false;
		}

		private void SwitchEditComplectBtnRight()
		{
			if (cabinetsLBoxRight.SelectedIndex != -1)
				editComplectBtnRight.Enabled = true;
			else
				editComplectBtnRight.Enabled = false;
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
				var complect = complectsLBoxLeft.SelectedItem as Complect;
				var cabinet = complect is null ? cabinetsLBoxLeft.SelectedItem as Cabinet : complect.Cabinet;
				EditComplect(complect, cabinet);
			}
			else if (sender as Button == editComplectBtnRight)
			{
				var complect = complectsLBoxRight.SelectedItem as Complect;
				var cabinet = complect is null ? cabinetsLBoxRight.SelectedItem as Cabinet : complect.Cabinet;
				EditComplect(complect, cabinet);
			}
		}

		private void EditComplect(Complect? complect, Cabinet cabinet)
		{
			var form = new EditComplectForm(complect, cabinet);
			var result = form.ShowDialog();
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
			var before = new List<string>();
			var after = new List<string>();
			var complect = complectsLBoxLeft.SelectedItem as Complect;
			var devicesInComplect = context.Devices.Include(d => d.Complect.Cabinet.Building)
				.Where(d => d.Complect == complect)
				.ToList();

			foreach (var device in devicesInComplect)
				before.Add(device.ToStringForHistory());

			complect.Cabinet = cabinetsLBoxRight.SelectedItem as Cabinet;

			foreach (var device in devicesInComplect)
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
			var before = new List<string>();
			var after = new List<string>();
			var complect = complectsLBoxRight.SelectedItem as Complect;
			var devicesInComplect = context.Devices.Include(d => d.Complect.Cabinet.Building)
				.Where(d => d.Complect == complect)
				.ToList();

			foreach (var device in devicesInComplect)
				before.Add(device.ToStringForHistory());

			complect.Cabinet = cabinetsLBoxLeft.SelectedItem as Cabinet;

			foreach (var device in devicesInComplect)
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

		// Блок работы со списком единиц техники
		private void RefreshDevicesLBoxes()
		{
			RefreshDevicesLBoxLeft();
			RefreshDevicesLBoxRight();
		}

		private void RefreshDevicesLBoxLeft()
		{
			var selectedItem = devicesLBoxLeft.SelectedItem;
			if (searchTBoxLeft.Text.Length == 0)
				devicesLBoxLeft.DataSource = context.Devices.Include(d => d.DeviceName)
					.Where(d => d.Complect == complectsLBoxLeft.SelectedItem)
					.OrderBy(d => d.DeviceName.Name)
					.ToList();
			else
				devicesLBoxLeft.DataSource = SearchDevice(searchTBoxLeft.Text, complectsLBoxLeft.SelectedItem as Complect);
			RestoreSelectedItem(devicesLBoxLeft, selectedItem);
			SwitchMoveDeviceBtns();
		}

		private void RefreshDevicesLBoxRight()
		{
			var selectedItem = devicesLBoxRight.SelectedItem;
			if (searchTBoxRight.Text.Length == 0)
				devicesLBoxRight.DataSource = context.Devices.Include(d => d.DeviceName)
					.Where(d => d.Complect == complectsLBoxRight.SelectedItem)
					.OrderBy(d => d.DeviceName.Name)
					.ToList();
			else
				devicesLBoxRight.DataSource = SearchDevice(searchTBoxRight.Text, complectsLBoxRight.SelectedItem as Complect);
			RestoreSelectedItem(devicesLBoxRight, selectedItem);
			SwitchMoveDeviceBtns();
		}

		private List<Device> SearchDevice(string text, Complect? complect)
		{
			text = text.ToLower();
			return context.Devices.ToList()
				.Where(d => d.ToString().ToLower().Contains(text) && d.Complect == complect)
				.OrderBy(d => d.DeviceName.Name)
				.ToList();
		}

		private void SwitchEditDeviceBtnLeft()
		{
			if (complectsLBoxLeft.SelectedIndex != -1 && context.DeviceNames.Any() && context.DeviceProviders.Any())
				editDeviceBtnLeft.Enabled = true;
			else
				editDeviceBtnLeft.Enabled = false;
		}

		private void SwitchEditDeviceBtnRight()
		{
			if (complectsLBoxRight.SelectedIndex != -1 && context.DeviceNames.Any() && context.DeviceProviders.Any())
				editDeviceBtnRight.Enabled = true;
			else
				editDeviceBtnRight.Enabled = false;
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
				var device = (Device?)devicesLBoxLeft.SelectedItem;
				var complect = device is null ? complectsLBoxLeft.SelectedItem as Complect : device.Complect;
				EditDevice(device, complect);
			}
			else if (sender as Button == editDeviceBtnRight)
			{
				var device = (Device?)devicesLBoxRight.SelectedItem;
				var complect = device is null ? complectsLBoxRight.SelectedItem as Complect : device.Complect;
				EditDevice(device, complect);
			}
		}

		private void EditDevice(Device? device, Complect complect)
		{
			var form = new EditDeviceForm(device, complect);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshBuildings();
		}

		private void devicesLBox_SelectedValueChanged(object sender, EventArgs e)
		{
			SwitchMoveDeviceBtns();
		}

		private async void moveDeviceToRightBtn_Click(object sender, EventArgs e)
		{
			var device = devicesLBoxLeft.SelectedItem as Device;
			var before = device.ToStringForHistory();
			device.Complect = complectsLBoxRight.SelectedItem as Complect;
			var after = device.ToStringForHistory();
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
			var device = devicesLBoxRight.SelectedItem as Device;
			var before = device.ToStringForHistory();
			device.Complect = complectsLBoxLeft.SelectedItem as Complect;
			var after = device.ToStringForHistory();
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

		// Блок работы со списком типов техники
		private void deviceTypeEditBtn_Click(object sender, EventArgs e)
		{
			var deviceType = deviceTypesLBox.SelectedItem as DeviceType;
			var form = new EditDeviceTypeForm(deviceType);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshDeviceTypes();
		}

		private void RefreshDeviceTypesAndNames()
		{
			RefreshDeviceTypes();
			RefreshDeviceNames();
		}

		private void RefreshDeviceTypes()
		{
			var selectedItem = deviceTypesLBox.SelectedItem;
			deviceTypesLBox.DataSource = context.DeviceTypes.OrderBy(d => d.Name).ToList();
			if (selectedItem is not null)
				if (deviceTypesLBox.Items.Contains(selectedItem))
					deviceTypesLBox.SelectedItem = selectedItem;
			RefreshDeviceNames();
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

		// Блок работы со списком названий техники
		private void deviceNameEditBtn_Click(object sender, EventArgs e)
		{
			var deviceName = deviceNamesLBox.SelectedItem as DeviceName;
			var deviceType = deviceName is null ? deviceTypesLBox.SelectedItem as DeviceType : deviceName.DeviceType;
			var form = new EditDeviceNameForm(deviceName, deviceType);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshDeviceTypes();
		}

		private void RefreshDeviceNames()
		{
			var selectedItem = deviceNamesLBox.SelectedItem;
			deviceNamesLBox.DataSource = context.DeviceNames.Where(dn => dn.DeviceType == deviceTypesLBox.SelectedItem)
				.OrderBy(d => d.Name)
				.ToList();
			if (selectedItem is not null)
				if (deviceNamesLBox.Items.Contains(selectedItem))
					deviceNamesLBox.SelectedItem = selectedItem;
			SwitchEditDeviceBtnLeft();
			SwitchEditDeviceBtnRight();
		}

		// Блок работы со списком предоставителей техники
		private void deviceProviderEditBtn_Click(object sender, EventArgs e)
		{
			var deviceProvider = deviceProvidersLBox.SelectedItem as DeviceProvider;
			var form = new EditDeviceProviderForm(deviceProvider);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshDeviceProviders();
		}

		private void RefreshDeviceProviders()
		{
			var selectedItem = deviceProvidersLBox.SelectedItem;
			deviceProvidersLBox.DataSource = context.DeviceProviders.OrderBy(d => d.Name).ToList();
			if (selectedItem is not null)
				if (deviceProvidersLBox.Items.Contains(selectedItem))
					deviceProvidersLBox.SelectedItem = selectedItem;
			SwitchEditDeviceBtnLeft();
			SwitchEditDeviceBtnRight();
		}

		// Блок работы с историей перемещений
		private void InitializeHistoryDGW()
		{
			historyDGW.DataSource = context.History.OrderByDescending(h => h.ChangedAt).ToList();
			historyDGW.Columns[0].Visible = false;
			historyDGW.Columns[1].HeaderText = "Было";
			historyDGW.Columns[2].HeaderText = "Стало";
			historyDGW.Columns[3].HeaderText = "Дата и время изменения";
		}

		private void RefreshHistoryDGW()
		{
			if (historySearchTBox.Text.Length == 0)
				historyDGW.DataSource = context.History.OrderByDescending(h => h.ChangedAt).ToList();
			else
			{
				var text = historySearchTBox.Text.ToLower();
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

		// Блок работы с полным списком техники
		private void InitializeFullListDGW()
		{
			fullListDGW.DataSource = context.Devices.Select(d => new
			{
				d.Complect.Cabinet.Building,
				d.Complect.Cabinet,
				d.Complect,
				d.DeviceName.DeviceType,
				d.DeviceName,
				d.DeviceProvider,
				d.Serial,
				d.Inventory
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

			fullListNumberOfDevicesLabel.Text = $"Всего единиц техники: {fullListDGW.RowCount}";
		}

		private void RefreshFullListDGW()
		{
			var list = context.Devices.Select(d => new
			{
				d.Complect.Cabinet.Building,
				d.Complect.Cabinet,
				d.Complect,
				d.DeviceName.DeviceType,
				d.DeviceName,
				d.DeviceProvider,
				d.Serial,
				d.Inventory
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
	}
}
