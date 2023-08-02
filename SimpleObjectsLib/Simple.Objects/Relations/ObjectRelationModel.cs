using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Collections;
using Simple.Modeling;

namespace Simple.Objects
{
    public class ObjectRelationModel : ModelElement, IObjectRelationModel, IModelElement
    {
		private RelationPolicyModelBase relationPolicyModel;
		private bool includeSubclasses = true;
		//private HashArray<OneToOneOrManyRelationModel> foregnRelationModelsByPropertyIndex = new HashArray<OneToOneOrManyRelationModel>();
		//private HashArray<OneToOneOrManyRelationModel> foregnRelationModelsByPropertyIndex = new HashArray<OneToOneOrManyRelationModel>();
		//private HashArray<OneToOneOrManyRelationModel> foregnRelationModelsByTableId = new HashArray<OneToOneOrManyRelationModel>();

		private ModelDictionary<int, OneToOneRelationModel> asPrimaryObjectInOneToOneRelations = new ModelDictionary<int, OneToOneRelationModel>();
		private ModelDictionary<int, OneToOneRelationModel> asForeignObjectInOneToOneRelations = new ModelDictionary<int, OneToOneRelationModel>();

		private ModelDictionary<int, OneToManyRelationModel> asPrimaryObjectInOneToManyRelations = new ModelDictionary<int, OneToManyRelationModel>();
		private ModelDictionary<int, OneToManyRelationModel> asForeignObjectInOneToManyRelations = new ModelDictionary<int, OneToManyRelationModel>();
		
		private ModelDictionary<int, OneToOneOrManyRelationModel> asPrimaryObjectInRelations = new ModelDictionary<int, OneToOneOrManyRelationModel>();
		private ModelDictionary<int, OneToOneOrManyRelationModel> asForeignObjectInRelations = new ModelDictionary<int, OneToOneOrManyRelationModel>();

		private ModelDictionary<int, ManyToManyRelationModel> asFirstObjectInManyToManyRelations = new ModelDictionary<int, ManyToManyRelationModel>();
		private ModelDictionary<int, ManyToManyRelationModel> asSecondObjectInManyToManyRelations = new ModelDictionary<int, ManyToManyRelationModel>();
		private ModelDictionary<int, ManyToManyRelationModel> asObjectInGroupMembership = new ModelDictionary<int, ManyToManyRelationModel>();

		private IObjectRelationModelCollection<IOneToOneRelationModel> iasPrimaryObjectInOneToOneRelations, iasForeignObjectInOneToOneRelations = null;
		private IObjectRelationModelCollection<IOneToManyRelationModel> iasPrimaryObjectInOneToManyRelations, iasForeignObjectInOneToManyRelations = null;
		private IObjectRelationModelCollection<IOneToOneOrManyRelationModel> iasPrimaryObjectInRelarions, iasForeignObjectInRelarions = null;
		private IObjectRelationModelCollection<IManyToManyRelationModel> iasFirstObjectInManyToManyRelations, iasSecondObjectInManyToManyRelations = null;
		private IObjectRelationModelCollection<IManyToManyRelationModel> iasObjectInGroupMembership = null;

		private List<int> foreingRelationKeys = new List<int>();
		private Dictionary<int, List<IOneToOneOrManyRelationModel>> foreignRelationsByPropertyIndex = new Dictionary<int, List<IOneToOneOrManyRelationModel>>();
		
		public ObjectRelationModel(SimpleObjectModel objectModel, SimpleObjectManager manager)
			: this(objectModel, manager, includeSubclasses: true)
		{
		}

		public ObjectRelationModel(SimpleObjectModel objectModel, SimpleObjectManager manager, bool includeSubclasses)
			: this(objectModel, manager.ModelDiscovery.RelationPolicyModel, includeSubclasses)
		{
		}

		public ObjectRelationModel(SimpleObjectModel objectModel, RelationPolicyModelBase relationPolicyModel)
			: this(objectModel, relationPolicyModel, includeSubclasses: true)
		{
		}

		public ObjectRelationModel(SimpleObjectModel objectModel, RelationPolicyModelBase relationPolicyModel, bool includeSubclasses)
			: this(objectModel, objectModel.ObjectType, relationPolicyModel, includeSubclasses)
		{
		}

