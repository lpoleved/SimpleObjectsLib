//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple;

//namespace Simple.Objects
//{
//	// TODO: Remove second constructor + pola stvari netreba

//	public partial class GroupMembershipElement : SimpleObject
//	{
//		private ManyToManyRelation manyToManyRelation = null;
//		//private SimpleObject simpleObject1 = null; 
//		//private SimpleObject simpleObject2 = null;

//		public GroupMembershipElement(SimpleObjectManager objectManager)
//			: base(objectManager)
//		{
//		}

//		public GroupMembershipElement(SimpleObjectManager objectManager, int relationKey, ManyToManyRelation manyToManyRelation, SimpleObject simpleObject1, SimpleObject simpleObject2, ChangeContainer changeContainer)
//			: base(objectManager, changeContainer)
//		{
//			// Force key creation first.
//			long key = this.Id;

//			this.RelationKey = relationKey;
//			this.ManyToManyRelation = manyToManyRelation;

//			//this.simpleObject1 = simpleObject1;
//			this.Object1TableId = simpleObject1.GetModel().TableInfo.TableId;
//			this.Object1Id = simpleObject1.Id;
//			//this.Object1CreatorServerId = simpleObject1.Key.CreatorServerId;

//			//this.simpleObject2 = simpleObject2;
//			this.Object2TableId = simpleObject2.GetModel().TableInfo.TableId;
//			this.Object2Id = simpleObject2.Id;
//			//this.Object2CreatorServerId = simpleObject2.Key.CreatorServerId;
//		}

//		internal IManyToManyCollectionControl ManyToManyCollection1 { get; set; }
//		internal IManyToManyCollectionControl ManyToManyCollection2 { get; set; }

//		public ManyToManyRelation ManyToManyRelation
//		{
//			get { return this.manyToManyRelation; }
//			set
//			{
//				if (this.IsNew)
//				{
//					if (this.ManyToManyRelation != null)
//						this.ManyToManyRelation.GroupMemebershipElements.ListRemove(this);

//					this.manyToManyRelation = value;
//					this.RelationKey = this.manyToManyRelation.RelationKey;

//					if (this.ManyToManyRelation != null && !this.ManyToManyRelation.GroupMemebershipElements.Contains(this))
//						this.ManyToManyRelation.GroupMemebershipElements.ListAdd(this);
//				}
//				else
//				{
//					throw new ArgumentException("You can set GroupMembershipElement's ManyToManyRelation property only when IsNew = true.");
//				}
//			}
//		}

//		public SimpleObject SimpleObject1 => this.Manager.GetObject(this.Object1TableId, this.Object1Id);
//		//{
//		//    get 
//		//    {
//		//        if (this.simpleObject1 == null)
//		//            this.simpleObject1 = this.Manager.GetObject(this.Object1TableId, this.Object1Id);

//		//        return simpleObject1;
//		//    }
//		//}

//		public SimpleObject SimpleObject2 => this.Manager.GetObject(this.Object2TableId, this.Object2Id);
//		//{
//		//    get 
//		//    {
//		//        if (this.simpleObject2 == null)
//		//            this.simpleObject2 = this.Manager.GetObject(this.Object2TableId, this.Object2Id);

//		//        return this.simpleObject2;
//		//    }
//		//}

//		//public SimpleObjectKey ObjectKey1
//		//{
//		//    get 
//		//    {
//		//        if (this.simpleObject1 == null)
//		//        {
//		//            this.simpleObject1 = new SimpleObjectKey(this.Object1TableId, this.Object1ObjectId, this.Object1CreatorServerId);
//		//        }

//		//        return this.simpleObject1;
//		//    }
//		//}

//		//public SimpleObjectKey ObjectKey2
//		//{
//		//    get 
//		//    {
//		//        if (this.simpleObject2 == null)
//		//        {
//		//            this.simpleObject2 = new SimpleObjectKey(this.Object2TableId, this.Object2ObjectId, this.Object2CreatorServerId);
//		//        }

