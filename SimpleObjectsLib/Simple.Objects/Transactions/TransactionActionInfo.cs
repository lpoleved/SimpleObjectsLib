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
		private List<PropertyIndexValuePair>? propertyIndexValueList = null;
		internal PropertyIndexValuePair[]? propertyIndexValueArray;

		public TransactionActionInfo()
		{
		}

		public TransactionActionInfo(int tableId, long objectId, TransactionActionType actionType, List<PropertyIndexValuePair>? propertyIndexValueList)
			: this(tableId, objectId, actionType, propertyIndexValues: null)
		{
			this.propertyIndexValueList = propertyIndexValueList;
		}


		public TransactionActionInfo(int tableId, long objectId, TransactionActionType actionType, PropertyIndexValuePair[]? propertyIndexValues)
		{
			this.TableId = tableId;
			this.ObjectId = objectId;
			this.ActionType = actionType;
			this.propertyIndexValueArray = propertyIndexValues;
		}

		public int TableId { get; private set; }
		public long ObjectId { get; internal set; }
		public TransactionActionType ActionType { get; private set; }

		public IEnumerable<PropertyIndexValuePair>? PropertyIndexValues => (this.propertyIndexValueList != null) ? this.propertyIndexValueList as IEnumerable<PropertyIndexValuePair> : this.propertyIndexValueArray;

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

			if (this.ActionType != TransactionActionType.Delete)
			{
				IEnumerable<PropertyIndexValuePair>? propertyIndexValues = this.PropertyIndexValues;

				if (propertyIndexValues != null)
				{
					writer.WriteInt32Optimized(propertyIndexValues.Count());

					foreach (var item in propertyIndexValues)
					{
						ServerPropertyInfo propertyModel = objectModel.GetPropertyInfo(item.PropertyIndex);
						int propertyTypeId = this.GetPropertyTypeId(propertyModel);

						writer.WriteInt32Optimized(item.PropertyIndex);
						this.WritePropertyTypeId(ref writer, propertyTypeId);
						this.WriteIsPropertySeriaizationOptimizable(ref writer, propertyModel.IsSerializationOptimizable);

						if (propertyModel.IsSerializationOptimizable)
							writer.WriteOptimized(propertyTypeId, item.PropertyValue);
						else
							writer.Write(propertyTypeId, item.PropertyValue);
					}
				}
				else
				{
					writer.WriteInt32Optimized(0);
				}
			}
		}

		protected virtual void WritePropertyTypeId(ref SequenceWriter writer, int propertyTypeId) { }

		protected virtual void WriteIsPropertySeriaizationOptimizable(ref SequenceWriter writer, bool isSeriazable) { }

		public virtual void ReadFrom(ref SequenceReader reader, Func<int, ServerObjectModelInfo?> getServerObjectModel)
		{
			this.TableId = reader.ReadInt32Optimized();
			this.ObjectId = reader.ReadInt64Optimized();
			this.ActionType = TransactionActionType.Delete;
			PropertyIndexValuePair[] propertyValues;

			if (reader.ReadBoolean())
				this.ActionType = TransactionActionType.Insert;
			else if (reader.ReadBoolean())
				this.ActionType = TransactionActionType.Update;

			if (this.ActionType != TransactionActionType.Delete)
			{
				int propertyCount = reader.ReadInt32Optimized();
				propertyValues = new PropertyIndexValuePair[propertyCount];

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

					propertyValues[i] = new PropertyIndexValuePair(propertyIndex, propertyValue);
				}
			}
			else
			{
				propertyValues = new PropertyIndexValuePair[0];
			}

			this.propertyIndexValueArray = propertyValues;
		}

		protected virtual int ReadPropertyTypeId(ref SequenceReader reader, Func<int, ServerObjectModelInfo?> getServerObjectModel, int propertyIndex) => getServerObjectModel(this.TableId)![propertyIndex].PropertyTypeId;
		protected virtual bool ReadIsProperySerializationOptimizable(ref SequenceReader reader, Func<int, ServerObjectModelInfo?> getServerObjectModel, int propertyIndex) => getServerObjectModel(this.TableId)![propertyIndex].IsSerializationOptimizable;

		public virtual int GetBufferCapacity() => this.PropertyIndexValues?.Count() ?? 0 * 30 + 8;

		protected virtual int GetPropertyTypeId(IServerPropertyInfo propertyModel)
		{
			return propertyModel.PropertyTypeId;
		}

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
