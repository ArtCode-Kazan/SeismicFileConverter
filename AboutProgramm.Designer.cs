namespace BinaryToJSONConverterApp
{
    partial class AboutProgramm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonMail = new System.Windows.Forms.Button();
            this.separatorControl = new DevExpress.XtraEditors.SeparatorControl();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "SeismicConverter 1.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Copyright© 2022-2023 by Art-Code";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.Location = new System.Drawing.Point(12, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Thank for using this programm.";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(123, 203);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(71, 33);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonMail
            // 
            this.buttonMail.Location = new System.Drawing.Point(12, 203);
            this.buttonMail.Name = "buttonMail";
            this.buttonMail.Size = new System.Drawing.Size(71, 33);
            this.buttonMail.TabIndex = 4;
            this.buttonMail.Text = "Mail us!";
            this.buttonMail.UseVisualStyleBackColor = true;
            this.buttonMail.Click += new System.EventHandler(this.buttonMail_Click);
            // 
            // separatorControl
            // 
            this.separatorControl.LineColor = System.Drawing.Color.Silver;
            this.separatorControl.Location = new System.Drawing.Point(-17, 174);
            this.separatorControl.Name = "separatorControl";
            this.separatorControl.Size = new System.Drawing.Size(240, 23);
            this.separatorControl.TabIndex = 5;
            // 
            // AboutProgramm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 248);
            this.Controls.Add(this.separatorControl);
            this.Controls.Add(this.buttonMail);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(222, 287);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(222, 287);
            this.Name = "AboutProgramm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About SeismicConverter";
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonMail;
        private DevExpress.XtraEditors.SeparatorControl separatorControl;
    }
}