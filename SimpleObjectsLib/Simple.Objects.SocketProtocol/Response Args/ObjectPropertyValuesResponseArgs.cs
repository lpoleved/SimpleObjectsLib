using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Collections;
using Simple.Modeling;
using Simple.Objects;
using Simple.Serialization;
using Simple.SocketEngine;

namespace Simple.Objects.SocketProtocol
{
	[SystemResponseArgs((int)SystemRequest.GetObjectPropertyValues)]

	public class ObjectPropertyValuesResponseArgs : ResponseArgs
	{
        public ObjectPropertyValuesResponseArgs()
        {
        }

		public ObjectPropertyValuesResponseArgs(int tableId, IEnumerable<PropertyIndexValuePair>? propertyIndexValues)
		{
			this.TableId = tableId;
			this.PropertyIndexValues = propertyIndexValues;
        }
		public int TableId { get; private set; }

		public IEnumerable<PropertyIndexValuePair>? PropertyIndexValues { get; private set; }

		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
        {
			base.WriteTo(ref writer, session);

			if (session is ISimpleObjectSession simpleObjectSession)
			{
				if (this.PropertyIndexValues != null)
				{
					writer.WriteBoolean(true); // The SimpleObject exists
					
					var serverObjectModel = simpleObjectSession.GetServerObjectModel(this.TableId);

					writer.WriteInt32Optimized(this.TableId);
					writer.WriteInt32Optimized(this.PropertyIndexValues!.Count());
					this.PropertyIndexValues?.WtiteTo(ref writer, serverObjectModel!);
				}
				else
				{
					writer.WriteBoolean(false); // The SimpleObject is null and not exists!
				}
			}
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
        {
			base.ReadFrom(ref reader, session);

			if (session is ISimpleObjectSession simpleObjectSession)
			{
				if (reader.ReadBoolean()) // The SimpleObject exists
				{
					this.TableId = reader.ReadInt32Optimized();

					var serverObjectModel = simpleObjectSession.GetServerObjectModel(this.TableId);
					PropertyIndexValuePair[] propertyIndexValues = new PropertyIndexValuePair[reader.ReadInt32Optimized()];

					propertyIndexValues.ReadFrom(ref reader, serverObjectModel!);
					this.PropertyIndexValues = propertyIndexValues;
				}
				else
				{
					this.PropertyIndexValues = null; // The SimpleObject does not exits
				}
			}
		}
	}
}
