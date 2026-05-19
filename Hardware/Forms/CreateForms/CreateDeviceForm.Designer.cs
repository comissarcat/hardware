namespace Hardware.Forms
{
	partial class CreateDeviceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateDeviceForm));
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            notesTBox = new TextBox();
            label9 = new Label();
            label8 = new Label();
            inventoryTBox = new TextBox();
            serialTBox = new TextBox();
            label7 = new Label();
            label6 = new Label();
            deviceProviderCBox = new ComboBox();
            label5 = new Label();
            deviceNameCBox = new ComboBox();
            label3 = new Label();
            complectTBox = new TextBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            createBtn = new Button();
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
            tableLayoutPanel1.Size = new Size(484, 236);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(notesTBox, 1, 5);
            tableLayoutPanel2.Controls.Add(label9, 0, 5);
            tableLayoutPanel2.Controls.Add(label8, 0, 0);
            tableLayoutPanel2.Controls.Add(inventoryTBox, 1, 4);
            tableLayoutPanel2.Controls.Add(serialTBox, 1, 3);
            tableLayoutPanel2.Controls.Add(label7, 0, 4);
            tableLayoutPanel2.Controls.Add(label6, 0, 3);
            tableLayoutPanel2.Controls.Add(deviceProviderCBox, 1, 2);
            tableLayoutPanel2.Controls.Add(label5, 0, 2);
            tableLayoutPanel2.Controls.Add(deviceNameCBox, 1, 1);
            tableLayoutPanel2.Controls.Add(label3, 0, 1);
            tableLayoutPanel2.Controls.Add(complectTBox, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 6;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(478, 174);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // notesTBox
            // 
            notesTBox.Dock = DockStyle.Fill;
            notesTBox.Location = new Point(131, 148);
            notesTBox.Name = "notesTBox";
            notesTBox.Size = new Size(344, 23);
            notesTBox.TabIndex = 23;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Location = new Point(47, 152);
            label9.Name = "label9";
            label9.Size = new Size(78, 15);
            label9.TabIndex = 22;
            label9.Text = "Примечание";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Location = new Point(64, 7);
            label8.Name = "label8";
            label8.Size = new Size(61, 15);
            label8.TabIndex = 19;
            label8.Text = "Комплект";
            // 
            // inventoryTBox
            // 
            inventoryTBox.Dock = DockStyle.Fill;
            inventoryTBox.Location = new Point(131, 119);
            inventoryTBox.Name = "inventoryTBox";
            inventoryTBox.Size = new Size(344, 23);
            inventoryTBox.TabIndex = 18;
            // 
            // serialTBox
            // 
            serialTBox.Dock = DockStyle.Fill;
            serialTBox.Location = new Point(131, 90);
            serialTBox.Name = "serialTBox";
            serialTBox.Size = new Size(344, 23);
            serialTBox.TabIndex = 17;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new Point(3, 123);
            label7.Name = "label7";
            label7.Size = new Size(122, 15);
            label7.TabIndex = 16;
            label7.Text = "Инвентарный номер";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new Point(21, 94);
            label6.Name = "label6";
            label6.Size = new Size(104, 15);
            label6.TabIndex = 15;
            label6.Text = "Серийный номер";
            // 
            // deviceProviderCBox
            // 
            deviceProviderCBox.Dock = DockStyle.Fill;
            deviceProviderCBox.FormattingEnabled = true;
            deviceProviderCBox.Location = new Point(131, 61);
            deviceProviderCBox.Name = "deviceProviderCBox";
            deviceProviderCBox.Size = new Size(344, 23);
            deviceProviderCBox.TabIndex = 13;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(23, 65);
            label5.Name = "label5";
            label5.Size = new Size(102, 15);
            label5.TabIndex = 12;
            label5.Text = "Принадлежность";
            // 
            // deviceNameCBox
            // 
            deviceNameCBox.Dock = DockStyle.Fill;
            deviceNameCBox.FormattingEnabled = true;
            deviceNameCBox.Location = new Point(131, 32);
            deviceNameCBox.Name = "deviceNameCBox";
            deviceNameCBox.Size = new Size(344, 23);
            deviceNameCBox.TabIndex = 10;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(66, 36);
            label3.Name = "label3";
            label3.Size = new Size(59, 15);
            label3.TabIndex = 4;
            label3.Text = "Название";
            // 
            // complectTBox
            // 
            complectTBox.Dock = DockStyle.Fill;
            complectTBox.Location = new Point(131, 3);
            complectTBox.Name = "complectTBox";
            complectTBox.ReadOnly = true;
            complectTBox.Size = new Size(344, 23);
            complectTBox.TabIndex = 24;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.ColumnCount = 4;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.Controls.Add(createBtn, 0, 0);
            tableLayoutPanel3.Controls.Add(cancelBtn, 3, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 183);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new Size(478, 50);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // createBtn
            // 
            createBtn.Anchor = AnchorStyles.None;
            createBtn.Location = new Point(22, 13);
            createBtn.Name = "createBtn";
            createBtn.Size = new Size(75, 23);
            createBtn.TabIndex = 0;
            createBtn.Text = "Добавить";
            createBtn.UseVisualStyleBackColor = true;
            createBtn.Click += CreateBtn_Click;
            // 
            // cancelBtn
            // 
            cancelBtn.Anchor = AnchorStyles.None;
            cancelBtn.Location = new Point(380, 13);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(75, 23);
            cancelBtn.TabIndex = 3;
            cancelBtn.Text = "Отмена";
            cancelBtn.UseVisualStyleBackColor = true;
            cancelBtn.Click += CancelBtn_Click;
            // 
            // CreateDeviceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelBtn;
            ClientSize = new Size(484, 236);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CreateDeviceForm";
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
		private Label label3;
		private TableLayoutPanel tableLayoutPanel3;
		private Button createBtn;
		private Button cancelBtn;
		private ComboBox deviceNameCBox;
		private Label label5;
		private Label label7;
		private Label label6;
		private ComboBox deviceProviderCBox;
		private TextBox inventoryTBox;
		private TextBox serialTBox;
		private Label label8;
		private Label label9;
		private TextBox notesTBox;
        private TextBox complectTBox;
    }
}