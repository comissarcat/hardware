using Hardware.Forms;
using Hardware.Models;

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

		private void RefreshAll()
		{
			RefreshBuildings();
			RefreshCabinets();
			RefreshComplects();
		}

		private void RefreshBuildings()
		{
			RefreshBuildingsLBoxLeft();
			RefreshBuildingsLBoxRight();
		}

		private void RefreshBuildingsLBoxLeft()
		{
			var selectedItem = buildingsLBoxLeft.SelectedItem;
			buildingsLBoxLeft.DataSource = context.Buildings.ToList();
			buildingsLBoxLeft.DisplayMember = "Name";
			if (selectedItem is not null)
				if (buildingsLBoxLeft.Items.Contains(selectedItem))
					buildingsLBoxLeft.SelectedItem = selectedItem;
			SwitchEditCabinetBtnLeft();
		}

		private void RefreshBuildingsLBoxRight()
		{
			var selectedItem = buildingsLBoxRight.SelectedItem;
			buildingsLBoxRight.DataSource = context.Buildings.ToList();
			buildingsLBoxRight.DisplayMember = "Name";
			if (selectedItem is not null)
				if (buildingsLBoxRight.Items.Contains(selectedItem))
					buildingsLBoxRight.SelectedItem = selectedItem;
			SwitchEditCabinetBtnRight();
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

		private void RefreshCabinets()
		{
			RefreshCabinetsLBoxLeft();
			RefreshCabinetsLBoxRight();
		}

		private void RefreshCabinetsLBoxLeft()
		{
			var selectedItem = cabinetsLBoxLeft.SelectedItem;
			cabinetsLBoxLeft.DataSource = context.Cabinets.Where(c => c.Building == buildingsLBoxLeft.SelectedItem).ToList();
			cabinetsLBoxLeft.DisplayMember = "Name";
			if (selectedItem is not null)
				if (cabinetsLBoxLeft.Items.Contains(selectedItem))
					cabinetsLBoxLeft.SelectedItem = selectedItem;
			SwitchEditComplectBtnLeft();
			SwitchMoveCabinetBtns();
		}

		private void RefreshCabinetsLBoxRight()
		{
			var selectedItem = cabinetsLBoxRight.SelectedItem;
			cabinetsLBoxRight.DataSource = context.Cabinets.Where(c => c.Building == buildingsLBoxRight.SelectedItem).ToList();
			cabinetsLBoxRight.DisplayMember = "Name";
			if (selectedItem is not null)
				if (cabinetsLBoxRight.Items.Contains(selectedItem))
					cabinetsLBoxRight.SelectedItem = selectedItem;
			SwitchEditComplectBtnRight();
			SwitchMoveCabinetBtns();
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

		private void SwitchMoveCabinetBtns()
		{
			SwitchMoveCabinetToLeftBtn();
			SwitchMoveCabinetToRightBtn();
		}

		private void SwitchMoveCabinetToRightBtn()
		{
			if (cabinetsLBoxLeft.SelectedIndex != -1 && buildingsLBoxLeft.SelectedItem != buildingsLBoxRight.SelectedItem)
				moveCabinetToRightBtn.Enabled = true;
			else
				moveCabinetToRightBtn.Enabled = false;
		}

		private void SwitchMoveCabinetToLeftBtn()
		{
			if (cabinetsLBoxRight.SelectedIndex != -1 && buildingsLBoxLeft.SelectedItem != buildingsLBoxRight.SelectedItem)
				moveCabinetToLeftBtn.Enabled = true;
			else
				moveCabinetToLeftBtn.Enabled = false;
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
				var cabinet = (Cabinet?)cabinetsLBoxLeft.SelectedItem;
				var building = cabinet is null ? (Building)buildingsLBoxLeft.SelectedItem : cabinet.Building;
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
			Cabinet cabinet = (Cabinet)cabinetsLBoxLeft.SelectedItem;
			var before = $"{cabinet.Building.Name} -> {cabinet.Name}";
			Building building = (Building)buildingsLBoxRight.SelectedItem;
			cabinet.Building = building;
			var history = new History() { Before = before, After = $"{cabinet.Building.Name} -> {cabinet.Name}" };
			await context.History.AddAsync(history);
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
			Cabinet cabinet = (Cabinet)cabinetsLBoxRight.SelectedItem;
			var before = $"{cabinet.Building.Name} -> {cabinet.Name}";
			Building building = (Building)buildingsLBoxLeft.SelectedItem;
			cabinet.Building = building;
			var history = new History() { Before = before, After = $"{cabinet.Building.Name} -> {cabinet.Name}" };
			await context.History.AddAsync(history);
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

		private void RefreshComplects()
		{
			RefreshComplectsLBoxLeft();
			RefreshComplectsLBoxRight();
		}

		private void RefreshComplectsLBoxLeft()
		{
			var selectedItem = complectsLBoxLeft.SelectedItem;
			complectsLBoxLeft.DataSource = context.Complects.Where(c => c.Cabinet == cabinetsLBoxLeft.SelectedItem).ToList();
			complectsLBoxLeft.DisplayMember = "Name";
			if (selectedItem is not null)
				if (complectsLBoxLeft.Items.Contains(selectedItem))
					complectsLBoxLeft.SelectedItem = selectedItem;
			SwitchEditDeviceBtnLeft();
		}

		private void SwitchEditDeviceBtnLeft()
		{
			if (complectsLBoxLeft.SelectedIndex != -1)
				editDeviceBtnLeft.Enabled = true;
			else
				editDeviceBtnLeft.Enabled = false;
		}

		private void RefreshComplectsLBoxRight()
		{
			var selectedItem = complectsLBoxRight.SelectedItem;
			complectsLBoxRight.DataSource = context.Complects.Where(c => c.Cabinet == cabinetsLBoxRight.SelectedItem).ToList();
			complectsLBoxRight.DisplayMember = "Name";
			if (selectedItem is not null)
				if (complectsLBoxRight.Items.Contains(selectedItem))
					complectsLBoxRight.SelectedItem = selectedItem;
			SwitchEditDeviceBtnRight();
		}

		private void SwitchEditDeviceBtnRight()
		{
			if (complectsLBoxRight.SelectedIndex != -1)
				editDeviceBtnRight.Enabled = true;
			else
				editDeviceBtnRight.Enabled = false;
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
	}
}
