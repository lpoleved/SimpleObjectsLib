//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple;
//using Simple.Collections;

//namespace Simple.Objects
//{
//    public abstract class SimpleObjectCollection_OLD : IList, ICollection, IEnumerable
//    {
//        #region |   Private Members   |

//        protected object lockObject = new object();
//        private SimpleList<Guid> objectKeys = null;

//        #endregion |   Private Members   |

//        #region |   Constructor(s) and Initialization   |

//        public SimpleObjectCollection_OLD(SimpleObjectManager manager, Type objectType)
//            : this(manager, objectType, new List<Guid>())
//        {
//        }

//        public SimpleObjectCollection_OLD(SimpleObjectManager manager, Type objectType, IList<Guid> objectKeys)
//            : this(manager, objectType, objectKeys, null)
//        {
//        }

//        public SimpleObjectCollection_OLD(SimpleObjectManager manager, Type objectType, IList<Guid> objectKeys, Func<SimpleObject, SimpleObject> getSimpleObject)
//        {
//            this.Manager = manager;
//            this.ObjectType = objectType;
//            this.InternalObjectKeys = objectKeys;
//            this.GetSimpleObjectDelegate = getSimpleObject;
//        }

//        #endregion |   Constructor(s) and Initialization   |

//        #region |   Events   |

//        //public event ActionRequesterSimpleObjectEventHandler ObjectAction;

//        public event OldObjectKeyIndexEventHandler BeforeSet;
//        public event OldObjectKeyIndexEventHandler AfterSet;
//        public event ObjectKeyIndexEventHandler BeforeInsert;
//        public event ObjectKeyIndexEventHandler AfterInsert;
//        public event ObjectKeyIndexEventHandler BeforeRemove;
//        public event ObjectKeyIndexEventHandler AfterRemove;
//        public event EventHandler BeforeClear;
//        public event EventHandler AfterClear;
//        public event CountChangeEventHandler CountChange;

//        #endregion |   Events   |

//        #region |   Private Properties   |

//        private Func<SimpleObject, SimpleObject> GetSimpleObjectDelegate { get; set; }

//        #endregion |   Private Properties   |

//        #region |   Public Properties   |

//        /// <summary>
//        /// Gets the element at the specified index.
//        /// </summary>
//        /// <param name="index">The zero-based index of the element to get.</param>
//        /// <returns>The element at the specified index.</returns>
//        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero.  -or- index is equal to or greater than <see cref="CustomList&lt;T&gt;"/>.Count.</exception>
//        /// <exception cref="System.NotSupportedException">the property is set and the <see cref="T:System.Collections.Generic.IList`1"></see> is read-only.</exception>
//        public SimpleObject this[int index]
//        {
//            get { return this.ListGet(index); }
//        }

//        /// <summary>
//        /// Gets the element for the specified object key.
//        /// </summary>
//        /// <param name="objectKey">The object key of the element to get.</param>
//        /// <returns>The element at the specified index.</returns>
//        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero.  -or- index is equal to or greater than <see cref="CustomList&lt;T&gt;"/>.Count.</exception>
//        /// <exception cref="System.NotSupportedException">the property is set and the <see cref="T:System.Collections.Generic.IList`1"></see> is read-only.</exception>
//        public SimpleObject this[Guid objectKey]
//        {
//            get { return this.ListGet(objectKey); }
//        }

//        /// <summary>
//        /// Gets the number of elements contained in the <see cref="T:SimpleObjectCollection"></see>.
//        /// </summary>
//        /// <value></value>
//        /// <returns>The number of elements contained in the <see cref="T:SimpleObjectCollection"></see>.</returns>
//        public int Count
//        {
//            get { return this.InternalObjectKeys.Count; }
//        }

//        //private SimpleObjectHashtable ObjectHashtable
//        //{
//        //    get
//        //    {
//        //        if (this.objectHashtable == null)
//        //        {
//        //            this.objectHashtable = new SimpleObjectHashtable();
//        //        }

//        //        return this.objectHashtable;
//        //    }
//        //}


//        /// <summary>Determines whether the <see cref="SimpleObjectCollection"></see> contains a specific value.</summary>
//        /// <param name="objectKey">The SimpleObjectKey to locate in the <see cref="SimpleObjectCollection"></see>.</param>
//        /// <returns>true if the SimpleObjectKey is found in the <see cref="SimpleObjectCollection"></see>; otherwise, false.</returns>
//        public bool ContainsKey(Guid objectKey)
//        {
//            bool value = this.InternalObjectKeys.Contains(objectKey);
//            return value;
//        }

