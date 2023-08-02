namespace Simple.Objects.Controls
{
	partial class GroupPropertyPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.labelControlInfo = new DevExpress.XtraEditors.LabelControl();
			this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
			((System.ComponentModel.ISupportInitialize)this.panelControl1).BeginInit();
			this.panelControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelControlInfo
			// 
			this.labelControlInfo.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.labelControlInfo.Appearance.Options.UseFont = true;
			this.labelControlInfo.Location = new System.Drawing.Point(135, 198);
			this.labelControlInfo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.labelControlInfo.Name = "labelControlInfo";
			this.labelControlInfo.Size = new System.Drawing.Size(97, 13);
			this.labelControlInfo.TabIndex = 0;
			this.labelControlInfo.Text = "GroupPropertyPanel";
			// 
			// panelControl1
			// 
			this.panelControl1.Controls.Add(this.labelControlInfo);
			this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelControl1.Location = new System.Drawing.Point(0, 0);
			this.panelControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.panelControl1.Name = "panelControl1";
			this.panelControl1.Size = new System.Drawing.Size(427, 440);
			this.panelControl1.TabIndex = 1;
			// 
			// GroupPropertyPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelControl1);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "GroupPropertyPanel";
			this.Size = new System.Drawing.Size(427, 440);
			((System.ComponentModel.ISupportInitialize)this.panelControl1).EndInit();
			this.panelControl1.ResumeLayout(false);
			this.panelControl1.PerformLayout();
			this.ResumeLayout(false);
		}

		#endregion

		private DevExpress.XtraEditors.LabelControl labelControlInfo;
		private DevExpress.XtraEditors.PanelControl panelControl1;
	}
}
