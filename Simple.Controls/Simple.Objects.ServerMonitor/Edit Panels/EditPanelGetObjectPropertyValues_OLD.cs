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
//using Simple.Objects;
//using Simple.SocketEngine;
//using Simple.Objects.SocketProtocol;

//namespace Simple.Objects.ServerMonitor
//{
//	[SystemRequestArgs((int)SystemRequest.GetObjectPropertyValues)]
//	public partial class EditPanelGetObjectPropertyValues_OLD : EditPanelGetObjectPropertyIndexValuePairs
//	{
//		public EditPanelGetObjectPropertyValues_OLD()
//		{
//			InitializeComponent();
//		}

//		protected override GetObjectPropertyValuesResponseArgs CreateGetObjectPropertyValuesResponseArgs(int tableId)
//		{
//			return new GetObjectPropertyValuesResponseArgs(tableId, skipReadingKey: true, serializationModel: SerializationModel.IndexValuePairs, defaultOptimization: false);
//		}
//	}
//}