		public ObjectRelationModel(SimpleObjectModel objectModel, Type objectType, RelationPolicyModelBase relationPolicyModel, bool includeSubclasses)
		{
			this.ObjectModel = objectModel;
			this.ObjectType = objectType;
			this.relationPolicyModel = relationPolicyModel;
			this.includeSubclasses = includeSubclasses;

			// One to One relations creation
			foreach (OneToOneRelationModel relationModel in this.relationPolicyModel.OneToOneRelations.Values)
			{
				//if ((relationModel.PrimaryObjectAllowedTypes != null && relationModel.PrimaryObjectAllowedTypes.Contains(objectType)) ||
				//	this.IsObjectTypeSameOrSubclassOf(objectType, relationModel.PrimaryObjectType))
				if (!(relationModel.PrimaryObjectExcludedTypes != null && relationModel.PrimaryObjectExcludedTypes.Contains(objectType)) &&
					(
						(relationModel.PrimaryObjectAllowedTypes != null && relationModel.PrimaryObjectAllowedTypes.Contains(objectType)) ||
						(this.IsObjectTypeSameOrSubclassOf(objectType, relationModel.PrimaryObjectType)))
					)

				{
					this.asPrimaryObjectInOneToOneRelations.Add(relationModel.RelationKey, relationModel);
					this.asPrimaryObjectInRelations.Add(relationModel.RelationKey, relationModel);
				}

				// Foreign object type is only one fiksed with TableId and has its own table in datastore.
				if (this.ObjectType == relationModel.ForeignObjectType)
				{
					this.asForeignObjectInOneToOneRelations.Add(relationModel.RelationKey, relationModel);
					this.asForeignObjectInRelations.Add(relationModel.RelationKey, relationModel);
					this.foreingRelationKeys.Add(relationModel.RelationKey);
				}
			}

			this.AsPrimaryObjectInOneToOneRelations = new ObjectRelationModelCollection<OneToOneRelationModel>(this.asPrimaryObjectInOneToOneRelations);
			this.iasPrimaryObjectInOneToOneRelations = new ObjectRelationModelCollection<IOneToOneRelationModel>(this.asPrimaryObjectInOneToOneRelations.AsCustom<IOneToOneRelationModel>());
			this.AsForeignObjectInOneToOneRelations = new ObjectRelationModelCollection<OneToOneRelationModel>(this.asForeignObjectInOneToOneRelations);
			this.iasForeignObjectInOneToOneRelations = new ObjectRelationModelCollection<IOneToOneRelationModel>(this.asForeignObjectInOneToOneRelations.AsCustom<IOneToOneRelationModel>());

			//if (objectType.Name == "Device")
			//	objectType = objectType;

			// One to Many relations creation
			foreach (OneToManyRelationModel relationModel in relationPolicyModel.OneToManyRelations.Values)
			{
				if (relationModel.RelationKey == 3)
				{
					int key = relationModel.RelationKey;
				}
				
				if (!(relationModel.PrimaryObjectExcludedTypes != null && relationModel.PrimaryObjectExcludedTypes.Contains(objectType)) &&
					(
						(relationModel.PrimaryObjectAllowedTypes != null && relationModel.PrimaryObjectAllowedTypes.Contains(objectType)) ||
						(this.IsObjectTypeSameOrSubclassOf(objectType, relationModel.PrimaryObjectType)))
					)
				{
					this.asPrimaryObjectInOneToManyRelations.Add(relationModel.RelationKey, relationModel);
					this.asPrimaryObjectInRelations.Add(relationModel.RelationKey, relationModel);
				}

				if (this.ObjectType == relationModel.ForeignObjectType)
				{
					this.asForeignObjectInOneToManyRelations.Add(relationModel.RelationKey, relationModel);
					this.asForeignObjectInRelations.Add(relationModel.RelationKey, relationModel);
					this.foreingRelationKeys.Add(relationModel.RelationKey);
				}
			}

			this.AsPrimaryObjectInOneToManyRelations = new ObjectRelationModelCollection<OneToManyRelationModel>(this.asPrimaryObjectInOneToManyRelations);
			this.iasPrimaryObjectInOneToManyRelations = new ObjectRelationModelCollection<IOneToManyRelationModel>(this.asPrimaryObjectInOneToManyRelations.AsCustom<IOneToManyRelationModel>());
			this.AsForeignObjectInOneToManyRelations = new ObjectRelationModelCollection<OneToManyRelationModel>(this.asForeignObjectInOneToManyRelations);
			this.iasForeignObjectInOneToManyRelations = new ObjectRelationModelCollection<IOneToManyRelationModel>(this.asForeignObjectInOneToManyRelations.AsCustom<IOneToManyRelationModel>());

			this.AsPrimaryObjectInRelations = new ObjectRelationModelCollection<OneToOneOrManyRelationModel>(this.asPrimaryObjectInRelations);
			this.iasPrimaryObjectInRelarions = new ObjectRelationModelCollection<IOneToOneOrManyRelationModel>(this.asPrimaryObjectInRelations.AsCustom<IOneToOneOrManyRelationModel>());
			this.AsForeignObjectInRelations = new ObjectRelationModelCollection<OneToOneOrManyRelationModel>(this.asForeignObjectInRelations);
			this.iasForeignObjectInRelarions = new ObjectRelationModelCollection<IOneToOneOrManyRelationModel>(this.asForeignObjectInRelations.AsCustom<IOneToOneOrManyRelationModel>());

			// Many to Many relation creation
			foreach (ManyToManyRelationModel manyToManyRelationModel in relationPolicyModel.ManyToManyRelations.Values)
			{
				if (this.IsObjectTypeSameOrSubclassOf(this.ObjectType, manyToManyRelationModel.FirstObjectType))
				{
					this.asSecondObjectInManyToManyRelations.Add(manyToManyRelationModel.RelationKey, manyToManyRelationModel);
					
					if (!this.asObjectInGroupMembership.ContainsKey(manyToManyRelationModel.RelationKey))
						this.asObjectInGroupMembership.Add(manyToManyRelationModel.RelationKey, manyToManyRelationModel);
				}

				if (this.IsObjectTypeSameOrSubclassOf(this.ObjectType, manyToManyRelationModel.SecondObjectType))
				{
					this.asFirstObjectInManyToManyRelations.Add(manyToManyRelationModel.RelationKey, manyToManyRelationModel);
					
					if (!this.asObjectInGroupMembership.ContainsKey(manyToManyRelationModel.RelationKey))
						this.asObjectInGroupMembership.Add(manyToManyRelationModel.RelationKey, manyToManyRelationModel);
				}
			}

			this.AsFirstObjectInManyToManyRelations = new ObjectRelationModelCollection<ManyToManyRelationModel>(this.asFirstObjectInManyToManyRelations);
			this.iasFirstObjectInManyToManyRelations = new ObjectRelationModelCollection<IManyToManyRelationModel>(this.asFirstObjectInManyToManyRelations.AsCustom<IManyToManyRelationModel>());
			this.AsSecondObjectInManyToManyRelations = new ObjectRelationModelCollection<ManyToManyRelationModel>(this.asSecondObjectInManyToManyRelations);
			this.iasSecondObjectInManyToManyRelations = new ObjectRelationModelCollection<IManyToManyRelationModel>(this.asSecondObjectInManyToManyRelations.AsCustom<IManyToManyRelationModel>());
			this.AsObjectInGroupMembership = new ObjectRelationModelCollection<ManyToManyRelationModel>(this.asObjectInGroupMembership);
			this.iasObjectInGroupMembership = new ObjectRelationModelCollection<IManyToManyRelationModel>(this.asObjectInGroupMembership.AsCustom<IManyToManyRelationModel>());
		}
        
