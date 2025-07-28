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
			components = new System.ComponentModel.Container();
			treeList = new Simple.Controls.TreeList.SimpleTreeList();
			imageList = new System.Windows.Forms.ImageList(components);
			popupMenu = new DevExpress.XtraBars.PopupMenu(components);
			barButtonSort = new DevExpress.XtraBars.BarButtonItem();
			barButtonItemValidate = new DevExpress.XtraBars.BarButtonItem();
			barButtonCommitChanges = new DevExpress.XtraBars.BarButtonItem();
			barButtonItemCancelDeleteRequests = new DevExpress.XtraBars.BarButtonItem();
			barManager = new DevExpress.XtraBars.BarManager(components);
			barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			barButtonItemRefresh = new DevExpress.XtraBars.BarButtonItem();
			((System.ComponentModel.ISupportInitialize)treeList).BeginInit();
			((System.ComponentModel.ISupportInitialize)popupMenu).BeginInit();
			((System.ComponentModel.ISupportInitialize)barManager).BeginInit();
			SuspendLayout();
			// 
			// treeList
			// 
			treeList.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
			treeList.Appearance.FocusedCell.ForeColor = System.Drawing.SystemColors.ControlText;
			treeList.Appearance.FocusedRow.ForeColor = System.Drawing.SystemColors.ControlText;
			treeList.Appearance.FocusedRow.Options.UseBackColor = true;
			treeList.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
			treeList.Appearance.HideSelectionRow.Options.UseBackColor = true;
			treeList.Appearance.SelectedRow.Options.UseForeColor = true;
			treeList.Dock = System.Windows.Forms.DockStyle.Fill;
			treeList.Location = new System.Drawing.Point(0, 0);
			treeList.Name = "treeList";
			treeList.OptionsBehavior.Editable = false;
			treeList.OptionsMenu.ShowExpandCollapseItems = false;
			treeList.OptionsSelection.EnableAppearanceFocusedCell = false;
			treeList.OptionsSelection.EnableAppearanceFocusedRow = false;
			treeList.OptionsSimple.EditMode = Simple.Controls.TreeList.SimpleTreeEditMode.ViewOnly;
			treeList.OptionsSimple.LookAndFeelStyle = Simple.Controls.TreeList.SimpleTreeLookAndFeelStyle.ExcelStyle;
			treeList.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
			treeList.OptionsView.ShowIndicator = false;
			treeList.OptionsView.ShowVertLines = false;
			treeList.OptionsView.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None;
			treeList.SelectImageList = imageList;
			treeList.Size = new System.Drawing.Size(650, 226);
			treeList.TabIndex = 0;
			treeList.MouseDown += treeList_MouseDown;
			// 
			// imageList
			// 
			imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			imageList.ImageSize = new System.Drawing.Size(16, 16);
			imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// popupMenu
			// 
			popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] { new DevExpress.XtraBars.LinkPersistInfo(barButtonSort), new DevExpress.XtraBars.LinkPersistInfo(barButtonItemValidate, true), new DevExpress.XtraBars.LinkPersistInfo(barButtonCommitChanges, true), new DevExpress.XtraBars.LinkPersistInfo(barButtonItemCancelDeleteRequests, true), new DevExpress.XtraBars.LinkPersistInfo(barButtonItemRefresh, true) });
			popupMenu.Manager = barManager;
			popupMenu.MenuCaption = "Change Container";
			popupMenu.Name = "popupMenu";
			popupMenu.ShowCaption = true;
			// 
			// barButtonSort
			// 
			barButtonSort.Caption = "Sort";
			barButtonSort.Id = 0;
			barButtonSort.Name = "barButtonSort";
			barButtonSort.ItemClick += barButtonSort_ItemClick_1;
			// 
			// barButtonItemValidate
			// 
			barButtonItemValidate.Caption = "Validate";
			barButtonItemValidate.Id = 3;
			barButtonItemValidate.Name = "barButtonItemValidate";
			barButtonItemValidate.ItemClick += barButtonItemValidate_ItemClick;
			// 
			// barButtonCommitChanges
			// 
			barButtonCommitChanges.Caption = "Commit Changes";
			barButtonCommitChanges.Id = 2;
			barButtonCommitChanges.Name = "barButtonCommitChanges";
			barButtonCommitChanges.ItemClick += barButtonCommitChanges_ItemClick;
			// 
			// barButtonItemCancelDeleteRequests
			// 
			barButtonItemCancelDeleteRequests.Caption = "Cancel Delete Requests";
			barButtonItemCancelDeleteRequests.Id = 4;
			barButtonItemCancelDeleteRequests.Name = "barButtonItemCancelDeleteRequests";
			barButtonItemCancelDeleteRequests.ItemClick += barButtonItemCancelDeleteRequests_ItemClick;
			// 
			// barManager
			// 
			barManager.DockControls.Add(barDockControlTop);
			barManager.DockControls.Add(barDockControlBottom);
			barManager.DockControls.Add(barDockControlLeft);
			barManager.DockControls.Add(barDockControlRight);
			barManager.Form = this;
			barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] { barButtonSort, barButtonCommitChanges, barButtonItemValidate, barButtonItemCancelDeleteRequests, barButtonItemRefresh });
			barManager.MaxItemId = 6;
			// 
			// barDockControlTop
			// 
			barDockControlTop.CausesValidation = false;
			barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			barDockControlTop.Location = new System.Drawing.Point(0, 0);
			barDockControlTop.Manager = barManager;
			barDockControlTop.Size = new System.Drawing.Size(650, 0);
			// 
			// barDockControlBottom
			// 
			barDockControlBottom.CausesValidation = false;
			barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			barDockControlBottom.Location = new System.Drawing.Point(0, 226);
			barDockControlBottom.Manager = barManager;
			barDockControlBottom.Size = new System.Drawing.Size(650, 0);
			// 
			// barDockControlLeft
			// 
			barDockControlLeft.CausesValidation = false;
			barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			barDockControlLeft.Location = new System.Drawing.Point(0, 0);
			barDockControlLeft.Manager = barManager;
			barDockControlLeft.Size = new System.Drawing.Size(0, 226);
			// 
			// barDockControlRight
			// 
			barDockControlRight.CausesValidation = false;
			barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			barDockControlRight.Location = new System.Drawing.Point(650, 0);
			barDockControlRight.Manager = barManager;
			barDockControlRight.Size = new System.Drawing.Size(0, 226);
			// 
			// barButtonItemRefresh
			// 
			barButtonItemRefresh.Caption = "Refresh";
			barButtonItemRefresh.Id = 5;
			barButtonItemRefresh.Name = "barButtonItemRefresh";
			barButtonItemRefresh.ItemClick += barButtonItemRefresh_ItemClick;
			// 
			// FormDefaultChangeContainer
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(650, 226);
			Controls.Add(treeList);
			Controls.Add(barDockControlLeft);
			Controls.Add(barDockControlRight);
			Controls.Add(barDockControlBottom);
			Controls.Add(barDockControlTop);
			Name = "FormDefaultChangeContainer";
			StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "Default Change Container";
			TopMost = true;
			FormClosing += FormDefaultChangeContainer_FormClosing;
			FormClosed += FormDefaultChangeContainer_FormClosed;
			((System.ComponentModel.ISupportInitialize)treeList).EndInit();
			((System.ComponentModel.ISupportInitialize)popupMenu).EndInit();
			((System.ComponentModel.ISupportInitialize)barManager).EndInit();
			ResumeLayout(false);
			PerformLayout();
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
		private DevExpress.XtraBars.BarButtonItem barButtonItemRefresh;
	}
}