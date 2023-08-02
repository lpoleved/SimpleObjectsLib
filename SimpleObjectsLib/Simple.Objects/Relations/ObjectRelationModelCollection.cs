using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects
{
	public class ObjectRelationModelCollection<T> : IObjectRelationModelCollection<T>
		where T : IRelationModel
	{
		private T[] innerArray;
		private IDictionary<int, T> relationModelDictionary;

		public ObjectRelationModelCollection(IDictionary<int, T> relationModelDictionary)
		{
			this.relationModelDictionary = relationModelDictionary;
			int maxKey = (relationModelDictionary.Keys.Count > 0) ? this.relationModelDictionary.Keys.Max() : -1;

			this.innerArray = new T[maxKey + 1];

			foreach (var item in this.relationModelDictionary)
				this.innerArray[item.Key] = item.Value;
		}
		
		public T this[int relationKey]
		{
			get 
			{
				if (relationKey >= 0 && relationKey < this.innerArray.Length)
				{
					return this.innerArray[relationKey];
				}
				else
				{
					return default(T);
				}
			}
		}

		public int Count
		{
			get { return this.relationModelDictionary.Count; }
		}

		//public IEnumerable<T> Collection
		//{
		//	get { return this.relationModelDictionary.Values; }
		//}

		public T GetRelationModel(int relationKey)
		{
			return this[relationKey];
		}

		public bool ContainsKey(int relationKey)
		{
			return this.relationModelDictionary.Keys.Contains(relationKey);
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.</returns>
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.relationModelDictionary.Values.GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.relationModelDictionary.Values.GetEnumerator();
		}
	}

	public interface IObjectRelationModelCollection<T> : IEnumerable<T>
		where T : IRelationModel 
	{
		T this[int relationKey] { get; }
		int Count { get; }
		T GetRelationModel(int relationKey);
		bool ContainsKey(int relationKey);
	}
}