//        public IList<Guid> ObjectKeys
//        {
//            get 
//            { 
//                if (this.objectKeys == null)
//                    this.objectKeys = new SimpleList<Guid>(this.InternalObjectKeys);

//                return this.objectKeys.AsReadOnly(); 
//            }
//        }

//        #endregion |   Public Properties   |

//        #region |   Protected Properties   |

//        protected SimpleObjectManager Manager { get; private set; }
//        protected Type ObjectType { get; private set; }
//        protected IList<Guid> InternalObjectKeys { get; private set; }

//        #endregion |   Protected Properties   |

//        #region |   Protected Internal Methods   |

//        protected internal SimpleObject ListGet(int index)
//        {
//            lock (lockObject)
//            {
//                Guid objectKey = this.InternalObjectKeys[index];
//                return this.ListGet(objectKey);
//            }
//        }

//        protected internal SimpleObject ListGet(Guid objectKey)
//        {
//            lock (lockObject)
//            {
//                SimpleObject value = this.Manager.ObjectCache.GetObject(objectKey);
//                SimpleObject result = this.GetSimpleObject(value);

//                return result;
//            }
//        }

//        protected internal void ListSet(int index, Guid objectKey)
//        {
//            lock (lockObject)
//            {
//                Guid oldObjectKey = this.InternalObjectKeys[index];
//                this.OnBeforeSet(index, objectKey, oldObjectKey);
//                this.RaiseBeforeSet(index, objectKey, oldObjectKey);

//                this.InternalObjectKeys[index] = objectKey;

//                this.OnAfterSet(index, objectKey, oldObjectKey);
//                this.RaiseAfterSet(index, objectKey, oldObjectKey);
//            }
//        }

//        internal protected int ListAdd(Guid objectKey)
//        {
//            this.ListInsert(this.InternalObjectKeys.Count, objectKey);
//            return this.InternalObjectKeys.Count;
//        }

//        protected internal void ListInsert(int index, Guid objectKey)
//        {
//            if (this.ContainsKey(objectKey))
//            {
//                throw new ArgumentException("The object to be added to the collection already exists in the collection.");
//            }
//            else
//            {
//                lock (lockObject)
//                {
//                    this.OnBeforeInsert(index, objectKey);
//                    this.RaiseBeforeInsert(index, objectKey);

//                    this.InternalObjectKeys.Insert(index, objectKey);

//                    this.OnAfterInsert(index, objectKey);
//                    this.RaiseAfterInsert(index, objectKey);
//                    this.OnCountChange(this.Count, this.Count - 1);
//                    this.RaiseCountChange(this.Count, this.Count - 1);
//                }
//            }
//        }

//        protected internal bool ListRemove(Guid objectKey)
//        {
//            lock (lockObject)
//            {
//                bool result = false;
//                Guid objectKeyToRemove = this.ListMatchItem(objectKey);

//                if (objectKeyToRemove != null)
//                {
//                    int index = this.InternalObjectKeys.IndexOf(objectKeyToRemove);
//                    result = this.ListRemoveAt(index);
//                }

//                return result;
//            }
//        }

//        protected internal bool ListRemoveAt(int index)
//        {
//            lock (lockObject)
//            {
//                bool exists = index >= 0 && index < this.InternalObjectKeys.Count;

//                if (exists)
//                {
//                    Guid objectKey = this.InternalObjectKeys[index];

//                    this.OnBeforeRemove(index, objectKey);
//                    this.RaiseBeforeRemove(index, objectKey);

//                    this.InternalObjectKeys.RemoveAt(index);

//                    this.OnAftereRemove(index, objectKey);
//                    this.RaiseAfterRemove(index, objectKey);


//                    this.OnCountChange(this.Count, this.Count + 1);
//                    this.RaiseCountChange(this.Count, this.Count + 1);
//                }

//                return exists;
//            }
//        }

//        protected internal void ListClear()
//        {
//            lock (lockObject)
//            {
//                int oldCount = this.Count;
//                this.OnBeforeClear();
//                this.RaiseBeforeClear();

//                this.InternalObjectKeys.Clear();

//                this.OnAfterClear();
//                this.RaiseAfterClear();
//                this.OnCountChange(this.Count, oldCount);
//                this.RaiseCountChange(this.Count, oldCount);
//            }
//        }

