using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Simple;
using Simple.Modeling;
using Simple.Collections;
using Simple.Objects;
using Simple.Objects.Controls;
//using Simple.Objects.Services;
using Simple.AppContext;
using Simple.Controls;
using Simple.Controls.TreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Skins;

namespace Simple.Objects.Controls
{
	public partial class RibbonModuleDevelopment : SimpleRibbonModuleSplitPanel
	{
		private const int MaxBestFitNodeCount = 1000;

		private int columnNameIndex, columnStatusIndex, columnPropertiesIndex;
		private FormDefaultChangeContainer formDefaultChangeContainer = null;
		//private const string strName = "Name";
		//      private const string strStatus = "Status";
		//      private const string strProperties = "Properties";
		//      private const string strOldProperties = "OldProperties";
		private SimpleDictionary<object, TreeListNode> nodesByTransactionObject = new SimpleDictionary<object, TreeListNode>();
		private Dictionary<long, Dictionary<int, Dictionary<long, TreeListNode>>> childNodesByObjectIdByTableIdByTransactionId = new Dictionary<long, Dictionary<int, Dictionary<long, TreeListNode>>>();
		int splitControlTransactionLogWidth = 0;

		public RibbonModuleDevelopment(SimpleObjectManager objectManager, IClientAppContext appContext)
			: base()
        {
			//this.AppClient = appClient;
			this.ObjectManager = objectManager;
			this.AppContext = appContext;

			InitializeComponent();

			this.imageList = ImageControl.SmallImageCollection;
			this.treeListTransactionLog.SelectImageList = this.imageList;
			this.barButtonItemStopTransactionMonitor.Enabled = false;
            
            this.columnNameIndex = SimpleTreeList.AddColumn(this.treeListTransactionLog, "Name");
			this.columnPropertiesIndex = SimpleTreeList.AddColumn(this.treeListTransactionLog, "Changed Properties  (* property is not storable)");
			this.columnStatusIndex = SimpleTreeList.AddColumn(this.treeListTransactionLog, "Status");

			this.graphController.ObjectManager = this.ObjectManager;
			this.treeListTransactionLog.FocusedNodeChanged += treeListTransactionLog_FocusedNodeChanged;

			this.groupPropertyPanel.SetPanelObjectDefinition(typeof(SystemTransaction), typeof(SystemEditPanelTransaction));
			this.groupPropertyPanel.SetPanelObjectDefinition(typeof(SystemTransactionActionInfo), typeof(SystemEditPanelTransactionActionInfo));
			this.groupPropertyPanel.SetBindingObject(null, context: this.RibbonForm);

			this.splitControlTransactionLogWidth = this.splitContainerControlTransactionLog.Width;
        }

		protected override void OnSetIsActive()
		{
			base.OnSetIsActive();
			
			this.treeListTransactionLog.Visible = this.IsActive;
		}
		
		public SimpleObjectManager ObjectManager {  get; private set; }
		public IClientAppContext AppContext { get; private set; }

		public bool ShowCodeGenerator
		{
			get => this.ribbonPageGroupCodeGenerator.Visible;
			set
			{
				this.ribbonPageGroupCodeGenerator.Visible = value;
				this.tabPageCodeGeneratorLog.PageVisible = value;
			}
		}

		public void StartTransactionMonitor()
        {
			this.tabControl.SelectedTabPage = this.tabPageTransactionLog; 
			this.ObjectManager.TransactionStarted += ObjectManager_TransactionStarted;
            this.ObjectManager.TransactionFinished += ObjectManager_TransactionFinished;
        }

		public void StopTransactionMonitor()
        {
			this.tabControl.SelectedTabPage = this.tabPageTransactionLog;
			
			if (this.ObjectManager != null)
            {
				this.ObjectManager.TransactionStarted -= ObjectManager_TransactionStarted;
				this.ObjectManager.TransactionFinished -= ObjectManager_TransactionFinished;
            }
        }

		private void ObjectManager_TransactionStarted(object sender, TransactionDatastoreActionRequesterEventArgs e)
		{
			Form form = this.FindForm();

			if (form != null && form.InvokeRequired)
			{
				form.Invoke(new MethodInvoker(() => this.OnObjectManager_TransactionStarted(e)));
			}
			else
			{
				this.OnObjectManager_TransactionStarted(e);
			}
		}

