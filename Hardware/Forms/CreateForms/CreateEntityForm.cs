using Hardware.Models;

namespace Hardware.Forms
{
    public partial class CreateEntityForm : Form
    {
        private readonly ConfigManager configManager = new();
        private readonly Type entityType;

        public CreateEntityForm(Type entityType)
        {
            InitializeComponent();
            Icon = Resources.inventarisation;
            DialogResult = DialogResult.Cancel;
            this.entityType = entityType;

            if (this.entityType == typeof(Building))
                base.Text = "Добавление здания";
            else if (this.entityType == typeof(DeviceType))
                base.Text = "Добавление типа техники";
            else if (this.entityType == typeof(DeviceProvider))
                base.Text = "Добавление поставщика техники";
        }

        private async Task<(bool result, string message)> CreateEntity()
        {
            if (nameTBox.Text == string.Empty)
                return (false, "Название не может быть пустым");

            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            bool result = false;
            try
            {
                result = await context.CreateEntity(entityType, nameTBox.Text);
            }
            catch (Exception ex)
            {
                return (result, $"{ex.Message}\n{ex.InnerException}");
            }
            if (result)
                return (result, $"\"{nameTBox.Text}\" успешно добавлено");
            else
                return (result, $"\"{nameTBox.Text}\" уже существует");
        }

        private async void CreateBtn_Click(object sender, EventArgs e)
        {
            (bool result, string message) = await CreateEntity();
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
