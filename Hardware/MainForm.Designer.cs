namespace Hardware
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			tabControl = new TabControl();
			tabPage1 = new TabPage();
			devicesTLP = new TableLayoutPanel();
			devicesTLPLeft = new TableLayoutPanel();
			searchLabelLeft = new Label();
			searchTBoxLeft = new TextBox();
			buildingsLabelLeft = new Label();
			cabinetsLabelLeft = new Label();
			complectsLabelLeft = new Label();
			devicesLabelLeft = new Label();
			editBuildingBtnLeft = new Button();
			editCabinetBtnLeft = new Button();
			editComplectBtnLeft = new Button();
			editDeviceBtnLeft = new Button();
			buildingsLBoxLeft = new ListBox();
			cabinetsLBoxLeft = new ListBox();
			complectsLBoxLeft = new ListBox();
			devicesLBoxLeft = new ListBox();
			devicesTLPRight = new TableLayoutPanel();
			searchLabelRight = new Label();
			searchTBoxRight = new TextBox();
			buildingsLabelRight = new Label();
			cabinetsLabelRight = new Label();
			complectsLabelRight = new Label();
			devicesLabelRight = new Label();
			editBuildingBtnRight = new Button();
			editCabinetBtnRight = new Button();
			editComplectBtnRight = new Button();
			editDeviceBtnRight = new Button();
			buildingsLBoxRight = new ListBox();
			cabinetsLBoxRight = new ListBox();
			complectsLBoxRight = new ListBox();
			devicesLBoxRight = new ListBox();
			moveLeftRightBtnsTLP = new TableLayoutPanel();
			moveCabinetToRightBtn = new Button();
			moveCabinetToLeftBtn = new Button();
			moveComplectToRightBtn = new Button();
			moveComplectToLeftBtn = new Button();
			moveDeviceToRightBtn = new Button();
			moveDeviceToLeftBtn = new Button();
			tabPage2 = new TabPage();
			deviceTypesNamesProvidersTLP = new TableLayoutPanel();
			deviceTypesNamesGBox = new GroupBox();
			deviceTypesNamesTLP = new TableLayoutPanel();
			deviceTypesLabel = new Label();
			deviceNamesLabel = new Label();
			editDeviceTypeBtn = new Button();
			editDeviceNameBtn = new Button();
			deviceTypesLBox = new ListBox();
			deviceNamesLBox = new ListBox();
			deviceProvidersTLP = new TableLayoutPanel();
			deviceProvidersLabel = new Label();
			editDeviceProviderBtn = new Button();
			deviceProvidersLBox = new ListBox();
			tabPage3 = new TabPage();
			tableLayoutPanel1 = new TableLayoutPanel();
			historySearchTBox = new TextBox();
			historySearchLabel = new Label();
			historyDGW = new DataGridView();
			menuStrip = new MenuStrip();
			refreshToolStripMenuItem = new ToolStripMenuItem();
			tabControl.SuspendLayout();
			tabPage1.SuspendLayout();
			devicesTLP.SuspendLayout();
			devicesTLPLeft.SuspendLayout();
			devicesTLPRight.SuspendLayout();
			moveLeftRightBtnsTLP.SuspendLayout();
			tabPage2.SuspendLayout();
			deviceTypesNamesProvidersTLP.SuspendLayout();
			deviceTypesNamesGBox.SuspendLayout();
			deviceTypesNamesTLP.SuspendLayout();
			deviceProvidersTLP.SuspendLayout();
			tabPage3.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)historyDGW).BeginInit();
			menuStrip.SuspendLayout();
			SuspendLayout();
			// 
			// tabControl
			// 
			tabControl.Controls.Add(tabPage1);
			tabControl.Controls.Add(tabPage2);
			tabControl.Controls.Add(tabPage3);
			tabControl.Dock = DockStyle.Fill;
			tabControl.Location = new Point(0, 24);
			tabControl.Name = "tabControl";
			tabControl.SelectedIndex = 0;
			tabControl.Size = new Size(984, 437);
			tabControl.TabIndex = 0;
			// 
			// tabPage1
			// 
			tabPage1.Controls.Add(devicesTLP);
			tabPage1.Location = new Point(4, 24);
			tabPage1.Name = "tabPage1";
			tabPage1.Padding = new Padding(3);
			tabPage1.Size = new Size(976, 409);
			tabPage1.TabIndex = 0;
			tabPage1.Text = "Перемещение техники";
			tabPage1.UseVisualStyleBackColor = true;
			// 
			// devicesTLP
			// 
			devicesTLP.ColumnCount = 3;
			devicesTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			devicesTLP.ColumnStyles.Add(new ColumnStyle());
			devicesTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0000076F));
			devicesTLP.Controls.Add(devicesTLPLeft, 0, 0);
			devicesTLP.Controls.Add(devicesTLPRight, 2, 0);
			devicesTLP.Controls.Add(moveLeftRightBtnsTLP, 1, 0);
			devicesTLP.Dock = DockStyle.Fill;
			devicesTLP.Location = new Point(3, 3);
			devicesTLP.Name = "devicesTLP";
			devicesTLP.RowCount = 1;
			devicesTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			devicesTLP.Size = new Size(970, 403);
			devicesTLP.TabIndex = 0;
			// 
			// devicesTLPLeft
			// 
			devicesTLPLeft.ColumnCount = 4;
			devicesTLPLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
			devicesTLPLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
			devicesTLPLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
			devicesTLPLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
			devicesTLPLeft.Controls.Add(searchLabelLeft, 0, 0);
			devicesTLPLeft.Controls.Add(searchTBoxLeft, 1, 0);
			devicesTLPLeft.Controls.Add(buildingsLabelLeft, 0, 1);
			devicesTLPLeft.Controls.Add(cabinetsLabelLeft, 1, 1);
			devicesTLPLeft.Controls.Add(complectsLabelLeft, 2, 1);
			devicesTLPLeft.Controls.Add(devicesLabelLeft, 3, 1);
			devicesTLPLeft.Controls.Add(editBuildingBtnLeft, 0, 2);
			devicesTLPLeft.Controls.Add(editCabinetBtnLeft, 1, 2);
			devicesTLPLeft.Controls.Add(editComplectBtnLeft, 2, 2);
			devicesTLPLeft.Controls.Add(editDeviceBtnLeft, 3, 2);
			devicesTLPLeft.Controls.Add(buildingsLBoxLeft, 0, 3);
			devicesTLPLeft.Controls.Add(cabinetsLBoxLeft, 1, 3);
			devicesTLPLeft.Controls.Add(complectsLBoxLeft, 2, 3);
			devicesTLPLeft.Controls.Add(devicesLBoxLeft, 3, 3);
			devicesTLPLeft.Dock = DockStyle.Fill;
			devicesTLPLeft.Location = new Point(3, 3);
			devicesTLPLeft.Name = "devicesTLPLeft";
			devicesTLPLeft.RowCount = 4;
			devicesTLPLeft.RowStyles.Add(new RowStyle());
			devicesTLPLeft.RowStyles.Add(new RowStyle());
			devicesTLPLeft.RowStyles.Add(new RowStyle());
			devicesTLPLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			devicesTLPLeft.Size = new Size(419, 397);
			devicesTLPLeft.TabIndex = 0;
			// 
			// searchLabelLeft
			// 
			searchLabelLeft.Anchor = AnchorStyles.None;
			searchLabelLeft.AutoSize = true;
			searchLabelLeft.Location = new Point(20, 7);
			searchLabelLeft.Name = "searchLabelLeft";
			searchLabelLeft.Size = new Size(42, 15);
			searchLabelLeft.TabIndex = 13;
			searchLabelLeft.Text = "Поиск";
			// 
			// searchTBoxLeft
			// 
			devicesTLPLeft.SetColumnSpan(searchTBoxLeft, 3);
			searchTBoxLeft.Dock = DockStyle.Fill;
			searchTBoxLeft.Location = new Point(86, 3);
			searchTBoxLeft.Name = "searchTBoxLeft";
			searchTBoxLeft.Size = new Size(330, 23);
			searchTBoxLeft.TabIndex = 14;
			searchTBoxLeft.TextChanged += searchTBox_TextChanged;
			// 
			// buildingsLabelLeft
			// 
			buildingsLabelLeft.Anchor = AnchorStyles.None;
			buildingsLabelLeft.AutoSize = true;
			buildingsLabelLeft.Location = new Point(18, 29);
			buildingsLabelLeft.Name = "buildingsLabelLeft";
			buildingsLabelLeft.Size = new Size(46, 15);
			buildingsLabelLeft.TabIndex = 4;
			buildingsLabelLeft.Text = "Здания";
			// 
			// cabinetsLabelLeft
			// 
			cabinetsLabelLeft.Anchor = AnchorStyles.None;
			cabinetsLabelLeft.AutoSize = true;
			cabinetsLabelLeft.Location = new Point(94, 29);
			cabinetsLabelLeft.Name = "cabinetsLabelLeft";
			cabinetsLabelLeft.Size = new Size(61, 15);
			cabinetsLabelLeft.TabIndex = 5;
			cabinetsLabelLeft.Text = "Кабинеты";
			// 
			// complectsLabelLeft
			// 
			complectsLabelLeft.Anchor = AnchorStyles.None;
			complectsLabelLeft.AutoSize = true;
			complectsLabelLeft.Location = new Point(172, 29);
			complectsLabelLeft.Name = "complectsLabelLeft";
			complectsLabelLeft.Size = new Size(70, 15);
			complectsLabelLeft.TabIndex = 8;
			complectsLabelLeft.Text = "Комплекты";
			// 
			// devicesLabelLeft
			// 
			devicesLabelLeft.Anchor = AnchorStyles.None;
			devicesLabelLeft.AutoSize = true;
			devicesLabelLeft.Location = new Point(282, 29);
			devicesLabelLeft.Name = "devicesLabelLeft";
			devicesLabelLeft.Size = new Size(103, 15);
			devicesLabelLeft.TabIndex = 11;
			devicesLabelLeft.Text = "Единицы техники";
			// 
			// editBuildingBtnLeft
			// 
			editBuildingBtnLeft.Anchor = AnchorStyles.None;
			editBuildingBtnLeft.AutoSize = true;
			editBuildingBtnLeft.Location = new Point(3, 47);
			editBuildingBtnLeft.Name = "editBuildingBtnLeft";
			editBuildingBtnLeft.Size = new Size(77, 25);
			editBuildingBtnLeft.TabIndex = 2;
			editBuildingBtnLeft.Text = "editBuildingBtn1";
			editBuildingBtnLeft.UseVisualStyleBackColor = true;
			editBuildingBtnLeft.Click += editBuildingBtn_Click;
			// 
			// editCabinetBtnLeft
			// 
			editCabinetBtnLeft.Anchor = AnchorStyles.None;
			editCabinetBtnLeft.AutoSize = true;
			editCabinetBtnLeft.Enabled = false;
			editCabinetBtnLeft.Location = new Point(86, 47);
			editCabinetBtnLeft.Name = "editCabinetBtnLeft";
			editCabinetBtnLeft.Size = new Size(77, 25);
			editCabinetBtnLeft.TabIndex = 3;
			editCabinetBtnLeft.Text = "editCabinetBtn1";
			editCabinetBtnLeft.UseVisualStyleBackColor = true;
			editCabinetBtnLeft.Click += editCabinetBtn_Click;
			// 
			// editComplectBtnLeft
			// 
			editComplectBtnLeft.Anchor = AnchorStyles.None;
			editComplectBtnLeft.AutoSize = true;
			editComplectBtnLeft.Enabled = false;
			editComplectBtnLeft.Location = new Point(169, 47);
			editComplectBtnLeft.Name = "editComplectBtnLeft";
			editComplectBtnLeft.Size = new Size(77, 25);
			editComplectBtnLeft.TabIndex = 7;
			editComplectBtnLeft.Text = "editComplectBtn1";
			editComplectBtnLeft.UseVisualStyleBackColor = true;
			editComplectBtnLeft.Click += editComplectBtn_Click;
			// 
			// editDeviceBtnLeft
			// 
			editDeviceBtnLeft.Anchor = AnchorStyles.None;
			editDeviceBtnLeft.AutoSize = true;
			editDeviceBtnLeft.Enabled = false;
			editDeviceBtnLeft.Location = new Point(286, 47);
			editDeviceBtnLeft.Name = "editDeviceBtnLeft";
			editDeviceBtnLeft.Size = new Size(96, 25);
			editDeviceBtnLeft.TabIndex = 10;
			editDeviceBtnLeft.Text = "editDeviceBtn1";
			editDeviceBtnLeft.UseVisualStyleBackColor = true;
			editDeviceBtnLeft.Click += editDeviceBtn_Click;
			// 
			// buildingsLBoxLeft
			// 
			buildingsLBoxLeft.Dock = DockStyle.Fill;
			buildingsLBoxLeft.FormattingEnabled = true;
			buildingsLBoxLeft.ItemHeight = 15;
			buildingsLBoxLeft.Location = new Point(3, 78);
			buildingsLBoxLeft.Name = "buildingsLBoxLeft";
			buildingsLBoxLeft.Size = new Size(77, 316);
			buildingsLBoxLeft.TabIndex = 0;
			buildingsLBoxLeft.SelectedValueChanged += buildingsLBox_SelectedValueChanged;
			// 
			// cabinetsLBoxLeft
			// 
			cabinetsLBoxLeft.Dock = DockStyle.Fill;
			cabinetsLBoxLeft.FormattingEnabled = true;
			cabinetsLBoxLeft.ItemHeight = 15;
			cabinetsLBoxLeft.Location = new Point(86, 78);
			cabinetsLBoxLeft.Name = "cabinetsLBoxLeft";
			cabinetsLBoxLeft.Size = new Size(77, 316);
			cabinetsLBoxLeft.TabIndex = 1;
			cabinetsLBoxLeft.SelectedValueChanged += cabinetsLBox_SelectedValueChanged;
			// 
			// complectsLBoxLeft
			// 
			complectsLBoxLeft.Dock = DockStyle.Fill;
			complectsLBoxLeft.FormattingEnabled = true;
			complectsLBoxLeft.ItemHeight = 15;
			complectsLBoxLeft.Location = new Point(169, 78);
			complectsLBoxLeft.Name = "complectsLBoxLeft";
			complectsLBoxLeft.Size = new Size(77, 316);
			complectsLBoxLeft.TabIndex = 6;
			complectsLBoxLeft.SelectedValueChanged += complectsLBox_SelectedValueChanged;
			// 
			// devicesLBoxLeft
			// 
			devicesLBoxLeft.Dock = DockStyle.Fill;
			devicesLBoxLeft.FormattingEnabled = true;
			devicesLBoxLeft.ItemHeight = 15;
			devicesLBoxLeft.Location = new Point(252, 78);
			devicesLBoxLeft.Name = "devicesLBoxLeft";
			devicesLBoxLeft.Size = new Size(164, 316);
			devicesLBoxLeft.TabIndex = 15;
			devicesLBoxLeft.SelectedValueChanged += devicesLBox_SelectedValueChanged;
			// 
			// devicesTLPRight
			// 
			devicesTLPRight.ColumnCount = 4;
			devicesTLPRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
			devicesTLPRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
			devicesTLPRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
			devicesTLPRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
			devicesTLPRight.Controls.Add(searchLabelRight, 0, 0);
			devicesTLPRight.Controls.Add(searchTBoxRight, 1, 0);
			devicesTLPRight.Controls.Add(buildingsLabelRight, 0, 1);
			devicesTLPRight.Controls.Add(cabinetsLabelRight, 1, 1);
			devicesTLPRight.Controls.Add(complectsLabelRight, 2, 1);
			devicesTLPRight.Controls.Add(devicesLabelRight, 3, 1);
			devicesTLPRight.Controls.Add(editBuildingBtnRight, 0, 2);
			devicesTLPRight.Controls.Add(editCabinetBtnRight, 1, 2);
			devicesTLPRight.Controls.Add(editComplectBtnRight, 2, 2);
			devicesTLPRight.Controls.Add(editDeviceBtnRight, 3, 2);
			devicesTLPRight.Controls.Add(buildingsLBoxRight, 0, 3);
			devicesTLPRight.Controls.Add(cabinetsLBoxRight, 1, 3);
			devicesTLPRight.Controls.Add(complectsLBoxRight, 2, 3);
			devicesTLPRight.Controls.Add(devicesLBoxRight, 3, 3);
			devicesTLPRight.Dock = DockStyle.Fill;
			devicesTLPRight.Location = new Point(547, 3);
			devicesTLPRight.Name = "devicesTLPRight";
			devicesTLPRight.RowCount = 4;
			devicesTLPRight.RowStyles.Add(new RowStyle());
			devicesTLPRight.RowStyles.Add(new RowStyle());
			devicesTLPRight.RowStyles.Add(new RowStyle());
			devicesTLPRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			devicesTLPRight.Size = new Size(420, 397);
			devicesTLPRight.TabIndex = 1;
			// 
			// searchLabelRight
			// 
			searchLabelRight.Anchor = AnchorStyles.None;
			searchLabelRight.AutoSize = true;
			searchLabelRight.Location = new Point(21, 7);
			searchLabelRight.Name = "searchLabelRight";
			searchLabelRight.Size = new Size(42, 15);
			searchLabelRight.TabIndex = 14;
			searchLabelRight.Text = "Поиск";
			// 
			// searchTBoxRight
			// 
			devicesTLPRight.SetColumnSpan(searchTBoxRight, 3);
			searchTBoxRight.Dock = DockStyle.Fill;
			searchTBoxRight.Location = new Point(87, 3);
			searchTBoxRight.Name = "searchTBoxRight";
			searchTBoxRight.Size = new Size(330, 23);
			searchTBoxRight.TabIndex = 15;
			searchTBoxRight.TextChanged += searchTBox_TextChanged;
			// 
			// buildingsLabelRight
			// 
			buildingsLabelRight.Anchor = AnchorStyles.None;
			buildingsLabelRight.AutoSize = true;
			buildingsLabelRight.Location = new Point(19, 29);
			buildingsLabelRight.Name = "buildingsLabelRight";
			buildingsLabelRight.Size = new Size(46, 15);
			buildingsLabelRight.TabIndex = 5;
			buildingsLabelRight.Text = "Здания";
			// 
			// cabinetsLabelRight
			// 
			cabinetsLabelRight.Anchor = AnchorStyles.None;
			cabinetsLabelRight.AutoSize = true;
			cabinetsLabelRight.Location = new Point(95, 29);
			cabinetsLabelRight.Name = "cabinetsLabelRight";
			cabinetsLabelRight.Size = new Size(61, 15);
			cabinetsLabelRight.TabIndex = 6;
			cabinetsLabelRight.Text = "Кабинеты";
			// 
			// complectsLabelRight
			// 
			complectsLabelRight.Anchor = AnchorStyles.None;
			complectsLabelRight.AutoSize = true;
			complectsLabelRight.Location = new Point(175, 29);
			complectsLabelRight.Name = "complectsLabelRight";
			complectsLabelRight.Size = new Size(70, 15);
			complectsLabelRight.TabIndex = 9;
			complectsLabelRight.Text = "Комплекты";
			// 
			// devicesLabelRight
			// 
			devicesLabelRight.Anchor = AnchorStyles.None;
			devicesLabelRight.AutoSize = true;
			devicesLabelRight.Location = new Point(284, 29);
			devicesLabelRight.Name = "devicesLabelRight";
			devicesLabelRight.Size = new Size(103, 15);
			devicesLabelRight.TabIndex = 12;
			devicesLabelRight.Text = "Единицы техники";
			// 
			// editBuildingBtnRight
			// 
			editBuildingBtnRight.Anchor = AnchorStyles.None;
			editBuildingBtnRight.AutoSize = true;
			editBuildingBtnRight.Location = new Point(3, 47);
			editBuildingBtnRight.Name = "editBuildingBtnRight";
			editBuildingBtnRight.Size = new Size(78, 25);
			editBuildingBtnRight.TabIndex = 2;
			editBuildingBtnRight.Text = "editBuildingBtn2";
			editBuildingBtnRight.UseVisualStyleBackColor = true;
			editBuildingBtnRight.Click += editBuildingBtn_Click;
			// 
			// editCabinetBtnRight
			// 
			editCabinetBtnRight.Anchor = AnchorStyles.None;
			editCabinetBtnRight.AutoSize = true;
			editCabinetBtnRight.Enabled = false;
			editCabinetBtnRight.Location = new Point(87, 47);
			editCabinetBtnRight.Name = "editCabinetBtnRight";
			editCabinetBtnRight.Size = new Size(78, 25);
			editCabinetBtnRight.TabIndex = 3;
			editCabinetBtnRight.Text = "editCabinetBtn2";
			editCabinetBtnRight.UseVisualStyleBackColor = true;
			editCabinetBtnRight.Click += editCabinetBtn_Click;
			// 
			// editComplectBtnRight
			// 
			editComplectBtnRight.Anchor = AnchorStyles.None;
			editComplectBtnRight.AutoSize = true;
			editComplectBtnRight.Enabled = false;
			editComplectBtnRight.Location = new Point(171, 47);
			editComplectBtnRight.Name = "editComplectBtnRight";
			editComplectBtnRight.Size = new Size(78, 25);
			editComplectBtnRight.TabIndex = 8;
			editComplectBtnRight.Text = "editComplectBtn2";
			editComplectBtnRight.UseVisualStyleBackColor = true;
			editComplectBtnRight.Click += editComplectBtn_Click;
			// 
			// editDeviceBtnRight
			// 
			editDeviceBtnRight.Anchor = AnchorStyles.None;
			editDeviceBtnRight.AutoSize = true;
			editDeviceBtnRight.Enabled = false;
			editDeviceBtnRight.Location = new Point(286, 47);
			editDeviceBtnRight.Name = "editDeviceBtnRight";
			editDeviceBtnRight.Size = new Size(99, 25);
			editDeviceBtnRight.TabIndex = 11;
			editDeviceBtnRight.Text = "editDeviceBtn2";
			editDeviceBtnRight.UseVisualStyleBackColor = true;
			editDeviceBtnRight.Click += editDeviceBtn_Click;
			// 
			// buildingsLBoxRight
			// 
			buildingsLBoxRight.Dock = DockStyle.Fill;
			buildingsLBoxRight.FormattingEnabled = true;
			buildingsLBoxRight.ItemHeight = 15;
			buildingsLBoxRight.Location = new Point(3, 78);
			buildingsLBoxRight.Name = "buildingsLBoxRight";
			buildingsLBoxRight.Size = new Size(78, 316);
			buildingsLBoxRight.TabIndex = 0;
			buildingsLBoxRight.SelectedValueChanged += buildingsLBox_SelectedValueChanged;
			// 
			// cabinetsLBoxRight
			// 
			cabinetsLBoxRight.Dock = DockStyle.Fill;
			cabinetsLBoxRight.FormattingEnabled = true;
			cabinetsLBoxRight.ItemHeight = 15;
			cabinetsLBoxRight.Location = new Point(87, 78);
			cabinetsLBoxRight.Name = "cabinetsLBoxRight";
			cabinetsLBoxRight.Size = new Size(78, 316);
			cabinetsLBoxRight.TabIndex = 1;
			cabinetsLBoxRight.SelectedValueChanged += cabinetsLBox_SelectedValueChanged;
			// 
			// complectsLBoxRight
			// 
			complectsLBoxRight.Dock = DockStyle.Fill;
			complectsLBoxRight.FormattingEnabled = true;
			complectsLBoxRight.ItemHeight = 15;
			complectsLBoxRight.Location = new Point(171, 78);
			complectsLBoxRight.Name = "complectsLBoxRight";
			complectsLBoxRight.Size = new Size(78, 316);
			complectsLBoxRight.TabIndex = 7;
			complectsLBoxRight.SelectedValueChanged += complectsLBox_SelectedValueChanged;
			// 
			// devicesLBoxRight
			// 
			devicesLBoxRight.Dock = DockStyle.Fill;
			devicesLBoxRight.FormattingEnabled = true;
			devicesLBoxRight.ItemHeight = 15;
			devicesLBoxRight.Location = new Point(255, 78);
			devicesLBoxRight.Name = "devicesLBoxRight";
			devicesLBoxRight.Size = new Size(162, 316);
			devicesLBoxRight.TabIndex = 16;
			devicesLBoxRight.SelectedValueChanged += devicesLBox_SelectedValueChanged;
			// 
			// moveLeftRightBtnsTLP
			// 
			moveLeftRightBtnsTLP.Anchor = AnchorStyles.None;
			moveLeftRightBtnsTLP.AutoSize = true;
			moveLeftRightBtnsTLP.ColumnCount = 1;
			moveLeftRightBtnsTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			moveLeftRightBtnsTLP.Controls.Add(moveCabinetToRightBtn, 0, 0);
			moveLeftRightBtnsTLP.Controls.Add(moveCabinetToLeftBtn, 0, 1);
			moveLeftRightBtnsTLP.Controls.Add(moveComplectToRightBtn, 0, 3);
			moveLeftRightBtnsTLP.Controls.Add(moveComplectToLeftBtn, 0, 4);
			moveLeftRightBtnsTLP.Controls.Add(moveDeviceToRightBtn, 0, 6);
			moveLeftRightBtnsTLP.Controls.Add(moveDeviceToLeftBtn, 0, 7);
			moveLeftRightBtnsTLP.Location = new Point(428, 77);
			moveLeftRightBtnsTLP.Name = "moveLeftRightBtnsTLP";
			moveLeftRightBtnsTLP.RowCount = 8;
			moveLeftRightBtnsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			moveLeftRightBtnsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			moveLeftRightBtnsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			moveLeftRightBtnsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			moveLeftRightBtnsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			moveLeftRightBtnsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			moveLeftRightBtnsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			moveLeftRightBtnsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			moveLeftRightBtnsTLP.Size = new Size(113, 248);
			moveLeftRightBtnsTLP.TabIndex = 2;
			// 
			// moveCabinetToRightBtn
			// 
			moveCabinetToRightBtn.AutoSize = true;
			moveCabinetToRightBtn.Dock = DockStyle.Fill;
			moveCabinetToRightBtn.Enabled = false;
			moveCabinetToRightBtn.Location = new Point(3, 3);
			moveCabinetToRightBtn.Name = "moveCabinetToRightBtn";
			moveCabinetToRightBtn.Size = new Size(107, 25);
			moveCabinetToRightBtn.TabIndex = 0;
			moveCabinetToRightBtn.Text = "Кабинет >>>";
			moveCabinetToRightBtn.UseVisualStyleBackColor = true;
			moveCabinetToRightBtn.Click += moveCabinetToRightBtn_Click;
			// 
			// moveCabinetToLeftBtn
			// 
			moveCabinetToLeftBtn.AutoSize = true;
			moveCabinetToLeftBtn.Dock = DockStyle.Fill;
			moveCabinetToLeftBtn.Enabled = false;
			moveCabinetToLeftBtn.Location = new Point(3, 34);
			moveCabinetToLeftBtn.Name = "moveCabinetToLeftBtn";
			moveCabinetToLeftBtn.Size = new Size(107, 25);
			moveCabinetToLeftBtn.TabIndex = 1;
			moveCabinetToLeftBtn.Text = "<<< Кабинет";
			moveCabinetToLeftBtn.UseVisualStyleBackColor = true;
			moveCabinetToLeftBtn.Click += moveCabinetToLeftBtn_Click;
			// 
			// moveComplectToRightBtn
			// 
			moveComplectToRightBtn.AutoSize = true;
			moveComplectToRightBtn.Dock = DockStyle.Fill;
			moveComplectToRightBtn.Location = new Point(3, 96);
			moveComplectToRightBtn.Name = "moveComplectToRightBtn";
			moveComplectToRightBtn.Size = new Size(107, 25);
			moveComplectToRightBtn.TabIndex = 2;
			moveComplectToRightBtn.Text = "Комплект >>>";
			moveComplectToRightBtn.UseVisualStyleBackColor = true;
			moveComplectToRightBtn.Click += moveComplectToRightBtn_Click;
			// 
			// moveComplectToLeftBtn
			// 
			moveComplectToLeftBtn.AutoSize = true;
			moveComplectToLeftBtn.Dock = DockStyle.Fill;
			moveComplectToLeftBtn.Location = new Point(3, 127);
			moveComplectToLeftBtn.Name = "moveComplectToLeftBtn";
			moveComplectToLeftBtn.Size = new Size(107, 25);
			moveComplectToLeftBtn.TabIndex = 3;
			moveComplectToLeftBtn.Text = "<<< Комплект";
			moveComplectToLeftBtn.UseVisualStyleBackColor = true;
			moveComplectToLeftBtn.Click += moveComplectToLeftBtn_Click;
			// 
			// moveDeviceToRightBtn
			// 
			moveDeviceToRightBtn.AutoSize = true;
			moveDeviceToRightBtn.Dock = DockStyle.Fill;
			moveDeviceToRightBtn.Location = new Point(3, 189);
			moveDeviceToRightBtn.Name = "moveDeviceToRightBtn";
			moveDeviceToRightBtn.Size = new Size(107, 25);
			moveDeviceToRightBtn.TabIndex = 4;
			moveDeviceToRightBtn.Text = "Устройство >>>";
			moveDeviceToRightBtn.UseVisualStyleBackColor = true;
			moveDeviceToRightBtn.Click += moveDeviceToRightBtn_Click;
			// 
			// moveDeviceToLeftBtn
			// 
			moveDeviceToLeftBtn.AutoSize = true;
			moveDeviceToLeftBtn.Dock = DockStyle.Fill;
			moveDeviceToLeftBtn.Location = new Point(3, 220);
			moveDeviceToLeftBtn.Name = "moveDeviceToLeftBtn";
			moveDeviceToLeftBtn.Size = new Size(107, 25);
			moveDeviceToLeftBtn.TabIndex = 5;
			moveDeviceToLeftBtn.Text = "<<< Устройство";
			moveDeviceToLeftBtn.UseVisualStyleBackColor = true;
			moveDeviceToLeftBtn.Click += moveDeviceToLeftBtn_Click;
			// 
			// tabPage2
			// 
			tabPage2.Controls.Add(deviceTypesNamesProvidersTLP);
			tabPage2.Location = new Point(4, 24);
			tabPage2.Name = "tabPage2";
			tabPage2.Padding = new Padding(3);
			tabPage2.Size = new Size(976, 409);
			tabPage2.TabIndex = 3;
			tabPage2.Text = "Типы, названия, предоставители техники";
			tabPage2.UseVisualStyleBackColor = true;
			// 
			// deviceTypesNamesProvidersTLP
			// 
			deviceTypesNamesProvidersTLP.ColumnCount = 2;
			deviceTypesNamesProvidersTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.6666641F));
			deviceTypesNamesProvidersTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
			deviceTypesNamesProvidersTLP.Controls.Add(deviceTypesNamesGBox, 0, 0);
			deviceTypesNamesProvidersTLP.Controls.Add(deviceProvidersTLP, 1, 0);
			deviceTypesNamesProvidersTLP.Dock = DockStyle.Fill;
			deviceTypesNamesProvidersTLP.Location = new Point(3, 3);
			deviceTypesNamesProvidersTLP.Name = "deviceTypesNamesProvidersTLP";
			deviceTypesNamesProvidersTLP.RowCount = 1;
			deviceTypesNamesProvidersTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			deviceTypesNamesProvidersTLP.Size = new Size(970, 403);
			deviceTypesNamesProvidersTLP.TabIndex = 0;
			// 
			// deviceTypesNamesGBox
			// 
			deviceTypesNamesGBox.Controls.Add(deviceTypesNamesTLP);
			deviceTypesNamesGBox.Dock = DockStyle.Fill;
			deviceTypesNamesGBox.Location = new Point(3, 3);
			deviceTypesNamesGBox.Name = "deviceTypesNamesGBox";
			deviceTypesNamesGBox.Size = new Size(640, 397);
			deviceTypesNamesGBox.TabIndex = 3;
			deviceTypesNamesGBox.TabStop = false;
			deviceTypesNamesGBox.Text = "Типы и названия";
			// 
			// deviceTypesNamesTLP
			// 
			deviceTypesNamesTLP.ColumnCount = 2;
			deviceTypesNamesTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			deviceTypesNamesTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			deviceTypesNamesTLP.Controls.Add(deviceTypesLabel, 0, 0);
			deviceTypesNamesTLP.Controls.Add(deviceNamesLabel, 1, 0);
			deviceTypesNamesTLP.Controls.Add(editDeviceTypeBtn, 0, 1);
			deviceTypesNamesTLP.Controls.Add(editDeviceNameBtn, 1, 1);
			deviceTypesNamesTLP.Controls.Add(deviceTypesLBox, 0, 2);
			deviceTypesNamesTLP.Controls.Add(deviceNamesLBox, 1, 2);
			deviceTypesNamesTLP.Dock = DockStyle.Fill;
			deviceTypesNamesTLP.Location = new Point(3, 19);
			deviceTypesNamesTLP.Name = "deviceTypesNamesTLP";
			deviceTypesNamesTLP.RowCount = 3;
			deviceTypesNamesTLP.RowStyles.Add(new RowStyle());
			deviceTypesNamesTLP.RowStyles.Add(new RowStyle());
			deviceTypesNamesTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			deviceTypesNamesTLP.Size = new Size(634, 375);
			deviceTypesNamesTLP.TabIndex = 0;
			// 
			// deviceTypesLabel
			// 
			deviceTypesLabel.Anchor = AnchorStyles.None;
			deviceTypesLabel.AutoSize = true;
			deviceTypesLabel.Location = new Point(117, 0);
			deviceTypesLabel.Name = "deviceTypesLabel";
			deviceTypesLabel.Size = new Size(83, 15);
			deviceTypesLabel.TabIndex = 2;
			deviceTypesLabel.Text = "Типы техники";
			// 
			// deviceNamesLabel
			// 
			deviceNamesLabel.Anchor = AnchorStyles.None;
			deviceNamesLabel.AutoSize = true;
			deviceNamesLabel.Location = new Point(422, 0);
			deviceNamesLabel.Name = "deviceNamesLabel";
			deviceNamesLabel.Size = new Size(106, 15);
			deviceNamesLabel.TabIndex = 2;
			deviceNamesLabel.Text = "Названия техники";
			// 
			// editDeviceTypeBtn
			// 
			editDeviceTypeBtn.Anchor = AnchorStyles.None;
			editDeviceTypeBtn.AutoSize = true;
			editDeviceTypeBtn.Location = new Point(95, 18);
			editDeviceTypeBtn.Name = "editDeviceTypeBtn";
			editDeviceTypeBtn.Size = new Size(126, 25);
			editDeviceTypeBtn.TabIndex = 1;
			editDeviceTypeBtn.Text = "Добавить/изменить";
			editDeviceTypeBtn.UseVisualStyleBackColor = true;
			editDeviceTypeBtn.Click += deviceTypeEditBtn_Click;
			// 
			// editDeviceNameBtn
			// 
			editDeviceNameBtn.Anchor = AnchorStyles.None;
			editDeviceNameBtn.AutoSize = true;
			editDeviceNameBtn.Location = new Point(412, 18);
			editDeviceNameBtn.Name = "editDeviceNameBtn";
			editDeviceNameBtn.Size = new Size(126, 25);
			editDeviceNameBtn.TabIndex = 1;
			editDeviceNameBtn.Text = "Добавить/изменить";
			editDeviceNameBtn.UseVisualStyleBackColor = true;
			editDeviceNameBtn.Click += deviceNameEditBtn_Click;
			// 
			// deviceTypesLBox
			// 
			deviceTypesLBox.Dock = DockStyle.Fill;
			deviceTypesLBox.FormattingEnabled = true;
			deviceTypesLBox.ItemHeight = 15;
			deviceTypesLBox.Location = new Point(3, 49);
			deviceTypesLBox.Name = "deviceTypesLBox";
			deviceTypesLBox.Size = new Size(311, 323);
			deviceTypesLBox.TabIndex = 0;
			deviceTypesLBox.SelectedValueChanged += typesLBox_SelectedValueChanged;
			// 
			// deviceNamesLBox
			// 
			deviceNamesLBox.Dock = DockStyle.Fill;
			deviceNamesLBox.FormattingEnabled = true;
			deviceNamesLBox.ItemHeight = 15;
			deviceNamesLBox.Location = new Point(320, 49);
			deviceNamesLBox.Name = "deviceNamesLBox";
			deviceNamesLBox.Size = new Size(311, 323);
			deviceNamesLBox.TabIndex = 3;
			// 
			// deviceProvidersTLP
			// 
			deviceProvidersTLP.ColumnCount = 1;
			deviceProvidersTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			deviceProvidersTLP.Controls.Add(deviceProvidersLabel, 0, 0);
			deviceProvidersTLP.Controls.Add(editDeviceProviderBtn, 0, 1);
			deviceProvidersTLP.Controls.Add(deviceProvidersLBox, 0, 2);
			deviceProvidersTLP.Dock = DockStyle.Fill;
			deviceProvidersTLP.Location = new Point(649, 3);
			deviceProvidersTLP.Name = "deviceProvidersTLP";
			deviceProvidersTLP.RowCount = 3;
			deviceProvidersTLP.RowStyles.Add(new RowStyle());
			deviceProvidersTLP.RowStyles.Add(new RowStyle());
			deviceProvidersTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			deviceProvidersTLP.Size = new Size(318, 397);
			deviceProvidersTLP.TabIndex = 1;
			// 
			// deviceProvidersLabel
			// 
			deviceProvidersLabel.Anchor = AnchorStyles.None;
			deviceProvidersLabel.AutoSize = true;
			deviceProvidersLabel.Location = new Point(120, 0);
			deviceProvidersLabel.Name = "deviceProvidersLabel";
			deviceProvidersLabel.Size = new Size(78, 15);
			deviceProvidersLabel.TabIndex = 2;
			deviceProvidersLabel.Text = "Получено от";
			// 
			// editDeviceProviderBtn
			// 
			editDeviceProviderBtn.Anchor = AnchorStyles.None;
			editDeviceProviderBtn.AutoSize = true;
			editDeviceProviderBtn.Location = new Point(96, 18);
			editDeviceProviderBtn.Name = "editDeviceProviderBtn";
			editDeviceProviderBtn.Size = new Size(126, 25);
			editDeviceProviderBtn.TabIndex = 1;
			editDeviceProviderBtn.Text = "Добавить/изменить";
			editDeviceProviderBtn.UseVisualStyleBackColor = true;
			editDeviceProviderBtn.Click += deviceProviderEditBtn_Click;
			// 
			// deviceProvidersLBox
			// 
			deviceProvidersLBox.Dock = DockStyle.Fill;
			deviceProvidersLBox.FormattingEnabled = true;
			deviceProvidersLBox.ItemHeight = 15;
			deviceProvidersLBox.Location = new Point(3, 49);
			deviceProvidersLBox.Name = "deviceProvidersLBox";
			deviceProvidersLBox.Size = new Size(312, 345);
			deviceProvidersLBox.TabIndex = 0;
			// 
			// tabPage3
			// 
			tabPage3.Controls.Add(tableLayoutPanel1);
			tabPage3.Location = new Point(4, 24);
			tabPage3.Name = "tabPage3";
			tabPage3.Padding = new Padding(3);
			tabPage3.Size = new Size(976, 409);
			tabPage3.TabIndex = 4;
			tabPage3.Text = "История перемещений";
			tabPage3.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel1.Controls.Add(historySearchTBox, 1, 0);
			tableLayoutPanel1.Controls.Add(historySearchLabel, 0, 0);
			tableLayoutPanel1.Controls.Add(historyDGW, 0, 1);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(3, 3);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 2;
			tableLayoutPanel1.RowStyles.Add(new RowStyle());
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			tableLayoutPanel1.Size = new Size(970, 403);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// historySearchTBox
			// 
			historySearchTBox.Dock = DockStyle.Fill;
			historySearchTBox.Location = new Point(51, 3);
			historySearchTBox.Name = "historySearchTBox";
			historySearchTBox.Size = new Size(916, 23);
			historySearchTBox.TabIndex = 15;
			historySearchTBox.TextChanged += historySearchTBox_TextChanged;
			// 
			// historySearchLabel
			// 
			historySearchLabel.Anchor = AnchorStyles.None;
			historySearchLabel.AutoSize = true;
			historySearchLabel.Location = new Point(3, 7);
			historySearchLabel.Name = "historySearchLabel";
			historySearchLabel.Size = new Size(42, 15);
			historySearchLabel.TabIndex = 14;
			historySearchLabel.Text = "Поиск";
			// 
			// historyDGW
			// 
			historyDGW.AllowUserToAddRows = false;
			historyDGW.AllowUserToDeleteRows = false;
			historyDGW.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			historyDGW.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			historyDGW.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			tableLayoutPanel1.SetColumnSpan(historyDGW, 2);
			historyDGW.Dock = DockStyle.Fill;
			historyDGW.Location = new Point(3, 32);
			historyDGW.MultiSelect = false;
			historyDGW.Name = "historyDGW";
			historyDGW.ReadOnly = true;
			historyDGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			historyDGW.Size = new Size(964, 368);
			historyDGW.TabIndex = 16;
			// 
			// menuStrip
			// 
			menuStrip.Items.AddRange(new ToolStripItem[] { refreshToolStripMenuItem });
			menuStrip.Location = new Point(0, 0);
			menuStrip.Name = "menuStrip";
			menuStrip.Size = new Size(984, 24);
			menuStrip.TabIndex = 1;
			menuStrip.Text = "menuStrip1";
			// 
			// refreshToolStripMenuItem
			// 
			refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			refreshToolStripMenuItem.Size = new Size(73, 20);
			refreshToolStripMenuItem.Text = "Обновить";
			refreshToolStripMenuItem.Click += refreshToolStripMenuItem_Click;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(984, 461);
			Controls.Add(tabControl);
			Controls.Add(menuStrip);
			Icon = (Icon)resources.GetObject("$this.Icon");
			MainMenuStrip = menuStrip;
			Name = "MainForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Инвентаризация";
			tabControl.ResumeLayout(false);
			tabPage1.ResumeLayout(false);
			devicesTLP.ResumeLayout(false);
			devicesTLP.PerformLayout();
			devicesTLPLeft.ResumeLayout(false);
			devicesTLPLeft.PerformLayout();
			devicesTLPRight.ResumeLayout(false);
			devicesTLPRight.PerformLayout();
			moveLeftRightBtnsTLP.ResumeLayout(false);
			moveLeftRightBtnsTLP.PerformLayout();
			tabPage2.ResumeLayout(false);
			deviceTypesNamesProvidersTLP.ResumeLayout(false);
			deviceTypesNamesGBox.ResumeLayout(false);
			deviceTypesNamesTLP.ResumeLayout(false);
			deviceTypesNamesTLP.PerformLayout();
			deviceProvidersTLP.ResumeLayout(false);
			deviceProvidersTLP.PerformLayout();
			tabPage3.ResumeLayout(false);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)historyDGW).EndInit();
			menuStrip.ResumeLayout(false);
			menuStrip.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TabControl tabControl;
		private TabPage tabPage1;
		private TableLayoutPanel devicesTLP;
		private TableLayoutPanel devicesTLPLeft;		
		private TableLayoutPanel devicesTLPRight;
		private TableLayoutPanel moveLeftRightBtnsTLP;
		private Button moveCabinetToRightBtn;
		private Button moveCabinetToLeftBtn;
		private ListBox buildingsLBoxLeft;
		private ListBox cabinetsLBoxLeft;
		private ListBox buildingsLBoxRight;
		private ListBox cabinetsLBoxRight;
		private Button editBuildingBtnLeft;
		private Button editCabinetBtnLeft;
		private Button editBuildingBtnRight;
		private Button editCabinetBtnRight;
		private TabPage tabPage2;
		private TableLayoutPanel deviceTypesNamesProvidersTLP;
		private TableLayoutPanel deviceTypesNamesTLP;
		private ListBox deviceTypesLBox;
		private Button editDeviceTypeBtn;
		private TableLayoutPanel deviceProvidersTLP;
		private ListBox deviceProvidersLBox;
		private Button editDeviceProviderBtn;
		private Button editDeviceNameBtn;
		private Label buildingsLabelLeft;
		private Label cabinetsLabelLeft;
		private Label cabinetsLabelRight;
		private Label buildingsLabelRight;
		private Label deviceNamesLabel;
		private Label deviceTypesLabel;
		private Label deviceProvidersLabel;
		private Label complectsLabelLeft;
		private Button editComplectBtnLeft;
		private ListBox complectsLBoxLeft;
		private Label devicesLabelLeft;
		private Button editDeviceBtnLeft;
		private Button moveComplectToLeftBtn;
		private Button moveComplectToRightBtn;
		private Label complectsLabelRight;
		private Button editComplectBtnRight;
		private ListBox complectsLBoxRight;
		private Button moveDeviceToLeftBtn;
		private Button moveDeviceToRightBtn;
		private Label devicesLabelRight;
		private Button editDeviceBtnRight;
		private Label searchLabelLeft;
		private TextBox searchTBoxLeft;
		private Label searchLabelRight;
		private TextBox searchTBoxRight;
		private MenuStrip menuStrip;
		private ToolStripMenuItem refreshToolStripMenuItem;
		private GroupBox deviceTypesNamesGBox;
		private ListBox deviceNamesLBox;
		private ListBox devicesLBoxLeft;
		private ListBox devicesLBoxRight;
		private TabPage tabPage3;
		private TableLayoutPanel tableLayoutPanel1;
		private TextBox historySearchTBox;
		private Label historySearchLabel;
		private DataGridView historyDGW;
	}
}
