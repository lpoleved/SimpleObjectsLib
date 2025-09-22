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
	[SystemMessageArgs((int)SystemMessage.GetGraphElementsWithSimpleObjectsRestOfData)]
	public class GraphElementsWithObjectsRestOfDataMessageArgs : MessageArgs
	{
		private GraphElementsObjectPairsNewResponseArgs graphElementsWithObjectsResponseArgs;
		//private int maxNumOfBytesToWrite;
		//private SimpleSession session;
		//private int numberOfGraphElementsWritten = 0;
		//private ActionBlock<SendUnicastSystemMessageArgs>? sendUnicastSystemMessageActionBlock = null;

		public GraphElementsWithObjectsRestOfDataMessageArgs()
        {
			this.graphElementsWithObjectsResponseArgs = new GraphElementsObjectPairsNewResponseArgs();
		}

		public GraphElementsWithObjectsRestOfDataMessageArgs(GraphElementsObjectPairsNewResponseArgs graphElementsWithObjectsResponseArgs, int graphKey, long parentGraphElementId) //, int maxNumOfBytesToWrite, ref ActionBlock<SendUnicastSystemMessageArgs> sendUnicastSystemMessageActionBlock)
		{
			//this.maxNumOfBytesToWrite = maxNumOfBytesToWrite;
			//this.sendUnicastSystemMessageActionBlock = sendUnicastSystemMessageActionBlock;
			this.graphElementsWithObjectsResponseArgs = graphElementsWithObjectsResponseArgs;
			this.GraphKey = graphKey;
			this.ParentGraphElementId = parentGraphElementId;
        }

		public int GraphKey { get; private set; }
		public long ParentGraphElementId { get; private set; }
		public IEnumerable<GraphElementObjectPair> GraphElementObjectsPairs => this.graphElementsWithObjectsResponseArgs.GraphElementObjectPairs;

		//public IEnumerable<ServerObjectModelInfo>? NewServerObjectModels => this.graphElementsWithObjectsResponseArgs.NewServerObjectModels;


		public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
        {
			base.WriteTo(ref writer, session);

			this.graphElementsWithObjectsResponseArgs.WriteTo(ref writer, session);
			writer.WriteInt32Optimized(this.GraphKey);
			writer.WriteInt64Optimized(this.ParentGraphElementId);

			//if (session is ISimpleObjectSession simpleObjectSession)
			//{
			//	if (this.NewServerObjectModels != null)
			//	{
			//		writer.WriteBoolean(true);
			//		writer.WriteInt32Optimized(this.NewServerObjectModels.Count());

			//		foreach (var model in this.NewServerObjectModels)
			//			model.WriteTo(ref writer, context: session);

			//		this.NewServerObjectModels = null; // No more writing object models, only first time
			//	}
			//	else
			//	{
			//		writer.WriteBoolean(false);
			//	}
				
			//	//writer.WriteInt32Optimized(this.GraphElementWithObjects!.Length);
				
			//	while (this.itemIndex < this.GraphElementWithObjects.Count())
			//	{
			//		var item = this.GraphElementWithObjects.ElementAt(this.itemIndex++);

			//		writer.WriteInt64Optimized(item.GraphElementId);
			//		writer.WriteInt32Optimized(item.SimpleObjectTableId);
			//		writer.WriteInt64Optimized(item.SimpleObjectId);
			//		writer.WriteBoolean(item.HasChildren);

			//		var serverObjectModel = simpleObjectSession.GetServerObjectModel(item.SimpleObjectTableId);

			//		writer.WriteInt32Optimized(item.SimpleObjectPropertyIndexValues.Count());
			//		item.SimpleObjectPropertyIndexValues.WtiteTo(ref writer, serverObjectModel!);

			//		if (writer.BytesWritten < this.maxNumOfBytesToWrite)
			//		{
			//			writer.WriteBoolean(true); // There is more data to come
			//		}
			//		else
			//		{
			//			writer.WriteBoolean(false);
						
			//			if (this.itemIndex < this.GraphElementWithObjects.Count()) // There is more data that need to be delivered
			//				this.sendUnicastSystemMessageActionBlock?.Post(new SendUnicastSystemMessageArgs((session as SimpleSession)!, (int)SystemMessage.RestDataOfGetGraphElementsWithSimpleObjects, 
			//																								messageArgs: new ));

			//			break;
			//		}
			//	}
			//}
		}

		public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
        {
			base.ReadFrom(ref reader, session);

			this.graphElementsWithObjectsResponseArgs.ReadFrom (ref reader, session);
			this.GraphKey = reader.ReadInt32Optimized();
			this.ParentGraphElementId = reader.ReadInt64Optimized();
			//if (session is ISimpleObjectSession simpleObjectSession)
			//{
			//	if (reader.ReadBoolean())
			//	{
			//		var newServerObjectModels = new ServerObjectModelInfo[reader.ReadInt32Optimized()];

			//		for (int i = 0; i < newServerObjectModels.Length; i++)
			//		{
			//			ServerObjectModelInfo model = new ServerObjectModelInfo(ref reader, context: session);

			//			newServerObjectModels[i] = model;
			//			simpleObjectSession.SetServerObjectModel(model.TableId, model);
			//		}

			//		this.NewServerObjectModels = newServerObjectModels;
			//	}
			//	else
			//	{
			//		this.NewServerObjectModels = null;
			//	}

			//	do
			//	{
			//		long graphElementId = reader.ReadInt64Optimized();
			//		int simpleObjectTableId = reader.ReadInt32Optimized();
			//		long simpleObjectId = reader.ReadInt64Optimized();
			//		bool hasChildren = reader.ReadBoolean();
			//		var serverObjectModel = simpleObjectSession.GetServerObjectModel(simpleObjectTableId);
			//		PropertyIndexValuePair[] simpleObjectPropertyIndexValues = new PropertyIndexValuePair[reader.ReadInt32Optimized()];

			//		simpleObjectPropertyIndexValues.ReadFrom(ref reader, serverObjectModel!);
			//		(this.GraphElementWithObjects as List<GraphElementObjectPair>)!.Add(new GraphElementObjectPair(graphElementId, simpleObjectTableId, simpleObjectId, hasChildren, simpleObjectPropertyIndexValues));
			//	} while (reader.ReadBoolean());
			//}
		}
	}
}
