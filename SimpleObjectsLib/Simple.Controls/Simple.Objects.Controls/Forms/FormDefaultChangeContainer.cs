using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors.Repository;
using Simple;
using Simple.Collections;
using Simple.Modeling;
using Simple.Controls.TreeList;
using Simple.AppContext;
using Simple.Controls;

namespace Simple.Objects.Controls
{
    public partial class FormDefaultChangeContainer : XtraForm
	{
        //private bool initializing = true;
        private int columnDescriptionIndex, columnOrderIndex;
        private SimpleDictionary<SimpleObject, TreeListNode> nodesBySimpleObject = new SimpleDictionary<SimpleObject, TreeListNode>();
        //private RepositoryItemRichTextEdit repositoryItemRichTextEdit = new RepositoryItemRichTextEdit();
        private RepositoryItemSpinEdit repositoryItemSpinEditOrderIndex = new RepositoryItemSpinEdit();
        private readonly object lockObject = new object();

        public FormDefaultChangeContainer(SimpleObjectManager objectManager, IClientAppContext appContext)
		{
            //this.initializing = true;

            InitializeComponent();

            this.ObjectManager = objectManager;
            this.AppContext = appContext;

            this.imageList = ImageControl.SmallImageCollection;
            this.treeList.SelectImageList = this.imageList;

            this.LoadLocationAndSizeSettings();

            this.columnDescriptionIndex = SimpleTreeList.AddColumn(this.treeList, "Action TableId.Id: ObjectName / PropertyIndex PropertyName=Value (OldValue)");
            this.columnOrderIndex = SimpleTreeList.AddColumn(this.treeList, "Order");

            var colummnOrder = this.treeList.Columns[this.columnOrderIndex];
            colummnOrder.ColumnEdit = this.repositoryItemSpinEditOrderIndex;
            colummnOrder.Visible = true;
            colummnOrder.OptionsFilter.AllowFilter = false;
            colummnOrder.OptionsColumn.ShowInCustomizationForm = false;
            colummnOrder.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colummnOrder.SortOrder = SortOrder.Ascending;
            colummnOrder.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemSpinEditOrderIndex.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;

            this.ObjectManager.DefaultChangeContainer.AfterAdd += DefaultChangeContainer_AfterAdd;
            this.ObjectManager.DefaultChangeContainer.AfterRemove += DefaultChangeContainer_AfterRemove;
            this.ObjectManager.DefaultChangeContainer.AfterSet += DefaultChangeContainer_AfterSet;
            this.ObjectManager.DefaultChangeContainer.AfterClear += DefaultChangeContainer_AfterClear;
            this.ObjectManager.PropertyValueChange += ObjectManager_PropertyValueChange;
            this.ObjectManager.RelationForeignObjectSet += ObjectManager_RelationForeignObjectSet;
            this.ObjectManager.ImageNameChange += this.ObjectManager_ImageNameChange;
            this.ObjectManager.MultipleImageNameChange += this.ObjectManager_MultipleImageNameChange;

            this.LoadData();

            //this.initializing = false;
        }

        public SimpleObjectManager ObjectManager { get; private set; }
        public IClientAppContext AppContext { get; private set; }

        private void LoadData()
        {
            foreach (var item in this.ObjectManager.DefaultChangeContainer.TransactionRequests.ToArray())
                this.AddSimpleObjectNode(item.Key, item.Value);

            this.treeList.Columns[this.columnOrderIndex].BestFit();
        }

