namespace Simple.Objects.ServerMonitor
{
    partial class FormSettings
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
			this.tabPage = new DevExpress.XtraTab.XtraTabControl();
			this.tabPageGeneral = new DevExpress.XtraTab.XtraTabPage();
			this.groupControlRibbonStyling = new DevExpress.XtraEditors.GroupControl();
			this.comboBoxEditRibbonSkin = new DevExpress.XtraEditors.ComboBoxEdit();
			this.labelControlRibbonSkin = new DevExpress.XtraEditors.LabelControl();
			this.checkEditDarkMode = new DevExpress.XtraEditors.CheckEdit();
			this.comboBoxEditRibbonStyle = new DevExpress.XtraEditors.ComboBoxEdit();
			this.comboBoxEditRibbonColorSheme = new DevExpress.XtraEditors.ComboBoxEdit();
			this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.buttonClose = new DevExpress.XtraEditors.SimpleButton();
			this.buttonOK = new DevExpress.XtraEditors.SimpleButton();
			this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.tabPage)).BeginInit();
			this.tabPage.SuspendLayout();
			this.tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupControlRibbonStyling)).BeginInit();
			this.groupControlRibbonStyling.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditRibbonSkin.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditDarkMode.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditRibbonStyle.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditRibbonColorSheme.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// tabPage
			// 
			this.tabPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabPage.Location = new System.Drawing.Point(2, 2);
			this.tabPage.Name = "tabPage";
			this.tabPage.SelectedTabPage = this.tabPageGeneral;
			this.tabPage.Size = new System.Drawing.Size(419, 351);
			this.tabPage.TabIndex = 0;
			this.tabPage.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageGeneral});
			// 
			// tabPageGeneral
			// 
			this.tabPageGeneral.Controls.Add(this.groupControlRibbonStyling);
			this.tabPageGeneral.Name = "tabPageGeneral";
			this.tabPageGeneral.Size = new System.Drawing.Size(417, 326);
			this.tabPageGeneral.Text = "General";
			// 
			// groupControlRibbonStyling
			// 
			this.groupControlRibbonStyling.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupControlRibbonStyling.Controls.Add(this.comboBoxEditRibbonSkin);
			this.groupControlRibbonStyling.Controls.Add(this.labelControlRibbonSkin);
			this.groupControlRibbonStyling.Controls.Add(this.checkEditDarkMode);
			this.groupControlRibbonStyling.Controls.Add(this.comboBoxEditRibbonStyle);
			this.groupControlRibbonStyling.Controls.Add(this.comboBoxEditRibbonColorSheme);
			this.groupControlRibbonStyling.Controls.Add(this.labelControl4);
			this.groupControlRibbonStyling.Controls.Add(this.labelControl3);
			this.groupControlRibbonStyling.Location = new System.Drawing.Point(3, 14);
			this.groupControlRibbonStyling.Name = "groupControlRibbonStyling";
			this.groupControlRibbonStyling.Size = new System.Drawing.Size(411, 309);
			this.groupControlRibbonStyling.TabIndex = 56;
			this.groupControlRibbonStyling.Text = "Ribbon Styling";
			// 
			// comboBoxEditRibbonSkin
			// 
			this.comboBoxEditRibbonSkin.Location = new System.Drawing.Point(243, 38);
			this.comboBoxEditRibbonSkin.Name = "comboBoxEditRibbonSkin";
			this.comboBoxEditRibbonSkin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditRibbonSkin.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditRibbonSkin.Size = new System.Drawing.Size(158, 20);
			this.comboBoxEditRibbonSkin.TabIndex = 54;
			// 
			// labelControlRibbonSkin
			// 
			this.labelControlRibbonSkin.Location = new System.Drawing.Point(216, 41);
			this.labelControlRibbonSkin.Name = "labelControlRibbonSkin";
			this.labelControlRibbonSkin.Size = new System.Drawing.Size(23, 13);
			this.labelControlRibbonSkin.TabIndex = 53;
			this.labelControlRibbonSkin.Text = "Skin:";
			// 
			// checkEditDarkMode
			// 
			this.checkEditDarkMode.Location = new System.Drawing.Point(216, 63);
			this.checkEditDarkMode.Name = "checkEditDarkMode";
			this.checkEditDarkMode.Properties.AutoWidth = true;
			this.checkEditDarkMode.Properties.Caption = "Dark Mode";
			this.checkEditDarkMode.Size = new System.Drawing.Size(74, 20);
			this.checkEditDarkMode.TabIndex = 52;
			this.checkEditDarkMode.CheckedChanged += new System.EventHandler(this.checkEditDarkMode_CheckedChanged);
			// 
			// comboBoxEditRibbonStyle
			// 
			this.comboBoxEditRibbonStyle.Location = new System.Drawing.Point(86, 39);
			this.comboBoxEditRibbonStyle.Name = "comboBoxEditRibbonStyle";
			this.comboBoxEditRibbonStyle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditRibbonStyle.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditRibbonStyle.Size = new System.Drawing.Size(116, 20);
			this.comboBoxEditRibbonStyle.TabIndex = 32;
			this.comboBoxEditRibbonStyle.EditValueChanged += new System.EventHandler(this.comboBoxEditRibbonStyle_EditValueChanged);
			// 
			// comboBoxEditRibbonColorSheme
			// 
			this.comboBoxEditRibbonColorSheme.Location = new System.Drawing.Point(86, 63);
			this.comboBoxEditRibbonColorSheme.Name = "comboBoxEditRibbonColorSheme";
			this.comboBoxEditRibbonColorSheme.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditRibbonColorSheme.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditRibbonColorSheme.Size = new System.Drawing.Size(116, 20);
			this.comboBoxEditRibbonColorSheme.TabIndex = 31;
			this.comboBoxEditRibbonColorSheme.EditValueChanged += new System.EventHandler(this.comboBoxEditRibbonColorSheme_EditValueChanged);
			// 
			// labelControl4
			// 
			this.labelControl4.Location = new System.Drawing.Point(14, 66);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(64, 13);
			this.labelControl4.TabIndex = 30;
			this.labelControl4.Text = "Color Sheme:";
			// 
			// labelControl3
			// 
			this.labelControl3.Location = new System.Drawing.Point(14, 41);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(64, 13);
			this.labelControl3.TabIndex = 28;
			this.labelControl3.Text = "Ribbon Style:";
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(20, 36);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(64, 13);
			this.labelControl2.TabIndex = 28;
			this.labelControl2.Text = "Ribbon Style:";
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(316, 360);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(93, 26);
			this.buttonClose.TabIndex = 2;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(203, 360);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(93, 26);
			this.buttonOK.TabIndex = 1;
			this.buttonOK.Text = "OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// textEdit1
			// 
			this.textEdit1.Location = new System.Drawing.Point(85, 41);
			this.textEdit1.Name = "textEdit1";
			this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.textEdit1.Properties.Appearance.Options.UseFont = true;
			this.textEdit1.Properties.PasswordChar = '*';
			this.textEdit1.Size = new System.Drawing.Size(193, 20);
			this.textEdit1.TabIndex = 31;
			// 
			// FormSettings
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(423, 393);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.tabPage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("FormSettings.IconOptions.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSettings";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			((System.ComponentModel.ISupportInitialize)(this.tabPage)).EndInit();
			this.tabPage.ResumeLayout(false);
			this.tabPageGeneral.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.groupControlRibbonStyling)).EndInit();
			this.groupControlRibbonStyling.ResumeLayout(false);
			this.groupControlRibbonStyling.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditRibbonSkin.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditDarkMode.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditRibbonStyle.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditRibbonColorSheme.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraTab.XtraTabControl tabPage;
        protected DevExpress.XtraTab.XtraTabPage tabPageGeneral;
        private DevExpress.XtraEditors.SimpleButton buttonClose;
        private DevExpress.XtraEditors.SimpleButton buttonOK;
		protected DevExpress.XtraEditors.GroupControl groupControlRibbonStyling;
		protected DevExpress.XtraEditors.LabelControl labelControl2;
		protected DevExpress.XtraEditors.TextEdit textEdit1;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditRibbonColorSheme;
		protected DevExpress.XtraEditors.LabelControl labelControl4;
		protected DevExpress.XtraEditors.LabelControl labelControl3;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditRibbonStyle;
		protected DevExpress.XtraEditors.CheckEdit checkEditDarkMode;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditRibbonSkin;
		protected DevExpress.XtraEditors.LabelControl labelControlRibbonSkin;
	}
}