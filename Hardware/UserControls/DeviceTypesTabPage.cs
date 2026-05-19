using Hardware.Forms;
using Hardware.Models;
using Timer = System.Windows.Forms.Timer;

namespace Hardware.UserControls
{
    internal class DeviceTypesTabPage : UserControl
    {
        protected TreeView treeViewLeft;
        protected TreeView treeViewRight;
        protected Button moveEntityToRightBtn;
        protected Button moveEntityToLeftBtn;
        protected TextBox searchTBoxLeft;
        protected TextBox searchTBoxRight;

        private ImageList treeImageList;

        private readonly ConfigManager configManager = new();

        private const int searchTimerDelayMS = 500;
        private readonly Timer searchTreeTimerLeft = new() { Interval = searchTimerDelayMS };
        private readonly Timer searchTreeTimerRight = new() { Interval = searchTimerDelayMS };

        private object? selectedEntityLeft;
        private object? selectedEntityRight;
        private ContextMenuStrip contextMenu;

        public DeviceTypesTabPage()
        {
            InitializeBaseComponents();
            LoadData();
        }

        private void InitializeBaseComponents()
        {
            treeImageList = new() { ColorDepth = ColorDepth.Depth32Bit, ImageSize = new(16, 16) };
            treeImageList.Images.Add("devicetype", Resources.devicetype);
            treeImageList.Images.Add("devicename", Resources.devicename);
            treeImageList.Images.Add("device", Resources.device);

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
                    new(SizeType.Percent, 100),
                    new(SizeType.AutoSize)
                }
            };

            treeViewLeft = new() { Dock = DockStyle.Fill, ImageList = treeImageList };
            mainTLP.Controls.Add(treeViewLeft, 0, 0);

            treeViewRight = new() { Dock = DockStyle.Fill, ImageList = treeImageList };
            mainTLP.Controls.Add(treeViewRight, 2, 0);

            FlowLayoutPanel buttonsPanel = new()
            {
                Anchor = AnchorStyles.None,
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown
            };

            moveEntityToRightBtn = new() { AutoSize = true, Text = ">>>", Margin = new(3, 3, 3, 3) };
            moveEntityToLeftBtn = new() { AutoSize = true, Text = "<<<", Margin = new(3, 3, 3, 3) };

            buttonsPanel.Controls.AddRange([moveEntityToRightBtn, moveEntityToLeftBtn]);
            mainTLP.Controls.Add(buttonsPanel, 1, 0);

