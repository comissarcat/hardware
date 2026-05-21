using Hardware.Models;
using Microsoft.EntityFrameworkCore;
using Timer = System.Windows.Forms.Timer;

namespace Hardware.UserControls
{
    public partial class HistoryTabPage : UserControl
    {
        private readonly ConfigManager configManager = new();

        private List<History> history;
        private readonly List<TextBox> filters = [];

        private DataGridView dataGridView;
        private readonly Timer timer = new() { Interval = 500 };
        private ContextMenuStrip contextMenu;

        private CheckBox startCheckBox;
        private DateTimePicker startDTP;
        private CheckBox endCheckBox;
        private DateTimePicker endDTP;

        public HistoryTabPage()
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
                Name = "Before",
                DataPropertyName = "Before",
                HeaderText = "Было",
                SortMode = DataGridViewColumnSortMode.Automatic,
                CellTemplate = new DataGridViewTextBoxCell(),
            }, new()
            {
                Name = "After",
                DataPropertyName = "After",
                HeaderText = "Стало",
                SortMode = DataGridViewColumnSortMode.Automatic,
                CellTemplate = new DataGridViewTextBoxCell()
            }, new()
            {
                Name = "ChangedAt",
                DataPropertyName = "ChangedAt",
                HeaderText = "Дата изменения",
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd.MM.yyyy"
                },
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
            FilterPanelSetup.AddFilter(filterPanel, "Было", "Before", timer, filters);
            FilterPanelSetup.AddFilter(filterPanel, "Стало", "After", timer, filters);
            FilterPanelSetup.AddFilter(filterPanel, ref startCheckBox, "Начало периода", ref startDTP, FilterData);
            FilterPanelSetup.AddFilter(filterPanel, ref endCheckBox, "Конец периода", ref endDTP, FilterData);

            return filterPanel;
        }

        private async void LoadData()
        {
            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();

            history = await context.History.OrderByDescending(h => h.ChangedAt).AsNoTracking().ToListAsync();

            FilterData();
        }

        private void FilterData()
        {
            IEnumerable<History> filtered = history;

            foreach (TextBox textBox in filters)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                    continue;
                string filter = textBox.Text.ToLower();
                switch (textBox.Tag as string)
                {
                    case "Before":
                        filtered = filtered.Where(f => f.Before.ToLower().Contains(filter));
                        break;
                    case "After":
                        filtered = filtered.Where(f => f.After.ToLower().Contains(filter));
                        break;
                }
            }

            if (startCheckBox.Checked)
                filtered = filtered.Where(f => f.ChangedAt >= startDTP.Value);
            if (endCheckBox.Checked)
                filtered = filtered.Where(f => f.ChangedAt <= endDTP.Value);

            dataGridView.DataSource = new SortableBindingList<History>([.. filtered]);
        }
    }
}