		private void OnObjectManager_TransactionStarted(TransactionDatastoreActionRequesterEventArgs e)
		{
			this.treeListTransactionLog.BeginUpdate();

			TreeListNode parentNode = this.AppendNode(e.Transaction, parentNode: null);
			IEnumerable<DatastoreActionInfo> datastoreActions = e.DatastoreActions ?? new DatastoreActionInfo[0];

			parentNode[this.columnNameIndex] = String.Format("Transaction {0}:  Time={1}.{2}", e.Transaction.TransactionId, e.Transaction.CreationTime.ToString(), e.Transaction.CreationTime.Millisecond.ToString());
			parentNode[this.columnStatusIndex] = e.Transaction.Status.ToString();
			parentNode.Tag = e.Transaction;
			
			if (!this.nodesByTransactionObject.ContainsKey(e.Transaction))
				this.nodesByTransactionObject.Add(e.Transaction, parentNode);

			this.SetNodeImage(parentNode, "CollapseAll");

			var transactionActions = (e.DatastoreActions != null) ? e.Transaction.RollbackTransactionActions.Union(e.DatastoreActions) :
																	e.Transaction.RollbackTransactionActions; // e.DatastoreActions is null on when ObjectManager working mode is client

			foreach (TransactionActionInfo transactionAction in transactionActions)
			{
				SimpleObject? simpleObject = this.ObjectManager.GetObject(transactionAction.TableId, transactionAction.ObjectId);
				TreeListNode childNode = this.GetChildNode(e.Transaction.TransactionId, transactionAction.TableId, transactionAction.ObjectId);
				DatastoreActionInfo? datastoreAction = transactionAction as DatastoreActionInfo;
				//string status = (datastoreAction != null) ? datastoreAction.DatastoreStatus.ToString() : e.Transaction.Status.ToString();

				if (childNode == null)
					childNode = this.AppendChildNode(e.Transaction.TransactionId, transactionAction, parentNode);

				childNode[this.columnNameIndex] = this.CreateNameText(simpleObject, transactionAction);
				childNode[this.columnPropertiesIndex] = this.CreatePropertyText(simpleObject, transactionAction.ActionType);
				childNode[this.columnStatusIndex] = "Processing";
				childNode.Tag = new SystemTransactionActionInfo(this.ObjectManager) { TransactionStatus = e.Transaction.Status, DatastoreAction = datastoreAction };
				this.SetNodeImage(childNode, simpleObject?.GetImageName(), transactionAction.ActionType);
			}

			this.treeListTransactionLog.EndUpdate();
		}

		private void ObjectManager_TransactionFinished(object sender, TransactionDatastoreActionRequesterEventArgs e)
		{
			Form form = this.FindForm();

			if (form != null && form.InvokeRequired)
			{
				form.Invoke(new MethodInvoker(() => this.OnObjectManager_TransactionFinished(e)));
			}
			else
			{
				this.OnObjectManager_TransactionFinished(e);
			}
		}

