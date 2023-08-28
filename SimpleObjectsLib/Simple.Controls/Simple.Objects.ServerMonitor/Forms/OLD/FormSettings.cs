using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Ribbon;
using DevExpress.Skins;
using Simple.Controls;
//using MsgPack.Serialization;

namespace Simple.Objects.ServerMonitor
{
    public partial class FormSettings : XtraForm
    {
        //private int minNumOfPasswdChars = 6;
        private Dictionary<RibbonControlStyle, string> ribbonStylesByEnumKey = new Dictionary<RibbonControlStyle, string>();
        private Dictionary<RibbonControlColorScheme, string> ribbonColorSchemeByEnumKey = new Dictionary<RibbonControlColorScheme, string>();
        //private Dictionary<string, DefaultSystemAclPolicy> defaultAclPoliciesByEnumName = null;
        //private Dictionary<Enum, string> defaultAclPoliciesByEnumKey2 = null;
        private RibbonControl ribbon = null;
        private RibbonControlStyle ribbonStyle;
        private RibbonControlColorScheme ribbonColorScheme;

        public FormSettings(RibbonControl ribbon)
        {
            this.ribbon = ribbon;

            InitializeComponent();

            this.ribbonStylesByEnumKey.Add(RibbonControlStyle.Office2007, "Office 2007");
            this.ribbonStylesByEnumKey.Add(RibbonControlStyle.Office2010, "Office 2010");
            this.ribbonStylesByEnumKey.Add(RibbonControlStyle.Office2013, "Office 2013");
            this.ribbonStylesByEnumKey.Add(RibbonControlStyle.Office2019, "Office 2019");
            this.ribbonStylesByEnumKey.Add(RibbonControlStyle.MacOffice, "Mac Office");
            this.ribbonStylesByEnumKey.Add(RibbonControlStyle.TabletOffice, "Tablet Office");
            this.ribbonStylesByEnumKey.Add(RibbonControlStyle.OfficeUniversal, "Office Universal");

            this.ribbonColorSchemeByEnumKey.Add(RibbonControlColorScheme.Yellow, "Yellow");
            this.ribbonColorSchemeByEnumKey.Add(RibbonControlColorScheme.Blue, "Blue");
            this.ribbonColorSchemeByEnumKey.Add(RibbonControlColorScheme.Green, "Green");
            this.ribbonColorSchemeByEnumKey.Add(RibbonControlColorScheme.Orange, "Orange");
            this.ribbonColorSchemeByEnumKey.Add(RibbonControlColorScheme.Purple, "Purple");
            this.ribbonColorSchemeByEnumKey.Add(RibbonControlColorScheme.Default, "Default");
            this.ribbonColorSchemeByEnumKey.Add(RibbonControlColorScheme.Teal, "Teal");
            this.ribbonColorSchemeByEnumKey.Add(RibbonControlColorScheme.Red, "Red");
            this.ribbonColorSchemeByEnumKey.Add(RibbonControlColorScheme.DarkBlue, "Dark Blue");

            this.Text = String.Format("{0} {1}", MonitorAppContext.Instance.AppName, " Settings");

            try
            {
                // Ribbon Style
                this.ribbonStyle = (RibbonControlStyle)MonitorAppContext.Instance.UserSettings.RibbonStyle;
               
                foreach (var item in this.ribbonStylesByEnumKey)
                    this.comboBoxEditRibbonStyle.Properties.Items.Add(item.Value);

                this.comboBoxEditRibbonStyle.EditValue = this.ribbonStylesByEnumKey[this.ribbonStyle];

                // Ribbon Color Sheme
                this.ribbonColorScheme = (RibbonControlColorScheme)MonitorAppContext.Instance.UserSettings.RibbonColorScheme;

                foreach (var item in this.ribbonColorSchemeByEnumKey)
                    this.comboBoxEditRibbonColorSheme.Properties.Items.Add(item.Value);

                this.comboBoxEditRibbonColorSheme.EditValue = this.ribbonColorSchemeByEnumKey[this.ribbonColorScheme];

                // Ribbon Skin
                foreach (SkinContainer skin in SkinManager.Default.Skins)
                    this.comboBoxEditRibbonSkin.Properties.Items.Add(skin.SkinName);
                
                this.comboBoxEditRibbonSkin.EditValue = MonitorAppContext.Instance.UserSettings.RibbonSkinName;
                this.checkEditDarkMode_CheckedChanged(null, null);

                // Dark Mode
                this.checkEditDarkMode.Checked = MonitorAppContext.Instance.UserSettings.DarkMode;
                
                // System
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, MonitorAppContext.Instance.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.tabPageGeneral.Focus();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.SetRibbonStyling((RibbonControlStyle)MonitorAppContext.Instance.UserSettings.RibbonStyle, (RibbonControlColorScheme)MonitorAppContext.Instance.UserSettings.RibbonColorScheme);
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            bool closeForm = true;

            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            if (closeForm)
            {
                try
                {
                    // Set Settings
                    MonitorAppContext.Instance.UserSettings.RibbonStyle = (int)this.ribbonStyle;
                    MonitorAppContext.Instance.UserSettings.RibbonColorScheme = (int)this.ribbonColorScheme;
                    MonitorAppContext.Instance.UserSettings.RibbonSkinName = this.comboBoxEditRibbonSkin.EditValue.ToString();
                    MonitorAppContext.Instance.UserSettings.DarkMode = this.checkEditDarkMode.Checked;

                    // Save Settings
                    MonitorAppContext.Instance.UserSettings.Save();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, MonitorAppContext.Instance.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            Cursor.Current = currentCursor;

            if (closeForm)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
        }

        private void comboBoxEditRibbonStyle_EditValueChanged(object sender, EventArgs e)
        {
            this.ribbonStyle = this.GetRibbonControlStyle();
            this.SetRibbonStyling(this.ribbonStyle, this.ribbonColorScheme);            
        }

        private void comboBoxEditRibbonColorSheme_EditValueChanged(object sender, EventArgs e)
        {
            this.ribbonColorScheme = this.GetRibbonColorScheme();
            this.SetRibbonStyling(this.ribbonStyle, this.ribbonColorScheme);
        }

        private void SetRibbonStyling(RibbonControlStyle ribbonStyle, RibbonControlColorScheme colorScheme)
        {
            this.ribbon.RibbonStyle = this.ribbonStylesByEnumKey.FirstOrDefault(item => item.Value == this.comboBoxEditRibbonStyle.EditValue.ToString()).Key;
            this.ribbon.ApplicationIcon = (this.ribbon.RibbonStyle == RibbonControlStyle.Office2007) ? global::Simple.Objects.ServerMonitor.Properties.Resources.SimpleObjects_Large : null;

            //this.comboBoxEditRibbonColorSheme.Enabled = (this.ribbon.RibbonStyle != RibbonControlStyle.Office2007);
            this.ribbon.ColorScheme = colorScheme;
        }

        private RibbonControlStyle GetRibbonControlStyle()
        {
            return this.ribbonStylesByEnumKey.FirstOrDefault(item => item.Value == this.comboBoxEditRibbonStyle.EditValue.ToString()).Key;
        }

        private RibbonControlColorScheme GetRibbonColorScheme()
        {
            return this.ribbonColorSchemeByEnumKey.FirstOrDefault(item => item.Value == this.comboBoxEditRibbonColorSheme.EditValue.ToString()).Key;
        }

		private void checkEditDarkMode_CheckedChanged(object sender, EventArgs e)
		{
            this.comboBoxEditRibbonSkin.Enabled = !this.checkEditDarkMode.Checked;
            this.labelControlRibbonSkin.Enabled = this.comboBoxEditRibbonSkin.Enabled;
        }
    }
}
