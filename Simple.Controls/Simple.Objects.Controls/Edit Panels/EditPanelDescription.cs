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
	public partial class EditPanelDescription : EditPanelWithTabControl
	{
		public EditPanelDescription()
		{
			InitializeComponent();
		}

		protected override void OnInitializeEditors()
		{
			base.OnInitializeEditors();

			IPropertyModel? propertyModel = this.BindingObject?.GetModel().DescriptionPropertyModel;

			if (propertyModel != null)
				this.EditorBindings.Register(this.editorDescription.Properties, this.BindingObject!.GetModel().ObjectType, propertyModel);
		}
    }
}
