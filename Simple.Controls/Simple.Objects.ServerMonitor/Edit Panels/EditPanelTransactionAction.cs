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
using Simple.Serialization;
using Simple.Modeling;
using Simple.Objects;
using Simple.Controls;
using Simple.Objects.SocketProtocol;
using Simple.SocketEngine;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraEditors.Repository;


namespace Simple.Objects.ServerMonitor
{
	[SystemRequestArgs((int)SystemRequest.TransactionRequest)]
	public partial class EditPanelTransactionAction : EditPanelSessionPackageRequestResponse
	{
		private GridColumn columnAction, columnTableId, columnObjectId, columnPropertyValues;
		private BindingList<TransactionRequestRow> dataSourceTransactionRequestDetails = new BindingList<TransactionRequestRow>();
		RepositoryItemMemoEdit repositoryItemPropertyValues = new RepositoryItemMemoEdit();

		public EditPanelTransactionAction()
		{
			InitializeComponent();

			this.columnAction = new GridColumn();
			this.columnAction.Name = this.columnAction.FieldName = this.columnAction.Caption = "Action";
			this.columnAction.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnAction.OptionsFilter.AllowFilter = false;
			//this.columnAction.OptionsColumn.FixedWidth = true;
			//this.columnAction.ColumnEdit = repositoryItemPropertyValues;
			this.columnAction.Visible = true;
			this.gridViewGetRequestDetails.Columns.Add(this.columnAction);

			this.columnTableId = new GridColumn();
			this.columnTableId.Name = this.columnTableId.FieldName = "TableId";
			this.columnTableId.Caption = "TableId";
			this.columnTableId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnTableId.OptionsFilter.AllowFilter = false;
			//this.columnObjectKey.OptionsColumn.FixedWidth = true;
			//this.columnObjectKey.ColumnEdit = repositoryItemPropertyValues;
			this.columnTableId.Visible = true;
			this.gridViewGetRequestDetails.Columns.Add(this.columnTableId);

			this.columnObjectId = new GridColumn();
			this.columnObjectId.Name = this.columnObjectId.FieldName = "ObjectId";
			this.columnObjectId.Caption = "Id";
			this.columnObjectId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnObjectId.OptionsFilter.AllowFilter = false;
			this.columnObjectId.Visible = true;
			this.gridViewGetRequestDetails.Columns.Add(this.columnObjectId);

			this.columnPropertyValues = new GridColumn();
			this.columnPropertyValues.Name = this.columnPropertyValues.FieldName = "PropertyValues";
			this.columnPropertyValues.Caption = "Property Values: Index Name=Value";
			this.columnPropertyValues.ColumnEdit = this.repositoryItemPropertyValues;
			this.columnPropertyValues.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.columnPropertyValues.OptionsFilter.AllowFilter = false;
			this.columnPropertyValues.Visible = true;
			this.gridViewGetRequestDetails.Columns.Add(this.columnPropertyValues);
			this.gridControlGetRequestsDetails.RepositoryItems.Add(this.repositoryItemPropertyValues);

			this.gridViewGetRequestDetails.OptionsBehavior.AutoPopulateColumns = false;
			this.gridViewGetRequestDetails.OptionsView.ShowGroupPanel = false;
			this.gridViewGetRequestDetails.OptionsView.ColumnAutoWidth = true;
			this.gridViewGetRequestDetails.OptionsView.RowAutoHeight = true;
			GridHelper.SetViewOnlyMode(this.gridViewGetRequestDetails);

			this.gridControlGetRequestsDetails.DataSource = this.dataSourceTransactionRequestDetails;
		}

