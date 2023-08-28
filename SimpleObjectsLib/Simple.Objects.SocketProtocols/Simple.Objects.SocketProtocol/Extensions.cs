using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Modeling;
using Simple.Serialization;

namespace Simple.Objects.SocketProtocol
{
	public static class Extensions
	{

		public static void WtiteTo(this IEnumerable<PropertyIndexValuePair> propertyIndexValues, ref SequenceWriter writer, ServerObjectModelInfo serverObjectModel)
		{
			foreach (PropertyIndexValuePair item in propertyIndexValues!)
			{
				ServerPropertyInfo serverPropertyInfo = serverObjectModel![item.PropertyIndex];
				int propertyTypeId = serverPropertyInfo.IsEncrypted ? (int)PropertyTypeId.String : serverPropertyInfo.PropertyTypeId;

				writer.WriteInt32Optimized(item.PropertyIndex);

				if (serverPropertyInfo.IsSerializationOptimizable)
					writer.WriteOptimized(propertyTypeId, item.PropertyValue);
				else
					writer.Write(propertyTypeId, item.PropertyValue);
			}
		}

		public static void ReadFrom(this PropertyIndexValuePair[] propertyIndexValues, ref SequenceReader reader, ServerObjectModelInfo serverObjectModel)
		{
			for (int i = 0; i < propertyIndexValues.Length; i++)
			{
				int propertyIndex = reader.ReadInt32Optimized();
				ServerPropertyInfo serverPropertyInfo = serverObjectModel![propertyIndex];
				int propertyTypeId = serverPropertyInfo.IsEncrypted ? (int)PropertyTypeId.String : serverPropertyInfo.PropertyTypeId;

				object? propertyValue = (serverPropertyInfo.IsSerializationOptimizable) ? reader.ReadOptimized(propertyTypeId) :
																						  reader.Read(propertyTypeId);

				propertyIndexValues[i] = new PropertyIndexValuePair(propertyIndex, propertyValue);
			}

		}
	}
}
