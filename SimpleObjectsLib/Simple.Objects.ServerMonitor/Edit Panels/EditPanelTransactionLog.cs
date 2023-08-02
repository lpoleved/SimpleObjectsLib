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
		private GridColumn columnTransactionAction, columnTransactionTableId, columnTransactionObjectId, columnTransactionPropertyValues;
		private GridColumn columnDatastoreAction, columnDatastoreTableName, columnDatastoreObjectId, columnDatastorePropertyValues;
		private BindingList<TransactionActionLogRow> dataSourceRollbackActions = new BindingList<TransactionActionLogRow>();
		private BindingList<DatastoreActionRow> dataSourceDatastoreActions = new BindingList<DatastoreActionRow>();
		private RepositoryItemMemoEdit repositoryItemRollbackActionPropertyValues = new RepositoryItemMemoEdit();
		private RepositoryItemMemoEdit repositoryItemDatastoreActionPropertyValues = new RepositoryItemMemoEdit();
		//private bool isFirst = true;

		public EditPanelTransactionLog()
		{
			InitializeComponent();

			this.columnTransactionAction = new GridColumn();
			this.columnTransactionAction.Name = this.columnTransactionAction.FieldName = this.columnTransactionAction.Caption = "Action";
			this.columnTransactionAction.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnTransactionAction.Visible = true;
			this.gridViewRollbackActions.Columns.Add(this.columnTransactionAction);

			this.columnTransactionTableId = new GridColumn();
			this.columnTransactionTableId.Name = this.columnTransactionTableId.FieldName = this.columnTransactionTableId.Caption = "TableId";
			this.columnTransactionTableId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnTransactionTableId.Visible = true;
			this.gridViewRollbackActions.Columns.Add(this.columnTransactionTableId);

			this.columnTransactionObjectId = new GridColumn();
			this.columnTransactionObjectId.Name = this.columnTransactionObjectId.FieldName = "Id";
			this.columnTransactionObjectId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnTransactionObjectId.Visible = true;
			this.gridViewRollbackActions.Columns.Add(this.columnTransactionObjectId);

			this.columnTransactionPropertyValues = new GridColumn();
			this.columnTransactionPropertyValues.Name = this.columnTransactionPropertyValues.FieldName = "PropertyValues";
			this.columnTransactionPropertyValues.Caption = "Property Values: Index Name=Value";
			this.columnTransactionPropertyValues.ColumnEdit = this.repositoryItemRollbackActionPropertyValues;
			this.columnTransactionPropertyValues.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnTransactionPropertyValues.Visible = true;
			this.gridViewRollbackActions.Columns.Add(this.columnTransactionPropertyValues);
			this.gridControlRollbackActions.RepositoryItems.Add(this.repositoryItemRollbackActionPropertyValues);

			this.gridViewRollbackActions.OptionsBehavior.AutoPopulateColumns = false;
			this.gridViewRollbackActions.OptionsView.ShowGroupPanel = false;
			this.gridViewRollbackActions.OptionsView.ColumnAutoWidth = true;
			this.gridViewRollbackActions.OptionsView.RowAutoHeight = true;
			GridHelper.SetViewOnlyMode(this.gridViewRollbackActions);

			this.gridControlRollbackActions.DataSource = this.dataSourceRollbackActions;

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
		}
		//protected override void OnBindingObjectChange(object oldBindingObject, object bindingObject)
		//{
		//	base.OnBindingObjectChange(oldBindingObject, bindingObject);
		//	this.OnRefreshBindingObject();
		//}

		protected new FormMain? Context => base.Context as FormMain;
		bool isFirstTime = true;

		protected override void OnRefreshBindingObject()
		{
			base.OnRefreshBindingObject();

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

			TransactionInfoRow transactionInfoRow = this.BindingObject as TransactionInfoRow;

			//var objectModel = this.Context?.GetServerObjectModel(41).GetAwaiter().GetResult();
			//var objectModel2 = Program.MonitorClient.GetServerObjectModel(41).GetAwaiter().GetResult();

			this.editorTransactionId.Text = transactionInfoRow.TransactionId.ToString();
			this.editorStatus.Text = transactionInfoRow.Status.ToString();
			this.editorCreationTime.Text = transactionInfoRow.CreationTime.ToString();
			this.editorUser.Text = transactionInfoRow.UserId.ToString();

			//this.editorNumberOfActions.Text = transactionInfoRow.TransactionActions.Length.ToString();

			transactionInfoRow.RollbackActions ??= this.ReadRollbackActions();
			transactionInfoRow.DatastoreActions ??= this.ReadDatastoreActions();


			this.labelControlNoOfDatastoreActions.Text = String.Format("({0} Action{1}):", transactionInfoRow.DatastoreActions.Count(), transactionInfoRow.DatastoreActions.Count() > 1 ? "s" : "");




			//this.labelControlNoOfTransactionActions.Text = String.Format("({0} Action{1}):", transactionInfoRow.TransactionRequests.Count(), transactionInfoRow.TransactionRequests.Count() > 1 ? "s" : "");
			this.labelControlRollbackActionDataNoOfBytes.Text = String.Format("{0} Byte{1}", transactionInfoRow.RollbackActionData?.Length ?? 0, transactionInfoRow.RollbackActionData?.Length > 1 ? "s" : "");
			this.editorTransactionActionData.Text = BitConverter.ToString(transactionInfoRow.RollbackActionData ?? new byte[0]);

			// Rollback Actions
			this.gridViewRollbackActions.BeginUpdate();
			this.dataSourceRollbackActions.Clear();

			for (int i = 0; i < transactionInfoRow.RollbackActions.Count(); i++)
				this.AppendTransactionActionRow(i + 1, transactionInfoRow.RollbackActions.ElementAt(i));

			this.columnTransactionAction.BestFit();
			this.columnTransactionTableId.BestFit();
			this.columnTransactionObjectId.BestFit();
			this.gridViewRollbackActions.EndUpdate();

			// Datastore Actions
			this.gridViewDatastoreActions.BeginUpdate();
			this.dataSourceDatastoreActions.Clear();

			for (int i = 0; i < transactionInfoRow.DatastoreActions.Count(); i++)
				this.AppendDatastoreActionRow(i + 1, transactionInfoRow.DatastoreActions.ElementAt(i));

			this.columnDatastoreAction.BestFit();
			this.columnDatastoreTableName.BestFit();
			this.columnDatastoreObjectId.BestFit();
			this.gridViewDatastoreActions.EndUpdate();
			//this.labelControlNumberOfActions.Text = String.Format("Transaction Action Details ({0} Action{1}):", transactionInfoRow.TransactionActions.Length,
			//																										transactionInfoRow.TransactionActions.Length > 1 ? "s" : "");
		}

		private void AppendTransactionActionRow(int actionNumber, TransactionActionInfo transactionActionLog)
		{
			//ServerObjectModelInfo objectModel = Program.MonitorClient.GetServerObjectModelInfo(transactionActionLog.TableId).GetAwaiter().GetResult();
			ServerObjectModelInfo? serverObjectModel = this.GetServerObjectModel(transactionActionLog.TableId);

			string action = transactionActionLog.ActionType.ToString(); //String.Format("{0} ({1})", (int)transactionAction.ActionType, transactionAction.ActionType.ToString());
																		//string objectKey = String.Format("{0} ({1})", transactionActionLog.ObjectKey.ToObjectKeyString(), transactionActionLog.GetObjectModel().ObjectType.Name);
			string tableIdText = String.Format("{0} ({1})", transactionActionLog.TableId, serverObjectModel.ObjectName);
			string transactionActionInfo = this.CreateTransactionRequestsInfoText(transactionActionLog, serverObjectModel);

			TransactionActionLogRow row = new TransactionActionLogRow(action, tableIdText, transactionActionLog.ObjectId, transactionActionInfo);
			int rowIndex = this.dataSourceRollbackActions.Count;

			this.dataSourceRollbackActions.Add(row);
			//this.dataSourceDatastoreTransactionActionDetails.RefreshRow(rowIndex);
		}

		private void AppendDatastoreActionRow(int actionNumber, DatastoreActionInfo datastoreAction)
		{
			string action = datastoreAction.ActionType.ToString(); //String.Format("{0} ({1})", (int)transactionAction.ActionType, transactionAction.ActionType.ToString());
																   //string objectKey = String.Format("{0} ({1})", datastoreAction.ObjectKey.ToObjectKeyString(), datastoreAction.GetObjectModel().ObjectType.Name);
																   //ServerObjectModelInfo objectModel = Program.MonitorClient.GetServerObjectModelInfo(datastoreAction.TableId).GetAwaiter().GetResult();
			ServerObjectModelInfo? serverObjectModel = this.GetServerObjectModel(datastoreAction.TableId);

			string actionInfo = this.CreateDatastoreActionInfoText(datastoreAction, serverObjectModel);

			DatastoreActionRow row = new DatastoreActionRow(action, serverObjectModel.TableName, datastoreAction.ObjectId, actionInfo);
			int rowIndex = this.dataSourceDatastoreActions.Count;

			this.dataSourceDatastoreActions.Add(row);
			//this.dataSourceDatastoreTransactionActionDetails.RefreshRow(rowIndex);
		}

		private string CreateTransactionRequestsInfoText(TransactionActionInfo transactionAction, ServerObjectModelInfo objectModel)
		{
			string result = String.Empty;
			//string splitter = String.Empty;

			if (transactionAction.ActionType != TransactionActionType.Insert)
			{
				if (transactionAction.ActionType == TransactionActionType.Update)
					result += "Old Values: ";

				if (objectModel != null && transactionAction.PropertyIndexValues != null)
				{
					result = this.CreatePropertyIndexValuesString(objectModel, transactionAction.PropertyIndexValues);

					//for (int i = 0; i < transactionAction.PropertyIndexValues.Count(); i++)
					//{
					//	var item = transactionAction.PropertyIndexValues.ElementAt(i);

					//	//if (propertyIndex == SimpleObject.IndexPropertyId)
					//	//	continue;

					//	//IPropertyModel propertyModel = objectModel.PropertyModels.GetPropertyModel(propertyIndex);
					//	ServerPropertyInfo propertyInfo = objectModel.GetPropertyInfo(item.PropertyIndex);
					//	object propertyValue = transactionAction.PropertyIndexValues.ElementAt(i);
					//	int propertyTypeId = propertyInfo.PropertyTypeId;
					//	string propertyValueString = this.GePropertyValueString(propertyInfo, propertyValue); // SimpleObject.GetPropertyValueString(propertyModel, propertyValue);

					//	result += String.Format("{0}{1} {2}={3}", splitter, propertyInfo.PropertyIndex, propertyInfo.PropertyName, propertyValueString); //, propertyTypeId);
					//	splitter = ", ";
					//}
				}
			}

			return result;
		}

		private string CreateDatastoreActionInfoText(DatastoreActionInfo datastoreAction, ServerObjectModelInfo objectModel)
		{
			string result = String.Empty;
			//string splitter = String.Empty;

			if (datastoreAction.PropertyIndexValues != null)
			{
				//ServerObjectModelInfo serverObjectModelInfo = Program.AppClient.GetServerObjectModelInfoFromCache(datastoreAction.TableId);
				int startIndex = (datastoreAction.ActionType == TransactionActionType.Insert) ? 1 : 0; // Avoid Id for insert

				result = this.CreatePropertyIndexValuesString(objectModel, datastoreAction.PropertyIndexValues, startIndex);

				//if (datastoreAction.PropertyIndexValues != null)
				//{
				//	for (int i = startIndex; i < datastoreAction.PropertyIndexValues.Count(); i++) // avoid Id
				//	{
				//		var propertyInfo = datastoreAction.PropertyIndexValues.ElementAt(i);

				//		//if (propertyModel.Index == SimpleObject.IndexPropertyId)
				//		//	continue;

				//		IServerPropertyInfo propertyModel = objectModel[propertyInfo.PropertyIndex];
				//		string propertyValueString = this.GePropertyValueString(propertyModel, propertyInfo.PropertyValue); // SimpleObject.GetPropertyValueString(propertyModel, propertyValue);

				//		//if (propertyModel.PropertyType.IsEnum)
				//		//	propertyValueString = String.Format("{0}.{1}", propertyModel.PropertyType.Name, Enum.GetName(propertyModel.PropertyType, propertyValue));

				//		result += String.Format("{0}{1} {2}={3}", splitter, propertyInfo.PropertyIndex, propertyModel.PropertyName, propertyValueString); //, propertyTypeId);
				//		splitter = "; ";
				//	}
				//}
			}

			return result;
		}


		private IEnumerable<TransactionActionInfo> ReadRollbackActions()
		{
			IEnumerable<TransactionActionInfo> result;

			if (this.BindingObject is TransactionInfoRow transactionInfoRow)
			{
				if (transactionInfoRow != null && transactionInfoRow.IsRollbackActionDataCompressed)
					transactionInfoRow.RollbackActionData = SystemTransaction.Decompress(transactionInfoRow.RollbackActionData);

				SequenceReader reader = new SequenceReader(transactionInfoRow.RollbackActionData, transactionInfoRow.characterEncoding);
				int count = reader.ReadInt32Optimized();
				TransactionActionInfo[] transactionActions = new TransactionActionInfo[count];

				for (int i = 0; i < count; i++)
				{
					TransactionActionInfo item = new TransactionActionInfo();

					item.ReadFrom(ref reader, this.GetServerObjectModel);
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

		public IEnumerable<DatastoreActionInfo> ReadDatastoreActions()
		{
			IEnumerable<DatastoreActionInfo> result;

			if (this.BindingObject is TransactionInfoRow transactionInfoRow)
			{
				SequenceReader reader = new SequenceReader(transactionInfoRow.DatastoreActionsData, transactionInfoRow.characterEncoding);
				int datastoreActionCount = reader.ReadInt32Optimized();
				DatastoreActionInfo[] datastoreActions = new DatastoreActionInfo[datastoreActionCount];

				for (int i = 0; i < datastoreActionCount; i++)
				{
					DatastoreActionInfo item = new DatastoreActionInfo();

					item.ReadFrom(ref reader, this.GetServerObjectModel);
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

		private ServerObjectModelInfo? GetServerObjectModel(int tableId)
		{
			ServerObjectModelInfo? result = null;

			if (this.Context is FormMain formMain)
				result = formMain.GetServerObjectModel(tableId);

			return result;
		}

		class TransactionActionLogRow : TransactionLogBaseRow
		{
			public TransactionActionLogRow(string actionText, string tableIdText, long objectId, string propertyValuesText)
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
