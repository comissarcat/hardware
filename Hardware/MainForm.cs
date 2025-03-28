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
			context = new ApplicationContext();
			InitializeButtons();
			RefreshBuildings();
			RefreshCabinets();
		}

		private void InitializeButtons()
		{
			editBuildingBtn1.Text = "Добавить\nизменить";
			editBuildingBtn2.Text = "Добавить\nизменить";
			editCabinetBtn1.Text = "Добавить\nизменить";
			editCabinetBtn2.Text = "Добавить\nизменить";
			editComplectBtn1.Text = "Добавить\nизменить";
			editComplectBtn2.Text = "Добавить\nизменить";
			editDeviceBtn1.Text = "Добавить\nизменить";
			editDeviceBtn2.Text = "Добавить\nизменить";
		}
		private void RefreshBuildings()
		{
			int b1 = buildingsLBox1.SelectedIndex;
			int b2 = buildingsLBox2.SelectedIndex;

			var buildings = context.Buildings.ToList();

			buildingsLBox1.DataSource = buildings.ToList();
			buildingsLBox1.DisplayMember = "Name";
			buildingsLBox2.DataSource = buildings.ToList();
			buildingsLBox2.DisplayMember = "Name";

			if (b1 != -1 && b1 < buildingsLBox1.Items.Count)
				buildingsLBox1.SelectedIndex = b1;
			if (b2 != -1 && b2 < buildingsLBox2.Items.Count)
				buildingsLBox2.SelectedIndex = b2;
		}

		private void CheckEditButtonsEnabling()
		{
			if (buildingsLBox1.SelectedIndex != -1)
			{
				editCabinetBtn1.Enabled = true;
				cabinetsLBox1.DataSource = context.Cabinets.Where(c => c.Building == buildingsLBox1.SelectedItem).ToList();
				cabinetsLBox1.DisplayMember = "Name";
			}
			else
				editCabinetBtn1.Enabled = false;

			if (buildingsLBox2.SelectedIndex != -1)
			{
				editCabinetBtn2.Enabled = true;
				cabinetsLBox2.DataSource = context.Cabinets.Where(c => c.Building == buildingsLBox2.SelectedItem).ToList();
				cabinetsLBox2.DisplayMember = "Name";
			}
			else
				editCabinetBtn2.Enabled = false;

			if (cabinetsLBox1.SelectedIndex != -1)
			{
				editComplectBtn1.Enabled = true;
				complectsLBox1.DataSource = context.Complects.Where(c => c.Cabinet == complectsLBox1.SelectedItem).ToList();
				complectsLBox1.DisplayMember = "Name";
			}
			else
				editComplectBtn1.Enabled = false;

			if (cabinetsLBox2.SelectedIndex != -1)
			{
				editComplectBtn2.Enabled = true;
				complectsLBox2.DataSource = context.Complects.Where(c => c.Cabinet == complectsLBox2.SelectedItem).ToList();
				complectsLBox2.DisplayMember = "Name";
			}
			else
				editComplectBtn2.Enabled = false;
		}

		private async void editBuildingBtn1_Click(object sender, EventArgs e)
		{
			Building? building = null;
			if (buildingsLBox1.SelectedIndex != -1)
				building = (Building)buildingsLBox1.SelectedItem;
			var form = new EditBuildingForm(context, building);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshBuildings();
		}

		private async void editBuildingBtn2_Click(object sender, EventArgs e)
		{
			Building? building = null;
			if (buildingsLBox2.SelectedIndex != -1)
				building = (Building)buildingsLBox2.SelectedItem;
			var form = new EditBuildingForm(context, building);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshBuildings();
		}

		private void buildingsLBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			CheckEditButtonsEnabling();
			CheckMoveCabinetsPossibility();
		}

		private void buildingsLBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			CheckEditButtonsEnabling();
			CheckMoveCabinetsPossibility();
		}

		private void RefreshCabinets()
		{
			int c1 = cabinetsLBox1.SelectedIndex;
			int c2 = cabinetsLBox2.SelectedIndex;

			var cabinets = context.Cabinets.ToList();

			if (buildingsLBox1.SelectedIndex != -1)
			{
				cabinetsLBox1.DataSource = cabinets.Where(c => c.Building == buildingsLBox1.SelectedItem).ToList();
				cabinetsLBox1.DisplayMember = "Name";
			}
			if (buildingsLBox2.SelectedIndex != -1)
			{
				cabinetsLBox2.DataSource = cabinets.Where(c => c.Building == buildingsLBox2.SelectedItem).ToList();
				cabinetsLBox2.DisplayMember = "Name";
			}

			if (c1 != -1 && c1 < cabinetsLBox1.Items.Count)
				cabinetsLBox1.SelectedIndex = c1;
			if (c2 != -1 && c2 < cabinetsLBox2.Items.Count)
				cabinetsLBox2.SelectedIndex = c2;
		}

		private void editCabinetBtn1_Click(object sender, EventArgs e)
		{
			Cabinet? cabinet = null;
			Building building = (Building)buildingsLBox1.SelectedItem;
			if (cabinetsLBox1.SelectedIndex != -1)
				cabinet = (Cabinet)cabinetsLBox1.SelectedItem;
			var form = new EditCabinetForm(context, cabinet, building);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshCabinets();
		}

		private void editCabinetBtn2_Click(object sender, EventArgs e)
		{
			Cabinet? cabinet = null;
			Building building = (Building)buildingsLBox2.SelectedItem;
			if (cabinetsLBox2.SelectedIndex != -1)
				cabinet = (Cabinet)cabinetsLBox2.SelectedItem;
			var form = new EditCabinetForm(context, cabinet, building);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshCabinets();
		}

		private void cabinetsLBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			CheckEditButtonsEnabling();
			CheckMoveCabinetsPossibility();
		}

		private void cabinetsLBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			CheckEditButtonsEnabling();
			CheckMoveCabinetsPossibility();
		}

		private void CheckMoveCabinetsPossibility()
		{
			if (cabinetsLBox1.SelectedIndex != -1 && buildingsLBox1.SelectedIndex != buildingsLBox2.SelectedIndex)
				moveCabinetToRightBtn.Enabled = true;
			else
				moveCabinetToRightBtn.Enabled = false;
			if (cabinetsLBox2.SelectedIndex != -1 && buildingsLBox1.SelectedIndex != buildingsLBox2.SelectedIndex)
				moveCabinetToLeftBtn.Enabled = true;
			else
				moveCabinetToLeftBtn.Enabled = false;
		}

		private async void moveCabinetToRightBtn_Click(object sender, EventArgs e)
		{
			Cabinet cabinet = (Cabinet)cabinetsLBox1.SelectedItem;
			var before = $"{cabinet.Building.Name} -> {cabinet.Name}";
			Building building = (Building)buildingsLBox2.SelectedItem;
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
				RefreshCabinets();
		}

		private async void moveCabinetToLeftBtn_Click(object sender, EventArgs e)
		{
			Cabinet cabinet = (Cabinet)cabinetsLBox2.SelectedItem;
			var before = $"{cabinet.Building.Name} -> {cabinet.Name}";
			Building building = (Building)buildingsLBox1.SelectedItem;
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
				RefreshCabinets();
		}



		private void RefreshComplects()
		{
			int c1 = complectsLBox1.SelectedIndex;
			int c2 = complectsLBox2.SelectedIndex;

			var complects = context.Complects.ToList();

			if (cabinetsLBox1.SelectedIndex != -1)
			{
				complectsLBox1.DataSource = complects.Where(c => c.Cabinet == cabinetsLBox1.SelectedItem).ToList();
				complectsLBox1.DisplayMember = "Name";
			}
			if (cabinetsLBox2.SelectedIndex != -1)
			{
				complectsLBox2.DataSource = complects.Where(c => c.Cabinet == cabinetsLBox2.SelectedItem).ToList();
				complectsLBox2.DisplayMember = "Name";
			}

			if (c1 != -1 && c1 < complectsLBox1.Items.Count)
				complectsLBox1.SelectedIndex = c1;
			if (c2 != -1 && c2 < complectsLBox2.Items.Count)
				complectsLBox2.SelectedIndex = c2;
		}

		private void editComplectBtn1_Click(object sender, EventArgs e)
		{
			Complect? complect = null;
			Cabinet cabinet = (Cabinet)cabinetsLBox1.SelectedItem;
			if (complectsLBox1.SelectedIndex != -1)
				complect = (Complect)complectsLBox1.SelectedItem;
			var form = new EditComplectForm(context, complect, cabinet);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshComplects();
		}

		private void editComplectBtn2_Click(object sender, EventArgs e)
		{
			Complect? complect = null;
			Cabinet cabinet = (Cabinet)cabinetsLBox2.SelectedItem;
			if (complectsLBox2.SelectedIndex != -1)
				complect = (Complect)complectsLBox2.SelectedItem;
			var form = new EditComplectForm(context, complect, cabinet);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshComplects();
		}
	}
}
