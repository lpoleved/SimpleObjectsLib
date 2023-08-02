using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Modeling;

namespace Simple.Objects
{
	public class GroupMembershipModel : SimpleObjectModel<GroupMembership, GroupMembershipPropertyModel, GroupMembershipModel>
    {
		public GroupMembershipModel()	: base(SystemTables.GroupMembership)

		{
			this.MustHaveAtLeastOneGraphElement = false;

			this.UpdateValidationRules.Add(new ValidationRuleExistance(PropertyModel.RelationKey, (simpleObject, transaction) => (simpleObject as GroupMembership).RelationKey >= 0, "GroupMembershipElement must have RelationKey specified."));
			this.UpdateValidationRules.Add(new ValidationRuleExistance(PropertyModel.Object1Id, (simpleObject, transaction) => (simpleObject as GroupMembership).SimpleObject1 != null, "GroupMembershipElement must have SimpleObject1 specified."));
			this.UpdateValidationRules.Add(new ValidationRuleExistance(PropertyModel.Object2Id, (simpleObject, transaction) => (simpleObject as GroupMembership).SimpleObject2 != null, "GroupMembershipElement must have SimpleObject2 specified."));
		}
	}

  //  public class ManyToManyRelationElementPropertyModel_OLD : SimpleObjectPropertyModel_OLD
  //  {
  //      public readonly PropertyModel RelationKey			= new PropertyModel(10, typeof(int))  { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Public,   SetAccessModifier = AccessModifier.Private, DatastoreType = typeof(short) };
  //      public readonly PropertyModel SimpleObject1TableId  = new PropertyModel(11, typeof(int))  { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, SetAccessModifier = AccessModifier.Private, DatastoreType = typeof(short), IsMemberOfSerializationSequence = false };
		//public readonly PropertyModel SimpleObject1Guid     = new PropertyModel(12, typeof(Guid)) { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, SetAccessModifier = AccessModifier.Private, IsRelationKey = true };
		//public readonly PropertyModel SimpleObject2TableId  = new PropertyModel(13, typeof(int))  { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, SetAccessModifier = AccessModifier.Private, DatastoreType = typeof(short), IsMemberOfSerializationSequence = false };
		//public readonly PropertyModel SimpleObject2Guid     = new PropertyModel(14, typeof(Guid)) { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, SetAccessModifier = AccessModifier.Private, IsRelationKey = true };
  //  }

	public class GroupMembershipPropertyModel : SimpleObjectPropertyModelBase
	{
		public PM RelationKey    = new PM<int> (1) { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Public,   SetAccessModifier = AccessModifier.Private, DatastoreType = typeof(short) };
		public PM Object1TableId = new PM<int> (2) { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, SetAccessModifier = AccessModifier.Private, IsRelationTableId = true, DatastoreType = typeof(short) };
		public PM Object1Id		 = new PM<long>(3) { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, SetAccessModifier = AccessModifier.Private, IsRelationObjectId = true };
		public PM Object2TableId = new PM<int> (4) { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, SetAccessModifier = AccessModifier.Private, IsRelationTableId = true, DatastoreType = typeof(short) };
		public PM Object2Id      = new PM<long>(5) { AccessPolicy = PropertyAccessPolicy.ReadOnly, GetAccessModifier = AccessModifier.Internal, SetAccessModifier = AccessModifier.Private, IsRelationObjectId = true };
	}
}
