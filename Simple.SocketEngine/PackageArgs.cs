using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Serialization;

namespace Simple.SocketEngine
{
    public abstract class PackageArgs 
	{
		public PackageStatus Status { get; set; } = PackageStatus.OK;
		public string ErrorMessage { get; set; } = String.Empty;

		public virtual int GetBufferCapacity() => 24;

		public virtual void WriteTo(ref SequenceWriter writer, ISimpleSession session) { }
		public virtual void ReadFrom(ref SequenceReader reader, ISimpleSession session) { }


		internal void WriteErrorInfo(SequenceWriter writer)
		{
			writer.WriteInt32Optimized((int)this.Status);
			writer.WriteString(this.ErrorMessage);
		}

		internal void ReadErrorInfo(ref SequenceReader reader)
		{
			this.Status = (PackageStatus)reader.ReadInt32Optimized();
			this.ErrorMessage = reader.ReadString();
		}

		protected virtual int GetStringCapacity(string value)
		{
			return String.IsNullOrEmpty(value) ? 1 : value.Length + 2;
		}

		protected virtual int GetStringCapacity(IEnumerable<string> values)
		{
			int result = 4; // 4 bytes for value length

			foreach (string item in values)
				result += GetStringCapacity(item);

			return result;
		}
	}
}
