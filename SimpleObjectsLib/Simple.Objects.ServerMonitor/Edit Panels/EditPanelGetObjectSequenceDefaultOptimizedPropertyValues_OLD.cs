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
//	[RequestKey(SystemRequest.GetObjectSequenceDefaultOptimizedPropertyValues)]
//	public partial class EditPanelGetObjectSequenceDefaultOptimizedPropertyValues : EditPanelGetObjectSequencePropertyValues
//	{
//		public EditPanelGetObjectSequenceDefaultOptimizedPropertyValues()
//		{
//			InitializeComponent();
//		}

//		protected override GetObjectPropertyValuesResponseArgs CreateGetObjectPropertyValuesResponseArgs(int tableId)
//		{
//			return new GetObjectPropertyValuesResponseArgs(tableId, skipReadingKey: true, serializationModel: SerializationModel.SequenceValuesOnly, defaultOptimization: true);
//		}
//	}
//}
