using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Modeling;
using Simple.Serialization;

namespace Simple.Objects
{
	public class DatastoreActionInfo : TransactionActionInfo
	{
		public DatastoreActionInfo()
		{
		}

		public DatastoreActionInfo(int tableId, long objectId, TransactionActionType actionType, PropertyIndexValuePair[]? propertyIndexValues)
			: base(tableId, objectId, actionType, propertyIndexValues)
		{
		}

		public DatastoreActionStatus DatastoreStatus { get; set; }

		public override void WriteTo(ref SequenceWriter writer, ServerObjectModelInfo objectModel)
		{
			base.WriteTo(ref writer, objectModel);

			writer.WriteBits((byte)this.DatastoreStatus, 2);
		}


		// Datastore rollback data will encode TypeId in sequence, to be imunne on property model change over the time.
		protected override void WritePropertyTypeId(ref SequenceWriter writer, int propertyTypeId) => writer.WriteInt32Optimized(propertyTypeId);

		protected override void WriteIsPropertySeriaizationOptimizable(ref SequenceWriter writer, bool isSeriazable) => writer.WriteBoolean(isSeriazable);


		public override void ReadFrom(ref SequenceReader reader, Func<int, ServerObjectModelInfo?> getServerObjectModel)
		{
			base.ReadFrom(ref reader, getServerObjectModel);

			this.DatastoreStatus = (DatastoreActionStatus)reader.ReadBits(2);
		}

		// Reads this info from stream, not from model, since model can be change over the time
		protected override int ReadPropertyTypeId(ref SequenceReader reader, Func<int, ServerObjectModelInfo?> getServerObjectModel, int propertyIndex) => reader.ReadInt32Optimized();
		protected override bool ReadIsProperySerializationOptimizable(ref SequenceReader reader, Func<int, ServerObjectModelInfo?> getServerObjectModel, int propertyIndex) => reader.ReadBoolean();

		protected override int GetPropertyTypeId(IServerPropertyInfo propertyModel)
		{
			return propertyModel.DatastoreTypeId;
		}
	}

	public enum DatastoreActionStatus
	{
		Unfinished = 0,
		Inserted = 1,
		Updated = 2,
		Deleted = 3
	}
}
