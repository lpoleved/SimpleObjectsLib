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
			tabPage = new DevExpress.XtraTab.XtraTabControl();
			tabPageGeneral = new DevExpress.XtraTab.XtraTabPage();
			groupControlRibbonStyling = new DevExpress.XtraEditors.GroupControl();
			comboBoxEditRibbonSkin = new DevExpress.XtraEditors.ComboBoxEdit();
			labelControlRibbonSkin = new DevExpress.XtraEditors.LabelControl();
			checkEditDarkMode = new DevExpress.XtraEditors.CheckEdit();
			comboBoxEditRibbonStyle = new DevExpress.XtraEditors.ComboBoxEdit();
			comboBoxEditRibbonColorSheme = new DevExpress.XtraEditors.ComboBoxEdit();
			labelControl4 = new DevExpress.XtraEditors.LabelControl();
			labelControl3 = new DevExpress.XtraEditors.LabelControl();
			labelControl2 = new DevExpress.XtraEditors.LabelControl();
			buttonClose = new DevExpress.XtraEditors.SimpleButton();
			buttonOK = new DevExpress.XtraEditors.SimpleButton();
			textEdit1 = new DevExpress.XtraEditors.TextEdit();
			checkEditCompactView = new DevExpress.XtraEditors.CheckEdit();
			((System.ComponentModel.ISupportInitialize)tabPage).BeginInit();
			tabPage.SuspendLayout();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)groupControlRibbonStyling).BeginInit();
			groupControlRibbonStyling.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEditRibbonSkin.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)checkEditDarkMode.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEditRibbonStyle.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEditRibbonColorSheme.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)textEdit1.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)checkEditCompactView.Properties).BeginInit();
			SuspendLayout();
			// 
			// tabPage
			// 
			tabPage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tabPage.Location = new System.Drawing.Point(2, 2);
			tabPage.Name = "tabPage";
			tabPage.SelectedTabPage = tabPageGeneral;
			tabPage.Size = new System.Drawing.Size(419, 351);
			tabPage.TabIndex = 0;
			tabPage.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { tabPageGeneral });
			// 
			// tabPageGeneral
			// 
			tabPageGeneral.Controls.Add(groupControlRibbonStyling);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(417, 326);
			tabPageGeneral.Text = "General";
			// 
			// groupControlRibbonStyling
			// 
			groupControlRibbonStyling.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			groupControlRibbonStyling.Controls.Add(checkEditCompactView);
			groupControlRibbonStyling.Controls.Add(comboBoxEditRibbonSkin);
			groupControlRibbonStyling.Controls.Add(labelControlRibbonSkin);
			groupControlRibbonStyling.Controls.Add(checkEditDarkMode);
			groupControlRibbonStyling.Controls.Add(comboBoxEditRibbonStyle);
			groupControlRibbonStyling.Controls.Add(comboBoxEditRibbonColorSheme);
			groupControlRibbonStyling.Controls.Add(labelControl4);
			groupControlRibbonStyling.Controls.Add(labelControl3);
			groupControlRibbonStyling.Location = new System.Drawing.Point(3, 14);
			groupControlRibbonStyling.Name = "groupControlRibbonStyling";
			groupControlRibbonStyling.Size = new System.Drawing.Size(411, 309);
			groupControlRibbonStyling.TabIndex = 56;
			groupControlRibbonStyling.Text = "Ribbon Styling";
			// 
			// comboBoxEditRibbonSkin
			// 
			comboBoxEditRibbonSkin.Location = new System.Drawing.Point(243, 38);
			comboBoxEditRibbonSkin.Name = "comboBoxEditRibbonSkin";
			comboBoxEditRibbonSkin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
			comboBoxEditRibbonSkin.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			comboBoxEditRibbonSkin.Size = new System.Drawing.Size(158, 20);
			comboBoxEditRibbonSkin.TabIndex = 54;
			comboBoxEditRibbonSkin.SelectedIndexChanged += comboBoxEditRibbonSkin_SelectedIndexChanged;
			// 
			// labelControlRibbonSkin
			// 
			labelControlRibbonSkin.Location = new System.Drawing.Point(216, 41);
			labelControlRibbonSkin.Name = "labelControlRibbonSkin";
			labelControlRibbonSkin.Size = new System.Drawing.Size(23, 13);
			labelControlRibbonSkin.TabIndex = 53;
			labelControlRibbonSkin.Text = "Skin:";
			// 
			// checkEditDarkMode
			// 
			checkEditDarkMode.Location = new System.Drawing.Point(216, 63);
			checkEditDarkMode.Name = "checkEditDarkMode";
			checkEditDarkMode.Properties.AutoWidth = true;
			checkEditDarkMode.Properties.Caption = "Dark Mode";
			checkEditDarkMode.Size = new System.Drawing.Size(74, 20);
			checkEditDarkMode.TabIndex = 52;
			checkEditDarkMode.CheckedChanged += checkEditDarkMode_CheckedChanged;
			// 
			// comboBoxEditRibbonStyle
			// 
			comboBoxEditRibbonStyle.Location = new System.Drawing.Point(86, 39);
			comboBoxEditRibbonStyle.Name = "comboBoxEditRibbonStyle";
			comboBoxEditRibbonStyle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
			comboBoxEditRibbonStyle.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			comboBoxEditRibbonStyle.Size = new System.Drawing.Size(116, 20);
			comboBoxEditRibbonStyle.TabIndex = 32;
			comboBoxEditRibbonStyle.EditValueChanged += comboBoxEditRibbonStyle_EditValueChanged;
			// 
			// comboBoxEditRibbonColorSheme
			// 
			comboBoxEditRibbonColorSheme.Location = new System.Drawing.Point(86, 63);
			comboBoxEditRibbonColorSheme.Name = "comboBoxEditRibbonColorSheme";
			comboBoxEditRibbonColorSheme.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
			comboBoxEditRibbonColorSheme.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			comboBoxEditRibbonColorSheme.Size = new System.Drawing.Size(116, 20);
			comboBoxEditRibbonColorSheme.TabIndex = 31;
			comboBoxEditRibbonColorSheme.EditValueChanged += comboBoxEditRibbonColorSheme_EditValueChanged;
			// 
			// labelControl4
			// 
			labelControl4.Location = new System.Drawing.Point(14, 66);
			labelControl4.Name = "labelControl4";
			labelControl4.Size = new System.Drawing.Size(64, 13);
			labelControl4.TabIndex = 30;
			labelControl4.Text = "Color Sheme:";
			// 
			// labelControl3
			// 
			labelControl3.Location = new System.Drawing.Point(14, 41);
			labelControl3.Name = "labelControl3";
			labelControl3.Size = new System.Drawing.Size(64, 13);
			labelControl3.TabIndex = 28;
			labelControl3.Text = "Ribbon Style:";
			// 
			// labelControl2
			// 
			labelControl2.Location = new System.Drawing.Point(20, 36);
			labelControl2.Name = "labelControl2";
			labelControl2.Size = new System.Drawing.Size(64, 13);
			labelControl2.TabIndex = 28;
			labelControl2.Text = "Ribbon Style:";
			// 
			// buttonClose
			// 
			buttonClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(316, 360);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(93, 26);
			buttonClose.TabIndex = 2;
			buttonClose.Text = "Close";
			buttonClose.Click += buttonClose_Click;
			// 
			// buttonOK
			// 
			buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			buttonOK.Location = new System.Drawing.Point(203, 360);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(93, 26);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "OK";
			buttonOK.Click += buttonOK_Click;
			// 
			// textEdit1
			// 
			textEdit1.Location = new System.Drawing.Point(85, 41);
			textEdit1.Name = "textEdit1";
			textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			textEdit1.Properties.Appearance.Options.UseFont = true;
			textEdit1.Properties.PasswordChar = '*';
			textEdit1.Size = new System.Drawing.Size(193, 20);
			textEdit1.TabIndex = 31;
			// 
			// checkEditCompactView
			// 
			checkEditCompactView.Location = new System.Drawing.Point(310, 64);
			checkEditCompactView.Name = "checkEditCompactView";
			checkEditCompactView.Properties.AutoWidth = true;
			checkEditCompactView.Properties.Caption = "Compact View";
			checkEditCompactView.Size = new System.Drawing.Size(90, 20);
			checkEditCompactView.TabIndex = 55;
			checkEditCompactView.CheckedChanged += checkEditCompactView_CheckedChanged;
			// 
			// FormSettings
			// 
			AcceptButton = buttonOK;
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			CancelButton = buttonClose;
			ClientSize = new System.Drawing.Size(423, 393);
			Controls.Add(buttonOK);
			Controls.Add(buttonClose);
			Controls.Add(tabPage);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			IconOptions.Icon = (System.Drawing.Icon)resources.GetObject("FormSettings.IconOptions.Icon");
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "FormSettings";
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Settings";
			((System.ComponentModel.ISupportInitialize)tabPage).EndInit();
			tabPage.ResumeLayout(false);
			tabPageGeneral.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)groupControlRibbonStyling).EndInit();
			groupControlRibbonStyling.ResumeLayout(false);
			groupControlRibbonStyling.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEditRibbonSkin.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)checkEditDarkMode.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEditRibbonStyle.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEditRibbonColorSheme.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)textEdit1.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)checkEditCompactView.Properties).EndInit();
			ResumeLayout(false);
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
		protected DevExpress.XtraEditors.CheckEdit checkEditCompactView;
	}
}