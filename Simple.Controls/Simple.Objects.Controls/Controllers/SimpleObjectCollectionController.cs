using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using Simple;
using Simple.Collections;
using Simple.Modeling;
using Simple.Controls;
using Simple.Objects;

namespace Simple.Objects.Controls
{
    public abstract class SimpleObjectCollectionController : GenericGraphControllerBase<SimpleObject>
    {
        private int oneToManyRelationKey = 0;
		private SimpleObjectManager? objectManager = null;
        
        public SimpleObjectCollectionController()
        {
			//this.InitializeObjectManager();
			this.EditorBindings.ChangableBindingObjectControl = this;
        }

        public SimpleObjectCollectionController(IContainer container)
            : base(container)
        {
			//this.InitializeObjectManager();
        }

        [Category("General"), Browsable(true)]
        public int OneToManyRelationKey
        {
            get { return this.oneToManyRelationKey; }
            set { this.oneToManyRelationKey = value; }
        }

		[Category("General"), Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SimpleObjectManager? ObjectManager
		{
			get { return this.objectManager; }

			set
			{
				if (this.objectManager != null)
				{
					this.objectManager.NewObjectCreated -= objectManager_NewClientObjectCreated;
					this.objectManager.BeforeDelete -= objectManager_BeforeDeleting;
					this.objectManager.PropertyValueChange -= objectManager_PropertyValueChange;
					this.objectManager.RelationForeignObjectSet -= objectManager_RelationForeignObjectSet;
					this.objectManager.OrderIndexChange -= objectManager_OrderIndexChange;
					this.objectManager.ImageNameChange -= objectManager_ImageNameChange;
					this.objectManager.MultipleImageNameChange -= objectManager_MultipleImageNameChange;
					this.objectManager.UpdateStarted -= objectManager_UpdateStarted;
					this.objectManager.UpdateEnded -= objectManager_UpdateEnded;
					this.objectManager.ValidationInfo -= this.ObjectManager_ValidationInfo;
					this.objectManager.RequireSavingChange -= ObjectManager_RequireSavingChange;
					this.objectManager.RequireCommitChange -= this.DefaultChangeContainer_RequireCommitChange;
				}

				this.objectManager = value;

				if (this.objectManager != null)
				{
					this.objectManager.NewObjectCreated += objectManager_NewClientObjectCreated;
					this.objectManager.BeforeDelete += objectManager_BeforeDeleting;
					this.objectManager.PropertyValueChange += objectManager_PropertyValueChange;
					this.objectManager.RelationForeignObjectSet += objectManager_RelationForeignObjectSet;
					this.objectManager.OrderIndexChange += objectManager_OrderIndexChange;
					this.objectManager.ImageNameChange += objectManager_ImageNameChange;
					this.objectManager.MultipleImageNameChange += objectManager_MultipleImageNameChange;
					this.objectManager.UpdateStarted += objectManager_UpdateStarted;
					this.objectManager.UpdateEnded += objectManager_UpdateEnded;
					this.objectManager.ValidationInfo += this.ObjectManager_ValidationInfo;
					this.objectManager.RequireSavingChange += ObjectManager_RequireSavingChange;
					this.objectManager.RequireCommitChange += this.DefaultChangeContainer_RequireCommitChange;

					this.EditorBindings.ObjectManager = this.objectManager;
				}
			}
		}

		protected override SimpleObject GetParentGraphElement(SimpleObject graphElement)
        {
            return null;
        }

        protected override bool DoesGraphElementRequireSaving(SimpleObject graphElement)
        {
            if (graphElement == null)
                return false;

            return graphElement.IsChanged;
        }

        protected override object? GetBindingObject(SimpleObject? graphElement)
        {
            return graphElement;
        }

        protected override bool CanAddGraphElement(Type objectType, SimpleObject parentGraphElement)
        {
            return true;
        }

        protected override SimpleObject? CreateNewGraphElement(Type objectType, SimpleObject? parentGraphElement, object? requester)
        {
			return this.ObjectManager?.CreateNewEmptyObject(objectType, isNew: true, objectId: 0, ObjectActionContext.Client, requester);
		}

		protected override bool DeleteGraphElement(SimpleObject graphElement)
        {
			bool deleteNode = false;
			
			graphElement.RequestDelete();

			if (this.CommitChangeOnDeleteRequest)
				deleteNode = graphElement.Manager.CommitChanges().TransactionSucceeded;

			if (!deleteNode)
			{
				string? imageName = graphElement.GetImageName();
				string newImageName = ImageControl.ImageNameAddOption(imageName, ImageControl.ImageOptionRemoveExt, insertionPosition: 1);

				this.SetGraphElementImage(graphElement, newImageName);
			}

			return deleteNode; // If GraphElement is not deleted from database, thus shouild be exists in a graph control
		}

        protected override bool SaveGraphElement(SimpleObject graphElement)
        {
			if (graphElement.RequireSaving()) // this.ObjectManager.DefaultChangeContainer.Contains(graphElement))
				return graphElement.Manager.CommitChanges().TransactionSucceeded;

			return true;
		}

        protected override ValidationResult ValidateGraphElement(SimpleObject graphElement)
        {
            return graphElement.ValidateSave();
        }

        protected override bool CanDeleteGraphElement(SimpleObject graphElement)
        {
            return true;
        }

        protected override bool CanGraphElementChangeParent(SimpleObject graphElement, SimpleObject? newParentGraphElement)
        {
            return false;
        }

        protected override IEnumerable<SimpleObject> GetChildGraphElements(SimpleObject? graphElement)
        {
            if (graphElement == null && this.GetCollection() is IEnumerable<SimpleObject> collection)
				return collection; 
            else
                return new SimpleObjectCollection<SimpleObject>(this.ObjectManager!, GraphElementModel.TableId, new List<long>());
        }

        protected override object? GetGraphElementPropertyValue(SimpleObject graphElement, int propertyIndex)
        {
            return graphElement.GetPropertyValue(propertyIndex);
        }

        protected override void SetGraphElementPropertyValue(SimpleObject graphElement, int propertyIndex, object? value, object? requester)
        {
            graphElement.SetPropertyValue(propertyIndex, value, requester);
        }

        protected override void SetParentGraphElement(SimpleObject graphElement, SimpleObject? parentGraphElement)
        {
            throw new NotImplementedException();
        }

		protected override void SetOrderIndex(SimpleObject? graphElement, int orderIndex)
		{
			if (graphElement is SortableSimpleObject sortableSimpleObject)
				sortableSimpleObject.OrderIndex = orderIndex;
		}

		protected override string GetGraphElementName(SimpleObject graphElement)
        {
            return graphElement.ToString() ?? String.Empty;
        }

        protected override string GetGraphElementImageName(SimpleObject graphElement)
        {
            return graphElement.GetImageName() ?? String.Empty;
        }

        protected override IPropertyModel? GetBindingObjectPropertyModel(Type bindingObjectType, int propertyIndex)
        {
            return this.ObjectManager?.GetObjectModel(bindingObjectType)?.PropertyModels[propertyIndex];
        }

        protected override IPropertyModel? GetBindingObjectPropertyModel(Type bindingObjectType, string propertyName)
        {
            return this.ObjectManager?.GetObjectModel(bindingObjectType)?.PropertyModels[propertyName];
        }

        protected override bool ControlTryAddNewObjectByButtonClick(Component button, object requester)
        {
            return this.ControlTryAddNewObjectByButtonClick(button, null, requester);
        }

        protected override bool ControlTryAddNewObjectByButtonClick(Component button, SimpleObject parentGraphElement, object requester)
        {
            bool success = base.ControlTryAddNewObjectByButtonClick(button, parentGraphElement, requester);

            if (success && this.FocusedGraphElement is not null && this.ChangableBindingObjectControlFocusedBindingObject != null)
                this.FocusedGraphElement.SetOneToManyPrimaryObject(this.ChangableBindingObjectControlFocusedBindingObject, this.OneToManyRelationKey, requester: this);

            return success;
        }

		protected override void OnSetChangableBindingObjectControl() => this.EditorBindings.ChangableBindingObjectControl = this;

		protected SimpleObjectCollection? GetCollection()
        {
            if (this.OneToManyRelationKey > 0 && this.ChangableBindingObjectControlFocusedBindingObject != null)
                return this.ChangableBindingObjectControlFocusedBindingObject.GetOneToManyForeignObjectCollection(this.OneToManyRelationKey);
            else
                return null;
        }

		//private void InitializeObjectManager()
		//{
		//	SimpleObjectManager manager = null;

		//	try
		//	{
		//		manager = SimpleObjectManager.Instance;
		//	}
		//	catch
		//	{
		//		manager = null;
		//	}
		//	finally
		//	{
		//		this.ObjectManager = manager;
		//	}
		//}

		private void objectManager_NewClientObjectCreated(object sender, SimpleObjectChangeContainerContextRequesterEventArgs e)
		{
			if (this.IsLoadingInProgress || !this.IsBinded || this.IsDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
				form.Invoke(new MethodInvoker(() => this.OnNewObjectCreated(e)));
			else
				this.OnNewObjectCreated(e);
		}

		protected virtual void OnNewObjectCreated(SimpleObjectChangeContainerContextRequesterEventArgs e)
		{
			if (e.Requester == this)
			{
				//this.AddGraphElement(e.SimpleObject, parentGraphElement: null);
			}

			//if (e.SimpleObject == this.ChangableBindingObjectControlFocusedBindingObject) 
			//{
			//	if (e.SimpleObject.GetOneToManyRelationForeignObject<SimpleObject>(this.OneToManyRelationKey) == this.ChangableBindingObjectControlFocusedBindingObject &&
			//		!this.ContainsGraphElement(e.SimpleObject))
			//	{ 
			//		//this.BeginUpdate();
			//		this.AddGraphElement(e.SimpleObject, parentGraphElement: null);
			//		//this.EndUpdate ();
			//	}
			//}
		}

		private void objectManager_BeforeDeleting(object sender, SimpleObjectChangeContainerContextRequesterEventArgs e)
		{
			if (this.IsLoadingInProgress || !this.IsBinded || this.IsDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
				form.Invoke(new MethodInvoker(() => this.OnBeforeDeleting(e)));
			else
				this.OnBeforeDeleting(e);
		}

		protected virtual void OnBeforeDeleting(SimpleObjectChangeContainerContextRequesterEventArgs e)
		{
			this.RemoveGraphElement(e.SimpleObject as SimpleObject);
		}

		private void objectManager_RelationForeignObjectSet(object sender, RelationForeignObjectSetChangeContainerContextRequesterEventArgs e)
		{
			if (this.IsLoadingInProgress || !this.IsBinded || this.IsDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
				form.Invoke(new MethodInvoker(() => this.OnRelationForeignObjectSet(e)));
			else
				this.OnRelationForeignObjectSet(e);
		}

		protected virtual void OnRelationForeignObjectSet(RelationForeignObjectSetChangeContainerContextRequesterEventArgs e)
		{
			if (e.RelationModel.RelationKey == this.OneToManyRelationKey && e.ForeignSimpleObject == this.ChangableBindingObjectControlFocusedBindingObject && 
				e.SimpleObject.GetOneToManyPrimaryObject(this.OneToManyRelationKey) == this.ChangableBindingObjectControlFocusedBindingObject && !this.ContainsGraphElement(e.SimpleObject))
			{
				this.BeginUpdate();
				this.AddGraphElement(e.SimpleObject, parentGraphElement: null);
				this.EndUpdate ();
			}

			// TODO: Check if this replace with if (this.FocusedGraphElement == e.SimpleObject)
			if (this.ContainsGraphElement(e.SimpleObject))
				this.RaiseBindingObjectRelationForeignObjectSet(new BindingObjectRelationForeignObjectSetEventArgs(e));
		}

		private void objectManager_PropertyValueChange(object sender, ChangePropertyValuePertyModelSimpleObjectChangeContainerContextRequesterEventArgs e)
		{
			if (this.IsLoadingInProgress || !this.IsBinded || this.IsDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
				form.Invoke(new MethodInvoker(() => this.OnPropertyValueChange(e)));
			else
				this.OnPropertyValueChange(e);
		}
	
		protected virtual void OnPropertyValueChange(ChangePropertyValuePertyModelSimpleObjectChangeContainerContextRequesterEventArgs e)
		{
			if (this.ContainsGraphElement(e.SimpleObject))
			{
				if (e.Requester != this)
				{
					object? node = this.GetNode(e.SimpleObject);
					
					if (node is not null)
						this.UpdateNodeInternal(e.SimpleObject, node);
				}
				
				this.RaiseBindingObjectPropertyValueChange(e.SimpleObject, e.PropertyModel, e.OldPropertyValue, e.PropertyValue, e.IsChanged, e.ChangeContainer, e.Requester);
			}
		}

		private void objectManager_OrderIndexChange(object sender, ChangeSortedSimpleObjectRequesterEventArgs e)
		{
			if (this.IsLoadingInProgress || !this.IsBinded || this.IsDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
				form.Invoke(new MethodInvoker(() => this.OnOrderIndexChange(e)));
			else
				this.OnOrderIndexChange(e);
		}

		protected virtual void OnOrderIndexChange(ChangeSortedSimpleObjectRequesterEventArgs e)
		{
			object? node = this.GetNode(e.SortableSimpleObject);

			if (node != null) //this.nodesByGraphElement.ContainsKey(graphElement))
			{
				this.GraphControl?.SetOrderIndex(node, e.OrderIndex);

				if (this.FocusedGraphElement != null)
				{
					this.SetButtonsMoveUpEnableProperty(this.FocusedGraphElement);
					this.SetButtonsMoveDownEnableProperty(this.FocusedGraphElement);
				}
			}
		}

		private void objectManager_ImageNameChange(object sender, ImageNameChangeSimpleObjectEventArgs e)
		{
			if (this.IsLoadingInProgress || !this.IsBinded || this.IsDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
				form.Invoke(new MethodInvoker(() => this.OnObjectManager_ImageNameChange(e)));
			else
				this.OnObjectManager_ImageNameChange(e);
		}

		private void OnObjectManager_ImageNameChange(ImageNameChangeSimpleObjectEventArgs e)
		{
			this.SetGraphElementImage(e.SimpleObject, e.ImageName);
		}

		private void objectManager_MultipleImageNameChange(object sender, List<ImageNameChangeSimpleObjectEventArgs> e)
		{
			if (this.IsLoadingInProgress || !this.IsBinded || this.IsDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
				form.Invoke(new MethodInvoker(() => this.OnObjectManager_MultipleImageNameChange(e)));
			else
				this.OnObjectManager_MultipleImageNameChange(e);
		}

		private void OnObjectManager_MultipleImageNameChange(List<ImageNameChangeSimpleObjectEventArgs> e)
		{
			foreach (var item in e)
				this.SetGraphElementImage(item.SimpleObject, item.ImageName);
		}

		private void objectManager_UpdateStarted(object? sender, EventArgs e)
		{
			if (this.IsLoadingInProgress || !this.IsBinded || this.IsDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
				form.Invoke(new MethodInvoker(() => this.OnObjectManager_UpdateStarted(e)));
			else
				this.OnObjectManager_UpdateStarted(e);
		}

		private void OnObjectManager_UpdateStarted(EventArgs e)
		{
			this.BeginUpdate();
		}

		private void objectManager_UpdateEnded(object? sender, EventArgs e)
		{
			if (this.IsLoadingInProgress || !this.IsBinded || this.IsDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
				form.Invoke(new MethodInvoker(() => this.OnObjectManager_UpdateEnded(e)));
			else
				this.OnObjectManager_UpdateEnded(e);
		}

		private void OnObjectManager_UpdateEnded(EventArgs e)
		{
			this.EndUpdate();
		}

		private void ObjectManager_ValidationInfo(object sender, ValidationInfoEventArgs e)
		{
			if (this.IsLoadingInProgress || !this.IsBinded || this.IsDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
				form.Invoke(new MethodInvoker(() => this.OnObjectManager_ValidationInfo(e)));
			else
				this.OnObjectManager_ValidationInfo(e);
		}

		private void OnObjectManager_ValidationInfo(ValidationInfoEventArgs e)
		{
			if (this.IsLoadingInProgress || !this.IsBinded || this.IsDisposed || e.ValidationResult.Target != this.FocusedGraphElement )
				return;

			object? node = this.GetNode(e.ValidationResult.Target);

			if (node == null)
				return;
			
			GraphValidationResult graphValidationResult = this.ValidateNode(e.ValidationResult);

			this.SetValidation(graphValidationResult);
		}

		private void ObjectManager_RequireSavingChange(object sender, RequireSavingSimpleObjectEventArgs e)
		{
			if (this.IsLoadingInProgress || !this.IsBinded || this.IsDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
				form.Invoke(new MethodInvoker(() => this.OnObjectManager_RequireSavingChange(e)));
			else
				this.OnObjectManager_RequireSavingChange(e);
		}


		private void OnObjectManager_RequireSavingChange(RequireSavingSimpleObjectEventArgs e)
		{
			bool enabled = false;
			
			if (this.SaveButonOption == SaveButtonOption.SaveFocusedNode && this.FocusedGraphElement != null && e.SimpleObject == this.FocusedGraphElement)
				enabled = e.RequireSaving;
			
			this.SetButtonsSaveProperty(enabled);
		}


		private void DefaultChangeContainer_RequireCommitChange(object sender, RequireCommiChangeContainertEventArgs e)
		{
			if (this.IsLoadingInProgress || !this.IsBinded || this.IsDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
				form.Invoke(new MethodInvoker(() => this.OnDefaultChangeContainer_RequireCommitChange(e)));
			else
				this.OnDefaultChangeContainer_RequireCommitChange(e);
		}

		private void OnDefaultChangeContainer_RequireCommitChange(RequireCommiChangeContainertEventArgs e)
		{
			if (this.SaveButonOption == SaveButtonOption.CommitChanges)
				this.SetButtonsSaveProperty(e.RequireCommit);

			if (!e.RequireCommit)
			{
				if (this.GraphControl is not null)
					this.GraphControl.ResetColumnErrors();
				
				this.isGraphErrorMessageSet = false;
			}
		}

		private GraphValidationResult ValidateNode(SimpleObjectValidationResult validationResult)
		{
			GraphColumn? errorColumn = null;
			int errorColumnIndex = -1;

			if (!validationResult.Passed)
			{
				if (validationResult.FailedRuleResult != null && validationResult.FailedRuleResult is PropertyValidationResult propertyValidationResult && propertyValidationResult.ErrorPropertyModel is not null && validationResult.Target is not null)
				{
					IPropertyModel errorPropertyModel = propertyValidationResult.ErrorPropertyModel;
					GraphColumnBindingPolicy<SimpleObject>? columnBindingPolicy = this.GetGraphColumnBindingPolicyByObjectPropertyIndex(validationResult.Target.GetType(), errorPropertyModel.PropertyIndex);

					if (columnBindingPolicy != null)
						errorColumn = this.Columns[columnBindingPolicy.GraphColumn.Index];
				}
			}

			if (errorColumn != null)
				errorColumnIndex = errorColumn.Index;

			object? node = (validationResult.Target is not null) ? this.GetNode(validationResult.Target) 
																 : null;

			return new GraphValidationResult(validationResult, node, errorColumnIndex);
		}
	}
}