		private void OnObjectManager_TransactionFinished(TransactionDatastoreActionRequesterEventArgs e)
        {
			TreeListNode parentNode;

			this.treeListTransactionLog.BeginUpdate();
			
			if (this.nodesByTransactionObject.TryGetValue(e.Transaction, out parentNode))
			{
				//Dictionary<SimpleObject, TreeListNode> nodesBySimpleObject = new Dictionary<SimpleObject, TreeListNode>();
				IEnumerable<DatastoreActionInfo> datastoreActions = e.DatastoreActions ?? new DatastoreActionInfo[0];

				if (parentNode == this.treeListTransactionLog.FocusedNode)
					this.groupPropertyPanel.RefreshBindingObject(e.Requester);
				
				parentNode[this.columnStatusIndex] = e.Transaction.Status.ToString();

				foreach (TransactionActionInfo transactionAction in e.Transaction.RollbackTransactionActions.Union(datastoreActions))
				{
					TreeListNode childNode = this.GetChildNode(e.Transaction.TransactionId, transactionAction.TableId, transactionAction.ObjectId);
					SystemTransactionActionInfo transactionActionInfo = (childNode.Tag as SystemTransactionActionInfo);
					DatastoreActionInfo datastoreAction = transactionAction as DatastoreActionInfo;
					//string status = (datastoreAction != null) ? datastoreAction.DatastoreStatus.ToString() : transactionAction.Status.ToString();
					string status = (childNode[this.columnStatusIndex] != null) ? childNode[this.columnStatusIndex].ToString() : String.Empty;

					if (status.Length > 0)
						status += ", ";

					status += (datastoreAction != null) ? datastoreAction.DatastoreStatus.ToString() : (transactionActionInfo.TransactionStatus == TransactionStatus.Completed) ? "Archived" : "Transaction " + transactionActionInfo.TransactionStatus.ToString();
					childNode[this.columnStatusIndex] = status;

					//transactionActionInfo.TransactionActionLog = transactionActionLog;
					transactionActionInfo.TransactionStatus = e.Transaction.Status;
				}

				//if (e.DatastoreActions != null) // Client e.Datastore is null
				//{
				//	foreach (DatastoreActionInfo datastoreAction in e.DatastoreActions)
				//	{
				//		TreeListNode childNode = this.GetChildNode(e.Transaction.TransactionId, datastoreAction.TableId, datastoreAction.ObjectId);

				//		//if (childNode == null)
				//		//{
				//		//	SimpleObject simpleObject = this.ObjectManager.GetObject(datastoreAction.TableId, datastoreAction.ObjectId);

				//		//	childNode = this.AppendChildNode(e.Transaction.TransactionId, datastoreAction, parentNode);
				//		//	childNode[this.columnNameIndex] = this.CreateNameText(simpleObject, datastoreAction);
				//		//	childNode[this.columnPropertiesIndex] = this.CreatePropertyText(simpleObject, datastoreAction.ActionType);
				//		//	childNode.Tag = new SystemTransactionActionInfo(this.ObjectManager) { TransactionStatus = e.Transaction.Status };
				//		//	this.SetNodeImage(childNode, simpleObject.GetImageName(), datastoreAction.ActionType);
				//		//}

				//		SystemTransactionActionInfo transactionActionInfo = (childNode.Tag as SystemTransactionActionInfo);

				//		childNode[this.columnStatusIndex] = datastoreAction.DatastoreStatus.ToString();

				//		transactionActionInfo.DatastoreAction = datastoreAction;
				//		transactionActionInfo.TransactionStatus = e.Transaction.Status;
				//	}
				//}

				this.nodesByTransactionObject.Remove(e.Transaction);
				this.childNodesByObjectIdByTableIdByTransactionId.Remove(e.Transaction.TransactionId);
			}

			this.treeListTransactionLog.EndUpdate();
			this.treeListTransactionLog.BestFitColumns();
		}

		private void barButtonItemStartTransactionMonitor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
			this.tabControl.SelectedTabPage = this.tabPageTransactionLog;
			this.barButtonItemStartTransactionMonitor.Enabled = false;
            this.barButtonItemStopTransactionMonitor.Enabled = true;
            this.StartTransactionMonitor();
        }

