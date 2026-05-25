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
            tabPage6 = new TabPage();
            tabPage7 = new TabPage();
            menuStrip = new MenuStrip();
            downloadToExcelToolStripMenuItem = new ToolStripMenuItem();
            downloadQRsToolStripMenuItem = new ToolStripMenuItem();
            downloadInventoryCardsToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1 = new TableLayoutPanel();
            tabControl.SuspendLayout();
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
            tabControl.Size = new Size(978, 431);
            tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(970, 403);
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
            // 
            // tabPage5
            // 
            tabPage5.Location = new Point(4, 24);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(970, 353);
            tabPage5.TabIndex = 6;
            tabPage5.Text = "Полный список (только чтение)";
            tabPage5.UseVisualStyleBackColor = true;
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
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(menuStrip, 0, 0);
            tableLayoutPanel1.Controls.Add(tabControl, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
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
        private ToolStripMenuItem downloadToExcelToolStripMenuItem;
        private ToolStripMenuItem downloadQRsToolStripMenuItem;
        private ToolStripMenuItem downloadInventoryCardsToolStripMenuItem;
        private TabPage tabPage5;
        private TableLayoutPanel tableLayoutPanel1;
        private TabPage tabPage6;
        private TabPage tabPage7;
    }
}
