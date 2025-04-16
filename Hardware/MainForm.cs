using Hardware.Forms;
using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware
{
	public partial class MainForm : Form
	{
		readonly ApplicationContext context;
		public MainForm()
		{
			InitializeComponent();
			context = ApplicationContext.Instanse();
			Init();
		}

		private void Init()
		{
			InitializeButtons();
			InitializeHistoryDGW();
			RefreshAll();
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
			if ((TextBox)sender == searchTBoxLeft)
				RefreshLeft();
			else if ((TextBox)sender == searchTBoxRight)
				RefreshRight();
		}

		private void RefreshAll()
		{
			RefreshBuildings();
			RefreshCabinets();
			RefreshComplects();
			RefreshDevicesLBoxes();
			RefreshDeviceProviders();
			RefreshDeviceTypesAndNames();
		}

		private void RefreshLeft()
		{
			RefreshBuildingsLBoxLeft();
			RefreshCabinetsLBoxLeft();
			RefreshComplectsLBoxLeft();
			RefreshDevicesLBoxLeft();
		}

		private void RefreshRight()
		{
			RefreshBuildingsLBoxRight();
			RefreshCabinetsLBoxRight();
			RefreshComplectsLBoxRight();
			RefreshDevicesLBoxRight();
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
			if (searchTBoxLeft.Text.Length == 0)
				buildingsLBoxLeft.DataSource = context.Buildings.ToList();
			else
				buildingsLBoxLeft.DataSource = SearchBuildingByDevice(searchTBoxLeft.Text);
			if (selectedItem is not null)
				if (buildingsLBoxLeft.Items.Contains(selectedItem))
					buildingsLBoxLeft.SelectedItem = selectedItem;
			SwitchEditCabinetBtnLeft();
		}

		private void RefreshBuildingsLBoxRight()
		{
			var selectedItem = buildingsLBoxRight.SelectedItem;
			if (searchTBoxRight.Text.Length == 0)
				buildingsLBoxRight.DataSource = context.Buildings.ToList();
			else
				buildingsLBoxRight.DataSource = SearchBuildingByDevice(searchTBoxRight.Text);
			if (selectedItem is not null)
				if (buildingsLBoxRight.Items.Contains(selectedItem))
					buildingsLBoxRight.SelectedItem = selectedItem;
			SwitchEditCabinetBtnRight();
		}

		private List<Building> SearchBuildingByDevice(string text)
		{
			text = text.ToLower();
			return [.. context.Devices.Where(d => d.DeviceName.Name.ToLower().Contains(text) || d.Serial.ToLower().Contains(text) || d.Inventory.ToLower().Contains(text))
					.GroupBy(d => d.Complect.Cabinet.Building)
					.Select(d => d.First().Complect.Cabinet.Building)];
		}

		private void editBuildingBtn_Click(object sender, EventArgs e)
		{
			if (((Button)sender) == editBuildingBtnLeft)
			{
				var building = (Building?)buildingsLBoxLeft.SelectedItem;
				EditBuilding(building);
			}
			else if (((Button)sender) == editBuildingBtnRight)
			{
				var building = (Building?)buildingsLBoxRight.SelectedItem;
				EditBuilding(building);
			}
		}

		private void EditBuilding(Building? building)
		{
			var form = new EditBuildingForm(building);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshAll();
		}

		private void buildingsLBox_SelectedValueChanged(object sender, EventArgs e)
		{
			if (((ListBox)sender) == buildingsLBoxLeft)
				RefreshCabinetsLBoxLeft();
			else if (((ListBox)sender) == buildingsLBoxRight)
				RefreshCabinetsLBoxRight();
		}

		// Блок работы со списком кабинетов
		private void RefreshCabinets()
		{
			RefreshCabinetsLBoxLeft();
			RefreshCabinetsLBoxRight();
		}

		private void RefreshCabinetsLBoxLeft()
		{
			var selectedItem = cabinetsLBoxLeft.SelectedItem;
			if (searchTBoxLeft.Text.Length == 0)
				cabinetsLBoxLeft.DataSource = context.Cabinets.Where(c => c.Building == buildingsLBoxLeft.SelectedItem).ToList();
			else
				cabinetsLBoxLeft.DataSource = SearchCabinetByDevice(searchTBoxLeft.Text, (Building?)buildingsLBoxLeft.SelectedItem);
			if (selectedItem is not null)
				if (cabinetsLBoxLeft.Items.Contains(selectedItem))
					cabinetsLBoxLeft.SelectedItem = selectedItem;
			RefreshComplectsLBoxLeft();
			SwitchEditComplectBtnLeft();
			SwitchMoveCabinetBtns();
		}

		private void RefreshCabinetsLBoxRight()
		{
			var selectedItem = cabinetsLBoxRight.SelectedItem;
			if (searchTBoxRight.Text.Length == 0)
				cabinetsLBoxRight.DataSource = context.Cabinets.Where(c => c.Building == buildingsLBoxRight.SelectedItem).ToList();
			else
				cabinetsLBoxRight.DataSource = SearchCabinetByDevice(searchTBoxRight.Text, (Building?)buildingsLBoxRight.SelectedItem);
			if (selectedItem is not null)
				if (cabinetsLBoxRight.Items.Contains(selectedItem))
					cabinetsLBoxRight.SelectedItem = selectedItem;
			RefreshComplectsLBoxRight();
			SwitchEditComplectBtnRight();
			SwitchMoveCabinetBtns();
		}

		private List<Cabinet> SearchCabinetByDevice(string text, Building? building)
		{
			text = text.ToLower();
			return [.. context.Devices.Where(d => d.DeviceName.Name.ToLower().Contains(text) || d.Serial.ToLower().Contains(text) || d.Inventory.ToLower().Contains(text))
				.GroupBy(d => d.Complect.Cabinet)
				.Select(d => d.First().Complect.Cabinet)
				.ToList()
				.Where(c => c.Building == building)];
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
			if (((Button)sender) == editCabinetBtnLeft)
			{
				var cabinet = (Cabinet?)cabinetsLBoxLeft.SelectedItem;
				var building = cabinet is null ? (Building)buildingsLBoxLeft.SelectedItem : cabinet.Building;
				EditCabinet(cabinet, building);
			}
			else if (((Button)sender) == editCabinetBtnRight)
			{
				var cabinet = (Cabinet?)cabinetsLBoxRight.SelectedItem;
				var building = cabinet is null ? (Building)buildingsLBoxRight.SelectedItem : cabinet.Building;
				EditCabinet(cabinet, building);
			}
		}

		private void EditCabinet(Cabinet? cabinet, Building building)
		{
			var form = new EditCabinetForm(cabinet, building);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshAll();

		}

		private void cabinetsLBox_SelectedValueChanged(object sender, EventArgs e)
		{
			if (((ListBox)sender) == cabinetsLBoxLeft)
				RefreshComplectsLBoxLeft();
			else if (((ListBox)sender) == cabinetsLBoxRight)
				RefreshComplectsLBoxRight();
		}

		private async void moveCabinetToRightBtn_Click(object sender, EventArgs e)
		{
			List<string> before = [];
			List<string> after = [];
			Cabinet cabinet = (Cabinet)cabinetsLBoxLeft.SelectedItem;
			var cabinetDevices = await context.Devices.Include(d => d.Complect.Cabinet.Building).Where(d => d.Complect.Cabinet == cabinet).ToListAsync();
			if (cabinetDevices.Count > 0)
			{
				foreach (var device in cabinetDevices)
					before.Add($"{device.Complect.Cabinet.Building.Name} -> {device.Complect.Cabinet.Name} -> {device.Complect.Name} -> {device.DeviceName} {device.Serial} {device.Inventory}");
				cabinet.Building = (Building)buildingsLBoxRight.SelectedItem;
				foreach (var device in cabinetDevices)
					after.Add($"{device.Complect.Cabinet.Building.Name} -> {device.Complect.Cabinet.Name} -> {device.Complect.Name} -> {device.DeviceName} {device.Serial} {device.Inventory}");
				for (int i = 0; i < cabinetDevices.Count; i++)
					await context.History.AddAsync(new History() { Before = before[i], After = after[i] });
			}
			else
				cabinet.Building = (Building)buildingsLBoxRight.SelectedItem;
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
				RefreshAll();
		}

		private async void moveCabinetToLeftBtn_Click(object sender, EventArgs e)
		{
			List<string> before = [];
			List<string> after = [];
			Cabinet cabinet = (Cabinet)cabinetsLBoxRight.SelectedItem;
			var cabinetDevices = await context.Devices.Include(d => d.Complect.Cabinet.Building).Where(d => d.Complect.Cabinet == cabinet).ToListAsync();
			if (cabinetDevices.Count > 0)
			{
				foreach (var device in cabinetDevices)
					before.Add($"{device.Complect.Cabinet.Building.Name} -> {device.Complect.Cabinet.Name} -> {device.Complect.Name} -> {device.DeviceName} {device.Serial} {device.Inventory}");
				cabinet.Building = (Building)buildingsLBoxLeft.SelectedItem;
				foreach (var device in cabinetDevices)
					after.Add($"{device.Complect.Cabinet.Building.Name} -> {device.Complect.Cabinet.Name} -> {device.Complect.Name} -> {device.DeviceName} {device.Serial} {device.Inventory}");
				for (int i = 0; i < cabinetDevices.Count; i++)
					await context.History.AddAsync(new History() { Before = before[i], After = after[i] });
			}
			else
				cabinet.Building = (Building)buildingsLBoxLeft.SelectedItem;
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
				RefreshAll();
		}

		// Блок работы со списком комплектов
		private void RefreshComplects()
		{
			RefreshComplectsLBoxLeft();
			RefreshComplectsLBoxRight();
		}

		private void RefreshComplectsLBoxLeft()
		{
			var selectedItem = complectsLBoxLeft.SelectedItem;
			if (searchTBoxLeft.Text.Length == 0)
				complectsLBoxLeft.DataSource = context.Complects.Where(c => c.Cabinet == cabinetsLBoxLeft.SelectedItem).ToList();
			else
				complectsLBoxLeft.DataSource = SearchComplectByDevice(searchTBoxLeft.Text, (Cabinet?)cabinetsLBoxLeft.SelectedItem);
			if (selectedItem is not null)
				if (complectsLBoxLeft.Items.Contains(selectedItem))
					complectsLBoxLeft.SelectedItem = selectedItem;
			RefreshDevicesLBoxLeft();
			SwitchEditDeviceBtnLeft();
			SwitchMoveComplectBtns();
		}

		private void RefreshComplectsLBoxRight()
		{
			var selectedItem = complectsLBoxRight.SelectedItem;
			if (searchTBoxRight.Text.Length == 0)
				complectsLBoxRight.DataSource = context.Complects.Where(c => c.Cabinet == cabinetsLBoxRight.SelectedItem).ToList();
			else
				complectsLBoxRight.DataSource = SearchComplectByDevice(searchTBoxRight.Text, (Cabinet?)cabinetsLBoxRight.SelectedItem);
			if (selectedItem is not null)
				if (complectsLBoxRight.Items.Contains(selectedItem))
					complectsLBoxRight.SelectedItem = selectedItem;
			RefreshDevicesLBoxRight();
			SwitchEditDeviceBtnRight();
			SwitchMoveComplectBtns();
		}

		private List<Complect> SearchComplectByDevice(string text, Cabinet? cabinet)
		{
			text = text.ToLower();
			return [..context.Devices.Where(d => d.DeviceName.Name.ToLower().Contains(text) || d.Serial.ToLower().Contains(text) || d.Inventory.ToLower().Contains(text))
				.GroupBy(d => d.Complect)
				.Select(d => d.First().Complect)
				.ToList()
				.Where(c => c.Cabinet == cabinet)];
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
			if (((Button)sender) == editComplectBtnLeft)
			{
				var complect = (Complect?)complectsLBoxLeft.SelectedItem;
				var cabinet = complect is null ? (Cabinet)cabinetsLBoxLeft.SelectedItem : complect.Cabinet;
				EditComplect(complect, cabinet);
			}
			else if (((Button)sender) == editComplectBtnRight)
			{
				var complect = (Complect?)complectsLBoxRight.SelectedItem;
				var cabinet = complect is null ? (Cabinet)cabinetsLBoxRight.SelectedItem : complect.Cabinet;
				EditComplect(complect, cabinet);
			}
		}

		private void EditComplect(Complect? complect, Cabinet cabinet)
		{
			var form = new EditComplectForm(complect, cabinet);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshAll();
		}

		private void complectsLBox_SelectedValueChanged(object sender, EventArgs e)
		{
			if (((ListBox)sender) == complectsLBoxLeft)
				RefreshDevicesLBoxLeft();
			else if (((ListBox)sender) == complectsLBoxRight)
				RefreshDevicesLBoxRight();
		}

		private async void moveComplectToRightBtn_Click(object sender, EventArgs e)
		{
			List<string> before = [];
			List<string> after = [];
			Complect complect = (Complect)complectsLBoxLeft.SelectedItem;
			var complectDevices = await context.Devices.Include(d => d.Complect.Cabinet.Building).Where(d => d.Complect == complect).ToListAsync();
			if (complectDevices.Count > 0)
			{
				foreach (var device in complectDevices)
					before.Add($"{device.Complect.Cabinet.Building.Name} -> {device.Complect.Cabinet.Name} -> {device.Complect.Name} -> {device.DeviceName} {device.Serial} {device.Inventory}");
				complect.Cabinet = (Cabinet)cabinetsLBoxRight.SelectedItem;
				foreach (var device in complectDevices)
					after.Add($"{device.Complect.Cabinet.Building.Name} -> {device.Complect.Cabinet.Name} -> {device.Complect.Name} -> {device.DeviceName} {device.Serial} {device.Inventory}");
				for (int i = 0; i < complectDevices.Count; i++)
					await context.History.AddAsync(new History() { Before = before[i], After = after[i] });
			}
			else
				complect.Cabinet = (Cabinet)cabinetsLBoxRight.SelectedItem;
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
				RefreshAll();
		}

		private async void moveComplectToLeftBtn_Click(object sender, EventArgs e)
		{
			List<string> before = [];
			List<string> after = [];
			Complect complect = (Complect)complectsLBoxRight.SelectedItem;
			var complectDevices = await context.Devices.Include(d => d.Complect.Cabinet.Building).Where(d => d.Complect == complect).ToListAsync();
			if (complectDevices.Count > 0)
			{
				foreach (var device in complectDevices)
					before.Add($"{device.Complect.Cabinet.Building.Name} -> {device.Complect.Cabinet.Name} -> {device.Complect.Name} -> {device.DeviceName} {device.Serial} {device.Inventory}");
				complect.Cabinet = (Cabinet)cabinetsLBoxLeft.SelectedItem;
				foreach (var device in complectDevices)
					after.Add($"{device.Complect.Cabinet.Building.Name} -> {device.Complect.Cabinet.Name} -> {device.Complect.Name} -> {device.DeviceName} {device.Serial} {device.Inventory}");
				for (int i = 0; i < complectDevices.Count; i++)
					await context.History.AddAsync(new History() { Before = before[i], After = after[i] });
			}
			else
				complect.Cabinet = (Cabinet)cabinetsLBoxLeft.SelectedItem;
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
				RefreshAll();
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
			devicesLBoxLeft.DataSource = context.Devices.Include(d => d.DeviceName).Where(d => d.Complect == complectsLBoxLeft.SelectedItem).ToList();
			if (selectedItem is not null)
				if (devicesLBoxLeft.Items.Contains(selectedItem))
					devicesLBoxLeft.SelectedItem = selectedItem;
			SwitchMoveDeviceBtns();
		}

		private void RefreshDevicesLBoxRight()
		{
			var selectedItem = devicesLBoxRight.SelectedItem;
			devicesLBoxRight.DataSource = context.Devices.Include(d => d.DeviceName).Where(d => d.Complect == complectsLBoxRight.SelectedItem).ToList();
			if (selectedItem is not null)
				if (devicesLBoxRight.Items.Contains(selectedItem))
					devicesLBoxRight.SelectedItem = selectedItem;
			SwitchMoveDeviceBtns();
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
			if (((Button)sender) == editDeviceBtnLeft)
			{
				var device = (Device?)devicesLBoxLeft.SelectedItem;
				var complect = device is null ? (Complect)complectsLBoxLeft.SelectedItem : device.Complect;
				EditDevice(device, complect);
			}
			else if (((Button)sender) == editDeviceBtnRight)
			{
				var device = (Device?)devicesLBoxRight.SelectedItem;
				var complect = device is null ? (Complect)complectsLBoxRight.SelectedItem : device.Complect;
				EditDevice(device, complect);
			}
		}

		private void EditDevice(Device? device, Complect complect)
		{
			var form = new EditDeviceForm(device, complect);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshAll();
		}

		private void devicesLBox_SelectedValueChanged(object sender, EventArgs e)
		{
			SwitchMoveDeviceBtns();
		}

		private async void moveDeviceToRightBtn_Click(object sender, EventArgs e)
		{
			Device device = (Device)devicesLBoxLeft.SelectedItem;
			var before = $"{device.Complect.Cabinet.Building.Name} -> {device.Complect.Cabinet.Name} -> {device.Complect.Name} -> {device.DeviceName} {device.Serial} {device.Inventory}";
			device.Complect = (Complect)complectsLBoxRight.SelectedItem;
			var after = $"{device.Complect.Cabinet.Building.Name} -> {device.Complect.Cabinet.Name} -> {device.Complect.Name} -> {device.DeviceName} {device.Serial} {device.Inventory}";
			context.History.AddAsync(new History() { Before = before, After = after });
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
				RefreshAll();
		}

		private async void moveDeviceToLeftBtn_Click(object sender, EventArgs e)
		{
			Device device = (Device)devicesLBoxRight.SelectedItem;
			var before = $"{device.Complect.Cabinet.Building.Name} -> {device.Complect.Cabinet.Name} -> {device.Complect.Name} -> {device.DeviceName} {device.Serial} {device.Inventory}";
			device.Complect = (Complect)complectsLBoxLeft.SelectedItem;
			var after = $"{device.Complect.Cabinet.Building.Name} -> {device.Complect.Cabinet.Name} -> {device.Complect.Name} -> {device.DeviceName} {device.Serial} {device.Inventory}";
			context.History.AddAsync(new History() { Before = before, After = after });
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
				RefreshAll();
		}

		// Блок работы со списком типов техники
		private void deviceTypeEditBtn_Click(object sender, EventArgs e)
		{
			var deviceType = (DeviceType?)deviceTypesLBox.SelectedItem;
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
			deviceTypesLBox.DataSource = context.DeviceTypes.ToList();
			if (selectedItem is not null)
				if (deviceTypesLBox.Items.Contains(selectedItem))
					deviceTypesLBox.SelectedItem = selectedItem;
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
			var deviceName = (DeviceName?)deviceNamesLBox.SelectedItem;
			var deviceType = deviceName is null ? (DeviceType)deviceTypesLBox.SelectedItem : deviceName.DeviceType;
			var form = new EditDeviceNameForm(deviceName, deviceType);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshDeviceTypesAndNames();
		}

		private void RefreshDeviceNames()
		{
			var selectedItem = deviceNamesLBox.SelectedItem;
			deviceNamesLBox.DataSource = context.DeviceNames.Where(dn => dn.DeviceType == deviceTypesLBox.SelectedItem).ToList();
			if (selectedItem is not null)
				if (deviceNamesLBox.Items.Contains(selectedItem))
					deviceNamesLBox.SelectedItem = selectedItem;
			SwitchEditDeviceBtnLeft();
			SwitchEditDeviceBtnRight();
		}

		// Блок работы со списком предоставителей техники
		private void deviceProviderEditBtn_Click(object sender, EventArgs e)
		{
			var deviceProvider = (DeviceProvider?)deviceProvidersLBox.SelectedItem;
			var form = new EditDeviceProviderForm(deviceProvider);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshDeviceProviders();
		}

		private void RefreshDeviceProviders()
		{
			var selectedItem = deviceProvidersLBox.SelectedItem;
			deviceProvidersLBox.DataSource = context.DeviceProviders.ToList();
			if (selectedItem is not null)
				if (deviceProvidersLBox.Items.Contains(selectedItem))
					deviceProvidersLBox.SelectedItem = selectedItem;
			SwitchEditDeviceBtnLeft();
			SwitchEditDeviceBtnRight();
		}

		// Блок работы с историей перемещений
		private void InitializeHistoryDGW()
		{
			historyDGW.DataSource = context.History.ToList();
			historyDGW.Columns[0].Visible = false;
			historyDGW.Columns[1].HeaderText = "Было";
			historyDGW.Columns[2].HeaderText = "Стало";
			historyDGW.Columns[3].HeaderText = "Дата и время изменения";
		}

		private void RefreshHistoryDGW()
		{
			if (historySearchTBox.Text.Length == 0)
				historyDGW.DataSource = context.History.ToList();
			else
			{
				var text = historySearchTBox.Text.ToLower();
				historyDGW.DataSource = context.History.Where(h => h.Before.ToLower().Contains(text) || h.After.ToLower().Contains(text)).ToList();
			}
		}

		private void historySearchTBox_TextChanged(object sender, EventArgs e)
		{
			RefreshHistoryDGW();
		}
	}
}
