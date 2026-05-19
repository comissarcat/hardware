using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware.Forms
{
    public partial class UpdateDeviceForm : Form
    {
        private readonly Device device;
        private readonly ConfigManager configManager = new();

        public UpdateDeviceForm(Device device)
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            this.device = device;
            oldSerialTBox.Text = device.Serial;
            oldInventoryTBox.Text = device.Inventory;
        }

        private async Task<(bool result, string message)> UpdateDevice()
        {
            if (newSerialTBox.Text == string.Empty)
                return (false, "Серийный номер не может быть пустым");

            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            bool result = false;
            try
            {
                result = await context.UpdateDevice(device, newSerialTBox.Text, newInventoryTBox.Text);
            }
            catch (Exception ex)
            {
                return (false, $"{ex.Message}\n{ex.InnerException}");
            }
            if (result)
                return (result, $"\"{oldSerialTBox.Text}\" успешно изменён");
            else
                return (result, $"\"{oldSerialTBox.Text}\" не удалось изменить");
        }

        private async void UpdateBtn_Click(object sender, EventArgs e)
        {
            (bool result, string message) = await UpdateDevice();
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
