using Hardware.Models;

namespace Hardware.Forms
{
    public partial class UpdateEntityForm : Form
    {
        private readonly object entity;
        private readonly ConfigManager configManager = new();

        public UpdateEntityForm(object entity)
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            this.entity = entity;

            Action action = this.entity switch
            {
                Building item => () =>
                {
                    Text = "Редактирование здания";
                    idTBox.Text = item.Id.ToString();
                    oldNameTBox.Text = item.Name;
                }
                ,
                Cabinet item => () =>
                {
                    Text = "Редактирование кабинета";
                    idTBox.Text = item.Id.ToString();
                    oldNameTBox.Text = item.Name;
                }
                ,
                Complect item => () =>
                {
                    Text = "Редактирование комплекта";
                    idTBox.Text = item.Id.ToString();
                    oldNameTBox.Text = item.Name;
                }
                ,
                DeviceType item => () =>
                {
                    Text = "Редактирование типа техники";
                    idTBox.Text = item.Id.ToString();
                    oldNameTBox.Text = item.Name;
                }
                ,
                DeviceName item => () =>
                {
                    Text = "Редактирование названия техники";
                    idTBox.Text = item.Id.ToString();
                    oldNameTBox.Text = item.Name;
                }
                ,
                DeviceProvider item => () =>
                {
                    Text = "Редактирование поставщика техники";
                    idTBox.Text = item.Id.ToString();
                    oldNameTBox.Text = item.Name;
                }
                ,
                _ => () =>
                {
                    Text = "Ошибка";
                    idTBox.Text = "Ошибка";
                    oldNameTBox.Text = "Ошибка";
                }
            };

            action();
        }

        private async Task<(bool result, string message)> UpdateCabinet()
        {
            if (newNameTBox.Text == string.Empty)
                return (false, "Название не может быть пустым");

            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            bool result = false;
            try
            {
                result = await context.UpdateEntity(entity, newNameTBox.Text);
            }
            catch (Exception ex)
            {
                return (false, $"{ex.Message}\n{ex.InnerException}");
            }
            if (result)
                return (result, $"Название успешно изменено с {oldNameTBox.Text} на {newNameTBox.Text}");
            else
                return (result, $"Название \"{newNameTBox.Text}\" занято");
        }

        private async void UpdateBtn_Click(object sender, EventArgs e)
        {
            (bool result, string message) = await UpdateCabinet();
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
