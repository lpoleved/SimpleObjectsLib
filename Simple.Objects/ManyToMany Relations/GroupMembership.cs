using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Modeling;

namespace Simple.Objects
{
    // TODO: Remove second constructor + pola stvari netreba

    public partial class GroupMembership : SimpleObject
    {
        private bool groupMmembershipJoinedEventIsRised = false;

		public GroupMembership()
		{
		}

		public GroupMembership(SimpleObjectManager objectManager)
            : base(objectManager)
        {
        }

        public GroupMembership(SimpleObjectManager objectManager, int relationKey, SimpleObject simpleObject1, SimpleObject simpleObject2, ObjectActionContext context = ObjectActionContext.Unspecified, object? requester = null)
            : this(objectManager, relationKey, simpleObject1, simpleObject2, objectManager.DefaultChangeContainer, context, requester)
        {
        }

        public GroupMembership(SimpleObjectManager objectManager, int relationKey, SimpleObject simpleObject1, SimpleObject simpleObject2, ChangeContainer defaultChangeContainer, ObjectActionContext context = ObjectActionContext.Unspecified, object? requester = null)
            : this(objectManager, relationKey, simpleObject1.GetModel().TableInfo.TableId, simpleObject1.Id, simpleObject2.GetModel().TableInfo.TableId, simpleObject2.Id, defaultChangeContainer, context, requester)
        {
   //         var relationModel = this.Manager.GetRelationModel(relationKey) as IManyToManyRelationModel;

			//this.relationKey = relationKey;
			//this.Requester = requester;

			////if (simpleObject1.GetType() == relationModel!.FirstObjectType) // simpleObject1.GetType().IsSameOrSubclassOf(relationModel!.FirstObjectType)
			////{
			//	this.object1TableId = simpleObject1.GetModel().TableInfo.TableId;
			//	this.object1Id = simpleObject1.Id;
			//	this.object2TableId = simpleObject2.GetModel().TableInfo.TableId;
			//	this.object2Id = simpleObject2.Id;
			////}
			////else if (simpleObject2.GetType() == relationModel!.FirstObjectType)
			////{
			////	this.object2TableId = simpleObject1.GetModel().TableInfo.TableId;
			////	this.object2Id = simpleObject1.Id;
			////	this.object1TableId = simpleObject2.GetModel().TableInfo.TableId;
			////	this.object1Id = simpleObject2.Id;
			////}
   ////         else
   ////         {
   ////             throw new Exception();
   ////         }

			//_ = this.Id; // Force key creation

			//this.SimpleObject1.AddGroupMembershipIfInCache(this, changeContainer, requester);
			//this.SimpleObject2.AddGroupMembershipIfInCache(this, changeContainer, requester);
		}

		public GroupMembership(SimpleObjectManager objectManager, int relationKey, int object1TableId, long object1Id, int object2TableId, long object2Id, ObjectActionContext context = ObjectActionContext.Unspecified, object ? requester = null)
            : this(objectManager, relationKey, object1TableId, object1Id, object2TableId, object2Id, objectManager.DefaultChangeContainer, context, requester)
        {
        }

        public GroupMembership(SimpleObjectManager objectManager, int relationKey, int object1TableId, long object1Id, int object2TableId, long object2Id, ChangeContainer? defaultChangeContainer, ObjectActionContext context = ObjectActionContext.Unspecified, object? requester = null)
            : base(objectManager, defaultChangeContainer, context)
        {
            this.RelationKey = relationKey;
            this.Object1TableId = object1TableId;
            this.Object1Id = object1Id;
            this.Object2TableId = object2TableId;
            this.Object2Id = object2Id;
            this.Requester = requester;

            // Force key creation
            _ = this.Id;

            // TODO: Check if this is needed
            this.SimpleObject1.AddGroupMembershipIfInCache(this, defaultChangeContainer, requester);
            this.SimpleObject2.AddGroupMembershipIfInCache(this, defaultChangeContainer, requester);
        }

        public SimpleObject SimpleObject1 => this.Manager.GetObject(this.Object1TableId, this.Object1Id)!;

        public SimpleObject SimpleObject2 => this.Manager.GetObject(this.Object2TableId, this.Object2Id)!;

        protected override void OnBeforeDelete(ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
        {
            if (this.Manager.IsObjectInCache(this.Object1TableId, this.Object1Id))
                this.SimpleObject1.RemoveGroupMembershipFromCache(this, changeContainer, requester);

            if (this.Manager.IsObjectInCache(this.Object2TableId, this.Object2Id))
                this.SimpleObject2.RemoveGroupMembershipFromCache(this, changeContainer, requester);
                    
            this.Manager.GroupMembershipIsDisjoined(this, changeContainer, context, requester);

            base.OnBeforeDelete(changeContainer, context, requester);
        }

        protected override void OnPropertyValueChange(IPropertyModel propertyModel, object? value, object? oldValue, bool isChanged, ChangeContainer? changeContainer, ObjectActionContext context, object? requester)
        {
            base.OnPropertyValueChange(propertyModel, value, oldValue, isChanged, changeContainer, context, requester);

            if (!this.groupMmembershipJoinedEventIsRised && this.IsNew && this.RelationKey > 0 && this.Manager.ContainsObject(this.Object1TableId, this.Object1Id)
                                                                                               && this.Manager.ContainsObject(this.Object2TableId, this.Object2Id))
            {
                this.groupMmembershipJoinedEventIsRised = true;

                if (this.Manager.IsObjectInCache(this.Object1TableId, this.Object1Id))
                   this.SimpleObject1.AddGroupMembershipIfInCache(this, changeContainer, requester);

               if (this.Manager.IsObjectInCache(this.Object2TableId, this.Object2Id))
                   this.SimpleObject2.AddGroupMembershipIfInCache(this, changeContainer, requester);
                    
               this.Manager.GroupMembershipIsJoined(this, changeContainer, context, requester);
            }
        }
    }
}
