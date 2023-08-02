namespace Simple.Objects.Controls
{
	partial class UndoPopup
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
			this.Label = new DevExpress.XtraEditors.LabelControl();
			this.ListBox = new DevExpress.XtraEditors.ListBoxControl();
			((System.ComponentModel.ISupportInitialize)(this.ListBox)).BeginInit();
			this.SuspendLayout();
			// 
			// Label
			// 
			this.Label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Label.Appearance.Options.UseTextOptions = true;
			this.Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.Label.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.Label.Location = new System.Drawing.Point(0, 289);
			this.Label.Name = "Label";
			this.Label.Size = new System.Drawing.Size(169, 13);
			this.Label.TabIndex = 0;
			this.Label.Text = "Undo/Redo Action Info";
			// 
			// ListBox
			// 
			this.ListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ListBox.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.ListBox.Cursor = System.Windows.Forms.Cursors.Default;
			this.ListBox.Location = new System.Drawing.Point(0, 0);
			this.ListBox.Name = "ListBox";
			this.ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.ListBox.ShowFocusRect = false;
			this.ListBox.Size = new System.Drawing.Size(169, 287);
			this.ListBox.TabIndex = 2;
			this.ListBox.MouseLeave += new System.EventHandler(this.ListBox_MouseLeave);
			this.ListBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ListBox_MouseMove);
			// 
			// UndoPopup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Label);
			this.Controls.Add(this.ListBox);
			this.Name = "UndoPopup";
			this.Size = new System.Drawing.Size(169, 302);
			((System.ComponentModel.ISupportInitialize)(this.ListBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		public DevExpress.XtraEditors.LabelControl Label;
		public DevExpress.XtraEditors.ListBoxControl ListBox;
	}
}