//        #endregion |   Protected Internal Methods   |

//        #region |   Protected Methods   |

//        protected virtual Guid ListMatchItem(Guid objectKey)
//        {
//            lock (lockObject)
//            {
//				Guid result = Guid.Empty;

//                if (this.InternalObjectKeys.Contains(objectKey))
//                    result = objectKey;

//                return result;
//            }
//        }

//        #endregion |   Protected Methods   |

//        #region |   Protected Virtual Methods   |

//        protected virtual void OnBeforeInsert(int index, Guid objectKey)
//        {
//        }

//        protected virtual void OnAfterInsert(int index, Guid objectKey)
//        {
//        }

//        protected virtual void OnBeforeRemove(int index, Guid objectKey)
//        {
//        }

//        protected virtual void OnAftereRemove(int index, Guid objectKey)
//        {
//        }

//        protected virtual void OnBeforeSet(int index, Guid objectKey, Guid oldObjectKey)
//        {
//        }

//        protected virtual void OnAfterSet(int index, Guid objectKey, Guid oldObjectKey)
//        {
//        }

//        protected virtual void OnBeforeClear()
//        {
//        }

//        protected virtual void OnAfterClear()
//        {
//        }

//        protected virtual void OnCountChange(int count, int oldCount)
//        {
//        }

//        #endregion |   Protected Virtual Methods   |

//        #region |   Private Methods   |

//        private SimpleObject GetSimpleObject(SimpleObject simpleObject)
//        {
//            SimpleObject result = simpleObject;

//            if (this.GetSimpleObjectDelegate != null)
//                result = this.GetSimpleObjectDelegate(simpleObject);

//            return result;
//        }

//        #endregion |   Private Methods   |

//        #region |   Private Raise Event Methods   |

//        private void RaiseBeforeInsert(int index, Guid objectKey)
//        {
//            if (this.BeforeInsert != null)
//            {
//                this.BeforeInsert(this, new ObjectKeyIndexEventArgs(index, objectKey));
//            }
//        }

//        private void RaiseAfterInsert(int index, Guid objectKey)
//        {
//            if (this.AfterInsert != null)
//            {
//                this.AfterInsert(this, new ObjectKeyIndexEventArgs(index, objectKey));
//            }
//        }

//        private void RaiseBeforeRemove(int index, Guid objectKey)
//        {
//            if (this.BeforeRemove != null)
//            {
//                this.BeforeRemove(this, new ObjectKeyIndexEventArgs(index, objectKey));
//            }
//        }

//        private void RaiseAfterRemove(int index, Guid objectKey)
//        {
//            if (this.AfterRemove != null)
//            {
//                this.AfterRemove(this, new ObjectKeyIndexEventArgs(index, objectKey));
//            }
//        }

//        private void RaiseBeforeSet(int index, Guid objectKey, Guid oldObjectKey)
//        {
//            if (this.BeforeSet != null)
//            {
//                this.BeforeSet(this, new OldObjectKeyIndexEventArgs(index, objectKey, oldObjectKey));
//            }
//        }

//        private void RaiseAfterSet(int index, Guid objectKey, Guid oldObjectKey)
//        {
//            if (this.AfterSet != null)
//            {
//                this.AfterSet(this, new OldObjectKeyIndexEventArgs(index, objectKey, oldObjectKey));
//            }
//        }

//        private void RaiseBeforeClear()
//        {
//            if (this.BeforeClear != null)
//            {
//                this.BeforeClear(this, new EventArgs());
//            }
//        }

//        private void RaiseAfterClear()
//        {
//            if (this.AfterClear != null)
//            {
//                this.AfterClear(this, new EventArgs());
//            }
//        }

//        private void RaiseCountChange(int count, int oldCount)
//        {
//            if (this.CountChange != null)
//            {
//                this.CountChange(this, new CountChangeEventArgs(count, oldCount));
//            }
//        }

//        #endregion |   Private Raise Event Methods   |

//        #region |   IList Interface   |

//        /// <summary>
//        /// Gets or sets the element at the specified index.
//        /// </summary>
//        /// <param name="index">The zero-based index of the element to get or set.</param>
//        /// <returns>The element at the specified index.</returns>
//        /// <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="System.Collections.IList"></see>.</exception>
//        /// <exception cref="System.NotSupportedException">The property is set and the <see cref="System.Collections.IList"></see> is read-only.-or- The <see cref="T:System.Collections.Generic.IList"></see> has a fixed size.</exception>
//        object IList.this[int index]
//        {
//            get { return this.ListGet(index); }
//            set { throw new NotSupportedException("Collection is read-only."); }
//        }

