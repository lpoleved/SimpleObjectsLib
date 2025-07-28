using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Simple;
//using Simple.Serialization;
using Simple.Modeling;
using Simple.Controls;
using Simple.Controls.TreeList;
using Simple.Objects;
using Simple.Collections;

namespace Simple.Objects.Controls
{
	public partial class SystemEditPanelTransactionActionInfo : SystemEditPanelWithTabControl 
    {
		private int columnIndexIndex, columnNameIndex, columnValueIndex;
		//private const string strName = "Name";
		//private const string strValue = "Value";

		public SystemEditPanelTransactionActionInfo()
        {
            InitializeComponent();
			//this.splitContainerControlPropertyValues_SplitterMoved(this, null);

			this.columnIndexIndex = SimpleTreeList.AddColumn(this.treeListPropertyValues, "Index");
			this.columnNameIndex  = SimpleTreeList.AddColumn(this.treeListPropertyValues, "Name");
			this.columnValueIndex = SimpleTreeList.AddColumn(this.treeListPropertyValues, "Value");
			//this.columnOldValueIndex = SimpleTreeList.AddColumn(this.treeListPropertyValues, "OldValue", "Old Value");

			this.columnIndexIndex = SimpleTreeList.AddColumn(this.treeListDatastorePropertyValues, "Index");
			this.columnNameIndex  = SimpleTreeList.AddColumn(this.treeListDatastorePropertyValues, "Name");
			this.columnValueIndex = SimpleTreeList.AddColumn(this.treeListDatastorePropertyValues, "Value");
			//this.columnOldValueIndex = SimpleTreeList.AddColumn(this.treeListDatastorePropertyValues, "OldValue", "Old Value");
			
		}

		//public new SystemTransactionActionInfo BindingObject
		//{
		//	get { return base.BindingObject as SystemTransactionActionInfo; }
		//}

		//protected override void OnBindingObjectChange(object bindingObject, object oldBindingObject)
		//{
		//	base.OnBindingObjectChange(bindingObject, oldBindingObject);
		//	this.OnRefreshBindingObject();
		//}
		
		protected override void OnRefreshBindingObject()
		{
			base.OnRefreshBindingObject();

			var selectedTabPage = this.tabControl.SelectedTabPage;
			
			if (this.BindingObject is SystemTransactionActionInfo transactionActionInfo)
			{
				if (transactionActionInfo.TransactionActionLog != null)
				{
					TransactionActionInfo transactionActionLog = transactionActionInfo.TransactionActionLog;
					ISimpleObjectModel? objectModel = transactionActionInfo.ObjectManager.GetObjectModel(transactionActionLog.TableId);

					this.tabPageObjectName.PageVisible = true;
					this.editorActionType.Text = transactionActionLog.ActionType.ToString();
					this.editorObjectTableId.Text = String.Format("{0}  ({1})", transactionActionLog.TableId, objectModel.ObjectType.Name);
					this.editorObjectId.Text = transactionActionLog.ObjectId.ToString();
					this.editorStatus.Text = (transactionActionInfo.TransactionStatus == TransactionStatus.Completed) ? "Archived" : "Transaction " + transactionActionInfo.TransactionStatus.ToString();

					this.treeListPropertyValues.BeginUpdate();
					this.treeListPropertyValues.Nodes.Clear();

					if (transactionActionLog.PropertyIndexValues != null)
					{
						for (int i = 0; i < transactionActionLog.PropertyIndexValues.Count(); i++)
						{
							var item = transactionActionLog.PropertyIndexValues.ElementAt(i);
							IPropertyModel propertyModel = objectModel.PropertyModels[item.PropertyIndex];
							//object propertyValue = (transactionActionLog.Tag is object[]) ? (transactionActionLog.Tag as object[])[i] : null;
							TreeListNode node = this.treeListPropertyValues.AppendNode(null, parentNode: null);

							node[columnIndexIndex] = propertyModel.PropertyIndex.ToString();
							node[columnNameIndex] = propertyModel.PropertyName + ((!propertyModel.IsStorable) ? "*" : "");
							node[columnValueIndex] = this.GePropertyValueString(propertyModel, item.PropertyValue); // SimpleObject.GetPropertyValueString(propertyModel, propertyValue);
																											   //node[columnOldValueIndex] = SimpleObject.GetPropertyValueString(propertyModel, oldPropertyValue);
						}

						//this.treeListPropertyValues.Columns[this.columnOldValueIndex].Visible = true;
					}
					//else
					//{
					//	//this.treeListPropertyValues.Columns[this.columnOldValueIndex].Visible = false;

					//	PropertyValueSequence propertyValues = transactionActionLog.PropertyValueSequence;

					//	for (int i = 0; i < propertyValues.Length; i++)
					//	{
					//		IPropertyModel propertyModel = propertyValues.PropertyModels[i]; // datastoreTransactionAction.GetObjectModel().PropertyModels[propertyIndex];
					//		object propertyValue = propertyValues.PropertyValues[i];
					//		TreeListNode node = this.treeListPropertyValues.AppendNode(propertyValue, parentNode: null);

					//		node[columnIndexIndex] = propertyModel.Index.ToString();
					//		node[columnNameIndex] = propertyModel.Name;
					//		node[columnValueIndex] = SimpleObject.GetPropertyValueString(propertyModel, propertyValue);
					//	}
					//}

					this.treeListPropertyValues.Columns[0].BestFit();
					this.treeListPropertyValues.Columns[1].BestFit();
					//this.treeListPropertyValues.BestFitColumns();
					this.treeListPropertyValues.EndUpdate();
				}
				else
				{
					this.tabPageObjectName.PageVisible = false;
				}

				if (transactionActionInfo.DatastoreAction != null)
				{
					DatastoreActionInfo datastoreAction = transactionActionInfo.DatastoreAction;
					ISimpleObjectModel? objectModel = transactionActionInfo.ObjectManager.GetObjectModel(datastoreAction.TableId);

					this.tabPageDatastoreAction.PageVisible = true;
					this.editorDatastoreActionType.Text = datastoreAction.ActionType.ToString();
					this.editorDatastoreTableId.Text = String.Format("{0}  ({1})", datastoreAction.TableId, objectModel.ObjectType.Name);
					this.editorDatastoreObjectId.Text = datastoreAction.ObjectId.ToString();
					this.editorDatastoreStatus.Text = datastoreAction.DatastoreStatus.ToString();

					this.treeListDatastorePropertyValues.BeginUpdate();
					this.treeListDatastorePropertyValues.Nodes.Clear();

					if (datastoreAction.PropertyIndexValues != null)
					{
						for (int i = 0; i < datastoreAction.PropertyIndexValues.Count(); i++)
						{
							var item = datastoreAction.PropertyIndexValues.ElementAt(i);
							IPropertyModel propertyModel = objectModel.GetPropertyModel(item.PropertyIndex);
							//object propertyValue = (datastoreAction.Tag is object[]) ? (datastoreAction.Tag as object[])[i] : null;
							TreeListNode node = this.treeListDatastorePropertyValues.AppendNode(null, parentNode: null);

							node[columnIndexIndex] = propertyModel.PropertyIndex.ToString();
							node[columnNameIndex] = propertyModel.PropertyName;
							node[columnValueIndex] = this.GePropertyValueString(propertyModel, item.PropertyValue); // SimpleObject.GetPropertyValueString(propertyModel, propertyValue);

							//node[columnOldValueIndex] = SimpleObject.GetPropertyValueString(propertyModel, oldPropertyValue);
						}
						//this.treeListDatastorePropertyValues.Columns[this.columnOldValueIndex].Visible = true;
					}

					this.treeListDatastorePropertyValues.Columns[0].BestFit();
					this.treeListDatastorePropertyValues.Columns[1].BestFit();
					//this.treeListDatastorePropertyValues.BestFitColumns();
					this.treeListDatastorePropertyValues.EndUpdate();
				}
				else
				{
					this.tabPageDatastoreAction.PageVisible = false;
				}
			}

			if (selectedTabPage != null && selectedTabPage.PageVisible)
				this.tabControl.SelectedTabPage = selectedTabPage;
		}

