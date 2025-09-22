//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using Simple.Modeling;
////using Simple.Serialization;
//using Simple.Objects;
//using Simple.Objects.SocketProtocol;

//namespace Simple.Objects.ServerMonitor
//{
//	[RequestKey(SystemRequest.GetObjectPropertyValues)]
//	public partial class EditPanelGetObjectDefaultOptimizedIndexedPropertyValues : EditPanelGetObjectSequencePropertyValues
//	{
//		public EditPanelGetObjectDefaultOptimizedIndexedPropertyValues()
//		{
//			InitializeComponent();
//		}

//		protected override GetObjectPropertyValuesResponseArgs CreateGetObjectPropertyValuesResponseArgs(int tableId)
//		{
//			return new GetObjectPropertyValuesResponseArgs(tableId, skipReadingKey: true, serializationModel: SerializationModel.IndexValuePairs, defaultOptimization: true);
//		}
//	}
//}
