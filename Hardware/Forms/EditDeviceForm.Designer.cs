namespace Hardware.Forms
{
	partial class EditDeviceForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditDeviceForm));
			tableLayoutPanel1 = new TableLayoutPanel();
			tableLayoutPanel2 = new TableLayoutPanel();
			editComplectBtn = new Button();
			complectCBox = new ComboBox();
			label8 = new Label();
			inventoryTBox = new TextBox();
			serialTBox = new TextBox();
			label7 = new Label();
			label6 = new Label();
			editDeviceProviderBtn = new Button();
			deviceProviderCBox = new ComboBox();
			label5 = new Label();
			editDeviceNameBtn = new Button();
			deviceNameCBox = new ComboBox();
			label4 = new Label();
			cabinetCBox = new ComboBox();
			label3 = new Label();
			label1 = new Label();
			label2 = new Label();
			idTBox = new TextBox();
			buildingCBox = new ComboBox();
			editBuildingBtn = new Button();
			editCabinetBtn = new Button();
			tableLayoutPanel3 = new TableLayoutPanel();
			addBtn = new Button();
			editBtn = new Button();
			removeBtn = new Button();
			cancelBtn = new Button();
			tableLayoutPanel1.SuspendLayout();
			tableLayoutPanel2.SuspendLayout();
			tableLayoutPanel3.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 1;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
			tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 1);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 2;
			tableLayoutPanel1.RowStyles.Add(new RowStyle());
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.Size = new Size(534, 311);
			tableLayoutPanel1.TabIndex = 3;
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.AutoSize = true;
			tableLayoutPanel2.ColumnCount = 3;
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel2.Controls.Add(editComplectBtn, 2, 3);
			tableLayoutPanel2.Controls.Add(complectCBox, 1, 3);
			tableLayoutPanel2.Controls.Add(label8, 0, 3);
			tableLayoutPanel2.Controls.Add(inventoryTBox, 1, 7);
			tableLayoutPanel2.Controls.Add(serialTBox, 1, 6);
			tableLayoutPanel2.Controls.Add(label7, 0, 7);
			tableLayoutPanel2.Controls.Add(label6, 0, 6);
			tableLayoutPanel2.Controls.Add(editDeviceProviderBtn, 2, 5);
			tableLayoutPanel2.Controls.Add(deviceProviderCBox, 1, 5);
			tableLayoutPanel2.Controls.Add(label5, 0, 5);
			tableLayoutPanel2.Controls.Add(editDeviceNameBtn, 2, 4);
			tableLayoutPanel2.Controls.Add(deviceNameCBox, 1, 4);
			tableLayoutPanel2.Controls.Add(label4, 0, 2);
			tableLayoutPanel2.Controls.Add(cabinetCBox, 1, 2);
			tableLayoutPanel2.Controls.Add(label3, 0, 4);
			tableLayoutPanel2.Controls.Add(label1, 0, 0);
			tableLayoutPanel2.Controls.Add(label2, 0, 1);
			tableLayoutPanel2.Controls.Add(idTBox, 1, 0);
			tableLayoutPanel2.Controls.Add(buildingCBox, 1, 1);
			tableLayoutPanel2.Controls.Add(editBuildingBtn, 2, 1);
			tableLayoutPanel2.Controls.Add(editCabinetBtn, 2, 2);
			tableLayoutPanel2.Dock = DockStyle.Fill;
			tableLayoutPanel2.Location = new Point(3, 3);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 8;
			tableLayoutPanel2.RowStyles.Add(new RowStyle());
			tableLayoutPanel2.RowStyles.Add(new RowStyle());
			tableLayoutPanel2.RowStyles.Add(new RowStyle());
			tableLayoutPanel2.RowStyles.Add(new RowStyle());
			tableLayoutPanel2.RowStyles.Add(new RowStyle());
			tableLayoutPanel2.RowStyles.Add(new RowStyle());
			tableLayoutPanel2.RowStyles.Add(new RowStyle());
			tableLayoutPanel2.RowStyles.Add(new RowStyle());
			tableLayoutPanel2.Size = new Size(528, 242);
			tableLayoutPanel2.TabIndex = 0;
			// 
			// editComplectBtn
			// 
			editComplectBtn.Anchor = AnchorStyles.None;
			editComplectBtn.AutoSize = true;
			editComplectBtn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			editComplectBtn.Location = new Point(399, 94);
			editComplectBtn.Name = "editComplectBtn";
			editComplectBtn.Size = new Size(126, 25);
			editComplectBtn.TabIndex = 21;
			editComplectBtn.Text = "Добавить/изменить";
			editComplectBtn.UseVisualStyleBackColor = true;
			editComplectBtn.Click += editComplectBtn_Click;
			// 
			// complectCBox
			// 
			complectCBox.Dock = DockStyle.Fill;
			complectCBox.FormattingEnabled = true;
			complectCBox.Location = new Point(131, 94);
			complectCBox.Name = "complectCBox";
			complectCBox.Size = new Size(262, 23);
			complectCBox.TabIndex = 20;
			// 
			// label8
			// 
			label8.Anchor = AnchorStyles.Right;
			label8.AutoSize = true;
			label8.Location = new Point(64, 99);
			label8.Name = "label8";
			label8.Size = new Size(61, 15);
			label8.TabIndex = 19;
			label8.Text = "Комплект";
			// 
			// inventoryTBox
			// 
			inventoryTBox.Dock = DockStyle.Fill;
			inventoryTBox.Location = new Point(131, 216);
			inventoryTBox.Name = "inventoryTBox";
			inventoryTBox.Size = new Size(262, 23);
			inventoryTBox.TabIndex = 18;
			// 
			// serialTBox
			// 
			serialTBox.Dock = DockStyle.Fill;
			serialTBox.Location = new Point(131, 187);
			serialTBox.Name = "serialTBox";
			serialTBox.Size = new Size(262, 23);
			serialTBox.TabIndex = 17;
			// 
			// label7
			// 
			label7.Anchor = AnchorStyles.Right;
			label7.AutoSize = true;
			label7.Location = new Point(3, 220);
			label7.Name = "label7";
			label7.Size = new Size(122, 15);
			label7.TabIndex = 16;
			label7.Text = "Инвентарный номер";
			// 
			// label6
			// 
			label6.Anchor = AnchorStyles.Right;
			label6.AutoSize = true;
			label6.Location = new Point(21, 191);
			label6.Name = "label6";
			label6.Size = new Size(104, 15);
			label6.TabIndex = 15;
			label6.Text = "Серийный номер";
			// 
			// editDeviceProviderBtn
			// 
			editDeviceProviderBtn.Anchor = AnchorStyles.None;
			editDeviceProviderBtn.AutoSize = true;
			editDeviceProviderBtn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			editDeviceProviderBtn.Location = new Point(399, 156);
			editDeviceProviderBtn.Name = "editDeviceProviderBtn";
			editDeviceProviderBtn.Size = new Size(126, 25);
			editDeviceProviderBtn.TabIndex = 14;
			editDeviceProviderBtn.Text = "Добавить/изменить";
			editDeviceProviderBtn.UseVisualStyleBackColor = true;
			editDeviceProviderBtn.Click += editDeviceProviderBtn_Click;
			// 
			// deviceProviderCBox
			// 
			deviceProviderCBox.Dock = DockStyle.Fill;
			deviceProviderCBox.FormattingEnabled = true;
			deviceProviderCBox.Location = new Point(131, 156);
			deviceProviderCBox.Name = "deviceProviderCBox";
			deviceProviderCBox.Size = new Size(262, 23);
			deviceProviderCBox.TabIndex = 13;
			// 
			// label5
			// 
			label5.Anchor = AnchorStyles.Right;
			label5.AutoSize = true;
			label5.Location = new Point(23, 161);
			label5.Name = "label5";
			label5.Size = new Size(102, 15);
			label5.TabIndex = 12;
			label5.Text = "Принадлежность";
			// 
			// editDeviceNameBtn
			// 
			editDeviceNameBtn.Anchor = AnchorStyles.None;
			editDeviceNameBtn.AutoSize = true;
			editDeviceNameBtn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			editDeviceNameBtn.Location = new Point(399, 125);
			editDeviceNameBtn.Name = "editDeviceNameBtn";
			editDeviceNameBtn.Size = new Size(126, 25);
			editDeviceNameBtn.TabIndex = 11;
			editDeviceNameBtn.Text = "Добавить/изменить";
			editDeviceNameBtn.UseVisualStyleBackColor = true;
			editDeviceNameBtn.Click += editDeviceNameBtn_Click;
			// 
			// deviceNameCBox
			// 
			deviceNameCBox.Dock = DockStyle.Fill;
			deviceNameCBox.FormattingEnabled = true;
			deviceNameCBox.Location = new Point(131, 125);
			deviceNameCBox.Name = "deviceNameCBox";
			deviceNameCBox.Size = new Size(262, 23);
			deviceNameCBox.TabIndex = 10;
			// 
			// label4
			// 
			label4.Anchor = AnchorStyles.Right;
			label4.AutoSize = true;
			label4.Location = new Point(73, 68);
			label4.Name = "label4";
			label4.Size = new Size(52, 15);
			label4.TabIndex = 7;
			label4.Text = "Кабинет";
			// 
			// cabinetCBox
			// 
			cabinetCBox.Dock = DockStyle.Fill;
			cabinetCBox.FormattingEnabled = true;
			cabinetCBox.Location = new Point(131, 63);
			cabinetCBox.Name = "cabinetCBox";
			cabinetCBox.Size = new Size(262, 23);
			cabinetCBox.TabIndex = 6;
			cabinetCBox.SelectedIndexChanged += cabinetCBox_SelectedIndexChanged;
			// 
			// label3
			// 
			label3.Anchor = AnchorStyles.Right;
			label3.AutoSize = true;
			label3.Location = new Point(66, 130);
			label3.Name = "label3";
			label3.Size = new Size(59, 15);
			label3.TabIndex = 4;
			label3.Text = "Название";
			// 
			// label1
			// 
			label1.Anchor = AnchorStyles.Right;
			label1.AutoSize = true;
			label1.Location = new Point(108, 7);
			label1.Name = "label1";
			label1.Size = new Size(17, 15);
			label1.TabIndex = 0;
			label1.Text = "Id";
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Right;
			label2.AutoSize = true;
			label2.Location = new Point(79, 37);
			label2.Name = "label2";
			label2.Size = new Size(46, 15);
			label2.TabIndex = 1;
			label2.Text = "Здание";
			// 
			// idTBox
			// 
			idTBox.Dock = DockStyle.Fill;
			idTBox.Location = new Point(131, 3);
			idTBox.Name = "idTBox";
			idTBox.ReadOnly = true;
			idTBox.Size = new Size(262, 23);
			idTBox.TabIndex = 2;
			// 
			// buildingCBox
			// 
			buildingCBox.Dock = DockStyle.Fill;
			buildingCBox.FormattingEnabled = true;
			buildingCBox.Location = new Point(131, 32);
			buildingCBox.Name = "buildingCBox";
			buildingCBox.Size = new Size(262, 23);
			buildingCBox.TabIndex = 5;
			buildingCBox.SelectedIndexChanged += buildingCBox_SelectedIndexChanged;
			// 
			// editBuildingBtn
			// 
			editBuildingBtn.Anchor = AnchorStyles.None;
			editBuildingBtn.AutoSize = true;
			editBuildingBtn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			editBuildingBtn.Location = new Point(399, 32);
			editBuildingBtn.Name = "editBuildingBtn";
			editBuildingBtn.Size = new Size(126, 25);
			editBuildingBtn.TabIndex = 8;
			editBuildingBtn.Text = "Добавить/изменить";
			editBuildingBtn.UseVisualStyleBackColor = true;
			editBuildingBtn.Click += editBuildingBtn_Click;
			// 
			// editCabinetBtn
			// 
			editCabinetBtn.Anchor = AnchorStyles.None;
			editCabinetBtn.AutoSize = true;
			editCabinetBtn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			editCabinetBtn.Location = new Point(399, 63);
			editCabinetBtn.Name = "editCabinetBtn";
			editCabinetBtn.Size = new Size(126, 25);
			editCabinetBtn.TabIndex = 9;
			editCabinetBtn.Text = "Добавить/изменить";
			editCabinetBtn.UseVisualStyleBackColor = true;
			editCabinetBtn.Click += editCabinetBtn_Click;
			// 
			// tableLayoutPanel3
			// 
			tableLayoutPanel3.AutoSize = true;
			tableLayoutPanel3.ColumnCount = 4;
			tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
			tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
			tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
			tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
			tableLayoutPanel3.Controls.Add(addBtn, 0, 0);
			tableLayoutPanel3.Controls.Add(editBtn, 1, 0);
			tableLayoutPanel3.Controls.Add(removeBtn, 2, 0);
			tableLayoutPanel3.Controls.Add(cancelBtn, 3, 0);
			tableLayoutPanel3.Dock = DockStyle.Fill;
			tableLayoutPanel3.Location = new Point(3, 251);
			tableLayoutPanel3.Name = "tableLayoutPanel3";
			tableLayoutPanel3.RowCount = 1;
			tableLayoutPanel3.RowStyles.Add(new RowStyle());
			tableLayoutPanel3.Size = new Size(528, 57);
			tableLayoutPanel3.TabIndex = 1;
			// 
			// addBtn
			// 
			addBtn.Anchor = AnchorStyles.None;
			addBtn.Location = new Point(28, 17);
			addBtn.Name = "addBtn";
			addBtn.Size = new Size(75, 23);
			addBtn.TabIndex = 0;
			addBtn.Text = "Добавить";
			addBtn.UseVisualStyleBackColor = true;
			addBtn.Click += addBtn_Click;
			// 
			// editBtn
			// 
			editBtn.Anchor = AnchorStyles.None;
			editBtn.Enabled = false;
			editBtn.Location = new Point(160, 17);
			editBtn.Name = "editBtn";
			editBtn.Size = new Size(75, 23);
			editBtn.TabIndex = 1;
			editBtn.Text = "Изменить";
			editBtn.UseVisualStyleBackColor = true;
			editBtn.Click += editBtn_Click;
			// 
			// removeBtn
			// 
			removeBtn.Anchor = AnchorStyles.None;
			removeBtn.Enabled = false;
			removeBtn.Location = new Point(292, 17);
			removeBtn.Name = "removeBtn";
			removeBtn.Size = new Size(75, 23);
			removeBtn.TabIndex = 2;
			removeBtn.Text = "Удалить";
			removeBtn.UseVisualStyleBackColor = true;
			removeBtn.Click += removeBtn_Click;
			// 
			// cancelBtn
			// 
			cancelBtn.Anchor = AnchorStyles.None;
			cancelBtn.Location = new Point(424, 17);
			cancelBtn.Name = "cancelBtn";
			cancelBtn.Size = new Size(75, 23);
			cancelBtn.TabIndex = 3;
			cancelBtn.Text = "Отмена";
			cancelBtn.UseVisualStyleBackColor = true;
			cancelBtn.Click += cancelBtn_Click;
			// 
			// EditDeviceForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = cancelBtn;
			ClientSize = new Size(534, 311);
			Controls.Add(tableLayoutPanel1);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "EditDeviceForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Редактирование единицы техники";
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			tableLayoutPanel2.ResumeLayout(false);
			tableLayoutPanel2.PerformLayout();
			tableLayoutPanel3.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel tableLayoutPanel1;
		private TableLayoutPanel tableLayoutPanel2;
		private Label label4;
		private ComboBox cabinetCBox;
		private Label label3;
		private Label label1;
		private Label label2;
		private TextBox idTBox;
		private ComboBox buildingCBox;
		private Button editBuildingBtn;
		private Button editCabinetBtn;
		private TableLayoutPanel tableLayoutPanel3;
		private Button addBtn;
		private Button editBtn;
		private Button removeBtn;
		private Button cancelBtn;
		private ComboBox deviceNameCBox;
		private Label label5;
		private Button editDeviceNameBtn;
		private Label label7;
		private Label label6;
		private Button editDeviceProviderBtn;
		private ComboBox deviceProviderCBox;
		private TextBox inventoryTBox;
		private TextBox serialTBox;
		private ComboBox complectCBox;
		private Label label8;
		private Button editComplectBtn;
	}
}