		protected override void OnRefreshBindingObject(object? requester)
		{
			base.OnRefreshBindingObject(requester);

			//MonitorProcessTransactionRequestArgs requestArgs = this.PackageInfoRow.RequestArgs as MonitorProcessTransactionRequestArgs;
			//ProcessTransactionResponseArgs responseArgs = this.PackageInfoRow.ResponseArgs as ProcessTransactionResponseArgs;

			ProcessTransactionRequestArgs? requestArgs = this.PackageInfoRow?.RequestOrMessagePackageReader.PackageInfo.PackageArgs as ProcessTransactionRequestArgs;
			ProcessTransactionResponseArgs? responseArgs = this.PackageInfoRow?.ResponsePackageReader?.PackageInfo.PackageArgs as ProcessTransactionResponseArgs;

			this.labelControlTransactionRequestNumOfActions.Text = String.Format("({0} Action{1}):", requestArgs?.TransactionActionInfoList?.Count(), requestArgs?.TransactionActionInfoList?.Count() > 1 ? "s" : "");
			this.gridViewGetRequestDetails.BeginUpdate();
			this.dataSourceTransactionRequestDetails.Clear();

			for (int i = 0; i < requestArgs.TransactionActionInfoList?.Count(); i++)
				this.AppendTransactionRequestActionRow(i + 1, requestArgs.TransactionActionInfoList.ElementAt(i));

			this.columnAction.BestFit();
			this.columnTableId.BestFit();
			this.columnObjectId.BestFit();
			//this.gridViewGetRequestDetails.BestFitColumns();
			this.gridViewGetRequestDetails.EndUpdate();

			this.editorTransactionSucceed.Text = responseArgs?.TransactionSucceeded.ToString();
			this.editorTransactionId.Text = responseArgs?.TransactionId.ToString();

			if (responseArgs != null)
			{
				if (responseArgs.TransactionSucceeded)
				{
					string newIdsText = String.Empty;
					string separator = String.Empty;

					this.editorTransactionId.Text = responseArgs.TransactionId.ToString();

					//this.labelControlInfoMessage.Text = "New Object Ids:";

					if (responseArgs.NewObjectIds != null)
					{
						foreach (long id in responseArgs.NewObjectIds)
						{
							newIdsText += separator + id.ToString();
							separator = ", ";
						}
					}

					this.editorNewIds.Text = newIdsText;
				}
				else
				{
					this.editorNewIds.Text = String.Empty;
				}

				if (responseArgs.InfoMessage.IsNullOrEmpty())
				{
					this.labelControlInfoMessage.Visible = false;
					this.editorInfoMessage.Visible = false;
				}
				else
				{
					this.labelControlInfoMessage.Text = (responseArgs.TransactionSucceeded) ? "Info Message:" : "Error Description:";
					this.editorInfoMessage.Text = responseArgs.InfoMessage;
					this.labelControlInfoMessage.Visible = true;
					this.editorInfoMessage.Visible = true;
				}
			}
		}

		protected override RequestArgs CreateRequestArgs()
		{
			//return new MonitorProcessTransactionRequestArgs(Program.ModelDiscovery.GetObjectModel);ž
			ProcessTransactionRequestArgs result = new ProcessTransactionRequestArgs();

			return result;
			// (tableId) => Program.MonitorClient.GetServerObjectModelInfo(tableId).GetAwaiter().GetResult()!);
		}

		protected override ResponseArgs CreateResponseArgs()
		{
			//return new ProcessTransactionResponseArgs((this.PackageInfoRow.RequestArgs as MonitorProcessTransactionRequestArgs).NewObjectCount); // responsePackage.Reader);
			//return new ProcessTransactionResponseArgs((this.PackageInfoRow.RequestArgs as ProcessTransactionRequestArgsNew).TempClientObjectIds.Count); // responsePackage.Reader);
			ProcessTransactionResponseArgs result = new ProcessTransactionResponseArgs(); // (this.PackageInfoRow.RequestArgs as ProcessTransactionRequestArgs).NewObjectCount); // NewObjectIds.Count = TempClientObjectIds.Count;

			return result;
		}

		//private void AppendTransactionRequestActionRow(int actionNumber, TransactionActionRequestInfo transactionActionRequest)
		//{
		//	string action = transactionActionRequest.ActionType.ToString();
		//	//string objectKey = String.Format("{0} ({1})", transactionAction.ObjectKey.ToObjectKeyString(), transactionAction.ObjectModel.ObjectType.Name);
		//	string tableIdText = String.Format("{0} ({1})", transactionActionRequest.TableId, transactionActionRequest.ObjectModel.ObjectType.Name);
		//	string transactionActionInfo = this.CreateTransactionActionInfoText(transactionActionRequest);

		//	TransactionRequestRow row = new TransactionRequestRow(action, tableIdText, transactionActionRequest.ObjectId.ToString(), transactionActionInfo);
		//	//int rowIndex = this.dataSourceTransactionRequestDetails.Count;

