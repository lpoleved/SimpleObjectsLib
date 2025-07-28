using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Simple;
using Simple.Modeling;
using Simple.Datastore;

namespace Simple.Objects
{
	public class SystemObjectModel : ISystemObjectModel
	{
		public SystemObjectModel(Type objectType)
		{
			List<int> storablePropertyIndexes = new List<int>();
			//List<IPropertyModel> storablePropertyModels = new List<IPropertyModel>();
			int propertyIndex = 0;

			this.ObjectType = objectType;
			//this.TableName = this.ObjectType.Name + "s";
			this.ObjectKeyPropertyModel = null!;
			this.AutoGenerateKey = true;

			PropertyInfo[] propertiesByReflection = this.ObjectType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			this.PropertyModels = new IPropertyModel[propertiesByReflection.Length];

			foreach (PropertyInfo propertyInfo in propertiesByReflection)
			{
				PropertyModel propertyModel = new PropertyModel(propertyIndex++, propertyInfo.Name, propertyInfo.PropertyType);

				propertyModel.DatastoreFieldName = propertyModel.PropertyName;
				propertyModel.PropertyInfo = propertyInfo;
				this.PropertyModels[propertyModel.PropertyIndex] = propertyModel;

				if (propertyModel.PropertyName != null)
				{
					propertyModel.Caption = propertyModel.PropertyName.InsertSpaceOnUpperChange();
					propertyModel.Caption2 = propertyModel.Caption;
				}

				object[] datastoreTypeAttributes = propertyInfo.GetCustomAttributes(typeof(DatastoreTypeAttribute), true);

				if (datastoreTypeAttributes.Length > 0)
				{
					DatastoreTypeAttribute datastoreTypeAttribute = datastoreTypeAttributes[0] as DatastoreTypeAttribute;
					
					propertyModel.DatastoreType = datastoreTypeAttribute.DatastoreType;
				}

				object[] objectKeyAttributes = propertyInfo.GetCustomAttributes(typeof(ObjectKeyAttribute), true);

				if (objectKeyAttributes.Length > 0)
				{
					if (this.ObjectKeyPropertyModel != null)
						throw new Exception(String.Format("Duplicate ObjectKey attribute. SystemObject type: {0}, property names: {1} & {2}.", this.ObjectType.Name, this.ObjectKeyPropertyModel.PropertyName, propertyInfo.Name));

					this.ObjectKeyPropertyModel = propertyModel;
				}

				object[] storableAttributes = propertyInfo.GetCustomAttributes(typeof(NonStorableAttribute), true);

				if (storableAttributes.Length > 0)
					propertyModel.IsStorable = false;
				else 
					storablePropertyIndexes.Add(propertyModel.PropertyIndex);
			}

			this.StorablePropertyIndexes = storablePropertyIndexes.ToArray();
			//this.StorablePropertyModels = storablePropertyModels.ToArray();
		}

		Type ObjectType { get; set; }
		public PropertyModel ObjectKeyPropertyModel { get; set; }
		
		public TableInfo TableInfo { get; set; }
		//public string TableName { get; set; }
		public bool AutoGenerateKey { get; set; }
		public bool ReuseObjectKeys { get; set; }
		public int[] StorablePropertyIndexes { get; private set; }
		//public IPropertyModel[] StorablePropertyModels { get; private set; }
		public IPropertyModel[] PropertyModels { get; private set; }
		//IPropertyModel[] PropertyModelSequence { get; set; }

		public IPropertyModel GetPropertyModel(int propertyIndex)
		{
			return this.PropertyModels[propertyIndex];
		}
	}

	public interface ISystemObjectModel //: ISimpleObjectModel
	{
		PropertyModel ObjectKeyPropertyModel { get; }
		TableInfo TableInfo { get; }
		//string TableName { get; }
		bool AutoGenerateKey { get; }
		bool ReuseObjectKeys { get; }
		int[] StorablePropertyIndexes { get; }
		//IPropertyModel[] StorablePropertyModels { get; }
		IPropertyModel[] PropertyModels { get; }
		IPropertyModel GetPropertyModel(int propertyIndex);
	}
}
