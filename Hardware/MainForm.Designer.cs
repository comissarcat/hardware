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
			tabControl1 = new TabControl();
			tabPage1 = new TabPage();
			tableLayoutPanel1 = new TableLayoutPanel();
			tableLayoutPanel2 = new TableLayoutPanel();
			devicesLBoxLeft = new ListBox();
			label18 = new Label();
			editDeviceBtnLeft = new Button();
			label10 = new Label();
			editComplectBtnLeft = new Button();
			complectsLBoxLeft = new ListBox();
			buildingsLBoxLeft = new ListBox();
			cabinetsLBoxLeft = new ListBox();
			editBuildingBtnLeft = new Button();
			editCabinetBtnLeft = new Button();
			label1 = new Label();
			label2 = new Label();
			label7 = new Label();
			searchTBoxLeft = new TextBox();
			tableLayoutPanel3 = new TableLayoutPanel();
			devicesLBoxRight = new ListBox();
			searchTBoxRight = new TextBox();
			label8 = new Label();
			label20 = new Label();
			editDeviceBtnRight = new Button();
			label12 = new Label();
			editComplectBtnRight = new Button();
			complectsLBoxRight = new ListBox();
			label6 = new Label();
			label5 = new Label();
			buildingsLBoxRight = new ListBox();
			cabinetsLBoxRight = new ListBox();
			editBuildingBtnRight = new Button();
			editCabinetBtnRight = new Button();
			tableLayoutPanel4 = new TableLayoutPanel();
			moveDeviceToLeftBtn = new Button();
			moveDeviceToRightBtn = new Button();
			moveComplectToLeftBtn = new Button();
			moveComplectToRightBtn = new Button();
			moveCabinetToRightBtn = new Button();
			moveCabinetToLeftBtn = new Button();
			tabPage4 = new TabPage();
			tableLayoutPanel18 = new TableLayoutPanel();
			tableLayoutPanel20 = new TableLayoutPanel();
			deviceProvidersLBox = new ListBox();
			editDeviceProviderBtn = new Button();
			label23 = new Label();
			groupBox1 = new GroupBox();
			tableLayoutPanel19 = new TableLayoutPanel();
			deviceNamesLBox = new ListBox();
			deviceTypesLBox = new ListBox();
			editDeviceNameBtn = new Button();
			editDeviceTypeBtn = new Button();
			label22 = new Label();
			label21 = new Label();
			tableLayoutPanel7 = new TableLayoutPanel();
			tableLayoutPanel8 = new TableLayoutPanel();
			button8 = new Button();
			button9 = new Button();
			button10 = new Button();
			textBox5 = new TextBox();
			textBox6 = new TextBox();
			label3 = new Label();
			tableLayoutPanel9 = new TableLayoutPanel();
			label4 = new Label();
			textBox7 = new TextBox();
			textBox8 = new TextBox();
			button11 = new Button();
			button12 = new Button();
			button13 = new Button();
			tableLayoutPanel10 = new TableLayoutPanel();
			tableLayoutPanel11 = new TableLayoutPanel();
			menuStrip1 = new MenuStrip();
			refreshToolStripMenuItem = new ToolStripMenuItem();
			tabControl1.SuspendLayout();
			tabPage1.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			tableLayoutPanel2.SuspendLayout();
			tableLayoutPanel3.SuspendLayout();
			tableLayoutPanel4.SuspendLayout();
			tabPage4.SuspendLayout();
			tableLayoutPanel18.SuspendLayout();
			tableLayoutPanel20.SuspendLayout();
			groupBox1.SuspendLayout();
			tableLayoutPanel19.SuspendLayout();
			tableLayoutPanel7.SuspendLayout();
			tableLayoutPanel8.SuspendLayout();
			tableLayoutPanel9.SuspendLayout();
			menuStrip1.SuspendLayout();
			SuspendLayout();
			// 
			// tabControl1
			// 
			tabControl1.Controls.Add(tabPage1);
			tabControl1.Controls.Add(tabPage4);
			tabControl1.Dock = DockStyle.Fill;
			tabControl1.Location = new Point(0, 24);
			tabControl1.Name = "tabControl1";
			tabControl1.SelectedIndex = 0;
			tabControl1.Size = new Size(984, 437);
			tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			tabPage1.Controls.Add(tableLayoutPanel1);
			tabPage1.Location = new Point(4, 24);
			tabPage1.Name = "tabPage1";
			tabPage1.Padding = new Padding(3);
			tabPage1.Size = new Size(976, 409);
			tabPage1.TabIndex = 0;
			tabPage1.Text = "Перемещение техники";
			tabPage1.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 3;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0000076F));
			tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
			tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 2, 0);
			tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 1, 0);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(3, 3);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.Size = new Size(970, 403);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.ColumnCount = 4;
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
			tableLayoutPanel2.Controls.Add(devicesLBoxLeft, 3, 3);
			tableLayoutPanel2.Controls.Add(label18, 3, 1);
			tableLayoutPanel2.Controls.Add(editDeviceBtnLeft, 3, 2);
			tableLayoutPanel2.Controls.Add(label10, 2, 1);
			tableLayoutPanel2.Controls.Add(editComplectBtnLeft, 2, 2);
			tableLayoutPanel2.Controls.Add(complectsLBoxLeft, 2, 3);
			tableLayoutPanel2.Controls.Add(buildingsLBoxLeft, 0, 3);
			tableLayoutPanel2.Controls.Add(cabinetsLBoxLeft, 1, 3);
			tableLayoutPanel2.Controls.Add(editBuildingBtnLeft, 0, 2);
			tableLayoutPanel2.Controls.Add(editCabinetBtnLeft, 1, 2);
			tableLayoutPanel2.Controls.Add(label1, 0, 1);
			tableLayoutPanel2.Controls.Add(label2, 1, 1);
			tableLayoutPanel2.Controls.Add(label7, 0, 0);
			tableLayoutPanel2.Controls.Add(searchTBoxLeft, 1, 0);
			tableLayoutPanel2.Dock = DockStyle.Fill;
			tableLayoutPanel2.Location = new Point(3, 3);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 4;
			tableLayoutPanel2.RowStyles.Add(new RowStyle());
			tableLayoutPanel2.RowStyles.Add(new RowStyle());
			tableLayoutPanel2.RowStyles.Add(new RowStyle());
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel2.Size = new Size(419, 397);
			tableLayoutPanel2.TabIndex = 0;
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
			// label18
			// 
			label18.Anchor = AnchorStyles.None;
			label18.AutoSize = true;
			label18.Location = new Point(282, 29);
			label18.Name = "label18";
			label18.Size = new Size(103, 15);
			label18.TabIndex = 11;
			label18.Text = "Единицы техники";
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
			// label10
			// 
			label10.Anchor = AnchorStyles.None;
			label10.AutoSize = true;
			label10.Location = new Point(172, 29);
			label10.Name = "label10";
			label10.Size = new Size(70, 15);
			label10.TabIndex = 8;
			label10.Text = "Комплекты";
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
			// label1
			// 
			label1.Anchor = AnchorStyles.None;
			label1.AutoSize = true;
			label1.Location = new Point(18, 29);
			label1.Name = "label1";
			label1.Size = new Size(46, 15);
			label1.TabIndex = 4;
			label1.Text = "Здания";
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.None;
			label2.AutoSize = true;
			label2.Location = new Point(94, 29);
			label2.Name = "label2";
			label2.Size = new Size(61, 15);
			label2.TabIndex = 5;
			label2.Text = "Кабинеты";
			// 
			// label7
			// 
			label7.Anchor = AnchorStyles.None;
			label7.AutoSize = true;
			label7.Location = new Point(20, 7);
			label7.Name = "label7";
			label7.Size = new Size(42, 15);
			label7.TabIndex = 13;
			label7.Text = "Поиск";
			// 
			// searchTBoxLeft
			// 
			tableLayoutPanel2.SetColumnSpan(searchTBoxLeft, 3);
			searchTBoxLeft.Dock = DockStyle.Fill;
			searchTBoxLeft.Location = new Point(86, 3);
			searchTBoxLeft.Name = "searchTBoxLeft";
			searchTBoxLeft.Size = new Size(330, 23);
			searchTBoxLeft.TabIndex = 14;
			searchTBoxLeft.TextChanged += searchTBox_TextChanged;
			// 
			// tableLayoutPanel3
			// 
			tableLayoutPanel3.ColumnCount = 4;
			tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
			tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
			tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
			tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
			tableLayoutPanel3.Controls.Add(devicesLBoxRight, 3, 3);
			tableLayoutPanel3.Controls.Add(searchTBoxRight, 1, 0);
			tableLayoutPanel3.Controls.Add(label8, 0, 0);
			tableLayoutPanel3.Controls.Add(label20, 3, 1);
			tableLayoutPanel3.Controls.Add(editDeviceBtnRight, 3, 2);
			tableLayoutPanel3.Controls.Add(label12, 2, 1);
			tableLayoutPanel3.Controls.Add(editComplectBtnRight, 2, 2);
			tableLayoutPanel3.Controls.Add(complectsLBoxRight, 2, 3);
			tableLayoutPanel3.Controls.Add(label6, 1, 1);
			tableLayoutPanel3.Controls.Add(label5, 0, 1);
			tableLayoutPanel3.Controls.Add(buildingsLBoxRight, 0, 3);
			tableLayoutPanel3.Controls.Add(cabinetsLBoxRight, 1, 3);
			tableLayoutPanel3.Controls.Add(editBuildingBtnRight, 0, 2);
			tableLayoutPanel3.Controls.Add(editCabinetBtnRight, 1, 2);
			tableLayoutPanel3.Dock = DockStyle.Fill;
			tableLayoutPanel3.Location = new Point(547, 3);
			tableLayoutPanel3.Name = "tableLayoutPanel3";
			tableLayoutPanel3.RowCount = 4;
			tableLayoutPanel3.RowStyles.Add(new RowStyle());
			tableLayoutPanel3.RowStyles.Add(new RowStyle());
			tableLayoutPanel3.RowStyles.Add(new RowStyle());
			tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel3.Size = new Size(420, 397);
			tableLayoutPanel3.TabIndex = 1;
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
			// searchTBoxRight
			// 
			tableLayoutPanel3.SetColumnSpan(searchTBoxRight, 3);
			searchTBoxRight.Dock = DockStyle.Fill;
			searchTBoxRight.Location = new Point(87, 3);
			searchTBoxRight.Name = "searchTBoxRight";
			searchTBoxRight.Size = new Size(330, 23);
			searchTBoxRight.TabIndex = 15;
			searchTBoxRight.TextChanged += searchTBox_TextChanged;
			// 
			// label8
			// 
			label8.Anchor = AnchorStyles.None;
			label8.AutoSize = true;
			label8.Location = new Point(21, 7);
			label8.Name = "label8";
			label8.Size = new Size(42, 15);
			label8.TabIndex = 14;
			label8.Text = "Поиск";
			// 
			// label20
			// 
			label20.Anchor = AnchorStyles.None;
			label20.AutoSize = true;
			label20.Location = new Point(284, 29);
			label20.Name = "label20";
			label20.Size = new Size(103, 15);
			label20.TabIndex = 12;
			label20.Text = "Единицы техники";
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
			// label12
			// 
			label12.Anchor = AnchorStyles.None;
			label12.AutoSize = true;
			label12.Location = new Point(175, 29);
			label12.Name = "label12";
			label12.Size = new Size(70, 15);
			label12.TabIndex = 9;
			label12.Text = "Комплекты";
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
			// label6
			// 
			label6.Anchor = AnchorStyles.None;
			label6.AutoSize = true;
			label6.Location = new Point(95, 29);
			label6.Name = "label6";
			label6.Size = new Size(61, 15);
			label6.TabIndex = 6;
			label6.Text = "Кабинеты";
			// 
			// label5
			// 
			label5.Anchor = AnchorStyles.None;
			label5.AutoSize = true;
			label5.Location = new Point(19, 29);
			label5.Name = "label5";
			label5.Size = new Size(46, 15);
			label5.TabIndex = 5;
			label5.Text = "Здания";
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
			// tableLayoutPanel4
			// 
			tableLayoutPanel4.Anchor = AnchorStyles.None;
			tableLayoutPanel4.AutoSize = true;
			tableLayoutPanel4.ColumnCount = 1;
			tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel4.Controls.Add(moveDeviceToLeftBtn, 0, 7);
			tableLayoutPanel4.Controls.Add(moveDeviceToRightBtn, 0, 6);
			tableLayoutPanel4.Controls.Add(moveComplectToLeftBtn, 0, 4);
			tableLayoutPanel4.Controls.Add(moveComplectToRightBtn, 0, 3);
			tableLayoutPanel4.Controls.Add(moveCabinetToRightBtn, 0, 0);
			tableLayoutPanel4.Controls.Add(moveCabinetToLeftBtn, 0, 1);
			tableLayoutPanel4.Location = new Point(428, 77);
			tableLayoutPanel4.Name = "tableLayoutPanel4";
			tableLayoutPanel4.RowCount = 8;
			tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			tableLayoutPanel4.Size = new Size(113, 248);
			tableLayoutPanel4.TabIndex = 2;
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
			// tabPage4
			// 
			tabPage4.Controls.Add(tableLayoutPanel18);
			tabPage4.Location = new Point(4, 24);
			tabPage4.Name = "tabPage4";
			tabPage4.Padding = new Padding(3);
			tabPage4.Size = new Size(976, 409);
			tabPage4.TabIndex = 3;
			tabPage4.Text = "Прочие настройки";
			tabPage4.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel18
			// 
			tableLayoutPanel18.ColumnCount = 2;
			tableLayoutPanel18.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.6666641F));
			tableLayoutPanel18.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
			tableLayoutPanel18.Controls.Add(tableLayoutPanel20, 1, 0);
			tableLayoutPanel18.Controls.Add(groupBox1, 0, 0);
			tableLayoutPanel18.Dock = DockStyle.Fill;
			tableLayoutPanel18.Location = new Point(3, 3);
			tableLayoutPanel18.Name = "tableLayoutPanel18";
			tableLayoutPanel18.RowCount = 1;
			tableLayoutPanel18.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel18.Size = new Size(970, 403);
			tableLayoutPanel18.TabIndex = 0;
			// 
			// tableLayoutPanel20
			// 
			tableLayoutPanel20.ColumnCount = 1;
			tableLayoutPanel20.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel20.Controls.Add(deviceProvidersLBox, 0, 1);
			tableLayoutPanel20.Controls.Add(editDeviceProviderBtn, 0, 0);
			tableLayoutPanel20.Controls.Add(label23, 0, 0);
			tableLayoutPanel20.Dock = DockStyle.Fill;
			tableLayoutPanel20.Location = new Point(649, 3);
			tableLayoutPanel20.Name = "tableLayoutPanel20";
			tableLayoutPanel20.RowCount = 3;
			tableLayoutPanel20.RowStyles.Add(new RowStyle());
			tableLayoutPanel20.RowStyles.Add(new RowStyle());
			tableLayoutPanel20.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel20.Size = new Size(318, 397);
			tableLayoutPanel20.TabIndex = 1;
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
			// label23
			// 
			label23.Anchor = AnchorStyles.None;
			label23.AutoSize = true;
			label23.Location = new Point(120, 0);
			label23.Name = "label23";
			label23.Size = new Size(78, 15);
			label23.TabIndex = 2;
			label23.Text = "Получено от";
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(tableLayoutPanel19);
			groupBox1.Dock = DockStyle.Fill;
			groupBox1.Location = new Point(3, 3);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(640, 397);
			groupBox1.TabIndex = 3;
			groupBox1.TabStop = false;
			groupBox1.Text = "Типы и названия";
			// 
			// tableLayoutPanel19
			// 
			tableLayoutPanel19.ColumnCount = 2;
			tableLayoutPanel19.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanel19.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanel19.Controls.Add(deviceNamesLBox, 1, 2);
			tableLayoutPanel19.Controls.Add(deviceTypesLBox, 0, 2);
			tableLayoutPanel19.Controls.Add(editDeviceNameBtn, 1, 1);
			tableLayoutPanel19.Controls.Add(editDeviceTypeBtn, 0, 1);
			tableLayoutPanel19.Controls.Add(label22, 1, 0);
			tableLayoutPanel19.Controls.Add(label21, 0, 0);
			tableLayoutPanel19.Dock = DockStyle.Fill;
			tableLayoutPanel19.Location = new Point(3, 19);
			tableLayoutPanel19.Name = "tableLayoutPanel19";
			tableLayoutPanel19.RowCount = 3;
			tableLayoutPanel19.RowStyles.Add(new RowStyle());
			tableLayoutPanel19.RowStyles.Add(new RowStyle());
			tableLayoutPanel19.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel19.Size = new Size(634, 375);
			tableLayoutPanel19.TabIndex = 0;
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
			// label22
			// 
			label22.Anchor = AnchorStyles.None;
			label22.AutoSize = true;
			label22.Location = new Point(422, 0);
			label22.Name = "label22";
			label22.Size = new Size(106, 15);
			label22.TabIndex = 2;
			label22.Text = "Названия техники";
			// 
			// label21
			// 
			label21.Anchor = AnchorStyles.None;
			label21.AutoSize = true;
			label21.Location = new Point(117, 0);
			label21.Name = "label21";
			label21.Size = new Size(83, 15);
			label21.TabIndex = 2;
			label21.Text = "Типы техники";
			// 
			// tableLayoutPanel7
			// 
			tableLayoutPanel7.ColumnCount = 4;
			tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
			tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
			tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
			tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
			tableLayoutPanel7.Controls.Add(tableLayoutPanel8, 0, 0);
			tableLayoutPanel7.Controls.Add(tableLayoutPanel9, 1, 0);
			tableLayoutPanel7.Controls.Add(tableLayoutPanel10, 2, 0);
			tableLayoutPanel7.Location = new Point(0, 0);
			tableLayoutPanel7.Name = "tableLayoutPanel7";
			tableLayoutPanel7.RowCount = 1;
			tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			tableLayoutPanel7.Size = new Size(200, 100);
			tableLayoutPanel7.TabIndex = 0;
			// 
			// tableLayoutPanel8
			// 
			tableLayoutPanel8.ColumnCount = 3;
			tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
			tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
			tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
			tableLayoutPanel8.Controls.Add(button8, 0, 2);
			tableLayoutPanel8.Controls.Add(button9, 1, 2);
			tableLayoutPanel8.Controls.Add(button10, 2, 2);
			tableLayoutPanel8.Controls.Add(textBox5, 1, 1);
			tableLayoutPanel8.Controls.Add(textBox6, 0, 1);
			tableLayoutPanel8.Controls.Add(label3, 0, 0);
			tableLayoutPanel8.Location = new Point(3, 3);
			tableLayoutPanel8.Name = "tableLayoutPanel8";
			tableLayoutPanel8.RowCount = 3;
			tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
			tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
			tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
			tableLayoutPanel8.Size = new Size(44, 94);
			tableLayoutPanel8.TabIndex = 0;
			// 
			// button8
			// 
			button8.Location = new Point(3, 65);
			button8.Name = "button8";
			button8.Size = new Size(8, 23);
			button8.TabIndex = 0;
			button8.Text = "button8";
			button8.UseVisualStyleBackColor = true;
			// 
			// button9
			// 
			button9.Location = new Point(17, 65);
			button9.Name = "button9";
			button9.Size = new Size(8, 23);
			button9.TabIndex = 1;
			button9.Text = "button9";
			button9.UseVisualStyleBackColor = true;
			// 
			// button10
			// 
			button10.Location = new Point(31, 65);
			button10.Name = "button10";
			button10.Size = new Size(10, 23);
			button10.TabIndex = 2;
			button10.Text = "button10";
			button10.UseVisualStyleBackColor = true;
			// 
			// textBox5
			// 
			tableLayoutPanel8.SetColumnSpan(textBox5, 2);
			textBox5.Location = new Point(17, 34);
			textBox5.Name = "textBox5";
			textBox5.Size = new Size(24, 23);
			textBox5.TabIndex = 3;
			// 
			// textBox6
			// 
			textBox6.Location = new Point(3, 34);
			textBox6.Name = "textBox6";
			textBox6.Size = new Size(8, 23);
			textBox6.TabIndex = 4;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(3, 0);
			label3.Name = "label3";
			label3.Size = new Size(8, 31);
			label3.TabIndex = 5;
			label3.Text = "Здание";
			// 
			// tableLayoutPanel9
			// 
			tableLayoutPanel9.ColumnCount = 3;
			tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
			tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
			tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
			tableLayoutPanel9.Controls.Add(label4, 0, 0);
			tableLayoutPanel9.Controls.Add(textBox7, 0, 1);
			tableLayoutPanel9.Controls.Add(textBox8, 1, 1);
			tableLayoutPanel9.Controls.Add(button11, 0, 2);
			tableLayoutPanel9.Controls.Add(button12, 1, 2);
			tableLayoutPanel9.Controls.Add(button13, 2, 2);
			tableLayoutPanel9.Location = new Point(53, 3);
			tableLayoutPanel9.Name = "tableLayoutPanel9";
			tableLayoutPanel9.RowCount = 3;
			tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
			tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
			tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
			tableLayoutPanel9.Size = new Size(44, 94);
			tableLayoutPanel9.TabIndex = 1;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(3, 0);
			label4.Name = "label4";
			label4.Size = new Size(8, 31);
			label4.TabIndex = 0;
			label4.Text = "Кабинет";
			// 
			// textBox7
			// 
			textBox7.Location = new Point(3, 34);
			textBox7.Name = "textBox7";
			textBox7.Size = new Size(8, 23);
			textBox7.TabIndex = 1;
			// 
			// textBox8
			// 
			textBox8.Location = new Point(17, 34);
			textBox8.Name = "textBox8";
			textBox8.Size = new Size(8, 23);
			textBox8.TabIndex = 2;
			// 
			// button11
			// 
			button11.Location = new Point(3, 65);
			button11.Name = "button11";
			button11.Size = new Size(8, 23);
			button11.TabIndex = 3;
			button11.Text = "button11";
			button11.UseVisualStyleBackColor = true;
			// 
			// button12
			// 
			button12.Location = new Point(17, 65);
			button12.Name = "button12";
			button12.Size = new Size(8, 23);
			button12.TabIndex = 4;
			button12.Text = "button12";
			button12.UseVisualStyleBackColor = true;
			// 
			// button13
			// 
			button13.Location = new Point(31, 65);
			button13.Name = "button13";
			button13.Size = new Size(10, 23);
			button13.TabIndex = 5;
			button13.Text = "button13";
			button13.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel10
			// 
			tableLayoutPanel10.ColumnCount = 3;
			tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
			tableLayoutPanel10.Location = new Point(103, 3);
			tableLayoutPanel10.Name = "tableLayoutPanel10";
			tableLayoutPanel10.RowCount = 3;
			tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			tableLayoutPanel10.Size = new Size(44, 94);
			tableLayoutPanel10.TabIndex = 2;
			// 
			// tableLayoutPanel11
			// 
			tableLayoutPanel11.ColumnCount = 3;
			tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanel11.Location = new Point(0, 0);
			tableLayoutPanel11.Name = "tableLayoutPanel11";
			tableLayoutPanel11.RowCount = 3;
			tableLayoutPanel11.Size = new Size(200, 100);
			tableLayoutPanel11.TabIndex = 0;
			// 
			// menuStrip1
			// 
			menuStrip1.Items.AddRange(new ToolStripItem[] { refreshToolStripMenuItem });
			menuStrip1.Location = new Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new Size(984, 24);
			menuStrip1.TabIndex = 1;
			menuStrip1.Text = "menuStrip1";
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
			Controls.Add(tabControl1);
			Controls.Add(menuStrip1);
			Icon = (Icon)resources.GetObject("$this.Icon");
			MainMenuStrip = menuStrip1;
			Name = "MainForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Инвентаризация";
			tabControl1.ResumeLayout(false);
			tabPage1.ResumeLayout(false);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			tableLayoutPanel2.ResumeLayout(false);
			tableLayoutPanel2.PerformLayout();
			tableLayoutPanel3.ResumeLayout(false);
			tableLayoutPanel3.PerformLayout();
			tableLayoutPanel4.ResumeLayout(false);
			tableLayoutPanel4.PerformLayout();
			tabPage4.ResumeLayout(false);
			tableLayoutPanel18.ResumeLayout(false);
			tableLayoutPanel20.ResumeLayout(false);
			tableLayoutPanel20.PerformLayout();
			groupBox1.ResumeLayout(false);
			tableLayoutPanel19.ResumeLayout(false);
			tableLayoutPanel19.PerformLayout();
			tableLayoutPanel7.ResumeLayout(false);
			tableLayoutPanel8.ResumeLayout(false);
			tableLayoutPanel8.PerformLayout();
			tableLayoutPanel9.ResumeLayout(false);
			tableLayoutPanel9.PerformLayout();
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TabControl tabControl1;
		private TabPage tabPage1;
		private TableLayoutPanel tableLayoutPanel1;
		private TableLayoutPanel tableLayoutPanel2;
		private TableLayoutPanel tableLayoutPanel7;
		private TableLayoutPanel tableLayoutPanel8;
		private Button button8;
		private Button button9;
		private Button button10;
		private TextBox textBox5;
		private TextBox textBox6;
		private Label label3;
		private TableLayoutPanel tableLayoutPanel9;
		private Label label4;
		private TextBox textBox7;
		private TextBox textBox8;
		private Button button11;
		private Button button12;
		private Button button13;
		private TableLayoutPanel tableLayoutPanel10;
		private TableLayoutPanel tableLayoutPanel11;
		private TableLayoutPanel tableLayoutPanel3;
		private TableLayoutPanel tableLayoutPanel4;
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
		private TabPage tabPage4;
		private TableLayoutPanel tableLayoutPanel18;
		private TableLayoutPanel tableLayoutPanel19;
		private ListBox deviceTypesLBox;
		private Button editDeviceTypeBtn;
		private TableLayoutPanel tableLayoutPanel20;
		private ListBox deviceProvidersLBox;
		private Button editDeviceProviderBtn;
		private Button editDeviceNameBtn;
		private Label label1;
		private Label label2;
		private Label label6;
		private Label label5;
		private Label label22;
		private Label label21;
		private Label label23;
		private Label label10;
		private Button editComplectBtnLeft;
		private ListBox complectsLBoxLeft;
		private Label label18;
		private Button editDeviceBtnLeft;
		private Button moveComplectToLeftBtn;
		private Button moveComplectToRightBtn;
		private Label label12;
		private Button editComplectBtnRight;
		private ListBox complectsLBoxRight;
		private Button moveDeviceToLeftBtn;
		private Button moveDeviceToRightBtn;
		private Label label20;
		private Button editDeviceBtnRight;
		private Label label7;
		private TextBox searchTBoxLeft;
		private Label label8;
		private TextBox searchTBoxRight;
		private MenuStrip menuStrip1;
		private ToolStripMenuItem refreshToolStripMenuItem;
		private GroupBox groupBox1;
		private ListBox deviceNamesLBox;
		private ListBox devicesLBoxLeft;
		private ListBox devicesLBoxRight;
	}
}