		//	this.dataSourceTransactionRequestDetails.Add(row);
		//	//this.dataSourceTransactionRequestDetails.RefreshRow(rowIndex);
		//}

		private void AppendTransactionRequestActionRow(int actionNumber, TransactionActionInfo transactionActionWithDataInfo)
		{
			ServerObjectModelInfo? objectModel = this.Context?.GetServerObjectModel(transactionActionWithDataInfo.TableId);
			string action = transactionActionWithDataInfo.ActionType.ToString();
			//string objectKey = String.Format("{0} ({1})", transactionAction.ObjectKey.ToObjectKeyString(), transactionAction.ObjectModel.ObjectType.Name);
			string tableIdText = String.Format("{0} ({1})", transactionActionWithDataInfo.TableId, objectModel?.ObjectName);
			string transactionActionInfo = this.CreateTransactionActionInfoText(objectModel, transactionActionWithDataInfo);
			TransactionRequestRow row = new TransactionRequestRow(action, tableIdText, transactionActionWithDataInfo.ObjectId.ToString(), transactionActionInfo);
			//int rowIndex = this.dataSourceTransactionRequestDetails.Count;

			this.dataSourceTransactionRequestDetails.Add(row);
			//this.dataSourceTransactionRequestDetails.RefreshRow(rowIndex);
		}

		private string CreateTransactionActionInfoText(ServerObjectModelInfo objectModel, TransactionActionInfo transactionActionWithDataInfo)
		{
			string result = String.Empty;

			if (transactionActionWithDataInfo.PropertyIndexValues != null)
			{
				for (int i = 0; i < transactionActionWithDataInfo.PropertyIndexValues.Count(); i++)
				{
					var item = transactionActionWithDataInfo.PropertyIndexValues.ElementAt(i);
					var propertyModel = objectModel[item.PropertyIndex];

					if (transactionActionWithDataInfo.ActionType == TransactionActionType.Insert && propertyModel.PropertyIndex == SimpleObject.IndexPropertyId)
						continue;

					if (result.Length > 0)
						result += "; ";

					string propertyValueString = this.GePropertyValueString(propertyModel, item.PropertyValue);

					result += String.Format("{0} {1}={2}", propertyModel.PropertyIndex, propertyModel.PropertyName, propertyValueString);

				}
			}

			//switch (transactionActionRequest.ActionType)
			//{
			//	case DatastoreActionType.Insert:

			//		for (int i = 0; i < transactionActionRequest.PropertyValueSequence.PropertyValues.Length; i++)
			//		{
			//			IPropertyModel propertyModel = transactionActionRequest.ObjectModel.SerializablePropertySequence.PropertyModels[i];
			//			object propertyValue = transactionActionRequest.PropertyValueSequence.PropertyValues[i];

			//			if (i > 1)
			//				result += "; ";

			//			result += String.Format("{0}({1})={2}", propertyModel.Name, propertyModel.Index, SimpleObject.GetPropertyValueString(propertyValue));
			//		}

			//		break;

			//	case DatastoreActionType.Update:

			//		for (int i = 0; i < transactionActionRequest.PropertyValueSequence.PropertyValues.Length; i++)
			//		{
			//			IPropertyModel propertyModel = transactionActionRequest.PropertyValueSequence.PropertyModels[i];
			//			object propertyValue = transactionActionRequest.PropertyValueSequence.PropertyValues[i];

			//			if (i > 0)
			//				result += "; ";

			//			result += String.Format("{0} ({1})={2}", propertyModel.Name, propertyModel.Index, SimpleObject.GetPropertyValueString(propertyValue));
			//		}

			//		break;

			//	case DatastoreActionType.Delete:

			//		break;
			//}

			return result;
		}

		//protected override string GePropertyValueString(IServerPropertyInfo propertyModel, object? propertyValue)
		//{
		//	if (propertyValue is null)
		//		return "DBNull";

		//	return base.GePropertyValueString(propertyModel, propertyValue);
		//}


		//class MonitorProcessTransactionRequestArgs_OLD : RequestArgs // ProcessTransactionRequestArgs2
		//{
		//	private Func<int, ISimpleObjectModel> getObjectModel { get; set; }

