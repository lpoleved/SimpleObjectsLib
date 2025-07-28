using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using SuperSocket.ProtoBase;
using System.Buffers;
using System.Diagnostics.Eventing.Reader;
using System.IO.Packaging;
using Simple.Serialization;

namespace Simple.SocketEngine
{
	public class PackageReader : IPackageDecoder<PackageReader>, IKeyedPackageInfo<int>
	{
		private long packageDataLength = 0;
		private int packageLengthSize = 0;
		private bool packageLengthReceived = false;
		private long totalPackageLength = 0;
		private PackageArgsFactory packageArgsFactory;
		private bool createDataCopy;

		private static readonly TryGetInt64By7BitEncodingDelegate tryGetInt64By7BitEncoding;

		/// <summary>
		/// TryGetPackageLengthBy7BitEncoding endianness delegate
		/// </summary>
		/// <param name="reader">The reader</param>
		/// <param name="value">The length of the package.</param>
		/// <returns></returns>
		private delegate bool TryGetInt64By7BitEncodingDelegate(ref SequenceReader<byte> reader, ref long value, ref int bytesConsumed);

		static PackageReader()
		{
			if (BitConverter.IsLittleEndian)
				tryGetInt64By7BitEncoding = TryGetInt64By7BitLittleEndianEncoding;
			else
				tryGetInt64By7BitEncoding = TryGetInt64By7BitBigEndianEncoding;
		}


		public PackageReader(PackageArgsFactory packageArgsFactory, bool createDataCopy = false)
        {
			this.packageArgsFactory = packageArgsFactory;
			this.createDataCopy = createDataCopy;
		}

		internal bool CreateDataCopy { get => this.createDataCopy; set => this.createDataCopy = value; }

		internal bool IsPackageLengthReceived(ref SequenceReader<byte> reader)
		{
			if (!this.packageLengthReceived)
			{
				this.packageLengthReceived = TryGetInt64By7BitEncoding(ref reader, ref this.packageDataLength, ref this.packageLengthSize);

				if (this.packageLengthReceived)
					this.totalPackageLength = this.packageDataLength + this.packageLengthSize;
			}

			return this.packageLengthReceived;
		}

		public long PackageLength => this.packageDataLength;
		public int PackageLengthSize => this.packageLengthSize;

		public long TotalPackageLength => this.totalPackageLength;

		public HeaderInfo HeaderInfo { get; set; }
		//public int HeaderSize { get; set; }


		/// <summary>
		/// The package key.
		/// If package type is message it is message code.
		/// If package is request it is request id.
		/// </summary>
		public int Key { get; set; }

		//public int Token { get; set; }

		public PackageArgs? PackageArgs { get; set; }

		public byte[]? Buffer { get; set; } = null;

		public PackageReader Decode(ref ReadOnlySequence<byte> sequence, object context)
		{
			SequenceReader<byte> reader = new SequenceReader<byte>(sequence);

			return this.Decode(ref reader, context as ISimpleSession);
		}

		public PackageReader Decode(ref SequenceReader<byte> buffer, ISimpleSession? session)
		{
			if (this.createDataCopy)
				this.Buffer = buffer.Sequence.ToArray();

			//if (context is ISimpleSession session)
			//{
				//SerializationReader reader = new SerializationReader(ref buffer);
				SequenceReader reader = new SequenceReader(ref buffer);

				//reader.AdvancePosition(this.PackageLengthSize); // Skip reading package data length again
				
				this.HeaderInfo = new HeaderInfo(reader.ReadUInt64Optimized());
				this.Key = reader.ReadInt32Optimized(); // package is response => package.Key is RequestId

				if (this.HeaderInfo.PackageType == PackageType.Request)
				{
					//this.Token = reader.ReadInt32Optimized();
					this.PackageArgs = (this.HeaderInfo.IsSystem) ? this.packageArgsFactory.CreateSystemRequestArgs(this.Key) :
																	this.packageArgsFactory.CreateRequestArgs(this.Key);
				}
				else if (this.HeaderInfo.PackageType == PackageType.Response)
				{
					//this.Token = reader.ReadInt32Optimized();
					this.PackageArgs = (this.HeaderInfo.IsSystem) ? this.packageArgsFactory.CreateSystemResponseArgs(this.Key) :
																	this.packageArgsFactory.CreateResponseArgs(this.Key);
				}
				else if (this.HeaderInfo.PackageType == PackageType.Message)
				{
					this.PackageArgs = (this.HeaderInfo.IsSystem) ? this.packageArgsFactory.CreateSystemMessageArgs(this.Key) :
																	this.packageArgsFactory.CreateMessageArgs(this.Key);
				}

				if (this.PackageArgs != null)
				{
					try
					{
						this.PackageArgs.ReadFrom(ref reader, session);
					}
					catch (Exception ex)
					{
						this.PackageArgs.Status = PackageStatus.ExceptionIsCaughtOnArgsSerialization;
						this.PackageArgs.ErrorMessage = $"Error in args serialization, PackageType={this.HeaderInfo.PackageType.ToString()}, RequestId={this.Key}, " +
														$"IsSystem={this.HeaderInfo.IsSystem.ToString()}: {ExceptionHelper.GetFullErrorMessage(ex)}";
					}
				}
			//}

			return this;
		}

