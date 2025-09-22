//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using Simple.Collections;
//using Simple.Modeling;

//namespace Simple.Objects
//{
//	public class UserModel : UserModelBase<User, UserPropertyModel, UserModel>
//	{
//	}

//	public abstract class UserModelBase<TSimpleObject, TSimpleObjectPropertyModel, TObjectModel> : SimpleObjectModel<TSimpleObject, TSimpleObjectPropertyModel, TObjectModel>, ISimpleObjectModel
//		where TSimpleObject : User
//		where TSimpleObjectPropertyModel : UserPropertyModel, new()
//		where TObjectModel : UserModelBase<TSimpleObject, TSimpleObjectPropertyModel, TObjectModel>, new()
//	{
//		public UserModelBase() : base(SimpleObjectTables.Users)
//		{
//			this.ValidationRules.Add(new ValidationRuleExistance(PropertyModel.Username));
//			this.ValidationRules.Add(new ValidationRuleUnique(PropertyModel.Username, simpleObject => simpleObject.Manager.Users.Collection));
//			this.ValidationRules.Add(new ValidationRuleExistance(PropertyModel.Password));
//		}
//	}

//	//public class UserPropertyModel_OLD : SimpleObjectPropertyModel_OLD
//	//   {
//	//	public readonly PropertyModel Description = new PropertyModel( 1, typeof(string));
//	//	public readonly PropertyModel FirstName   = new PropertyModel(10, typeof(string)) { Caption = "First Name" };
//	//       public readonly PropertyModel LastName    = new PropertyModel(11, typeof(string)) { Caption = "Last Name" };
//	//       public readonly PropertyModel Username    = new PropertyModel(13, typeof(string));
//	//       public readonly PropertyModel Password    = new PropertyModel(14, typeof(string)) { IsEncrypted = true };
//	//	public readonly PropertyModel Location    = new PropertyModel(15, typeof(string));
//	//	public readonly PropertyModel Enabled     = new PropertyModel(16, typeof(bool));
//	//}

//	public class UserPropertyModel : SimpleObjectPropertyModelBase
//	{
//		public PM Description = new PM<string>(1);
//		public PM FirstName = new PM<string>(2) { Caption = "First Name" };
//		public PM LastName = new PM<string>(3) { Caption = "Last Name" };
//		public PM Username = new PM<string>(4);
//		public PM Password = new PM<string>(5) { IsEncrypted = true };
//		public PM Location = new PM<string>(6);
//		public PM Enabled = new PM<bool>(7);
//	}
//}