//		//        return this.simpleObject2;
//		//    }
//		//}

//		//internal int ManyToManyRelationKey
//		//{
//		//	get { return this.GetPropertyValue<int>(ManyToManyRelationElementModel.PropertyModel.ManyToManyRelationKey, 0); }
//		//	set { this.SetPropertyValueInternal(ManyToManyRelationElementModel.PropertyModel.ManyToManyRelationKey.Index, value, false, true, this); }
//		//}

//		//internal int Object1TableId
//		//{
//		//	get { return this.GetPropertyValue<int>(ManyToManyRelationElementModel.PropertyModel.Object1TableId, 0); }
//		//	private set { this.SetPropertyValueInternal(ManyToManyRelationElementModel.PropertyModel.Object1TableId.Index, value, false, true, this); }
//		//}

//		//internal Guid Object1Guid
//		//{
//		//	get { return this.GetPropertyValue<Guid>(ManyToManyRelationElementModel.PropertyModel.Object1Guid, Guid.Empty); }
//		//	private set { this.SetPropertyValueInternal(ManyToManyRelationElementModel.PropertyModel.Object1Guid.Index, value, false, true, this); }
//		//}

//		////internal int Object1CreatorServerId
//		////{
//		////	get { return this.GetPropertyValue<int>(ManyToManyRelationElementModel.PropertyModel.Object1CreatorServerId, 0); }
//		////	private set { this.SetPropertyValueInternal(ManyToManyRelationElementModel.PropertyModel.Object1CreatorServerId.Name, value, false, true, this); }
//		////}

//		//internal int Object2TableId
//		//{
//		//	get { return this.GetPropertyValue<int>(ManyToManyRelationElementModel.PropertyModel.Object2TableId, 0); }
//		//	private set { this.SetPropertyValueInternal(ManyToManyRelationElementModel.PropertyModel.Object2TableId.Index, value, false, true, this); }
//		//}

//		//internal Guid Object2Guid
//		//{
//		//	get { return this.GetPropertyValue<Guid>(ManyToManyRelationElementModel.PropertyModel.Object2Guid, Guid.Empty); }
//		//	private set { this.SetPropertyValueInternal(ManyToManyRelationElementModel.PropertyModel.Object2Guid.Index, value, false, true, this); }
//		//}

//		////internal int Object2CreatorServerId
//		////{
//		////	get { return this.GetPropertyValue<int>(ManyToManyRelationElementModel.PropertyModel.Object2CreatorServerId, 0); }
//		////	private set { this.SetPropertyValueInternal(ManyToManyRelationElementModel.PropertyModel.Object2CreatorServerId.Name, value, false, true, this); }
//		////}

//		// TODO: Posibly remove this. It will be saved when object requre saving in ChangeContainer.

//		public void SaveIfNeeded()
//		{
//			if (this.IsNew)
//			{
//				//bool canSave = this.Manager.ObjectCache.IsObjectInCache(this.ObjectKey1) ? !this.SimpleObject1.IsNew : true;
//				//canSave &= this.Manager.ObjectCache.IsObjectInCache(this.ObjectKey2) ? !this.SimpleObject2.IsNew : true;

//				bool canSave = !this.SimpleObject1.IsNew && !this.SimpleObject2.IsNew;

//				if (canSave)
//					this.Manager.CommitChanges();
//			}
//		}

//		protected override void OnBeforeDelete(ChangeContainer changeContainer, object requester)
//		{
//			if (this.ManyToManyRelation != null)
//				this.ManyToManyRelation.GroupMemebershipElements.ListRemove(this);

//			if (this.ManyToManyCollection1 != null)
//				this.ManyToManyCollection1.RemoveGroupMembershipElement(this);

//			if (this.ManyToManyCollection2 != null)
//				this.ManyToManyCollection2.RemoveGroupMembershipElement(this);

//			base.OnBeforeDelete(changeContainer, requester);
//		}
//	}
//}