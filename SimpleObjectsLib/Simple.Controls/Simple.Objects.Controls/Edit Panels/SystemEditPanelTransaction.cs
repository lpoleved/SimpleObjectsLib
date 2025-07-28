using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Simple.Controls;
using Simple.Objects;

namespace Simple.Objects.Controls
{
	public partial class SystemEditPanelTransaction : SystemEditPanelWithTabControl 
    {
		public SystemEditPanelTransaction()
        {
            InitializeComponent();
        }

		//protected override void OnBindingObjectChange(object bindingObject, object oldBindingObject)
		//{
		//	base.OnBindingObjectChange(bindingObject, oldBindingObject);
		//	this.OnRefreshBindingObject();
		//}

		protected override void OnRefreshBindingObject()
		{
			base.OnRefreshBindingObject();

			if (this.BindingObject is SystemTransaction transaction)
			{
				this.editorTransactionId.Text = String.Format("{0}", transaction.TransactionId);
				this.editorTime.Text = String.Format("{0}.{1}", transaction.CreationTime.ToString(), transaction.CreationTime.Millisecond.ToString());
				this.editorRequestCount.Text = transaction.RollbackTransactionActions?.Count().ToString();
				this.editorStatus.Text = transaction.Status.ToString();
				this.editorErrorDescription.Text = transaction.ErrorDescription;
			}
		}
    }
}