		public PackageReader DecodeOld(ref SequenceReader<byte> buffer, object context)
		{
			if (this.createDataCopy)
				this.Buffer = buffer.Sequence.ToArray();

			if (context is SimpleSession session)
			{
				//SerializationReader reader = new SerializationReader(ref buffer);
				SequenceReader reader = new SequenceReader(ref buffer);

				reader.AdvancePosition(this.PackageLengthSize); // Skip reading package data length again

				this.HeaderInfo = new HeaderInfo(reader.ReadUInt64Optimized());

				if (this.HeaderInfo.PackageType == PackageType.Request)
				{
					this.Key = reader.ReadInt32Optimized(); // package is response => package.Key is RequestId
					//this.Token = reader.ReadInt32Optimized();

					try
					{
						this.PackageArgs = (this.HeaderInfo.IsSystem) ? this.packageArgsFactory.CreateSystemRequestArgs(this.Key) :
																		this.packageArgsFactory.CreateRequestArgs(this.Key);
					}
					catch (Exception ex)
					{
						PackageStatus status = PackageStatus.UnknownRequest;

						if (this.HeaderInfo.IsSystem)
						{
							if (this.packageArgsFactory.SystemRequestArgsByRequestId.GetValue(this.Key) != null)
								status = PackageStatus.ExceptionIsCaughtOnRequestArgsInitialization;
						}
						else if (this.packageArgsFactory.RequestArgsByRequestId.GetValue(this.Key) != null)
							status = PackageStatus.ExceptionIsCaughtOnRequestArgsInitialization;

						if (this.PackageArgs != null)
						{
							this.PackageArgs.Status = status;
							this.PackageArgs.ErrorMessage = $"Error in requst args initialization, RequestId={this.Key}: {ExceptionHelper.GetFullErrorMessage(ex)}";
						}
						else
						{
							this.PackageArgs = new ErrorRequestArgs(status, $"Unknown Request (no method to process found), RequestId={this.Key}, SessionKey={session.SessionKey}");
						}

						return this;
					}

					if (this.PackageArgs != null)
					{
						try
						{
							this.PackageArgs.ReadFrom(ref reader, session);
						}
						catch (Exception ex)
						{
							this.PackageArgs.Status = PackageStatus.ExceptionIsCaughtOnRequestArgsSerialization;
							this.PackageArgs.ErrorMessage = $"Error in requst args initialization, RequestId={this.Key}: {ExceptionHelper.GetFullErrorMessage(ex)}";

							return this;
						}
					}
					else
					{
						this.PackageArgs = new ErrorRequestArgs(PackageStatus.UnknownRequest, $"Unknown Request (no method to process found), RequestId={this.Key}, SessionKey={session.SessionKey}");
					}
				}
				else if (this.HeaderInfo.PackageType == PackageType.Response)
				{
					//this.Token = reader.ReadInt32Optimized();

					try
					{
						this.PackageArgs = (this.HeaderInfo.IsSystem) ? this.packageArgsFactory.CreateSystemResponseArgs(this.Key) :
																		this.packageArgsFactory.CreateResponseArgs(this.Key);
					}
					catch (Exception ex)
					{
						PackageStatus status = PackageStatus.UnknownRequest;

						if (this.HeaderInfo.IsSystem)
						{
							if (this.packageArgsFactory.SystemResponseArgsByRequestId.GetValue(this.Key) != null)
								status = PackageStatus.ExceptionIsCaughtOnResponseArgsInitialization;
						}
						else if (this.packageArgsFactory.ResponseArgsByRequestId.GetValue(this.Key) != null)
							status = PackageStatus.ExceptionIsCaughtOnResponseArgsInitialization;

						if (this.PackageArgs != null)
						{
							this.PackageArgs.Status = status;
							this.PackageArgs.ErrorMessage = $"Error in response args initialization, RequestId={this.Key}: {ExceptionHelper.GetFullErrorMessage(ex)}";
						}
						else
						{
							this.PackageArgs = new ErrorResponseArgs(status, $"Unknown Response (no method to process found), RequestId={this.Key}, SessionKey={session.SessionKey}");
						}

						return this;
					}

					if (this.PackageArgs != null)
					{
						try
						{
							if (this.HeaderInfo.ResponseSucceed)
							{
								this.PackageArgs.ReadFrom(ref reader, session);
							}
							else
							{
								this.PackageArgs.ReadErrorInfo(ref reader);

								if (this.PackageArgs.Status == PackageStatus.OK)
								{
									this.PackageArgs.Status = PackageStatus.Error;
									this.PackageArgs.ErrorMessage += $" PAckage ResponseSucceed flag set to false, but Status is OK, RequestId={this.Key}";

									return this;
								}
							}
						}
						catch (Exception ex)
						{
							this.PackageArgs.Status = PackageStatus.ExceptionIsCaughtOnRequestArgsSerialization;
							this.PackageArgs.ErrorMessage = $"Error in requst args initialization, RequestId={this.Key}: {ExceptionHelper.GetFullErrorMessage(ex)}";

							return this;
						}
					}
					else
					{
						this.PackageArgs = new ErrorResponseArgs(PackageStatus.UnknownRequest, $"Unknown Request (no method to process found), RequestId={this.Key}, SessionKey={session.SessionKey}");
					}

				}
				else if (this.HeaderInfo.PackageType == PackageType.Message)
				{
					this.Key = reader.ReadInt32Optimized(); // package is response => package.Key is RequestId

					try
					{
						this.PackageArgs = (this.HeaderInfo.IsSystem) ? this.packageArgsFactory.CreateSystemMessageArgs(this.Key) :
																		this.packageArgsFactory.CreateMessageArgs(this.Key);
					}
					catch (Exception ex)
					{
						PackageStatus status = PackageStatus.UnknownMessage;

						if (this.HeaderInfo.IsSystem)
						{
							if (this.packageArgsFactory.SystemMessageArgsByMessageCode.GetValue(this.Key) != null)
								status = PackageStatus.ExceptionIsCaughtOnMessageArgsInitialization;
						}
						else if (this.packageArgsFactory.MessageArgsByMessageCode.GetValue(this.Key) != null)
							status = PackageStatus.ExceptionIsCaughtOnMessageArgsInitialization;

						if (this.PackageArgs != null)
						{
							this.PackageArgs.Status = status;
							this.PackageArgs.ErrorMessage = $"Error in message args initialization, MeesageCode={this.Key}: {ExceptionHelper.GetFullErrorMessage(ex)}";
						}
						else
						{
							this.PackageArgs = new ErrorResponseArgs(status, $"Unknown message (no method to process found), MessageCode={this.Key}, SessionKey={session.SessionKey}");
						}

						return this;
					}

					if (this.PackageArgs != null)
					{
						try
						{
							this.PackageArgs.ReadFrom(ref reader, session);
						}
						catch (Exception ex)
						{
							this.PackageArgs.Status = PackageStatus.ExceptionIsCaughtOnMessageArgsSerialization;
							this.PackageArgs.ErrorMessage = $"Error in message args initialization, RequestId={this.Key}: {ExceptionHelper.GetFullErrorMessage(ex)}";

							return this;
						}
					}
					else
					{
						this.PackageArgs = new ErrorResponseArgs(PackageStatus.UnknownMessage, $"Unknown message (no method to process found), MessageCode={this.Key}, SessionKey={session.SessionKey}");
					}
				}
			}

			return this;
		}

