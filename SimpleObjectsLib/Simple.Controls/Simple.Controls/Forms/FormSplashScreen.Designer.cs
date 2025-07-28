namespace Simple.Controls
{
    partial class FormSplashScreen 
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
            if(disposing && (components != null)) 
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
        private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSplashScreen));
			this.marqueeProgressBarControl1 = new DevExpress.XtraEditors.MarqueeProgressBarControl();
			this.labelControlCopyright = new DevExpress.XtraEditors.LabelControl();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
			this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
			((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// marqueeProgressBarControl1
			// 
			this.marqueeProgressBarControl1.EditValue = 0;
			this.marqueeProgressBarControl1.Location = new System.Drawing.Point(12, 288);
			this.marqueeProgressBarControl1.Name = "marqueeProgressBarControl1";
			this.marqueeProgressBarControl1.Size = new System.Drawing.Size(259, 10);
			this.marqueeProgressBarControl1.TabIndex = 5;
			// 
			// labelControlCopyright
			// 
			this.labelControlCopyright.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.labelControlCopyright.Location = new System.Drawing.Point(12, 323);
			this.labelControlCopyright.Name = "labelControlCopyright";
			this.labelControlCopyright.Size = new System.Drawing.Size(87, 13);
			this.labelControlCopyright.TabIndex = 6;
			this.labelControlCopyright.Text = "Copyright © 2025";
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(12, 269);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(50, 13);
			this.labelControl2.TabIndex = 7;
			this.labelControl2.Text = "Starting...";
			// 
			// labelControl3
			// 
			this.labelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.labelControl3.Location = new System.Drawing.Point(12, 342);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(141, 13);
			this.labelControl3.TabIndex = 10;
			this.labelControl3.Text = "Powered by Simple.Objects™";
			// 
			// pictureEdit2
			// 
			this.pictureEdit2.EditValue = ((object)(resources.GetObject("pictureEdit2.EditValue")));
			this.pictureEdit2.Location = new System.Drawing.Point(12, 12);
			this.pictureEdit2.Name = "pictureEdit2";
			this.pictureEdit2.Properties.AllowFocused = false;
			this.pictureEdit2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pictureEdit2.Properties.Appearance.Options.UseBackColor = true;
			this.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
			this.pictureEdit2.Size = new System.Drawing.Size(259, 251);
			this.pictureEdit2.TabIndex = 9;
			// 
			// pictureEdit1
			// 
			this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
			this.pictureEdit1.Location = new System.Drawing.Point(221, 312);
			this.pictureEdit1.Name = "pictureEdit1";
			this.pictureEdit1.Properties.AllowFocused = false;
			this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
			this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEdit1.Properties.ShowMenu = false;
			this.pictureEdit1.Size = new System.Drawing.Size(51, 51);
			this.pictureEdit1.TabIndex = 8;
			// 
			// FormSplashScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(283, 367);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.pictureEdit2);
			this.Controls.Add(this.pictureEdit1);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.labelControlCopyright);
			this.Controls.Add(this.marqueeProgressBarControl1);
			this.Name = "FormSplashScreen";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.MarqueeProgressBarControl marqueeProgressBarControl1;
        private DevExpress.XtraEditors.LabelControl labelControlCopyright;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}
