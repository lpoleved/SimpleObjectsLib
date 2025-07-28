//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Simple;
//using Simple.Collections;
//using Simple.Serialization;

//namespace Simple.Objects
//{
//	public class SimpleObjectSerializator
//	{
//		private static HashArray<Action<SerializationWriter, object>> WriteActionsByTypeId = new HashArray<Action<SerializationWriter, object>>(SimpleObjectManager.ObjectKeyNullableGuidTypeId);
//		private static HashArray<Func<SerializationReader, object>> ReadActionsByTypeId = new HashArray<Func<SerializationReader, object>>(SimpleObjectManager.ObjectKeyNullableGuidTypeId);

//		static SimpleObjectSerializator()
//		{
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.Object] = (writer, value) => writer.WriteObject((object)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.Boolean] = (writer, value) => writer.WriteBoolean((Boolean)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.Byte] = (writer, value) => writer.WriteValueTypeOptimized<Byte>((Byte)value, () => writer.WriteByte((Byte)value));
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.Int16] = (writer, value) => writer.WriteValueTypeOptimized<Int16>((Int16)value, () => writer.WriteInt16Optimized((Int16)value));

//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.Int32] = (writer, value) => writer.WriteInt32Optimized((Int32)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.Int64] = (writer, value) => writer.WriteInt64Optimized((Int64)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.BooleanArray] = (writer, value) => writer.WriteBooleanArrayOptimized((Boolean[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.ByteArray] = (writer, value) => writer.WriteByteArray((Byte[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.Int16Array] = (writer, value) => writer.WriteInt16ArrayOptimized((Int16[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.Int32Array] = (writer, value) => writer.WriteInt32ArrayOptimized((Int32[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.Int64Array] = (writer, value) => writer.WriteInt64ArrayOptimized((Int64[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableBoolean] = (writer, value) => writer.WriteNullableBoolean((Boolean?)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableByte] = (writer, value) => writer.WriteNullableByte((Byte?)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableInt16] = (writer, value) => writer.WriteNullableInt16Optimized((Int16?)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableInt32] = (writer, value) => writer.WriteNullableInt32Optimized((Int32?)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableInt64] = (writer, value) => writer.WriteNullableInt64Optimized((Int64?)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableBooleanArray] = (writer, value) => writer.WriteNullableBooleanArrayOptimized((Boolean?[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableByteArray] = (writer, value) => writer.WriteNullableByteArray((Byte?[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableInt16Array] = (writer, value) => writer.WriteNullableInt16ArrayOptimized((Int16?[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableInt32Array] = (writer, value) => writer.WriteNullableInt32ArrayOptimized((Int32?[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableInt64Array] = (writer, value) => writer.WriteNullableInt64ArrayOptimized((Int64?[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.SByte] = (writer, value) => writer.WriteSByte((SByte)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.UInt16] = (writer, value) => writer.WriteUInt16Optimized((UInt16)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.UInt32] = (writer, value) => writer.WriteUInt32Optimized((UInt32)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.UInt64] = (writer, value) => writer.WriteUInt64Optimized((UInt64)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.SByteArray] = (writer, value) => writer.WriteSByteArray((SByte[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.UInt16Array] = (writer, value) => writer.WriteUInt16ArrayOptimized((UInt16[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.UInt32Array] = (writer, value) => writer.WriteUInt32ArrayOptimized((UInt32[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.UInt64Array] = (writer, value) => writer.WriteUInt64ArrayOptimized((UInt64[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableSByte] = (writer, value) => writer.WriteNullableSByte((SByte?)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableUInt16] = (writer, value) => writer.WriteNullableUInt16Optimized((UInt16?)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableUInt32] = (writer, value) => writer.WriteNullableUInt32Optimized((UInt32?)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableUInt64] = (writer, value) => writer.WriteNullableUInt64Optimized((UInt64?)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableSByteArray] = (writer, value) => writer.WriteNullableSByteArray((SByte?[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableUInt16Array] = (writer, value) => writer.WriteNullableUInt16ArrayOptimized((UInt16?[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableUInt32Array] = (writer, value) => writer.WriteNullableUInt32ArrayOptimized((UInt32?[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableUInt64Array] = (writer, value) => writer.WriteNullableUInt64ArrayOptimized((UInt64?[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.Single] = (writer, value) => writer.WriteSingle((Single)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.Double] = (writer, value) => writer.WriteDouble((Double)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.Decimal] = (writer, value) => writer.WriteDecimalOptimized((Decimal)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.SingleArray] = (writer, value) => writer.WriteSingleArray((Single[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.DoubleArray] = (writer, value) => writer.WriteDoubleArray((Double[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.DecimalArray] = (writer, value) => writer.WriteDecimalArray((Decimal[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableSingle] = (writer, value) => writer.WriteNullableSingle((Single?)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableDouble] = (writer, value) => writer.WriteNullableDouble((Double?)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableDecimal] = (writer, value) => writer.WriteNullableDecimalOptimized((Decimal?)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableSingleArray] = (writer, value) => writer.WriteNullableSingleArray((Single?[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableDoubleArray] = (writer, value) => writer.WriteNullableDoubleArray((Double?[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableDecimalArray] = (writer, value) => writer.WriteNullableDecimalArrayOptimized((Decimal?[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.DateTime] = (writer, value) => writer.WriteDateTimeOptimized((DateTime)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.TimeSpan] = (writer, value) => writer.WriteTimeSpanOptimized((TimeSpan)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.DateTimeArray] = (writer, value) => writer.WriteDateTimeArrayOptimized((DateTime[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.TimeSpanArray] = (writer, value) => writer.WriteTimeSpanArrayOptimized((TimeSpan[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableDateTime] = (writer, value) => writer.WriteNullableDateTimeOptimized((DateTime?)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableTimeSpan] = (writer, value) => writer.WriteNullableTimeSpanOptimized((TimeSpan?)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableDateTimeArray] = (writer, value) => writer.WriteNullableDateTimeArrayOptimized((DateTime?[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableTimeSpanArray] = (writer, value) => writer.WriteNullableTimeSpanArrayOptimized((TimeSpan?[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.BitVector32] = (writer, value) => writer.WriteBitVector32Optimized((BitVector32)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.Guid] = (writer, value) => writer.WriteGuid((Guid)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.BitVector32Array] = (writer, value) => writer.WriteBitVector32ArrayOptimized((BitVector32[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.GuidArray] = (writer, value) => writer.WriteGuidArray((Guid[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableBitVector32] = (writer, value) => writer.WriteNullableBitVector32Optimized((BitVector32?)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableGuid] = (writer, value) => writer.WriteNullableGuid((Guid?)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableBitVector32Array] = (writer, value) => writer.WriteNullableBitVector32ArrayOptimized((BitVector32?[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableGuidArray] = (writer, value) => writer.WriteNullableGuidArray((Guid?[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.Char] = (writer, value) => writer.WriteChar((Char)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableChar] = (writer, value) => writer.WriteNullableChar((Char?)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.String] = (writer, value) => writer.WriteString((String)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.CharArray] = (writer, value) => writer.WriteCharArray((Char[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.NullableCharArray] = (writer, value) => writer.WriteNullableCharArray((Char?[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.StringArray] = (writer, value) => writer.WriteStringArray((String[])value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.BitArray] = (writer, value) => writer.WriteBitArray((BitArray)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.ArrayList] = (writer, value) => writer.WriteArrayList((ArrayList)value);
//			WriteActionsByTypeId[(int)ObjectTypes.TypeId.Type] = (writer, value) => writer.WriteTypeOptimized((Type)value);
//			WriteActionsByTypeId[SimpleObjectManager.ObjectKeyGuidTypeId] = SimpleObjectManager.WriteObjectKeyGuidOptimized;
//			WriteActionsByTypeId[SimpleObjectManager.ObjectKeyNullableGuidTypeId] = SimpleObjectManager.WriteNullableObjectKeyGuidOptimized;

