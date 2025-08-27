//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple.Collections;
//using Simple.Modeling;
//using Simple.Objects;
//using Simple.Serialization;
//using Simple.SocketEngine;

//namespace Simple.Objects.SocketProtocol
//{
//	[SystemResponseArgs((int)SystemRequest.GetOneToManyForeignObjectCollection)]

//	public class ObjectCollectionResponseArgs_OLD : ResponseArgs
//	{
//        public ObjectCollectionResponseArgs_OLD()
//        {
//        }

//		public ObjectCollectionResponseArgs_OLD(ObjectCollectionElement_OLD[] objectCollectionElements)
//		{
//			this.ObjectCollectionElements = objectCollectionElements;
//        }

//		public ObjectCollectionElement_OLD[]? ObjectCollectionElements { get; private set; }

//		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
//        {
//			base.WriteTo(ref writer, session);

//			if (session is ISimpleObjectSession simpleObjectSession)
//			{
//				writer.WriteInt32Optimized(this.ObjectCollectionElements!.Length);

//				foreach (var item in this.ObjectCollectionElements!)
//				{
//					var serverObjectModel = simpleObjectSession.GetServerObjectModel(item.TableId);

//					writer.WriteInt32Optimized(item.TableId);
//					writer.WriteInt32Optimized(item.PropertyIndexValues.Count());
//					item.PropertyIndexValues.WtiteTo(ref writer, serverObjectModel);
//				}
//			}
//		}

//		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
//        {
//			base.ReadFrom(ref reader, session);

//			if (session is ISimpleObjectSession simpleObjectSession)
//			{
//				ObjectCollectionElement_OLD[] objectCollectionElements = new ObjectCollectionElement_OLD[reader.ReadInt32Optimized()];

//				for (int i = 0; i < objectCollectionElements.Length; i++)
//				{
//					int tableId = reader.ReadInt32Optimized();
//					var serverObjectModel = simpleObjectSession.GetServerObjectModel(tableId);
//					PropertyIndexValuePair[] propertyIndexValues = new PropertyIndexValuePair[reader.ReadInt32Optimized()];

//					propertyIndexValues.ReadFrom(ref reader, serverObjectModel);
//					objectCollectionElements[i] = new ObjectCollectionElement_OLD(tableId, propertyIndexValues);
//				}

//				this.ObjectCollectionElements = objectCollectionElements;
//			}
//		}
//	}
//}
