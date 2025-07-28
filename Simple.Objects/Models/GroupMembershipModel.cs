using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Simple.Modeling;

namespace Simple.Objects
{
	public class GroupMembershipModel : SimpleObjectModel<GroupMembership, GroupMembershipPropertyModel, GroupMembershipModel>
    {
		public GroupMembershipModel()	: base(SystemTablesBase.GroupMembership)

		{
			this.MustHaveAtLeastOneGraphElement = false;

			this.UpdateValidationRules.Add(new ValidationRuleExistance(PropertyModel.RelationKey, (simpleObject, transaction) => (simpleObject as GroupMembership)?.RelationKey >= 0, "GroupMembershipElement must have RelationKey specified."));
			this.UpdateValidationRules.Add(new ValidationRuleExistance(PropertyModel.Object1Id, (simpleObject, transaction) => (simpleObject as GroupMembership)?.SimpleObject1 != null, "GroupMembershipElement must have SimpleObject1 specified."));
			this.UpdateValidationRules.Add(new ValidationRuleExistance(PropertyModel.Object2Id, (simpleObject, transaction) => (simpleObject as GroupMembership)?.SimpleObject2 != null, "GroupMembershipElement must have SimpleObject2 specified."));
			this.UpdateValidationRules.Add(new ValidationRuleExistance(PropertyModel.Object2Id, ValidateUnique, "Group membership element is duplicated."));
		}

		/// <summary>
		/// Validate GroupMembership to prevents duplicated group membership
		/// </summary>
		/// <param name="simpleObject"></param>
		/// <param name="transactionRequests"></param>
		/// <returns></returns>
		public static bool ValidateUnique(SimpleObject simpleObject, IDictionary<SimpleObject, TransactionRequestAction> transactionRequests) 
		{
			if (simpleObject is GroupMembership groupMembership && (groupMembership.Manager.GetObjectCache(GroupMembershipModel.TableId) is ServerObjectCache objectCache))
			{
				var collection = objectCache.Select<GroupMembership>(gm => gm.RelationKey == groupMembership.RelationKey &&
																   ((gm.Object1TableId == groupMembership.Object1TableId && gm.Object1Id == groupMembership.Object1Id && gm.Object2TableId == groupMembership.Object2TableId && gm.Object2Id == groupMembership.Object2Id) ||
								 								    (gm.Object1TableId == groupMembership.Object2TableId && gm.Object1Id == groupMembership.Object2Id && gm.Object2TableId == groupMembership.Object1TableId && gm.Object2Id == groupMembership.Object1Id)));

				if (collection.Count > 1)
					return false;
				
				
				
				//var collection = groupMembership.SimpleObject1.GetGroupMemberCollection(groupMembership.RelationKey);

				//if (collection != null)
				//{
				//	int count = 0;
					
				//	foreach (var item in collection)
				//	{
				//		if (item.GetModel().TableInfo.TableId == groupMembership.Object2TableId && item.Id == groupMembership.Object2Id)
				//			count++;

				//		if (count > 1)
				//			return false;

				//		//else if ((item.GetModel().TableInfo.TableId == groupMembership.Id) ||
				//		//		 (item.Object2TableId == groupMembership.GetModel().TableInfo.TableId && item.Object2Id == groupMembership.Id))
				//		//	return false;
				//	}
				//}
			}

			return true;
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
