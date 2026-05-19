using Microsoft.EntityFrameworkCore;

namespace Hardware.Forms
{
    public partial class SettingsForm : Form
    {
        private readonly ConfigManager configManager;
        public SettingsForm()
        {
            InitializeComponent();
            Icon = Resources.connection;
            configManager = new ConfigManager();
            InitTBoxes();
        }

        private void InitTBoxes()
        {
            Models.Config config = configManager.GetConfig();
            serverTBox.Text = config.Server;
            userTBox.Text = config.User;
            passwordTBox.Text = config.Password;
            databaseTBox.Text = config.Database;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                configManager.SetConfig(serverTBox.Text, userTBox.Text, passwordTBox.Text, databaseTBox.Text);
                MessageBox.Show("Успешно сохранено", "Сохранено", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Не сохранено", MessageBoxButtons.OK);
            }
        }

        private void testBtn_Click(object sender, EventArgs e)
        {
            try
            {
                configManager.SetConfig(serverTBox.Text, userTBox.Text, passwordTBox.Text, databaseTBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Конфигурация не применена", MessageBoxButtons.OK);
                return;
            }
            try
            {
                using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
                context.Database.OpenConnection();
                context.Database.CloseConnection();
                MessageBox.Show("Успех!", "Соединение с базой данных установлено!", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Нет соединения с базой данных", MessageBoxButtons.OK);
            }
        }
    }
}
