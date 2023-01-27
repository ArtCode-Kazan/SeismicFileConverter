namespace BinaryToJSONConverterApp
{
    partial class MainConverterWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonBrowseJsonFiles = new System.Windows.Forms.Button();
            this.textBoxLoadFromFolder = new System.Windows.Forms.TextBox();
            this.labelLoadFromFolder = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.buttonBrowseSaveFolder = new System.Windows.Forms.Button();
            this.textBoxSaveFolder = new System.Windows.Forms.TextBox();
            this.labelSaveToFolder = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ReportAProblemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonBrowseJsonFiles
            // 
            this.buttonBrowseJsonFiles.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonBrowseJsonFiles.Location = new System.Drawing.Point(345, 63);
            this.buttonBrowseJsonFiles.Name = "buttonBrowseJsonFiles";
            this.buttonBrowseJsonFiles.Size = new System.Drawing.Size(58, 25);
            this.buttonBrowseJsonFiles.TabIndex = 8;
            this.buttonBrowseJsonFiles.Text = "browse";
            this.buttonBrowseJsonFiles.UseVisualStyleBackColor = true;
            this.buttonBrowseJsonFiles.Click += new System.EventHandler(this.buttonBrowseJsonFiles_Click);
            // 
            // textBoxLoadFromFolder
            // 
            this.textBoxLoadFromFolder.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxLoadFromFolder.Location = new System.Drawing.Point(119, 64);
            this.textBoxLoadFromFolder.Name = "textBoxLoadFromFolder";
            this.textBoxLoadFromFolder.ReadOnly = true;
            this.textBoxLoadFromFolder.Size = new System.Drawing.Size(226, 23);
            this.textBoxLoadFromFolder.TabIndex = 7;
            // 
            // labelLoadFromFolder
            // 
            this.labelLoadFromFolder.AutoSize = true;
            this.labelLoadFromFolder.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelLoadFromFolder.Location = new System.Drawing.Point(12, 65);
            this.labelLoadFromFolder.Name = "labelLoadFromFolder";
            this.labelLoadFromFolder.Size = new System.Drawing.Size(101, 19);
            this.labelLoadFromFolder.TabIndex = 6;
            this.labelLoadFromFolder.Text = "Input json files:";
            // 
            // buttonConvert
            // 
            this.buttonConvert.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonConvert.Location = new System.Drawing.Point(166, 185);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(82, 33);
            this.buttonConvert.TabIndex = 9;
            this.buttonConvert.Text = "Convert";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // buttonBrowseSaveFolder
            // 
            this.buttonBrowseSaveFolder.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonBrowseSaveFolder.Location = new System.Drawing.Point(345, 107);
            this.buttonBrowseSaveFolder.Name = "buttonBrowseSaveFolder";
            this.buttonBrowseSaveFolder.Size = new System.Drawing.Size(58, 25);
            this.buttonBrowseSaveFolder.TabIndex = 12;
            this.buttonBrowseSaveFolder.Text = "browse";
            this.buttonBrowseSaveFolder.UseVisualStyleBackColor = true;
            this.buttonBrowseSaveFolder.Click += new System.EventHandler(this.buttonBrowseSaveFolder_Click);
            // 
            // textBoxSaveFolder
            // 
            this.textBoxSaveFolder.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxSaveFolder.Location = new System.Drawing.Point(119, 108);
            this.textBoxSaveFolder.Name = "textBoxSaveFolder";
            this.textBoxSaveFolder.ReadOnly = true;
            this.textBoxSaveFolder.Size = new System.Drawing.Size(226, 23);
            this.textBoxSaveFolder.TabIndex = 11;
            // 
            // labelSaveToFolder
            // 
            this.labelSaveToFolder.AutoSize = true;
            this.labelSaveToFolder.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelSaveToFolder.Location = new System.Drawing.Point(12, 109);
            this.labelSaveToFolder.Name = "labelSaveToFolder";
            this.labelSaveToFolder.Size = new System.Drawing.Size(81, 19);
            this.labelSaveToFolder.TabIndex = 10;
            this.labelSaveToFolder.Text = "Export root:";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "JSON files (*.json)|*.json";
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.ReadOnlyChecked = true;
            this.openFileDialog.RestoreDirectory = true;
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 239);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(414, 22);
            this.statusStrip.TabIndex = 13;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(414, 27);
            this.menuStrip.TabIndex = 16;
            this.menuStrip.Text = "Menu";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenHelpToolStripMenuItem,
            this.toolStripSeparator1,
            this.updateToolStripMenuItem,
            this.toolStripSeparator3,
            this.ReportAProblemToolStripMenuItem,
            this.toolStripSeparator2,
            this.AboutToolStripMenuItem});
            this.оПрограммеToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(49, 23);
            this.оПрограммеToolStripMenuItem.Text = "Help";
            // 
            // OpenHelpToolStripMenuItem
            // 
            this.OpenHelpToolStripMenuItem.Name = "OpenHelpToolStripMenuItem";
            this.OpenHelpToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
            this.OpenHelpToolStripMenuItem.Text = "Open help";
            this.OpenHelpToolStripMenuItem.Click += new System.EventHandler(this.OpenHelpToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(216, 6);
            // 
            // ReportAProblemToolStripMenuItem
            // 
            this.ReportAProblemToolStripMenuItem.Name = "ReportAProblemToolStripMenuItem";
            this.ReportAProblemToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
            this.ReportAProblemToolStripMenuItem.Text = "Report a problem";
            this.ReportAProblemToolStripMenuItem.Click += new System.EventHandler(this.ReportAProblemToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(216, 6);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
            this.AboutToolStripMenuItem.Text = "About BinaryConverter";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(216, 6);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
            this.updateToolStripMenuItem.Text = "Update";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // MainConverterWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 261);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.buttonBrowseSaveFolder);
            this.Controls.Add(this.textBoxSaveFolder);
            this.Controls.Add(this.labelSaveToFolder);
            this.Controls.Add(this.buttonConvert);
            this.Controls.Add(this.buttonBrowseJsonFiles);
            this.Controls.Add(this.textBoxLoadFromFolder);
            this.Controls.Add(this.labelLoadFromFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(430, 300);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(430, 300);
            this.Name = "MainConverterWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Binary converter";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBrowseJsonFiles;
        private System.Windows.Forms.TextBox textBoxLoadFromFolder;
        private System.Windows.Forms.Label labelLoadFromFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button buttonConvert;
        private System.Windows.Forms.Button buttonBrowseSaveFolder;
        private System.Windows.Forms.TextBox textBoxSaveFolder;
        private System.Windows.Forms.Label labelSaveToFolder;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReportAProblemToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

