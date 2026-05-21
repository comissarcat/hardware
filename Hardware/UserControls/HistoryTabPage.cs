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

        public HistoryTabPage()
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

        private FlowLayoutPanel InitFilterPanel()
        {
            FlowLayoutPanel filterPanel = new()
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                Padding = new Padding(5)
            };
            AddFilter(filterPanel, "Было", "Before");
            AddFilter(filterPanel, "Стало", "After");

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
            dataGridView.DataSource = new SortableBindingList<History>([.. filtered]);
        }
    }
}
