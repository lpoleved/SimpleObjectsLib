using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Modeling;
using Simple.Serialization;

namespace Simple.Objects
{
	public class TransactionActionInfo : ISequenceSerializable
	{
		private IList<PropertyIndexValuePair>? propertyIndexValueList = null;
		//internal PropertyIndexValuePair[]? propertyIndexValueArray;

		public TransactionActionInfo()
		{
		}

		public TransactionActionInfo(int tableId, long objectId, TransactionActionType actionType, IList<PropertyIndexValuePair>? propertyIndexValueList)
			//: this(tableId, objectId, actionType, propertyIndexValues: null)
		{
		//	this.propertyIndexValueList = propertyIndexValueList;
		//}


		//public TransactionActionInfo(int tableId, long objectId, TransactionActionType actionType, PropertyIndexValuePair[]? propertyIndexValues)
		//{
			this.TableId = tableId;
			this.ObjectId = objectId;
			this.ActionType = actionType;
			this.propertyIndexValueList = propertyIndexValueList;
		}

		public int TableId { get; protected set; }
		public long ObjectId { get; protected internal set; }

		public SimpleObject? SimpleObject { get; set; } = null;
		public RelationListInfo? RelationInfo { get; set; } = null;

		public TransactionActionType ActionType { get; private set; }

		public IList<PropertyIndexValuePair>? PropertyIndexValues => this.propertyIndexValueList; // (this.propertyIndexValueList != null) ? this.propertyIndexValueList : this.propertyIndexValueArray;

		//public IEnumerable<int> PropertyIndexes { get; private set; }
		//public IEnumerable<object?> PropertyValues { get; private set; }

		// TODO: This serialalization logic should be removed from here!!!!!
		public virtual void WriteTo(ref SequenceWriter writer, ServerObjectModelInfo objectModel)
		{
			writer.WriteInt32Optimized(this.TableId);
			writer.WriteInt64Optimized(this.ObjectId);

			if (this.ActionType == TransactionActionType.Insert)
			{
				writer.WriteBoolean(true);
			}
			else
			{
				writer.WriteBoolean(false);
				writer.WriteBoolean(this.ActionType == TransactionActionType.Update);
			}

			IEnumerable<PropertyIndexValuePair>? propertyIndexValues = this.PropertyIndexValues;

			if (propertyIndexValues != null)
			{
				writer.WriteInt32Optimized(propertyIndexValues.Count());

				foreach (var item in propertyIndexValues)
				{
					ServerPropertyInfo propertyModel = objectModel.GetPropertyInfo(item.PropertyIndex);
					object? propertyValue = this.GetPropertyValue(item.PropertyValue);
					int propertyTypeId = this.GetPropertyTypeId(propertyModel, propertyValue);

					writer.WriteInt32Optimized(item.PropertyIndex);
					this.WritePropertyTypeId(ref writer, propertyTypeId);
					this.WriteIsPropertySeriaizationOptimizable(ref writer, propertyModel.IsSerializationOptimizable);

					if (propertyModel.IsSerializationOptimizable)
						writer.WriteOptimized(propertyTypeId, propertyValue);
					else
						writer.Write(propertyTypeId, propertyValue);
				}
			}
			else
			{
				writer.WriteInt32Optimized(0);
			}
		}

		protected virtual int GetPropertyTypeId(IServerPropertyInfo propertyModel, object? propertyValue) => propertyModel.PropertyTypeId;

		protected virtual object? GetPropertyValue(object? value) => value;

		protected virtual void WritePropertyTypeId(ref SequenceWriter writer, int propertyTypeId) { }

		protected virtual void WriteIsPropertySeriaizationOptimizable(ref SequenceWriter writer, bool isSeriazable) { }

		public virtual void ReadFrom(ref SequenceReader reader, Func<int, ServerObjectModelInfo?> getServerObjectModel)
		{
			IList<PropertyIndexValuePair> propertyValues;

			this.TableId = reader.ReadInt32Optimized();
			this.ObjectId = reader.ReadInt64Optimized();
			this.ActionType = TransactionActionType.Delete;

			if (reader.ReadBoolean())
				this.ActionType = TransactionActionType.Insert;
			else if (reader.ReadBoolean())
				this.ActionType = TransactionActionType.Update;

			//if (this.ActionType != TransactionActionType.Delete)
			//{
				int propertyCount = reader.ReadInt32Optimized();
				
				propertyValues = new List<PropertyIndexValuePair>(propertyCount);

				for (int i = 0; i < propertyCount; i++)
				{
					int propertyIndex = reader.ReadInt32Optimized();
					//var propertyModel = objectModel.GetPropertyInfo(propertyIndex);
					int propertyTypeId = this.ReadPropertyTypeId(ref reader, getServerObjectModel, propertyIndex);

					//int propertyTypeId = this.GetPropertyTypeId(propertyModel);
					bool isSerializationOptimizable = this.ReadIsProperySerializationOptimizable(ref reader, getServerObjectModel, propertyIndex); // propertyModel.IsSerializationOptimizable;
					object? propertyValue;

					if (isSerializationOptimizable)
						propertyValue = reader.ReadOptimized(propertyTypeId);
					else
						propertyValue = reader.Read(propertyTypeId);

					propertyValues.Add(new PropertyIndexValuePair(propertyIndex, propertyValue));
				}
			//}
			//else
			//{
			//	propertyValues = new PropertyIndexValuePair[0];
			//}

			this.propertyIndexValueList = propertyValues;
		}

		protected virtual int ReadPropertyTypeId(ref SequenceReader reader, Func<int, ServerObjectModelInfo?> getServerObjectModel, int propertyIndex) => getServerObjectModel(this.TableId)![propertyIndex].PropertyTypeId;
		protected virtual bool ReadIsProperySerializationOptimizable(ref SequenceReader reader, Func<int, ServerObjectModelInfo?> getServerObjectModel, int propertyIndex) => getServerObjectModel(this.TableId)![propertyIndex].IsSerializationOptimizable;

		public virtual int GetBufferCapacity() => this.PropertyIndexValues?.Count() ?? 0 * 30 + 8;

		void ISequenceWritable.WriteTo(ref SequenceWriter writer, object? context)
		{
			if (context is ServerObjectModelInfo objectModel)
				this.WriteTo(ref writer, objectModel);
		}

		void ISequenceReadable.ReadFrom(ref SequenceReader reader, object? context)
		{
			if (context is Func<int, ServerObjectModelInfo?> getServerObjectModel)
				this.ReadFrom(ref reader, getServerObjectModel);
		}
	}
}