		//public Type ObjectType { get; private set; }
		public SimpleObjectModel ObjectModel { get; private set; }
		public Type ObjectType { get; private set; }
		public ObjectRelationModelCollection<OneToOneRelationModel> AsPrimaryObjectInOneToOneRelations { get; private set; }
		public ObjectRelationModelCollection<OneToOneRelationModel> AsForeignObjectInOneToOneRelations { get; private set; }
		public ObjectRelationModelCollection<OneToManyRelationModel> AsPrimaryObjectInOneToManyRelations { get; private set; }
		public ObjectRelationModelCollection<OneToManyRelationModel> AsForeignObjectInOneToManyRelations { get; private set; }
		public ObjectRelationModelCollection<OneToOneOrManyRelationModel> AsPrimaryObjectInRelations { get; private set; }
		public ObjectRelationModelCollection<OneToOneOrManyRelationModel> AsForeignObjectInRelations { get; private set; }
		public ObjectRelationModelCollection<ManyToManyRelationModel> AsFirstObjectInManyToManyRelations { get; private set; }
		public ObjectRelationModelCollection<ManyToManyRelationModel> AsSecondObjectInManyToManyRelations { get; private set; }
		public ObjectRelationModelCollection<ManyToManyRelationModel> AsObjectInGroupMembership { get; private set; }

		internal bool IsObjectTypeSameOrSubclassOf(Type objectType, Type relationModelType)
		{
			return objectType == relationModelType || (this.includeSubclasses && objectType.IsSubclassOf(relationModelType));
		}

		public IOneToOneOrManyRelationModel GetForeignRelationModel(int propertyIndex, int foreignObjectTableId = 0)
		{
			List<IOneToOneOrManyRelationModel> itemList;

			if (this.foreignRelationsByPropertyIndex.TryGetValue(propertyIndex, out itemList))
			{
				if (itemList.Count == 1)
					return itemList[0];

				return itemList.Find(item => item.PrimaryObjectTableId == foreignObjectTableId);
			}

			return null;
		}

