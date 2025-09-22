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
	[SystemResponseArgs((int)SystemRequest.GetGraphElementsWithObjects)]
	public class GraphElementsWithObjectsResponseArgs : ResponseArgs
	{
        public GraphElementsWithObjectsResponseArgs()
        {
		}

		public GraphElementsWithObjectsResponseArgs(GraphElementObjectPair[] graphElementWithObjects, IEnumerable<ServerObjectModelInfo>? newServerObjectModels)
		{
			this.GraphElementWithObjects = graphElementWithObjects;
			this.NewServerObjectModels = newServerObjectModels;
        }

		public IEnumerable<ServerObjectModelInfo>? NewServerObjectModels { get; private set; }
		public GraphElementObjectPair[] GraphElementWithObjects { get; private set; } = new GraphElementObjectPair[0];

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
				}
				else
				{
					writer.WriteBoolean(false);
				}
				
				writer.WriteInt32Optimized(this.GraphElementWithObjects!.Length);
				
				foreach (var item in this.GraphElementWithObjects!)
				{
					writer.WriteInt64Optimized(item.GraphElementId);
					writer.WriteInt32Optimized(item.SimpleObjectTableId);
					writer.WriteInt64Optimized(item.SimpleObjectId);
					writer.WriteBoolean(item.HasChildren);

					var serverObjectModel = simpleObjectSession.GetServerObjectModel(item.SimpleObjectTableId);

					writer.WriteInt32Optimized(item.SimpleObjectPropertyIndexValues.Count());
					item.SimpleObjectPropertyIndexValues.WtiteTo(ref writer, serverObjectModel!);

					//ObjectPropertyValuesResponseArgs graphElementObjectProperyValues = new ObjectPropertyValuesResponseArgs(GraphElementModel.TableId, item.GraphElementPropertyIndexValues);
					//ObjectPropertyValuesResponseArgs simpleObjectProperyValues = new ObjectPropertyValuesResponseArgs(item.SimpleObjectTableId, item.SimpleObjectPropertyIndexValues);

					//graphElementObjectProperyValues.WriteTo(ref writer, session);
					//writer.WriteBoolean(item.HasChildren);
					//simpleObjectProperyValues.WriteTo(ref writer, session);
				}
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
				
				this.GraphElementWithObjects = new GraphElementObjectPair[reader.ReadInt32Optimized()];

				for (int i = 0; i < this.GraphElementWithObjects.Length; i++)
				{
					long graphElementId = reader.ReadInt64Optimized();
					int simpleObjectTableId = reader.ReadInt32Optimized();
					long simpleObjectId = reader.ReadInt64Optimized();
					bool hasChildren = reader.ReadBoolean();
					
					var serverObjectModel = simpleObjectSession.GetServerObjectModel(simpleObjectTableId);
					PropertyIndexValuePair[] simpleObjectPropertyIndexValues = new PropertyIndexValuePair[reader.ReadInt32Optimized()];

					simpleObjectPropertyIndexValues.ReadFrom(ref reader, serverObjectModel!);
					this.GraphElementWithObjects[i] = new GraphElementObjectPair(graphElementId, simpleObjectTableId, simpleObjectId, hasChildren, simpleObjectPropertyIndexValues);


					//ObjectPropertyValuesResponseArgs graphElementObjectProperyValues = new ObjectPropertyValuesResponseArgs().CreateFromReader(ref reader, session);
					//bool hasChild = reader.ReadBoolean();
					//ObjectPropertyValuesResponseArgs simpleObjectProperyValues = new ObjectPropertyValuesResponseArgs().CreateFromReader(ref reader, session);

					//this.GraphElementWithObjects[i] = new GraphElementObjectPair(graphElementObjectProperyValues.PropertyIndexValues!, hasChild, simpleObjectProperyValues.TableId, simpleObjectProperyValues.PropertyIndexValues!); // simpleObjectTableId will be determined from graphElementObjectProperyValues
				}
			}
		}

		//protected ServerObjectModelInfo GetServerObjectModel(ISimpleObjectSession session, int tableId)
		//{
		//	return session.GetServerObjectModel(tableId) ?? this.NewServerObjectModels.First(item => item.TableId == tableId);
		//}
	}
}
