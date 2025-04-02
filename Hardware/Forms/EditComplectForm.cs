using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware.Forms
{
	public partial class EditComplectForm : Form
	{
		readonly ApplicationContext context;
		Complect? complect;
		public EditComplectForm(Complect? complect, Cabinet cabinet)
		{
			InitializeComponent();
			context = ApplicationContext.Instanse();
			DialogResult = DialogResult.Cancel;
			buildingCBox.DataSource = context.Buildings.ToList();
			buildingCBox.DisplayMember = "Name";
			this.complect = complect;
			if (this.complect is not null)
			{
				idTBox.Text = this.complect.Id.ToString();
				nameTBox.Text = this.complect.Name;
				editBtn.Enabled = true;
				cabinet = complect.Cabinet;
				cabinetCBox.DataSource = context.Cabinets.Where(c => c.Building == complect.Cabinet.Building).ToList();
				var complectDevices = context.Devices.Where(d => d.Complect == complect).ToList();
				if (complectDevices.Count == 0)
					removeBtn.Enabled = true;
			}
			else
				cabinetCBox.DataSource = context.Cabinets.Where(c => c.Building == cabinet.Building).ToList();
			cabinetCBox.DisplayMember = "Name";
			buildingCBox.SelectedItem = cabinet.Building;
			cabinetCBox.SelectedItem = cabinet;
		}

		private void RefreshAll()
		{
			RefreshBuildings();
			RefreshCabinets();
		}

		private void RefreshBuildings()
		{
			int b = buildingCBox.SelectedIndex;

			buildingCBox.DataSource = context.Buildings.ToList();
			buildingCBox.DisplayMember = "Name";

			if (b != -1 && b > buildingCBox.Items.Count)
				buildingCBox.SelectedIndex = b;
		}

		private void RefreshCabinets()
		{
			int c = cabinetCBox.SelectedIndex;

			cabinetCBox.DataSource = context.Cabinets.Where(c => c.Building == buildingCBox.SelectedItem).ToList();
			cabinetCBox.DisplayMember = "Name";

			if (c != -1 && c > cabinetCBox.Items.Count)
				cabinetCBox.SelectedIndex = c;
		}

		private async Task<bool> AddComplect()
		{
			complect = new() { Name = nameTBox.Text, Cabinet = (Cabinet)cabinetCBox.SelectedItem };
			await context.Complects.AddAsync(complect);
			try
			{
				await context.SaveChangesAsync();
			}
			catch
			{
				return false;
			}
			return true;
		}

		private async Task<bool> EditComplect()
		{
			List<string> b = [];
			List<string> a = [];
			var complectDevices = await context.Devices.Include(d => d.Complect.Cabinet.Building).Where(d => d.Complect == complect).ToListAsync();
			if (complectDevices.Count > 0)
			{
				foreach (var device in complectDevices)
					b.Add($"{device.Complect.Cabinet.Building.Name} -> {device.Complect.Cabinet.Name} -> {device.Complect.Name} -> {device.DeviceName} {device.Serial} {device.Inventory}");
				complect.Name = nameTBox.Text;
				complect.Cabinet = (Cabinet)cabinetCBox.SelectedItem;
				foreach (var device in complectDevices)
					a.Add($"{device.Complect.Cabinet.Building.Name} -> {device.Complect.Cabinet.Name} -> {device.Complect.Name} -> {device.DeviceName} {device.Serial} {device.Inventory}");
				for (int i = 0; i < complectDevices.Count; i++)
					await context.History.AddAsync(new History() { Before = b[i], After = a[i] });
				try
				{
					await context.SaveChangesAsync();
				}
				catch
				{
					return false;
				}
			}
			return true;
		}

		private async Task<bool> RemoveComplect()
		{
			context.Complects.Remove(complect);
			try
			{
				await context.SaveChangesAsync();
			}
			catch
			{
				return false;
			}
			return true;
		}

		private async void addBtn_Click(object sender, EventArgs e)
		{
			if (!await AddComplect())
				MessageBox.Show("А нихера", "Ошибка", MessageBoxButtons.OK);
			else
			{
				MessageBox.Show("Успешно добавлено", "Успех", MessageBoxButtons.OK);
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		private async void editBtn_Click(object sender, EventArgs e)
		{
			if (!await EditComplect())
				MessageBox.Show("А нихера", "Ошибка", MessageBoxButtons.OK);
			else
			{
				MessageBox.Show("Успешно изменено", "Успех", MessageBoxButtons.OK);
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		private async void removeBtn_Click(object sender, EventArgs e)
		{
			if (!await RemoveComplect())
				MessageBox.Show("А нихера", "Ошибка", MessageBoxButtons.OK);
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
			var building = (Building)buildingCBox.SelectedItem;
			var form = new EditBuildingForm(building);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshAll();
		}

		private void editCabinetBtn_Click(object sender, EventArgs e)
		{
			var cabinet = (Cabinet?)cabinetCBox.SelectedItem;
			var building = cabinet is null ? (Building)buildingCBox.SelectedItem : cabinet.Building;
			var form = new EditCabinetForm(cabinet, building);
			var result = form.ShowDialog();
			if (result == DialogResult.OK)
				RefreshAll();
		}

		private void buildingCBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			cabinetCBox.DataSource = context.Cabinets.Where(c => c.Building == buildingCBox.SelectedItem).ToList();
			cabinetCBox.DisplayMember = "Name";
		}
	}
}
