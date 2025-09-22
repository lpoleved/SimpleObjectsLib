//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple;
//using Simple.Collections;

//namespace Simple.Objects
//{
//    public class ManyToManySimpleObjectCollection<T> : SimpleObjectCollection<T>, IManyToManySimpleObjectCollectionControl, IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable where T : SimpleObject
//    {
//        #region |   Private Members   |

//        private SimpleObject owner = null;
//        private ManyToManyRelation manayToManyRelation = null;
//        internal bool isOwnerObjectIdFirst = false;
//        private ReadOnlyList<T> readOnlyList = null;

//        #endregion |   Private Members   |

//        #region |   Constructor(s) and Initialization   |

//        public ManyToManySimpleObjectCollection(SimpleObject owner, ManyToManyRelation manayToManyRelation, IList<ManyToManyRelationElement> manyToManyRelationElements, bool isOwnerObjectIdFirst)
//            : base(owner.Manager)
//        {
//            this.owner = owner;
//            this.manayToManyRelation = manayToManyRelation;
//            this.ManyToManyRelationElements = manyToManyRelationElements;
//            this.isOwnerObjectIdFirst = isOwnerObjectIdFirst;

//            foreach (ManyToManyRelationElement manyToManyRelationElement in this.ManyToManyRelationElements)
//            {
//                SimpleObject relatedSimpleObject = this.isOwnerObjectIdFirst ? manyToManyRelationElement.SimpleObject2 : manyToManyRelationElement.SimpleObject1;
//                this.InternalObjectKeys.Add(relatedSimpleObject.Key);
//            }
//        }

//        #endregion |   Constructor(s) and Initialization   |

//        #region |   Public Properties   |

//        /// <summary>
//        /// Gets or sets the element at the specified index.
//        /// </summary>
//        /// <param name="index">The zero-based index of the element to get.</param>
//        /// <returns>The element at the specified index.</returns>
//        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero.  -or- index is equal to or greater than <see cref="ManyToManySimpleObjectCollection&lt;T&gt;"/>.Count.</exception>
//        /// <exception cref="System.NotSupportedException">the property is set and the <see cref="T:System.Collections.Generic.IList`1"></see> is read-only.</exception>
//        public new T this[int index]
//        {
//            get { return this.ListGet(index) as T; }
//            set { this.ListSet(index, value.Key); }
//        }

//        #endregion |   Public Properties   |

//        #region |   Protected Properties   |

//        /// <summary>
//        /// Gets the List of the GroupMembershipElement members.
//        /// </summary>
//        protected IList<ManyToManyRelationElement> ManyToManyRelationElements { get; private set; }

//        #endregion |   Protected Properties   |

//        #region |   Public Methods   |

//        /// <summary>
//        /// Adds an item to the <see cref="ManyToManySimpleObjectCollection&lt;T&gt;"/>.
//        /// </summary>
//        /// <typeparam name="T">The type of elements in the list.</typeparam> 
//        /// <param name="item">The object to add to the <see cref="ManyToManySimpleObjectCollection&lt;T&gt;"/>.</param>
//        public void Add(T simpleObject)
//        {
//            this.ListAdd(simpleObject.Key);
//        }

//        /// <summary>
//        /// Removes the first occurrence of a specific object from the <see cref="ManyToManySimpleObjectCollection&lt;T&gt;"/>.
//        /// </summary>
//        /// <typeparam name="T">The type of elements in the list.</typeparam> 
//        /// <param name="item">The object to remove from the <see cref="ManyToManySimpleObjectCollection&lt;T&gt;"/>.</param>
//        /// <returns>true if item was successfully removed from the <see cref="ManyToManySimpleObjectCollection&lt;T&gt;"/>; otherwise, false. 
//        ///   This method also returns false if item is not found in the original <see cref="ManyToManySimpleObjectCollection&lt;T&gt;"/>.</returns>
//        public bool Remove(T simpleObject)
//        {
//            return this.ListRemove(simpleObject.Key);
//        }

//        /// <summary>
//        /// Removes all items from the <see cref="ManyToManySimpleObjectCollection&lt;T&gt;"/>.
//        /// </summary>
//        public void Clear()
//        {
//            base.ListClear();
//        }

//        /// <summary>
//        /// Returns a read-only <see cref="ReadOnlyList&lt;T&gt;"/> wrapper for the current collection.
//        /// </summary>
//        /// <returns>A <see cref="ReadOnlyList&lt;T&gt;"/> that acts as a read-only wrapper around the current <see cref="T:System.Collections.Generic.IList`1"></see>.</returns>
//        public ReadOnlyList<T> AsReadOnly()
//        {
//            if (this.readOnlyList == null)
//            {
//                this.readOnlyList = new ReadOnlyList<T>(this);
//            }

