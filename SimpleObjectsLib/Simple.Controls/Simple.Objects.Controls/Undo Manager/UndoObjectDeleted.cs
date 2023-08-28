using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Modeling;
using Simple.Objects;

namespace Simple.Objects.Controls
{
	public class UndoObjectDeleted : UndoNewObjectCreated
	{
		public UndoObjectDeleted(SimpleObject simpleObject)
			: base(simpleObject)
		{
			this.propertyIndexValues = this.SimpleObject.GetNonDefaultPropertyIndexValues();
		}

		public override string GetText(UndoActionType actionType)
		{
			string text = (actionType == UndoActionType.Undo) ? "Create " : "Delete ";
			text += this.SimpleObject.ToString();

			return text;
		}

		public override void Undo()
		{
			SimpleObject newSimpleObject = this.SimpleObject.Manager.CreateNewEmptyObject(this.SimpleObject.GetType(), isNew: true, objectId: 0, requester: this);

			if (this.propertyIndexValues != null)
			{
				newSimpleObject.SetPropertyValues(this.propertyIndexValues, this.SimpleObject.Manager.DefaultChangeContainer, requester: this);
				this.SimpleObject = newSimpleObject;
			}
		}

		public override void Redo()
		{
			//object[] propertyValues = this.SimpleObject.GetPropertyValues(this.SimpleObject.GetModel().PropertyIndexes);
			//int[] propertyIndexes = this.SimpleObject.GetChangedPropertyIndexes();
			//object[] propertyValues = this.SimpleObject.GetPropertyValues(propertyIndexes);
			this.propertyIndexValues = this.SimpleObject.GetNonDefaultPropertyIndexValuePairs();

			this.SimpleObject.RequestDelete();
			this.SimpleObject.Manager.CommitChanges();
		}
	}
}
