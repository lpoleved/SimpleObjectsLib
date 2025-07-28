//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Simple.Serialization;

//namespace Simple.Objects.MonitorProtocol
//{
//	public class PropertyModelInfo : SerializableObject, ISerialization
//	{
//		PropertyModel propertyModel = null;

//		public PropertyModelInfo(SerializationReader reader)
//			: base(reader)
//		{
//		}

//		public PropertyModelInfo(PropertyModel propertyModel)
//		{
//			this.propertyModel = propertyModel;
//		}

//		public int Index { get; private set; }
//		public Type PropertyType { get; private set; }
//		public string Name { get; private set; }
//		public string Caption { get; private set; }
//		public string Caption2 { get; private set; }
//		public int PropertyTypeId { get; private set; }
//		public PropertyAccessPolicy AccessPolicy { get; private set; }
//		public Type DatastoreType { get; private set; }
//		public bool IsStorable { get; private set; }
//		public bool IsEncrypted { get; private set; }
//		public bool IsIndexed { get; private set; }
//		public bool IsSerializable { get; private set; }
//		public bool IsSerializationOptimizable { get; private set; }

//		/// <summary>
//		/// If set true this property is not considered when rejecting property changes.
//		/// </summary>
//		public bool AvoidRejectChanges { get; private set; }
//		public AccessModifier AccessModifier { get; private set; }
//		public AccessModifier SetAccessModifier { get; private set; }
//		public bool FirePropertyValueChangeEvent { get; private set; }
//		public bool AddOrRemoveInChangedProperties { get; private set; }
//		public bool AutoGenerateProperty { get; private set; }


//		public override void WriteTo(SerializationWriter writer)
//		{
//			writer.WriteInt32Optimized(this.propertyModel.Index);
//			writer.WriteType(this.propertyModel.PropertyType, fullyQualified: true);
//			writer.WriteString(this.propertyModel.Name);
//			writer.WriteString(this.propertyModel.Caption);
//			writer.WriteString(this.propertyModel.Caption2);
//			writer.WriteInt32Optimized(this.propertyModel.PropertyTypeId);
//			writer.WriteInt32Optimized((int)this.propertyModel.AccessPolicy);
//			writer.WriteType(this.propertyModel.DatastoreType, fullyQualified: true);
//			writer.WriteBoolean(this.propertyModel.IsStorable);
//			writer.WriteBoolean(this.propertyModel.IsEncrypted);
//			writer.WriteBoolean(this.propertyModel.IsIndexed);
//			writer.WriteBoolean(this.propertyModel.IsSerializable);
//			writer.WriteBoolean(this.propertyModel.IsSerializationOptimizable);
//			writer.WriteBoolean(this.propertyModel.AvoidRejectChanges);
//			writer.WriteInt32Optimized((int)this.propertyModel.AccessModifier);
//			writer.WriteInt32Optimized((int)this.propertyModel.SetAccessModifier);
//			writer.WriteBoolean(this.propertyModel.FirePropertyValueChangeEvent);
//			writer.WriteBoolean(this.propertyModel.AddOrRemoveInChangedProperties);
//			writer.WriteBoolean(this.propertyModel.AutoGenerateProperty);
//		}

//		public override void ReadFrom(SerializationReader reader)
//		{
//			int index = reader.ReadInt32Optimized();
//			Type propertyType = reader.ReadType(throwOnError: false);

//			this.propertyModel = new PropertyModel(index, propertyType);
//			this.propertyModel.Name = reader.ReadString();
//			this.propertyModel.Caption = reader.ReadString();
//			this.propertyModel.Caption2 = reader.ReadString();
//			this.propertyModel.PropertyTypeId = reader.ReadInt32Optimized();
//			this.propertyModel.AccessPolicy = (PropertyAccessPolicy)reader.ReadInt32Optimized();
//			this.propertyModel.DatastoreType = reader.ReadType(throwOnError: false);
//			this.propertyModel.IsStorable = reader.ReadBoolean();
//			this.propertyModel.IsEncrypted = reader.ReadBoolean();
//			this.propertyModel.IsIndexed = reader.ReadBoolean();
//			this.propertyModel.IsSerializable = reader.ReadBoolean();
//			this.propertyModel.IsSerializationOptimizable = reader.ReadBoolean();
//			this.propertyModel.AvoidRejectChanges = reader.ReadBoolean();
//			this.propertyModel.AccessModifier = (AccessModifier)reader.ReadInt32Optimized();
//			this.propertyModel.SetAccessModifier = (AccessModifier)reader.ReadInt32Optimized();
//			this.propertyModel.FirePropertyValueChangeEvent = reader.ReadBoolean();
//			this.propertyModel.AddOrRemoveInChangedProperties = reader.ReadBoolean();
//			this.propertyModel.AutoGenerateProperty = reader.ReadBoolean();
//		}
//	}
//}