		//	public MonitorProcessTransactionRequestArgs_OLD(Func<int, ISimpleObjectModel> getObjectModel)
		//	{
		//		this.getObjectModel = getObjectModel;
		//	}

		//	public int NewObjectCount { get; private set; }

		//	public List<TransactionActionRequestInfo>? TransactionActionRequests { get; private set; }

		//	public override void WriteTo(ref SequenceWriter writer, ISimpleSession session)
		//	{
		//		throw new NotImplementedException();
		//	}

		//	public override void ReadFrom(ref SequenceReader reader, ISimpleSession session)
		//	{
		//		base.ReadFrom(ref reader, session);

		//		List<SimpleObjectKeyOLD> tempClientObjectKeys = new List<SimpleObjectKeyOLD>();

		//		this.TransactionActionRequests = new List<TransactionActionRequestInfo>();

		//		while (reader.ReadBoolean()) // Is there new object that was created?
		//		{
		//			int tableId = reader.ReadInt32Optimized();
		//			long tempClientObjectId = -reader.ReadInt64Optimized();

		//			tempClientObjectKeys.Add(new SimpleObjectKeyOLD(tableId, tempClientObjectId));
		//		}

		//		this.NewObjectCount = tempClientObjectKeys.Count;

		//		foreach (SimpleObjectKeyOLD simpleObjectKey in tempClientObjectKeys)
		//		{
		//			ISimpleObjectModel objectModel = this.getObjectModel(simpleObjectKey.TableId);
		//			//int idPosition = objectModel.SerializablePropertySequence.PropertyIndexes.FirstOrDefault(indexer => indexer == SimpleObject.IndexPropertyId);
		//			//// object[] propertyValues = this.ReadPropertyValues(reader, objectModel.SerializablePropertySequence.PropertyModels, SimpleObjectManager.NormalizeWhenReadingByPropertyTypeWithoutDescription, defaultOptimization: false);
		//			//IPropertyModel[] propertyModels = objectModel.SerializablePropertySequence.PropertyModels;
		//			//object[] propertyValues = new object[propertyModels.Length];

		//			//propertyValues[idPosition] = simpleObjectKey.ObjectId; // propertyValues[0] = simpleObjectKey.ObjectId

		//			//for (int i = 1; i < propertyValues.Length; i++) // skip reading ObjectKey
		//			//	propertyValues[i] = this.ReadPropertyValue(reader, propertyModels[i], SimpleObjectManager.NormalizeWhenReadingByPropertyTypeWithoutDecryption, defaultOptimization: false);

		//			int propertyCount = reader.ReadInt32Optimized();
		//			//IPropertyModel[] propertyModelSequence = new IPropertyModel[propertyCount];
		//			int[] propertyIndexes = new int[propertyCount];
		//			object?[] propertyValues = new object[propertyCount];

		//			for (int i = 0; i < propertyCount; i++)
		//			{
		//				int propertyIndex = reader.ReadInt32Optimized();
		//				IPropertyModel propertyModel = objectModel.PropertyModels[propertyIndex];
		//				object? propertyValue = this.ReadPropertyValue(ref reader, propertyModel, SimpleObjectManager.NormalizeWhenReadingByPropertyTypeWithoutDecryption, defaultOptimization: false);

		//				propertyIndexes[i] = propertyIndex;
		//				propertyValues[i] = propertyValue;
		//			}

		//			TransactionActionRequestInfo transactionActionRequest = new TransactionActionRequestInfo()
		//			{
		//				ActionType = TransactionActionType.Insert,
		//				TableId = simpleObjectKey.TableId,
		//				ObjectId = simpleObjectKey.ObjectId,
		//				ObjectModel = objectModel,
		//				PropertyIndexes = propertyIndexes,
		//				PropertyValues = propertyValues,
		//			};

		//			this.TransactionActionRequests.Add(transactionActionRequest);
		//		}

		//		while (reader.ReadBoolean()) // Is there more data
		//		{
		//			int tableId = reader.ReadInt32Optimized();
		//			long objectId = reader.ReadInt64Optimized();
		//			ISimpleObjectModel objectModel = this.getObjectModel(tableId);
		//			TransactionActionType actionType;
		//			int[] propertyIndexes = null;
		//			object?[] propertyValues = null;

