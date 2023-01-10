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
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxLoadFromFolder = new System.Windows.Forms.TextBox();
            this.labelLoadFromFolder = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonGo = new System.Windows.Forms.Button();
            this.buttonSaveBrowse = new System.Windows.Forms.Button();
            this.textBoxSaveFolder = new System.Windows.Forms.TextBox();
            this.labelSaveToFolder = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(326, 62);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 8;
            this.buttonBrowse.Text = "browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
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
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(164, 179);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(75, 23);
            this.buttonGo.TabIndex = 9;
            this.buttonGo.Text = "Convert";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // buttonSaveBrowse
            // 
            this.buttonSaveBrowse.Location = new System.Drawing.Point(326, 106);
            this.buttonSaveBrowse.Name = "buttonSaveBrowse";
            this.buttonSaveBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveBrowse.TabIndex = 12;
            this.buttonSaveBrowse.Text = "browse";
            this.buttonSaveBrowse.UseVisualStyleBackColor = true;
            this.buttonSaveBrowse.Click += new System.EventHandler(this.buttonSaveBrowse_Click);
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
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "JSON files (*.json)|*.json";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.ReadOnlyChecked = true;
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 239);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(413, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
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
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonSaveBrowse);
            this.Controls.Add(this.textBoxSaveFolder);
            this.Controls.Add(this.labelSaveToFolder);
            this.Controls.Add(this.buttonGo);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxLoadFromFolder);
            this.Controls.Add(this.labelLoadFromFolder);
            this.MaximumSize = new System.Drawing.Size(429, 300);
            this.MinimumSize = new System.Drawing.Size(429, 300);
            this.Name = "MainConverterWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Binary converter";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textBoxLoadFromFolder;
        private System.Windows.Forms.Label labelLoadFromFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.Button buttonSaveBrowse;
        private System.Windows.Forms.TextBox textBoxSaveFolder;
        private System.Windows.Forms.Label labelSaveToFolder;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button buttonHelp;
    }
}

