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
            this.buttonHelp = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonBrowseJsonFiles
            // 
            this.buttonBrowseJsonFiles.Location = new System.Drawing.Point(326, 62);
            this.buttonBrowseJsonFiles.Name = "buttonBrowseJsonFiles";
            this.buttonBrowseJsonFiles.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseJsonFiles.TabIndex = 8;
            this.buttonBrowseJsonFiles.Text = "browse";
            this.buttonBrowseJsonFiles.UseVisualStyleBackColor = true;
            this.buttonBrowseJsonFiles.Click += new System.EventHandler(this.buttonBrowseJsonFiles_Click);
            // 
            // textBoxLoadFromFolder
            // 
            this.textBoxLoadFromFolder.Location = new System.Drawing.Point(53, 64);
            this.textBoxLoadFromFolder.Name = "textBoxLoadFromFolder";
            this.textBoxLoadFromFolder.ReadOnly = true;
            this.textBoxLoadFromFolder.Size = new System.Drawing.Size(275, 20);
            this.textBoxLoadFromFolder.TabIndex = 7;
            // 
            // labelLoadFromFolder
            // 
            this.labelLoadFromFolder.AutoSize = true;
            this.labelLoadFromFolder.Location = new System.Drawing.Point(13, 67);
            this.labelLoadFromFolder.Name = "labelLoadFromFolder";
            this.labelLoadFromFolder.Size = new System.Drawing.Size(34, 13);
            this.labelLoadFromFolder.TabIndex = 6;
            this.labelLoadFromFolder.Text = "Load:";
            // 
            // buttonConvert
            // 
            this.buttonConvert.Location = new System.Drawing.Point(164, 179);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(75, 23);
            this.buttonConvert.TabIndex = 9;
            this.buttonConvert.Text = "Convert";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // buttonBrowseSaveFolder
            // 
            this.buttonBrowseSaveFolder.Location = new System.Drawing.Point(326, 106);
            this.buttonBrowseSaveFolder.Name = "buttonBrowseSaveFolder";
            this.buttonBrowseSaveFolder.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseSaveFolder.TabIndex = 12;
            this.buttonBrowseSaveFolder.Text = "browse";
            this.buttonBrowseSaveFolder.UseVisualStyleBackColor = true;
            this.buttonBrowseSaveFolder.Click += new System.EventHandler(this.buttonBrowseSaveFolder_Click);
            // 
            // textBoxSaveFolder
            // 
            this.textBoxSaveFolder.Location = new System.Drawing.Point(53, 108);
            this.textBoxSaveFolder.Name = "textBoxSaveFolder";
            this.textBoxSaveFolder.ReadOnly = true;
            this.textBoxSaveFolder.Size = new System.Drawing.Size(275, 20);
            this.textBoxSaveFolder.TabIndex = 11;
            // 
            // labelSaveToFolder
            // 
            this.labelSaveToFolder.AutoSize = true;
            this.labelSaveToFolder.Location = new System.Drawing.Point(12, 111);
            this.labelSaveToFolder.Name = "labelSaveToFolder";
            this.labelSaveToFolder.Size = new System.Drawing.Size(35, 13);
            this.labelSaveToFolder.TabIndex = 10;
            this.labelSaveToFolder.Text = "Save:";
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
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 239);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(413, 22);
            this.statusStrip.TabIndex = 13;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // buttonHelp
            // 
            this.buttonHelp.BackColor = System.Drawing.SystemColors.Control;
            this.buttonHelp.Location = new System.Drawing.Point(386, 7);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(20, 20);
            this.buttonHelp.TabIndex = 14;
            this.buttonHelp.Text = "?";
            this.buttonHelp.UseVisualStyleBackColor = false;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // MainConverterWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 261);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.buttonBrowseSaveFolder);
            this.Controls.Add(this.textBoxSaveFolder);
            this.Controls.Add(this.labelSaveToFolder);
            this.Controls.Add(this.buttonConvert);
            this.Controls.Add(this.buttonBrowseJsonFiles);
            this.Controls.Add(this.textBoxLoadFromFolder);
            this.Controls.Add(this.labelLoadFromFolder);
            this.MaximumSize = new System.Drawing.Size(429, 300);
            this.MinimumSize = new System.Drawing.Size(429, 300);
            this.Name = "MainConverterWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Binary converter";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
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
        private System.Windows.Forms.Button buttonHelp;
    }
}

