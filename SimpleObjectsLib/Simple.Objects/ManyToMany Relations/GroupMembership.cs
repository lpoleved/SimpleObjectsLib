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
        private bool groupMmembershipJonedEventIsRised = false;

        public GroupMembership(SimpleObjectManager objectManager)
            : base(objectManager)
        {
        }

        public GroupMembership(SimpleObjectManager objectManager, int relationKey, SimpleObject simpleObject1, SimpleObject simpleObject2, object? requester = null)
            : this(objectManager, relationKey, simpleObject1, simpleObject2, objectManager.DefaultChangeContainer, requester)
        {
        }

        public GroupMembership(SimpleObjectManager objectManager, int relationKey, SimpleObject simpleObject1, SimpleObject simpleObject2, ChangeContainer changeContainer, object? requester = null)
            : this(objectManager, relationKey, simpleObject1.GetModel().TableInfo.TableId, simpleObject1.Id, simpleObject2.GetModel().TableInfo.TableId, simpleObject2.Id, changeContainer, requester)
        {
        }

        public GroupMembership(SimpleObjectManager objectManager, int relationKey, int object1TableId, long object1Id, int object2TableId, long object2Id, object? requester = null)
            : this(objectManager, relationKey, object1TableId, object1Id, object2TableId, object2Id, objectManager.DefaultChangeContainer, requester)
        {
        }

        public GroupMembership(SimpleObjectManager objectManager, int relationKey, int object1TableId, long object1Id, int object2TableId, long object2Id, ChangeContainer changeContainer, object? requester = null)
            : base(objectManager, changeContainer)
        {
            this.relationKey = relationKey;
            this.object1TableId = object1TableId;
            this.object1Id = object1Id;
            this.object2TableId = object2TableId;
            this.object2Id = object2Id;
            this.Requester = requester;

            // Force key creation
            _ = this.Id;

            //this.SimpleObject1.AddToManyToManyObjectCollectionIfInCache(this);
            //this.SimpleObject2.AddToManyToManyObjectCollectionIfInCache(this);
        }

        public SimpleObject SimpleObject1 => this.Manager.GetObject(this.Object1TableId, this.Object1Id)!;

        public SimpleObject SimpleObject2 => this.Manager.GetObject(this.Object2TableId, this.Object2Id)!;

        protected override void OnBeforeDelete(ChangeContainer changeContainer, object? requester)
        {
            if (this.Manager.IsObjectInCache(this.Object1TableId, this.Object1Id))
                this.SimpleObject1.RemoveGroupMembershipIfInCache(this);

            if (this.Manager.IsObjectInCache(this.Object2TableId, this.Object2Id))
                this.SimpleObject2.RemoveGroupMembershipIfInCache(this);
                    
            this.Manager.GroupMembershipIsDisjoined(this, changeContainer, requester);

            base.OnBeforeDelete(changeContainer, requester);
        }

        protected override void OnPropertyValueChange(IPropertyModel propertyModel, object? value, object? oldValue, bool isChanged, bool isSaveable, ChangeContainer changeContainer, object? requester)
        {
            base.OnPropertyValueChange(propertyModel, value, oldValue, isChanged, isSaveable, changeContainer, requester);

            if (!this.groupMmembershipJonedEventIsRised && this.IsNew && this.RelationKey > 0 && this.Manager.ContainsObject(this.Object1TableId, this.Object1Id)
                                                                                              && this.Manager.ContainsObject(this.Object2TableId, this.Object2Id))
            {
                this.groupMmembershipJonedEventIsRised = true;

                if (this.Manager.IsObjectInCache(this.Object1TableId, this.Object1Id))
                   this.SimpleObject1.AddGroupMembershipIfInCache(this);

               if (this.Manager.IsObjectInCache(this.Object2TableId, this.Object2Id))
                   this.SimpleObject2.AddGroupMembershipIfInCache(this);
                    
               this.Manager.GroupMembershipIsJoined(this, changeContainer, requester);
            }
        }
    }
}
