//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Simple.Objects
//{
//    public class UserCollection : GenericObjectCollectionBase<SimpleObjectManager, User>
//    {
//        private User systemAdmin = null;
//        private string systemAdminUsername = String.Empty;
//        private string administratorUsername = String.Empty;

//        public UserCollection(SimpleObjectManager objectManager, string systemAdminUsername)
//            : base(objectManager)
//        {
//            this.systemAdminUsername = systemAdminUsername;
//        }

//        public User SystemAdmin
//        {
//            get
//            {
//                if (this.systemAdmin == null)
//                    this.systemAdmin = this.Collection.FirstOrDefault(administrator => administrator.Username == this.systemAdminUsername);

//                return this.systemAdmin;
//            }
//        }

//        public int Count => this.Collection.Count;

//        protected override List<User> CreateGenericList()
//        {
//            return new List<User>() { SystemAdmin };
//        }
//    }
//}
