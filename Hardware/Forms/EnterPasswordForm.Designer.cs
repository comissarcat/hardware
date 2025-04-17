namespace Hardware.Forms
{
	partial class EnterPasswordForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnterPasswordForm));
			tableLayoutPanel1 = new TableLayoutPanel();
			passwordLabel = new Label();
			passwordTBox = new TextBox();
			tableLayoutPanel2 = new TableLayoutPanel();
			enterBtn = new Button();
			cancelBtn = new Button();
			configBtn = new Button();
			tableLayoutPanel1.SuspendLayout();
			tableLayoutPanel2.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel1.Controls.Add(passwordLabel, 0, 0);
			tableLayoutPanel1.Controls.Add(passwordTBox, 1, 0);
			tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 2;
			tableLayoutPanel1.RowStyles.Add(new RowStyle());
			tableLayoutPanel1.RowStyles.Add(new RowStyle());
			tableLayoutPanel1.Size = new Size(384, 71);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// passwordLabel
			// 
			passwordLabel.Anchor = AnchorStyles.None;
			passwordLabel.AutoSize = true;
			passwordLabel.Location = new Point(3, 7);
			passwordLabel.Name = "passwordLabel";
			passwordLabel.Size = new Size(49, 15);
			passwordLabel.TabIndex = 0;
			passwordLabel.Text = "Пароль";
			// 
			// passwordTBox
			// 
			passwordTBox.Dock = DockStyle.Fill;
			passwordTBox.Location = new Point(58, 3);
			passwordTBox.Name = "passwordTBox";
			passwordTBox.PasswordChar = '*';
			passwordTBox.Size = new Size(323, 23);
			passwordTBox.TabIndex = 1;
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.AutoSize = true;
			tableLayoutPanel2.ColumnCount = 3;
			tableLayoutPanel1.SetColumnSpan(tableLayoutPanel2, 2);
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanel2.Controls.Add(configBtn, 0, 0);
			tableLayoutPanel2.Controls.Add(cancelBtn, 2, 0);
			tableLayoutPanel2.Controls.Add(enterBtn, 1, 0);
			tableLayoutPanel2.Dock = DockStyle.Fill;
			tableLayoutPanel2.Location = new Point(3, 32);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 1;
			tableLayoutPanel2.RowStyles.Add(new RowStyle());
			tableLayoutPanel2.Size = new Size(378, 36);
			tableLayoutPanel2.TabIndex = 4;
			// 
			// enterBtn
			// 
			enterBtn.Anchor = AnchorStyles.None;
			enterBtn.Location = new Point(169, 6);
			enterBtn.Name = "enterBtn";
			enterBtn.Size = new Size(75, 23);
			enterBtn.TabIndex = 2;
			enterBtn.Text = "Войти";
			enterBtn.UseVisualStyleBackColor = true;
			enterBtn.Click += enterBtn_Click;
			// 
			// cancelBtn
			// 
			cancelBtn.Anchor = AnchorStyles.None;
			cancelBtn.Location = new Point(283, 6);
			cancelBtn.Name = "cancelBtn";
			cancelBtn.Size = new Size(75, 23);
			cancelBtn.TabIndex = 3;
			cancelBtn.Text = "Отмена";
			cancelBtn.UseVisualStyleBackColor = true;
			cancelBtn.Click += cancelBtn_Click;
			// 
			// configBtn
			// 
			configBtn.Anchor = AnchorStyles.None;
			configBtn.AutoSize = true;
			configBtn.Location = new Point(3, 5);
			configBtn.Name = "configBtn";
			configBtn.Size = new Size(144, 25);
			configBtn.TabIndex = 4;
			configBtn.Text = "Настройка соединения";
			configBtn.UseVisualStyleBackColor = true;
			configBtn.Click += configBtn_Click;
			// 
			// EnterPasswordForm
			// 
			AcceptButton = enterBtn;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = cancelBtn;
			ClientSize = new Size(384, 71);
			Controls.Add(tableLayoutPanel1);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "EnterPasswordForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Введите пароль";
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			tableLayoutPanel2.ResumeLayout(false);
			tableLayoutPanel2.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel tableLayoutPanel1;
		private Label passwordLabel;
		private TextBox passwordTBox;
		private Button enterBtn;
		private Button cancelBtn;
		private TableLayoutPanel tableLayoutPanel2;
		private Button configBtn;
	}
}