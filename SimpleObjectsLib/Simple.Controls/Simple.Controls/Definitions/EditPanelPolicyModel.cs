using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Modeling;

namespace Simple.Controls
{
    public class EditPanelPolicyModel : ModelElement, IEditPanelPolicyModel
    {
        public EditPanelPolicyModel(Type objectType, Type editPanelType, params int[] objectSubTypes)
        {
			this.ObjectType = objectType;
			this.Name = objectType.Name;
			this.EditPanelType = editPanelType;
			this.ObjectSubTypes = objectSubTypes;
        }
        
        public Type ObjectType { get; set; }
        public Type EditPanelType { get; set; }
		public int[] ObjectSubTypes { get; set; }
	}

	public interface IEditPanelPolicyModel : IModelElement
    {
        Type ObjectType { get; }
        Type EditPanelType { get; }
		int[] ObjectSubTypes { get; set; }
	}
}
