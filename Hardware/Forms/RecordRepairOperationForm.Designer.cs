namespace Hardware.Forms
{
    partial class RecordRepairOperationForm
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
            tableLayoutPanel1 = new TableLayoutPanel();
            notesTBox = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            okBtn = new Button();
            cancelBtn = new Button();
            repairmanTBox = new TextBox();
            deviceTBox = new TextBox();
            repairOperationsCBox = new ComboBox();
            label1 = new Label();
            label5 = new Label();
            dateTimePicker = new DateTimePicker();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(notesTBox, 1, 4);
            tableLayoutPanel1.Controls.Add(label4, 0, 3);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 5);
            tableLayoutPanel1.Controls.Add(repairmanTBox, 1, 0);
            tableLayoutPanel1.Controls.Add(deviceTBox, 1, 1);
            tableLayoutPanel1.Controls.Add(repairOperationsCBox, 1, 2);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label5, 0, 4);
            tableLayoutPanel1.Controls.Add(dateTimePicker, 1, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(484, 186);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // notesTBox
            // 
            notesTBox.Dock = DockStyle.Fill;
            notesTBox.Location = new Point(132, 119);
            notesTBox.Name = "notesTBox";
            notesTBox.Size = new Size(349, 23);
            notesTBox.TabIndex = 9;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(3, 94);
            label4.Name = "label4";
            label4.Size = new Size(32, 15);
            label4.TabIndex = 6;
            label4.Text = "Дата";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(3, 65);
            label3.Name = "label3";
            label3.Size = new Size(123, 15);
            label3.TabIndex = 5;
            label3.Text = "Ремонтная операция";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(3, 36);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 4;
            label2.Text = "Устройство";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel1.SetColumnSpan(tableLayoutPanel2, 2);
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.Controls.Add(okBtn, 0, 0);
            tableLayoutPanel2.Controls.Add(cancelBtn, 3, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 148);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(478, 35);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // okBtn
            // 
            okBtn.Anchor = AnchorStyles.None;
            okBtn.Location = new Point(22, 6);
            okBtn.Name = "okBtn";
            okBtn.Size = new Size(75, 23);
            okBtn.TabIndex = 0;
            okBtn.Text = "OK";
            okBtn.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            cancelBtn.Anchor = AnchorStyles.None;
            cancelBtn.DialogResult = DialogResult.Cancel;
            cancelBtn.Location = new Point(380, 6);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(75, 23);
            cancelBtn.TabIndex = 1;
            cancelBtn.Text = "Отмена";
            cancelBtn.UseVisualStyleBackColor = true;
            // 
            // repairmanTBox
            // 
            repairmanTBox.Dock = DockStyle.Fill;
            repairmanTBox.Location = new Point(132, 3);
            repairmanTBox.Name = "repairmanTBox";
            repairmanTBox.ReadOnly = true;
            repairmanTBox.Size = new Size(349, 23);
            repairmanTBox.TabIndex = 0;
            // 
            // deviceTBox
            // 
            deviceTBox.Dock = DockStyle.Fill;
            deviceTBox.Location = new Point(132, 32);
            deviceTBox.Name = "deviceTBox";
            deviceTBox.ReadOnly = true;
            deviceTBox.Size = new Size(349, 23);
            deviceTBox.TabIndex = 1;
            // 
            // repairOperationsCBox
            // 
            repairOperationsCBox.Dock = DockStyle.Fill;
            repairOperationsCBox.DropDownStyle = ComboBoxStyle.DropDownList;
            repairOperationsCBox.FormattingEnabled = true;
            repairOperationsCBox.Location = new Point(132, 61);
            repairOperationsCBox.Name = "repairOperationsCBox";
            repairOperationsCBox.Size = new Size(349, 23);
            repairOperationsCBox.TabIndex = 2;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(3, 7);
            label1.Name = "label1";
            label1.Size = new Size(68, 15);
            label1.TabIndex = 3;
            label1.Text = "Ремонтник";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new Point(3, 123);
            label5.Name = "label5";
            label5.Size = new Size(78, 15);
            label5.TabIndex = 7;
            label5.Text = "Примечание";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dateTimePicker
            // 
            dateTimePicker.Dock = DockStyle.Fill;
            dateTimePicker.Format = DateTimePickerFormat.Short;
            dateTimePicker.Location = new Point(132, 90);
            dateTimePicker.Name = "dateTimePicker";
            dateTimePicker.Size = new Size(349, 23);
            dateTimePicker.TabIndex = 8;
            dateTimePicker.Value = new DateTime(2026, 5, 20, 11, 42, 32, 0);
            // 
            // RecordRepairOperationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 186);
            Controls.Add(tableLayoutPanel1);
            Name = "RecordRepairOperationForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Записать операцию по ремонту";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox repairmanTBox;
        private TextBox deviceTBox;
        private ComboBox repairOperationsCBox;
        private Button okBtn;
        private Button cancelBtn;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label4;
        private Label label5;
        private DateTimePicker dateTimePicker;
        private TextBox notesTBox;
    }
}