namespace Simple.Controls
{
	partial class FormFind
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
			this.editorFindWhat = new DevExpress.XtraEditors.MRUEdit();
			this.labelControlFindWhat = new DevExpress.XtraEditors.LabelControl();
			this.buttonFindNext = new DevExpress.XtraEditors.SimpleButton();
			this.checkEditMatchCase = new DevExpress.XtraEditors.CheckEdit();
			this.simpleButtonFindPrevious = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.editorFindWhat.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditMatchCase.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// editorFindWhat
			// 
			this.editorFindWhat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.editorFindWhat.Location = new System.Drawing.Point(8, 28);
			this.editorFindWhat.Name = "editorFindWhat";
			this.editorFindWhat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.editorFindWhat.Size = new System.Drawing.Size(393, 20);
			this.editorFindWhat.TabIndex = 1;
			// 
			// labelControlFindWhat
			// 
			this.labelControlFindWhat.Location = new System.Drawing.Point(8, 9);
			this.labelControlFindWhat.Name = "labelControlFindWhat";
			this.labelControlFindWhat.Size = new System.Drawing.Size(51, 13);
			this.labelControlFindWhat.TabIndex = 33;
			this.labelControlFindWhat.Text = "Find what:";
			// 
			// buttonFindNext
			// 
			this.buttonFindNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonFindNext.Location = new System.Drawing.Point(313, 57);
			this.buttonFindNext.Name = "buttonFindNext";
			this.buttonFindNext.Size = new System.Drawing.Size(88, 26);
			this.buttonFindNext.TabIndex = 6;
			this.buttonFindNext.Text = " Find Next";
			this.buttonFindNext.Click += new System.EventHandler(this.buttonFindNext_Click);
			// 
			// checkEditMatchCase
			// 
			this.checkEditMatchCase.Location = new System.Drawing.Point(6, 54);
			this.checkEditMatchCase.Name = "checkEditMatchCase";
			this.checkEditMatchCase.Properties.AutoWidth = true;
			this.checkEditMatchCase.Properties.Caption = "Match case";
			this.checkEditMatchCase.Size = new System.Drawing.Size(75, 19);
			this.checkEditMatchCase.TabIndex = 2;
			// 
			// simpleButtonFindPrevious
			// 
			this.simpleButtonFindPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonFindPrevious.Location = new System.Drawing.Point(205, 57);
			this.simpleButtonFindPrevious.Name = "simpleButtonFindPrevious";
			this.simpleButtonFindPrevious.Size = new System.Drawing.Size(88, 26);
			this.simpleButtonFindPrevious.TabIndex = 5;
			this.simpleButtonFindPrevious.Text = "Find Previuous";
			this.simpleButtonFindPrevious.Visible = false;
			// 
			// FormFind
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(408, 91);
			this.Controls.Add(this.simpleButtonFindPrevious);
			this.Controls.Add(this.buttonFindNext);
			this.Controls.Add(this.checkEditMatchCase);
			this.Controls.Add(this.editorFindWhat);
			this.Controls.Add(this.labelControlFindWhat);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FormFind";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Find";
			((System.ComponentModel.ISupportInitialize)(this.editorFindWhat.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditMatchCase.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.MRUEdit editorFindWhat;
		protected DevExpress.XtraEditors.LabelControl labelControlFindWhat;
		private DevExpress.XtraEditors.SimpleButton buttonFindNext;
		protected DevExpress.XtraEditors.CheckEdit checkEditMatchCase;
		private DevExpress.XtraEditors.SimpleButton simpleButtonFindPrevious;
	}
}