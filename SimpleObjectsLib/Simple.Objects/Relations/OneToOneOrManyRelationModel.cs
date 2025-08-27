using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Modeling;

namespace Simple.Objects
{
    public abstract class OneToOneOrManyRelationModel : RelationModel, IOneToOneOrManyRelationModel
    {
		private Type foreignObjectType, primaryObjectType;
		private PropertyModel? primaryTableIdPropertyModel;
		private PropertyModel primaryObjectIdPropertyModel; //, foreignKeyPropertyModel;

		public OneToOneOrManyRelationModel()
        {
			//this.ForeignTableIdPropertyIndex = -1;
			//this.ForeignGuidPropertyIndex = -1;
			this.ForeignAccessModifier = AccessModifier.Public;
			this.PrimaryAccessModifier = AccessModifier.Public;
			//this.KeyHolderObjectTableId = -1;
			//this.ForeignObjectTableId = -1;
        }
        
		public abstract ObjectRelationType RelationType { get; }

		public Type ForeignObjectType 
		{
			get { return this.foreignObjectType; }
			
			set
			{
				this.foreignObjectType = value;
				this.OnSetForeignObjectType();
			}
		}

		protected virtual void OnSetForeignObjectType()	{ }

		public Type PrimaryObjectType
		{
			get { return this.primaryObjectType; }
			
			set
			{
				this.primaryObjectType = value;

				if (value != null && String.IsNullOrEmpty(this.PrimaryObjectName))
					this.PrimaryObjectName = value.Name;
			}
		}

		public Type[] PrimaryObjectAllowedTypes { get; set; }
		public Type[] PrimaryObjectExcludedTypes { get; set; }
		public int PrimaryObjectTableId { get; internal set; }
		public int ForeignObjectTableId { get; internal set; }
		public string PrimaryObjectName { get; set; }
		public string PrimaryObjectSummary { get; set; }

		public PropertyModel? PrimaryTableIdPropertyModel
		{
			get { return this.primaryTableIdPropertyModel; }

			set
			{
				this.primaryTableIdPropertyModel = value;

				if (value != null)
				{
					value.IsStorable = true;
					//value.IsMemberOfSerializationSequence = false; // This is only datastore key object type (TableId) redundancy.
					//value.IncludeInTransactionActionLog = false;
					//value.TableIdAsRelationKeyPropertyModel = this.ForeignKeyPropertyModel;
					value.IsRelationTableId = true;
					value.RelationKey = this.RelationKey;
					value.IsIndexed = true;
				}
			}
		}

		//public int ForeignGuidPropertyIndex { get; set; }
		public PropertyModel PrimaryObjectIdPropertyModel
		{
			get { return this.primaryObjectIdPropertyModel; }

			set
			{
				this.primaryObjectIdPropertyModel = value;

				if (this.primaryObjectIdPropertyModel != null)
				{
					this.primaryObjectIdPropertyModel.IsRelationObjectId = true;
					this.primaryObjectIdPropertyModel.RelationKey = this.RelationKey;
					this.primaryObjectIdPropertyModel.IsIndexed = true;
				}

				
				//if (this.ForeignTableIdPropertyModel != null)
				//	this.ForeignTableIdPropertyModel.TableIdAsRelationKeyPropertyModel = value;
			}
		}

		//public PropertyModel ForeignKeyPropertyModel
		//{
		//	get { return this.foreignKeyPropertyModel; }

		//	set
		//	{
		//		this.foreignKeyPropertyModel = value;

		//		if (this.foreignKeyPropertyModel != null)
		//		{
		//			this.foreignKeyPropertyModel.IsRelationKey = true;
		//			this.foreignKeyPropertyModel.IsIndexed = true;
		//		}

		//		if (this.ForeignTableIdPropertyModel != null)
		//			this.ForeignTableIdPropertyModel.TableIdAsRelationKeyPropertyModel = value;
		//	}
		//}

		//public string ForeignObjectCreatorServerIdPropertyName { get; set; }
		public AccessModifier ForeignAccessModifier { get; set; }
		public AccessModifier ForeignSetAccessModifier { get; set; }
		public AccessModifier PrimaryAccessModifier { get; set; }
		public AccessModifier PrimarySetAccessModifier { get; set; }
		public bool CascadeDelete { get; set; }

		IPropertyModel? IOneToOneOrManyRelationModel.PrimaryTableIdPropertyModel
		{
			get { return this.PrimaryTableIdPropertyModel; }
		}

		IPropertyModel IOneToOneOrManyRelationModel.PrimaryObjectIdPropertyModel
		{
			get { return this.PrimaryObjectIdPropertyModel; }
		}

		//IPropertyModel IOneToOneOrManyRelationModel.ForeignKeyPropertyModel
		//{
		//	get { return this.ForeignKeyPropertyModel; }
		//}
	}

    public interface IOneToOneOrManyRelationModel : IRelationModel
    {
		ObjectRelationType RelationType { get; }
		Type PrimaryObjectType { get; }
		Type ForeignObjectType { get; }
		Type[] PrimaryObjectAllowedTypes { get; }
		Type[] PrimaryObjectExcludedTypes { get; }

		int PrimaryObjectTableId { get; }
		int ForeignObjectTableId { get; }
		string PrimaryObjectName { get; }
		string PrimaryObjectSummary { get; }
		//int ForeignTableIdPropertyIndex { get; }
		IPropertyModel? PrimaryTableIdPropertyModel { get; }
		IPropertyModel PrimaryObjectIdPropertyModel { get; }
		//IPropertyModel ForeignKeyPropertyModel { get; }
		//string ForeignObjectCreatorServerIdPropertyName { get; }
        bool CascadeDelete { get; set; }
    }
}
