using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Simple;
using Simple.Serialization;
using Simple.Modeling;

namespace Simple.Objects
{
	public sealed class SystemPropertySequence : SystemObject<int, SystemPropertySequence>, IPropertySequence
	{
		//private IPropertyModel[] modelSequence = null;

		static SystemPropertySequence()
		{
			Model.TableInfo = SystemTablesBase.SystemPropertySequences;
			Model.AutoGenerateKey = true;
		}

		public static byte[] ToValueData(int[] propertyIndexSequence, int[] propertyTypeIdSequence)
		{
			BufferSequenceWriter bufferSequenceWriter = new BufferSequenceWriter(propertyIndexSequence.Length * 2 + 4);
			SerialWriter writer = new SerialWriter(bufferSequenceWriter);

			writer.WriteInt32Optimized(propertyIndexSequence.Length);

			for (int i = 0; i < propertyIndexSequence.Length; i++)
			{
				writer.WriteInt32Optimized(propertyIndexSequence[i]);
				writer.WriteInt32Optimized(propertyTypeIdSequence[i]);
			}

			return bufferSequenceWriter.ToArray();
		}

		public SystemPropertySequence()
		{
		}

		public SystemPropertySequence(SimpleObjectManager objectManager, ref SystemObjectCollectionByObjectKey<int, SystemPropertySequence> dictionaryCollection, int[] propertyIndexSequence, int[] propertyTypeIdSequence, byte[] valueData)
			: base(objectManager, ref dictionaryCollection,
				  (item) =>
				  {
					  item.PropertyIndexes = propertyIndexSequence;
					  item.PropertyTypeIds = propertyTypeIdSequence;
					  item.ValueData = valueData;
				  })
		{
		}

		[ObjectKey]
		public int PropertySequenceId { get; private set; }

		public byte[]? ValueData { get; private set; }

		[NonStorable]
		public int[]? PropertyIndexes { get; private set; }

		[NonStorable]
		public int[]? PropertyTypeIds { get; private set; }

		[NonStorable]
		public int Length => this.PropertyIndexes?.Length ?? 0;

		protected override void OnLoad()
		{
			base.OnLoad();

			SerialReader reader = new SerialReader(this.ValueData);

			this.PropertyIndexes = new int[reader.ReadInt32Optimized()];
			this.PropertyTypeIds = new int[this.PropertyIndexes.Length];

			for (int i = 0; i < this.PropertyIndexes.Length; i++)
			{
				this.PropertyIndexes[i] = reader.ReadInt32Optimized();
				this.PropertyTypeIds[i] = reader.ReadInt32Optimized();
			}
		}
	}

	public interface IPropertySequence
	{
		//int PropertySequenceId { get; }
		//IPropertyModel[] ModelSequence { get; }
		int[] PropertyIndexes { get; }
		int[] PropertyTypeIds { get; }
		int Length { get; }
		//IPropertyModel GetPropertyModel(int propertyIndex);
	}
}
