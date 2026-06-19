using Microsoft.EntityFrameworkCore;
using Timer = System.Windows.Forms.Timer;

namespace Hardware.UserControls
{
    public partial class RepairsTabPage : UserControl
    {
        private readonly ConfigManager configManager = new();

        private List<RepairDTO> repairs;
        private readonly List<TextBox> filters = [];

        private DataGridView dataGridView;
        private readonly Timer timer = new() { Interval = 500 };
        private ContextMenuStrip contextMenu;

        private CheckBox startCheckBox;
        private DateTimePicker startDTP;
        private CheckBox endCheckBox;
        private DateTimePicker endDTP;

        public RepairsTabPage()
        {
            Load += (sender, e) => OnLoad();
        }

        private void OnLoad()
        {
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

            LoadData();
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

        private TableLayoutPanel InitFilterPanel()
        {
            TableLayoutPanel filterPanel = new()
            {
                Dock = DockStyle.Left,
                AutoSize = true,
                ColumnCount = 2,
                ColumnStyles =
                {
                    new(SizeType.AutoSize),
                    new(SizeType.Percent,100)
                },
                RowCount = 0
            };
            FilterPanelSetup.AddFilter(filterPanel, "Название устройства", "DeviceName", timer, filters);
            FilterPanelSetup.AddFilter(filterPanel, "Серийный номер", "Serial", timer, filters);
            FilterPanelSetup.AddFilter(filterPanel, "Инвентарный номер", "Inventory", timer, filters);
            FilterPanelSetup.AddFilter(filterPanel, "Операция", "Operation", timer, filters);
            FilterPanelSetup.AddFilter(filterPanel, "Ремонтник", "Repairman", timer, filters);
            FilterPanelSetup.AddFilter(filterPanel, ref startCheckBox, "Начало периода", ref startDTP, FilterData);
            FilterPanelSetup.AddFilter(filterPanel, ref endCheckBox, "Конец периода", ref endDTP, FilterData);

            return filterPanel;
        }

        private async void LoadData()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            repairs = await context.CompletedRepairOperations.Include(r => r.Device)
                                                             .Include(r => r.RepairOperation)
                                                             .Include(r => r.Repairman)
                                                             .OrderBy(r => r.Device.Inventory)
                                                             .ThenBy(r => r.Device.Serial)
                                                             .ThenByDescending(r => r.Date)
                                                             .ThenBy(r => r.Repairman.Name)
                                                             .Select(r => new RepairDTO(r.Id, r.Device.DeviceName.Name, r.Device.Serial, r.Device.Inventory, r.RepairOperation.Name, r.Repairman.Name, r.Date, r.Notes))
                                                             .AsSplitQuery()
                                                             .AsNoTracking()
                                                             .ToListAsync();

            FilterData();
        }

        private void FilterData()
        {
            IEnumerable<RepairDTO> filtered = repairs;

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

            if (startCheckBox.Checked)
                filtered = filtered.Where(f => f.Date >= DateOnly.FromDateTime(startDTP.Value));
            if (endCheckBox.Checked)
                filtered = filtered.Where(f => f.Date <= DateOnly.FromDateTime(endDTP.Value));

            dataGridView.DataSource = new SortableBindingList<RepairDTO>([.. filtered]);
        }

        public class RepairDTO(int id,
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
