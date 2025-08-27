//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple;

//namespace Simple.Objects
//{
//	public static class SortableSimpleObjectExtension
//	{
//		public static void SortByDefaultDatastoreSortingIfSortable<T>(this IList<T> collection) where T : SimpleObject
//		{
//			if (collection != null && collection.Count > 0 && collection[0].GetType().IsSubclassOf(typeof(SortableSimpleObject)))
//			{
//				int orderIndex = 0;
//				SortableSimpleObject previous = null;
//				SortableSimpleObject item = collection.FirstOrDefault(element => (element as SortableSimpleObject).IsFirst) as SortableSimpleObject;

//				if (collection.Count() > 0 && item == null)
//				{
//					SetOrderInSortableSimpleObjectCollection(collection, updateAndSaveObjects: true);
//				}
//				else
//				{
//					while (item != null)
//					{
//						item.OrderIndex = orderIndex++;
//						item.Previous = previous;
//						previous = item;
//						item = item.Next as SortableSimpleObject;

//						if (orderIndex > collection.Count)
//							break;
//					}

//					if (orderIndex != collection.Count())
//					{
//						if (orderIndex == collection.Count() - 1)
//						{
//							if ((collection.Last() as SortableSimpleObject).IsNew)
//							{
//								// Prevent sorting AsItIs if new element is inserted -> Set OrderIndex of the new element only
//								(collection.Last() as SortableSimpleObject).OrderIndex = orderIndex++;
//							}
//							else if (!(collection.Last() as SortableSimpleObject).IsDeleteStarted)
//							{
//								SetOrderInSortableSimpleObjectCollection(collection, updateAndSaveObjects: false);
//							}
//						}
//						else
//						{
//							SetOrderInSortableSimpleObjectCollection(collection, updateAndSaveObjects: false);
//						}
//					}
//				}

//				collection.Sort((s1, s2) => (s1 as SortableSimpleObject).OrderIndex.CompareTo((s2 as SortableSimpleObject).OrderIndex));
//				//collection = collection.OrderBy(element => (element as SortableSimpleObject).OrderIndex);
//			}
//		}

//		//public static void SortByDefaultDatastoreSorting<T>(this IEnumerable<T> collection)
//		//{
//		//	int orderIndex = 0;
//		//	SortableSimpleObject previous = null;
//		//	SortableSimpleObject item = collection.FirstOrDefault(element => (element as SortableSimpleObject).IsFirst) as SortableSimpleObject;

//		//	if (collection.Count() > 0 && item == null)
//		//	{
//		//		SetSortOrderingAsItIs(collection);
//		//	}
//		//	else
//		//	{
//		//		while (item != null)
//		//		{
//		//			item.OrderIndex = orderIndex++;
//		//			item.Previous = previous;
//		//			previous = item;
//		//			item = item.Next;
//		//		}

//		//		if (orderIndex != collection.Count())
//		//		{
//		//			if (orderIndex == collection.Count() - 1)
//		//			{
//		//				if (collection.Last().IsNew)
//		//				{
//		//					// Prevent sorting AsItIs if new element is inserted -> Set OrderIndex of the new element only
//		//					(collection.Last() as SortableSimpleObject).OrderIndex = orderIndex++;
//		//				}
//		//				else if (!collection.Last().IsDeleting)
//		//				{
//		//					SetSortOrderingAsItIs(collection);
//		//				}
//		//			}
//		//			else
//		//			{
//		//				SetSortOrderingAsItIs(collection);
//		//			}
//		//		}
//		//	}

//		//	//this.InnerList.Sort((s1, s2) => (s1 as SortableSimpleObject).OrderIndex.CompareTo((s2 as SortableSimpleObject).OrderIndex));

//		//	collection.OrderBy(i => (i.OrderIndex);
//		//}

//		private static void SetOrderInSortableSimpleObjectCollection<T>(IList<T> collection, bool updateAndSaveObjects) where T : SimpleObject
//		{
//			int orderIndex = 0;
//			SortableSimpleObject item = null;
//			SortableSimpleObject firstItem = null;
//			SortableSimpleObject previous = null;

//			foreach (T element in collection)
//			{
//				item = element as SortableSimpleObject;

//				if (orderIndex == 0)
//				{
//					item.Previous = null;
					
//					if (updateAndSaveObjects)
//					{
//						firstItem = item;
//						item.IsFirst = true;
//					}
//				}
//				else
//				{
//					item.Previous = previous;
					
//					if (updateAndSaveObjects)
//					{
//						item.IsFirst = false;
//						previous.Next = item;
//						firstItem.RelatedTransactionRequests.Add(new TransactionActionRequest(item, DatastoreRequestAction.Save));
//					}
//				}

//				item.OrderIndex = orderIndex++;
//				previous = item;
//			}

//			if (updateAndSaveObjects && item != null)
//				item.Next = null;

//			if (updateAndSaveObjects && firstItem != null)
//				firstItem.Save();
//		}
//	}
//}
