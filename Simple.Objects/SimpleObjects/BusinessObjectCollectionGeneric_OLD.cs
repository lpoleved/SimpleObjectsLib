//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple;

//namespace Simple.Objects
//{
//    public class SimpleObjectCollection_OLD<T> : SimpleObjectCollection_OLD, IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable where T : SimpleObject
//    {
//        #region |   Constructor(s) and Initialization   |

//        public SimpleObjectCollection_OLD(SimpleObjectManager manager)
//            : base(manager, typeof(T))
//        {
//        }

//        public SimpleObjectCollection_OLD(SimpleObjectManager manager, IList<Guid> objectKeys)
//            : base(manager, typeof(T), objectKeys)
//        {
//        }

//        public SimpleObjectCollection_OLD(SimpleObjectManager manager, IList<Guid> objectKeys, Func<SimpleObject, SimpleObject> getSimpleObject)
//            : base(manager, typeof(T), objectKeys, getSimpleObject)
//        {
//        }

//        #endregion |   Constructor(s) and Initialization   |

//        #region |   Public Methods   |

//        /// <summary>
//        /// Gets the element at the specified index.
//        /// </summary>
//        /// <param name="index">The zero-based index of the element to get.</param>
//        /// <returns>The element at the specified index.</returns>
//        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero.  -or- index is equal to or greater than <see cref="CustomList&lt;T&gt;"/>.Count.</exception>
//        /// <exception cref="System.NotSupportedException">the property is set and the <see cref="T:System.Collections.Generic.IList`1"></see> is read-only.</exception>
//        public new T this[int index]
//        {
//            get { return base[index] as T; }
//        }

//        /// <summary>
//        /// Gets the element for the specified object key.
//        /// </summary>
//        /// <param name="objectKey">The object key of the element to get.</param>
//        /// <returns>The element at the specified index.</returns>
//        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero.  -or- index is equal to or greater than <see cref="CustomList&lt;T&gt;"/>.Count.</exception>
//        /// <exception cref="System.NotSupportedException">the property is set and the <see cref="T:System.Collections.Generic.IList`1"></see> is read-only.</exception>
//        public new T this[Guid objectKey]
//        {
//            get { return base[objectKey] as T; }
//        }

//        /// <summary>
//        /// Determines whether the <see cref="T:System.Collections.Generic.IList`1"></see> contains a specific value.
//        /// </summary>
//        /// <param name="simpleObject">The object to locate in the <see cref="T:System.Collections.Generic.IList`1"></see>.</param>
//        /// <returns>
//        /// true if item is found in the <see cref="T:System.Collections.Generic.IList`1"></see>; otherwise, false.
//        /// </returns>
//        public bool Contains(T simpleObject)
//        {
//            return (this as IList).Contains(simpleObject);
//        }

//        /// <summary>
//        /// Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="CustomList&lt;T&gt;"/>.
//        /// </summary>
//        /// <typeparam name="T">The type of elements in the list.</typeparam> 
//        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1"></see>. The item can be null for reference types.</param>
//        /// <returns>The zero-based index of the first occurrence of item within the entire <see cref="SimpleList&lt;T&gt;"/>, if found; otherwise, -1.</returns>
//        public int IndexOf(T item)
//        {
//            return (this as IList).IndexOf(item);
//        }

//        /// <summary>
//        /// Retrieves all the elements that match the conditions defined by the specified predicate.
//        /// </summary>
//        /// <param name="match">The <see cref="T:System.Predicate`1"></see> delegate that defines the conditions of the elements to search for.</param>
//        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerable`1"></see> containing all the elements that match  the conditions defined by the 
//        /// specified predicate, if found; otherwise, an empty <see cref="T:System.Collections.Generic.IEnumerable`1"></see>.</returns>
//        /// <exception cref="T:System.ArgumentNullException">match is null.</exception>
//        public IEnumerable<T> Find(Predicate<T> match)
//        {
//            lock (lockObject)
//            {

//				var result = from T item in this
//							 where match(item)
//							 select item;

//                return result;
//            }
//        }

//        /// <summary>
//        /// Returns an enumerator that iterates through the collection.
//        /// </summary>
//        /// <returns> A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection. </returns>
//        public IEnumerator<T> GetEnumerator()
//        {
//            lock (lockObject)
//            {
//                foreach (Guid objectKey in this.InternalObjectKeys)
//                    yield return this[objectKey];
//            }
//        }

//        #endregion |   Public Methods   |

//        #region |   IList<T> Interface   |

