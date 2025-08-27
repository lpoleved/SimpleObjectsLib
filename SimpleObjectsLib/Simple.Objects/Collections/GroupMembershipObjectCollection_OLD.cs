//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple;
//using Simple.Collections;

//namespace Simple.Objects
//{
//    public class GroupMembershipObjectCollection<T> : SimpleObjectCollection<T>, IEnumerable<T>, IEnumerable, IManyToManyCollectionControl
//        where T : SimpleObject
//    {
//        #region |   Private Members   |

//        private SimpleObject owner = null;
//        private ManyToManyRelation manayToManyRelation = null;
//        internal bool isOwnerObjectIdFirst = false;
//        //private ReadOnlyList<T> readOnlyList = null;
//        private ChangeContainer changeContainer = null;

//        #endregion |   Private Members   |

//        #region |   Constructor(s) and Initialization   |

//        public GroupMembershipObjectCollection(SimpleObject owner, ManyToManyRelation manayToManyRelation, IList<GroupMembershipElement> manyToManyRelationElements, IList<long> objectIds, bool isOwnerObjectIdFirst, ChangeContainer changeContainer)
//            : base(owner.Manager, owner.Manager.GetObjectModel(typeof(T)).TableInfo.TableId, objectIds)
//        {
//            this.owner = owner;
//            this.manayToManyRelation = manayToManyRelation;
//            this.ManyToManyRelationElements = manyToManyRelationElements;
//            this.isOwnerObjectIdFirst = isOwnerObjectIdFirst;
//            this.changeContainer = changeContainer;

//            foreach (GroupMembershipElement manyToManyRelationElement in manyToManyRelationElements)
//            {
//                T relatedSimpleObject = (this.isOwnerObjectIdFirst) ? manyToManyRelationElement.SimpleObject2 as T : manyToManyRelationElement.SimpleObject1 as T;
                
//                this.Add(relatedSimpleObject.GetModel().TableInfo.TableId, relatedSimpleObject.Id);
//            }
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

//        public bool IsOwnerObjectIdFirst { get { return this.isOwnerObjectIdFirst; } }

//        #endregion |   Public Properties   |

//        #region |   Public Methods   |

//        public void Add(T item)
//		{
//            lock (this.lockObject)
//            {
//                SimpleObject simpleObject1 = (this.isOwnerObjectIdFirst) ? this.owner : item;
//                SimpleObject simpleObject2 = (this.isOwnerObjectIdFirst) ? item : this.owner;
//                GroupMembershipElement manyToManyRelationElement = new GroupMembershipElement(this.owner.Manager, this.manayToManyRelation.RelationKey, this.manayToManyRelation, simpleObject1, simpleObject2, this.changeContainer);

//                this.ManyToManyRelationElements.Add(manyToManyRelationElement);
//                item.AddToManyToManyObjectCollectionIfInCache(manyToManyRelationElement);
//                manyToManyRelationElement.SaveIfNeeded();

//                base.Add(item.Id);
//            }
//		}

//        public void Remove(T item)
//		{
//            lock (this.lockObject)
//            {
//                int index = this.IndexOf(item.Id);
//                GroupMembershipElement manyToManyRelationElement = this.ManyToManyRelationElements[index];
//                SimpleObject relatedSimpleObject = this.isOwnerObjectIdFirst ? manyToManyRelationElement.SimpleObject2 : manyToManyRelationElement.SimpleObject1;

//                relatedSimpleObject.RemoveFromManyToManyObjectCollectionIfInCache(manyToManyRelationElement);
//                this.ManyToManyRelationElements.Remove(manyToManyRelationElement);

//                base.RemoveAt(index);

//                manyToManyRelationElement.RequestDelete();
//                manyToManyRelationElement.Manager.CommitChanges();
//            }
//        }

//        public new void Clear()
//		{
//            lock (this.lockObject)
//            {

//                while (this.Count > 0)
//                    this.RemoveAt(0);

//                base.Clear();
//            }
//        }

//        #endregion |   Public Methods   |

//        #region |   Protected Properties   |

//        /// <summary>
//        /// Gets the List of the GroupMembershipElement members.
//        /// </summary>
//        protected internal IList<GroupMembershipElement> ManyToManyRelationElements { get; private set; }

//        #endregion |   Protected Properties   |

//        #region |   Protected Methods   |

//        protected void AddGroupMembershipElement(GroupMembershipElement groupMembershipElement)
//        {
//        }

//        protected void RemoveGroupMembershipElement(GroupMembershipElement groupMembershipElement)
//        {
//        }

//        #endregion |   Protected Methods   |

//        #region |   IManyToManySimpleObjectCollectionControl Interface   |

//        void IManyToManyCollectionControl.AddGroupMembershipElement(GroupMembershipElement groupMembershipElement)
//        {
//            this.ManyToManyRelationElements.Add(groupMembershipElement);
//            SimpleObject simpleObject = (this.isOwnerObjectIdFirst) ? groupMembershipElement.SimpleObject2 : groupMembershipElement.SimpleObject1;

//            lock (this.lockObject)
//            {
//                this.Add(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id);
//            }
//        }

//        void IManyToManyCollectionControl.RemoveGroupMembershipElement(GroupMembershipElement groupMembershipElement)
//        {
//            this.ManyToManyRelationElements.Remove(groupMembershipElement);
//            SimpleObject simpleObject = (this.isOwnerObjectIdFirst) ? groupMembershipElement.SimpleObject2 : groupMembershipElement.SimpleObject1;

//            lock (this.lockObject)
//            {
//                this.Remove(simpleObject.GetModel().TableInfo.TableId, simpleObject.Id);
//            }
//        }

//        IList<GroupMembershipElement> IManyToManyCollectionControl.ManyToManyRelationElements
//        {
//            get { return this.ManyToManyRelationElements; }
//        }

//        void IManyToManyCollectionControl.Clear()
//		{
//            this.Clear();
//		}

//        #endregion |   IManyToManySimpleObjectCollectionControl Interface   |
//    }

//    #region |   Interfaces   |

//    internal interface IGroupMembershipCollectionControl
//    {
//        void AddGroupMembershipElement(GroupMembershipElement groupMembershipElement);
//        void RemoveGroupMembershipElement(GroupMembershipElement groupMembershipElement);
//        void Clear();
//        IList<GroupMembershipElement> ManyToManyRelationElements { get; }
//        bool IsOwnerObjectIdFirst { get; }
//    }

//    #endregion |   Interfaces   |
//}
