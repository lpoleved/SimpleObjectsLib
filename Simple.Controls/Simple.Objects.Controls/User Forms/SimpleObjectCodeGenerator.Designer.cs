namespace Simple.Objects.Controls
{
    partial class ObjectCodeGeneratorControl
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
			this.memoEdit = new DevExpress.XtraEditors.MemoEdit();
			((System.ComponentModel.ISupportInitialize)(this.memoEdit.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// memoEdit
			// 
			this.memoEdit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.memoEdit.Location = new System.Drawing.Point(0, 0);
			this.memoEdit.Name = "memoEdit";
			this.memoEdit.Size = new System.Drawing.Size(649, 536);
			this.memoEdit.TabIndex = 0;
			// 
			// SimpleObjectCodeGenerator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.memoEdit);
			this.Name = "SimpleObjectCodeGenerator";
			this.Size = new System.Drawing.Size(649, 536);
			((System.ComponentModel.ISupportInitialize)(this.memoEdit.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit memoEdit;
    }
}
