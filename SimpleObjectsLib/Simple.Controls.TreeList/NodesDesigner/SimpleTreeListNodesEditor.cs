//#if IncludeNodesEditorCode 

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.IO;
using System.Drawing;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Utils;
using Simple.Controls.TreeList;

namespace Simple.Controls.TreeList.Designer
{
	public class SimpleTreeListNodesEditor : Form 
    {
		private Panel tlPreview;
		private System.Windows.Forms.Label label1;
		private DevExpress.XtraEditors.SimpleButton btnAddRoot;
		private DevExpress.XtraEditors.SimpleButton btnAddChild;
		private DevExpress.XtraEditors.SimpleButton btnDelete;
		private DevExpress.XtraEditors.SimpleButton btnOK;
		private DevExpress.XtraEditors.SimpleButton btnCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lbImage;
		private System.Windows.Forms.Label lbSelected;
		private System.Windows.Forms.Label lbState;
		private DevExpress.XtraEditors.ImageComboBoxEdit piImage;
		private DevExpress.XtraEditors.ImageComboBoxEdit piSelected;
		private DevExpress.XtraEditors.ImageComboBoxEdit piState;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private DevExpress.XtraEditors.SpinEdit seImage;
		private DevExpress.XtraEditors.SpinEdit seSelected;
		private DevExpress.XtraEditors.SpinEdit seState;
		private DevExpress.XtraEditors.SimpleButton btnBestFit;
        private ImageList imageList1;
        private IContainer components;
        private DevExpress.XtraEditors.ImageComboBoxEdit piExpanded;
        private Label lbExpanded;
        private DevExpress.XtraEditors.SpinEdit seExpanded;
        private Label label6;
        private GroupBox groupBox3;
        private DevExpress.XtraEditors.CheckEdit ceUseExpandedImages;
        private DevExpress.XtraEditors.CheckEdit ceShowExtraChecked;
        private DevExpress.XtraEditors.CheckEdit ceCustomNodeImagesDraw;
        private DevExpress.XtraEditors.CheckEdit ceCustomCheckBoxDraw;
        private DevExpress.XtraEditors.CheckEdit ceExtraChecked;
		private SimpleTreeListNodes nodes;
        private SimpleTreeList treeListPreview;
        private bool init = false;

        public SimpleTreeListNodesEditor(object nodes) 
        {
			this.nodes = nodes as SimpleTreeListNodes;
			InitializeComponent();
			InitData();
			InitButtonImages();
		}

        public SimpleTreeList TreeList
        {
            get { return nodes.TreeList as Simple.Controls.TreeList.SimpleTreeList; }
        }

        private TreeListNode FocusedNode 
        { 
            get { return treeListPreview.FocusedNode; } 
        }