//        /// <summary>
//        /// Gets or sets the element at the specified index.
//        /// </summary>
//        /// <param name="index">The zero-based index of the element to get or set.</param>
//        /// <returns>The element at the specified index.</returns>
//        /// <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="System.Collections.IList`1"></see>.</exception>
//        /// <exception cref="System.NotSupportedException">The property is set and the <see cref="System.Collections.IList`1"></see> is read-only.-or- The <see cref="T:System.Collections.Generic.IList"></see> has a fixed size.</exception>
//        T IList<T>.this[int index]
//        {
//            get { return this[index]; }
//            set { throw new NotSupportedException("Collection is read-only."); }
//        }

//        /// <summary>
//        /// Inserts an item to the <see cref="T:System.Collections.Generic.IList`1"></see> at the specified index.
//        /// </summary>
//        /// <typeparam name="T">The type of elements in the list.</typeparam> 
//        /// <param name="index">The zero-based index at which item should be inserted.</param>
//        /// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1"></see>.</param>
//        /// <returns>The index of item if found in the list; otherwise, -1.</returns>
//        /// <exception cref="System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1"></see> is read-only.-or- The <see cref="T:System.Collections.Generic.IList`1"></see> has a fixed size.</exception>
//        void IList<T>.Insert(int index, T item)
//        {
//            throw new NotSupportedException("Collection is read-only.");
//        }

//        /// <summary>
//        /// Removes cannot be performed on the read-only collection.
//        /// </summary>
//        /// <param name="index">The zero-based index of the item to remove.</param>
//        /// <returns>The index of item if found in the list; otherwise, -1.</returns>
//        /// <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"></see>.</exception>
//        /// <exception cref="System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1"></see> is read-only.-or- The <see cref="T:System.Collections.Generic.IList`1"></see> has a fixed size.</exception>
//        void IList<T>.RemoveAt(int index)
//        {
//            throw new NotSupportedException("Collection is read-only.");
//        }

//        #endregion |   IList<T> Interface   |

//        #region |   ICollection<T> Interface   |

//        /// <summary>
//        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
//        /// </summary>
//        /// <typeparam name="T">The type of elements in the list.</typeparam> 
//        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
//        /// <exception cref="System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.-or- The <see cref="T:System.Collections.Generic.ICollection`1"></see> has a fixed size.</exception>
//        void ICollection<T>.Add(T item)
//        {
//            throw new NotSupportedException("Collection is read-only.");
//        }

//        /// <summary>
//        /// Removes all items from the System.Collections.Generic.ICollection<T>.
//        /// </summary>
//        /// <exception cref="System.NotSupportedException">The System.Collections.Generic.ICollection<T> is read-only.</exception>
//        void ICollection<T>.Clear()
//        {
//            throw new NotSupportedException("Collection is read-only.");
//        }

//        /// <summary>
//        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
//        /// </summary>
//        /// <typeparam name="T">The type of elements in the list.</typeparam> 
//        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
//        /// <returns>true if item was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false. 
//        ///   This method also returns false if item is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"></see>.</returns>
//        /// <exception cref="System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.-or- The <see cref="T:System.Collections.Generic.ICollection`1"></see> has a fixed size.</exception>
//        bool ICollection<T>.Remove(T item)
//        {
//            throw new NotSupportedException("Collection is read-only.");
//        }

//        /// <summary>
//        /// Copies the elements of the <see cref="T:System.Collections.Generic.IList`1"></see> to an <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.
//        /// </summary>
//        /// <param name="array">The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.IList`1"></see>. The <see cref="T:System.Array"></see> must have zero-based indexing.</param>
//        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
//        /// <exception cref="T:System.ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
//        /// <exception cref="T:System.ArgumentNullException">array is null.</exception>
//        /// <exception cref="T:System.ArgumentException">array is multidimensional.-or-arrayIndex is equal to or greater than the length of array.-or-The number of elements in the source <see cref="T:System.Collections.Generic.IList`1"></see> is greater than the available space from arrayIndex to the end of the destination array.-or-Type T cannot be cast automatically to the type of the destination array.</exception>
//        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
//        {
//            if (object.ReferenceEquals(array, null))
//            {
//                throw new ArgumentNullException("Null array reference", "array");
//            }

//            if (arrayIndex < 0)
//            {
//                throw new ArgumentOutOfRangeException("Index is out of range", "index");
//            }

//            if (array.Rank > 1)
//            {
//                throw new ArgumentException("Array is multi-dimensional", "array");
//            }

//            lock (lockObject)
//            {
//                foreach (T t in this)
//                {
//                    array.SetValue(t, arrayIndex);
//                    arrayIndex++;
//                }
//            }
//        }

//        /// <summary>
//        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.
//        /// </summary>
//        /// <value></value>
//        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only; otherwise, false.</returns>
//        bool ICollection<T>.IsReadOnly
//        {
//            get { return true; }
//        }

//        #endregion |   ICollection<T> Interface   |
//    }
//}
