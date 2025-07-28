using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.ComponentModel;
using Simple;
using Simple.Collections;
using Simple.Modeling;

namespace Simple.Objects
{
	public abstract class SystemObject<TKey, TObject> : IDisposable
		where TObject : SystemObject<TKey, TObject>, new() 
		where TKey : notnull
	{
		//private IDictionary<string, object> oldPropertyValuesByName = null;
		private readonly object lockObject = new object();

		//private static List<string> excludedPropertyNamesOnStoring = new List<string>() { "IsNew", "IsDeleted", "ObjectManager" };
		//protected static SystemObjectCollectionByObjectKey<TKey, TObject> NullDictionary = null;
		protected static PropertyModel[]? propertyModelsByFieldIndex = null;

		static SystemObject()
		{
			Model = new SystemObjectModel(typeof(TObject));
		}

		protected SystemObject()
		{
		}

		public SystemObject(SimpleObjectManager objectManager, ref SystemObjectCollectionByObjectKey<TKey, TObject> collectionByObjectKey, Action<TObject> setProperties)
		{
			this.ObjectManager = objectManager;
			this.CollectionByObjectKey = collectionByObjectKey;

			if (this is TObject tObject)
			{
				setProperties(tObject);
				collectionByObjectKey.Add(tObject);
			}

			this.IsNew = true;
		}

		public static SystemObjectModel Model { get; private set; }

		[NonStorable]
		public bool IsNew { get; internal set; }
		
		[NonStorable]
		public bool IsDeleted { get; private set; }
		
		[NonStorable]
		public SimpleObjectManager? ObjectManager { get; internal set; }

		internal SystemObjectCollectionByObjectKey<TKey, TObject>? CollectionByObjectKey { get; set; }

		//protected static List<string> ExcludedPropertyNamesOnStoring
		//{
		//	get { return excludedPropertyNamesOnStoring; }
		//}

		protected static PropertyModel[] GetPropertyModelsByFieldIndex(IDataReader dataReader)
		{
			if (propertyModelsByFieldIndex == null)
			{
				propertyModelsByFieldIndex = new PropertyModel[dataReader.FieldCount];

				for (int i = 0; i < propertyModelsByFieldIndex.Length; i++)
				{
					PropertyModel? propertyModel = Model.PropertyModels.FirstOrDefault((item) => item.PropertyName == dataReader.GetName(i)) as PropertyModel;

					if (propertyModel != null)
						propertyModelsByFieldIndex[i] = propertyModel;
				}
			}

			return propertyModelsByFieldIndex;
		}

		internal void Load(IDataReader dataReader)
		{
			lock (this.lockObject)
			{
				PropertyModel[] propertyModelsByFieldIndex = GetPropertyModelsByFieldIndex(dataReader);

				this.IsNew = false;
				this.LoadFromDatastore(dataReader, propertyModelsByFieldIndex);
				this.OnLoad();
			}
		}

		protected virtual void LoadFromDatastore(IDataReader dataReader, PropertyModel[] propertyModelsByFieldIndex)
		{
			//for (int fieldIndex = (recordCountFieldIncluded) ? 1 : 0; fieldIndex < dataReader.FieldCount; fieldIndex++)
			for (int fieldIndex = 0; fieldIndex < dataReader.FieldCount; fieldIndex++)
			{
				PropertyModel? propertyModel = propertyModelsByFieldIndex[fieldIndex];

				if (propertyModel != null)
				{
					object? propertyValue = dataReader.GetValue(fieldIndex);
					
					propertyValue = GetNormalizedSystemPropertyValue(propertyModel.PropertyType, propertyValue);
					propertyModel.PropertyInfo.SetValue(this, propertyValue, null);
				}
				else
				{
					throw new Exception(String.Format("The datastore field '{0}' has no linked object propery in SystemObject '{1}'", dataReader.GetName(fieldIndex), this.GetType().Name));
				}
			}
		}

		public void Save()
		{
			lock (this.lockObject)
			{
				this.OnBeforeSave();

				int[] propertyIndexes = this.GetModel().StorablePropertyIndexes;

				//propertyIndexes = new int[] { 0, 1 }; //, 1, 2, 3 };

				var propertyIndexValues = this.GetPropertyIndexValuePairs(propertyIndexes);

				if (this.IsNew) // -> Insert
				{
					this.ObjectManager?.LocalDatastore?.InsertRecord(this.GetModel().TableInfo, propertyIndexValues, this.GetModel().GetPropertyModel);
					this.IsNew = false;
				}
				else // -> Update
				{
					TKey key = this.GetKeyValue();
					
					this.ObjectManager?.LocalDatastore?.UpdateRecord(this.GetModel().TableInfo, this.GetModel().ObjectKeyPropertyModel.PropertyIndex, key, propertyIndexValues, this.GetModel().GetPropertyModel);
				}

				this.OnAfterSave();
			}
		}

		public void Delete()
		{
			lock (this.lockObject)
			{
				this.OnBeforeDelete();

				if (!this.IsNew)
					this.ObjectManager?.LocalDatastore?.DeleteRecord(this.GetModel().TableInfo, this.GetModel().ObjectKeyPropertyModel.PropertyIndex, this.GetModel().ObjectKeyPropertyModel.PropertyName, this.GetKeyValue());

				TKey key = this.GetKeyValue();

				this.CollectionByObjectKey?.Remove(key);
				this.IsDeleted = true;
				this.OnAfterDelete();
			}

			this.Dispose();
		}

		public ISystemObjectModel GetModel()
		{
			return Model;
		}

		protected internal virtual TKey GetKeyValue()
		{
			var value = this.GetModel().ObjectKeyPropertyModel.PropertyInfo?.GetValue(this, null);

			if (value is TKey tKey)
				return tKey;

			throw new ArgumentNullException("Key cannot be null");
		}

		protected internal virtual void SetKeyValue(TKey value)
		{
			this.GetModel().ObjectKeyPropertyModel.PropertyInfo?.SetValue(this, value, null);
		}

		protected virtual void OnLoad()
		{
		}

		protected virtual void OnBeforeSave()
		{
		}

		protected virtual void OnAfterSave()
		{
		}

		protected virtual void OnBeforeDelete()
		{
		}

		protected virtual void OnAfterDelete()
		{
		}

		//private IDictionary<string, object> GetPropertyValuesByNameDictionary(Func<Type, object, object> normalizer)
		//{
		//	Dictionary<string, object> result = new Dictionary<string, object>(this.GetModel().StorablePropertyModels.Count);

		//	lock (lockObject)
		//	{
		//		foreach (PropertyModel propertyModel in this.GetModel().StorablePropertyModels)
		//		{
		//			object value = propertyModel.PropertyInfo.GetValue(this, null);

		//			if (normalizer != null)
		//				value = normalizer(propertyModel.PropertyType, value);

		//			result.Add(propertyModel.Name, propertyModel.PropertyInfo.GetValue(this, null));
				
		//			//	if (excludedPropertyNamesOnStoring.Contains(propertyInfo.Name))
		//		//		continue;

		//		//	object propertyValue = propertyInfo.GetValue(this, null);

		//		//	if (this.IsNew || !Comparison.Equals(propertyValue, this.oldPropertyValuesByName[propertyInfo.Name]))
		//		//		result.Add(propertyInfo.Name, propertyValue);
		//		}
		//	}

		//	return result;
		//}

		private object?[] GetPropertyValues(IEnumerable<int> propertyIndexes)
		{
			object?[] propertyValues = new object[propertyIndexes.Count()];

			for (int i = 0; i < propertyIndexes.Count(); i++)
			{
				int propertyIndex = propertyIndexes.ElementAt(i);
				IPropertyModel propertyModel = this.GetModel().GetPropertyModel(propertyIndex);
				object? propertyValue = propertyModel.PropertyInfo?.GetValue(this, null);
				object? normalizedPropertyValue = (propertyValue != null) ? this.GetNormalizedSystemPropertyValue(propertyModel.DatastoreType, propertyValue) : null;

				propertyValues[i] = normalizedPropertyValue;
			}

			return propertyValues;
		}

		protected PropertyIndexValuePair[] GetPropertyIndexValuePairs(IEnumerable<int> propertyIndexes)
		{
			PropertyIndexValuePair[] result = new PropertyIndexValuePair[propertyIndexes.Count()];

			for (int i = 0; i < propertyIndexes.Count(); i++)
			{
				int propertyIndex = propertyIndexes.ElementAt(i);
				IPropertyModel propertyModel = this.GetModel().GetPropertyModel(propertyIndex);
				object? propertyValue = propertyModel.PropertyInfo?.GetValue(this, null);
				object? normalizedPropertyValue = (propertyValue != null) ? this.GetNormalizedSystemPropertyValue(propertyModel.DatastoreType, propertyValue) : null;

				result[i] = new PropertyIndexValuePair(propertyIndex, normalizedPropertyValue);
			}

			return result;
		}

		private object? GetNormalizedSystemPropertyValue(Type propertyType, object? fieldValue)
		{
			if (fieldValue == null || fieldValue.GetType() == typeof(System.DBNull))
				return propertyType.GetDefaultValue();
			else if (fieldValue.GetType() != propertyType)
				return Conversion.TryChangeType(fieldValue, propertyType);

			return fieldValue;
		}

		private void Dispose()
		{
			//this.oldPropertyValuesByName = null;
		}
		
		void IDisposable.Dispose()
		{
			this.Dispose();
		}
	}
}
