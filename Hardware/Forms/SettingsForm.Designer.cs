namespace Hardware.Forms
{
	partial class SettingsForm
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
			tableLayoutPanel1 = new TableLayoutPanel();
			label1 = new Label();
			label2 = new Label();
			label3 = new Label();
			label4 = new Label();
			serverTBox = new TextBox();
			userTBox = new TextBox();
			passwordTBox = new TextBox();
			databaseTBox = new TextBox();
			tableLayoutPanel2 = new TableLayoutPanel();
			saveBtn = new Button();
			testBtn = new Button();
			toolTip = new ToolTip(components);
			tableLayoutPanel1.SuspendLayout();
			tableLayoutPanel2.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.Controls.Add(label1, 0, 0);
			tableLayoutPanel1.Controls.Add(label2, 0, 1);
			tableLayoutPanel1.Controls.Add(label3, 0, 2);
			tableLayoutPanel1.Controls.Add(label4, 0, 3);
			tableLayoutPanel1.Controls.Add(serverTBox, 1, 0);
			tableLayoutPanel1.Controls.Add(userTBox, 1, 1);
			tableLayoutPanel1.Controls.Add(passwordTBox, 1, 2);
			tableLayoutPanel1.Controls.Add(databaseTBox, 1, 3);
			tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 4);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 5;
			tableLayoutPanel1.RowStyles.Add(new RowStyle());
			tableLayoutPanel1.RowStyles.Add(new RowStyle());
			tableLayoutPanel1.RowStyles.Add(new RowStyle());
			tableLayoutPanel1.RowStyles.Add(new RowStyle());
			tableLayoutPanel1.RowStyles.Add(new RowStyle());
			tableLayoutPanel1.Size = new Size(384, 161);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// label1
			// 
			label1.Anchor = AnchorStyles.Left;
			label1.AutoSize = true;
			label1.Location = new Point(3, 7);
			label1.Name = "label1";
			label1.Size = new Size(47, 15);
			label1.TabIndex = 0;
			label1.Text = "Сервер";
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Left;
			label2.AutoSize = true;
			label2.Location = new Point(3, 36);
			label2.Name = "label2";
			label2.Size = new Size(84, 15);
			label2.TabIndex = 1;
			label2.Text = "Пользователь";
			// 
			// label3
			// 
			label3.Anchor = AnchorStyles.Left;
			label3.AutoSize = true;
			label3.Location = new Point(3, 65);
			label3.Name = "label3";
			label3.Size = new Size(49, 15);
			label3.TabIndex = 2;
			label3.Text = "Пароль";
			// 
			// label4
			// 
			label4.Anchor = AnchorStyles.Left;
			label4.AutoSize = true;
			label4.Location = new Point(3, 94);
			label4.Name = "label4";
			label4.Size = new Size(75, 15);
			label4.TabIndex = 3;
			label4.Text = "База данных";
			// 
			// serverTBox
			// 
			serverTBox.Dock = DockStyle.Fill;
			serverTBox.Location = new Point(93, 3);
			serverTBox.Name = "serverTBox";
			serverTBox.Size = new Size(288, 23);
			serverTBox.TabIndex = 4;
			// 
			// userTBox
			// 
			userTBox.Dock = DockStyle.Fill;
			userTBox.Location = new Point(93, 32);
			userTBox.Name = "userTBox";
			userTBox.Size = new Size(288, 23);
			userTBox.TabIndex = 5;
			// 
			// passwordTBox
			// 
			passwordTBox.Dock = DockStyle.Fill;
			passwordTBox.Location = new Point(93, 61);
			passwordTBox.Name = "passwordTBox";
			passwordTBox.PasswordChar = '*';
			passwordTBox.Size = new Size(288, 23);
			passwordTBox.TabIndex = 6;
			// 
			// databaseTBox
			// 
			databaseTBox.Dock = DockStyle.Fill;
			databaseTBox.Location = new Point(93, 90);
			databaseTBox.Name = "databaseTBox";
			databaseTBox.Size = new Size(288, 23);
			databaseTBox.TabIndex = 7;
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.AutoSize = true;
			tableLayoutPanel2.ColumnCount = 2;
			tableLayoutPanel1.SetColumnSpan(tableLayoutPanel2, 2);
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanel2.Controls.Add(saveBtn, 0, 0);
			tableLayoutPanel2.Controls.Add(testBtn, 1, 0);
			tableLayoutPanel2.Dock = DockStyle.Fill;
			tableLayoutPanel2.Location = new Point(3, 119);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 1;
			tableLayoutPanel2.RowStyles.Add(new RowStyle());
			tableLayoutPanel2.Size = new Size(378, 39);
			tableLayoutPanel2.TabIndex = 8;
			// 
			// saveBtn
			// 
			saveBtn.Anchor = AnchorStyles.None;
			saveBtn.Location = new Point(57, 8);
			saveBtn.Name = "saveBtn";
			saveBtn.Size = new Size(75, 23);
			saveBtn.TabIndex = 0;
			saveBtn.Text = "Сохранить";
			saveBtn.UseVisualStyleBackColor = true;
			saveBtn.Click += saveBtn_Click;
			// 
			// testBtn
			// 
			testBtn.Anchor = AnchorStyles.None;
			testBtn.AutoSize = true;
			testBtn.Location = new Point(229, 7);
			testBtn.Name = "testBtn";
			testBtn.Size = new Size(108, 25);
			testBtn.TabIndex = 1;
			testBtn.Text = "Тест соединения";
			toolTip.SetToolTip(testBtn, "Перед тестом соединения сохрани новую конфигурацию!");
			testBtn.UseVisualStyleBackColor = true;
			testBtn.Click += testBtn_Click;
			// 
			// SettingsForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(384, 161);
			Controls.Add(tableLayoutPanel1);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "SettingsForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Настройки";
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			tableLayoutPanel2.ResumeLayout(false);
			tableLayoutPanel2.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel tableLayoutPanel1;
		private Label label1;
		private Label label2;
		private Label label3;
		private Label label4;
		private TextBox serverTBox;
		private TextBox userTBox;
		private TextBox passwordTBox;
		private TextBox databaseTBox;
		private TableLayoutPanel tableLayoutPanel2;
		private Button saveBtn;
		private Button testBtn;
		private ToolTip toolTip;
	}
}