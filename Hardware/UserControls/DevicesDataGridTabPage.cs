using Microsoft.EntityFrameworkCore;
using Timer = System.Windows.Forms.Timer;

namespace Hardware.UserControls
{
    public partial class DevicesDataGridTabPage : UserControl
    {
        private readonly ConfigManager configManager = new();

        private List<DeviceDTO> devices;
        private readonly List<TextBox> filters = [];

        private DataGridView dataGridView;
        private readonly Timer timer = new() { Interval = 500 };
        private ContextMenuStrip contextMenu;

        public DevicesDataGridTabPage()
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
                Name = "Building",
                DataPropertyName = "Building",
                HeaderText = "Здание",
                SortMode = DataGridViewColumnSortMode.Automatic,
                CellTemplate = new DataGridViewTextBoxCell(),
            }, new()
            {
                Name = "Cabinet",
                DataPropertyName = "Cabinet",
                HeaderText = "Кабинет",
                SortMode = DataGridViewColumnSortMode.Automatic,
                CellTemplate = new DataGridViewTextBoxCell()
            }, new()
            {
                Name = "Complect",
                DataPropertyName = "Complect",
                HeaderText = "Комплект",
                SortMode = DataGridViewColumnSortMode.Automatic,
                CellTemplate = new DataGridViewTextBoxCell()
            }, new()
            {
                Name = "Type",
                DataPropertyName = "Type",
                HeaderText = "Тип устройства",
                SortMode = DataGridViewColumnSortMode.Automatic,
                CellTemplate = new DataGridViewTextBoxCell()
            }, new()
            {
                Name = "Name",
                DataPropertyName = "Name",
                HeaderText = "Название устройства",
                SortMode = DataGridViewColumnSortMode.Automatic,
                CellTemplate = new DataGridViewTextBoxCell()
            }, new()
            {
                Name = "Provider",
                DataPropertyName = "Provider",
                HeaderText = "Поставщик",
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

            contextMenu.Items.Add(menuRead);
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
            FilterPanelSetup.AddFilter(filterPanel, "Здание", "Building", timer, filters);
            FilterPanelSetup.AddFilter(filterPanel, "Кабинет", "Cabinet", timer, filters);
            FilterPanelSetup.AddFilter(filterPanel, "Комплект", "Complect", timer, filters);
            FilterPanelSetup.AddFilter(filterPanel, "Тип устройства", "Type", timer, filters);
            FilterPanelSetup.AddFilter(filterPanel, "Название устройства", "DeviceName", timer, filters);
            FilterPanelSetup.AddFilter(filterPanel, "Поставщик", "DevicePrivider", timer, filters);
            FilterPanelSetup.AddFilter(filterPanel, "Серийный номер", "Serial", timer, filters);
            FilterPanelSetup.AddFilter(filterPanel, "Инвентарный номер", "Inventory", timer, filters);
            FilterPanelSetup.AddFilter(filterPanel, "Примечание", "Notes", timer, filters);

            return filterPanel;
        }

        private async void LoadData()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            devices = await context.Devices.Include(d => d.DeviceName)
                                           .ThenInclude(dn => dn.DeviceType)
                                           .Include(d => d.DeviceProvider)
                                           .Include(d => d.Complect)
                                           .ThenInclude(c => c.Cabinet)
                                           .ThenInclude(c => c.Building)
                                           .OrderBy(d => d.Complect.Cabinet.Building.Name)
                                           .ThenBy(d => d.Complect.Cabinet.Name)
                                           .ThenBy(d => d.Complect.Name)
                                           .ThenBy(d => d.Inventory)
                                           .ThenBy(d => d.Serial)
                                           .Select(d => new DeviceDTO(d.Id,
                                                                      d.Complect.Cabinet.Building.Name,
                                                                      d.Complect.Cabinet.Name,
                                                                      d.Complect.Name,
                                                                      d.DeviceName.DeviceType.Name,
                                                                      d.DeviceName.Name,
                                                                      d.DeviceProvider.Name,
                                                                      d.Serial,
                                                                      d.Inventory,
                                                                      d.Notes))
                                           .AsNoTracking()
                                           .AsSplitQuery()
                                           .ToListAsync();

            FilterData();
        }

        private void FilterData()
        {
            IEnumerable<DeviceDTO> filtered = devices;

            foreach (TextBox textBox in filters)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                    continue;
                string filter = textBox.Text.ToLower();
                switch (textBox.Tag as string)
                {
                    case "Building":
                        filtered = filtered.Where(f => f.Building.ToLower().Contains(filter));
                        break;
                    case "Cabinet":
                        filtered = filtered.Where(f => f.Cabinet.ToLower().Contains(filter));
                        break;
                    case "Complect":
                        filtered = filtered.Where(f => f.Complect.ToLower().Contains(filter));
                        break;
                    case "Type":
                        filtered = filtered.Where(f => f.Type.ToLower().Contains(filter));
                        break;
                    case "Name":
                        filtered = filtered.Where(f => f.Name.ToLower().Contains(filter));
                        break;
                    case "Provider":
                        filtered = filtered.Where(f => f.Provider.ToLower().Contains(filter));
                        break;
                    case "Serial":
                        filtered = filtered.Where(f => f.Serial.ToLower().Contains(filter));
                        break;
                    case "Inventory":
                        filtered = filtered.Where(f => f.Inventory.ToLower().Contains(filter));
                        break;
                    case "Notes":
                        filtered = filtered.Where(f => f.Notes.ToLower().Contains(filter));
                        break;
                }
            }
            dataGridView.DataSource = new SortableBindingList<DeviceDTO>([.. filtered]);
        }

        public class DeviceDTO(int id, string building, string cabinet, string complect, string type, string name, string provider, string serial, string? inventory, string? notes)
        {
            public int Id { get; set; } = id;
            public string Building { get; set; } = building;
            public string Cabinet { get; set; } = cabinet;
            public string Complect { get; set; } = complect;
            public string Type { get; set; } = type;
            public string Name { get; set; } = name;
            public string Provider { get; set; } = provider;
            public string Serial { get; set; } = serial;
            public string? Inventory { get; set; } = inventory;
            public string? Notes { get; set; } = notes;
        }
    }
}
