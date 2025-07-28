using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Simple.Objects;
using Simple.Modeling;

namespace Simple.Objects.Controls
{
	public partial class EditPanelName : EditPanelWithTabControl
	{
		public EditPanelName()
		{
			InitializeComponent();
		}

		protected override void OnInitializeEditors()
		{
			base.OnInitializeEditors();

			IPropertyModel? propertyModel = this.BindingObject?.GetModel().NamePropertyModel;

			if (propertyModel != null)
				this.EditorBindings.Register(this.editorName.Properties, this.BindingObject!.GetModel().ObjectType, propertyModel);
		}
    }
}