		//			if (reader.ReadBoolean()) // Update
		//			{
		//				actionType = TransactionActionType.Update;

		//				int propertyCount = reader.ReadInt32Optimized();

		//				propertyIndexes = new int[propertyCount];
		//				propertyValues = new object[propertyCount];

		//				for (int i = 0; i < propertyCount; i++)
		//				{
		//					int propertyIndex = reader.ReadInt32Optimized();
		//					IPropertyModel propertyModel = objectModel.PropertyModels[propertyIndex];
		//					object? propertyValue = this.ReadPropertyValue(ref reader, propertyModel, SimpleObjectManager.NormalizeWhenReadingByPropertyTypeWithoutDecryption, defaultOptimization: false);

		//					propertyIndexes[i] = propertyIndex;
		//					propertyValues[i] = propertyValue;
		//				}
		//			}
		//			else // Delete
		//			{
		//				actionType = TransactionActionType.Delete;
		//				propertyIndexes = new int[0];
		//				propertyValues = new object[0];
		//			}

		//			TransactionActionRequestInfo transactionActionRequest = new TransactionActionRequestInfo()
		//			{
		//				ActionType = actionType,
		//				TableId = tableId,
		//				ObjectId = objectId,
		//				ObjectModel = objectModel,
		//				PropertyIndexes = propertyIndexes,
		//				PropertyValues = propertyValues,
		//			};

		//			this.TransactionActionRequests.Add(transactionActionRequest);
		//		}
		//	}

		//	private object? ReadPropertyValue(ref SequenceReader reader, IPropertyModel propertyModel, Func<IPropertyModel, object?, object?> readNormalizer, bool defaultOptimization)
		//	{
		//		object? propertyValue;

		//		if (propertyModel.IsRelationObjectId)
		//		{
		//			propertyValue = reader.ReadInt64Optimized();

		//			if (reader.ReadBoolean()) // Is negative
		//				propertyValue = -(long)propertyValue; //this.NewObjectIdsByTempClientObjectId[-(long)propertyValue];
		//		}
		//		else
		//		{
		//			if (defaultOptimization && reader.ReadBoolean()) // propertyModel.PropertyType.IsValueType
		//			{
		//				propertyValue = propertyModel.DefaultValue;
		//			}
		//			else if (propertyModel.IsSerializationOptimizable)
		//			{
		//				propertyValue = reader.ReadOptimized(propertyModel.PropertyTypeId);
		//			}
		//			else
		//			{
		//				propertyValue = reader.Read(propertyModel.PropertyTypeId);
		//			}

		//			// Encrypted property values stay encrypted
		//			propertyValue = readNormalizer(propertyModel, propertyValue); // this.ObjectManager.NormalizeWhenReadingByPropertyType(propertyModel, propertyValue);
		//		}

		//		return propertyValue;
		//	}


		//	//private object ReadPropertyValue(SerializationReader reader, IPropertyModel propertyModel, Func<IPropertyModel, object, object> readNormalizer, bool defaultOptimization)
		//	//{
		//	//	object result = SimpleObjectManager.ReadObjectPropertyValue(reader, propertyModel, defaultOptimization);
		//	//	result = readNormalizer(propertyModel, result);

		//	//	return result;
		//	//}
		//}

		//struct TransactionActionRequestInfo
		//{
		//	public TransactionActionType ActionType { get; set; }
		//	public int TableId { get; set; }
		//	public long ObjectId { get; set; }
		//	public ISimpleObjectModel ObjectModel { get; set; }
		//	public int[] PropertyIndexes { get; set; }
		//	public object?[] PropertyValues { get; set; }
		//	//public PropertyValueSequence PropertyValueSequence { get; set; }
		//}

		//class TransactionActionRequest : SerializableObject, ISerialization // DatastoreTransactionAction
		//{
		//	private Func<int, ISimpleObjectModel> getObjectModel = null;
		//	private ISimpleObjectModel objectModel = null;

		//	public TransactionActionRequest(SerializationReader reader, Func<int, ISimpleObjectModel> getObjectModel)
		//	{
		//		this.getObjectModel = getObjectModel;
		//		this.ReadFrom(reader);
		//	}

		//	public TransactionActionType ActionType { get; private set; }
		//	public int TableId { get; private set; }
		//	public long ObjectId { get; private set; }
		//	public PropertyValueSequence PropertyValueSequence { get; private set; }

