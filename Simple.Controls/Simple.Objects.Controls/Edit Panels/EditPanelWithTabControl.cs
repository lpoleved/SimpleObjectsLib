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
    public partial class EditPanelWithTabControl : SimpleObjectEditPanel
    {
        public EditPanelWithTabControl()
        {
            InitializeComponent();
        }

        protected override void OnInitializeEditors()
        {
            base.OnInitializeEditors();

            this.EditorBindings.TabControl = this.tabControl;

            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Size = new System.Drawing.Size(this.Size.Width + 1, this.Size.Height + 1);
			//this.tabControl.PaintStyleName = "Skin";
        }

		protected override void OnBindingObjectChange(BindingObjectEventArgs e)
		{
			base.OnBindingObjectChange(e);
			this.RefreshObjectName(); // In case binding object has object sub types.
		}

		protected override void OnBindingObjectPropertyValueChange(ChangePropertyValueBindingObjectEventArgs e)
		{
			base.OnBindingObjectPropertyValueChange(e);

			IPropertyModel objectSubTypePropertyModel = e.BindingObject.GetModel().SubTypePropertyModel;

			if (objectSubTypePropertyModel != null && e.PropertyModel?.PropertyIndex == objectSubTypePropertyModel.PropertyIndex)
				this.RefreshObjectName();
		}

		protected virtual void RefreshObjectName()
		{
			ISimpleObjectModel? objectModel = this.BindingObject?.GetModel();
			string objectName = String.Empty;

			if (this.BindingObject != null && objectModel?.SubTypes.Count > 0)
			{
				int propertyIndex = this.BindingObject.GetPropertyValue<int>(objectModel.SubTypePropertyModel);
				IModelElement? modelElement;

				if (objectModel.SubTypes.TryGetValue(propertyIndex, out modelElement))
					objectName = modelElement.Caption;
			}
			else
			{
				objectName = objectModel?.ObjectCaption ?? String.Empty;
			}

			this.tabPageObjectName.Text = objectName;
		}
	}
}
