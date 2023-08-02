using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Objects;
using Simple.Collections;
using Simple.Serialization;
using Simple.SocketEngine;

namespace Simple.Objects.SocketProtocol
{
	[SystemResponseArgs((int)SystemRequest.GetOneToManyForeignObjectCollection)]
	[SystemResponseArgs((int)SystemRequest.GetObjectIdsTEMP)]
	public class ObjectIdsResponseArgs : ResponseArgs
    {
        public ObjectIdsResponseArgs()
        {
        }

		public ObjectIdsResponseArgs(IList<long> objectIds)
        {
            this.ObjectIds = objectIds;
        }

        /// <summary>
        /// Get the Object Id's.
        /// </summary>
        public IList<long>? ObjectIds { get; private set; }

        public override int GetBufferCapacity()
        {
            return base.GetBufferCapacity();
        }

        public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
        {
			base.WriteTo(ref writer, session);

			writer.WriteInt32Optimized(this.ObjectIds!.Count);

			foreach (long objectId in this.ObjectIds)
				writer.WriteInt64Optimized(objectId);
        }

        public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
        {
            base.ReadFrom(ref reader, session);

            int count = reader.ReadInt32Optimized();
            
            this.ObjectIds = new List<long>(count);

            for (int i = 0; i < count; i++)
                this.ObjectIds.Add(reader.ReadInt64Optimized());
        }
    }
}
