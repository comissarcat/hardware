using Hardware.Forms;
using Hardware.Models;
using Hardware.Temp;
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
			//Import();
			Init();
		}

		// Блок импорта данных из старой БД
		private void Import()
		{
			var tempContext = new TempApplicatonContext();
			ImportBuildings(tempContext);
			ImportCabinets(tempContext);
			ImportComplects(tempContext);
			ImportDeviceTypes(tempContext);
			ImportDeviceNames(tempContext);
			CreateDeviceProviders();
			ImportDevices(tempContext);
		}

		private void ImportBuildings(TempApplicatonContext tempContext)
		{
			foreach (var building in tempContext.Buildings)
				if (!context.Buildings.Where(b => b.Name == building.Name).Any())
					context.Buildings.Add(new() { Name = building.Name });
			context.SaveChanges();
		}

		private void ImportCabinets(TempApplicatonContext tempContext)
		{
			foreach (var complect in tempContext.Complects)
			{
				var building = context.Buildings.Where(b => b.Name == complect.BuildingEntity.Name).First();
				var name = complect.Cabinet;
				if (!context.Cabinets.Where(c => c.Building == building && c.Name == name).Any())
					context.Cabinets.Add(new() { Building = building, Name = name });
				context.SaveChanges();
			}
		}

		private void ImportComplects(TempApplicatonContext tempContext)
		{
			foreach (var complect in tempContext.Complects)
			{
				var building = context.Buildings.Where(b => b.Name == complect.BuildingEntity.Name).First();
				var cabinet = context.Cabinets.Where(c => c.Building == building && c.Name == complect.Cabinet).First();
				var name = complect.User;
				if (!context.Complects.Where(c => c.Cabinet == cabinet && c.Name == name).Any())
					context.Complects.Add(new() { Cabinet = cabinet, Name = name });
				context.SaveChanges();
			}
		}

		private void ImportDeviceTypes(TempApplicatonContext tempContext)
		{
			foreach (var deviceType in tempContext.Types)
				if (!context.DeviceTypes.Where(d => d.Name == deviceType.Type).Any())
					context.DeviceTypes.Add(new() { Name = deviceType.Type });
			context.SaveChanges();
		}

		private void ImportDeviceNames(TempApplicatonContext tempContext)
		{
			foreach (var deviceName in tempContext.Names)
			{
				var deviceType = context.DeviceTypes.Where(d => d.Name == deviceName.TypeEntity.Type).First();
				var name = deviceName.Name;
				if (!context.DeviceNames.Where(d => d.DeviceType == deviceType && d.Name == name).Any())
					context.DeviceNames.Add(new() { DeviceType = deviceType, Name = name });
				context.SaveChanges();
			}
		}

		private void CreateDeviceProviders()
		{
			if (!context.DeviceProviders.Any())
			{
				context.DeviceProviders.Add(new() { Name = "ИАЦ" });
				context.SaveChanges();
			}
		}

		private void ImportDevices(TempApplicatonContext tempContext)
		{
			foreach (var device in tempContext.Devices)
			{
				var serial = device.Serial_number;
				var inventory = device.Inventory_number;
				var deviceName = context.DeviceNames.Where(d => d.Name == device.NameEntity.Name).First();
				var complect = context.Complects.Where(c => c.Name == device.ComplectEntity.User && c.Cabinet.Name == device.ComplectEntity.Cabinet && c.Cabinet.Building.Name == device.ComplectEntity.BuildingEntity.Name).First();
				var deviceProvider = context.DeviceProviders.First();
				if (!context.Devices.Where(d => d.Serial == serial).Any())
				{
					context.Devices.Add(new()
					{
						Serial = serial,
						Inventory = inventory,
						DeviceName = deviceName,
						Complect = complect,
						DeviceProvider = deviceProvider
					});
					context.SaveChanges();
				}
			}
		}

		// Конец блока импорта данных из старой БД
		private void Init()
		{
			InitializeButtons();
			InitializeHistoryDGW();
			InitializeFullListDGW();
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
				buildingsLBoxLeft.DataSource = context.Buildings.OrderBy(b => b.Name)
																.ToList();
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
				buildingsLBoxRight.DataSource = context.Buildings.OrderBy(b => b.Name)
																 .ToList();
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
			return context.Devices.ToList()
								  .Where(d => d.ToString().ToLower().Contains(text))
								  .GroupBy(d => d.Complect.Cabinet.Building)
								  .Select(d => d.First().Complect.Cabinet.Building)
								  .OrderBy(b => b.Name)
								  .ToList();
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
				cabinetsLBoxLeft.DataSource = context.Cabinets.Where(c => c.Building == buildingsLBoxLeft.SelectedItem)
															  .OrderBy(c => c.Name)
															  .ToList();
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
				cabinetsLBoxRight.DataSource = context.Cabinets.Where(c => c.Building == buildingsLBoxRight.SelectedItem)
															   .OrderBy(c => c.Name)
															   .ToList();
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
			var devicesInCabinet = context.Devices.Include(d => d.Complect.Cabinet.Building)
												  .Where(d => d.Complect.Cabinet == cabinet)
												  .ToList();
			if (devicesInCabinet.Count > 0)
			{
				foreach (var device in devicesInCabinet)
					before.Add(device.ToStringForHistory());

				cabinet.Building = (Building)buildingsLBoxRight.SelectedItem;

				foreach (var device in devicesInCabinet)
					after.Add(device.ToStringForHistory());

				for (int i = 0; i < devicesInCabinet.Count; i++)
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
			var devicesInCabinet = context.Devices.Include(d => d.Complect.Cabinet.Building)
												  .Where(d => d.Complect.Cabinet == cabinet)
												  .ToList();
			if (devicesInCabinet.Count > 0)
			{
				foreach (var device in devicesInCabinet)
					before.Add(device.ToStringForHistory());

				cabinet.Building = (Building)buildingsLBoxLeft.SelectedItem;

				foreach (var device in devicesInCabinet)
					after.Add(device.ToStringForHistory());

				for (int i = 0; i < devicesInCabinet.Count; i++)
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
				complectsLBoxLeft.DataSource = context.Complects.Where(c => c.Cabinet == cabinetsLBoxLeft.SelectedItem)
																.OrderBy(c => c.Name)
																.ToList();
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
				complectsLBoxRight.DataSource = context.Complects.Where(c => c.Cabinet == cabinetsLBoxRight.SelectedItem)
																 .OrderBy(c => c.Name)
																 .ToList();
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
			var devicesInComplect = context.Devices.Include(d => d.Complect.Cabinet.Building)
												   .Where(d => d.Complect == complect)
												   .ToList();
			if (devicesInComplect.Count > 0)
			{
				foreach (var device in devicesInComplect)
					before.Add(device.ToStringForHistory());

				complect.Cabinet = (Cabinet)cabinetsLBoxRight.SelectedItem;

				foreach (var device in devicesInComplect)
					after.Add(device.ToStringForHistory());

				for (int i = 0; i < devicesInComplect.Count; i++)
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
			var devicesInComplect = context.Devices.Include(d => d.Complect.Cabinet.Building)
												   .Where(d => d.Complect == complect)
												   .ToList();
			if (devicesInComplect.Count > 0)
			{
				foreach (var device in devicesInComplect)
					before.Add(device.ToStringForHistory());

				complect.Cabinet = (Cabinet)cabinetsLBoxLeft.SelectedItem;

				foreach (var device in devicesInComplect)
					after.Add(device.ToStringForHistory());

				for (int i = 0; i < devicesInComplect.Count; i++)
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
			devicesLBoxLeft.DataSource = context.Devices.Include(d => d.DeviceName)
														.Where(d => d.Complect == complectsLBoxLeft.SelectedItem)
														.OrderBy(d => d.DeviceName.Name)
														.ToList();
			if (selectedItem is not null)
				if (devicesLBoxLeft.Items.Contains(selectedItem))
					devicesLBoxLeft.SelectedItem = selectedItem;
			SwitchMoveDeviceBtns();
		}

		private void RefreshDevicesLBoxRight()
		{
			var selectedItem = devicesLBoxRight.SelectedItem;
			devicesLBoxRight.DataSource = context.Devices.Include(d => d.DeviceName)
														 .Where(d => d.Complect == complectsLBoxRight.SelectedItem)
														 .OrderBy(d => d.DeviceName.Name)
														 .ToList();
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
			var before = device.ToStringForHistory();
			device.Complect = (Complect)complectsLBoxRight.SelectedItem;
			var after = device.ToStringForHistory();
			await context.History.AddAsync(new History() { Before = before, After = after });
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
			var before = device.ToStringForHistory();
			device.Complect = (Complect)complectsLBoxLeft.SelectedItem;
			var after = device.ToStringForHistory();
			await context.History.AddAsync(new History() { Before = before, After = after });
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
			deviceTypesLBox.DataSource = context.DeviceTypes.OrderBy(d => d.Name)
															.ToList();
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
			var deviceProvider = (DeviceProvider?)deviceProvidersLBox.SelectedItem;
			var form = new EditDeviceProviderForm(deviceProvider);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshDeviceProviders();
		}

		private void RefreshDeviceProviders()
		{
			var selectedItem = deviceProvidersLBox.SelectedItem;
			deviceProvidersLBox.DataSource = context.DeviceProviders.OrderBy(d => d.Name)
																	.ToList();
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
				historyDGW.DataSource = context.History.Where(h => h.Before.ToLower().Contains(text) || h.After.ToLower().Contains(text))
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
		}

		private void RefreshFullListDGW()
		{
			if (fullListSearchTBox.Text.Length == 0)
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
			else
			{
				var text = fullListSearchTBox.Text.ToLower();
				fullListDGW.DataSource = context.Devices.ToList()
														.Where(d => d.ToFullString().ToLower().Contains(text))
														.Select(d => new
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
			}
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
