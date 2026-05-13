using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware.Forms
{
    public partial class EditComplectForm : Form
    {
        Complect? complect;
        private readonly ConfigManager configManager = new();
        public EditComplectForm(Complect? complect, Cabinet cabinet)
        {
            InitializeComponent();
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            DialogResult = DialogResult.Cancel;
            buildingCBox.DataSource = context.Buildings.OrderBy(b => b.Name)
                                                       .Include(b => b.Cabinets)
                                                       .ToList();
            this.complect = complect;
            if (this.complect is not null)
            {
                idTBox.Text = this.complect.Id.ToString();
                nameTBox.Text = this.complect.Name;

                buildingCBox.SelectedItem = this.complect.Cabinet.Building;

                cabinetCBox.DataSource = (buildingCBox.SelectedItem as Building).Cabinets.OrderBy(c => c.Name).ToList();
                cabinetCBox.SelectedItem = this.complect.Cabinet;

                editBtn.Enabled = true;
                SwitchRemoveBtn();
            }
            else
            {
                buildingCBox.SelectedItem = cabinet.Building;
                cabinetCBox.DataSource = (buildingCBox.SelectedItem as Building).Cabinets.OrderBy(c => c.Name).ToList();
                cabinetCBox.SelectedItem = cabinet;
            }
        }

        private void SwitchRemoveBtn()
        {
            bool complectHasDevices = complect.Devices.Count != 0;
            removeBtn.Enabled = !complectHasDevices;
        }

        private void SwitchEditCabinetBtn()
        {
            if (buildingCBox.SelectedIndex != -1)
                editCabinetBtn.Enabled = true;
            else
                editCabinetBtn.Enabled = false;
        }

        private void RefreshAll()
        {
            RefreshBuildings();
            RefreshCabinets();
        }

        private void RefreshBuildings()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            object? selectedItem = buildingCBox.SelectedItem;

            buildingCBox.DataSource = context.Buildings.OrderBy(b => b.Name)
                                                       .Include(b => b.Cabinets)
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

            cabinetCBox.DataSource = (buildingCBox.SelectedItem as Building).Cabinets.OrderBy(c => c.Name).ToList();

            if (cabinetCBox.Items.Count == 0)
                cabinetCBox.Text = string.Empty;
            else if (selectedItem is not null)
                if (cabinetCBox.Items.Contains(selectedItem))
                    cabinetCBox.SelectedItem = selectedItem;
        }

        private async Task<string?> AddComplect()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            Cabinet? cabinet = cabinetCBox.SelectedItem as Cabinet;
            cabinet = await context.Cabinets.FindAsync(cabinet.Id);
            complect = new() { Name = nameTBox.Text, Cabinet = cabinet };
            await context.Complects.AddAsync(complect);
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

        private async Task<string?> EditComplect()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            complect = await context.Complects.FindAsync(complect.Id);
            List<string> before = [];
            List<string> after = [];
            List<Device> devicesInComplect = complect?.Devices.ToList() ?? [];
            if (devicesInComplect.Count > 0)
            {
                foreach (Device device in devicesInComplect)
                    before.Add(device.ToStringForHistory());

                complect.Name = nameTBox.Text;
                Cabinet? cabinet = cabinetCBox.SelectedItem as Cabinet;
                cabinet = await context.Cabinets.FindAsync(cabinet.Id);
                complect.Cabinet = cabinet;

                foreach (Device device in devicesInComplect)
                    after.Add(device.ToStringForHistory());

                for (int i = 0; i < devicesInComplect.Count; i++)
                    await context.History.AddAsync(new History() { Before = before[i], After = after[i] });
            }
            else
            {
                complect.Name = nameTBox.Text;
                Cabinet? cabinet = cabinetCBox.SelectedItem as Cabinet;
                cabinet = await context.Cabinets.FindAsync(cabinet.Id);
                complect.Cabinet = cabinet;
            }
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

        private async Task<string?> RemoveComplect()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            complect = await context.Complects.FindAsync(complect.Id);
            context.Complects.Remove(complect);
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
            string? result = await AddComplect();
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
            string? result = await EditComplect();
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
            string? result = await RemoveComplect();
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
                RefreshAll();
        }

        private void editCabinetBtn_Click(object sender, EventArgs e)
        {
            Cabinet? cabinet = (Cabinet?)cabinetCBox.SelectedItem;
            Building? building = cabinet is null ? (Building)buildingCBox.SelectedItem : cabinet.Building;
            EditCabinetForm form = new(cabinet, building);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
                RefreshAll();
        }

        private void buildingCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshCabinets();
        }
    }
}
