using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware.Forms
{
    public partial class CreateDeviceForm : Form
    {
        private readonly Complect complect;
        private readonly ConfigManager configManager = new();

        public CreateDeviceForm(Complect complect)
        {
            InitializeComponent();
            Icon = Resources.inventarisation;
            DialogResult = DialogResult.Cancel;
            this.complect = complect;
            Load += async (sender, e) => { OnLoad(); };
        }

        private async void OnLoad()
        {
            ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            complectTBox.Text = complect.Name;
            deviceNameCBox.DataSource = await context.ReadDeviceNames();
            deviceProviderCBox.DataSource = await context.DeviceProviders.OrderBy(dp => dp.Name)
                                                                         .AsNoTracking()
                                                                         .ToListAsync();
            if (deviceNameCBox.Items.Count == 0 || deviceProviderCBox.Items.Count == 0)
            {
                MessageBox.Show("Нет ни одного наименования или предоставителя техники! Добавить технику невозможно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
        }

        private async Task<(bool result, string message)> CreateDevice()
        {
            if (serialTBox.Text == string.Empty)
                return (false, "Серийный номер не может быть пустым");

            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            bool result = false;
            try
            {
                result = await context.CreateDevice(complect,
                                                    deviceProviderCBox.SelectedItem as DeviceProvider,
                                                    deviceNameCBox.SelectedItem as DeviceName,
                                                    serialTBox.Text,
                                                    inventoryTBox.Text,
                                                    notesTBox.Text);
            }
            catch (Exception ex)
            {
                return (false, $"{ex.Message}\n{ex.InnerException}");
            }
            if (result)
                return (result, $"\"{serialTBox.Text}\" успешно добавлено");
            else
                return (result, $"\"{serialTBox.Text}\" уже существует");
        }

        private async void CreateBtn_Click(object sender, EventArgs e)
        {
            (bool result, string message) = await CreateDevice();
            if (result == false)
                MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK);
            else
            {
                MessageBox.Show(message, "Успех", MessageBoxButtons.OK);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
