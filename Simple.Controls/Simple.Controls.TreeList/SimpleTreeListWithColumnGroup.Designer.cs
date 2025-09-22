namespace Simple.Controls.TreeList
{
    partial class SimpleTreeListWithColumnGroup
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
			this.columnGroup = new Simple.Controls.TreeList.SimpleTreeList();
			this.treeList = new Simple.Controls.TreeList.SimpleTreeList();
			((System.ComponentModel.ISupportInitialize)(this.columnGroup)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
			this.SuspendLayout();
			// 
			// columnGroup
			// 
			this.columnGroup.Dock = System.Windows.Forms.DockStyle.Fill;
			this.columnGroup.Location = new System.Drawing.Point(0, 0);
			this.columnGroup.Name = "columnGroup";
			this.columnGroup.OptionsSimple.EditMode = Simple.Controls.TreeList.SimpleTreeEditMode.Standard;
			this.columnGroup.OptionsSimple.LookAndFeelStyle = Simple.Controls.TreeList.SimpleTreeLookAndFeelStyle.Standard;
			this.columnGroup.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
			this.columnGroup.OptionsView.ShowHorzLines = false;
			this.columnGroup.Size = new System.Drawing.Size(387, 410);
			this.columnGroup.TabIndex = 0;
			// 
			// treeList
			// 
			this.treeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeList.Location = new System.Drawing.Point(0, 20);
			this.treeList.Name = "treeList";
			this.treeList.OptionsSimple.EditMode = Simple.Controls.TreeList.SimpleTreeEditMode.Standard;
			this.treeList.OptionsSimple.LookAndFeelStyle = Simple.Controls.TreeList.SimpleTreeLookAndFeelStyle.Standard;
			this.treeList.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
			this.treeList.OptionsView.ShowHorzLines = false;
			this.treeList.Size = new System.Drawing.Size(387, 390);
			this.treeList.TabIndex = 1;
			// 
			// SimpleTreeListWithColumnGroup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.treeList);
			this.Controls.Add(this.columnGroup);
			this.Name = "SimpleTreeListWithColumnGroup";
			this.Size = new System.Drawing.Size(387, 410);
			((System.ComponentModel.ISupportInitialize)(this.columnGroup)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private SimpleTreeList columnGroup;
        private SimpleTreeList treeList;
    }
}
