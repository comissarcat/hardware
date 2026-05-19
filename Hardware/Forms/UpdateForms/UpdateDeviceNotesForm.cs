using Hardware.Models;

namespace Hardware.Forms
{
    public partial class UpdateDeviceNotesForm : Form
    {
        private readonly Device device;
        private readonly ConfigManager configManager = new();

        public UpdateDeviceNotesForm(Device device)
        {
            InitializeComponent();
            Icon = Resources.inventarisation;
            DialogResult = DialogResult.Cancel;
            this.device = device;
            oldNotesTBox.Text = device.Notes;
        }

        private async Task<(bool result, string message)> UpdateDevice()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            bool result = false;
            try
            {
                result = await context.UpdateDevice(device, newNotesTBox.Text);
            }
            catch (Exception ex)
            {
                return (false, $"{ex.Message}\n{ex.InnerException}");
            }
            if (result)
                return (result, $"Примечание успешно изменено");
            else
                return (result, $"Не удалось изменить примечание");
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
