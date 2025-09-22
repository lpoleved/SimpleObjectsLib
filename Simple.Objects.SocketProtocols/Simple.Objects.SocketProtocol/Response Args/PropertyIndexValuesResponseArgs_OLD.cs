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
//	[SystemResponseArgs((int)SystemRequest.GetObjectPropertyValues)]
//	public class PropertyIndexValuesResponseArgs_OLD : ResponseArgs, ISequenceSerializable
//	{
//		public PropertyIndexValuesResponseArgs_OLD()
//		{
//		}
		
//		public PropertyIndexValuesResponseArgs_OLD(int tableId, int[] propertyIndexes, object?[] propertyValues)
//		{
//			this.TableId = tableId;			
//			this.PropertyIndexes = propertyIndexes;
//			this.PropertyValues = propertyValues;
//        }

//		public int TableId { get; private set; }
//		public int[]? PropertyIndexes { get; private set; }
//		public object?[]? PropertyValues { get; private set; }

//		public override void WriteTo(ref SequenceWriter writer, object context)
//        {
//			base.WriteTo(ref writer, context);

//			if (context is ISimpleObjectSession session)
//			{
//				int propertyCount = 0;
//				int[] nonDefaultPropertyValueIndexes = new int[this.PropertyIndexes!.Length];
//				var serverObjectPropertyInfo = session.GetServerObjectModelInfo(this.TableId).GetAwaiter().GetResult();

//				//ServerPropertyInfo[] serverPropertyInfos = new ServerPropertyInfo[this.PropertyIndexes.Length];
//				//object[] propertyValuesToWrite = new object[this.PropertyIndexes.Length];

//				writer.WriteInt32Optimized(this.TableId);

//				for (int i = 0; i < this.PropertyIndexes.Length; i++)
//				{
//					int propertyIndex = this.PropertyIndexes[i];
//					object? propertyValue = this.PropertyValues![i];
//					ServerPropertyInfo serverPropertyInfo = serverObjectPropertyInfo![propertyIndex];

//					if (Comparison.IsEqual(propertyValue, serverPropertyInfo.DefaultValue)) // Exclude default values
//						continue;

//					nonDefaultPropertyValueIndexes[propertyCount++] = i;
//				}

//				writer.WriteInt32Optimized(propertyCount);

//				for (int i = 0; i < propertyCount; i++) // Writes only non default values
//				{
//					int index = nonDefaultPropertyValueIndexes[i];
//					int propertyIndex = this.PropertyIndexes[index];
//					object? propertyValue = this.PropertyValues![index];
//					ServerPropertyInfo serverPropertyInfo = serverObjectPropertyInfo![propertyIndex];

//					writer.WriteInt32Optimized(serverPropertyInfo.PropertyIndex);

//					if (serverPropertyInfo.IsSerializationOptimizable)
//						writer.WriteOptimized(serverPropertyInfo.PropertyTypeId, propertyValue);
//					else
//						writer.Write(serverPropertyInfo.PropertyTypeId, propertyValue);
//				}
//			}
//		}

//		public override void ReadFrom(ref SequenceReader reader, object context)
//		{
//			base.ReadFrom(ref reader, context);

//			if (context is ISimpleObjectSession session)
//			{
//				this.TableId = reader.ReadInt32Optimized();

//				var serverObjectPropertyInfo = session.GetServerObjectModelInfo(this.TableId).GetAwaiter().GetResult();
//				int propertyCount = reader.ReadInt32Optimized();

//				this.PropertyIndexes = new int[propertyCount];
//				this.PropertyValues = new object[propertyCount];

//				for (int i = 0; i < propertyCount; i++)
//				{
//					int propertyIndex = reader.ReadInt32Optimized();
//					ServerPropertyInfo serverPropertyInfo = serverObjectPropertyInfo![propertyIndex];
//					object? propertyValue;

//					if (serverPropertyInfo.IsSerializationOptimizable)
//						propertyValue = reader.ReadOptimized(serverPropertyInfo.PropertyTypeId);
//					else
//						propertyValue = reader.Read(serverPropertyInfo.PropertyTypeId);

//					this.PropertyIndexes[i] = propertyIndex;
//					this.PropertyValues[i] = propertyValue;
//				}
//			}
//		}
//	}
//}
