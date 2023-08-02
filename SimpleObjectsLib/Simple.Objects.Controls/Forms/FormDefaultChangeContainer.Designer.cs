namespace Simple.Objects.Controls
{
	partial class FormDefaultChangeContainer
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
			this.components = new System.ComponentModel.Container();
			this.treeList = new Simple.Controls.TreeList.SimpleTreeList();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
			this.barButtonSort = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemValidate = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonCommitChanges = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemCancelDeleteRequests = new DevExpress.XtraBars.BarButtonItem();
			this.barManager = new DevExpress.XtraBars.BarManager(this.components);
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			this.SuspendLayout();
			// 
			// treeList
			// 
			this.treeList.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
			this.treeList.Appearance.FocusedCell.ForeColor = System.Drawing.SystemColors.ControlText;
			this.treeList.Appearance.FocusedCell.Options.UseBackColor = true;
			this.treeList.Appearance.FocusedCell.Options.UseForeColor = true;
			this.treeList.Appearance.FocusedRow.ForeColor = System.Drawing.SystemColors.ControlText;
			this.treeList.Appearance.FocusedRow.Options.UseBackColor = true;
			this.treeList.Appearance.FocusedRow.Options.UseForeColor = true;
			this.treeList.Appearance.HideSelectionRow.ForeColor = System.Drawing.SystemColors.ControlText;
			this.treeList.Appearance.HideSelectionRow.Options.UseBackColor = true;
			this.treeList.Appearance.HideSelectionRow.Options.UseForeColor = true;
			this.treeList.Appearance.SelectedRow.Options.UseForeColor = true;
			this.treeList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeList.Location = new System.Drawing.Point(0, 0);
			this.treeList.Name = "treeList";
			this.treeList.OptionsBehavior.Editable = false;
			this.treeList.OptionsMenu.ShowExpandCollapseItems = false;
			this.treeList.OptionsSimple.EditMode = Simple.Controls.TreeList.SimpleTreeEditMode.ViewOnly;
			this.treeList.OptionsSimple.LookAndFeelStyle = Simple.Controls.TreeList.SimpleTreeLookAndFeelStyle.ExcelStyle;
			this.treeList.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
			this.treeList.OptionsView.ShowIndicator = false;
			this.treeList.OptionsView.ShowVertLines = false;
			this.treeList.OptionsView.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None;
			this.treeList.SelectImageList = this.imageList;
			this.treeList.Size = new System.Drawing.Size(650, 226);
			this.treeList.TabIndex = 0;
			this.treeList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeList_MouseDown);
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// popupMenu
			// 
			this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonSort),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemValidate, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonCommitChanges, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemCancelDeleteRequests, true)});
			this.popupMenu.Manager = this.barManager;
			this.popupMenu.MenuCaption = "Change Container";
			this.popupMenu.Name = "popupMenu";
			this.popupMenu.ShowCaption = true;
			// 
			// barButtonSort
			// 
			this.barButtonSort.Caption = "Sort";
			this.barButtonSort.Id = 0;
			this.barButtonSort.Name = "barButtonSort";
			this.barButtonSort.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonSort_ItemClick_1);
			// 
			// barButtonItemValidate
			// 
			this.barButtonItemValidate.Caption = "Validate";
			this.barButtonItemValidate.Id = 3;
			this.barButtonItemValidate.Name = "barButtonItemValidate";
			this.barButtonItemValidate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemValidate_ItemClick);
			// 
			// barButtonCommitChanges
			// 
			this.barButtonCommitChanges.Caption = "Commit Changes";
			this.barButtonCommitChanges.Id = 2;
			this.barButtonCommitChanges.Name = "barButtonCommitChanges";
			this.barButtonCommitChanges.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonCommitChanges_ItemClick);
			// 
			// barButtonItemCancelDeleteRequests
			// 
			this.barButtonItemCancelDeleteRequests.Caption = "Cancel Delete Requests";
			this.barButtonItemCancelDeleteRequests.Id = 4;
			this.barButtonItemCancelDeleteRequests.Name = "barButtonItemCancelDeleteRequests";
			this.barButtonItemCancelDeleteRequests.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCancelDeleteRequests_ItemClick);
			// 
			// barManager
			// 
			this.barManager.DockControls.Add(this.barDockControlTop);
			this.barManager.DockControls.Add(this.barDockControlBottom);
			this.barManager.DockControls.Add(this.barDockControlLeft);
			this.barManager.DockControls.Add(this.barDockControlRight);
			this.barManager.Form = this;
			this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonSort,
            this.barButtonCommitChanges,
            this.barButtonItemValidate,
            this.barButtonItemCancelDeleteRequests});
			this.barManager.MaxItemId = 5;
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Manager = this.barManager;
			this.barDockControlTop.Size = new System.Drawing.Size(650, 0);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 226);
			this.barDockControlBottom.Manager = this.barManager;
			this.barDockControlBottom.Size = new System.Drawing.Size(650, 0);
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
			this.barDockControlLeft.Manager = this.barManager;
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 226);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(650, 0);
			this.barDockControlRight.Manager = this.barManager;
			this.barDockControlRight.Size = new System.Drawing.Size(0, 226);
			// 
			// FormDefaultChangeContainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(650, 226);
			this.Controls.Add(this.treeList);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Name = "FormDefaultChangeContainer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Default Change Container";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDefaultChangeContainer_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormDefaultChangeContainer_FormClosed);
			((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Simple.Controls.TreeList.SimpleTreeList treeList;
		private System.Windows.Forms.ImageList imageList;
		private DevExpress.XtraBars.PopupMenu popupMenu;
		private DevExpress.XtraBars.BarButtonItem barButtonSort;
		private DevExpress.XtraBars.BarManager barManager;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarButtonItem barButtonCommitChanges;
		private DevExpress.XtraBars.BarButtonItem barButtonItemValidate;
		private DevExpress.XtraBars.BarButtonItem barButtonItemCancelDeleteRequests;
	}
}