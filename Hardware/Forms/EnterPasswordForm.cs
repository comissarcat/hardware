using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware.Forms
{
    public partial class EnterPasswordForm : Form
    {
        private readonly ConfigManager configManager = new();
        public Repairman? Repairman { get; private set; }
        public EnterPasswordForm()
        {
            InitializeComponent();
            Icon = Resources.inventarisation;
            DialogResult = DialogResult.Cancel;
            Load += (sender, e) => { LoadRepairmen(); };
            refreshRepairmenBtn.Click += (sender, e) => { LoadRepairmen(); };
            repairmanCBox.SelectedIndexChanged += (sender, e) =>
            {
                Repairman = repairmanCBox.SelectedItem as Repairman;
            };
        }

        private bool CheckPassword()
        {
            if (passwordTBox.Text == "aN271828")
                return true;
            else
                return false;
        }

        private async void LoadRepairmen()
        {
            repairmanCBox.DataSource = new List<Repairman>();
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            try
            {
                repairmanCBox.DataSource = await context.Repairmen.OrderBy(r => r.Name).ToListAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\n{ex.InnerException}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Repairman = repairmanCBox.SelectedItem as Repairman;
            }
        }

        private void EnterBtn_Click(object sender, EventArgs e)
        {
            if (CheckPassword())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Неверный пароль", "Неверный пароль", MessageBoxButtons.OK);
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ConfigBtn_Click(object sender, EventArgs e)
        {
            SettingsForm form = new();
            form.ShowDialog();
        }
    }
}