		internal void SetForegnKeyRelationModelsByPropertyIndex(IOneToOneOrManyRelationModel relationModel)
		{
			List<IOneToOneOrManyRelationModel> itemList;

			if (!this.foreignRelationsByPropertyIndex.TryGetValue(relationModel.PrimaryObjectIdPropertyModel.PropertyIndex, out itemList))
			{
				itemList = new List<IOneToOneOrManyRelationModel>();
				this.foreignRelationsByPropertyIndex.Add(relationModel.PrimaryObjectIdPropertyModel.PropertyIndex, itemList);
			}

			itemList.Add(relationModel);
		}

		ISimpleObjectModel IObjectRelationModel.ObjectModel
		{
			get { return this.ObjectModel; }
		}

		IObjectRelationModelCollection<IOneToOneRelationModel> IObjectRelationModel.AsPrimaryObjectInOneToOneRelations
		{
			get { return this.iasPrimaryObjectInOneToOneRelations; }
		}

		IObjectRelationModelCollection<IOneToOneRelationModel> IObjectRelationModel.AsForeignObjectInOneToOneRelations
		{
			get { return this.iasForeignObjectInOneToOneRelations; }
		}

		IObjectRelationModelCollection<IOneToManyRelationModel> IObjectRelationModel.AsPrimaryObjectInOneToManyRelations
		{
			get { return this.iasPrimaryObjectInOneToManyRelations; }
		}

		IObjectRelationModelCollection<IOneToManyRelationModel> IObjectRelationModel.AsForeignObjectInOneToManyRelations
		{
			get { return this.iasForeignObjectInOneToManyRelations; }
		}

		IObjectRelationModelCollection<IOneToOneOrManyRelationModel> IObjectRelationModel.AsPrimaryObjectInRelarions
		{
			get { return this.iasPrimaryObjectInRelarions; }
		}

		IObjectRelationModelCollection<IOneToOneOrManyRelationModel> IObjectRelationModel.AsForeignObjectInRelarions
		{
			get { return this.iasForeignObjectInRelarions; }
		}

		IEnumerable<int> IObjectRelationModel.ForeignObjectKeys
		{
			get { return this.foreingRelationKeys; }
		}

		IObjectRelationModelCollection<IManyToManyRelationModel> IObjectRelationModel.AsFirstObjectInManyToManyRelations
		{
			get { return this.iasFirstObjectInManyToManyRelations; }
		}

		IObjectRelationModelCollection<IManyToManyRelationModel> IObjectRelationModel.AsSecondObjectInManyToManyRelations
		{
			get { return this.iasSecondObjectInManyToManyRelations; }
		}

		IObjectRelationModelCollection<IManyToManyRelationModel> IObjectRelationModel.AsObjectInGroupMembership
		{
			get { return this.iasObjectInGroupMembership; }
		}

		IOneToOneOrManyRelationModel IObjectRelationModel.GetForeignRelationModel(int propertyIndex, int foreignTableId)
		{
			return this.GetForeignRelationModel(propertyIndex, foreignTableId);
		}
	}

	public interface IObjectRelationModel : IModelElement
    {
        ISimpleObjectModel ObjectModel { get; }
		IObjectRelationModelCollection<IOneToOneRelationModel> AsPrimaryObjectInOneToOneRelations { get; }
		IObjectRelationModelCollection<IOneToOneRelationModel> AsForeignObjectInOneToOneRelations { get; }
		IObjectRelationModelCollection<IOneToManyRelationModel> AsPrimaryObjectInOneToManyRelations { get; }
		IObjectRelationModelCollection<IOneToManyRelationModel> AsForeignObjectInOneToManyRelations { get; }
		IObjectRelationModelCollection<IOneToOneOrManyRelationModel> AsPrimaryObjectInRelarions { get; }
		IObjectRelationModelCollection<IOneToOneOrManyRelationModel> AsForeignObjectInRelarions { get; }
		IEnumerable<int> ForeignObjectKeys { get; }
		IObjectRelationModelCollection<IManyToManyRelationModel> AsFirstObjectInManyToManyRelations { get; }
		IObjectRelationModelCollection<IManyToManyRelationModel> AsSecondObjectInManyToManyRelations { get; }
		IObjectRelationModelCollection<IManyToManyRelationModel> AsObjectInGroupMembership { get; }

		// TODO: Remove this
		IOneToOneOrManyRelationModel GetForeignRelationModel(int propertyIndex, int foreignTableId);
	}
}