		private void barButtonItemStopTransactionMonitor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
			this.tabControl.SelectedTabPage = this.tabPageTransactionLog;
			this.barButtonItemStopTransactionMonitor.Enabled = false;
            this.barButtonItemStartTransactionMonitor.Enabled = true;
            this.StopTransactionMonitor();
        }

		private void barButtonItemClearTransactionLog_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.tabControl.SelectedTabPage = this.tabPageTransactionLog;
			this.ClearNodes();
		}

		private void barButtonItemGenerateSimpleObjects_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
			this.tabControl.SelectedTabPage = this.tabPageCodeGeneratorLog;
			this.simpleObjectCodeGenerator.GenerateSimpleObjectSourceCodes(this.ObjectManager.ModelDiscovery);
        }

        private void barCheckItemIncludeSystemObjects_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
			this.tabControl.SelectedTabPage = this.tabPageCodeGeneratorLog;
			this.simpleObjectCodeGenerator.IncludeSystemObjects = this.barCheckItemIncludeSystemObjects.Checked;
        }

		private void treeListTransactionLog_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
		{
			object bindingObject = (e.Node != null) ? e.Node.Tag : null;
			this.groupPropertyPanel.SetBindingObject(bindingObject, context: this.RibbonForm);
		}

		private TreeListNode GetChildNode(long transactionId, int tableId, long objectId)
		{
			Dictionary<int, Dictionary<long, TreeListNode>> nodesByObjectIdByTableId;
			Dictionary<long, TreeListNode> nodesByObjectId;
			TreeListNode node = null;

			if (this.childNodesByObjectIdByTableIdByTransactionId.TryGetValue(transactionId, out nodesByObjectIdByTableId))
				if (nodesByObjectIdByTableId.TryGetValue(tableId, out nodesByObjectId))
					nodesByObjectId.TryGetValue(objectId, out node);

			return node;
		}

		private TreeListNode AppendChildNode(long transactionId, TransactionActionInfo transactionAction, TreeListNode parentNode)
		{
			TreeListNode childNode = this.AppendNode(transactionAction, parentNode);

			Dictionary<int, Dictionary<long, TreeListNode>> nodesByObjectIdByTableId;
			Dictionary<long, TreeListNode> nodesByObjectId;

			if (!this.childNodesByObjectIdByTableIdByTransactionId.TryGetValue(transactionId, out nodesByObjectIdByTableId))
			{
				nodesByObjectIdByTableId = new Dictionary<int, Dictionary<long, TreeListNode>>();
				this.childNodesByObjectIdByTableIdByTransactionId.Add(transactionId, nodesByObjectIdByTableId);
			}

			if (!nodesByObjectIdByTableId.TryGetValue(transactionAction.TableId, out nodesByObjectId))
			{
				nodesByObjectId = new Dictionary<long, TreeListNode>();
				nodesByObjectIdByTableId.Add(transactionAction.TableId, nodesByObjectId);
			}

			nodesByObjectId.Add(transactionAction.ObjectId, childNode);

			return childNode;
		}

		private TreeListNode AppendNode(object nodeObject, TreeListNode parentNode)
		{
			TreeListNode node;

			if (nodeObject == null)
				return null;

			//if (!this.nodesByObject.TryGetValue(nodeObject, out node))
			//{
				node = this.treeListTransactionLog.AppendNode(nodeObject, parentNode, tag: nodeObject);
				//this.nodesByObject.Add(nodeObject, node);
			//}

			return node;
		}

		private TreeListNode GetNode(SystemTransaction transaction)
		{
			return (transaction != null) ? this.nodesByTransactionObject[transaction] : null;
		}

		//private TreeListNode GetNode(TransactionActionLog transactionActionLog)
		//{
		//	return (transactionActionLog != null) ? this.nodesByObject[transactionActionLog] : null;
		//}

		//private TreeListNode GetNode(SimpleObject simpleObject)
		//{
		//	return (simpleObject != null) ? this.nodesByObject[simpleObject] : null;
		//}

		private bool RemoveNode(object nodeObject)
		{
			TreeListNode node;

			if (this.nodesByTransactionObject.TryGetValue(nodeObject, out node))
			{
				this.treeListTransactionLog.Nodes.Remove(node);
				this.nodesByTransactionObject.Remove(nodeObject);
				
				return true;
			}
			
			return false;
		}

		private void ClearNodes()
		{
			this.treeListTransactionLog.ClearNodes();
			this.nodesByTransactionObject.Clear();
		}

		private string CreateNameText(SimpleObject simpleObject, TransactionActionInfo transactionAction)
		{
			return String.Format("{0} {1} {2}.{3}:  {4}", transactionAction.ActionType.ToString(),
					 									  simpleObject.GetModel().ObjectType.Name,
														  transactionAction.TableId.ToString(),
														  transactionAction.ObjectId.ToString(),
														  simpleObject.GetName());
		}

		private string CreatePropertyText(SimpleObject simpleObject, TransactionActionType actionType)
		{
			string result = String.Empty;

			if (actionType == TransactionActionType.Delete)
				return result;

			int[] changedIndexes = simpleObject.GetChangedPropertyIndexes();

			changedIndexes.Sort((s1, s2) => s1.CompareTo(s2));

			if (actionType == TransactionActionType.Insert)
				result = "Id=" + simpleObject.Id.ToString();

			foreach (var index in changedIndexes)
			{
				IPropertyModel propertyModel = simpleObject.GetModel().PropertyModels[index];
				object propertyValue = simpleObject.GetPropertyValue(index);

				result += String.Format("{0}{1}{2}={3}", (result.Length > 0) ? "; " : "", propertyModel.PropertyName, (!propertyModel.IsStorable) ? "*" : "", propertyValue.ValueToString()); 
														 //SimpleObject.GetPropertyValueString(propertyModel, propertyValue));
				
				if (actionType == TransactionActionType.Update)
				{
					object oldPropertyValue = simpleObject.GetOldPropertyValue(index);

					result += String.Format(" ({0})", oldPropertyValue.ValueToString()); // SimpleObject.GetPropertyValueString(propertyModel, oldPropertyValue));
				}
			}

			return result;
		}

		//private string CreatePropertyText(DatastoreAction datastoreAction)
		//{
		//	string result = String.Empty;

		//	SimpleObject simpleObject = this.ObjectManager.GetSimpleObject(datastoreAction.TableId, datastoreAction.ObjectId);

		//	if (simpleObject == null)
		//		return result;

		//	IPropertyModel[] propertyModelSequence = (datastoreAction.ActionType == TransactionActionType.Update) ? datastoreAction.PropertyValueSequence.PropertyModels :
		//																											simpleObject.GetModel().StorablePropertySequence.PropertyModels;
		//	object[] propertyValues = (datastoreAction.ActionType == TransactionActionType.Update) ? new object[propertyModelSequence.Length] : null;

		//	for (int i = 0; i < propertyModelSequence.Length; i++)
		//	{
		//		if (result != String.Empty)
		//			result += "; ";

		//		IPropertyModel propertyModel = propertyModelSequence[i];
		//		object propertyValue = simpleObject.GetPropertyValue(propertyModel.Index);

		//		result += propertyModel.Name + "=" + SimpleObject.GetPropertyValueString(propertyModel, propertyValue);

		//		if (datastoreAction.ActionType == TransactionActionType.Update)
		//		{
		//			//object oldPropertyValue = datastoreAction.PropertyValueSequence.PropertyValues[i];

		//			//result += String.Format(" ({0})", SimpleObject.GetPropertyValueString(propertyModel, oldPropertyValue));
		//			propertyValues[i] = propertyValue;
		//		}
		//	}

		//	datastoreAction.Tag = propertyValues;

		//	return result;
		//}

		//private string CreatePropertyText(TransactionActionLog transactionAction)
		//{
		//	string result = String.Empty;

		//	SimpleObject simpleObject = this.ObjectManager.GetSimpleObject(transactionAction.TableId, transactionAction.ObjectId);

		//	if (simpleObject == null)
		//		return result;

		//	IPropertyModel[] propertyModelSequence = (transactionAction.ActionType != TransactionActionType.Delete) ? transactionAction.PropertyValueSequence.PropertyModels : 
		//																											  simpleObject.GetModel().SerializablePropertySequence.PropertyModels;
		//	object[] propertyValues = (transactionAction.ActionType == TransactionActionType.Update) ? new object[propertyModelSequence.Length] : null;

		//	for (int i = 0; i < propertyModelSequence.Length; i++)
		//	{
		//		if (result != String.Empty)
		//			result += "; ";

		//		IPropertyModel propertyModel = propertyModelSequence[i];
		//		object propertyValue = simpleObject.GetPropertyValue(propertyModel.Index);

		//		result += propertyModel.Name + "=" + SimpleObject.GetPropertyValueString(propertyModel, propertyValue);

		//		if (transactionAction.ActionType == TransactionActionType.Update)
		//		{
		//			object oldPropertyValue = transactionAction.PropertyValueSequence.PropertyValues[i];

		//			result += String.Format(" ({0})", SimpleObject.GetPropertyValueString(propertyModel, oldPropertyValue));
		//			propertyValues[i] = propertyValue;
		//		}
		//	}

		//	transactionAction.Tag = propertyValues;

		//	return result;
		//}

		private void barCheckItemShowDefaultChangeContainer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (this.barCheckItemShowDefaultChangeContainer.Checked)
			{
				this.formDefaultChangeContainer = new FormDefaultChangeContainer(this.ObjectManager, this.AppContext); ;
				this.formDefaultChangeContainer.FormClosed += FormDefaultChangeContainer_FormClosed;
				this.formDefaultChangeContainer.TopMost = true;
				this.formDefaultChangeContainer.Owner = this.RibbonForm;
				this.formDefaultChangeContainer.Show();
			}
			else
			{
				this.formDefaultChangeContainer.Close();
				this.formDefaultChangeContainer = null;
			}
		}

		private void barCheckItemNoCommitChangeOnFocusedNodeChange_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (this.barCheckItemNoCommitChangeOnFocusedNodeChange.Checked)
			{
				GraphManager.SetCommitChangeOnFocusedNodeChangeForAll(false);
			}
			else
			{
				GraphManager.RestoreCommitChangeOnFocusedNodeChangeFromAll();
			}
		}

		private void barCheckItemNoCommitChangeOnDeleteRequest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (this.barCheckItemNoCommitChangeOnDeleteRequest.Checked)
			{
				GraphManager.SetCommitChangeOnDeleteRequestForAll(false);
			}
			else
			{
				GraphManager.RestoreCommitChangeOnDeleteRequestFromAll();
			}
		}

		private void FormDefaultChangeContainer_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.barCheckItemShowDefaultChangeContainer.Checked = false;
		}


		//private string CreateOldPropertyText(TransactionAction transactionAction)
		//{
		//	string result = String.Empty;

		//	if (transactionAction.ActionType != TransactionActionType.Update)
		//		return result;

		//	//SimpleObject simpleObject = this.ObjectManager.GetObject(transactionAction.ObjectKey);

		//	//if (simpleObject == null)
		//	//	return result;

		//	//int[] propertyIndexes = this.ObjectManager.GetPropertySequence(transactionAction.PropertyIndexSequenceId).PropertyIndexSequence;

		//	for (int i = 0; i < transactionAction.ChangedOldPropertyValues.PropertyValues.Length; i++)
		//	{
		//		IPropertyModel propertyModel = transactionAction.ChangedOldPropertyValues.PropertyModelSequence[i];
		//		object propertyValue = transactionAction.ChangedOldPropertyValues.PropertyValues[i];

		//		if (result != String.Empty)
		//			result += "; ";

		//		result += propertyModel.Name + "=" + SimpleObject.GetPropertyValueString(propertyValue);
		//	}

		//	return result;
		//}

		//private string CreatePropertyText(IDictionary<string, object> fieldData)
		//{
		//	string result = String.Empty;

		//	if (fieldData != null)
		//	{
		//		foreach (var singleData in fieldData)
		//		{
		//			if (result != String.Empty)
		//				result += "; ";

		//			string valueText = SimpleObject.GetPropertyValueString(singleData.Value);
		//			result += singleData.Key.ToString() + "=" + valueText;
		//		}
		//	}

		//	return result;
		//}

		private void splitContainerControl_Resize(object sender, EventArgs e)
		{
			if (this.splitControlTransactionLogWidth != 0)
				this.splitContainerControlTransactionLog.SplitterPosition = this.splitContainerControlTransactionLog.SplitterPosition * this.splitContainerControlTransactionLog.Width / this.splitControlTransactionLogWidth;

			this.splitControlTransactionLogWidth = this.splitContainerControlTransactionLog.Width;
		}

		private void SetNodeImage(TreeListNode node, string imageName, TransactionActionType actionType = TransactionActionType.Update)
		{
			string imageOption = String.Empty;
			int imageIndex = -1;

			if (actionType == TransactionActionType.Insert)
			{
				imageOption = ImageControl.ImageOptionAddExt;
			} 
			else if (actionType == TransactionActionType.Delete)
			{
				imageOption = ImageControl.ImageOptionRemoveExt;
			}

			if (!imageName.IsNullOrEmpty() && imageOption.Length > 0)
			{
				string newImageName = ImageControl.ImageNameAddOption(imageName, imageOption, insertionPosition: 1);
				imageIndex = this.imageList.Images.IndexOfKey(newImageName);

				if (imageIndex == -1)
					imageIndex = this.imageList.Images.IndexOfKey(imageName);
			}
			else
			{
				imageIndex = this.imageList.Images.IndexOfKey(imageName);
			}

			if (imageIndex >= 0)
			{
				node.TreeList.BeginUpdate();

				node.ImageIndex = imageIndex;
				node.SelectImageIndex = node.ImageIndex;

				node.TreeList.EndUpdate();
			}
		}
	}
}
