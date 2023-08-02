using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Simple.Controls;
using Simple.AppContext;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.Skins;
using DevExpress.LookAndFeel;

namespace Simple.Objects.Controls
{
    public abstract partial class SimpleRibbonFormTemplate : SimpleRibbonFormBase
    {
        #region |   Constructors and Initialization   |

		public SimpleRibbonFormTemplate()
        {
            this.InitializeComponent();

            /* Add Module definitions here...
            // Example
            // this.ModuleDefinitions.Add(typeof(ModuleUsers));
            // this.ModuleDefinitions.Add(typeof(ModuleUserGroups));
            */

            //this.Text = AppContext.Instance.AppName;
            this.barStaticItemServer.Caption = "Server";   // <- Server name

            base.Ribbon = this.ribbonMain;
            base.Panel = this.panelMain;
            base.BarSubItemChangeSkin = this.barSubItemChangeSkin;
            base.Initialize();
        }

        #endregion |   Constructors and Initialization   |

        #region |   Private Events Methods   |

        private void menuItemHelp_ItemClick(object sender, ItemClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void menuItemVersionHistory_ItemClick(object sender, ItemClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void menuItemAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void barButtonAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void barButtonHelp_ItemClick(object sender, ItemClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion |   Private Events Methods   |
    }
}