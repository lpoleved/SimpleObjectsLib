//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple;
//using Simple.Modeling;

//namespace Simple.Objects
//{
//	public class PropertyModel : PropertyModelBase, IPropertyModel
//    {
//		public PropertyModel(Type propertyType)
//			: this(PropertyModelBase.UnspecifiedIndex, propertyType)
//		{
//		}

//		public PropertyModel(int index, Type propertyType)
//        {
//			this.Index = index;
//			this.PropertyType = propertyType; //typeof(void);
//            this.DatastoreType = propertyType;
//            this.IsStorable = true;
//			this.AccessModifier = AccessModifier.Public;
//			this.SetAccessModifier = AccessModifier.Public;
//			this.FirePropertyValueChangeEvent = true;
//			this.AddOrRemoveInChangedProperties = true;
//			this.AutoGenerateProperty = true;
//        }

//		public string Caption2 { get; set; }
//        public Type DatastoreType { get; set; }
//        public bool IsStorable { get; set; }
//        public bool IsEncrypted { get; set; }

//        /// <summary>
//        /// If set true this property is not considered when rejecting property changes.
//        /// </summary>
//        public bool AvoidRejectChanges { get; set; }
//		public AccessModifier AccessModifier { get; set; }
//		public AccessModifier SetAccessModifier { get; set; }
//		public bool FirePropertyValueChangeEvent { get; set; }
//		public bool AddOrRemoveInChangedProperties { get; set; }
//		public bool AutoGenerateProperty { get; set; }
//		//public object Owner { get; set; }
//    }

//    public interface IPropertyModel : IPropertyModelBase
//	{
//        string Caption2 { get; }
//        Type DatastoreType { get; }
//        bool IsStorable { get; }
//        bool IsEncrypted { get; }

//        /// <summary>
//        /// If set true this property is not considered when rejecting property changes.
//        /// </summary>
//        bool AvoidRejectChanges { get; }
//		AccessModifier AccessModifier { get; }
//		AccessModifier SetAccessModifier { get; }
//		bool FirePropertyValueChangeEvent { get; }
//		bool AddOrRemoveInChangedProperties { get; }
//		bool AutoGenerateProperty { get; }
//    }

//  //  public enum DatastoreFieldType
//  //  {
//  //      Default,
//		//Byte,
//		//Short,
//		//Int,
//		//Long,
//		//Guid,
//		//Decimal,
//		//Float,
//		//DateTime,
//  //      Memo
//  //  }
//}
