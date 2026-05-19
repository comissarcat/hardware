using Hardware.Models;

namespace Hardware.Forms
{
    public partial class EditDeviceTypeForm : Form
    {
        private readonly DeviceType deviceType;
        private readonly ConfigManager configManager = new();

        public EditDeviceTypeForm(DeviceType deviceType)
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            this.deviceType = deviceType;
            idTBox.Text = this.deviceType.Id.ToString();
            nameTBox.Text = this.deviceType.Name;
        }

        private async Task<(bool result, string message)> UpdateDeviceType()
        {
            if (nameTBox.Text == string.Empty)
                return (false, "Название не может быть пустым");

            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            string oldName = deviceType.Name;
            bool result = false;
            try
            {
                result = await context.UpdateDeviceType(deviceType, nameTBox.Text);
            }
            catch (Exception ex)
            {
                return (result, $"{ex.Message}\n{ex.InnerException}");
            }
            if (result)
                return (result, $"Название успешно изменено с {oldName} на {nameTBox.Text}");
            else
                return (result, $"Тип техники с названием \"{nameTBox.Text}\" уже есть");
        }

        private async void UpdateBtn_Click(object sender, EventArgs e)
        {
            (bool result, string message) = await UpdateDeviceType();
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
