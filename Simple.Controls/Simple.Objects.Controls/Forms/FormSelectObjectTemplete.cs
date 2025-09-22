using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Simple.Objects.Controls
{
    public abstract partial class FormSelectObjectTemplate : XtraForm
    {
        public FormSelectObjectTemplate(SimpleObjectManager objectManager)
        {
			InitializeComponent();

			this.ObjectManager = objectManager;
            //this.treeList.OptionsSimple.EditMode = TreeList.SimpleTreeEditMode.SelectEdit;
            //this.treeList.OptionsSimple.LookAndFeelStyle = TreeList.SimpleTreeLookAndFeelStyle.ExplorerStyle;
        }

		public SimpleObjectManager ObjectManager { get; private set; }

        protected void SetGraphController(SimpleObjectGraphController graphController)
        {
            if (this.graphController != null)
                this.graphController.BindingObjectChange -= new BindingObjectEventHandler(graphController_BindingObjectChange);

            this.graphController = graphController;

			if (this.graphController != null)
			{
				this.graphController.ObjectManager = this.ObjectManager;
				this.graphController.BindingObjectChange += new BindingObjectEventHandler(graphController_BindingObjectChange);
			}
        }

        protected virtual void graphController_BindingObjectChange(object sender, BindingObjectEventArgs e)
        {
        }

        protected virtual void buttonCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