		private void InitializeComponent() 
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleTreeListNodesEditor));
            this.tlPreview = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddRoot = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddChild = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.piExpanded = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.lbExpanded = new System.Windows.Forms.Label();
            this.piState = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.lbState = new System.Windows.Forms.Label();
            this.piSelected = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.lbSelected = new System.Windows.Forms.Label();
            this.piImage = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.lbImage = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.seExpanded = new DevExpress.XtraEditors.SpinEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.seState = new DevExpress.XtraEditors.SpinEdit();
            this.seSelected = new DevExpress.XtraEditors.SpinEdit();
            this.seImage = new DevExpress.XtraEditors.SpinEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBestFit = new DevExpress.XtraEditors.SimpleButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ceUseExpandedImages = new DevExpress.XtraEditors.CheckEdit();
            this.ceShowExtraChecked = new DevExpress.XtraEditors.CheckEdit();
            this.ceCustomNodeImagesDraw = new DevExpress.XtraEditors.CheckEdit();
            this.ceCustomCheckBoxDraw = new DevExpress.XtraEditors.CheckEdit();
            this.ceExtraChecked = new DevExpress.XtraEditors.CheckEdit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.piExpanded.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.piState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.piSelected.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.piImage.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seExpanded.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seSelected.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seImage.Properties)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceUseExpandedImages.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceShowExtraChecked.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceCustomNodeImagesDraw.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceCustomCheckBoxDraw.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceExtraChecked.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tlPreview
            // 
            this.tlPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tlPreview.Location = new System.Drawing.Point(8, 32);
            this.tlPreview.Name = "tlPreview";
            this.tlPreview.Size = new System.Drawing.Size(439, 154);
            this.tlPreview.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select node to edit:";
            // 
            // btnAddRoot
            // 
            this.btnAddRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddRoot.ImageIndex = 0;
            this.btnAddRoot.Location = new System.Drawing.Point(11, 192);
            this.btnAddRoot.Name = "btnAddRoot";
            this.btnAddRoot.Size = new System.Drawing.Size(108, 24);
            this.btnAddRoot.TabIndex = 1;
            this.btnAddRoot.Text = "Add Root";
            this.btnAddRoot.Click += new System.EventHandler(this.btnAddRoot_Click);
            // 
            // btnAddChild
            // 
            this.btnAddChild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddChild.ImageIndex = 1;
            this.btnAddChild.Location = new System.Drawing.Point(124, 192);
            this.btnAddChild.Name = "btnAddChild";
            this.btnAddChild.Size = new System.Drawing.Size(108, 24);
            this.btnAddChild.TabIndex = 2;
            this.btnAddChild.Text = "Add Child";
            this.btnAddChild.Click += new System.EventHandler(this.btnAddChild_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.ImageIndex = 2;
            this.btnDelete.Location = new System.Drawing.Point(238, 192);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(108, 24);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(267, 454);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(84, 24);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(355, 454);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 24);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.piExpanded);
            this.groupBox1.Controls.Add(this.lbExpanded);
            this.groupBox1.Controls.Add(this.piState);
            this.groupBox1.Controls.Add(this.lbState);
            this.groupBox1.Controls.Add(this.piSelected);
            this.groupBox1.Controls.Add(this.lbSelected);
            this.groupBox1.Controls.Add(this.piImage);
            this.groupBox1.Controls.Add(this.lbImage);
            this.groupBox1.Location = new System.Drawing.Point(8, 310);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(439, 64);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Image Indexes";
            // 
            // piExpanded
            // 
            this.piExpanded.Location = new System.Drawing.Point(334, 32);
            this.piExpanded.Name = "piExpanded";
            this.piExpanded.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.piExpanded.Size = new System.Drawing.Size(96, 20);
            this.piExpanded.TabIndex = 8;
            this.piExpanded.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.piExpanded_EditValueChanging);
            // 
            // lbExpanded
            // 
            this.lbExpanded.Location = new System.Drawing.Point(334, 16);
            this.lbExpanded.Name = "lbExpanded";
            this.lbExpanded.Size = new System.Drawing.Size(96, 16);
            this.lbExpanded.TabIndex = 7;
            this.lbExpanded.Text = "Expanded Image:";
            // 
            // piState
            // 
            this.piState.Location = new System.Drawing.Point(224, 32);
            this.piState.Name = "piState";
            this.piState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.piState.Size = new System.Drawing.Size(96, 20);
            this.piState.TabIndex = 6;
            this.piState.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.piState_ValueChanging);
            // 
            // lbState
            // 
            this.lbState.Location = new System.Drawing.Point(224, 16);
            this.lbState.Name = "lbState";
            this.lbState.Size = new System.Drawing.Size(92, 16);
            this.lbState.TabIndex = 4;
            this.lbState.Text = "State Image:";
            // 
            // piSelected
            // 
            this.piSelected.Location = new System.Drawing.Point(116, 32);
            this.piSelected.Name = "piSelected";
            this.piSelected.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.piSelected.Size = new System.Drawing.Size(96, 20);
            this.piSelected.TabIndex = 5;
            this.piSelected.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.piSelected_ValueChanging);
            // 
            // lbSelected
            // 
            this.lbSelected.Location = new System.Drawing.Point(116, 16);
            this.lbSelected.Name = "lbSelected";
            this.lbSelected.Size = new System.Drawing.Size(96, 16);
            this.lbSelected.TabIndex = 2;
            this.lbSelected.Text = "Selected Image:";
            // 
            // piImage
            // 
            this.piImage.Location = new System.Drawing.Point(8, 32);
            this.piImage.Name = "piImage";
            this.piImage.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.piImage.Size = new System.Drawing.Size(96, 20);
            this.piImage.TabIndex = 4;
            this.piImage.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.piImage_ValueChanging);
            // 
            // lbImage
            // 
            this.lbImage.Location = new System.Drawing.Point(8, 16);
            this.lbImage.Name = "lbImage";
            this.lbImage.Size = new System.Drawing.Size(96, 16);
            this.lbImage.TabIndex = 0;
            this.lbImage.Text = "Image:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.seExpanded);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.seState);
            this.groupBox2.Controls.Add(this.seSelected);
            this.groupBox2.Controls.Add(this.seImage);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(8, 382);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(439, 64);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Default Image Indexes";
            // 
            // seExpanded
            // 
            this.seExpanded.EditValue = new decimal(new int[] { 0, 0, 0, 0});
            this.seExpanded.Location = new System.Drawing.Point(334, 32);
            this.seExpanded.Name = "seExpanded";
            this.seExpanded.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seExpanded.Properties.IsFloatValue = false;
            this.seExpanded.Properties.Mask.EditMask = "N00";
            this.seExpanded.Properties.MaxValue = new decimal(new int[] { 100, 0, 0, 0});
            this.seExpanded.Properties.MinValue = new decimal(new int[] { 1, 0, 0, -2147483648});
            this.seExpanded.Size = new System.Drawing.Size(96, 20);
            this.seExpanded.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(334, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "Expanded Image:";
            // 
            // seState
            // 
            this.seState.EditValue = new decimal(new int[] { 1, 0, 0, -2147483648});
            this.seState.Location = new System.Drawing.Point(224, 32);
            this.seState.Name = "seState";
            this.seState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seState.Properties.IsFloatValue = false;
            this.seState.Properties.Mask.EditMask = "N00";
            this.seState.Properties.MaxValue = new decimal(new int[] { 100, 0, 0, 0});
            this.seState.Properties.MinValue = new decimal(new int[] { 1, 0, 0, -2147483648});
            this.seState.Size = new System.Drawing.Size(96, 20);
            this.seState.TabIndex = 7;
            // 
            // seSelected
            // 
            this.seSelected.EditValue = new decimal(new int[] { 0, 0, 0, 0});
            this.seSelected.Location = new System.Drawing.Point(116, 32);
            this.seSelected.Name = "seSelected";
            this.seSelected.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seSelected.Properties.IsFloatValue = false;
            this.seSelected.Properties.Mask.EditMask = "N00";
            this.seSelected.Properties.MaxValue = new decimal(new int[] { 100, 0, 0, 0});
            this.seSelected.Properties.MinValue = new decimal(new int[] { 1, 0, 0, -2147483648});
            this.seSelected.Size = new System.Drawing.Size(96, 20);
            this.seSelected.TabIndex = 6;
            // 
            // seImage
            // 
            this.seImage.EditValue = new decimal(new int[] { 0, 0, 0, 0});
            this.seImage.Location = new System.Drawing.Point(8, 32);
            this.seImage.Name = "seImage";
            this.seImage.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seImage.Properties.IsFloatValue = false;
            this.seImage.Properties.Mask.EditMask = "N00";
            this.seImage.Properties.MaxValue = new decimal(new int[] { 100, 0, 0, 0});
            this.seImage.Properties.MinValue = new decimal(new int[] { 1, 0, 0, -2147483648});
            this.seImage.Size = new System.Drawing.Size(96, 20);
            this.seImage.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(224, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "State Image:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(116, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Selected Image:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Image:";
            // 
            // btnBestFit
            // 
            this.btnBestFit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBestFit.ImageIndex = 3;
            this.btnBestFit.Location = new System.Drawing.Point(303, 4);
            this.btnBestFit.Name = "btnBestFit";
            this.btnBestFit.Size = new System.Drawing.Size(144, 24);
            this.btnBestFit.TabIndex = 12;
            this.btnBestFit.Text = "Best Fit (all columns)";
            this.btnBestFit.Click += new System.EventHandler(this.btnBestFit_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "CheckBox.Checked_Checked.ico");
            this.imageList1.Images.SetKeyName(1, "CheckBox.Checked_Unchecked.ico");
            this.imageList1.Images.SetKeyName(2, "CheckBox.Unchecked_Checked.ico");
            this.imageList1.Images.SetKeyName(3, "CheckBox.Unchecked_Unchecked.ico");
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.ceExtraChecked);
            this.groupBox3.Controls.Add(this.ceUseExpandedImages);
            this.groupBox3.Controls.Add(this.ceShowExtraChecked);
            this.groupBox3.Controls.Add(this.ceCustomNodeImagesDraw);
            this.groupBox3.Controls.Add(this.ceCustomCheckBoxDraw);
            this.groupBox3.Location = new System.Drawing.Point(8, 233);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(439, 71);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Custom Draw Settings";
            // 
            // ceUseExpandedImages
            // 
            this.ceUseExpandedImages.Location = new System.Drawing.Point(180, 44);
            this.ceUseExpandedImages.Name = "ceUseExpandedImages";
            this.ceUseExpandedImages.Properties.Caption = "UseExpandedImages";
            this.ceUseExpandedImages.Size = new System.Drawing.Size(127, 19);
            this.ceUseExpandedImages.TabIndex = 3;
            this.ceUseExpandedImages.CheckedChanged += new System.EventHandler(this.ceUseExpandedImages_CheckedChanged);
            // 
            // ceShowExtraChecked
            // 
            this.ceShowExtraChecked.Location = new System.Drawing.Point(180, 19);
            this.ceShowExtraChecked.Name = "ceShowExtraChecked";
            this.ceShowExtraChecked.Properties.Caption = "ShowExtraChecked";
            this.ceShowExtraChecked.Size = new System.Drawing.Size(127, 19);
            this.ceShowExtraChecked.TabIndex = 2;
            //this.ceShowExtraChecked.CheckedChanged += new System.EventHandler(this.ceShowExtraChecked_CheckedChanged);
            // 
            // ceCustomNodeImagesDraw
            // 
            this.ceCustomNodeImagesDraw.Location = new System.Drawing.Point(11, 45);
            this.ceCustomNodeImagesDraw.Name = "ceCustomNodeImagesDraw";
            this.ceCustomNodeImagesDraw.Properties.Caption = "CustomNodeImagesDraw";
            this.ceCustomNodeImagesDraw.Size = new System.Drawing.Size(150, 19);
            this.ceCustomNodeImagesDraw.TabIndex = 1;
            this.ceCustomNodeImagesDraw.CheckedChanged += new System.EventHandler(this.ceCustomNodeImagesDraw_CheckedChanged);
            // 
            // ceCustomCheckBoxDraw
            // 
            this.ceCustomCheckBoxDraw.Location = new System.Drawing.Point(11, 20);
            this.ceCustomCheckBoxDraw.Name = "ceCustomCheckBoxDraw";
            this.ceCustomCheckBoxDraw.Properties.Caption = "CustomCheckBoxDraw";
            this.ceCustomCheckBoxDraw.Size = new System.Drawing.Size(150, 19);
            this.ceCustomCheckBoxDraw.TabIndex = 0;
            this.ceCustomCheckBoxDraw.CheckedChanged += new System.EventHandler(this.ceCustomCheckBoxDraw_CheckedChanged);
            // 
            // ceExtraChecked
            // 
            this.ceExtraChecked.Location = new System.Drawing.Point(331, 19);
            this.ceExtraChecked.Name = "ceExtraChecked";
            this.ceExtraChecked.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.ceExtraChecked.Properties.Appearance.Options.UseForeColor = true;
            this.ceExtraChecked.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.ceExtraChecked.Properties.Caption = "ExtraChecked";
            this.ceExtraChecked.Size = new System.Drawing.Size(99, 21);
            this.ceExtraChecked.TabIndex = 4;
            //this.ceExtraChecked.CheckedChanged += new System.EventHandler(this.ceExtraChecked_CheckedChanged);
            // 
            // xNodesEditor
            // 
            this.ClientSize = new System.Drawing.Size(455, 483);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnBestFit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAddChild);
            this.Controls.Add(this.btnAddRoot);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tlPreview);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(348, 329);
            this.Name = "xNodesEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nodes Editor";
            this.Load += new System.EventHandler(this.xNodesEditor_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.piExpanded.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.piState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.piSelected.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.piImage.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.seExpanded.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seSelected.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seImage.Properties)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceUseExpandedImages.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceShowExtraChecked.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceCustomNodeImagesDraw.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceCustomCheckBoxDraw.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceExtraChecked.Properties)).EndInit();
            this.ResumeLayout(false);
		}

        private void InitButtonImages()
        {
            //ImageCollection iml  = DevExpress.Utils.Controls.ImageHelper.CreateImageCollectionFromResources("Simple.Controls.TreeList.NodesDesigner.Images.bmp", typeof(xNodesEditor).Assembly, new Size(16, 16));
            ImageList iml = imageList1;
            btnAddRoot.Image = iml.Images[0];
			btnAddChild.Image = iml.Images[1];
			btnDelete.Image = iml.Images[2];
			btnBestFit.Image = iml.Images[3];
		}

		private void NodeChanged() 
        {
			bool b = FocusedNode != null;
			btnAddChild.Enabled = btnDelete.Enabled = b;
			piImage.Enabled = lbImage.Enabled = b && treeListPreview.SelectImageList != null;
			piSelected.Enabled = lbSelected.Enabled = b && treeListPreview.SelectImageList != null;
			piState.Enabled = lbState.Enabled = b && treeListPreview.StateImageList != null;
            piExpanded.Enabled = lbExpanded.Enabled = b && treeListPreview.StateImageList != null;
            ceExtraChecked.Enabled = b;

            if (b)
            {
                piImage.EditValue = FocusedNode.ImageIndex;
                piSelected.EditValue = FocusedNode.SelectImageIndex;
                piState.EditValue = FocusedNode.StateImageIndex;
                piExpanded.EditValue = (FocusedNode as SimpleTreeListNode).ExpandedImageIndex;
                //ceExtraChecked.Checked = (FocusedNode as SimpleTreeListNode).ExtraChecked;
            }
            else
            {
                piImage.EditValue = piSelected.EditValue = piState.EditValue = piExpanded.EditValue = -1;
            }
		}

		private void treeListPreview_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e) 
        {
			NodeChanged();			
		}

		private void SetNodeImageIndexes(SimpleTreeListNode node) 
        {
			try 
            {
				node.ImageIndex = Convert.ToInt32(seImage.EditValue);
				node.SelectImageIndex = Convert.ToInt32(seSelected.EditValue);
				node.StateImageIndex = Convert.ToInt32(seState.EditValue);
                node.ExpandedImageIndex = Convert.ToInt32(seExpanded.EditValue);
			} 
            catch {}
		}
		
        private void btnAddRoot_Click(object sender, System.EventArgs e) 
        {
			treeListPreview.FocusedNode = treeListPreview.AppendNode(null, null);
			SetNodeImageIndexes(treeListPreview.FocusedNode as SimpleTreeListNode);
		}

		private void btnAddChild_Click(object sender, System.EventArgs e) 
        {
			treeListPreview.FocusedNode = treeListPreview.AppendNode(null, FocusedNode);
            SetNodeImageIndexes(treeListPreview.FocusedNode as SimpleTreeListNode);
		}

		private void btnDelete_Click(object sender, System.EventArgs e) 
        {
			treeListPreview.DeleteNode(FocusedNode);
		}

		private void btnOK_Click(object sender, System.EventArgs e) 
        {
			SetData();
		}
		
		private static object[] GetNodeData(SimpleTreeListNode node, int dim) 
        {
			object[] ret = new object[dim];
			for(int i = 0; i < dim; i++)
				ret.SetValue(node.GetValue(i), i);
			return ret;
		}

        private static SimpleTreeListNode CreateNewNode(SimpleTreeListNode node, SimpleTreeList tl, SimpleTreeListNode parentNode)
        {
			SimpleTreeListNode newNode = tl.AppendNode(GetNodeData(node, tl.Columns.Count), parentNode) as SimpleTreeListNode; 
			newNode.ImageIndex = node.ImageIndex;
			newNode.SelectImageIndex = node.SelectImageIndex;
			newNode.StateImageIndex = node.StateImageIndex;
			newNode.CheckState = node.CheckState;
            //newNode.ExtraChecked = node.ExtraChecked;
            newNode.ExpandedImageIndex = node.ExpandedImageIndex;
			return newNode;
		}

		internal static void AddNodes(SimpleTreeListNodes fromNodes, SimpleTreeListNodes toNodes, SimpleTreeListNode parent) 
        {
			SimpleTreeListNode newNode = null;
			foreach(SimpleTreeListNode node in fromNodes) {
				newNode = CreateNewNode(node, toNodes.TreeList as SimpleTreeList, parent);
                AddNodes(node.Nodes, newNode.Nodes, newNode);
			}
		}

		private void InitData() 
        {
			if(TreeList == null) return;
			SetView();
			SetExView(); 
			treeListPreview.BeginUnboundLoad();
			AddNodes(TreeList.Nodes, treeListPreview.Nodes, null);
			treeListPreview.EndUnboundLoad();
			treeListPreview.OptionsView.AutoWidth = false;
			treeListPreview.ExpandAll();
			InitImages();
			NodeChanged();

            ceCustomCheckBoxDraw.Checked = treeListPreview.CustomCheckBoxDraw;
            ceCustomNodeImagesDraw.Checked = treeListPreview.CustomNodeImagesDraw;
            //ceShowExtraChecked.Checked = treeListPreview.ShowExtraChecked;
            ceUseExpandedImages.Checked = treeListPreview.UseExpandedImages;   
		}

		private void AddPickImageItems(DevExpress.XtraEditors.Controls.ImageComboBoxItemCollection items, int count) 
        {
			for(int i = -1; i < count; i++) {
				items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(i == -1 ? "(none)" : i.ToString() + " image", i, i));
			}
		}

		private void InitImages() {
			piImage.Properties.SmallImages = treeListPreview.SelectImageList;
			piSelected.Properties.SmallImages = treeListPreview.SelectImageList;
			piState.Properties.SmallImages = treeListPreview.StateImageList;
            piExpanded.Properties.SmallImages = treeListPreview.SelectImageList;
			int n = 0;
			if(treeListPreview.SelectImageList != null) n = ImageCollection.GetImageListImageCount(treeListPreview.SelectImageList);
			AddPickImageItems(piImage.Properties.Items, n);
			AddPickImageItems(piSelected.Properties.Items, n);
            AddPickImageItems(piExpanded.Properties.Items, n);
			n = 0; 
			if(treeListPreview.StateImageList != null) n = ImageCollection.GetImageListImageCount(treeListPreview.StateImageList);
			AddPickImageItems(piState.Properties.Items, n);
			init = true;
			piImage.EditValue = piSelected.EditValue = piState.EditValue = piExpanded.EditValue = -2;
			init = false;
			int maxH = 25;
			if(piImage.Height > maxH) {
				piImage.Properties.AutoHeight = piSelected.Properties.AutoHeight = piExpanded.Properties.AutoHeight = false;
				piImage.Height = piSelected.Height = piExpanded.Height = maxH;
			}
			if(piState.Height > maxH) {
				piState.Properties.AutoHeight = false;
				piState.Height = maxH;
			}
		}
        
        private void SetView() 
        {
			treeListPreview = SimpleTreeListAssign.CreateTreeListControlAssign(TreeList);
			treeListPreview.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListPreview_FocusedNodeChanged);
			tlPreview.Controls.Add(treeListPreview);
		}
		
        private void SetExView() 
        {
			foreach(DevExpress.XtraTreeList.Columns.TreeListColumn col in treeListPreview.Columns)
				col.OptionsColumn.ReadOnly = false;
			treeListPreview.OptionsView.ShowColumns = true;
			treeListPreview.OptionsBehavior.Editable = true;
		}
		
        private void SetData() 
        {
			if(TreeList == null) return;
			TreeList.ClearNodes();
			TreeList.BeginUnboundLoad();
			AddNodes(treeListPreview.Nodes, TreeList.Nodes, null); 
			TreeList.EndUnboundLoad();

            TreeList.CustomCheckBoxDraw = treeListPreview.CustomCheckBoxDraw;
            TreeList.CustomNodeImagesDraw = treeListPreview.CustomNodeImagesDraw;
            //TreeList.ShowExtraChecked = treeListPreview.ShowExtraChecked;
            TreeList.UseExpandedImages = treeListPreview.UseExpandedImages;

			TreeList.FireChanged();
		}
		
        private void piImage_ValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e) 
        {
			if(FocusedNode != null && !init && e.NewValue is int)
				FocusedNode.ImageIndex = (int)e.NewValue;
		}
		
        private void piSelected_ValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e) 
        {
			if(FocusedNode != null  && !init && e.NewValue is int)
				FocusedNode.SelectImageIndex = (int)e.NewValue;
		}
		
        private void piState_ValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e) 
        {
			if(FocusedNode != null  && !init && e.NewValue is int)
				FocusedNode.StateImageIndex = (int)e.NewValue;
		}
        
        private void piExpanded_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (FocusedNode != null && !init && e.NewValue is int)
            {
                (FocusedNode as SimpleTreeListNode).ExpandedImageIndex = (int)e.NewValue;
            }
        }

        //private void ceExtraChecked_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (FocusedNode != null && !init)
        //    {
        //        (FocusedNode as SimpleTreeListNode).ExtraChecked = ceExtraChecked.Checked;
        //    }
        //}

		private void btnBestFit_Click(object sender, System.EventArgs e) {
			treeListPreview.BestFitColumns(true);
		}
		
        private void xNodesEditor_Load(object sender, System.EventArgs e) {
			treeListPreview.BestFitColumns();
			int width = 0;
			foreach(DevExpress.XtraTreeList.Columns.TreeListColumn column in treeListPreview.Columns)
				if(column.VisibleIndex >=0) width += column.Width;
			treeListPreview.BestFitColumns(width < treeListPreview.Width);
		}

        private void ceCustomCheckBoxDraw_CheckedChanged(object sender, EventArgs e)
        {
            treeListPreview.CustomCheckBoxDraw = ceCustomCheckBoxDraw.Checked;
        }

        private void ceCustomNodeImagesDraw_CheckedChanged(object sender, EventArgs e)
        {
            treeListPreview.CustomNodeImagesDraw = ceCustomNodeImagesDraw.Checked;
        }

        //private void ceShowExtraChecked_CheckedChanged(object sender, EventArgs e)
        //{
        //    treeListPreview.ShowExtraChecked = ceShowExtraChecked.Checked;
        //}

        private void ceUseExpandedImages_CheckedChanged(object sender, EventArgs e)
        {
            treeListPreview.UseExpandedImages = ceUseExpandedImages.Checked;
        }
	}
}
//#endif