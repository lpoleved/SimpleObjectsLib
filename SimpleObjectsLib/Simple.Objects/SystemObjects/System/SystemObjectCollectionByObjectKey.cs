using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Reflection;
using System.Data;
using Simple;
using Simple.Collections;

namespace Simple.Objects
{
	public class SystemObjectCollectionByObjectKey<TKey, TObject> : SimpleDictionary<TKey, TObject>, IDictionaryWithEvents<TKey, TObject>, IDictionary<TKey, TObject>, ICollection<KeyValuePair<TKey, TObject>>, IDictionaryEvents<TKey, TObject>, IEnumerable<KeyValuePair<TKey, TObject>>, IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback, IXmlSerializable, ICloneable
		where TObject : SystemObject<TKey, TObject>, new() where TKey : notnull
	{
		private TObject systemSimpleObject = new TObject();
		private ISystemObjectModel model;

		//private bool includeRecordCountField = false;
		private bool isFirst = true;

		public TKey MaxKeyValue { get; internal set; } = default!;

		public SystemObjectCollectionByObjectKey()
		{
			this.model = this.systemSimpleObject.GetModel();
		}

		public ISystemObjectModel GetModel()
		{
			return this.model;
		}

		public void Load(SimpleObjectManager objectManager)
		{
			//IEnumerable<IDictionary<string, object>> allRecords = null; // objectManager.GetAllRecordsFromDatastore(this.model.TableInfo);

			lock (this.lockObject)
			{
				IDataReader dataReader = objectManager.LocalDatastore.GetRecords(this.GetModel().TableInfo);

				this.Clear();
				this.LoadAllRecords(objectManager, dataReader);

				//	// Set FieldIndex values in SystemPropertyModels definitions 
				//	if (this.propertyModels == null)
				//	{
				//		this.propertyModels = ReflectionHelper.GetFieldsByReflection<SystemPropertyModel>(this.systemSimpleObject);

				//		for (int fieldIndex = (this.includeRecordCountField) ? 1 : 0; fieldIndex < dataReader.FieldCount; fieldIndex++)
				//		{
				//			string fieldName = dataReader.GetName(fieldIndex);
				//			SystemPropertyModel propertyModel;

				//			if (this.propertyModels.TryGetValue(fieldName, out propertyModel))
				//				propertyModel.FieldIndex = fieldIndex;
				//		}
				//	}

				//	// Process read
				//	while (dataReader.Read())
				//	{
				//		if (this.isFirstPass)
				//		{
				//			this.isFirstPass = false;
				//		}
				//		else
				//		{
				//			this.systemSimpleObject = new TObject();
				//		}

				//		this.systemSimpleObject.ObjectManager = objectManager;
				//		this.systemSimpleObject.DictionaryCollection = this;
				//		this.systemSimpleObject.IsNew = false;
				//		this.systemSimpleObject.Load(recordPropertyValues);

				//		this.Add(this.systemSimpleObject.GetKeyValue(), this.systemSimpleObject);
				//	}
				dataReader.Close();
			}

			if (this.model.AutoGenerateKey)
				this.RecalculateMaxKeyValue();
		}

		private void LoadAllRecords(SimpleObjectManager objectManager, IDataReader dataReader) //, bool recordCountFieldIncluded)
		{
			bool isFirstPass = true;

			while (dataReader.Read())
			{
				if (isFirstPass)
				{
					//this.InnerDictionary = (recordCountFieldIncluded) ? new Dictionary<TKey, TObject>((int)dataReader.GetValue(0)) : new Dictionary<TKey, TObject>();
					this.InnerDictionary = new Dictionary<TKey, TObject>();
					isFirstPass = false;
				}
				else
				{
					this.systemSimpleObject = new TObject();
				}

				this.CreateObjectFromPropertyValueData(objectManager, this.systemSimpleObject, dataReader);
			}

			if (this.model.AutoGenerateKey) 
				this.RecalculateMaxKeyValue();
		}

		protected void CreateObjectFromPropertyValueData(SimpleObjectManager objectManager, TObject newSystemObject, IDataReader dataReader)
		{
			newSystemObject.ObjectManager = objectManager;
			newSystemObject.CollectionByObjectKey = this;
			newSystemObject.IsNew = false;
			newSystemObject.Load(dataReader);

			this.Add(newSystemObject.GetKeyValue(), newSystemObject);
		}

		internal TKey Add(TObject value)
		{
			lock (this.lockObject)
			{
				TKey key;

				if (this.model.AutoGenerateKey)
				{
					TKey one = Conversion.TryChangeType<TKey>(1);
					
					key = (TKey)Math2.Sum(this.MaxKeyValue, one);
					value.SetKeyValue(key);
				}
				else
				{
					key = value.GetKeyValue();
				}

				this.Add(key, value);

				return key;
			}
		}

		protected override void OnAfterClear(int oldCount)
		{
			base.OnAfterClear(oldCount);

			if (this.model.ReuseObjectKeys)
				this.MaxKeyValue = default!;
		}

		protected override void OnAfterAdd(TKey key, TObject value)
		{
			base.OnAfterAdd(key, value);

			if (this.model.AutoGenerateKey)
			{
				if (this.isFirst) // Comparison.IsEqual<TKey>(this.MaxKeyValue, default(TKey))) 
				{
					this.MaxKeyValue = value.GetKeyValue(); // Enforce MaxKeyValue to be same type as origin object key value, e.g. object {long}
					this.isFirst = false;
				}
				else
				{
					this.MaxKeyValue = Math2.Max(key, this.MaxKeyValue);
				}
			}
			//	if (value.IsNew)
			//	{
			//		this.MaxKeyValue = Math2.Sum(this.MaxKeyValue, 1);
			//		value.SetKeyValue(this.MaxKeyValue);
			//	}
			//	else
			//	{
			//		this.MaxKeyValue = Math2.Max(keyValue, this.MaxKeyValue);
			//	}
			//}
		}

		protected override void OnAfterSet(TKey key, TObject value, TObject oldValue)
		{
			base.OnAfterSet(key, value, oldValue);

			if (this.model.AutoGenerateKey)
			{
				TKey keyValue = value.GetKeyValue();
				
				this.MaxKeyValue = Math2.Max(keyValue, this.MaxKeyValue);
			}
		}

		protected override void OnAfterRemove(TKey key, TObject value)
		{
			base.OnAfterRemove(key, value);

			if (this.model.AutoGenerateKey && this.model.ReuseObjectKeys && Comparison.IsEqual(value.GetKeyValue(), this.MaxKeyValue))
				this.RecalculateMaxKeyValue();
		}

		private void RecalculateMaxKeyValue()
		{
			lock (this.lockObject)
			{
				this.MaxKeyValue = default!;

				foreach (TObject item in this.Values)
				{
					TKey keyValue = item.GetKeyValue();
					
					var max = Math2.Max(keyValue, this.MaxKeyValue);

					if (max != null)
						this.MaxKeyValue = max;
					else
						this.MaxKeyValue = default!;
				}
			}
		}
	}
}
