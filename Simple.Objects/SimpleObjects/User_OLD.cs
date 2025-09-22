//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Simple.Objects
//{
//	public partial class User : UserObject
//    {
//        public User(SimpleObjectManager objectManager)
//            : base(objectManager)
//        {
//        }

//		//public string FirstName
//		//{
//		//	get { return this.GetPropertyValue<string>(AdministratorModel.PropertyModel.FirstName); }
//		//	set { this.SetPropertyValue(AdministratorModel.PropertyModel.FirstName, value); }
//		//}

//		//public string LastName
//		//{
//		//	get { return this.GetPropertyValue<string>(AdministratorModel.PropertyModel.LastName); }
//		//	set { this.SetPropertyValue(AdministratorModel.PropertyModel.LastName, value); }
//		//}


//		//public string Username
//		//{
//		//	get { return this.GetPropertyValue<string>(AdministratorModel.PropertyModel.Username); }
//		//	set { this.SetPropertyValue(AdministratorModel.PropertyModel.Username, value); }
//		//}

//		//public string Password
//		//{
//		//	get { return this.GetPropertyValue<string>(AdministratorModel.PropertyModel.Password); }
//		//	set { this.SetPropertyValue(AdministratorModel.PropertyModel.Password, value); }
//		//}

//		//public bool Enabled
//		//{
//		//	get { return this.GetPropertyValue<bool>(AdministratorModel.PropertyModel.Enabled); }
//		//	set { this.SetPropertyValue(AdministratorModel.PropertyModel.Enabled, value); }
//		//}

//		public override string GetName()
//		{
//			string name = this.FirstName ?? String.Empty;

//			name += (name != null && name.Length > 0 && this.LastName != null && this.LastName.Length > 0) ? " " : "";
//			name += this.LastName ?? String.Empty;
			
//			return name;
//		}

//		public IEnumerable<SystemTransaction> Transactions
//		{
//			get { return this.Manager.GetSystemTransactionsByUser(this.Id); }
//		}
//	}
//}
