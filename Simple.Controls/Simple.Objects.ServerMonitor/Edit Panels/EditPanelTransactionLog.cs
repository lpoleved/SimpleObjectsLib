using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simple;
using Simple.Modeling;
using Simple.Objects;
using Simple.SocketEngine;
using Simple.Controls;
using Simple.Serialization;
using Simple.Objects.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using System.Threading;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace Simple.Objects.ServerMonitor
{
	public partial class EditPanelTransactionLog : EditPanelServerMonitorBase
	{
		private GridColumn columnTransactionRequestAction, columnTransactionRequestTableId, columnTransactionRequestObjectId, columnTransactionRequestPropertyValues;
		private GridColumn columnDatastoreAction, columnDatastoreTableName, columnDatastoreObjectId, columnDatastorePropertyValues;
		private GridColumn columnRollbackAction, columnRollbackTableId, columnRollbackObjectId, columnRollbackPropertyValues;
		private BindingList<TransactionActionRow> dataSourceTransactionRequests = new BindingList<TransactionActionRow>();
		private BindingList<DatastoreActionRow> dataSourceDatastoreActions = new BindingList<DatastoreActionRow>();
		private BindingList<TransactionActionRow> dataSourceRollbackActions = new BindingList<TransactionActionRow>();
		private RepositoryItemMemoEdit repositoryItemTransactionRequestPropertyValues = new RepositoryItemMemoEdit();
		private RepositoryItemMemoEdit repositoryItemDatastoreActionPropertyValues = new RepositoryItemMemoEdit();
		private RepositoryItemMemoEdit repositoryItemRollbackActionPropertyValues = new RepositoryItemMemoEdit();
		//private bool isFirst = true;

		public EditPanelTransactionLog()
		{
			InitializeComponent();

			//
			// Transaction Requests
			//
			this.columnTransactionRequestAction = new GridColumn();
			this.columnTransactionRequestAction.Name = this.columnTransactionRequestAction.FieldName = this.columnTransactionRequestAction.Caption = "Action";
			this.columnTransactionRequestAction.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnTransactionRequestAction.Visible = true;
			this.gridViewTransactionRequests.Columns.Add(this.columnTransactionRequestAction);

			this.columnTransactionRequestTableId = new GridColumn();
			this.columnTransactionRequestTableId.Name = this.columnTransactionRequestTableId.FieldName = "TableId";
			this.columnTransactionRequestTableId.Caption = "TableId (Object Name)";
			this.columnTransactionRequestTableId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnTransactionRequestTableId.Visible = true;
			this.gridViewTransactionRequests.Columns.Add(this.columnTransactionRequestTableId);

			this.columnTransactionRequestObjectId = new GridColumn();
			this.columnTransactionRequestObjectId.Name = this.columnTransactionRequestObjectId.FieldName = "Id";
			this.columnTransactionRequestObjectId.Caption = "ObjectId";
			this.columnTransactionRequestObjectId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnTransactionRequestObjectId.Visible = true;
			this.gridViewTransactionRequests.Columns.Add(this.columnTransactionRequestObjectId);

			this.columnTransactionRequestPropertyValues = new GridColumn();
			this.columnTransactionRequestPropertyValues.Name = this.columnTransactionRequestPropertyValues.FieldName = "PropertyValues";
			this.columnTransactionRequestPropertyValues.Caption = "Property Values: Index Name=Value";
			this.columnTransactionRequestPropertyValues.ColumnEdit = this.repositoryItemTransactionRequestPropertyValues;
			this.columnTransactionRequestPropertyValues.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnTransactionRequestPropertyValues.Visible = true;
			this.gridViewTransactionRequests.Columns.Add(this.columnTransactionRequestPropertyValues);
			this.gridControlTransactionRequests.RepositoryItems.Add(this.repositoryItemTransactionRequestPropertyValues);

			this.gridViewTransactionRequests.OptionsBehavior.AutoPopulateColumns = false;
			this.gridViewTransactionRequests.OptionsView.ShowGroupPanel = false;
			this.gridViewTransactionRequests.OptionsView.ColumnAutoWidth = true;
			this.gridViewTransactionRequests.OptionsView.RowAutoHeight = true;
			GridHelper.SetViewOnlyMode(this.gridViewTransactionRequests);

			this.gridControlTransactionRequests.DataSource = this.dataSourceTransactionRequests;


			//
			// Datastore Actions
			//
			this.columnDatastoreAction = new GridColumn();
			this.columnDatastoreAction.Name = this.columnDatastoreAction.FieldName = this.columnDatastoreAction.Caption = "Action";
			this.columnDatastoreAction.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnDatastoreAction.Visible = true;
			this.gridViewDatastoreActions.Columns.Add(this.columnDatastoreAction);

			this.columnDatastoreTableName = new GridColumn();
			this.columnDatastoreTableName.Name = this.columnDatastoreTableName.FieldName = "TableName";
			this.columnDatastoreTableName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnDatastoreTableName.Visible = true;
			this.gridViewDatastoreActions.Columns.Add(this.columnDatastoreTableName);

			this.columnDatastoreObjectId = new GridColumn();
			this.columnDatastoreObjectId.Name = this.columnDatastoreObjectId.FieldName = "Id";
			this.columnDatastoreObjectId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnDatastoreObjectId.Visible = true;
			this.gridViewDatastoreActions.Columns.Add(this.columnDatastoreObjectId);

			this.columnDatastorePropertyValues = new GridColumn();
			this.columnDatastorePropertyValues.Name = this.columnDatastorePropertyValues.FieldName = "PropertyValues";
			this.columnDatastorePropertyValues.Caption = "Property Values: Index Name=Value";
			this.columnDatastorePropertyValues.ColumnEdit = this.repositoryItemDatastoreActionPropertyValues;
			this.columnDatastorePropertyValues.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnDatastorePropertyValues.Visible = true;
			this.gridViewDatastoreActions.Columns.Add(this.columnDatastorePropertyValues);
			this.gridControlDatastoreActions.RepositoryItems.Add(this.repositoryItemDatastoreActionPropertyValues);

			this.gridViewDatastoreActions.OptionsBehavior.AutoPopulateColumns = false;
			this.gridViewDatastoreActions.OptionsView.ShowGroupPanel = false;
			this.gridViewDatastoreActions.OptionsView.ColumnAutoWidth = true;
			this.gridViewDatastoreActions.OptionsView.RowAutoHeight = true;
			GridHelper.SetViewOnlyMode(this.gridViewDatastoreActions);

			this.gridControlDatastoreActions.DataSource = this.dataSourceDatastoreActions;


			//
			// Rollback
			//
			this.columnRollbackAction = new GridColumn();
			this.columnRollbackAction.Name = this.columnRollbackAction.FieldName = this.columnRollbackAction.Caption = "Action";
			this.columnRollbackAction.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnRollbackAction.Visible = true;
			this.gridViewRollbackActions.Columns.Add(this.columnRollbackAction);

			this.columnRollbackTableId = new GridColumn();
			this.columnRollbackTableId.Name = this.columnRollbackTableId.FieldName = this.columnRollbackTableId.Caption = "TableId";
			this.columnRollbackTableId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnRollbackTableId.Visible = true;
			this.gridViewRollbackActions.Columns.Add(this.columnRollbackTableId);

			this.columnRollbackObjectId = new GridColumn();
			this.columnRollbackObjectId.Name = this.columnRollbackObjectId.FieldName = "Id";
			this.columnRollbackObjectId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnRollbackObjectId.Visible = true;
			this.gridViewRollbackActions.Columns.Add(this.columnRollbackObjectId);

			this.columnRollbackPropertyValues = new GridColumn();
			this.columnRollbackPropertyValues.Name = this.columnRollbackPropertyValues.FieldName = "PropertyValues";
			this.columnRollbackPropertyValues.Caption = "Property Values: Index Name=Value";
			this.columnRollbackPropertyValues.ColumnEdit = this.repositoryItemRollbackActionPropertyValues;
			this.columnRollbackPropertyValues.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnRollbackPropertyValues.Visible = true;
			this.gridViewRollbackActions.Columns.Add(this.columnRollbackPropertyValues);
			this.gridControlRollbackActions.RepositoryItems.Add(this.repositoryItemRollbackActionPropertyValues);

			this.gridViewRollbackActions.OptionsBehavior.AutoPopulateColumns = false;
			this.gridViewRollbackActions.OptionsView.ShowGroupPanel = false;
			this.gridViewRollbackActions.OptionsView.ColumnAutoWidth = true;
			this.gridViewRollbackActions.OptionsView.RowAutoHeight = true;
			GridHelper.SetViewOnlyMode(this.gridViewRollbackActions);

			this.gridControlRollbackActions.DataSource = this.dataSourceRollbackActions;
		}

		//protected override void OnBindingObjectChange(object oldBindingObject, object bindingObject)
		//{
		//	base.OnBindingObjectChange(oldBindingObject, bindingObject);
		//	this.OnRefreshBindingObject();
		//}

		//protected new FormMain? Context => base.Context as FormMain;
		bool isFirstTime = true;

		protected override void OnRefreshBindingObject(object? requester)
		{
			base.OnRefreshBindingObject(requester);

			//if (this.isFirstTime)
			//{
			//	this.isFirstTime = false;

			//	return; // For a first time do nothing
			//}




			//if (this.isFirstTime)
			//{
			//	Thread refreshThread = new Thread(() =>
			//	{
			//		TransactionInfoRow transactionInfoRow = this.BindingObject as TransactionInfoRow;

			//		transactionInfoRow.RollbackActions ??= this.ReadRollbackActions();
			//		transactionInfoRow.DatastoreActions ??= this.ReadDatastoreActions();

			//		this.isFirstTime = false;
			//	});

			//	refreshThread.IsBackground = true;
			//	refreshThread.Priority = ThreadPriority.BelowNormal;
			//	refreshThread.Start();
			//}

			this.DoRefreshBindingObject();
		}

		protected void DoRefreshBindingObject()
		{

			//while (this.isFirstTime)
			//	Thread.Sleep(500);

			TransactionInfoRow transactionInfoRow = (this.BindingObject as TransactionInfoRow)!;

			//var objectModel = this.Context?.GetServerObjectModel(41).GetAwaiter().GetResult();
			//var objectModel2 = Program.MonitorClient.GetServerObjectModel(41).GetAwaiter().GetResult();

			this.editorTransactionId.Text = transactionInfoRow.TransactionId.ToString();
			this.editorStatus.Text = transactionInfoRow.Status.ToString();
			this.editorCreationTime.Text = transactionInfoRow.CreationTime.ToString();
			this.editorUser.Text = transactionInfoRow.UserId.ToString();

			//this.editorNumberOfActions.Text = transactionInfoRow.TransactionActions.Length.ToString();

			transactionInfoRow.TransactionRequests ??= this.ReadTransactionRequests();
			transactionInfoRow.DatastoreActions ??= this.ReadDatastoreActions();
			transactionInfoRow.RollbackTransactionActions ??= SystemTransaction.GetRollbackTransactionActions(transactionInfoRow.RollbackActionData, transactionInfoRow.CodePage, transactionInfoRow.IsRollbackActionDataCompressed, transactionInfoRow.CompressionAlgorithm, this.Context!.GetServerObjectModel);

			//
			// Transaction Requests
			//
			this.labelControlNoOfTransactionActions.Text = String.Format("({0} Action{1}):", transactionInfoRow.TransactionRequests.Count(), transactionInfoRow.TransactionRequests.Count() > 1 ? "s" : "");

			this.gridViewTransactionRequests.BeginUpdate();
			this.dataSourceTransactionRequests.Clear();

			for (int i = 0; i < transactionInfoRow.TransactionRequests.Count(); i++)
				this.AppendTransactionRequestRow(i + 1, transactionInfoRow.TransactionRequests.ElementAt(i));

			this.columnTransactionRequestAction.BestFit();
			this.columnTransactionRequestTableId.BestFit();
			this.columnTransactionRequestObjectId.BestFit();
			this.gridViewTransactionRequests.EndUpdate();

			//
			// Datastore Actions
			//
			this.labelControlNoOfDatastoreActions.Text = String.Format("({0} Action{1}):", transactionInfoRow.DatastoreActions.Count(), transactionInfoRow.DatastoreActions.Count() > 1 ? "s" : "");
		
			this.gridViewDatastoreActions.BeginUpdate();
			this.dataSourceDatastoreActions.Clear();

			for (int i = 0; i < transactionInfoRow.DatastoreActions.Count(); i++)
				this.AppendDatastoreActionRow(i + 1, transactionInfoRow.DatastoreActions.ElementAt(i));

			this.columnDatastoreAction.BestFit();
			this.columnDatastoreTableName.BestFit();
			this.columnDatastoreObjectId.BestFit();
			this.gridViewDatastoreActions.EndUpdate();

			//
			// Rollback Actions
			//
			this.labelControlNoOfRollbackActions.Text = String.Format("({0} Action{1}):", transactionInfoRow.RollbackTransactionActions.Count(), transactionInfoRow.RollbackTransactionActions.Count() > 1 ? "s" : "");
			this.labelControlRollbackActionDataNoOfBytes.Text = String.Format("{0} Byte{1}", transactionInfoRow.RollbackActionData?.Length ?? 0, transactionInfoRow.RollbackActionData?.Length > 1 ? "s" : "");
			this.editorRollbackActionData.Text = BitConverter.ToString(transactionInfoRow.RollbackActionData ?? new byte[0]);
			//this.editorRollbackTransactionActionData.Text = BitConverter.ToString(transactionInfoRow.TransactionRequestData ?? new byte[0]);

			this.gridViewRollbackActions.BeginUpdate();
			this.dataSourceRollbackActions.Clear();

			for (int i = 0; i < transactionInfoRow.RollbackTransactionActions.Count(); i++)
				this.AppendRollbackActionRow(i + 1, transactionInfoRow.RollbackTransactionActions.ElementAt(i));

			this.columnRollbackAction.BestFit();
			this.columnRollbackTableId.BestFit();
			this.columnRollbackObjectId.BestFit();
			this.gridViewRollbackActions.EndUpdate();

			//this.labelControlNumberOfActions.Text = String.Format("Transaction Action Details ({0} Action{1}):", transactionInfoRow.TransactionActions.Length,
			//																										transactionInfoRow.TransactionActions.Length > 1 ? "s" : "");
		}

		private void AppendTransactionRequestRow(int actionNumber, TransactionActionInfo transactionAction)
		{
			//ServerObjectModelInfo objectModel = Program.MonitorClient.GetServerObjectModelInfo(transactionActionLog.TableId).GetAwaiter().GetResult();
			ServerObjectModelInfo? serverObjectModel = this.Context?.GetServerObjectModel(transactionAction.TableId);

			string action = transactionAction.ActionType.ToString(); //String.Format("{0} ({1})", (int)transactionAction.ActionType, transactionAction.ActionType.ToString());
																  //string objectKey = String.Format("{0} ({1})", transactionActionLog.ObjectKey.ToObjectKeyString(), transactionActionLog.GetObjectModel().ObjectType.Name);
			string tableIdText = String.Format("{0} ({1})", transactionAction.TableId, serverObjectModel?.ObjectName);
			string propertyInfo = this.CreatePropertyIndexValuesString(serverObjectModel, transactionAction.PropertyIndexValues); 
			TransactionActionRow row = new TransactionActionRow(action, tableIdText, transactionAction.ObjectId, propertyInfo);
			int rowIndex = this.dataSourceTransactionRequests.Count;

			this.dataSourceTransactionRequests.Add(row);
			//this.dataSourceDatastoreTransactionActionDetails.RefreshRow(rowIndex);
		}

		private void AppendDatastoreActionRow(int actionNumber, DatastoreActionInfo datastoreAction)
		{
			string action = datastoreAction.ActionType.ToString(); //String.Format("{0} ({1})", (int)transactionAction.ActionType, transactionAction.ActionType.ToString());
																   //string objectKey = String.Format("{0} ({1})", datastoreAction.ObjectKey.ToObjectKeyString(), datastoreAction.GetObjectModel().ObjectType.Name);
																   //ServerObjectModelInfo objectModel = Program.MonitorClient.GetServerObjectModelInfo(datastoreAction.TableId).GetAwaiter().GetResult();
			ServerObjectModelInfo? serverObjectModel = this.Context?.GetServerObjectModel(datastoreAction.TableId);
			string propertyInfo = this.CreatePropertyIndexValuesString(serverObjectModel, datastoreAction.PropertyIndexValues, startIndex: (datastoreAction.ActionType == TransactionActionType.Insert) ? 1 : 0);
			DatastoreActionRow row = new DatastoreActionRow(action, serverObjectModel?.TableName ?? "unknown object name/table", datastoreAction.ObjectId, propertyInfo);
			int rowIndex = this.dataSourceDatastoreActions.Count;

			this.dataSourceDatastoreActions.Add(row);
			//this.dataSourceDatastoreTransactionActionDetails.RefreshRow(rowIndex);
		}

		private void AppendRollbackActionRow(int actionNumber, TransactionActionInfo rollbackAction)
		{
			//ServerObjectModelInfo objectModel = Program.MonitorClient.GetServerObjectModelInfo(transactionActionLog.TableId).GetAwaiter().GetResult();
			ServerObjectModelInfo? serverObjectModel = this.Context?.GetServerObjectModel(rollbackAction.TableId);

			string action = rollbackAction.ActionType.ToString(); //String.Format("{0} ({1})", (int)transactionAction.ActionType, transactionAction.ActionType.ToString());
																		//string objectKey = String.Format("{0} ({1})", transactionActionLog.ObjectKey.ToObjectKeyString(), transactionActionLog.GetObjectModel().ObjectType.Name);
			string tableIdText = String.Format("{0} ({1})", rollbackAction.TableId, serverObjectModel?.ObjectName);
			string propertyInfo = (rollbackAction.ActionType == TransactionActionType.Update) ? "Old Values: " : String.Empty;

			propertyInfo += this.CreatePropertyIndexValuesString(serverObjectModel, rollbackAction.PropertyIndexValues);

			TransactionActionRow row = new TransactionActionRow(action, tableIdText, rollbackAction.ObjectId, propertyInfo);
			int rowIndex = this.dataSourceRollbackActions.Count;

			this.dataSourceRollbackActions.Add(row);
			//this.dataSourceDatastoreTransactionActionDetails.RefreshRow(rowIndex);
		}


		//private string CreateTransactionRequestsInfoText(TransactionActionInfo transactionAction, ServerObjectModelInfo objectModel)
		//{
		//	string result = String.Empty;
		//	//string splitter = String.Empty;

		//	if (transactionAction.ActionType != TransactionActionType.Insert)
		//	{
		//		if (transactionAction.ActionType == TransactionActionType.Update)
		//			result += "Old Values: ";

		//		if (objectModel != null && transactionAction.PropertyIndexValues != null)
		//		{
		//			result = this.CreatePropertyIndexValuesString(objectModel, transactionAction.PropertyIndexValues);

		//			//for (int i = 0; i < transactionAction.PropertyIndexValues.Count(); i++)
		//			//{
		//			//	var item = transactionAction.PropertyIndexValues.ElementAt(i);

		//			//	//if (propertyIndex == SimpleObject.IndexPropertyId)
		//			//	//	continue;

		//			//	//IPropertyModel propertyModel = objectModel.PropertyModels.GetPropertyModel(propertyIndex);
		//			//	ServerPropertyInfo propertyInfo = objectModel.GetPropertyInfo(item.PropertyIndex);
		//			//	object propertyValue = transactionAction.PropertyIndexValues.ElementAt(i);
		//			//	int propertyTypeId = propertyInfo.PropertyTypeId;
		//			//	string propertyValueString = this.GePropertyValueString(propertyInfo, propertyValue); // SimpleObject.GetPropertyValueString(propertyModel, propertyValue);

		//			//	result += String.Format("{0}{1} {2}={3}", splitter, propertyInfo.PropertyIndex, propertyInfo.PropertyName, propertyValueString); //, propertyTypeId);
		//			//	splitter = ", ";
		//			//}
		//		}
		//	}

		//	return result;
		//}

		//private string CreateDatastoreActionInfoText(DatastoreActionInfo datastoreAction, ServerObjectModelInfo objectModel)
		//{
		//	string result = String.Empty;
		//	//string splitter = String.Empty;

		//	if (datastoreAction.PropertyIndexValues != null)
		//	{
		//		//ServerObjectModelInfo serverObjectModelInfo = Program.AppClient.GetServerObjectModelInfoFromCache(datastoreAction.TableId);
		//		int startIndex = (datastoreAction.ActionType == TransactionActionType.Insert) ? 1 : 0; // Avoid Id for insert

		//		result = this.CreatePropertyIndexValuesString(objectModel, datastoreAction.PropertyIndexValues, startIndex);

		//		//if (datastoreAction.PropertyIndexValues != null)
		//		//{
		//		//	for (int i = startIndex; i < datastoreAction.PropertyIndexValues.Count(); i++) // avoid Id
		//		//	{
		//		//		var propertyInfo = datastoreAction.PropertyIndexValues.ElementAt(i);

		//		//		//if (propertyModel.Index == SimpleObject.IndexPropertyId)
		//		//		//	continue;

		//		//		IServerPropertyInfo propertyModel = objectModel[propertyInfo.PropertyIndex];
		//		//		string propertyValueString = this.GePropertyValueString(propertyModel, propertyInfo.PropertyValue); // SimpleObject.GetPropertyValueString(propertyModel, propertyValue);

		//		//		//if (propertyModel.PropertyType.IsEnum)
		//		//		//	propertyValueString = String.Format("{0}.{1}", propertyModel.PropertyType.Name, Enum.GetName(propertyModel.PropertyType, propertyValue));

		//		//		result += String.Format("{0}{1} {2}={3}", splitter, propertyInfo.PropertyIndex, propertyModel.PropertyName, propertyValueString); //, propertyTypeId);
		//		//		splitter = "; ";
		//		//	}
		//		//}
		//	}

		//	return result;
		//}

		public TransactionActionInfo[] ReadTransactionRequests()
		{
			TransactionActionInfo[] result;

			if (this.BindingObject is TransactionInfoRow transactionInfoRow && this.Context != null)
			{
				SequenceReader reader = new SequenceReader(transactionInfoRow.TransactionRequestData, Encoding.GetEncoding(transactionInfoRow.CodePage));
				int actionCount = reader.ReadInt32Optimized();
				TransactionActionInfo[] transactionActions = new TransactionActionInfo[actionCount];

				for (int i = 0; i < actionCount; i++)
				{
					TransactionActionInfo item = new TransactionActionInfo();

					item.ReadFrom(ref reader, this.Context.GetServerObjectModel);
					transactionActions[i] = item;
				}

				result = transactionActions;
			}
			else
			{
				result = new TransactionActionInfo[0];
			}

			return result;
		}

		private DatastoreActionInfo[] ReadDatastoreActions()
		{
			DatastoreActionInfo[] result;

			if (this.BindingObject is TransactionInfoRow transactionInfoRow && this.Context != null)
			{
				SequenceReader reader = new SequenceReader(transactionInfoRow.DatastoreActionsData, Encoding.GetEncoding(transactionInfoRow.CodePage));
				int datastoreActionCount = reader.ReadInt32Optimized();
				DatastoreActionInfo[] datastoreActions = new DatastoreActionInfo[datastoreActionCount];

				for (int i = 0; i < datastoreActionCount; i++)
				{
					DatastoreActionInfo item = new DatastoreActionInfo();

					item.ReadFrom(ref reader, this.Context.GetServerObjectModel);
					datastoreActions[i] = item;
				}

				result = datastoreActions;
			}
			else
			{
				result = new DatastoreActionInfo[0];
			}

			return result;
		}

		//private IEnumerable<TransactionActionInfo> ReadRollbackActions()
		//{
		//	IEnumerable<TransactionActionInfo> result;

		//	if (this.BindingObject is TransactionInfoRow transactionInfoRow && this.Context != null)
		//	{
		//		if (transactionInfoRow != null && transactionInfoRow.IsRollbackActionDataCompressed)
		//			transactionInfoRow.RollbackActionData = SystemTransaction.Decompress(transactionInfoRow.RollbackActionData);

		//		SequenceReader reader = new SequenceReader(transactionInfoRow.RollbackActionData, transactionInfoRow.characterEncoding);
		//		int count = reader.ReadInt32Optimized();
		//		TransactionActionInfo[] transactionActions = new TransactionActionInfo[count];

		//		for (int i = 0; i < count; i++)
		//		{
		//			TransactionActionInfo item = new TransactionActionInfo();

		//			item.ReadFrom(ref reader, this.Context.GetServerObjectModel);
		//			transactionActions[i] = item;
		//		}

		//		result = transactionActions;
		//	}
		//	else
		//	{
		//		result = new TransactionActionInfo[0];
		//	}

		//	return result;
		//}


		//private ServerObjectModelInfo? GetServerObjectModel(int tableId)
		//{
		//	ServerObjectModelInfo? result = null;

		//	if (this.Context is FormMain formMain)
		//		result = formMain.GetServerObjectModel(tableId);

		//	return result;
		//}

		class TransactionActionRow : TransactionLogBaseRow
		{
			public TransactionActionRow(string actionText, string tableIdText, long objectId, string propertyValuesText)
				: base(actionText, objectId, propertyValuesText)
			{
				this.TableId = tableIdText;
			}

			public string TableId { get; private set; }
		}

		class DatastoreActionRow : TransactionLogBaseRow
		{
			public DatastoreActionRow(string actionText, string tableName, long objectId, string propertyValuesText)
				: base(actionText, objectId, propertyValuesText)
			{
				this.TableName = tableName;
			}

			public string TableName { get; private set; }
		}

		class TransactionLogBaseRow
		{
			public TransactionLogBaseRow(string actionText, long id, string propertyValuesText)
			{
				this.Action = actionText;
				this.Id = id.ToString();
				this.PropertyValues = propertyValuesText;
			}

			public string Action { get; private set; }
			public string Id { get; private set; }
			public string PropertyValues { get; private set; }
		}
	}
}
