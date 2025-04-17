using Microsoft.EntityFrameworkCore;

namespace Hardware.Forms
{
	public partial class SettingsForm : Form
	{
		ConfigManager configManager;
		public SettingsForm()
		{
			InitializeComponent();
			configManager = new ConfigManager();
			InitTBoxes();
		}

		private void InitTBoxes()
		{
			var config = configManager.GetConfig();
			serverTBox.Text = config.Server;
			userTBox.Text = config.User;
			passwordTBox.Text = config.Password;
			databaseTBox.Text = config.Database;
		}

		private void saveBtn_Click(object sender, EventArgs e)
		{
			configManager.SetConfig(serverTBox.Text, userTBox.Text, passwordTBox.Text, databaseTBox.Text);
			MessageBox.Show("Успшно сохранено", "Сохранено", MessageBoxButtons.OK);
		}

		private void testBtn_Click(object sender, EventArgs e)
		{
			var context = ApplicationContext.RecreateInstance();
			try
			{
				context.Database.OpenConnection();
				context.Database.CloseConnection();
				MessageBox.Show("Успех!", "Соединение с базой данных установлено!", MessageBoxButtons.OK);
			}
			catch
			{
				MessageBox.Show("А нихера", "Нет соединения с базой данных", MessageBoxButtons.OK);
			}
		}
	}
}
