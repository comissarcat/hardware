namespace Hardware.Forms
{
	public partial class EnterPasswordForm : Form
	{
		public EnterPasswordForm()
		{
			InitializeComponent();
			DialogResult = DialogResult.Cancel;
		}

		private bool CheckPassword()
		{
			if (passwordTBox.Text == "aN271828")
				return true;
			else
				return false;
		}

		private void enterBtn_Click(object sender, EventArgs e)
		{
			if (CheckPassword())
			{
				DialogResult = DialogResult.OK;
				Close();
			}
			else
				MessageBox.Show("Неверный пароль", "Неверный пароль", MessageBoxButtons.OK);
		}

		private void cancelBtn_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void configBtn_Click(object sender, EventArgs e)
		{
			if (CheckPassword())
			{
				var form = new SettingsForm();
				form.ShowDialog();
			}
			else
				MessageBox.Show("Неверный пароль", "Неверный пароль", MessageBoxButtons.OK);
		}
	}
}