//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.Object] = (reader) => reader.ReadObject();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.Boolean] = (reader) => reader.ReadBoolean();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.Byte] = (reader) => reader.ReadByte();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.Int16] = (reader) => reader.ReadInt16Optimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.Int32] = (reader) => reader.ReadInt32Optimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.Int64] = (reader) => reader.ReadInt64Optimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.BooleanArray] = (reader) => reader.ReadBooleanArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.ByteArray] = (reader) => reader.ReadByteArray();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.Int16Array] = (reader) => reader.ReadInt16ArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.Int32Array] = (reader) => reader.ReadInt32ArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.Int64Array] = (reader) => reader.ReadInt64ArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableBoolean] = (reader) => reader.ReadNullableBoolean();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableByte] = (reader) => reader.ReadNullableByte();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableInt16] = (reader) => reader.ReadNullableInt16Optimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableInt32] = (reader) => reader.ReadNullableInt32Optimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableInt64] = (reader) => reader.ReadNullableInt64Optimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableBooleanArray] = (reader) => reader.ReadNullableBooleanArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableByteArray] = (reader) => reader.ReadNullableByteArray();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableInt16Array] = (reader) => reader.ReadNullableInt16ArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableInt32Array] = (reader) => reader.ReadNullableInt32ArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableInt64Array] = (reader) => reader.ReadNullableInt64ArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.SByte] = (reader) => reader.ReadSByte();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.UInt16] = (reader) => reader.ReadUInt16Optimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.UInt32] = (reader) => reader.ReadUInt32Optimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.UInt64] = (reader) => reader.ReadUInt64Optimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.SByteArray] = (reader) => reader.ReadSByteArray();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.UInt16Array] = (reader) => reader.ReadUInt16ArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.UInt32Array] = (reader) => reader.ReadUInt32ArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.UInt64Array] = (reader) => reader.ReadUInt64ArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableSByte] = (reader) => reader.ReadNullableSByte();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableUInt16] = (reader) => reader.ReadNullableUInt16Optimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableUInt32] = (reader) => reader.ReadNullableUInt32Optimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableUInt64] = (reader) => reader.ReadNullableUInt64Optimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableSByteArray] = (reader) => reader.ReadNullableSByteArray();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableUInt16Array] = (reader) => reader.ReadNullableUInt16ArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableUInt32Array] = (reader) => reader.ReadNullableUInt32ArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableUInt64Array] = (reader) => reader.ReadNullableUInt64ArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.Single] = (reader) => reader.ReadSingle();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.Double] = (reader) => reader.ReadDouble();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.Decimal] = (reader) => reader.ReadDecimalOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.SingleArray] = (reader) => reader.ReadSingleArray();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.DoubleArray] = (reader) => reader.ReadDoubleArray();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.DecimalArray] = (reader) => reader.ReadDecimalArray();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableSingle] = (reader) => reader.ReadNullableSingle();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableDouble] = (reader) => reader.ReadNullableDouble();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableDecimal] = (reader) => reader.ReadNullableDecimalOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableSingleArray] = (reader) => reader.ReadNullableSingleArray();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableDoubleArray] = (reader) => reader.ReadNullableDoubleArray();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableDecimalArray] = (reader) => reader.ReadNullableDecimalArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.DateTime] = (reader) => reader.ReadDateTimeOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.TimeSpan] = (reader) => reader.ReadTimeSpanOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.DateTimeArray] = (reader) => reader.ReadDateTimeArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.TimeSpanArray] = (reader) => reader.ReadTimeSpanArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableDateTime] = (reader) => reader.ReadNullableDateTimeOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableTimeSpan] = (reader) => reader.ReadNullableTimeSpanOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableDateTimeArray] = (reader) => reader.ReadNullableDateTimeArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableTimeSpanArray] = (reader) => reader.ReadNullableTimeSpanArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.BitVector32] = (reader) => reader.ReadBitVector32Optimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.Guid] = (reader) => reader.ReadGuid();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.BitVector32Array] = (reader) => reader.ReadBitVector32ArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.GuidArray] = (reader) => reader.ReadGuidArray();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableBitVector32] = (reader) => reader.ReadNullableBitVector32Optimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableGuid] = (reader) => reader.ReadNullableGuid();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableBitVector32Array] = (reader) => reader.ReadNullableBitVector32ArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableGuidArray] = (reader) => reader.ReadNullableGuidArray();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.Char] = (reader) => reader.ReadChar();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableChar] = (reader) => reader.ReadNullableChar();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.String] = (reader) => reader.ReadStringOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.CharArray] = (reader) => reader.ReadCharArray();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.NullableCharArray] = (reader) => reader.ReadNullableCharArray();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.StringArray] = (reader) => reader.ReadStringArrayOptimized();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.BitArray] = (reader) => reader.ReadBitArray();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.ArrayList] = (reader) => reader.ReadArrayList();
//			ReadActionsByTypeId[(int)ObjectTypes.TypeId.Type] = (reader) => reader.ReadTypeOptimized(throwOnError: true);
//			ReadActionsByTypeId[SimpleObjectManager.ObjectKeyGuidTypeId] = SimpleObjectManager.ReadObjectKeyGuidOptimized;
//			ReadActionsByTypeId[SimpleObjectManager.ObjectKeyNullableGuidTypeId] = SimpleObjectManager.ReadNullableObjectKeyGuidOptimized;
//		}

//		public void WriteByTypeId(SerializationWriter writer, int objectTypeId, object value)
//		{
//			WriteActionsByTypeId[objectTypeId](writer, value);
//		}

//		public object ReadByTypeId(SerializationReader reader, int objectTypeId)
//		{
//			return ReadActionsByTypeId[objectTypeId](reader);
//		}

//		public void WriteValueType<T>(SerializationWriter writer, T value, Action writeNonDefaultAction)
//		{
//			if (EqualityComparer<T>.Default.Equals(value, default(T)))
//			{
//				writer.WriteBoolean(true); // default
//			}
//			else
//			{
//				writer.WriteBoolean(false); // is not default
//				writeNonDefaultAction();
//			}
//		}
//	}
//}