		/// <summary>
		/// Reads a 64-bit unsigned value into the stream using 7-bit endianness encoding.
		/// 
		/// The value is written 7 bits at a time (starting with the least-significant bits) until there are no more bits to write.
		/// The eighth bit of each byte stored is used to indicate whether there are more bytes following this one.
		/// </summary>
		private static bool TryGetInt64By7BitEncoding(ref SequenceReader<byte> reader, ref long value, ref int bytesConsumed) => tryGetInt64By7BitEncoding(ref reader, ref value, ref bytesConsumed);

		/// <summary>
		/// reads a 64-bit unsigned value into the stream using 7-bit little endian encoding.
		/// 
		/// The value is written 7 bits at a time (starting with the least-significant bits) until there are no more bits to write.
		/// The eighth bit of each byte stored is used to indicate whether there are more bytes following this one.
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		private static bool TryGetInt64By7BitLittleEndianEncoding(ref SequenceReader<byte> reader, ref long value, ref int bytesConsumed)
		{
			int bitShift = 0;

			//byte? first = null;
			//byte? second = null;
			//bool thatsIt = false;

			while (true)
			{
				if (!reader.TryRead(out byte nextByte))
					return false; // no enought data

				bytesConsumed++;

				value |= ((long)nextByte & 0x7f) << bitShift;
				bitShift += 7;

				if ((nextByte & 0x80) == 0)
					return true;
			}
		}


		/// <summary>
		/// reads a 64-bit unsigned value into the stream using 7-bit big endian encoding.
		/// 
		/// The value is written 7 bits at a time (starting with the least-significant bits) until there are no more bits to write.
		/// The eighth bit of each byte stored is used to indicate whether there are more bytes following this one.
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		private static bool TryGetInt64By7BitBigEndianEncoding(ref SequenceReader<byte> reader, ref long value, ref int bytesConsumed)
		{
			for (int i = 0; i < 9; i++)
			{
				if (!reader.TryRead(out byte nextByte))
					return false; // no enought data

				bytesConsumed++;

				if (unchecked((long)nextByte) == -1)
					throw new Exception("End of Stream Exception");

				value = (value << 7) | ((long)nextByte & 0x7f);

				if ((nextByte & 0x80) == 0)
					return true;
			}

			throw new Exception("Invalid 7-bit encoded integer in stream."); // <- Still haven't seen a byte with the high bit unset? Dodgy data.
		}
	}
}