//            return this.readOnlyList;
//        }

//        #endregion |   Public Methods   |

//        #region |   Protected Methods   |

//        protected void AddGroupMembershipElement(ManyToManyRelationElement groupMembershipElement)
//        {
//            this.ManyToManyRelationElements.Add(groupMembershipElement);
//            SimpleObjectKey objectKey = this.isOwnerObjectIdFirst ? groupMembershipElement.ObjectKey2 : groupMembershipElement.ObjectKey1;
//            this.InternalObjectKeys.Add(objectKey);
//        }

//        protected void RemoveGroupMembershipElement(ManyToManyRelationElement groupMembershipElement)
//        {
//            this.ManyToManyRelationElements.Remove(groupMembershipElement);
//            SimpleObjectKey objectKey = this.isOwnerObjectIdFirst ? groupMembershipElement.ObjectKey2 : groupMembershipElement.ObjectKey1;
//            this.InternalObjectKeys.Remove(objectKey);
//        }

//        #endregion |   Protected Methods   |

//        #region |   Protected Overrided Methods   |

//        protected override void OnBeforeSet(int index, SimpleObjectKey objectKey, SimpleObjectKey oldObjectKey)
//        {
//            this.OnBeforeRemove(index, oldObjectKey);
//            base.OnBeforeSet(index, objectKey, oldObjectKey);
//            this.OnBeforeInsert(index, objectKey);
//        }

//        protected override void OnBeforeInsert(int index, SimpleObjectKey objectKey)
//        {
//            SimpleObjectKey objectKey1 = this.isOwnerObjectIdFirst ? this.owner.Key : objectKey;
//            SimpleObjectKey objectKey2 = this.isOwnerObjectIdFirst ? objectKey : this.owner.Key;

//            ManyToManyRelationElement manyToManyRelationElement = new ManyToManyRelationElement(this.manayToManyRelation, objectKey1, objectKey2);
//            this.ManyToManyRelationElements.Add(manyToManyRelationElement);

//            SimpleObject simpleObject = this[objectKey];
//            simpleObject.ObjectRelationCache.AddToManyToManyObjectCollectionIfInCache(this.manayToManyRelation.RelationKey, manyToManyRelationElement);
//            manyToManyRelationElement.SaveIfCan();

//            base.OnBeforeInsert(index, objectKey);
//        }

//        protected override void OnBeforeRemove(int index, SimpleObjectKey objectKey)
//        {
//            ManyToManyRelationElement groupMembershipElement = this.ManyToManyRelationElements[index];
//            SimpleObject relatedSimpleObject = this.isOwnerObjectIdFirst ? groupMembershipElement.SimpleObject2 : groupMembershipElement.SimpleObject1;

//            relatedSimpleObject.ObjectRelationCache.RemoveFromManyToManyObjectCollectionIfInCache(this.manayToManyRelation.RelationKey, groupMembershipElement);
//            this.ManyToManyRelationElements.Remove(groupMembershipElement);

//            groupMembershipElement.Delete();

//            base.OnBeforeRemove(index, objectKey);
//        }

//        protected override void OnBeforeClear()
//        {
//            while (this.Count > 0)
//            {
//                this.Remove(this[0]);
//            }

//            base.OnBeforeClear();
//        }

//        #endregion |   Protected Overrided Methods   |

//        #region |   IManyToManySimpleObjectCollectionControl Interface   |

//        void IManyToManySimpleObjectCollectionControl.AddGroupMembershipElement(ManyToManyRelationElement groupMembershipElement)
//        {
//            this.AddGroupMembershipElement(groupMembershipElement);
//        }

//        void IManyToManySimpleObjectCollectionControl.RemoveGroupMembershipElement(ManyToManyRelationElement groupMembershipElement)
//        {
//            this.RemoveGroupMembershipElement(groupMembershipElement);
//        }

//        #endregion |   IManyToManySimpleObjectCollectionControl Interface   |

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
//            set { this[index] = value; }
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
//            this.ListInsert(index, item.Key);
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
//            this.ListRemoveAt(index);
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
//            this.ListAdd(item.Key);
//        }

//        /// <summary>
//        /// Removes all items from the System.Collections.Generic.ICollection<T>.
//        /// </summary>
//        /// <exception cref="System.NotSupportedException">The System.Collections.Generic.ICollection<T> is read-only.</exception>
//        void ICollection<T>.Clear()
//        {
//            this.ListClear();
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
//            return this.ListRemove(item.Key);
//        }

