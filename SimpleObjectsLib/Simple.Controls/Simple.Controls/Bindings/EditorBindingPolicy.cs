using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraTab;
using DevExpress.XtraEditors.Repository;
using Simple.Modeling;

namespace Simple.Controls
{
    public class EditorBindingPolicy
    {
		public EditorBindingPolicy(Component editorComponent, Type objectType, EditorBindingType bindingType, IPropertyModel propertyModel, int relationKey, XtraTabPage? tabPage, 
								   Func<object, object?>? getPropertyValueFromControlValue, Func<object?, object>? getControlValueFromPropertyValue, Action<object>? setControlValue)
		{
			this.EditorComponent = editorComponent;
			this.ObjectType = objectType;
			this.BindingType = bindingType;
			this.PropertyModel = propertyModel;
			this.RelationKey = relationKey;
			this.TabPage = tabPage;
			this.GetPropertyValueFromControlValue = getPropertyValueFromControlValue;
			this.GetControlValueFromPropertyValue = getControlValueFromPropertyValue;
		}
		
		public Component EditorComponent { get; set; }
		public Type ObjectType { get; set; }
		public EditorBindingType BindingType { get; set; }
        public IPropertyModel PropertyModel { get; set; }
		public int RelationKey { get; set; }
        public XtraTabPage? TabPage { get; set; }
        public Func<object, object?>? GetPropertyValueFromControlValue { get; set; }
        public Func<object?, object>? GetControlValueFromPropertyValue { get; set; }
        public Action<object?>? SetControlValue { get; set; }
    }

	public enum EditorBindingType
	{
		ObjectProperty,
		OneToOneRelationPrimaryObject,
		OneToOneRelationForeignObject,
		OneToManyRelation,
	}
}
