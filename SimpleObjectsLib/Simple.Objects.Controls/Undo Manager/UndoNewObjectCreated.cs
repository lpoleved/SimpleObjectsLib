using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Modeling;
using Simple.Objects;

namespace Simple.Objects.Controls
{
	public class UndoNewObjectCreated : UndoAction
	{
		protected IEnumerable<PropertyIndexValuePair>? propertyIndexValues = null;

		public UndoNewObjectCreated(SimpleObject simpleObject)
		{
			this.SimpleObject = simpleObject;
		}

		public SimpleObject SimpleObject { get; protected set; }

		public override string GetText(UndoActionType actionType)
		{
			string text = (actionType == UndoActionType.Undo) ? "Delete " : "Create ";
			text += this.SimpleObject.ToString();

			return text;
		}

		public override void Undo()
		{
			this.propertyIndexValues = this.SimpleObject.GetNonDefaultPropertyIndexValues();
			this.SimpleObject.RequestDelete();
			this.SimpleObject.Manager.CommitChanges();
		}

		public override void Redo()
		{
			SimpleObject newSimpleObject = this.SimpleObject.Manager.CreateNewEmptyObject(this.SimpleObject.GetType(), isNew: true, objectId: 0, requester: this);

			this.propertyIndexValues = this.SimpleObject.GetNonDefaultPropertyIndexValues();
			//int primaryTableId = 0;

			for (int i = 0; i < this.propertyIndexValues.Count(); i++)
			{
				var item = this.propertyIndexValues.ElementAt(i);
				IPropertyModel propertyModel = newSimpleObject.GetModel().PropertyModels[item.PropertyIndex];

				//if (setPropertiesOption == SetPropertiesOption.AvoidSetsRelations && (propertyModel.IsRelationTableId || propertyModel.IsRelationObjectId))
				//	continue;

				//if (setPropertiesOption == SetPropertiesOption.SetOnlyRelations && (!propertyModel.IsRelationTableId || !propertyModel.IsRelationObjectId))
				//	continue;

				newSimpleObject.SetPropertyValue(propertyModel, item.PropertyValue, this.SimpleObject.Manager.DefaultChangeContainer, requester: this);
			}

			this.SimpleObject = newSimpleObject;
		}
	}
}
