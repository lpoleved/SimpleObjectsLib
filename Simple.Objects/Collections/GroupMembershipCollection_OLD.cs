//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple;
//using Simple.Collections;

//namespace Simple.Objects
//{
//    public class GroupMembershipCollection<T> : IEnumerable<T>, IEnumerable, IList, IGroupMembershipInternal 
//        where T : SimpleObject
//    {
//        #region |   Private Members   |

//        private SimpleObject owner = null;
//        private int relationKey;
//        private ChangeContainer changeContainer = null;
//        int tableId = 0;
//        private List<long> objectIds = null;
//        private SimpleObjectCollection<GroupMembershipElement> groupMembershipElements = null;
//        //internal bool isOwnerObjectIdFirst = false;
//        //private ReadOnlyList<T> readOnlyList = null;
//        private readonly object lockObject = new object();

//        #endregion |   Private Members   |

//        #region |   Constructor(s) and Initialization   |

//        public GroupMembershipCollection(SimpleObject owner, int manayToManyRelationKey)
//            : this(owner, manayToManyRelationKey, owner.ChangeContainer)
//		{
//		}

//        public GroupMembershipCollection(SimpleObject owner, int manayToManyRelationKey, ChangeContainer changeContainer)
//        {
//            this.owner = owner;
//            this.relationKey = manayToManyRelationKey;
//            this.changeContainer = changeContainer;
//            //this.GroupMembershipElements = manyToManyRelationElements;
//            this.tableId = this.owner.GetModel().TableInfo.TableId; //owner.Manager.GetObjectModel(typeof(T)).TableInfo.TableId;
//            //this.objectIds = objectIds;
//            //this.isOwnerObjectIdFirst = isOwnerObjectIdFirst;

//            this.groupMembershipElements = this.owner.Manager.GetObjectCache(GroupMembershipElementModel.TableId)
//                                                             .Select<GroupMembershipElement>(gme => gme.RelationKey == this.RelationKey && ((gme.Object1TableId == tableId && gme.Object1Id == this.owner.Id) ||
//                                                                                                                                            (gme.Object2TableId == tableId && gme.Object2Id == this.owner.Id)));
//            this.objectIds = new List<long>(this.groupMembershipElements.Count);

//            foreach (GroupMembershipElement groupMembershipElement in this.groupMembershipElements)
//			{
//                long objectId = (groupMembershipElement.Object1TableId == this.tableId) ? groupMembershipElement.Object1Id : groupMembershipElement.Object2Id;

//                this.objectIds.Add(objectId);
//            }


//            //foreach (ManyToManyElement manyToManyRelationElement in manyToManyRelationElements)
//            //{
//            //    T relatedSimpleObject = (this.isOwnerObjectIdFirst) ? manyToManyRelationElement.SimpleObject2 as T : manyToManyRelationElement.SimpleObject1 as T;

//            //    this.Add(this.tableId, relatedSimpleObject.Id);
//            //}
//        }

//        //internal GroupMembershipObjectCollection(SimpleObject owner, ManyToManyRelation manayToManyRelation, IList<ManyToManyElement> manyToManyRelationElements, IList<long> objectIds, IList<T> innerListToWrap, bool isOwnerObjectIdFirst)
//        //    : base(owner.Manager, innerListToWrap)
//        //{
//        //    this.owner = owner;
//        //    this.manayToManyRelation = manayToManyRelation;
//        //    this.ManyToManyRelationElements = manyToManyRelationElements;
//        //    this.isOwnerObjectIdFirst = isOwnerObjectIdFirst;
//        //}

//        #endregion |   Constructor(s) and Initialization   |

//        #region |   Public Properties   |

//        ///// <summary>
//        ///// Gets or sets the element at the specified index.
//        ///// </summary>
//        ///// <param name="index">The zero-based index of the element to get.</param>
//        ///// <returns>The element at the specified index.</returns>
//        ///// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero.  -or- index is equal to or greater than <see cref="ManyToManySimpleObjectCollection&lt;T&gt;"/>.Count.</exception>
//        ///// <exception cref="System.NotSupportedException">the property is set and the <see cref="T:System.Collections.Generic.IList`1"></see> is read-only.</exception>
//        //public new T this[int index]
//        //{
//        //    get { return this.ListGet(index) as T; }
//        //    set { this.ListSet(index, value.Key); }
//        //}

//        public T this[int index] => this.owner.Manager.GetObject(this.tableId, this.objectIds[index]) as T;

//        public int Count => this.objectIds.Count;
//        public int RelationKey => this.relationKey;

//        //public bool IsOwnerObjectIdFirst => this.isOwnerObjectIdFirst;

//        #endregion |   Public Properties   |

//        #region |   Public Methods   |

//        public void Add(T item)
//		{
//            lock (this.lockObject)
//            {
//                //SimpleObject simpleObject1 = (this.isOwnerObjectIdFirst) ? this.owner : item;
//                //SimpleObject simpleObject2 = (this.isOwnerObjectIdFirst) ? item : this.owner;
//                GroupMembershipElement groupMembershipElement = new GroupMembershipElement(this.owner.Manager, this.RelationKey, null, this.owner, item, this.changeContainer);

