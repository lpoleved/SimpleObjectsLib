using Simple.Collections;
using Simple.Modeling;
using Simple.Objects;
using Simple.Serialization;
using Simple.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace Simple.Objects.SocketProtocol
{
	[SystemResponseArgs((int)SystemRequest.GetGraphElementsWithObjectsNew)]
	public class GraphElementsObjectPairsNewResponseArgs : ResponseArgs
	{
		private int maxNumOfBytesToWrite;
		//private SimpleSession session;
		//private int numberOfGraphElementsWritten = 0;
		private int graphKey;
		private long parentGraphElementId;
		private ActionBlock<SendUnicastSystemMessageArgs>? sendUnicastSystemMessageActionBlock = null;
		private int itemIndex = 0;

		public GraphElementsObjectPairsNewResponseArgs()
        {
			this.GraphElementObjectPairs = new List<GraphElementObjectPair>();
		}

		public GraphElementsObjectPairsNewResponseArgs(IEnumerable<ServerObjectModelInfo>? newServerObjectModels, GraphElementObjectPair[] graphElementObjectPairs, int maxNumOfBytesToWrite, int graphKey, long parentGraphElementId, ref ActionBlock<SendUnicastSystemMessageArgs> sendUnicastSystemMessageActionBlock)
		{
			this.GraphElementObjectPairs = graphElementObjectPairs;
			this.NewServerObjectModels = newServerObjectModels;
			this.maxNumOfBytesToWrite = maxNumOfBytesToWrite;

			this.graphKey = graphKey;
			this.parentGraphElementId = parentGraphElementId;
			this.sendUnicastSystemMessageActionBlock = sendUnicastSystemMessageActionBlock;
        }

		public IEnumerable<GraphElementObjectPair> GraphElementObjectPairs { get; private set; }
		
		// Revisuib required for this!!!!
		public IEnumerable<ServerObjectModelInfo>? NewServerObjectModels { get; private set; }


		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
        {
			base.WriteTo(ref writer, session);

			if (session is ISimpleObjectSession simpleObjectSession)
			{
				if (this.NewServerObjectModels != null)
				{
					writer.WriteBoolean(true);
					writer.WriteInt32Optimized(this.NewServerObjectModels.Count());

					foreach (var model in this.NewServerObjectModels)
						model.WriteTo(ref writer, context: session);

					this.NewServerObjectModels = null; // No more writing object models, only first time
				}
				else
				{
					writer.WriteBoolean(false);
				}
				
				//writer.WriteInt32Optimized(this.GraphElementWithObjects!.Length);
				
				while (this.itemIndex < this.GraphElementObjectPairs.Count())
				{
					writer.WriteBoolean(true); 
					
					var item = this.GraphElementObjectPairs.ElementAt(this.itemIndex++);
					var serverObjectModel = simpleObjectSession.GetServerObjectModel(item.SimpleObjectTableId);

					writer.WriteInt64Optimized(item.GraphElementId);
					writer.WriteInt32Optimized(item.SimpleObjectTableId);
					writer.WriteInt64Optimized(item.SimpleObjectId);
					writer.WriteBoolean(item.HasChildren);

					writer.WriteInt32Optimized(item.SimpleObjectPropertyIndexValues.Count());
					item.SimpleObjectPropertyIndexValues.WtiteTo(ref writer, serverObjectModel!);

					if (writer.BytesWritten >= this.maxNumOfBytesToWrite)
					{ 
						if (this.itemIndex < this.GraphElementObjectPairs.Count()) // There is more data that need to be delivered
							this.sendUnicastSystemMessageActionBlock?.Post(new SendUnicastSystemMessageArgs((session as SimpleSession)!, (int)SystemMessage.GetGraphElementsWithSimpleObjectsRestOfData, 
																											messageArgs: new GraphElementsWithObjectsRestOfDataMessageArgs(this, this.graphKey, this.parentGraphElementId)));
						break;
					}
				}

				writer.WriteBoolean(false);
			}
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
        {
			base.ReadFrom(ref reader, session);

			if (session is ISimpleObjectSession simpleObjectSession)
			{
				if (reader.ReadBoolean())
				{
					var newServerObjectModels = new ServerObjectModelInfo[reader.ReadInt32Optimized()];

					for (int i = 0; i < newServerObjectModels.Length; i++)
					{
						ServerObjectModelInfo model = new ServerObjectModelInfo(ref reader, context: session);

						newServerObjectModels[i] = model;
						simpleObjectSession.SetServerObjectModel(model.TableId, model);
					}

					this.NewServerObjectModels = newServerObjectModels;
				}
				else
				{
					this.NewServerObjectModels = null;
				}

				while (reader.ReadBoolean())
				{
					long graphElementId = reader.ReadInt64Optimized();
					int simpleObjectTableId = reader.ReadInt32Optimized();
					long simpleObjectId = reader.ReadInt64Optimized();
					bool hasChildren = reader.ReadBoolean();
					var serverObjectModel = simpleObjectSession.GetServerObjectModel(simpleObjectTableId);
					PropertyIndexValuePair[] simpleObjectPropertyIndexValues = new PropertyIndexValuePair[reader.ReadInt32Optimized()];

					simpleObjectPropertyIndexValues.ReadFrom(ref reader, serverObjectModel!);
					(this.GraphElementObjectPairs as List<GraphElementObjectPair>)!.Add(new GraphElementObjectPair(graphElementId, simpleObjectTableId, simpleObjectId, hasChildren, simpleObjectPropertyIndexValues));
				};
			}
		}
	}
}