//        ///// <summary>
//        ///// Copies the elements of the <see cref="T:System.Collections.Generic.IList`1"></see> to an <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.
//        ///// </summary>
//        ///// <param name="array">The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.IList`1"></see>. The <see cref="T:System.Array"></see> must have zero-based indexing.</param>
//        ///// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
//        ///// <exception cref="T:System.ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
//        ///// <exception cref="T:System.ArgumentNullException">array is null.</exception>
//        ///// <exception cref="T:System.ArgumentException">array is multidimensional.-or-arrayIndex is equal to or greater than the length of array.-or-The number of elements in the source <see cref="T:System.Collections.Generic.IList`1"></see> is greater than the available space from arrayIndex to the end of the destination array.-or-Type T cannot be cast automatically to the type of the destination array.</exception>
//        //void ICollection<T>.CopyTo(T[] array, int arrayIndex)
//        //{
//        //    if (object.ReferenceEquals(array, null))
//        //    {
//        //        throw new ArgumentNullException("Null array reference", "array");
//        //    }

//        //    if (arrayIndex < 0)
//        //    {
//        //        throw new ArgumentOutOfRangeException("Index is out of range", "index");
//        //    }

//        //    if (array.Rank > 1)
//        //    {
//        //        throw new ArgumentException("Array is multi-dimensional", "array");
//        //    }

//        //    foreach (T t in this)
//        //    {
//        //        array.SetValue(t, arrayIndex);
//        //        arrayIndex++;
//        //    }
//        //}

//        /// <summary>
//        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.
//        /// </summary>
//        /// <value></value>
//        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only; otherwise, false.</returns>
//        bool ICollection<T>.IsReadOnly
//        {
//            get { return false; }
//        }

//        #endregion |   ICollection<T> Interface   |

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
//            set { this.ListSet(index, (value as T).Key); }
//        }

//        /// <summary>
//        /// Adds an item to the <see cref="System.Collections.IList"></see>.
//        /// </summary>
//        /// <param name="value">The System.Object to add to the <see cref="System.Collections.IList"></see>.</param>
//        /// <returns>The position into which the new element was inserted.</returns>
//        /// <exception cref="System.NotSupportedException">The <see cref="System.Collections.IList"></see> is read-only.-or- The <see cref="T:System.Collections.Generic.IList"></see> has a fixed size.</exception>
//        int IList.Add(object value)
//        {
//            return this.ListAdd((value as T).Key);
//        }

//        /// <summary>
//        /// Removes all items from the <see cref="System.Collections.IList"></see>.
//        /// </summary>
//        /// <exception cref="System.NotSupportedException">The <see cref="System.Collections.IList"></see> is read-only.</exception>
//        void IList.Clear()
//        {
//            this.ListClear();
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
//            int index = -1;

//            for (int i = 0; i < this.Count; i++)
//            {
//                if (this[i].Equals(value))
//                {
//                    index = i;
//                    break;
//                }
//            }
//            return index;
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
//            this.ListInsert(index, (item as T).Key);
//        }

//        /// <summary>
//        /// Removes the first occurrence of a specific object from the <see cref="System.Collections.IList"></see>.
//        /// </summary>
//        /// <param name="item">The System.Object to remove from the <see cref="System.Collections.IList"></see>.</param>
//        /// <exception cref="System.NotSupportedException">The <see cref="System.Collections.IList"></see> is read-only.-or- The <see cref="System.Collections.IList"></see> has a fixed size.</exception>
//        void IList.Remove(object item)
//        {
//            this.ListRemove((item as T).Key);
//        }

//        /// <summary>
//        /// Removes the <see cref="T:System.Collections.IList"></see> item at the specified index.
//        /// </summary>
//        /// <param name="index">The zero-based index of the item to remove.</param>
//        /// <exception cref="System.NotSupportedException">The <see cref="System.Collections.IList"></see> is read-only.-or- The <see cref="System.Collections.IList"></see> has a fixed size.</exception>
//        void IList.RemoveAt(int index)
//        {
//            this.ListRemoveAt(index);
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
//            get { return false; }
//        }

//        #endregion |   IList Interface   |
//    }

//    #region |   Interfaces   |

//    internal interface IManyToManySimpleObjectCollectionControl
//    {
//        void AddGroupMembershipElement(ManyToManyRelationElement groupMembershipElement);
//        void RemoveGroupMembershipElement(ManyToManyRelationElement groupMembershipElement);
//        void Clear();
//    }

//    #endregion |   Interfaces   |
//}
