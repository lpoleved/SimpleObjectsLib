namespace Simple.Objects.Controls
{
	partial class GraphWithGroupPropertyPanelControl
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
				components.Dispose();

			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
			this.treeList = new Simple.Controls.TreeList.SimpleTreeList();
			this.groupPropertyPanel = new Simple.Objects.Controls.GroupPropertyPanel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
			this.splitContainerControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainerControl1
			// 
			this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControl1.Name = "splitContainerControl1";
			this.splitContainerControl1.Panel1.Controls.Add(this.treeList);
			this.splitContainerControl1.Panel1.Text = "Panel1";
			this.splitContainerControl1.Panel2.Controls.Add(this.groupPropertyPanel);
			this.splitContainerControl1.Panel2.Text = "Panel2";
			this.splitContainerControl1.Size = new System.Drawing.Size(417, 441);
			this.splitContainerControl1.SplitterPosition = 255;
			this.splitContainerControl1.TabIndex = 0;
			this.splitContainerControl1.Text = "splitContainerControl1";
			// 
			// treeList
			// 
			this.treeList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeList.Location = new System.Drawing.Point(0, 0);
			this.treeList.Name = "treeList";
			this.treeList.OptionsSimple.EditMode = Simple.Controls.TreeList.SimpleTreeEditMode.SelectEdit;
			this.treeList.OptionsSimple.LookAndFeelStyle = Simple.Controls.TreeList.SimpleTreeLookAndFeelStyle.ExplorerStyle;
			this.treeList.Size = new System.Drawing.Size(255, 441);
			this.treeList.TabIndex = 0;
			this.treeList.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None;
			// 
			// groupPropertyPanel
			// 
			this.groupPropertyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupPropertyPanel.Location = new System.Drawing.Point(0, 0);
			this.groupPropertyPanel.Name = "groupPropertyPanel";
			this.groupPropertyPanel.Size = new System.Drawing.Size(157, 441);
			this.groupPropertyPanel.TabIndex = 0;
			// 
			// GraphWithGroupPropertyPanelControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainerControl1);
			this.Name = "GraphWithGroupPropertyPanelControl";
			this.Size = new System.Drawing.Size(417, 441);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
			this.splitContainerControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
		private Simple.Controls.TreeList.SimpleTreeList treeList;
		private GroupPropertyPanel groupPropertyPanel;
	}
}
