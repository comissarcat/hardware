namespace Hardware.Forms
{
	partial class EditDeviceNameForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditDeviceNameForm));
			tableLayoutPanel1 = new TableLayoutPanel();
			tableLayoutPanel2 = new TableLayoutPanel();
			label3 = new Label();
			label1 = new Label();
			label2 = new Label();
			idTBox = new TextBox();
			nameTBox = new TextBox();
			typeCBox = new ComboBox();
			editBuildingBtn = new Button();
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
			tableLayoutPanel1.Size = new Size(534, 136);
			tableLayoutPanel1.TabIndex = 2;
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.AutoSize = true;
			tableLayoutPanel2.ColumnCount = 3;
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel2.Controls.Add(label3, 0, 2);
			tableLayoutPanel2.Controls.Add(label1, 0, 0);
			tableLayoutPanel2.Controls.Add(label2, 0, 1);
			tableLayoutPanel2.Controls.Add(idTBox, 1, 0);
			tableLayoutPanel2.Controls.Add(nameTBox, 1, 2);
			tableLayoutPanel2.Controls.Add(typeCBox, 1, 1);
			tableLayoutPanel2.Controls.Add(editBuildingBtn, 2, 1);
			tableLayoutPanel2.Dock = DockStyle.Fill;
			tableLayoutPanel2.Location = new Point(3, 3);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 3;
			tableLayoutPanel2.RowStyles.Add(new RowStyle());
			tableLayoutPanel2.RowStyles.Add(new RowStyle());
			tableLayoutPanel2.RowStyles.Add(new RowStyle());
			tableLayoutPanel2.Size = new Size(528, 89);
			tableLayoutPanel2.TabIndex = 0;
			// 
			// label3
			// 
			label3.Anchor = AnchorStyles.Right;
			label3.AutoSize = true;
			label3.Location = new Point(3, 67);
			label3.Name = "label3";
			label3.Size = new Size(59, 15);
			label3.TabIndex = 4;
			label3.Text = "Название";
			// 
			// label1
			// 
			label1.Anchor = AnchorStyles.Right;
			label1.AutoSize = true;
			label1.Location = new Point(45, 7);
			label1.Name = "label1";
			label1.Size = new Size(17, 15);
			label1.TabIndex = 0;
			label1.Text = "Id";
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Right;
			label2.AutoSize = true;
			label2.Location = new Point(35, 37);
			label2.Name = "label2";
			label2.Size = new Size(27, 15);
			label2.TabIndex = 1;
			label2.Text = "Тип";
			// 
			// idTBox
			// 
			idTBox.Dock = DockStyle.Fill;
			idTBox.Location = new Point(68, 3);
			idTBox.Name = "idTBox";
			idTBox.ReadOnly = true;
			idTBox.Size = new Size(325, 23);
			idTBox.TabIndex = 2;
			// 
			// nameTBox
			// 
			nameTBox.Dock = DockStyle.Fill;
			nameTBox.Location = new Point(68, 63);
			nameTBox.Name = "nameTBox";
			nameTBox.Size = new Size(325, 23);
			nameTBox.TabIndex = 3;
			// 
			// typeCBox
			// 
			typeCBox.Dock = DockStyle.Fill;
			typeCBox.FormattingEnabled = true;
			typeCBox.Location = new Point(68, 32);
			typeCBox.Name = "typeCBox";
			typeCBox.Size = new Size(325, 23);
			typeCBox.TabIndex = 5;
			// 
			// editBuildingBtn
			// 
			editBuildingBtn.Anchor = AnchorStyles.None;
			editBuildingBtn.AutoSize = true;
			editBuildingBtn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			editBuildingBtn.Location = new Point(399, 32);
			editBuildingBtn.Name = "editBuildingBtn";
			editBuildingBtn.Size = new Size(126, 25);
			editBuildingBtn.TabIndex = 6;
			editBuildingBtn.Text = "Добавить/изменить";
			editBuildingBtn.UseVisualStyleBackColor = true;
			editBuildingBtn.Click += editDeviceTypeBtn_Click;
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
			tableLayoutPanel3.Location = new Point(3, 98);
			tableLayoutPanel3.Name = "tableLayoutPanel3";
			tableLayoutPanel3.RowCount = 1;
			tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel3.Size = new Size(528, 35);
			tableLayoutPanel3.TabIndex = 1;
			// 
			// addBtn
			// 
			addBtn.Anchor = AnchorStyles.None;
			addBtn.Location = new Point(28, 6);
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
			editBtn.Location = new Point(160, 6);
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
			removeBtn.Location = new Point(292, 6);
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
			cancelBtn.Location = new Point(424, 6);
			cancelBtn.Name = "cancelBtn";
			cancelBtn.Size = new Size(75, 23);
			cancelBtn.TabIndex = 3;
			cancelBtn.Text = "Отмена";
			cancelBtn.UseVisualStyleBackColor = true;
			cancelBtn.Click += cancelBtn_Click;
			// 
			// EditDeviceNameForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(534, 136);
			Controls.Add(tableLayoutPanel1);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "EditDeviceNameForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Редактирование названия техники";
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
		private Label label1;
		private Label label2;
		private TextBox idTBox;
		private TextBox nameTBox;
		private ComboBox typeCBox;
		private Button editBuildingBtn;
		private TableLayoutPanel tableLayoutPanel3;
		private Button addBtn;
		private Button editBtn;
		private Button removeBtn;
		private Button cancelBtn;
	}
}