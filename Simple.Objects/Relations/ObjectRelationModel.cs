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

		private ModelDictionary<int, OneToOneRelationModel> asObjectInOneToOneRelations = new ModelDictionary<int, OneToOneRelationModel>();
		private ModelDictionary<int, OneToOneRelationModel> asPrimaryObjectInOneToOneRelations = new ModelDictionary<int, OneToOneRelationModel>();
		private ModelDictionary<int, OneToOneRelationModel> asForeignObjectInOneToOneRelations = new ModelDictionary<int, OneToOneRelationModel>();

		private ModelDictionary<int, OneToManyRelationModel> asObjectInOneToManyRelations = new ModelDictionary<int, OneToManyRelationModel>();
		private ModelDictionary<int, OneToManyRelationModel> asPrimaryObjectInOneToManyRelations = new ModelDictionary<int, OneToManyRelationModel>();
		private ModelDictionary<int, OneToManyRelationModel> asForeignObjectInOneToManyRelations = new ModelDictionary<int, OneToManyRelationModel>();
		
		private ModelDictionary<int, OneToOneOrManyRelationModel> asPrimaryObjectInRelations = new ModelDictionary<int, OneToOneOrManyRelationModel>();
		private ModelDictionary<int, OneToOneOrManyRelationModel> asForeignObjectInRelations = new ModelDictionary<int, OneToOneOrManyRelationModel>();

		private ModelDictionary<int, ManyToManyRelationModel> asObjectInGroupMembership = new ModelDictionary<int, ManyToManyRelationModel>();
		private ModelDictionary<int, ManyToManyRelationModel> asFirstObjectInManyToManyRelations = new ModelDictionary<int, ManyToManyRelationModel>();
		private ModelDictionary<int, ManyToManyRelationModel> asSecondObjectInManyToManyRelations = new ModelDictionary<int, ManyToManyRelationModel>();

		private IObjectRelationModelCollection<IOneToOneRelationModel> iasObjectInOneToOneRelations = null;
		private IObjectRelationModelCollection<IOneToOneRelationModel> iasPrimaryObjectInOneToOneRelations, iasForeignObjectInOneToOneRelations = null;
		private IObjectRelationModelCollection<IOneToManyRelationModel> iasObjectInOneToManyRelations;
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
					this.asObjectInOneToOneRelations.Add(relationModel.RelationKey, relationModel);
					this.asPrimaryObjectInRelations.Add(relationModel.RelationKey, relationModel);
				}

				// Foreign object type is only one fiksed with TableId and has its own table in datastore.
				if (this.ObjectType == relationModel.ForeignObjectType)
				{
					this.asForeignObjectInOneToOneRelations.Add(relationModel.RelationKey, relationModel);
					this.asForeignObjectInRelations.Add(relationModel.RelationKey, relationModel);
					this.foreingRelationKeys.Add(relationModel.RelationKey);

					if (!this.asObjectInOneToOneRelations.ContainsKey(relationModel.RelationKey))
						this.asObjectInOneToOneRelations.Add(relationModel.RelationKey, relationModel);

				}
			}

			this.AsObjectInOneToOneRelations = new ObjectRelationModelCollection<OneToOneRelationModel>(this.asObjectInOneToOneRelations);
			this.iasObjectInOneToOneRelations = new ObjectRelationModelCollection<IOneToOneRelationModel>(this.asObjectInOneToOneRelations.AsCustom<IOneToOneRelationModel>());
			
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
					this.asObjectInOneToManyRelations.Add(relationModel.RelationKey, relationModel);
					this.asPrimaryObjectInRelations.Add(relationModel.RelationKey, relationModel);
				}

				if (this.ObjectType == relationModel.ForeignObjectType)
				{
					this.asForeignObjectInOneToManyRelations.Add(relationModel.RelationKey, relationModel);
					this.asForeignObjectInRelations.Add(relationModel.RelationKey, relationModel);
					this.foreingRelationKeys.Add(relationModel.RelationKey);

					if (!this.asObjectInOneToManyRelations.ContainsKey(relationModel.RelationKey))
						this.asObjectInOneToManyRelations.Add(relationModel.RelationKey, relationModel);
				}
			}

			this.AsObjectInOneToManyRelations = new ObjectRelationModelCollection<OneToManyRelationModel>(this.asObjectInOneToManyRelations);
			this.iasObjectInOneToManyRelations = new ObjectRelationModelCollection<IOneToManyRelationModel>(this.asObjectInOneToManyRelations.AsCustom<IOneToManyRelationModel>());

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
		public ObjectRelationModelCollection<OneToOneRelationModel> AsObjectInOneToOneRelations { get; private set; }
		public ObjectRelationModelCollection<OneToOneRelationModel> AsPrimaryObjectInOneToOneRelations { get; private set; }
		public ObjectRelationModelCollection<OneToOneRelationModel> AsForeignObjectInOneToOneRelations { get; private set; }

		public ObjectRelationModelCollection<OneToManyRelationModel> AsObjectInOneToManyRelations { get; private set; }
		public ObjectRelationModelCollection<OneToManyRelationModel> AsPrimaryObjectInOneToManyRelations { get; private set; }
		public ObjectRelationModelCollection<OneToManyRelationModel> AsForeignObjectInOneToManyRelations { get; private set; }
		
		public ObjectRelationModelCollection<OneToOneOrManyRelationModel> AsPrimaryObjectInRelations { get; private set; }
		public ObjectRelationModelCollection<OneToOneOrManyRelationModel> AsForeignObjectInRelations { get; private set; }

		public ObjectRelationModelCollection<ManyToManyRelationModel> AsObjectInGroupMembership { get; private set; }
		public ObjectRelationModelCollection<ManyToManyRelationModel> AsFirstObjectInManyToManyRelations { get; private set; }
		public ObjectRelationModelCollection<ManyToManyRelationModel> AsSecondObjectInManyToManyRelations { get; private set; }

		internal bool IsObjectTypeSameOrSubclassOf(Type objectType, Type relationModelType)
		{
			return objectType == relationModelType || (this.includeSubclasses && objectType.IsSubclassOf(relationModelType));
		}

		// TODO: Check for what purpose is this needed !!!

		public IOneToOneOrManyRelationModel? GetForeignRelationModel(int propertyIndex, int foreignObjectTableId = 0)
		{
			List<IOneToOneOrManyRelationModel>? itemList;

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

		ISimpleObjectModel IObjectRelationModel.ObjectModel => this.ObjectModel;

		IEnumerable<int> IObjectRelationModel.ForeignObjectKeys => this.foreingRelationKeys;

		IObjectRelationModelCollection<IOneToOneRelationModel> IObjectRelationModel.AsObjectInOneToOneRelations => this.iasObjectInOneToOneRelations;

		IObjectRelationModelCollection<IOneToOneRelationModel> IObjectRelationModel.AsPrimaryObjectInOneToOneRelations => this.iasPrimaryObjectInOneToOneRelations;

		IObjectRelationModelCollection<IOneToOneRelationModel> IObjectRelationModel.AsForeignObjectInOneToOneRelations => this.iasForeignObjectInOneToOneRelations;

		IObjectRelationModelCollection<IOneToManyRelationModel> IObjectRelationModel.AsObjectInOneToManyRelations => this.iasObjectInOneToManyRelations;
		IObjectRelationModelCollection<IOneToManyRelationModel> IObjectRelationModel.AsPrimaryObjectInOneToManyRelations => this.iasPrimaryObjectInOneToManyRelations;

		IObjectRelationModelCollection<IOneToManyRelationModel> IObjectRelationModel.AsForeignObjectInOneToManyRelations => this.iasForeignObjectInOneToManyRelations;

		IObjectRelationModelCollection<IOneToOneOrManyRelationModel> IObjectRelationModel.AsPrimaryObjectInRelarions => this.iasPrimaryObjectInRelarions;

		IObjectRelationModelCollection<IOneToOneOrManyRelationModel> IObjectRelationModel.AsForeignObjectInRelarions => this.iasForeignObjectInRelarions;

		IObjectRelationModelCollection<IManyToManyRelationModel> IObjectRelationModel.AsFirstObjectInManyToManyRelations => this.iasFirstObjectInManyToManyRelations;

		IObjectRelationModelCollection<IManyToManyRelationModel> IObjectRelationModel.AsSecondObjectInManyToManyRelations => this.iasSecondObjectInManyToManyRelations;

		IObjectRelationModelCollection<IManyToManyRelationModel> IObjectRelationModel.AsObjectInGroupMembership => this.iasObjectInGroupMembership;

		IOneToOneOrManyRelationModel? IObjectRelationModel.GetForeignRelationModel(int propertyIndex, int foreignTableId) => this.GetForeignRelationModel(propertyIndex, foreignTableId);
	}

	public interface IObjectRelationModel : IModelElement
    {
        ISimpleObjectModel ObjectModel { get; }
		IEnumerable<int> ForeignObjectKeys { get; }

		IObjectRelationModelCollection<IOneToOneRelationModel> AsObjectInOneToOneRelations { get; }
		IObjectRelationModelCollection<IOneToOneRelationModel> AsPrimaryObjectInOneToOneRelations { get; }
		IObjectRelationModelCollection<IOneToOneRelationModel> AsForeignObjectInOneToOneRelations { get; }

		IObjectRelationModelCollection<IOneToManyRelationModel> AsObjectInOneToManyRelations { get; }
		IObjectRelationModelCollection<IOneToManyRelationModel> AsPrimaryObjectInOneToManyRelations { get; }
		IObjectRelationModelCollection<IOneToManyRelationModel> AsForeignObjectInOneToManyRelations { get; }
		
		IObjectRelationModelCollection<IOneToOneOrManyRelationModel> AsPrimaryObjectInRelarions { get; }
		IObjectRelationModelCollection<IOneToOneOrManyRelationModel> AsForeignObjectInRelarions { get; }
		IObjectRelationModelCollection<IManyToManyRelationModel> AsObjectInGroupMembership { get; }
		IObjectRelationModelCollection<IManyToManyRelationModel> AsFirstObjectInManyToManyRelations { get; }
		IObjectRelationModelCollection<IManyToManyRelationModel> AsSecondObjectInManyToManyRelations { get; }

		// TODO: Remove this
		IOneToOneOrManyRelationModel? GetForeignRelationModel(int propertyIndex, int foreignTableId);
	}
}