//        /// <summary>
//        /// Adds an item to the <see cref="System.Collections.IList"></see>.
//        /// </summary>
//        /// <param name="value">The System.Object to add to the <see cref="System.Collections.IList"></see>.</param>
//        /// <returns>The position into which the new element was inserted.</returns>
//        /// <exception cref="System.NotSupportedException">The <see cref="System.Collections.IList"></see> is read-only.-or- The <see cref="T:System.Collections.Generic.IList"></see> has a fixed size.</exception>
//        int IList.Add(object value)
//        {
//            throw new NotSupportedException("Collection is read-only.");
//        }

//        /// <summary>
//        /// Removes all items from the <see cref="System.Collections.IList"></see>.
//        /// </summary>
//        /// <exception cref="System.NotSupportedException">The <see cref="System.Collections.IList"></see> is read-only.</exception>
//        void IList.Clear()
//        {
//            throw new NotSupportedException("Collection is read-only.");
//        }

//        /// <summary>Determines whether the <see cref="System.Collections.IList"></see> contains a specific value.</summary>
//        /// <param name="value">The System.Object to locate in the <see cref="System.Collections.IList"></see>.</param>
//        /// <returns>true if the System.Object is found in the <see cref="System.Collections.IList"></see>; otherwise, false.</returns>
//        bool IList.Contains(object value)
//        {
//            return value != null && value is SimpleObject ? this.ContainsKey((value as SimpleObject).Key) : false;
//        }

//        /// <summary>
//        /// Determines the index of a specific item in the <see cref="System.Collections.IList"></see>.
//        /// </summary>
//        /// <param name="value">The System.Object to locate in the <see cref="System.Collections.IList"></see>.</param>
//        /// <returns>The index of value if found in the list; otherwise, -1.</returns>
//        int IList.IndexOf(object value)
//        {
//            lock (lockObject)
//            {
//                int index = -1;

//                for (int i = 0; i < this.Count; i++)
//                {
//                    if (this[i].Equals(value))
//                    {
//                        index = i;
//                        break;
//                    }
//                }

//                return index;
//            }
//        }

//        /// <summary>
//        /// Inserts an item to the <see cref="System.Collections.Generic.IList"></see> at the specified index.
//        /// </summary>
//        /// <param name="index">The zero-based index at which value should be inserted.</param>
//        /// <param name="item">The System.Object to insert into the <see cref="System.Collections.IList"></see>.</param>
//        /// <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="System.Collections.IList"></see>.</exception>
//        /// <exception cref="System.NotSupportedException">The <see cref="System.Collections.IList"></see> is read-only.-or- The <see cref="System.Collections.IList"></see> has a fixed size.</exception>
//        /// <exception cref="System.NullReferenceException">value is null reference in the <see cref="System.Collections.IList"></see>.</exception>
//        void IList.Insert(int index, object item)
//        {
//            throw new NotSupportedException("Collection is read-only.");
//        }

//        /// <summary>
//        /// Removes the first occurrence of a specific object from the <see cref="System.Collections.IList"></see>.
//        /// </summary>
//        /// <param name="item">The System.Object to remove from the <see cref="System.Collections.IList"></see>.</param>
//        /// <exception cref="System.NotSupportedException">The <see cref="System.Collections.IList"></see> is read-only.-or- The <see cref="System.Collections.IList"></see> has a fixed size.</exception>
//        void IList.Remove(object item)
//        {
//            throw new NotSupportedException("Collection is read-only.");
//        }

//        /// <summary>
//        /// Removes the <see cref="T:System.Collections.IList"></see> item at the specified index.
//        /// </summary>
//        /// <param name="index">The zero-based index of the item to remove.</param>
//        /// <exception cref="System.NotSupportedException">The <see cref="System.Collections.IList"></see> is read-only.-or- The <see cref="System.Collections.IList"></see> has a fixed size.</exception>
//        void IList.RemoveAt(int index)
//        {
//            throw new NotSupportedException("Collection is read-only.");
//        }

//        /// <summary>
//        /// Gets a value indicating whether the <see cref="System.Collections.IList"></see> has a fixed size.
//        /// </summary>
//        /// <returns>true if the <see cref="System.Collections.IList"></see> has a fixed size; otherwise, false.</returns>
//        bool IList.IsFixedSize
//        {
//            get { return (this.InternalObjectKeys as IList).IsFixedSize; }
//        }

