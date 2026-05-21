using Microsoft.EntityFrameworkCore;
using Timer = System.Windows.Forms.Timer;

namespace Hardware.UserControls
{
    public partial class RepairsTabPage : UserControl
    {
        private readonly ConfigManager configManager = new();

        private List<RepairsDTO> repairs;
        private readonly List<TextBox> filters = [];

        private DataGridView dataGridView;
        private readonly Timer timer = new() { Interval = 500 };
        private ContextMenuStrip contextMenu;

        public RepairsTabPage()
        {
            Load += (sender, e) => OnLoad();
        }

        private void OnLoad()
        {
            LoadData();

            timer.Tick += (sender, e) =>
            {
                timer.Stop();
                FilterData();
            };

            TableLayoutPanel mainlayout = new()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                RowStyles =
                {
                    new(SizeType.AutoSize),
                    new(SizeType.Percent,100)
                }
            };
            Controls.Add(mainlayout);

            mainlayout.Controls.Add(InitFilterPanel(), 0, 0);

            InitDataGrid();
            mainlayout.Controls.Add(dataGridView, 0, 1);
        }

        private void InitDataGrid()
        {
            dataGridView = new()
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoGenerateColumns = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

            dataGridView.Columns.AddRange([new()
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "Id",
                SortMode = DataGridViewColumnSortMode.Automatic,
                CellTemplate = new DataGridViewTextBoxCell()
            }, new()
            {
                Name = "DeviceName",
                DataPropertyName = "DeviceName",
                HeaderText = "Название устройства",
                SortMode = DataGridViewColumnSortMode.Automatic,
                CellTemplate = new DataGridViewTextBoxCell()
            }, new()
            {
                Name = "Serial",
                DataPropertyName = "Serial",
                HeaderText = "Серийный номер",
                SortMode = DataGridViewColumnSortMode.Automatic,
                CellTemplate = new DataGridViewTextBoxCell()
            }, new()
            {
                Name = "Inventory",
                DataPropertyName = "Inventory",
                HeaderText = "Инвентарный номер",
                SortMode = DataGridViewColumnSortMode.Automatic,
                CellTemplate = new DataGridViewTextBoxCell()
            }, new()
            {
                Name = "Operation",
                DataPropertyName = "Operation",
                HeaderText = "Операция",
                SortMode = DataGridViewColumnSortMode.Automatic,
                CellTemplate = new DataGridViewTextBoxCell()
            }, new()
            {
                Name = "Repairman",
                DataPropertyName = "Repairman",
                HeaderText = "Ремонтник",
                SortMode = DataGridViewColumnSortMode.Automatic,
                CellTemplate = new DataGridViewTextBoxCell()
            }, new()
            {
                Name = "Date",
                DataPropertyName = "Date",
                HeaderText = "Дата",
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd.MM.yyyy"
                },
                CellTemplate = new DataGridViewTextBoxCell()
            }, new()
            {
                Name = "Notes",
                DataPropertyName = "Notes",
                HeaderText = "Примечание",
                SortMode = DataGridViewColumnSortMode.Automatic,
                CellTemplate = new DataGridViewTextBoxCell()
            }]);

            CreateContextMenu();
            dataGridView.ContextMenuStrip = contextMenu;
        }

        private void CreateContextMenu()
        {
            contextMenu = new();

            ToolStripMenuItem menuRead = new("Обновить");
            menuRead.Click += (sender, e) => LoadData();

            ToolStripMenuItem menuDelete = new("Удалить");
            menuDelete.Click += (sender, e) => DeleteRow();

            contextMenu.Items.AddRange([menuRead, menuDelete]);
        }

        private async void DeleteRow()
        {
            if (dataGridView.SelectedRows.Count == 0)
                return;

            DataGridViewRow row = dataGridView.SelectedRows[0];
            int id = (int)row.Cells[0].Value;
            if (MessageBox.Show($"Вы действительно хотите удалить эту запись? Это действие нельзя отменить", $"Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
                bool result;
                try
                {
                    result = await context.DeleteCompletedRepairOperation(id);
                    if (result)
                        MessageBox.Show($"Запись успешно удалена", "Успех", MessageBoxButtons.OK);
                    else
                        MessageBox.Show($"Запись не удалена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}\n{ex.InnerException}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    LoadData();
                }
            }
        }

        private FlowLayoutPanel InitFilterPanel()
        {
            FlowLayoutPanel filterPanel = new()
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                Padding = new Padding(5)
            };
            AddFilter(filterPanel, "Название устройства", "DeviceName");
            AddFilter(filterPanel, "Серийный номер", "Serial");
            AddFilter(filterPanel, "Инвентарный номер", "Inventory");
            AddFilter(filterPanel, "Операция", "Operation");
            AddFilter(filterPanel, "Ремонтник", "Repairman");

            return filterPanel;
        }

        private void AddFilter(FlowLayoutPanel parent, string labelText, string tag)
        {
            FlowLayoutPanel filterRow = new()
            {
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                Margin = new Padding(3)
            };
            parent.Controls.Add(filterRow);

            Label label = new()
            {
                Anchor = AnchorStyles.Left,
                TextAlign = ContentAlignment.MiddleLeft,
                Text = $"{labelText}:"
            };
            filterRow.Controls.Add(label);

            TextBox textBox = new()
            {
                Width = 150,
                Tag = tag
            };
            textBox.TextChanged += (sender, e) =>
            {
                timer.Stop();
                timer.Start();
            };
            filterRow.Controls.Add(textBox);
            filters.Add(textBox);
        }

        private async void LoadData()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            repairs = await context.CompletedRepairOperations.Include(r => r.Device).Include(r => r.RepairOperation).Include(r => r.Repairman).OrderBy(r => r.Device.Inventory).ThenBy(r => r.Device.Serial).ThenByDescending(r => r.Date).ThenBy(r => r.Repairman.Name).Select(r => new RepairsDTO(r.Id, r.Device.DeviceName.Name, r.Device.Serial, r.Device.Inventory, r.RepairOperation.Name, r.Repairman.Name, r.Date, r.Notes)).AsSplitQuery().AsNoTracking().ToListAsync();

            FilterData();
        }

        private void FilterData()
        {
            IEnumerable<RepairsDTO> filtered = repairs;

            foreach (TextBox textBox in filters)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                    continue;
                string filter = textBox.Text.ToLower();
                switch (textBox.Tag as string)
                {
                    case "DeviceName":
                        filtered = filtered.Where(f => f.DeviceName.ToLower().Contains(filter));
                        break;
                    case "Serial":
                        filtered = filtered.Where(f => f.Serial.ToLower().Contains(filter));
                        break;
                    case "Inventory":
                        filtered = filtered.Where(f => f.Inventory.ToLower().Contains(filter));
                        break;
                    case "Operation":
                        filtered = filtered.Where(f => f.Operation.ToLower().Contains(filter));
                        break;
                    case "Repairman":
                        filtered = filtered.Where(f => f.Repairman.ToLower().Contains(filter));
                        break;
                }
            }
            dataGridView.DataSource = new SortableBindingList<RepairsDTO>([.. filtered]);
        }

        public class RepairsDTO(int id,
                                string deviceName,
                                string serial,
                                string? inventory,
                                string operation,
                                string repairman,
                                DateOnly date,
                                string? notes)
        {
            public int Id { get; set; } = id;
            public string DeviceName { get; set; } = deviceName;
            public string Serial { get; set; } = serial;
            public string? Inventory { get; set; } = inventory;
            public string Operation { get; set; } = operation;
            public string Repairman { get; set; } = repairman;
            public DateOnly Date { get; set; } = date;
            public string? Notes { get; set; } = notes;
        }
    }
}
