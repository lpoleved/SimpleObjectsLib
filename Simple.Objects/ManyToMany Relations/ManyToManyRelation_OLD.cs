//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Simple.Objects
//{
//    public class ManyToManyRelation
//    {
//        public ManyToManyRelation(SimpleObjectManager manager, int relationKey, string name)
//        {
//            this.RelationKey = relationKey;
//            this.Name = name;
//            this.GroupMemebershipElements = new SimpleObjectCollection_OLD<GroupMembershipElement>(manager);
//        }

//        public int RelationKey { get; private set; }
//        public string Name { get; private set; }
//        public SimpleObjectCollection_OLD<GroupMembershipElement> GroupMemebershipElements { get; private set; }
//    }
//}
