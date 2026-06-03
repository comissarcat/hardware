using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware.Forms
{
    public partial class RecordRepairOperationForm : Form
    {
        private Repairman repairman;
        private Device device;
        private readonly ConfigManager configManager = new();

        public RecordRepairOperationForm(Repairman repairman, Device device)
        {
            InitializeComponent();
            Icon = Resources.inventarisation;
            DialogResult = DialogResult.Cancel;

            Load += (sender, e) => { OnLoad(repairman, device); };
            okBtn.Click += (sender, e) => { Ok_Click(); };
            cancelBtn.Click += (sender, e) => { Close(); };
        }

        private async void OnLoad(Repairman repairman, Device device)
        {
            ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            this.repairman = repairman;
            this.device = await context.Devices.Include(d => d.DeviceName).FirstOrDefaultAsync(d => d.Id == device.Id);

            repairmanTBox.Text = this.repairman.Name;
            deviceTBox.Text = this.device.ToString();

            dateTimePicker.Value = DateTime.Now;

            repairOperationsCBox.DataSource = await context.RepairOperations.OrderBy(r => r.Name).ToListAsync();
            repairOperationsCBox.SelectedIndex = -1;
            if (repairOperationsCBox.Items.Count == 0)
            {
                MessageBox.Show("Нет ни одной доступной операции", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
        }

        private async Task<(bool result, string message)> Save()
        {
            if (repairOperationsCBox.SelectedIndex == -1)
                return (false, "Не выбрана никакая операция");
            ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            bool result = false;
            try
            {
                result = await context.CreateCompletedRepairOperation(device,
                                                                      repairman,
                                                                      repairOperationsCBox.SelectedItem as RepairOperation,
                                                                      DateOnly.FromDateTime(dateTimePicker.Value),
                                                                      notesTBox.Text);
            }
            catch (Exception ex)
            {
                return (false, $"{ex.Message}\n{ex.InnerException}");
            }
            if (result)
                return (result, $"\"{(repairOperationsCBox.SelectedItem as RepairOperation).Name}\" успешно записано");
            else
                return (result, $"\"{(repairOperationsCBox.SelectedItem as RepairOperation).Name}\" не удалось записать");
        }

        private async void Ok_Click()
        {
            (bool result, string message) = await Save();
            if (result == false)
                MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK);
            else
            {
                MessageBox.Show(message, "Успех", MessageBoxButtons.OK);
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
