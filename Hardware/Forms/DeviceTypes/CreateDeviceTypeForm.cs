using Hardware.Models;

namespace Hardware.Forms
{
    public partial class CreateDeviceTypeForm : Form
    {
        private readonly ConfigManager configManager = new();
        public CreateDeviceTypeForm(DeviceType? deviceType)
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        private async Task<(bool result, string message)> CreateDeviceType()
        {
            if (nameTBox.Text == string.Empty)
                return (false, "Название не может быть пустым");

            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            bool result = false;
            try
            {
                result = await context.CreateDeviceType(nameTBox.Text);
            }
            catch (Exception ex)
            {
                return (result, $"{ex.Message}\n{ex.InnerException}");
            }
            if (result)
                return (result, $"Тип техники \"{nameTBox.Text}\" успешно добавлен");
            else
                return (result, $"Тип техники с названием \"{nameTBox.Text}\" уже есть");
        }

        private async void CreateBtn_Click(object sender, EventArgs e)
        {
            (bool result, string message) = await CreateDeviceType();
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
