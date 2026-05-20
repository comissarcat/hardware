using Hardware.Forms;
using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware.UserControls
{
    public partial class RepairmenAndOperationsTabPage : UserControl
    {
        protected ListBox repairmenLBox;
        protected ListBox repairOperationsLBox;

        private readonly ConfigManager configManager = new();
        private ContextMenuStrip contextMenu;

        public RepairmenAndOperationsTabPage()
        {
            InitializeBaseComponents();
            LoadData();
        }

        private void InitializeBaseComponents()
        {
            TableLayoutPanel mainTLP = new()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                ColumnCount = 3,
                ColumnStyles =
                {
                    new(SizeType.Percent, 40),
                    new(SizeType.Percent, 20),
                    new(SizeType.Percent, 40)
                },
                RowCount = 2,
                RowStyles =
                {
                    new(SizeType.AutoSize),
                    new(SizeType.Percent, 100)
                }
            };

            mainTLP.Controls.Add(new Label()
            {
                Anchor = AnchorStyles.None,
                AutoSize = true,
                Text = "Ремонтники",
                TextAlign = ContentAlignment.TopCenter
            }, 0, 0);
            repairmenLBox = new() { Dock = DockStyle.Fill };
            mainTLP.Controls.Add(repairmenLBox, 0, 1);

            mainTLP.Controls.Add(new Label()
            {
                Anchor = AnchorStyles.None,
                AutoSize = true,
                Text = "Операции по ремонту",
                TextAlign = ContentAlignment.TopCenter
            }, 2, 0);
            repairOperationsLBox = new() { Dock = DockStyle.Fill };
            mainTLP.Controls.Add(repairOperationsLBox, 2, 1);

            Controls.Add(mainTLP);

            CreateContextMenu();

            repairmenLBox.ContextMenuStrip = contextMenu;
            repairOperationsLBox.ContextMenuStrip = contextMenu;

            repairmenLBox.MouseDown += (sender, e) => { LBoxMouseClick(sender, e); };
            repairOperationsLBox.MouseDown += (sender, e) => { LBoxMouseClick(sender, e); };
        }

        private void LBoxMouseClick(object sender, MouseEventArgs e)
        {
            if (sender is ListBox listBox)
                if (e.Button == MouseButtons.Left)
                {
                    int index = listBox.IndexFromPoint(e.Location);
                    if (index == ListBox.NoMatches)
                        listBox.ClearSelected();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    int index = listBox.IndexFromPoint(e.Location);
                    if (index != ListBox.NoMatches)
                        listBox.SelectedIndex = index;
                    else
                        listBox.ClearSelected();
                    UpdateContextMenu(listBox);
                }
        }

        private void CreateContextMenu()
        {
            contextMenu = new();
        }

        private void UpdateContextMenu(ListBox listBox)
        {
            contextMenu.Items.Clear();

            ToolStripMenuItem menuRead = new("Обновить");
            menuRead.Click += (sender, e) => { LoadData(); };

            ToolStripMenuItem menuCreate = new("Добавить");
            menuCreate.Click += (sender, e) =>
            {
                if (listBox == repairmenLBox)
                    CreateEntity(typeof(Repairman));
                else
                    CreateEntity(typeof(RepairOperation));
            };

            ToolStripMenuItem menuUpdate = new("Изменить");
            if (listBox.SelectedIndex == -1)
                menuUpdate.Enabled = false;
            else
                menuUpdate.Click += (sender, e) => { UpdateEntity(listBox.SelectedItem); };

            ToolStripMenuItem menuDelete = new("Удалить");
            if (listBox.SelectedIndex == -1)
                menuDelete.Enabled = false;
            else
                menuDelete.Click += (sender, e) => { DeleteEntity(listBox.SelectedItem); };

            contextMenu.Items.Add(menuRead);
            contextMenu.Items.Add(menuCreate);
            contextMenu.Items.Add(menuUpdate);
            contextMenu.Items.Add(menuDelete);
        }

        private void CreateEntity(Type entityType)
        {
            CreateEntityForm form = new(entityType);
            if (form.ShowDialog() == DialogResult.OK)
                LoadData();
        }

        private void UpdateEntity(object entity)
        {
            UpdateEntityForm form = new(entity);
            if (form.ShowDialog() == DialogResult.OK)
                LoadData();
        }

        private async void DeleteEntity(object entity)
        {
            string name = entity switch
            {
                Repairman item => item.Name,
                RepairOperation item => item.Name,
                _ => "Ошибка"
            };
            if (MessageBox.Show($"Вы действительно хотите удалить \"{name}\"? Это действие нельзя отменить", $"Удаление \"{name}\"", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
                bool result;
                try
                {
                    result = await context.DeleteEntity(entity);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}\n{ex.InnerException}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (result)
                {
                    MessageBox.Show($"{name} успешно удалено", "Успех", MessageBoxButtons.OK);
                    LoadData();
                }
                else
                    MessageBox.Show($"{name} не удалено. Возможно, внутри имеются устройства", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            LoadRepairmen();
            LoadRepairOperations();
        }

        private async void LoadRepairmen()
        {
            repairmenLBox.DataSource = new List<Repairman>();
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            repairmenLBox.DataSource = await context.Repairmen.OrderBy(r => r.Name)
                                                              .AsNoTracking()
                                                              .ToListAsync();
        }

        private async void LoadRepairOperations()
        {
            repairOperationsLBox.DataSource = new List<RepairOperation>();
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            repairOperationsLBox.DataSource = await context.RepairOperations.OrderBy(r => r.Name)
                                                                            .AsNoTracking()
                                                                            .ToListAsync();
        }
    }
}