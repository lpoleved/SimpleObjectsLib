namespace Simple.Objects.Controls
{
	partial class RibbonModuleDevelopment
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
			this.ribbonPageTransactionMonitor = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			this.barButtonItemStartTransactionMonitor = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemStopTransactionMonitor = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemClearTransactionLog = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItemGenerateSimpleObjects = new DevExpress.XtraBars.BarButtonItem();
			this.ribbonPageGroupCodeGenerator = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			this.barCheckItemIncludeSystemObjects = new DevExpress.XtraBars.BarCheckItem();
			this.tabPageCodeGeneratorLog = new DevExpress.XtraTab.XtraTabPage();
			this.simpleObjectCodeGenerator = new Simple.Objects.Controls.ObjectCodeGeneratorControl();
			this.tabControl = new DevExpress.XtraTab.XtraTabControl();
			this.tabPageTransactionLog = new DevExpress.XtraTab.XtraTabPage();
			this.splitContainerControlTransactionLog = new DevExpress.XtraEditors.SplitContainerControl();
			this.treeListTransactionLog = new Simple.Controls.TreeList.SimpleTreeList();
			this.groupPropertyPanel = new Simple.Objects.Controls.GroupPropertyPanel();
			this.tabPageTestConsole = new DevExpress.XtraTab.XtraTabPage();
			this.memoEditTestConsole = new DevExpress.XtraEditors.MemoEdit();
			this.ribbonPageGroupChangeContainer = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			this.barCheckItemShowDefaultChangeContainer = new DevExpress.XtraBars.BarCheckItem();
			this.barCheckItemNoCommitChangeOnFocusedNodeChange = new DevExpress.XtraBars.BarCheckItem();
			this.barCheckItemNoCommitChangeOnDeleteRequest = new DevExpress.XtraBars.BarCheckItem();
			this.imageList = new System.Windows.Forms.ImageList();
			this.graphController = new Simple.Objects.Controls.GraphControllerDevExpress();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerMain.Panel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerMain.Panel2)).BeginInit();
			this.splitContainerMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tempRibbonControl)).BeginInit();
			this.tabPageCodeGeneratorLog.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
			this.tabControl.SuspendLayout();
			this.tabPageTransactionLog.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlTransactionLog)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlTransactionLog.Panel1)).BeginInit();
			this.splitContainerControlTransactionLog.Panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlTransactionLog.Panel2)).BeginInit();
			this.splitContainerControlTransactionLog.Panel2.SuspendLayout();
			this.splitContainerControlTransactionLog.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeListTransactionLog)).BeginInit();
			this.tabPageTestConsole.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditTestConsole.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainerMain
			// 
			this.splitContainerMain.Size = new System.Drawing.Size(1023, 719);
			this.splitContainerMain.SplitterPosition = 612;
			// 
			// tempRibbonControl
			// 
			this.tempRibbonControl.ExpandCollapseItem.Id = 0;
			this.tempRibbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemStartTransactionMonitor,
            this.barButtonItemStopTransactionMonitor,
            this.barButtonItemGenerateSimpleObjects,
            this.barCheckItemIncludeSystemObjects,
            this.barButtonItemClearTransactionLog,
            this.barCheckItemShowDefaultChangeContainer,
            this.barCheckItemNoCommitChangeOnFocusedNodeChange,
            this.barCheckItemNoCommitChangeOnDeleteRequest});
			this.tempRibbonControl.MaxItemId = 17;
			// 
			// 
			// 
			this.tempRibbonControl.SearchEditItem.AccessibleName = "Search Item";
			this.tempRibbonControl.SearchEditItem.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
			this.tempRibbonControl.SearchEditItem.EditWidth = 150;
			this.tempRibbonControl.SearchEditItem.Id = -5000;
			this.tempRibbonControl.SearchEditItem.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.tempRibbonControl.Size = new System.Drawing.Size(1023, 150);
			// 
			// ribbonPage
			// 
			this.ribbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageTransactionMonitor,
            this.ribbonPageGroupCodeGenerator,
            this.ribbonPageGroupChangeContainer});
			this.ribbonPage.Text = "Development";
			// 
			// ribbonPageTransactionMonitor
			// 
			this.ribbonPageTransactionMonitor.AllowTextClipping = false;
			this.ribbonPageTransactionMonitor.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
			this.ribbonPageTransactionMonitor.ItemLinks.Add(this.barButtonItemStartTransactionMonitor);
			this.ribbonPageTransactionMonitor.ItemLinks.Add(this.barButtonItemStopTransactionMonitor);
			this.ribbonPageTransactionMonitor.ItemLinks.Add(this.barButtonItemClearTransactionLog, true);
			this.ribbonPageTransactionMonitor.Name = "ribbonPageTransactionMonitor";
			this.ribbonPageTransactionMonitor.Text = "Transaction Monitor";
			// 
			// barButtonItemStartTransactionMonitor
			// 
			this.barButtonItemStartTransactionMonitor.Caption = "Start";
			this.barButtonItemStartTransactionMonitor.Id = 1;
			this.barButtonItemStartTransactionMonitor.ImageOptions.Image = global::Simple.Objects.Controls.Properties.Resources.Play_Green;
			this.barButtonItemStartTransactionMonitor.ImageOptions.LargeImage = global::Simple.Objects.Controls.Properties.Resources.Play_Green_Large;
			this.barButtonItemStartTransactionMonitor.Name = "barButtonItemStartTransactionMonitor";
			this.barButtonItemStartTransactionMonitor.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemStartTransactionMonitor_ItemClick);
			// 
			// barButtonItemStopTransactionMonitor
			// 
			this.barButtonItemStopTransactionMonitor.Caption = "Stop";
			this.barButtonItemStopTransactionMonitor.Id = 2;
			this.barButtonItemStopTransactionMonitor.ImageOptions.Image = global::Simple.Objects.Controls.Properties.Resources.Stop_Red;
			this.barButtonItemStopTransactionMonitor.ImageOptions.LargeImage = global::Simple.Objects.Controls.Properties.Resources.Stop_Red_Large;
			this.barButtonItemStopTransactionMonitor.Name = "barButtonItemStopTransactionMonitor";
			this.barButtonItemStopTransactionMonitor.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemStopTransactionMonitor_ItemClick);
			// 
			// barButtonItemClearTransactionLog
			// 
			this.barButtonItemClearTransactionLog.Caption = "Clear Log";
			this.barButtonItemClearTransactionLog.Id = 9;
			this.barButtonItemClearTransactionLog.ImageOptions.Image = global::Simple.Objects.Controls.Properties.Resources.Clear;
			this.barButtonItemClearTransactionLog.ImageOptions.LargeImage = global::Simple.Objects.Controls.Properties.Resources.Clear_Large;
			this.barButtonItemClearTransactionLog.Name = "barButtonItemClearTransactionLog";
			this.barButtonItemClearTransactionLog.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemClearTransactionLog_ItemClick);
			// 
			// barButtonItemGenerateSimpleObjects
			// 
			this.barButtonItemGenerateSimpleObjects.Caption = "Generate Object Code";
			this.barButtonItemGenerateSimpleObjects.Id = 3;
			this.barButtonItemGenerateSimpleObjects.ImageOptions.Image = global::Simple.Objects.Controls.Properties.Resources.TextCode;
			this.barButtonItemGenerateSimpleObjects.ImageOptions.LargeImage = global::Simple.Objects.Controls.Properties.Resources.TextCode_Large;
			this.barButtonItemGenerateSimpleObjects.Name = "barButtonItemGenerateSimpleObjects";
			this.barButtonItemGenerateSimpleObjects.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemGenerateSimpleObjects_ItemClick);
			// 
			// ribbonPageGroupCodeGenerator
			// 
			this.ribbonPageGroupCodeGenerator.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
			this.ribbonPageGroupCodeGenerator.ItemLinks.Add(this.barButtonItemGenerateSimpleObjects);
			this.ribbonPageGroupCodeGenerator.ItemLinks.Add(this.barCheckItemIncludeSystemObjects);
			this.ribbonPageGroupCodeGenerator.Name = "ribbonPageGroupCodeGenerator";
			this.ribbonPageGroupCodeGenerator.Text = "Code Generator";
			// 
			// barCheckItemIncludeSystemObjects
			// 
			this.barCheckItemIncludeSystemObjects.Caption = "Include System Objects";
			this.barCheckItemIncludeSystemObjects.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
			this.barCheckItemIncludeSystemObjects.Id = 4;
			this.barCheckItemIncludeSystemObjects.Name = "barCheckItemIncludeSystemObjects";
			this.barCheckItemIncludeSystemObjects.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItemIncludeSystemObjects_CheckedChanged);
			// 
			// tabPageCodeGeneratorLog
			// 
			this.tabPageCodeGeneratorLog.Controls.Add(this.simpleObjectCodeGenerator);
			this.tabPageCodeGeneratorLog.Name = "tabPageCodeGeneratorLog";
			this.tabPageCodeGeneratorLog.Size = new System.Drawing.Size(1021, 694);
			this.tabPageCodeGeneratorLog.Text = "Code Generator Log";
			// 
			// simpleObjectCodeGenerator
			// 
			this.simpleObjectCodeGenerator.Dock = System.Windows.Forms.DockStyle.Fill;
			this.simpleObjectCodeGenerator.IncludeSystemObjects = false;
			this.simpleObjectCodeGenerator.Location = new System.Drawing.Point(0, 0);
			this.simpleObjectCodeGenerator.Name = "simpleObjectCodeGenerator";
			this.simpleObjectCodeGenerator.Size = new System.Drawing.Size(1021, 694);
			this.simpleObjectCodeGenerator.TabIndex = 0;
			// 
			// tabControl
			// 
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedTabPage = this.tabPageTransactionLog;
			this.tabControl.Size = new System.Drawing.Size(1023, 719);
			this.tabControl.TabIndex = 1;
			this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageTransactionLog,
            this.tabPageCodeGeneratorLog,
            this.tabPageTestConsole});
			// 
			// tabPageTransactionLog
			// 
			this.tabPageTransactionLog.Controls.Add(this.splitContainerControlTransactionLog);
			this.tabPageTransactionLog.Name = "tabPageTransactionLog";
			this.tabPageTransactionLog.Size = new System.Drawing.Size(1018, 693);
			this.tabPageTransactionLog.Text = "Transaction Log";
			// 
			// splitContainerControlTransactionLog
			// 
			this.splitContainerControlTransactionLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControlTransactionLog.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControlTransactionLog.Name = "splitContainerControlTransactionLog";
			// 
			// splitContainerControlTransactionLog.Panel1
			// 
			this.splitContainerControlTransactionLog.Panel1.Controls.Add(this.treeListTransactionLog);
			this.splitContainerControlTransactionLog.Panel1.Text = "Panel1";
			// 
			// splitContainerControlTransactionLog.Panel2
			// 
			this.splitContainerControlTransactionLog.Panel2.Controls.Add(this.groupPropertyPanel);
			this.splitContainerControlTransactionLog.Panel2.Text = "Panel2";
			this.splitContainerControlTransactionLog.Size = new System.Drawing.Size(1018, 693);
			this.splitContainerControlTransactionLog.SplitterPosition = 664;
			this.splitContainerControlTransactionLog.TabIndex = 0;
			this.splitContainerControlTransactionLog.Text = "splitContainerControl1";
			this.splitContainerControlTransactionLog.Resize += new System.EventHandler(this.splitContainerControl_Resize);
			// 
			// treeListTransactionLog
			// 
			this.treeListTransactionLog.Appearance.FocusedCell.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.treeListTransactionLog.Appearance.FocusedCell.Options.UseBackColor = true;
			this.treeListTransactionLog.Appearance.FocusedCell.Options.UseForeColor = true;
			this.treeListTransactionLog.Appearance.FocusedRow.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.treeListTransactionLog.Appearance.FocusedRow.Options.UseForeColor = true;
			this.treeListTransactionLog.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.treeListTransactionLog.Appearance.HideSelectionRow.Options.UseForeColor = true;
			this.treeListTransactionLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeListTransactionLog.Location = new System.Drawing.Point(0, 0);
			this.treeListTransactionLog.Name = "treeListTransactionLog";
			this.treeListTransactionLog.OptionsMenu.ShowExpandCollapseItems = false;
			this.treeListTransactionLog.OptionsSimple.EditMode = Simple.Controls.TreeList.SimpleTreeEditMode.SelectEdit;
			this.treeListTransactionLog.OptionsSimple.LookAndFeelStyle = Simple.Controls.TreeList.SimpleTreeLookAndFeelStyle.ExplorerStyle;
			this.treeListTransactionLog.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
			this.treeListTransactionLog.OptionsView.ShowHorzLines = false;
			this.treeListTransactionLog.OptionsView.ShowIndicator = false;
			this.treeListTransactionLog.OptionsView.ShowVertLines = false;
			this.treeListTransactionLog.OptionsView.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None;
			this.treeListTransactionLog.Size = new System.Drawing.Size(664, 693);
			this.treeListTransactionLog.TabIndex = 0;
			// 
			// groupPropertyPanel
			// 
			this.groupPropertyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupPropertyPanel.Location = new System.Drawing.Point(0, 0);
			this.groupPropertyPanel.Name = "groupPropertyPanel";
			this.groupPropertyPanel.Size = new System.Drawing.Size(348, 693);
			this.groupPropertyPanel.TabIndex = 0;
			// 
			// tabPageTestConsole
			// 
			this.tabPageTestConsole.Controls.Add(this.memoEditTestConsole);
			this.tabPageTestConsole.Name = "tabPageTestConsole";
			this.tabPageTestConsole.Size = new System.Drawing.Size(1021, 694);
			this.tabPageTestConsole.Text = "Test Console";
			// 
			// memoEditTestConsole
			// 
			this.memoEditTestConsole.Dock = System.Windows.Forms.DockStyle.Fill;
			this.memoEditTestConsole.Location = new System.Drawing.Point(0, 0);
			this.memoEditTestConsole.Name = "memoEditTestConsole";
			this.memoEditTestConsole.Size = new System.Drawing.Size(1021, 694);
			this.memoEditTestConsole.TabIndex = 1;
			// 
			// ribbonPageGroupChangeContainer
			// 
			this.ribbonPageGroupChangeContainer.ItemLinks.Add(this.barCheckItemShowDefaultChangeContainer);
			this.ribbonPageGroupChangeContainer.ItemLinks.Add(this.barCheckItemNoCommitChangeOnFocusedNodeChange);
			this.ribbonPageGroupChangeContainer.ItemLinks.Add(this.barCheckItemNoCommitChangeOnDeleteRequest);
			this.ribbonPageGroupChangeContainer.Name = "ribbonPageGroupChangeContainer";
			this.ribbonPageGroupChangeContainer.Text = "Change Container";
			// 
			// barCheckItemShowDefaultChangeContainer
			// 
			this.barCheckItemShowDefaultChangeContainer.Caption = "Show Default Change Container";
			this.barCheckItemShowDefaultChangeContainer.Id = 13;
			this.barCheckItemShowDefaultChangeContainer.ImageOptions.Image = global::Simple.Objects.Controls.Properties.Resources.ChangeTo;
			this.barCheckItemShowDefaultChangeContainer.ImageOptions.LargeImage = global::Simple.Objects.Controls.Properties.Resources.ChangeTo_Large;
			this.barCheckItemShowDefaultChangeContainer.Name = "barCheckItemShowDefaultChangeContainer";
			this.barCheckItemShowDefaultChangeContainer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItemShowDefaultChangeContainer_ItemClick);
			// 
			// barCheckItemNoCommitChangeOnFocusedNodeChange
			// 
			this.barCheckItemNoCommitChangeOnFocusedNodeChange.Caption = "No Commit Change on Focused Node Change";
			this.barCheckItemNoCommitChangeOnFocusedNodeChange.Id = 15;
			this.barCheckItemNoCommitChangeOnFocusedNodeChange.ImageOptions.Image = global::Simple.Objects.Controls.Properties.Resources.Configure;
			this.barCheckItemNoCommitChangeOnFocusedNodeChange.ImageOptions.LargeImage = global::Simple.Objects.Controls.Properties.Resources.Configure_Large;
			this.barCheckItemNoCommitChangeOnFocusedNodeChange.Name = "barCheckItemNoCommitChangeOnFocusedNodeChange";
			this.barCheckItemNoCommitChangeOnFocusedNodeChange.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItemNoCommitChangeOnFocusedNodeChange_ItemClick);
			// 
			// barCheckItemNoCommitChangeOnDeleteRequest
			// 
			this.barCheckItemNoCommitChangeOnDeleteRequest.Caption = "No Commit Change on Delete Request";
			this.barCheckItemNoCommitChangeOnDeleteRequest.Id = 16;
			this.barCheckItemNoCommitChangeOnDeleteRequest.ImageOptions.Image = global::Simple.Objects.Controls.Properties.Resources.Configure;
			this.barCheckItemNoCommitChangeOnDeleteRequest.ImageOptions.LargeImage = global::Simple.Objects.Controls.Properties.Resources.Configure_Large;
			this.barCheckItemNoCommitChangeOnDeleteRequest.Name = "barCheckItemNoCommitChangeOnDeleteRequest";
			this.barCheckItemNoCommitChangeOnDeleteRequest.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItemNoCommitChangeOnDeleteRequest_ItemClick);
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// graphController
			// 
			this.graphController.IsActive = true;
			// 
			// RibbonModuleDevelopment
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl);
			this.GraphController = this.graphController;
			this.Name = "RibbonModuleDevelopment";
			this.Size = new System.Drawing.Size(1023, 719);
			this.Controls.SetChildIndex(this.splitContainerMain, 0);
			this.Controls.SetChildIndex(this.tabControl, 0);
			this.Controls.SetChildIndex(this.tempRibbonControl, 0);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerMain.Panel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerMain.Panel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
			this.splitContainerMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tempRibbonControl)).EndInit();
			this.tabPageCodeGeneratorLog.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
			this.tabControl.ResumeLayout(false);
			this.tabPageTransactionLog.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlTransactionLog.Panel1)).EndInit();
			this.splitContainerControlTransactionLog.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlTransactionLog.Panel2)).EndInit();
			this.splitContainerControlTransactionLog.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlTransactionLog)).EndInit();
			this.splitContainerControlTransactionLog.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.treeListTransactionLog)).EndInit();
			this.tabPageTestConsole.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.memoEditTestConsole.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		protected DevExpress.XtraBars.BarButtonItem barButtonItemStartTransactionMonitor;
		protected DevExpress.XtraBars.BarButtonItem barButtonItemStopTransactionMonitor;
		protected DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageTransactionMonitor;
		protected DevExpress.XtraBars.BarButtonItem barButtonItemGenerateSimpleObjects;
		protected DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupCodeGenerator;
		protected DevExpress.XtraBars.BarCheckItem barCheckItemIncludeSystemObjects;
		protected DevExpress.XtraBars.BarButtonItem barButtonItemClearTransactionLog;
		protected DevExpress.XtraTab.XtraTabControl tabControl;
		protected DevExpress.XtraTab.XtraTabPage tabPageTransactionLog;
		protected DevExpress.XtraEditors.SplitContainerControl splitContainerControlTransactionLog;
		protected DevExpress.XtraTab.XtraTabPage tabPageCodeGeneratorLog;
		protected ObjectCodeGeneratorControl simpleObjectCodeGenerator;
		protected DevExpress.XtraTab.XtraTabPage tabPageTestConsole;
		protected DevExpress.XtraEditors.MemoEdit memoEditTestConsole;
		protected DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupChangeContainer;
		protected DevExpress.XtraBars.BarCheckItem barCheckItemShowDefaultChangeContainer;
		protected DevExpress.XtraBars.BarCheckItem barCheckItemNoCommitChangeOnFocusedNodeChange;
		protected DevExpress.XtraBars.BarCheckItem barCheckItemNoCommitChangeOnDeleteRequest;
		protected System.Windows.Forms.ImageList imageList;
		protected GraphControllerDevExpress graphController;
		protected Simple.Controls.TreeList.SimpleTreeList treeListTransactionLog;
		protected GroupPropertyPanel groupPropertyPanel;
	}
}
