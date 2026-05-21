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
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage();
            tabPage5 = new TabPage();
            fullListTLP = new TableLayoutPanel();
            fullListNotesTBox = new TextBox();
            label8 = new Label();
            fullListInventoryTBox = new TextBox();
            fullListSerialTBox = new TextBox();
            fullListDeviceProviderTBox = new TextBox();
            fullListDeviceNameTBox = new TextBox();
            fullListDeviceTypeTBox = new TextBox();
            fullListComplectTBox = new TextBox();
            fullListCabinetTBox = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            fullListSearchLabel = new Label();
            fullListBuildingTBox = new TextBox();
            fullListDGW = new DataGridView();
            fullListNumberOfDevicesLabel = new Label();
            tabPage6 = new TabPage();
            tabPage7 = new TabPage();
            menuStrip = new MenuStrip();
            downloadToExcelToolStripMenuItem = new ToolStripMenuItem();
            downloadQRsToolStripMenuItem = new ToolStripMenuItem();
            downloadInventoryCardsToolStripMenuItem = new ToolStripMenuItem();
            progressBar1 = new ProgressBar();
            progressLabel = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            tabControl.SuspendLayout();
            tabPage5.SuspendLayout();
            fullListTLP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)fullListDGW).BeginInit();
            menuStrip.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPage1);
            tabControl.Controls.Add(tabPage2);
            tabControl.Controls.Add(tabPage3);
            tabControl.Controls.Add(tabPage4);
            tabControl.Controls.Add(tabPage5);
            tabControl.Controls.Add(tabPage6);
            tabControl.Controls.Add(tabPage7);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(3, 27);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(978, 381);
            tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(970, 353);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Перемещение техники";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(970, 353);
            tabPage2.TabIndex = 3;
            tabPage2.Text = "Типы и названия техники";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(970, 353);
            tabPage3.TabIndex = 4;
            tabPage3.Text = "Поставщики техники";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(970, 353);
            tabPage4.TabIndex = 5;
            tabPage4.Text = "История перемещений";
            tabPage4.UseVisualStyleBackColor = true;
            tabPage4.Enter += tabPage4_Enter;
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(fullListTLP);
            tabPage5.Location = new Point(4, 24);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(970, 353);
            tabPage5.TabIndex = 6;
            tabPage5.Text = "Полный список (только чтение)";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // fullListTLP
            // 
            fullListTLP.ColumnCount = 9;
            fullListTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            fullListTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            fullListTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            fullListTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            fullListTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            fullListTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            fullListTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            fullListTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            fullListTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1111107F));
            fullListTLP.Controls.Add(fullListNotesTBox, 8, 1);
            fullListTLP.Controls.Add(label8, 8, 0);
            fullListTLP.Controls.Add(fullListInventoryTBox, 7, 1);
            fullListTLP.Controls.Add(fullListSerialTBox, 6, 1);
            fullListTLP.Controls.Add(fullListDeviceProviderTBox, 5, 1);
            fullListTLP.Controls.Add(fullListDeviceNameTBox, 4, 1);
            fullListTLP.Controls.Add(fullListDeviceTypeTBox, 3, 1);
            fullListTLP.Controls.Add(fullListComplectTBox, 2, 1);
            fullListTLP.Controls.Add(fullListCabinetTBox, 1, 1);
            fullListTLP.Controls.Add(label7, 7, 0);
            fullListTLP.Controls.Add(label6, 6, 0);
            fullListTLP.Controls.Add(label5, 4, 0);
            fullListTLP.Controls.Add(label4, 5, 0);
            fullListTLP.Controls.Add(label3, 3, 0);
            fullListTLP.Controls.Add(label2, 2, 0);
            fullListTLP.Controls.Add(label1, 1, 0);
            fullListTLP.Controls.Add(fullListSearchLabel, 0, 0);
            fullListTLP.Controls.Add(fullListBuildingTBox, 0, 1);
            fullListTLP.Controls.Add(fullListDGW, 0, 2);
            fullListTLP.Controls.Add(fullListNumberOfDevicesLabel, 0, 3);
            fullListTLP.Dock = DockStyle.Fill;
            fullListTLP.Location = new Point(3, 3);
            fullListTLP.Name = "fullListTLP";
            fullListTLP.RowCount = 4;
            fullListTLP.RowStyles.Add(new RowStyle());
            fullListTLP.RowStyles.Add(new RowStyle());
            fullListTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            fullListTLP.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            fullListTLP.Size = new Size(964, 347);
            fullListTLP.TabIndex = 1;
            // 
            // fullListNotesTBox
            // 
            fullListNotesTBox.Dock = DockStyle.Fill;
            fullListNotesTBox.Location = new Point(859, 33);
            fullListNotesTBox.Name = "fullListNotesTBox";
            fullListNotesTBox.Size = new Size(102, 23);
            fullListNotesTBox.TabIndex = 33;
            fullListNotesTBox.TextChanged += fullListSearchTBox_TextChanged;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.None;
            label8.AutoSize = true;
            label8.Location = new Point(871, 7);
            label8.Name = "label8";
            label8.Size = new Size(78, 15);
            label8.TabIndex = 32;
            label8.Text = "Примечание";
            // 
            // fullListInventoryTBox
            // 
            fullListInventoryTBox.Dock = DockStyle.Fill;
            fullListInventoryTBox.Location = new Point(752, 33);
            fullListInventoryTBox.Name = "fullListInventoryTBox";
            fullListInventoryTBox.Size = new Size(101, 23);
            fullListInventoryTBox.TabIndex = 30;
            fullListInventoryTBox.TextChanged += fullListSearchTBox_TextChanged;
            // 
            // fullListSerialTBox
            // 
            fullListSerialTBox.Dock = DockStyle.Fill;
            fullListSerialTBox.Location = new Point(645, 33);
            fullListSerialTBox.Name = "fullListSerialTBox";
            fullListSerialTBox.Size = new Size(101, 23);
            fullListSerialTBox.TabIndex = 29;
            fullListSerialTBox.TextChanged += fullListSearchTBox_TextChanged;
            // 
            // fullListDeviceProviderTBox
            // 
            fullListDeviceProviderTBox.Dock = DockStyle.Fill;
            fullListDeviceProviderTBox.Location = new Point(538, 33);
            fullListDeviceProviderTBox.Name = "fullListDeviceProviderTBox";
            fullListDeviceProviderTBox.Size = new Size(101, 23);
            fullListDeviceProviderTBox.TabIndex = 28;
            fullListDeviceProviderTBox.TextChanged += fullListSearchTBox_TextChanged;
            // 
            // fullListDeviceNameTBox
            // 
            fullListDeviceNameTBox.Dock = DockStyle.Fill;
            fullListDeviceNameTBox.Location = new Point(431, 33);
            fullListDeviceNameTBox.Name = "fullListDeviceNameTBox";
            fullListDeviceNameTBox.Size = new Size(101, 23);
            fullListDeviceNameTBox.TabIndex = 27;
            fullListDeviceNameTBox.TextChanged += fullListSearchTBox_TextChanged;
            // 
            // fullListDeviceTypeTBox
            // 
            fullListDeviceTypeTBox.Dock = DockStyle.Fill;
            fullListDeviceTypeTBox.Location = new Point(324, 33);
            fullListDeviceTypeTBox.Name = "fullListDeviceTypeTBox";
            fullListDeviceTypeTBox.Size = new Size(101, 23);
            fullListDeviceTypeTBox.TabIndex = 26;
            fullListDeviceTypeTBox.TextChanged += fullListSearchTBox_TextChanged;
            // 
            // fullListComplectTBox
            // 
            fullListComplectTBox.Dock = DockStyle.Fill;
            fullListComplectTBox.Location = new Point(217, 33);
            fullListComplectTBox.Name = "fullListComplectTBox";
            fullListComplectTBox.Size = new Size(101, 23);
            fullListComplectTBox.TabIndex = 25;
            fullListComplectTBox.TextChanged += fullListSearchTBox_TextChanged;
            // 
            // fullListCabinetTBox
            // 
            fullListCabinetTBox.Dock = DockStyle.Fill;
            fullListCabinetTBox.Location = new Point(110, 33);
            fullListCabinetTBox.Name = "fullListCabinetTBox";
            fullListCabinetTBox.Size = new Size(101, 23);
            fullListCabinetTBox.TabIndex = 24;
            fullListCabinetTBox.TextChanged += fullListSearchTBox_TextChanged;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.None;
            label7.AutoSize = true;
            label7.Location = new Point(759, 0);
            label7.Name = "label7";
            label7.Size = new Size(86, 30);
            label7.TabIndex = 23;
            label7.Text = "Инвентарный номер";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.Location = new Point(661, 0);
            label6.Name = "label6";
            label6.Size = new Size(68, 30);
            label6.TabIndex = 22;
            label6.Text = "Серийный номер";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Location = new Point(452, 7);
            label5.Name = "label5";
            label5.Size = new Size(59, 15);
            label5.TabIndex = 21;
            label5.Text = "Название";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Location = new Point(549, 7);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 20;
            label4.Text = "Получено от";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Location = new Point(360, 7);
            label3.Name = "label3";
            label3.Size = new Size(28, 15);
            label3.TabIndex = 19;
            label3.Text = "Тип";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Location = new Point(237, 7);
            label2.Name = "label2";
            label2.Size = new Size(61, 15);
            label2.TabIndex = 18;
            label2.Text = "Комплект";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(134, 7);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 17;
            label1.Text = "Кабинет";
            // 
            // fullListSearchLabel
            // 
            fullListSearchLabel.Anchor = AnchorStyles.None;
            fullListSearchLabel.AutoSize = true;
            fullListSearchLabel.Location = new Point(30, 7);
            fullListSearchLabel.Name = "fullListSearchLabel";
            fullListSearchLabel.Size = new Size(46, 15);
            fullListSearchLabel.TabIndex = 14;
            fullListSearchLabel.Text = "Здание";
            // 
            // fullListBuildingTBox
            // 
            fullListBuildingTBox.Dock = DockStyle.Fill;
            fullListBuildingTBox.Location = new Point(3, 33);
            fullListBuildingTBox.Name = "fullListBuildingTBox";
            fullListBuildingTBox.Size = new Size(101, 23);
            fullListBuildingTBox.TabIndex = 15;
            fullListBuildingTBox.TextChanged += fullListSearchTBox_TextChanged;
            // 
            // fullListDGW
            // 
            fullListDGW.AllowUserToAddRows = false;
            fullListDGW.AllowUserToDeleteRows = false;
            fullListDGW.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            fullListDGW.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            fullListDGW.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            fullListTLP.SetColumnSpan(fullListDGW, 9);
            fullListDGW.Dock = DockStyle.Fill;
            fullListDGW.Location = new Point(3, 62);
            fullListDGW.MultiSelect = false;
            fullListDGW.Name = "fullListDGW";
            fullListDGW.ReadOnly = true;
            fullListDGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            fullListDGW.Size = new Size(958, 262);
            fullListDGW.TabIndex = 16;
            // 
            // fullListNumberOfDevicesLabel
            // 
            fullListNumberOfDevicesLabel.Anchor = AnchorStyles.Left;
            fullListNumberOfDevicesLabel.AutoSize = true;
            fullListTLP.SetColumnSpan(fullListNumberOfDevicesLabel, 8);
            fullListNumberOfDevicesLabel.Location = new Point(3, 329);
            fullListNumberOfDevicesLabel.Name = "fullListNumberOfDevicesLabel";
            fullListNumberOfDevicesLabel.Size = new Size(139, 15);
            fullListNumberOfDevicesLabel.TabIndex = 31;
            fullListNumberOfDevicesLabel.Text = "Всего единиц техники: 0";
            // 
            // tabPage6
            // 
            tabPage6.Location = new Point(4, 24);
            tabPage6.Name = "tabPage6";
            tabPage6.Padding = new Padding(3);
            tabPage6.Size = new Size(970, 353);
            tabPage6.TabIndex = 7;
            tabPage6.Text = "Ремонтники и операции";
            tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            tabPage7.Location = new Point(4, 24);
            tabPage7.Name = "tabPage7";
            tabPage7.Padding = new Padding(3);
            tabPage7.Size = new Size(970, 353);
            tabPage7.TabIndex = 8;
            tabPage7.Text = "Список ремонтов";
            tabPage7.UseVisualStyleBackColor = true;
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { downloadToExcelToolStripMenuItem, downloadQRsToolStripMenuItem, downloadInventoryCardsToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(984, 24);
            menuStrip.TabIndex = 1;
            menuStrip.Text = "menuStrip1";
            // 
            // downloadToExcelToolStripMenuItem
            // 
            downloadToExcelToolStripMenuItem.Name = "downloadToExcelToolStripMenuItem";
            downloadToExcelToolStripMenuItem.Size = new Size(108, 20);
            downloadToExcelToolStripMenuItem.Text = "Передать в Excel";
            downloadToExcelToolStripMenuItem.Click += DownloadToExcelToolStripMenuItem_Click;
            // 
            // downloadQRsToolStripMenuItem
            // 
            downloadQRsToolStripMenuItem.Name = "downloadQRsToolStripMenuItem";
            downloadQRsToolStripMenuItem.Size = new Size(128, 20);
            downloadQRsToolStripMenuItem.Text = "Выгрузить QR-коды";
            downloadQRsToolStripMenuItem.Click += DownloadQRsToolStripMenuItem_Click;
            // 
            // downloadInventoryCardsToolStripMenuItem
            // 
            downloadInventoryCardsToolStripMenuItem.Name = "downloadInventoryCardsToolStripMenuItem";
            downloadInventoryCardsToolStripMenuItem.Size = new Size(206, 20);
            downloadInventoryCardsToolStripMenuItem.Text = "Выгрузить инвентарные карточки";
            downloadInventoryCardsToolStripMenuItem.Click += DownloadInventoryCardsToolStripMenuItem_Click;
            // 
            // progressBar1
            // 
            progressBar1.Dock = DockStyle.Top;
            progressBar1.Location = new Point(3, 414);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(978, 23);
            progressBar1.TabIndex = 2;
            // 
            // progressLabel
            // 
            progressLabel.AutoSize = true;
            progressLabel.Dock = DockStyle.Top;
            progressLabel.Location = new Point(3, 443);
            progressLabel.Margin = new Padding(3);
            progressLabel.Name = "progressLabel";
            progressLabel.Size = new Size(978, 15);
            progressLabel.TabIndex = 3;
            progressLabel.Text = "label9";
            progressLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(menuStrip, 0, 0);
            tableLayoutPanel1.Controls.Add(tabControl, 0, 1);
            tableLayoutPanel1.Controls.Add(progressBar1, 0, 2);
            tableLayoutPanel1.Controls.Add(progressLabel, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(984, 461);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(984, 461);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Инвентаризация";
            tabControl.ResumeLayout(false);
            tabPage5.ResumeLayout(false);
            fullListTLP.ResumeLayout(false);
            fullListTLP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)fullListDGW).EndInit();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private MenuStrip menuStrip;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TableLayoutPanel fullListTLP;
        private TextBox fullListBuildingTBox;
        private Label fullListSearchLabel;
        private DataGridView fullListDGW;
        private TextBox fullListInventoryTBox;
        private TextBox fullListSerialTBox;
        private TextBox fullListDeviceProviderTBox;
        private TextBox fullListDeviceNameTBox;
        private TextBox fullListDeviceTypeTBox;
        private TextBox fullListComplectTBox;
        private TextBox fullListCabinetTBox;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label fullListNumberOfDevicesLabel;
        private Label label8;
        private TextBox fullListNotesTBox;
        private ToolStripMenuItem downloadToExcelToolStripMenuItem;
        private ToolStripMenuItem downloadQRsToolStripMenuItem;
        private ToolStripMenuItem downloadInventoryCardsToolStripMenuItem;
        private ProgressBar progressBar1;
        private Label progressLabel;
        private TabPage tabPage5;
        private TableLayoutPanel tableLayoutPanel1;
        private TabPage tabPage6;
        private TabPage tabPage7;
    }
}