            TableLayoutPanel searchLeftTlp = new()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                ColumnCount = 2,
                ColumnStyles =
                {
                    new(SizeType.AutoSize),
                    new(SizeType.Percent,100)
                },
                RowCount = 1,
                RowStyles = { new(SizeType.AutoSize) }
            };

            searchLeftTlp.Controls.Add(new Label()
            {
                Text = "Поиск:",
                TextAlign = ContentAlignment.MiddleLeft,
                Anchor = AnchorStyles.Left,
                AutoSize = true
            }, 0, 0);

            searchTBoxLeft = new() { Dock = DockStyle.Fill, AutoSize = true };
            searchLeftTlp.Controls.Add(searchTBoxLeft, 1, 0);

            mainTLP.Controls.Add(searchLeftTlp, 0, 1);

            TableLayoutPanel searchRightTlp = new()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                ColumnCount = 2,
                ColumnStyles =
                {
                    new(SizeType.AutoSize),
                    new(SizeType.Percent,100)
                },
                RowCount = 1,
                RowStyles = { new(SizeType.AutoSize) }
            };

            searchRightTlp.Controls.Add(new Label()
            {
                Text = "Поиск:",
                TextAlign = ContentAlignment.MiddleLeft,
                Anchor = AnchorStyles.Left,
                AutoSize = true
            }, 0, 0);

            searchTBoxRight = new() { Dock = DockStyle.Fill, AutoSize = true };
            searchRightTlp.Controls.Add(searchTBoxRight, 1, 0);

            mainTLP.Controls.Add(searchRightTlp, 2, 1);

            Controls.Add(mainTLP);

            searchTreeTimerLeft.Tick += (sender, e) =>
            {
                searchTreeTimerLeft.Stop();
                LoadData(treeViewLeft, searchTBoxLeft.Text);
            };

            searchTreeTimerRight.Tick += (sender, e) =>
            {
                searchTreeTimerRight.Stop();
                LoadData(treeViewRight, searchTBoxRight.Text);
            };

            searchTBoxLeft.TextChanged += (sender, e) =>
            {
                searchTreeTimerLeft.Stop();
                searchTreeTimerLeft.Start();
            };

            searchTBoxRight.TextChanged += (sender, e) =>
            {
                searchTreeTimerRight.Stop();
                searchTreeTimerRight.Start();
            };

            treeViewLeft.AfterSelect += (sender, e) =>
            {
                selectedEntityLeft = e.Node.Tag;
                SwitchMoveButtons();
            };

            treeViewRight.AfterSelect += (sender, e) =>
            {
                selectedEntityRight = e.Node.Tag;
                SwitchMoveButtons();
            };

            treeViewLeft.NodeMouseClick += (sender, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    treeViewLeft.SelectedNode = e.Node;
                    selectedEntityLeft = e.Node.Tag;
                    UpdateContextMenu(treeViewLeft, e.Node, e.Node.Tag);
                    SwitchMoveButtons();
                }
            };

            treeViewRight.NodeMouseClick += (sender, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    treeViewRight.SelectedNode = e.Node;
                    selectedEntityRight = e.Node.Tag;
                    UpdateContextMenu(treeViewRight, e.Node, e.Node.Tag);
                    SwitchMoveButtons();
                }
            };

            CreateContextMenu();
            treeViewLeft.ContextMenuStrip = contextMenu;
            treeViewRight.ContextMenuStrip = contextMenu;
        }

        private void CreateContextMenu()
        {
            contextMenu = new();
        }

        private void UpdateContextMenu(TreeView treeView, TreeNode selectedNode, object selectedEntity)
        {
            contextMenu.Items.Clear();

            ToolStripMenuItem menuRead = new("Обновить");
            ToolStripMenuItem menuExpandAll = new("Развернуть всё");
            menuExpandAll.Click += (sender, e) =>
            {
                treeView.BeginUpdate();
                treeView.ExpandAll();
                treeView.EndUpdate();
            };
            ToolStripMenuItem menuCollapseAll = new("Свернуть всё");
            menuCollapseAll.Click += (sender, e) => { treeView.CollapseAll(); };
            ToolStripMenuItem menuExpandNode = new("Развернуть ветку");
            menuExpandNode.Click += (sender, e) => { selectedNode.ExpandAll(); };
            ToolStripMenuItem menuCollapseNode = new("Свернуть ветку");
            menuCollapseNode.Click += (sender, e) => { selectedNode.Collapse(); };

            ToolStripMenuItem menuCreateDeviceType = new("Добавить тип устройства");
            menuCreateDeviceType.Click += (sender, e) => { CreateEntity(typeof(DeviceType)); };

            ToolStripMenuItem menuCreate = new("Добавить");
            ToolStripMenuItem menuUpdate = new("Изменить");
            ToolStripMenuItem menuDelete = new("Удалить");

            menuRead.Click += (sender, e) =>
            {
                LoadData(treeViewLeft, searchTBoxLeft.Text);
                LoadData(treeViewRight, searchTBoxRight.Text);
            };

            if (selectedEntity is DeviceType deviceType)
            {
                menuCreate.Text = "Добавить название устройства";
                menuCreate.Click += (sender, e) => { CreateEntity(deviceType); };
                menuUpdate.Text = "Изменить тип устройства";
                menuUpdate.Click += (sender, e) => { UpdateEntity(deviceType); };
                menuDelete.Text = "Удалить тип устройства";
                menuDelete.Click += (sender, e) => { DeleteEntity(deviceType); };
            }
            else if (selectedEntity is DeviceName deviceName)
            {
                menuCreate.Visible = false;
                menuUpdate.Text = "Изменить название устройства";
                menuUpdate.Click += (sender, e) => { UpdateEntity(deviceName); };
                menuDelete.Text = "Удалить название устройства";
                menuDelete.Click += (sender, e) => { DeleteEntity(deviceName); };
            }

            contextMenu.Items.Add(menuRead);
            contextMenu.Items.Add(menuExpandAll);
            contextMenu.Items.Add(menuCollapseAll);
            if (selectedNode.Nodes.Count > 0)
            {
                if (selectedNode.IsExpanded)
                    contextMenu.Items.Add(menuCollapseNode);
                contextMenu.Items.Add(menuExpandNode);
            }
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add(menuCreateDeviceType);
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add(menuCreate);
            contextMenu.Items.Add(menuUpdate);
            contextMenu.Items.Add(menuDelete);
        }

        private void SwitchMoveButtons()
        {
            moveEntityToRightBtn.Enabled = false;
            moveEntityToRightBtn.Text = ">>>";
            moveEntityToLeftBtn.Enabled = false;
            moveEntityToLeftBtn.Text = "<<<";

            if (selectedEntityLeft is DeviceName deviceName && selectedEntityRight is DeviceType deviceType)
                if (deviceName.DeviceTypeId != deviceType.Id)
                {
                    moveEntityToRightBtn.Enabled = true;
                    moveEntityToRightBtn.Text = $"{deviceName.Name} >>> {deviceType.Name}";
                    moveEntityToRightBtn.Click += (sender, e) => { UpdateEntity(deviceName, deviceType); };
                }
            if (selectedEntityLeft is Device device && selectedEntityRight is DeviceName deviceName1)
                if (device.DeviceNameId != deviceName1.Id)
                {
                    moveEntityToRightBtn.Enabled = true;
                    moveEntityToRightBtn.Text = $"{device.Serial} >>> {deviceName1.Name}";
                    moveEntityToRightBtn.Click += (sender, e) => { UpdateEntity(device, deviceName1); };
                }

            if (selectedEntityRight is DeviceName deviceName2 && selectedEntityLeft is DeviceType deviceType1)
                if (deviceName2.DeviceTypeId != deviceType1.Id)
                {
                    moveEntityToLeftBtn.Enabled = true;
                    moveEntityToLeftBtn.Text = $"{deviceType1.Name} <<< {deviceName2.Name}";
                    moveEntityToLeftBtn.Click += (sender, e) => { UpdateEntity(deviceName2, deviceType1); };
                }
            if (selectedEntityRight is Device device1 && selectedEntityLeft is DeviceName deviceName3)
                if (device1.DeviceNameId != deviceName3.Id)
                {
                    moveEntityToLeftBtn.Enabled = true;
                    moveEntityToLeftBtn.Text = $"{deviceName3.Name} <<< {device1.Serial}";
                    moveEntityToLeftBtn.Click += (sender, e) => { UpdateEntity(device1, deviceName3); };
                }

            if (moveEntityToRightBtn.Width > moveEntityToLeftBtn.Width)
                moveEntityToLeftBtn.Width = moveEntityToRightBtn.Width;
            else if (moveEntityToLeftBtn.Width > moveEntityToRightBtn.Width)
                moveEntityToRightBtn.Width = moveEntityToLeftBtn.Width;
        }

        private void CreateEntity(Type entityType)
        {
            CreateEntityForm form = new(entityType);
            if (form.ShowDialog() == DialogResult.OK)
                LoadData();
        }

        private void CreateEntity(object parent)
        {
            CreateEntityWithParentForm form = new(parent);
            if (form.ShowDialog() == DialogResult.OK)
                LoadData();
        }

        private void UpdateEntity(object entity)
        {
            UpdateEntityForm form = new(entity);
            if (form.ShowDialog() == DialogResult.OK)
                LoadData();
        }

        private async void UpdateEntity(object entity, object newParent)
        {
            ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            await context.UpdateEntity(entity, newParent);
            LoadData();
        }

        private async void DeleteEntity(object entity)
        {
            string name = entity switch
            {
                DeviceType item => item.Name,
                DeviceName item => item.Name,
                Device item => item.Serial,
                _ => "Ошибка"
            };
            if (MessageBox.Show($"Вы действительно хотите удалить \"{name}\"? Это действие нельзя отменить", $"Удаление \"{name}\"", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
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
            LoadData(treeViewLeft, searchTBoxLeft.Text);
            LoadData(treeViewRight, searchTBoxRight.Text);
        }

        private async void LoadData(TreeView treeView, string filter)
        {
            HashSet<string> expandedNodes = TreeViewStateRestorer.GetExpandedPaths(treeView);
            (string? selectedNode, string? selectedNodeParent) = TreeViewStateRestorer.GetSelectedPath(treeView);

            treeView.BeginUpdate();
            treeView.Nodes.Clear();

            using ApplicationContext context = new ApplicationContextFactory(configManager).CreateDbContext();
            List<DeviceType> deviceTypes = await context.ReadDeviceTypes(filter);
            foreach (DeviceType deviceType in deviceTypes)
            {
                TreeNode deviceTypeNode = new()
                {
                    Text = deviceType.Name,
                    Tag = deviceType,
                    ImageKey = "deviceType",
                    ToolTipText = $"Тип устройства: {deviceType.Name}"
                };

                foreach (DeviceName deviceName in deviceType.DeviceNames.OrderBy(c => c.Name))
                {
                    TreeNode deviceNameNode = new()
                    {
                        Text = deviceName.Name,
                        Tag = deviceName,
                        ImageKey = "deviceName",
                        ToolTipText = $"Название устройства: {deviceName.Name}"
                    };

                    foreach (Device device in deviceName.Devices.OrderBy(d => d.Inventory).ThenBy(d => d.DeviceName.Name))
                    {
                        TreeNode deviceNode = new()
                        {
                            Text = $"{device.DeviceName.Name} {device.Serial} {device.Inventory}",
                            Tag = device,
                            ImageKey = "device",
                            ToolTipText = $"Устройство: {device.DeviceName.Name} {device.Serial} {device.Inventory}"
                        };
                        deviceNameNode.Nodes.Add(deviceNode);
                    }

                    deviceTypeNode.Nodes.Add(deviceNameNode);
                }

                treeView.Nodes.Add(deviceTypeNode);
            }

            try
            {
                TreeViewStateRestorer.RestoreExpandedPaths(treeView, expandedNodes);
                TreeViewStateRestorer.RestoreSelectedPath(treeView, selectedNode, selectedNodeParent);
                if (treeView == treeViewLeft)
                    selectedEntityLeft = treeView.SelectedNode?.Tag;
                else
                    selectedEntityRight = treeView.SelectedNode?.Tag;
                SwitchMoveButtons();
            }
            finally
            {
                treeView.EndUpdate();
                treeView.SelectedNode?.EnsureVisible();
            }
        }
    }
}
