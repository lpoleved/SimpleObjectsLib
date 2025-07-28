using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple;
using Simple.Modeling;
using Simple.Serialization;

namespace Simple.Objects
{
	public class ServerPropertyModelInfo
	{
		private PropertyModel clientPropertyModel;
		private PropertyModel serverPropertyModel;

		public ServerPropertyModelInfo(PropertyModel clientPropertyModel, int propertyIndex, int propertyTypeId, bool isSerializationOptimizable)
		{
			this.clientPropertyModel = clientPropertyModel;
			Type propertyType = PropertyTypes.GetPropertyType(propertyTypeId);

			if (clientPropertyModel != null)
			{
				this.serverPropertyModel = clientPropertyModel.Clone();
				this.serverPropertyModel.PropertyTypeId = propertyTypeId;
				this.serverPropertyModel.PropertyType = propertyType;
			}
			else
			{
				this.serverPropertyModel = new PropertyModel(propertyIndex, propertyType);
			}

			this.serverPropertyModel.IsSerializationOptimizable = isSerializationOptimizable;
			//if (clientPropertyModel != null)
			//{
			//	this.IsObjectTypeDifferent = (clientPropertyModel.PropertyType != this.PropertyType);
			//}
			//else
			//{
			//	this.IsObjectTypeDifferent = true;
			//}
		}

		public IPropertyModel ClientPropertyModel
		{
			get { return this.clientPropertyModel; }
		}

		public IPropertyModel ServerPropertyModel
		{
			get { return this.serverPropertyModel; }
		}

		//public int PropertyIndex { get; private set; }
		//public Type PropertyType { get; private set; }
		//public bool IsSerializationOptimizable { get; private set; }

		////public PropertyModel ServerPropertyModel { get; private set; }

		//public bool IsObjectTypeDifferent { get; private set; }
	}
}
