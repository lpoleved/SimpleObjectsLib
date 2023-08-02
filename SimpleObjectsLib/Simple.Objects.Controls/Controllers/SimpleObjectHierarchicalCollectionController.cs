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
    public abstract class SimpleObjectHierarchicalCollectionController : GenericGraphControllerBase<SimpleObject>
    {
        private int oneToManyRelationKey = 0;
		private SimpleObjectManager? objectManager = null;
        
        public SimpleObjectHierarchicalCollectionController()
        {
			//this.InitializeObjectManager();
        }

        public SimpleObjectHierarchicalCollectionController(IContainer container)
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
					this.objectManager.NewObjectCreated -= objectManager_NewObjectCreated;
					this.objectManager.BeforeDelete -= objectManager_BeforeDeleting;
					this.objectManager.PropertyValueChange -= objectManager_PropertyValueChange;
					this.objectManager.RelationForeignObjectSet -= objectManager_RelationForeignObjectSet;
					this.objectManager.OrderIndexChange -= objectManager_OrderIndexChange;
					this.objectManager.ImageNameChange -= objectManager_ImageNameChange;
					this.objectManager.MultipleImageNameChange -= objectManager_MultipleImageNameChange;
					this.objectManager.UpdateStarted -= objectManager_LargeUpdateStarted;
					this.objectManager.UpdateEnded -= objectManager_LargeUpdateEnded;
				}

				this.objectManager = value;

				if (this.objectManager != null)
				{
					this.objectManager.NewObjectCreated += objectManager_NewObjectCreated;
					this.objectManager.BeforeDelete += objectManager_BeforeDeleting;
					this.objectManager.PropertyValueChange += objectManager_PropertyValueChange;
					this.objectManager.RelationForeignObjectSet += objectManager_RelationForeignObjectSet;
					this.objectManager.OrderIndexChange += objectManager_OrderIndexChange;
					this.objectManager.ImageNameChange += objectManager_ImageNameChange;
					this.objectManager.MultipleImageNameChange += objectManager_MultipleImageNameChange;
					this.objectManager.UpdateStarted += objectManager_LargeUpdateStarted;
					this.objectManager.UpdateEnded += objectManager_LargeUpdateEnded;
				}
			}
		}

		protected override SimpleObject? GetParentGraphElement(SimpleObject graphElement)
        {
            return null;
        }

        protected override bool DoesGraphElementRequireSaving(SimpleObject graphElement)
        {
            if (graphElement == null)
                return false;

            return graphElement.IsChanged;
        }

        protected override object GetBindingObject(SimpleObject graphElement)
        {
            return graphElement;
        }

        protected override bool CanAddGraphElement(Type objectType, SimpleObject parentGraphElement)
        {
            return true;
        }

        protected override SimpleObject? CreateNewGraphElement(Type objectType, SimpleObject? parentGraphElement, object? requester)
        {
			if (this.ObjectManager is not null)
				return this.ObjectManager.CreateNewEmptyObject(objectType, isNew: true, objectId: 0, requester);

			return default;
		}

		protected override bool DeleteGraphElement(SimpleObject graphElement)
        {
			bool deleteNode = false;

			graphElement.RequestDelete();

			if (this.CommitChangeOnDeleteRequest)
				deleteNode = graphElement.Manager.CommitChanges().TransactionSucceeded;

			if (!deleteNode)
			{
				string imageName = graphElement.GetImageName();
				string newImageName = ImageControl.ImageNameAddOption(imageName, ImageControl.ImageOptionRemoveExt, insertionPosition: 1);

				this.SetGraphElementImage(graphElement, newImageName);
			}

			return deleteNode; // If GraphElement is not deleted from database, thus shouild be exists in a graph control
		}

		protected override bool SaveGraphElement(SimpleObject graphElement)
        {
			return graphElement.Manager.CommitChanges().TransactionSucceeded;
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

        protected override IEnumerable<SimpleObject> GetChildGraphElements(SimpleObject graphElement)
        {
            if (graphElement == null && this.GetCollection() is IEnumerable<SimpleObject> collection)
				return collection;

            return new List<SimpleObject>();
        }

        protected override object GetGraphElementPropertyValue(SimpleObject graphElement, int propertyIndex)
        {
            return graphElement.GetPropertyValue(propertyIndex);
        }

        protected override void SetGraphElementPropertyValue(SimpleObject graphElement, int propertyIndex, object value, object? requester)
        {
            graphElement.SetPropertyValue(propertyIndex, value, requester);
        }

        protected override void SetParentGraphElement(SimpleObject graphElement, SimpleObject parentGraphElement)
        {
            throw new NotImplementedException();
        }

        protected override string GetGraphElementName(SimpleObject graphElement)
        {
            return graphElement.ToString();
        }

        protected override string GetGraphElementImageName(SimpleObject graphElement)
        {
            return graphElement.GetImageName();
        }

        protected override IPropertyModel? GetBindingObjectPropertyModel(Type bindingObjectType, int propertyIndex)
        {
            if (this.ObjectManager is not null)
				return this.ObjectManager.GetObjectModel(bindingObjectType).PropertyModels[propertyIndex];

			return default;
        }

        protected override IPropertyModel? GetBindingObjectPropertyModel(Type bindingObjectType, string propertyName)
        {
			if (this.ObjectManager is not null)
				return this.ObjectManager.GetObjectModel(bindingObjectType).PropertyModels[propertyName];

			return default;
        }

        protected override bool ControlTryAddNewObjectByButtonClick(Component button, object requester)
        {
            return this.ControlTryAddNewObjectByButtonClick(button, null, requester);
        }

        protected override bool ControlTryAddNewObjectByButtonClick(Component button, SimpleObject parentGraphElement, object requester)
        {
            bool success = base.ControlTryAddNewObjectByButtonClick(button, parentGraphElement, requester);

            if (success && this.FocusedGraphElement is not null)
                this.FocusedGraphElement.SetOneToManyPrimaryObject(this.ChangableBindingObjectControlFocusedBindingObject as SimpleObject, this.OneToManyRelationKey, requester: this);

            return success;
        }

        protected SimpleObjectCollection? GetCollection()
        {
            if (this.OneToManyRelationKey > 0 && this.ChangableBindingObjectControlFocusedBindingObject is not null)
            {
                return this.ChangableBindingObjectControlFocusedBindingObject.GetOneToManyForeignObjectCollection(this.OneToManyRelationKey);
            }
            else
            {
                return null;
            }
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

		private void objectManager_NewObjectCreated(object sender, SimpleObjectRequesterEventArgs e)
		{
			if (this.IsLoadingInProgress || !this.IsBinded || this.IsDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
			{
				form.Invoke(new MethodInvoker(() => this.OnNewObjectCreated(e)));
			}
			else
			{
				this.OnNewObjectCreated(e);
			}
		}

		protected virtual void OnNewObjectCreated(SimpleObjectRequesterEventArgs e)
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

		private void objectManager_BeforeDeleting(object sender, SimpleObjectRequesterEventArgs e)
		{
			if (this.IsLoadingInProgress || !this.IsBinded || this.IsDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
			{
				form.Invoke(new MethodInvoker(() => this.OnBeforeDeleting(e)));
			}
			else
			{
				this.OnBeforeDeleting(e);
			}
		}

		protected virtual void OnBeforeDeleting(SimpleObjectRequesterEventArgs e)
		{
			this.RemoveGraphElement(e.SimpleObject as SimpleObject);
		}

		private void objectManager_RelationForeignObjectSet(object sender, RelationForeignObjectSetRequesterEventArgs e)
		{
			if (this.IsLoadingInProgress || !this.IsBinded || this.IsDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
			{
				form.Invoke(new MethodInvoker(() => this.OnRelationForeignObjectSet(e)));
			}
			else
			{
				this.OnRelationForeignObjectSet(e);
			}
		}

		protected virtual void OnRelationForeignObjectSet(RelationForeignObjectSetRequesterEventArgs e)
		{
			if (e.ForeignSimpleObject == this.ChangableBindingObjectControlFocusedBindingObject && 
                e.SimpleObject.GetOneToManyPrimaryObject(this.OneToManyRelationKey) == this.ChangableBindingObjectControlFocusedBindingObject &&
				!this.ContainsGraphElement(e.SimpleObject))
			{
				//this.BeginUpdate();
				this.AddGraphElement(e.SimpleObject, parentGraphElement: null);
				//this.EndUpdate ();
			}

			if (this.ContainsGraphElement(e.SimpleObject))
				this.RaiseBindingObjectRelationForeignObjectSet(new BindingObjectRelationForeignObjectSetRequesterEventArgs(e));
		}

		private void objectManager_PropertyValueChange(object sender, ChangePropertyValueSimpleObjectRequesterEventArgs e)
		{
			if (this.IsLoadingInProgress || !this.IsBinded || this.IsDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
			{
				form.Invoke(new MethodInvoker(() => this.OnPropertyValueChange(e)));
			}
			else
			{
				this.OnPropertyValueChange(e);
			}
		}
	
		protected virtual void OnPropertyValueChange(ChangePropertyValueSimpleObjectRequesterEventArgs e)
		{
			if (this.ContainsGraphElement(e.SimpleObject as SimpleObject))
				this.RaiseBindingObjectPropertyValueChange(e.SimpleObject as SimpleObject, e.PropertyModel, e.OldValue, e.Value, e.IsChanged, e.IsSaveable, e.Requester);
		}

		private void objectManager_OrderIndexChange(object sender, ChangeSortedSimpleObjectRequesterEventArgs e)
		{
			if (this.IsLoadingInProgress || !this.IsBinded || this.IsDisposed)
				return;

			Form? form = this.FindGraphControlForm();

			if (form != null && form.InvokeRequired)
			{
				form.Invoke(new MethodInvoker(() => this.OnOrderIndexChange(e)));
			}
			else
			{
				this.OnOrderIndexChange(e);
			}
		}

		protected virtual void OnOrderIndexChange(ChangeSortedSimpleObjectRequesterEventArgs e)
		{
			object? node = this.GetNode(e.SortableSimpleObject);

			if (node != null && this.GraphControl is not null) //this.nodesByGraphElement.ContainsKey(graphElement))
				this.GraphControl.SetOrderIndex(node, e.OrderIndex);

			if (this.FocusedGraphElement != null)
			{
				this.SetButtonsMoveUpEnableProperty(this.FocusedGraphElement);
				this.SetButtonsMoveDownEnableProperty(this.FocusedGraphElement);
			}
		}

		private void objectManager_ImageNameChange(object sender, ImageNameChangeSimpleObjectEventArgs e)
		{
		}

		private void objectManager_MultipleImageNameChange(object sender, List<ImageNameChangeSimpleObjectEventArgs> e)
		{
		}

		private void objectManager_LargeUpdateStarted(object? sender, EventArgs e)
		{
			this.BeginUpdate();
		}

		private void objectManager_LargeUpdateEnded(object? sender, EventArgs e)
		{
			this.EndUpdate();
		}
	}
}