//                this.GroupMembershipElements.Add(GroupMembershipElementModel.TableId, groupMembershipElement.Id);
//                this.objectIds.Add(item.Id);
//                item.AddToManyToManyObjectCollectionIfInCache(groupMembershipElement);
                
//                groupMembershipElement.SaveIfNeeded();
//            }
//		}

//        public void Remove(T item)
//		{
//            lock (this.lockObject)
//            {
//                int index = this.objectIds.IndexOf(item.Id);

//                this.RemoveAt(index);
//            }
//        }

//        public void RemoveAt(int index)
//		{
//            lock (this.lockObject)
//            {
//                long objectId = this.objectIds[index];
//                //SimpleObject simpleObject = this.owner.Manager.GetObject(this.tableId, objectId);

//                GroupMembershipElement manyToManyRelationElement = this.GroupMembershipElements[index];
//                SimpleObject relatedSimpleObject = (manyToManyRelationElement.Object1TableId == this.tableId) ? manyToManyRelationElement.SimpleObject2 : manyToManyRelationElement.SimpleObject1;

//                relatedSimpleObject.RemoveFromManyToManyObjectCollectionIfInCache(manyToManyRelationElement);
//                this.GroupMembershipElements.RemoveAt(index);
//                this.objectIds.RemoveAt(index);

//                manyToManyRelationElement.SaveIfNeeded();
//            }
//        }

//        public void Clear()
//		{
//            lock (this.lockObject)
//            {
//                while (this.Count > 0)
//                    this.RemoveAt(0);
//            }
//        }

//        /// <summary>
//        /// Returns an enumerator that iterates through a collection.
//        /// </summary>
//        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.</returns>
//        public IEnumerator<T> GetEnumerator()
//        {
//            lock (this.lockObject)
//            {
//                for (int index = 0; index < this.Count; index++)
//                    yield return this[index];
//                //return this.collectionContainer.GetEnumerator();
//            }
//        }



//        #endregion |   Public Methods   |

//        #region |   Protected Properties   |

//        /// <summary>
//        /// Gets the List of the GroupMembershipElement members.
//        /// </summary>
//        protected internal SimpleObjectCollection<GroupMembershipElement> GroupMembershipElements => this.groupMembershipElements;


//		#endregion |   Protected Properties   |

//		#region |   Protected Methods   |

//		protected void AddGroupMembershipElement(GroupMembershipElement groupMembershipElement)
//        {
//        }

//        protected void RemoveGroupMembershipElement(GroupMembershipElement groupMembershipElement)
//        {
//        }

//        #endregion |   Protected Methods   |

//        #region |   IGroupMembershipInternal Interface   |

//        void IGroupMembershipInternal.AddInternal(long groupMembershipElementId, long objectId)
//		{
//            this.groupMembershipElements.Add(groupMembershipElementId);
//            this.objectIds.Add(objectId);
//        }

//        void IGroupMembershipInternal.RemoveInternal(long groupMembershipElementId)
//		{
//            int index = this.groupMembershipElements.IndexOf(groupMembershipElementId);

//            this.groupMembershipElements.RemoveAt(index);
//            this.objectIds.RemoveAt(index);
//        }

//        #endregion |   IGroupMembershipInternal Interface   |

//        #region |   IList Interface   |

//        object IList.this[int index] { get => this[index]; set => throw new NotImplementedException(); }

//        bool IList.IsReadOnly => false;

//        bool IList.IsFixedSize => throw new NotImplementedException();

//        int ICollection.Count => this.Count;

//        object ICollection.SyncRoot => throw new NotImplementedException();

//        bool ICollection.IsSynchronized => throw new NotImplementedException();

//        int IList.Add(object value)
//        {
//            lock (this.lockObject)
//            {
//                int index = this.Count;

//                this.Add(value as T);

//                return index;
//            }
//        }

//        bool IList.Contains(object value) => this.objectIds.IndexOf((value as T).Id) >= 0;

//        void IList.Clear() => throw new NotImplementedException();

//        int IList.IndexOf(object value) => this.objectIds.IndexOf((value as T).Id);

//        void IList.Insert(int index, object value) => throw new NotImplementedException();

//        void IList.Remove(object value) => this.Remove(value as T);

//        void IList.RemoveAt(int index) => this.RemoveAt(index);

//        void ICollection.CopyTo(Array array, int index) => throw new NotImplementedException();

//        #endregion |   IList Interface   |

//        #region |   IEnumerable Interface   |

//        /// <summary>
//        /// Returns an enumerator that iterates through a collection.
//        /// </summary>
//        /// <returns>An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.</returns>
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return this.GetEnumerator();
//            //for (int index = 0; index < this.Count; index++)
//            //	yield return this[index];
//        }
//        #endregion |   IEnumerable Interface   |
//    }

//    #region |   Interfaces   |

//    /// <summary>
//    /// Used internaly when related GroiupMembershipCollection is 
//    /// </summary>
//    internal interface IGroupMembershipInternal
//	{
//		void AddInternal(long groupMembershipElementId, long objectId);
//		void RemoveInternal(long groupMembershipElementId);
//		//void Clear();
//		//IList<ManyToManyElement> ManyToManyRelationElements { get; }
//		//bool IsOwnerObjectIdFirst { get; }
//	}

//	#endregion |   Interfaces   |
//}
