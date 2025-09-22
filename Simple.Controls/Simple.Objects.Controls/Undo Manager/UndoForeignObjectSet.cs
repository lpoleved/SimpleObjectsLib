using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Objects;

namespace Simple.Objects.Controls
{
	public class UndoForeignObjectSet : UndoAction
	{
		public UndoForeignObjectSet(RelationForeignObjectSetChangeContainerContextRequesterEventArgs foreignRelationSetInfo)
		{
			this.ForeignRelationSetInfo = foreignRelationSetInfo;
		}

		public RelationForeignObjectSetChangeContainerContextRequesterEventArgs ForeignRelationSetInfo { get; private set; }

		public override string GetText(UndoActionType actionType)
		{
			string text = "Set " + this.ForeignRelationSetInfo.RelationModel.Name + "=";
			
			if (actionType == UndoActionType.Undo)
			{
				text += (this.ForeignRelationSetInfo.OldForeignSimpleObject != null) ? this.ForeignRelationSetInfo.OldForeignSimpleObject.ToString() : "null";
			}
			else // if (actionType == UndoActionType.Redo)
			{
				text += (this.ForeignRelationSetInfo.ForeignSimpleObject != null) ? this.ForeignRelationSetInfo.ForeignSimpleObject.ToString() : "null";
			}

			return text;
		}

		public override void Undo()
		{
			switch (this.ForeignRelationSetInfo.RelationModel.RelationType)
			{
				case ObjectRelationType.OneToOne:

					this.ForeignRelationSetInfo.SimpleObject.SetOneToOnePrimaryObject(this.ForeignRelationSetInfo.OldForeignSimpleObject, this.ForeignRelationSetInfo.RelationModel.RelationKey, requester: this);
					
					break;

				case ObjectRelationType.OneToMany:

					this.ForeignRelationSetInfo.SimpleObject.SetOneToManyPrimaryObject(this.ForeignRelationSetInfo.OldForeignSimpleObject, this.ForeignRelationSetInfo.RelationModel.RelationKey, requester: this);
					
					break;
			}
		}

		public override void Redo()
		{
			switch (this.ForeignRelationSetInfo.RelationModel.RelationType)
			{
				case ObjectRelationType.OneToOne:

					this.ForeignRelationSetInfo.SimpleObject.SetOneToOnePrimaryObject(this.ForeignRelationSetInfo.ForeignSimpleObject, this.ForeignRelationSetInfo.RelationModel.RelationKey, requester: this);
					
					break;

				case ObjectRelationType.OneToMany:

					this.ForeignRelationSetInfo.SimpleObject.SetOneToManyPrimaryObject(this.ForeignRelationSetInfo.ForeignSimpleObject, this.ForeignRelationSetInfo.RelationModel.RelationKey, requester: this);
					
					break;
			}
		}
	}
}