        private void LoadLocationAndSizeSettings()
        {
            try
            {
                // Divide the screen in half, and find the center of the form to center it
                int defaultX = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
                int defaultY = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;

                // Set location.
                int x = this.AppContext.UserSettings.GetValue<int>(UserSettings.SettingFormDefaultChangeContainerWindowLocationX, defaultX);
                int y = this.AppContext.UserSettings.GetValue<int>(UserSettings.SettingFormDefaultChangeContainerWindowLocationY, defaultY);

                this.Location = new Point(x, y);

                // Set size.
                int width = this.AppContext.UserSettings.GetValue<int>(UserSettings.SettingFormDefaultChangeContainerWindowWidth, this.Size.Width);
                int height = this.AppContext.UserSettings.GetValue<int>(UserSettings.SettingFormDefaultChangeContainerWindowHeight, this.Size.Height);

                this.Size = new Size(width, height);

                // Set state.
                this.WindowState = Conversion.TryChangeType<FormWindowState>(this.AppContext.UserSettings.GetValue<int>(UserSettings.SettingFormDefaultChangeContainerWindowState, ((int)FormWindowState.Normal)));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, this.AppContext.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void FormDefaultChangeContainer_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    this.AppContext.UserSettings.FormDefaultChangeContainerWindowLocationX = this.Location.X;
                    this.AppContext.UserSettings.FormDefaultChangeContainerWindowLocationY = this.Location.Y;
                    this.AppContext.UserSettings.FormDefaultChangeContainerWindowWidth = this.Size.Width;
                    this.AppContext.UserSettings.FormDefaultChangeContainerWindowHeight = this.Size.Height;
                }

                this.AppContext.UserSettings.FormDefaultChangeContainerWindowState = (int)this.WindowState;
                this.AppContext.UserSettings.Save();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, this.AppContext.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private TreeListNode GetNode(SimpleObject simpleObject)
        {
            return (simpleObject != null) ? this.nodesBySimpleObject[simpleObject] : null;
        }

        private void DefaultChangeContainer_AfterAdd(object sender, RequestActionSimpleObjectRequesterEventArgs e)
        {
            Form form = this.FindForm();

            if (form != null && form.InvokeRequired)
            {
                form.Invoke(new MethodInvoker(() => this.OnDefaultChangeContainer_AfterAdd(e)));
            }
            else
            {
                this.OnDefaultChangeContainer_AfterAdd(e);
            }
        }

        private void OnDefaultChangeContainer_AfterAdd(RequestActionSimpleObjectRequesterEventArgs e)
        {
            this.AddSimpleObjectNode(e.SimpleObject, e.RequestAction);
            this.treeList.Columns[1].BestFit();
        }

        private void DefaultChangeContainer_AfterSet(object sender, RequestActionSimpleObjectRequesterEventArgs e)
        {
            Form form = this.FindForm();

            if (form != null && form.InvokeRequired)
            {
                form.Invoke(new MethodInvoker(() => this.OnDefaultChangeContainer_AfterSet(e)));
            }
            else
            {
                this.OnDefaultChangeContainer_AfterSet(e);
            }
        }

        private void OnDefaultChangeContainer_AfterSet(RequestActionSimpleObjectRequesterEventArgs e)
        {
            TreeListNode node = this.GetNode(e.SimpleObject);
            
            this.RefreshSimpleObjectNode(node, e.SimpleObject, e.RequestAction);
        }

        private void DefaultChangeContainer_AfterRemove(object sender, SimpleObjectRequesterEventArgs e)
        {
            Form form = this.FindForm();

            if (form != null && form.InvokeRequired)
            {
                form.Invoke(new MethodInvoker(() => this.OnDefaultChangeContainer_AfterRemove(e)));
            }
            else
            {
                this.OnDefaultChangeContainer_AfterRemove(e);
            }
        }

        private void OnDefaultChangeContainer_AfterRemove(SimpleObjectRequesterEventArgs e)
        {
            TreeListNode node = this.GetNode(e.SimpleObject);

            this.treeList.DeleteNode(node);
            this.nodesBySimpleObject.Remove(e.SimpleObject);
        }

        private void DefaultChangeContainer_AfterClear(object sender, OldCountEventArgs e)
        {
            Form form = this.FindForm();

            if (form != null && form.InvokeRequired)
            {
                form.Invoke(new MethodInvoker(() => this.OnDefaultChangeContainer_AfterClear(e)));
            }
            else
            {
                this.OnDefaultChangeContainer_AfterClear(e);
            }
        }

        private void OnDefaultChangeContainer_AfterClear(OldCountEventArgs e)
        {
            this.Clear();
        }

        private void ObjectManager_PropertyValueChange(object sender, ChangePropertyValueSimpleObjectRequesterEventArgs e)
        {
            Form form = this.FindForm();

            if (form != null && form.InvokeRequired)
            {
                form.Invoke(new MethodInvoker(() => this.OnObjectManager_PropertyValueChange(e)));
            }
            else
            {
                this.OnObjectManager_PropertyValueChange(e);
            }
        }

        private void OnObjectManager_PropertyValueChange(ChangePropertyValueSimpleObjectRequesterEventArgs e)
        {
            TreeListNode node;

            if (this.nodesBySimpleObject.TryGetValue(e.SimpleObject, out node))
            {
                this.RefreshSimpleObjectNode(node, e.SimpleObject, (TransactionRequestAction)node.Tag);
                
                TreeListNode propertyNode = this.FindPropertyNode(node, e.PropertyModel.PropertyIndex);

                if (propertyNode == null && e.IsChanged)
                {
                    this.AddPropertyNode(node, e.SimpleObject, e.PropertyModel, e.Value);
                }
                else if (e.IsChanged) // e.SimpleObject.GetChangedPropertyIndexes().Contains(e.PropertyModel.Index))
                {
                    this.RefreshPropertyNode(propertyNode, e.SimpleObject, e.PropertyModel, e.Value);

                    if (e.PropertyModel.IsRelationTableId) // and if RelationObjectId do exists within changed properties, refresh objectIdPropertyNode and related object
                    {
                        IRelationModel relationModel = e.SimpleObject.Manager.GetRelationModel(e.PropertyModel.RelationKey);

                        if (relationModel is IOneToOneOrManyRelationModel)
                        {
                            TreeListNode objectIdPropertyNode = this.FindPropertyNode(node, (relationModel as IOneToOneOrManyRelationModel).PrimaryObjectIdPropertyModel.PropertyIndex); // this Index should be e.PropertyModel.Index + 1);

                            if (objectIdPropertyNode != null)
                                this.RefreshPropertyNode(objectIdPropertyNode, e.SimpleObject, (relationModel as IOneToOneOrManyRelationModel).PrimaryObjectIdPropertyModel, e.Value);
                        }
                    }
                }
                else
                {
                    this.treeList.DeleteNode(propertyNode);
                }

                // Refresh GraphElements name description if SimpleObject name is changed

                if (!(e.SimpleObject is GraphElement) && e.SimpleObject.GetModel().NamePropertyModel != null && e.PropertyModel.PropertyIndex == e.SimpleObject.GetModel().NamePropertyModel.PropertyIndex)
                {
                    foreach (GraphElement graphElement in e.SimpleObject.GraphElements)
                    {
                        TreeListNode graphElemntNode;

                        if (this.nodesBySimpleObject.TryGetValue(graphElement, out graphElemntNode))
                            this.RefreshSimpleObjectNode(graphElemntNode, graphElement, (TransactionRequestAction)node.Tag);
                    }
                }
            }
        }

        private void ObjectManager_RelationForeignObjectSet(object sender, RelationForeignObjectSetRequesterEventArgs e)
        {
            Form form = this.FindForm();

            if (form != null && form.InvokeRequired)
            {
                form.Invoke(new MethodInvoker(() => this.OnObjectManager_RelationForeignObjectSet(e)));
            }
            else
            {
                this.OnObjectManager_RelationForeignObjectSet(e);
            }
        }

        private void OnObjectManager_RelationForeignObjectSet(RelationForeignObjectSetRequesterEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void ObjectManager_ImageNameChange(object sender, ImageNameChangeSimpleObjectEventArgs e)
        {
            Form form = this.FindForm();

            if (form != null && form.InvokeRequired)
            {
                form.Invoke(new MethodInvoker(() => this.OnObjectManager_ImageNameChange(e)));
            }
            else
            {
                this.OnObjectManager_ImageNameChange(e);
            }
        }

        private void OnObjectManager_ImageNameChange(ImageNameChangeSimpleObjectEventArgs e)
        {
            var node = this.GetNode(e.SimpleObject);

            if (node != null)
                this.SetNodeImage(node, e.SimpleObject, (TransactionRequestAction)node.Tag, e.ImageName);
        }


        private void ObjectManager_MultipleImageNameChange(object sender, List<ImageNameChangeSimpleObjectEventArgs> e)
        {
            Form form = this.FindForm();

            if (form != null && form.InvokeRequired)
            {
                form.Invoke(new MethodInvoker(() => this.OnObjectManager_MultipleImageNameChange(e)));
            }
            else
            {
                this.OnObjectManager_MultipleImageNameChange(e);
            }
        }

        private void OnObjectManager_MultipleImageNameChange(List<ImageNameChangeSimpleObjectEventArgs> e)
        {
            foreach (var item in e)
                this.OnObjectManager_ImageNameChange(item);
        }

        private void AddSimpleObjectNode(SimpleObject simpleObject, TransactionRequestAction requestAction)
        {
            lock (this.lockObject)
            {
                this.treeList.BeginUpdate();

                string imageName = simpleObject.GetImageName();
                int orderIndex = (this.treeList.Nodes.Count > 0) ? this.treeList.Nodes.Max(n => (int)(n.GetValue(this.columnOrderIndex) ?? 0) + 1) : 0;
                TreeListNode node = this.treeList.AppendNode(nodeData: null, parentNode: null);
                this.SetNodeOrderIndex(node, orderIndex);

                node.Tag = requestAction;
                //node.ImageIndex = this.imageList.Images.IndexOfKey(imageName);
                //node.SelectImageIndex = node.ImageIndex;
                ////node.StateImageIndex = node.SelectImageIndex;
                this.nodesBySimpleObject.Add(simpleObject, node);

                this.RefreshSimpleObjectNode(node, simpleObject, requestAction);

                foreach (int index in simpleObject.GetChangedPropertyIndexes())
                {
                    IPropertyModel propertyModel = simpleObject.GetModel().PropertyModels[index];

                    if (this.FindPropertyNode(node, index) == null)
                    {
                        object value = simpleObject.GetPropertyValue(propertyModel);
                        
                        this.AddPropertyNode(node, simpleObject, propertyModel, value);
                    }
                }

                this.treeList.EndUpdate();
            }
        }

        private void AddPropertyNode(TreeListNode parentNode, SimpleObject simpleObject, IPropertyModel propertyModel, object value)
        {
            lock (this.lockObject) // Avoid simultaneously adding same property by constructor initialization and ObjectManager_PropertyValueChange event
            {
                this.treeList.BeginUpdate();

                TreeListNode propertyNode = this.treeList.AppendNode(nodeData: null, parentNode);

                propertyNode.Tag = propertyModel.PropertyIndex;
                propertyNode.ImageIndex = this.imageList.Images.IndexOfKey("Policy");
                propertyNode.SelectImageIndex = propertyNode.ImageIndex;
                
                this.RefreshPropertyNode(propertyNode, simpleObject, propertyModel, value);
                this.SetNodeOrderIndex(propertyNode, propertyModel.PropertyIndex);

                this.treeList.EndUpdate();
            }
        }

        private void RefreshSimpleObjectNode(TreeListNode node, SimpleObject simpleObject, TransactionRequestAction requestAction, string additionalInfo = "")
        {
            String status = String.Empty; // requestAction.ToString();

            if (requestAction == TransactionRequestAction.Save)
            {
                if (simpleObject.IsNew)
                {
                    status = "Add";
                }
                else
                {
                    status = "Update";
                }
            }
            else // requestAction == TransactionRequestAction.Delete
            {
                status = "Delete";
            }

            this.treeList.BeginUpdate();
            
            node.SetValue(this.columnDescriptionIndex, String.Format("{0} {1}.{2}:  {3}{4}", status, simpleObject.GetModel().TableInfo.TableId, simpleObject.Id, simpleObject.GetName(), additionalInfo));
            this.SetNodeImage(node, simpleObject, requestAction);

            this.treeList.EndUpdate();
        }

        private void SetNodeImage(TreeListNode node, SimpleObject simpleObject, TransactionRequestAction requestAction, string? imageName = null)
        {
            imageName = imageName ?? simpleObject.GetImageName();
            string imageOption = String.Empty;
            int imageIndex = -1;

            if (requestAction == TransactionRequestAction.Save)
            {
                if (simpleObject.IsNew)
                {
                    imageOption = ImageControl.ImageOptionAddExt;
                }
                else
                {
                    //imageOption = ImageControl.ImageOptionEditExt;
                }
            }
            else // requestAction == TransactionRequestAction.Delete
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
                this.treeList.BeginUpdate();

                node.ImageIndex = imageIndex;
                node.SelectImageIndex = node.ImageIndex;

                this.treeList.EndUpdate();
            }
        }

        private void RefreshPropertyNode(TreeListNode propertyNode, SimpleObject simpleObject, IPropertyModel propertyModel, object value)
        {
            object propertyValue = value.ValueToString(); // simpleObject.GetPropertyValue(propertyModel.Index) ?? "null";
            string propertyName = propertyModel.PropertyName;
            object oldPropertyValue = simpleObject.GetOldPropertyValue(propertyModel.PropertyIndex).ValueToString();
            string relatedObjectText = String.Empty;

            if (propertyModel.IsRelationObjectId)
            {
                IRelationModel relationModel = simpleObject.Manager.GetRelationModel(propertyModel.RelationKey);

                if (relationModel is IOneToOneOrManyRelationModel)
                {
                    SimpleObject relatedSimpleObject = simpleObject.GetRelationPrimaryObject(relationModel as IOneToOneOrManyRelationModel);

                    if (relatedSimpleObject != null)
                    {
                        relatedObjectText += String.Format(" => {0} {1}.{2}", relatedSimpleObject.GetModel().ObjectType.Name, relatedSimpleObject.GetModel().TableInfo.TableId, relatedSimpleObject.Id);
                        //relatedObjectText += String.Format(" => {0} {1}.{2}:  {3}", relatedSimpleObject.GetModel().ObjectType.Name, relatedSimpleObject.GetModel().TableInfo.TableId, relatedSimpleObject.Id, relatedSimpleObject.GetName()); // <- require additional refresh handling when the object name is changing 
                    }
                    else
                    {
                        relatedObjectText += String.Format(" => {0} {1}", (relationModel as IOneToOneOrManyRelationModel).PrimaryObjectName, relatedSimpleObject.ValueToString()); // "null";
                    }
                }
            }

            if (!propertyModel.IsStorable)
                propertyName += "*";

            this.treeList.BeginUpdate();
            
            propertyNode.SetValue(this.columnDescriptionIndex, string.Format("{0} {1}={2} ({3}){4}", propertyModel.PropertyIndex, propertyName, propertyValue, oldPropertyValue, relatedObjectText));
            
            this.treeList.EndUpdate();
        }

        private void SetNodeOrderIndex(TreeListNode node, int orderIndex)
        {
            this.treeList.SetNodeIndex(node, orderIndex);
            node.SetValue(this.columnOrderIndex, orderIndex);
        }

        private TreeListNode FindPropertyNode(TreeListNode simpleObjectNode, int propertyIndex)
        {
            return simpleObjectNode.Nodes.FirstOrDefault(item => Conversion.TryChangeType<int>(item.Tag, -1) == propertyIndex);
        }

        private void treeList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeListHitInfo hitInfo = this.treeList.CalcHitInfo(e.Location);

                if (hitInfo.HitInfoType != HitInfoType.Column && hitInfo.HitInfoType != HitInfoType.BehindColumn)
                    this.popupMenu.ShowPopup(Cursor.Position);
            }
        }