		//	public ISimpleObjectModel ObjectModel
		//	{
		//		get
		//		{
		//			if (this.objectModel == null)
		//				this.objectModel = this.getObjectModel(this.TableId);

		//			return this.objectModel;
		//		}
		//	}

		//	public override void ReadFrom(SerializationReader reader)
		//	{
		//		this.TableId = reader.ReadInt32Optimized();
		//		this.ObjectId = reader.ReadInt64Optimized();

		//		if (reader.ReadBoolean()) // Insert
		//		{
		//			this.ActionType = TransactionActionType.Insert;
		//			object[] propertyValues = this.ReadPropertyValues(reader, this.ObjectModel.SerializablePropertySequence.PropertyModels,
		//																	  SimpleObjectManager.NormalizeWhenReadingByPropertyTypeWithoutDecription, defaultOptimization: false);
		//			int idPosition = this.ObjectModel.SerializablePropertySequence.PropertyIndexes.FindIndex(indexer => indexer == SimpleObject.IndexPropertyId);

		//			propertyValues[idPosition] = this.ObjectId;

		//			this.PropertyValueSequence = new PropertyValueSequence(this.ObjectModel.SerializablePropertySequence, propertyValues);
		//		}
		//		else
		//		{
		//			if (reader.ReadBoolean()) // Update
		//			{
		//				this.ActionType = TransactionActionType.Update;

		//				int propertyCount = reader.ReadInt32Optimized();
		//				IPropertyModel[] propertyModelSequence = new IPropertyModel[propertyCount];
		//				object[] propertyValues = new object[propertyCount];

		//				for (int i = 0; i < propertyCount; i++)
		//				{
		//					int propertyIndex = reader.ReadInt32Optimized();
		//					IPropertyModel propertyModel = this.ObjectModel.PropertyModels[propertyIndex];
		//					object propertyValue = this.ReadPropertyValue(reader, propertyModel, SimpleObjectManager.NormalizeWhenReadingByPropertyTypeWithoutDecription, defaultOptimization: false);

		//					propertyModelSequence[i] = propertyModel;
		//					propertyValues[i] = propertyValue;
		//				}

		//				this.PropertyValueSequence = new PropertyValueSequence(propertyModelSequence, propertyValues);
		//			}
		//			else // Delete
		//			{
		//				this.ActionType = TransactionActionType.Delete;
		//				this.PropertyValueSequence = new PropertyValueSequence();
		//			}
		//		}
		//	}

		//	public override void WriteTo(SerializationWriter writer)
		//	{
		//		throw new NotImplementedException();
		//	}

		//	private object[] ReadPropertyValues(SerializationReader reader, IPropertyModel[] propertyModelSequence, Func<IPropertyModel, object, object> readNormalizer, bool defaultOptimization)
		//	{
		//		object[] result = new object[propertyModelSequence.Length];

		//		for (int i = 0; i < propertyModelSequence.Length; i++) // skip ObjectKey
		//		{
		//			IPropertyModel propertyModel = propertyModelSequence[i];

		//			if (propertyModel.Index == SimpleObject.IndexPropertyId) // Skip reading key
		//				continue;

		//			result[i] = this.ReadPropertyValue(reader, propertyModel, readNormalizer, defaultOptimization);
		//		}

		//		return result;
		//	}

		//	private object ReadPropertyValue(SerializationReader reader, IPropertyModel propertyModel, Func<IPropertyModel, object, object> readNormalizer, bool defaultOptimization)
		//	{
		//		object result = SimpleObjectManager.ReadObjectPropertyValue(reader, propertyModel, defaultOptimization);
		//		result = readNormalizer(propertyModel, result);

		//		return result;
		//	}
		//}

		class TransactionRequestRow
		{
			public TransactionRequestRow(string actionText, string tableIdText, string objectId, string propertyValuesText)
			{
				this.Action = actionText;
				this.TableId = tableIdText;
				this.ObjectId = objectId;
				this.PropertyValues = propertyValuesText;
			}

			public string Action { get; private set; }
			public string TableId { get; private set; }
			public string ObjectId { get; private set; }
			public string PropertyValues { get; private set; }
		}
	}
}