		private string GePropertyValueString(IPropertyModel propertyModel, object? propertyValue)
		{
			if (propertyModel.PropertyType.IsEnum)
				return String.Format("{0} ({1}.{2})", Conversion.TryChangeType<int>(propertyValue), propertyModel.PropertyType.Name, Enum.GetName(propertyModel.PropertyType, propertyValue ?? "null"));

			return propertyValue?.ValueToString() ?? string.Empty;
		}

		//private string GetPropertyValueString(IPropertyModel propertyModel, object propertyValue)
		//{
		//	//return propertyValue.ValueToString();

		//	if (propertyValue == null)
		//		return String.Empty;

		//	return SimpleObject.GetPropertyValueString(propertyModel, propertyValue);
		//}


		//private void splitContainerControlPropertyValues_SplitterMoved(object sender, EventArgs e)
		//{
		//	this.labelControlOldPropertyValues.Left = this.splitContainerControlPropertyValues.SplitterPosition + 14;
		//}

		//private void splitContainerControlPropertyValues_Resize(object sender, EventArgs e)
		//{
		//	this.splitContainerControlPropertyValues.SplitterPosition = this.splitContainerControlPropertyValues.Width / 2;
		//}
	}

	public class SystemTransactionActionInfo
	{
		public SystemTransactionActionInfo(SimpleObjectManager objectManager)
		{
			this.ObjectManager = objectManager;
		}

		//public SystemTransactionActionInfo(DatastoreAction datastoreAction)
		//	: this(datastoreAction, null)
		//{
		//}

		//public SystemTransactionActionInfo(TransactionActionLog transactionActionLog)
		//	: this(null, transactionActionLog)
		//{

		//}

		//public SystemTransactionActionInfo(SimpleObjectManager objectManager, DatastoreActionInfo datastoreAction, TransactionActionInfo transactionActionLog)
		//{
		//	this.ObjectManager = objectManager;
		//	this.DatastoreAction = datastoreAction;
		//	this.TransactionActionLog = transactionActionLog;
		//}

		public SimpleObjectManager ObjectManager { get; private set; }
		public DatastoreActionInfo? DatastoreAction { get; set; }
		public TransactionActionInfo? TransactionActionLog { get; set; }
		public TransactionStatus TransactionStatus { get; set; }
	}
}
