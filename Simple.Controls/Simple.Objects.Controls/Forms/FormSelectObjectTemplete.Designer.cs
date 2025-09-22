namespace Simple.Objects.Controls
{
    partial class FormSelectObjectTemplate
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
			this.imageList = new System.Windows.Forms.ImageList();
			this.treeList = new Simple.Controls.TreeList.SimpleTreeList();
			this.panelFooter = new DevExpress.XtraEditors.PanelControl();
			this.buttonOK = new DevExpress.XtraEditors.SimpleButton();
			this.buttonCancel = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelFooter)).BeginInit();
			this.panelFooter.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// treeList
			// 
			this.treeList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeList.Location = new System.Drawing.Point(0, 0);
			this.treeList.Name = "treeList";
			this.treeList.OptionsSimple.EditMode = Simple.Controls.TreeList.SimpleTreeEditMode.Standard;
			this.treeList.OptionsSimple.LookAndFeelStyle = Simple.Controls.TreeList.SimpleTreeLookAndFeelStyle.Standard;
			this.treeList.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None; //this.treeList.OptionsView.ShowFocusedFrame = false;
			this.treeList.Size = new System.Drawing.Size(540, 598);
			this.treeList.TabIndex = 0;
			// 
			// panelFooter
			// 
			this.panelFooter.Controls.Add(this.buttonOK);
			this.panelFooter.Controls.Add(this.buttonCancel);
			this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelFooter.Location = new System.Drawing.Point(0, 598);
			this.panelFooter.Name = "panelFooter";
			this.panelFooter.Size = new System.Drawing.Size(540, 44);
			this.panelFooter.TabIndex = 19;
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(304, 10);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(110, 26);
			this.buttonOK.TabIndex = 1;
			this.buttonOK.Text = "OK";
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(439, 10);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(89, 26);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// FormSelectObjectTemplate
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(540, 642);
			this.Controls.Add(this.treeList);
			this.Controls.Add(this.panelFooter);
			this.MaximizeBox = false;
			this.Name = "FormSelectObjectTemplate";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FormSelectObject";
			((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelFooter)).EndInit();
			this.panelFooter.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private SimpleObjectGraphController graphController;
        protected System.Windows.Forms.ImageList imageList;
        protected Simple.Controls.TreeList.SimpleTreeList treeList;
        protected DevExpress.XtraEditors.PanelControl panelFooter;
        protected DevExpress.XtraEditors.SimpleButton buttonOK;
		protected DevExpress.XtraEditors.SimpleButton buttonCancel;
    }
}