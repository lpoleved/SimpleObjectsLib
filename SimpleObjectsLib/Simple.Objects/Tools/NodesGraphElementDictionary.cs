using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects
{
	public class NodesGraphElementDictionary
	{
		private Dictionary<GraphElement, object> nodesByGraphElement = new Dictionary<GraphElement, object>();

		private List<GraphElement> graphElements;
		private List<object> nodes;
		private static object lockObject = new object();
		
		public NodesGraphElementDictionary()
			: this(16)
		{ 
		}

		public NodesGraphElementDictionary(int capacity)
		{
			this.graphElements = new List<GraphElement>(capacity);
			this.nodes = new List<object>();
		}

		public bool Add(GraphElement graphElement, object node)
		{
			if (!this.Contains(graphElement))
			{
				this.graphElements.Add(graphElement);
				this.nodes.Add(node);

				return true;
			}

			return false;
		}

		public GraphElement GraphElementAt(int index) => this.graphElements[index];

		public object NodeAt(int index) => this.nodes[index];

		public bool Remove(GraphElement graphElement)
		{
			int index = this.graphElements.IndexOf(graphElement);

			if (index >= 0)
			{
				this.graphElements.RemoveAt(index);
				this.nodes.RemoveAt(index);

				return true;
			}

			return false;
		}

		public bool Remove(object node)
		{
			int index = this.nodes.IndexOf(node);

			if (index >= 0)
			{
				this.graphElements.RemoveAt(index);
				this.nodes.RemoveAt(index);

				return true;
			}

			return false;
		}


		public bool TrayGetGraphElement(object node, out GraphElement? graphElement)
		{
			int index = this.nodes.IndexOf(node);

			if (index >= 0)
			{
				graphElement = this.graphElements.ElementAt(index);

				return true;
			}

			graphElement = null;

			return false;
		}

		public bool TrayGetGraphElementId(object node, out long graphElementId)
		{
			int index = this.nodes.IndexOf(node);

			if (index >= 0)
			{
				GraphElement graphElement = this.graphElements.ElementAt(index);

				graphElementId = graphElement.Id;

				return false;
			}

			graphElementId = 0;

			return false;
		}



		public bool TrayGetNode(GraphElement graphElement, out object? node)
		{
			return this.TrayGetNode(graphElement.Id, out node);
		}

		public bool TrayGetNode(long graphElementId, out object? node)
		{
			//int index = this.graphElements.IndexOf(graphElement);

			//if (index >= 0)
			//{
			//	node = this.nodes[index];

			//	return true;
			//}

			//node = null;

			//return false;

			// TODO: Check is this fasest version
			lock (lockObject)
			{
				for (int i = 0; i < this.graphElements.Count; i++)
				{
					if (this.graphElements[i].Id == graphElementId)
					{
						node = this.nodes[i];

						return true;
					}
				}

				node = null;

				return false;
			}
		}


		public GraphElement[] GetGraphElements() => this.graphElements.ToArray();

		public object[] GetNodes() => this.nodes.ToArray();

		public bool Contains(GraphElement graphElement)
		{
			return this.graphElements.Contains(graphElement);
		}

		public bool Contains(long graphElementId)
		{
			lock (lockObject)
			{
				for (int i = 0; i < this.graphElements.Count; i++)
					if (this.graphElements[i].Id == graphElementId)
						return true;

				return false;
			}
		}

		public bool Contains(object node)
		{
			return this.nodes.Contains(node);
		}

		public int Count => this.nodes.Count;

		public void Clear()
		{
			this.graphElements.Clear();
			this.nodes.Clear();
		}
	}
}
