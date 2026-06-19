using Hardware.Models;

namespace Hardware.Forms
{
    public partial class CreateEntityWithParentForm : Form
    {
        private readonly ConfigManager configManager = new();
        private readonly object parent;

        public CreateEntityWithParentForm(object parent)
        {
            InitializeComponent();
            Icon = Resources.inventarisation;
            DialogResult = DialogResult.Cancel;
            this.parent = parent;

            Action action = this.parent switch
            {
                Building item => () =>
                {
                    Text = "Добавление кабинета";
                    parentTBox.Text = item.Name;
                    parentTBox.Text = "Здание";
                }
                ,
                Cabinet item => () =>
                {
                    Text = "Добавление комплекта";
                    parentTBox.Text = item.Name;
                    parentTBox.Text = "Кабинет";
                }
                ,
                DeviceType item => () =>
                {
                    Text = "Добавление названия техники";
                    parentTBox.Text = item.Name;
                    parentTBox.Text = "Тип устройства";
                }
                ,
                _ => () =>
                {
                    Text = "Ошибка";
                    parentTBox.Text = "Ошибка";
                }
            };

            action();
        }

        private async Task<(bool result, string message)> CreateEntity()
        {
            if (nameTBox.Text == string.Empty)
                return (false, "Название не может быть пустым");

            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            bool result = false;
            try
            {
                result = await context.CreateEntity(parent, nameTBox.Text);
            }
            catch (Exception ex)
            {
                return (false, $"{ex.Message}\n{ex.InnerException}");
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