//        /// <summary>
//        /// Gets a value indicating whether the <see cref="T:System.Collections.IList"></see> is read-only.
//        /// </summary>
//        /// <value></value>
//        /// <returns>true if the <see cref="T:System.Collections.IList"></see> is read-only; otherwise, false.</returns>
//        bool IList.IsReadOnly
//        {
//            get { return true; }
//        }

//        #endregion |   IList Interface   |

//        #region |   ICollection Interface   |

//        /// <summary>Gets a value indicating whether access to the <see cref="System.Collections.ICollection"></see> is synchronized (thread safe).</summary>
//        /// <returns>true if access to the System.Collections.ICollection is synchronized (thread safe); otherwise, false.</returns>
//        bool ICollection.IsSynchronized
//        {
//            get { return false; }
//        }

//        /// <summary>Gets an object that can be used to synchronize access to the <see cref="System.Collections.ICollection"></see>.</summary>
//        /// <returns>An object that can be used to synchronize access to the <see cref="System.Collections.ICollection"></see>.</returns>
//        object ICollection.SyncRoot
//        {
//            get { return this; }
//        }

//        /// <summary>
//        /// Copies the elements of the <see cref="System.Collections.ICollection"></see> to an <see cref="System.Array"></see>, starting at a particular <see cref="System.Array"></see> index.
//        /// </summary>
//        /// <param name="array">The one-dimensional <see cref="System.Array"></see> that is the destination of the elements copied from <see cref="System.Collections.ICollection"></see>. The <see cref="System.Array"></see> must have zero-based indexing.</param>
//        /// <param name="index">The zero-based index in array at which copying begins.</param>
//        /// <exception cref="System.ArgumentNullException">array is null.</exception>
//        /// <exception cref="System.ArgumentOutOfRangeException">index is less than zero.</exception>
//        /// <exception cref="System.ArgumentException">index is less than zero.</exception>
//        /// <exception cref="System.ArgumentException">The type of the source <see cref="System.Collections.ICollection"></see> cannot be cast automatically to the type of the destination array.</exception>
//        void ICollection.CopyTo(Array array, int index)
//        {
//            if (object.ReferenceEquals(array, null))
//            {
//                throw new ArgumentNullException("Null array reference", "array");
//            }

//            if (index < 0)
//            {
//                throw new ArgumentOutOfRangeException("Index is out of range", "index");
//            }

//            if (array.Rank > 1)
//            {
//                throw new ArgumentException("Array is multi-dimensional", "array");
//            }

//            lock (lockObject)
//            {
//                foreach (object o in this)
//                {
//                    array.SetValue(o, index);
//                    index++;
//                }
//            }
//        }

//        #endregion |   ICollection Interface   |

//        #region |   IEnumerable Interface   |

//        /// <summary>
//        /// Returns an enumerator that iterates through a collection.
//        /// </summary>
//        /// <returns>
//        /// An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
//        /// </returns>
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            lock (lockObject)
//            {
//                foreach (Guid objectKey in this.InternalObjectKeys)
//                    yield return this[objectKey];
//            }
//        }

//        #endregion |   IEnumerable Interface   |

//    }

//    #region |   Delegates   |

//    public delegate void ObjectKeyIndexEventHandler(object sender, ObjectKeyIndexEventArgs e);
//    public delegate void OldObjectKeyIndexEventHandler(object sender, OldObjectKeyIndexEventArgs e);

//    #endregion |   Delegates   |

//    #region |   EventArgs Classes   |

//    public class ObjectKeyIndexEventArgs : EventArgs
//    {
//        public ObjectKeyIndexEventArgs(int index, Guid objectKey)
//        {
//            this.Index = index;
//            this.ObjectKey = objectKey;
//        }

//        public int Index { get; private set; }
//        public Guid ObjectKey { get; private set; }
//    }

//    public class OldObjectKeyIndexEventArgs : ObjectKeyIndexEventArgs
//    {
//        public OldObjectKeyIndexEventArgs(int index, Guid objectKey, Guid oldObjectKey)
//            : base(index, objectKey)
//        {
//            this.OldObjectKey = objectKey;
//        }

//        public Guid OldObjectKey { get; private set; }
//    }
    
//    #endregion |   EventArgs Classes   |

//}
