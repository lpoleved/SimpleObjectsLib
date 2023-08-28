namespace Simple.Controls
{
    partial class FormBackgroundWorker<TResult, TArgument>
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
            this.panelFooter = new DevExpress.XtraEditors.PanelControl();
            this.buttonCloseCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelMessage = new DevExpress.XtraEditors.LabelControl();
            this.progressBar = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).BeginInit();
            this.panelFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.buttonCloseCancel);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 68);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(382, 37);
            this.panelFooter.TabIndex = 0;
            // 
            // buttonCloseCancel
            // 
            this.buttonCloseCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCloseCancel.Location = new System.Drawing.Point(283, 5);
            this.buttonCloseCancel.Name = "buttonCloseCancel";
            this.buttonCloseCancel.Size = new System.Drawing.Size(94, 26);
            this.buttonCloseCancel.TabIndex = 2;
            this.buttonCloseCancel.Text = "Cancel";
            this.buttonCloseCancel.Click += new System.EventHandler(this.buttonCloseCancel_Click);
            // 
            // labelMessage
            // 
            this.labelMessage.AutoEllipsis = true;
            this.labelMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.labelMessage.Location = new System.Drawing.Point(12, 24);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(42, 13);
            this.labelMessage.TabIndex = 1;
            this.labelMessage.Text = "Message";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 43);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(357, 19);
            this.progressBar.TabIndex = 2;
            // 
            // FormBackgroundWorker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 105);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.panelFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormBackgroundWorker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Progress";
            this.Activated += new System.EventHandler(this.FormBackgroundWorker_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBackgroundWorker_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).EndInit();
            this.panelFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelFooter;
        private DevExpress.XtraEditors.SimpleButton buttonCloseCancel;
        private DevExpress.XtraEditors.LabelControl labelMessage;
        private DevExpress.XtraEditors.ProgressBarControl progressBar;
    }
}