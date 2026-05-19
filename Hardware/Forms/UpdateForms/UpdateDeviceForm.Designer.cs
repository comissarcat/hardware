namespace Hardware.Forms
{
    partial class UpdateDeviceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateDeviceForm));
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            newInventoryTBox = new TextBox();
            newSerialTBox = new TextBox();
            label7 = new Label();
            label6 = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            updateBtn = new Button();
            cancelBtn = new Button();
            label1 = new Label();
            label2 = new Label();
            oldSerialTBox = new TextBox();
            oldInventoryTBox = new TextBox();
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
            tableLayoutPanel1.Size = new Size(484, 161);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(oldInventoryTBox, 1, 1);
            tableLayoutPanel2.Controls.Add(oldSerialTBox, 1, 0);
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Controls.Add(newInventoryTBox, 1, 3);
            tableLayoutPanel2.Controls.Add(newSerialTBox, 1, 2);
            tableLayoutPanel2.Controls.Add(label7, 0, 3);
            tableLayoutPanel2.Controls.Add(label6, 0, 2);
            tableLayoutPanel2.Controls.Add(label2, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(478, 116);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // newInventoryTBox
            // 
            newInventoryTBox.Dock = DockStyle.Fill;
            newInventoryTBox.Location = new Point(174, 90);
            newInventoryTBox.Name = "newInventoryTBox";
            newInventoryTBox.Size = new Size(301, 23);
            newInventoryTBox.TabIndex = 18;
            // 
            // newSerialTBox
            // 
            newSerialTBox.Dock = DockStyle.Fill;
            newSerialTBox.Location = new Point(174, 61);
            newSerialTBox.Name = "newSerialTBox";
            newSerialTBox.Size = new Size(301, 23);
            newSerialTBox.TabIndex = 17;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new Point(7, 94);
            label7.Name = "label7";
            label7.Size = new Size(161, 15);
            label7.TabIndex = 16;
            label7.Text = "Новый инвентарный номер";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new Point(25, 65);
            label6.Name = "label6";
            label6.Size = new Size(143, 15);
            label6.TabIndex = 15;
            label6.Text = "Новый серийный номер";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.ColumnCount = 4;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.Controls.Add(updateBtn, 0, 0);
            tableLayoutPanel3.Controls.Add(cancelBtn, 3, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 125);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new Size(478, 33);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // updateBtn
            // 
            updateBtn.Anchor = AnchorStyles.None;
            updateBtn.Location = new Point(22, 5);
            updateBtn.Name = "updateBtn";
            updateBtn.Size = new Size(75, 23);
            updateBtn.TabIndex = 0;
            updateBtn.Text = "Изменить";
            updateBtn.UseVisualStyleBackColor = true;
            updateBtn.Click += UpdateBtn_Click;
            // 
            // cancelBtn
            // 
            cancelBtn.Anchor = AnchorStyles.None;
            cancelBtn.Location = new Point(380, 5);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(75, 23);
            cancelBtn.TabIndex = 3;
            cancelBtn.Text = "Отмена";
            cancelBtn.UseVisualStyleBackColor = true;
            cancelBtn.Click += CancelBtn_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(21, 7);
            label1.Name = "label1";
            label1.Size = new Size(147, 15);
            label1.TabIndex = 19;
            label1.Text = "Старый серийный номер";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(3, 36);
            label2.Name = "label2";
            label2.Size = new Size(165, 15);
            label2.TabIndex = 20;
            label2.Text = "Старый инвентарный номер";
            // 
            // oldSerialTBox
            // 
            oldSerialTBox.Dock = DockStyle.Fill;
            oldSerialTBox.Location = new Point(174, 3);
            oldSerialTBox.Name = "oldSerialTBox";
            oldSerialTBox.ReadOnly = true;
            oldSerialTBox.Size = new Size(301, 23);
            oldSerialTBox.TabIndex = 21;
            // 
            // oldInventoryTBox
            // 
            oldInventoryTBox.Dock = DockStyle.Fill;
            oldInventoryTBox.Location = new Point(174, 32);
            oldInventoryTBox.Name = "oldInventoryTBox";
            oldInventoryTBox.ReadOnly = true;
            oldInventoryTBox.Size = new Size(301, 23);
            oldInventoryTBox.TabIndex = 22;
            // 
            // UpdateDeviceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelBtn;
            ClientSize = new Size(484, 161);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "UpdateDeviceForm";
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
        private TableLayoutPanel tableLayoutPanel3;
        private Button updateBtn;
        private Button cancelBtn;
        private Label label7;
        private Label label6;
        private TextBox newInventoryTBox;
        private TextBox newSerialTBox;
        private Label label1;
        private Label label2;
        private TextBox oldInventoryTBox;
        private TextBox oldSerialTBox;
    }
}