        private void barButtonSort_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ObjectManager.DefaultChangeContainer.Sort();
            this.Clear();
            this.LoadData();
        }

        private void barButtonItemValidate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (var item in this.ObjectManager.DefaultChangeContainer.TransactionRequests)
            {
                SimpleObject simpleObject = item.Key;
                TransactionRequestAction requestAction = item.Value;
                TreeListNode node = this.GetNode(simpleObject);
                var validationResult = (requestAction == TransactionRequestAction.Save) ? simpleObject.ValidateSave() : simpleObject.ValidateDelete();
                string validationInfo = " - Validation ";

                if (validationResult.Passed)
                {
                    validationInfo += "Succeeded";
                }
                else
                {
                    validationInfo += "Error: " + validationResult.Message;

                    if (validationResult.FailedRuleResult is PropertyValidationResult propertyValidationResult && propertyValidationResult.ErrorPropertyModel != null)
                        validationInfo += " Property: " + propertyValidationResult.ErrorPropertyModel.PropertyName;
                }

                this.RefreshSimpleObjectNode(node, simpleObject, requestAction, validationInfo);
            }
        }

        private void barButtonCommitChanges_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ObjectManager.CommitChanges();
        }

        private void barButtonItemCancelDeleteRequests_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (var item in this.ObjectManager.DefaultChangeContainer.TransactionRequests)
                if (item.Value == TransactionRequestAction.Delete)
                    item.Key.CancelDeleteRequest();
        }

		private void FormDefaultChangeContainer_FormClosed(object sender, FormClosedEventArgs e)
		{
            this.ObjectManager.DefaultChangeContainer.AfterAdd -= DefaultChangeContainer_AfterAdd;
            this.ObjectManager.DefaultChangeContainer.AfterRemove -= DefaultChangeContainer_AfterRemove;
            this.ObjectManager.DefaultChangeContainer.AfterSet -= DefaultChangeContainer_AfterSet;
            this.ObjectManager.DefaultChangeContainer.AfterClear -= DefaultChangeContainer_AfterClear;
            this.ObjectManager.PropertyValueChange -= ObjectManager_PropertyValueChange;
            this.ObjectManager.RelationForeignObjectSet -= ObjectManager_RelationForeignObjectSet;
            this.ObjectManager.ImageNameChange -= this.ObjectManager_ImageNameChange;
            this.ObjectManager.MultipleImageNameChange -= this.ObjectManager_MultipleImageNameChange;
        }

        private void Clear()
        {
            this.treeList.ClearNodes();
            this.nodesBySimpleObject.Clear();
        }
    }
}
