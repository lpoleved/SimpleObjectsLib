using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraBars.Ribbon;
using Simple.Modeling;
using Simple.Objects;

namespace Simple.Objects.Controls
{
	public class UndoManager : Component
	{
		private BarButtonItem undoButton = null;
		private BarButtonItem redoButton = null;
		private RibbonControl ribbonControl = null;
		private RibbonPage selectedRibbonPage = null;
		private SimpleObjectManager objectManager = null;
		private List<UndoAction> history = new List<UndoAction>();
		private UndoPopup undoPopup = new UndoPopup();
		private PopupControlContainer undoPopupContainer = null;
		private PopupControlContainer redoPopupContainer = null;
		private UndoPopup redoPopup = new UndoPopup();
		private int position = -1;

		private delegate void UndoRedoCallback(int count);
		//private Timer timer = new Timer();
		//private Action doAction = null;
		//private int interval = 150; // in milliseconds

		public UndoManager()
		{
			//this.timer.Tick += this.Timer_Tick;
			//this.timer.Interval = this.interval;

			this.undoPopup.ListBox.SelectedIndexChanged += this.UndoListBox_SelectedIndexChanged;
			this.undoPopup.ListBox.Click += this.UndoListBox_Click;

			this.undoPopupContainer = new PopupControlContainer();
			this.undoPopupContainer.BeginInit();
			this.undoPopupContainer.SuspendLayout();
			//this.popupContainer.Manager.Form.Controls.Add(this.popupContainer);
			this.undoPopupContainer.Controls.Add(this.undoPopup);
			this.undoPopupContainer.Ribbon = this.RibbonControl;
			this.undoPopupContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.undoPopupContainer.Size = this.undoPopup.Size;
			this.undoPopupContainer.Visible = true;
			this.undoPopupContainer.Popup += this.UndoPopupContainer_Popup;
			this.undoPopupContainer.CloseUp += this.UndoPopupContainer_CloseUp;

			this.redoPopup.ListBox.SelectedIndexChanged += this.RedoListBox_SelectedIndexChanged;
			this.redoPopup.ListBox.Click += this.RedoListBox_Click;

			this.redoPopupContainer = new PopupControlContainer();
			this.redoPopupContainer.BeginInit();
			this.redoPopupContainer.SuspendLayout();
			//this.popupContainer.Manager.Form.Controls.Add(this.popupContainer);
			this.redoPopupContainer.Controls.Add(this.redoPopup);
			this.redoPopupContainer.Ribbon = this.RibbonControl;
			this.redoPopupContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.redoPopupContainer.Size = this.redoPopup.Size;
			this.redoPopupContainer.Visible = true;
			this.redoPopupContainer.Popup += this.RedoPopupContainer_Popup;
			this.redoPopupContainer.CloseUp += this.RedoPopupContainer_CloseUp;
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			//this.timer.Stop();
			//this.doAction();
		}

		//private delegate void Action();

		[Category("General"), Browsable(true)]
		public RibbonControl RibbonControl
		{
			get { return this.ribbonControl; }
			set
			{
				if (this.ribbonControl != null)
					this.ribbonControl.SelectedPageChanged -= this.RibbonControl_SelectedPageChanged;

				this.ribbonControl = value;

				if (this.ribbonControl != null)
					this.ribbonControl.SelectedPageChanged += this.RibbonControl_SelectedPageChanged;
			}
		}

		[Category("General"), Browsable(true)]
		public BarButtonItem UndoButton
		{
			get { return this.undoButton; }

			set
			{
				if (this.undoButton != null)
					this.undoButton.ItemClick -= this.UndoButton_ItemClick;

				this.undoButton = value;

				if (this.undoButton != null)
				{
					this.undoPopupContainer.Manager = this.undoButton.Manager;
					this.undoButton.ButtonStyle = BarButtonStyle.DropDown;
					this.undoButton.DropDownControl = this.undoPopupContainer;
					this.undoButton.ItemClick += this.UndoButton_ItemClick;
				}

				this.SetEnableProperty();
			}
		}

		[Category("General"), Browsable(true)]
		public BarButtonItem RedoButton
		{
			get { return this.redoButton; }

			set
			{
				if (this.redoButton != null)
					this.redoButton.ItemClick -= this.RedoButton_ItemClick;

				this.redoButton = value;

				if (this.redoButton != null)
				{
					this.redoPopupContainer.Manager = this.redoButton.Manager;
					this.redoButton.ButtonStyle = BarButtonStyle.DropDown;
					this.redoButton.DropDownControl = this.redoPopupContainer;
					this.redoButton.ItemClick += this.RedoButton_ItemClick;
				}

				this.SetEnableProperty();
			}
		}


		//[Category("General"), Browsable(true)]
		//public BarSubItem RedoButton
		//{
		//	get { return this.redoButton; }

		//	set
		//	{
		//		if (this.redoButton != null)
		//		{
		//			this.redoButton.GetItemData -= this.RedoButton_GetItemData;
		//			this.redoButton.ItemClick -= this.RedoButton_ItemClick2;
		//		}

		//		this.redoButton = value;

		//		if (this.redoButton != null)
		//		{
		//			this.redoButton.GetItemData += this.RedoButton_GetItemData;
		//			this.redoButton.ItemClick += this.RedoButton_ItemClick2;
		//		}

		//		this.SetEnableProperty();
		//	}
		//}

		[Category("General"), Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SimpleObjectManager ObjectManager
		{
			get { return this.objectManager; }

			set
			{
				if (this.objectManager != null)
				{
					this.objectManager.PropertyValueChange += this.ObjectManager_PropertyValueChange;
					this.objectManager.RelationForeignObjectSet += this.ObjectManager_RelationForeignObjectSet;
					this.objectManager.NewObjectCreated += this.ObjectManager_NewObjectCreated;
					this.objectManager.AfterDelete += this.ObjectManager_AfterDelete;
					this.objectManager.TransactionFinished += this.ObjectManager_TransactionFinished;
				}

				this.objectManager = value;

				if (this.objectManager != null)
				{
					this.objectManager.PropertyValueChange += this.ObjectManager_PropertyValueChange;
					this.objectManager.RelationForeignObjectSet += this.ObjectManager_RelationForeignObjectSet;
					this.objectManager.NewObjectCreated += this.ObjectManager_NewObjectCreated;
					this.objectManager.AfterDelete += this.ObjectManager_AfterDelete;
					this.objectManager.TransactionFinished += this.ObjectManager_TransactionFinished;
				}
			}
		}

		private void UndoListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.RefreshLabel(this.undoPopup.Label, "Undo", this.undoPopup.ListBox.SelectedItems.Count);
		}

		private void UndoListBox_Click(object sender, EventArgs e)
		{
			this.undoButton.DropDownControl.HidePopup();
		}

		private void UndoButton_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.Undo(1);
		}

		private void UndoPopupContainer_Popup(object sender, EventArgs e)
		{
			this.undoPopup.ResetIndexes();

			foreach (var item in this.GetUndoCommands)
				this.undoPopup.ListBox.Items.Add(item);

			this.undoPopup.ListBox.UnSelectAll();
			this.RefreshLabel(this.undoPopup.Label, "Undo", 0);
			this.undoPopup.BestFit();
			this.undoPopupContainer.Size = this.undoPopup.Size;
		}

		private void UndoPopupContainer_CloseUp(object sender, EventArgs e)
		{
			this.Undo(this.undoPopup.SelectedCount);
			this.undoPopup.ListBox.Items.Clear();
			this.SetEnableProperty();
		}

		private void RedoListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.RefreshLabel(this.redoPopup.Label, "Redo", this.redoPopup.ListBox.SelectedItems.Count);
		}

		private void RedoListBox_Click(object sender, EventArgs e)
		{
			this.redoButton.DropDownControl.HidePopup();
		}

		private void RedoButton_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.Redo(1);
		}

		private void RedoPopupContainer_Popup(object sender, EventArgs e)
		{
			this.redoPopup.ResetIndexes();

			foreach (var item in this.GetRedoCommands)
				this.redoPopup.ListBox.Items.Add(item);

			this.redoPopup.ListBox.UnSelectAll();
			this.RefreshLabel(this.redoPopup.Label, "Redo", 0);
			this.redoPopup.BestFit();
			this.redoPopupContainer.Size = this.redoPopup.Size;
		}

		private void RedoPopupContainer_CloseUp(object sender, EventArgs e)
		{
			this.Redo(this.redoPopup.SelectedCount);
			this.redoPopup.ListBox.Items.Clear();
			this.SetEnableProperty();
		}


		private void UndoButton_GetItemData(object sender, EventArgs e)
		{
			this.DrawDropDown(this.undoButton, "Undo", this.GetUndoCommands, this.Undo);
			//this.doAction = () => this.DrawDropDown(this.undoButton, "Undo", this.GetUndoCommands, this.Undo);
			//this.timer.Start();
		}

		private void UndoButton_ItemClick2(object sender, ItemClickEventArgs e)
		{
			//this.doAction = () => this.Undo(1);
			//this.timer.Start();
		}

		private void RedoButton_GetItemData(object sender, EventArgs e)
		{
			this.DrawDropDown(this.redoButton, "Redo", this.GetRedoCommands, this.Redo);
			//this.doAction = () => this.DrawDropDown(this.redoButton, "Redo", this.GetRedoCommands, this.Redo);
			//this.timer.Start();
		}

		private void RedoButton_ItemClick2(object sender, ItemClickEventArgs e)
		{
			//this.doAction = () => this.Redo(1);
			//this.timer.Start();
		}

		private void RibbonControl_SelectedPageChanged(object sender, EventArgs e)
		{
			this.selectedRibbonPage = this.RibbonControl.SelectedPage;
		}

		private void ObjectManager_PropertyValueChange(object sender, ChangePropertyValueSimpleObjectRequesterEventArgs e)
		{
			if (this.CanAndo(e.Requester))
			{
				IPropertyModel propertyModel = e.SimpleObject.GetModel().PropertyModels.GetPropertyModel(e.PropertyModel.PropertyIndex);

				if (propertyModel.IsKey || propertyModel.IsRelationTableId || propertyModel.IsRelationObjectId)
					return;

				this.AddHistory(new UndoPropertyChange(e) { SelectedRibbonPage = this.selectedRibbonPage });
			}
		}

		private void ObjectManager_RelationForeignObjectSet(object sender, RelationForeignObjectSetRequesterEventArgs e)
		{
			if (this.CanAndo(e.Requester))
				this.AddHistory(new UndoForeignObjectSet(e) { SelectedRibbonPage = this.selectedRibbonPage });
		}

		private void ObjectManager_NewObjectCreated(object sender, SimpleObjectRequesterEventArgs e)
		{
			if (this.CanAndo(e.Requester))
				this.AddHistory(new UndoNewObjectCreated(e.SimpleObject) { SelectedRibbonPage = this.selectedRibbonPage });
		}

		private void ObjectManager_AfterDelete(object sender, SimpleObjectRequesterEventArgs e)
		{
			if (this.CanAndo(e.Requester))
				this.AddHistory(new UndoObjectDeleted(e.SimpleObject) { SelectedRibbonPage = this.selectedRibbonPage });
		}

		private bool CanAndo(object? requester)
		{
			return !(requester is UndoAction || requester is ForeignClientRequester);
		}

		private void ObjectManager_TransactionFinished(object sender, TransactionRequesterEventArgs e)
		{
			// Remove object actions that transaction contains first.
			int i = 0;

			while (i < this.history.Count)
			{
				UndoAction undoAction = this.history[i];

				if (!(undoAction is UndoTransaction))
				{
					this.history.RemoveAt(i);

					if (i <= this.position)
						this.position--;
				}
				else
				{
					i++;
				}
			}

			if (e.Requester is UndoTransaction undoTransaction)
				undoTransaction.Transaction = e.Transaction;
			else
				this.AddHistory(new UndoTransaction(e.Transaction) { SelectedRibbonPage = this.selectedRibbonPage });
		}

		private void Undo(int count)
		{
			//Cursor currentCursor = Cursor.Current;
			//Cursor.Current = Cursors.WaitCursor;

			for (int i = this.position; i > this.position - count; i--)
			{
				UndoAction undoAction = this.history[i];

				if (this.RibbonControl != null && undoAction.SelectedRibbonPage != null)
					this.RibbonControl.SelectedPage = undoAction.SelectedRibbonPage;

				undoAction.Undo();
			}

			this.position -= count;
			this.SetEnableProperty();

			//Cursor.Current = currentCursor;
		}

		private void Redo(int count)
		{
			//Cursor currentCursor = Cursor.Current;
			//Cursor.Current = Cursors.WaitCursor;

			for (int i = this.position + 1; i <= this.position + count; i++)
			{
				UndoAction undoAction = this.history[i];

				if (this.RibbonControl != null && undoAction.SelectedRibbonPage != null)
					this.RibbonControl.SelectedPage = undoAction.SelectedRibbonPage;

				undoAction.Redo();
			}

			this.position += count;
			this.SetEnableProperty();

			//Cursor.Current = currentCursor;
		}

		private IEnumerable<string> GetUndoCommands
		{
			// Provides a list of strings for testing the drop down
			get
			{
				for (int i = this.position; i >= 0; i--)
					yield return this.history[i].GetText(UndoAction.UndoActionType.Undo);
			}
		}

		private IEnumerable<string> GetRedoCommands
		{
			// Provides a list of strings for testing the drop down
			get
			{
				for (int i = this.position + 1; i < this.history.Count; i++)
					yield return this.history[i].GetText(UndoAction.UndoActionType.Redo);
			}
		}

		private void DrawDropDown(BarItem button, string action, IEnumerable<string> commands, UndoRedoCallback callback) // ToolStripItem
		{
			int width = 200;
			int listHeight = 181;
			int textHeight = 27;
			Rectangle bounds = button.Links[0].Bounds;
			int left = bounds.Left;
			int top = bounds.Top + bounds.Height;
			int index = -1;
			int lastIndex = index;

			//Rectangle pageGroupBounds = button.Manager.Form.RectangleToScreen((button.Manager.Form as DevExpress.XtraBars.Ribbon.RibbonControl).Controls.get ViewInfo.Panel.Groups[0].Bounds);

			PanelControl panel = new PanelControl()
			{
				Size = new Size(width, textHeight + listHeight),
				Padding = new Padding(0),
				Margin = new Padding(0),
				//BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles. BorderStyle.FixedSingle,
			};

			LabelControl label = new LabelControl()
			{
				Size = new Size(width, textHeight),
				Location = new Point(6, listHeight + 3),
				//TextAlign = ContentAlignment.MiddleCenter,
				Text = String.Format("{0} 1 Action", action),
				Padding = new Padding(0),
				Margin = new Padding(0),
				//AutoSizeMode = LabelAutoSizeMode.None
			};

			//label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

			ListBoxControl list = new ListBoxControl()
			{
				Size = new Size(width, listHeight),
				Location = new Point(1, 1),
				SelectionMode = SelectionMode.MultiExtended,
				//ScrollAlwaysVisible = true,
				Padding = new Padding(0),
				Margin = new Padding(0),
				BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder,
				Font = new Font(panel.Font.FontFamily, 8),
			};

			foreach (var item in commands)
				list.Items.Add(item);

			if (list.Items.Count == 0)
				return;

			list.ShowFocusRect = false;
			list.UnSelectAll();

			ToolStripControlHost toolHost = new ToolStripControlHost(panel) { Size = panel.Size, Margin = new Padding(0) };
			ToolStripDropDown toolDrop = new ToolStripDropDown() { Padding = new Padding(0) };
			toolDrop.Items.Add(toolHost);

			panel.Controls.Add(list);
			panel.Controls.Add(label);
			
			toolDrop.Show(this.undoButton.Manager.Form, new Point(left, top));
			this.RefreshLabel(label, action, index + 1);

			// *Note: These will be "up values" that will exist beyond the scope of this function
			list.Click += (sender, e) => 
			{
				toolDrop.Close();
				callback(index + 1);
				this.SetEnableProperty();
			};

			list.MouseMove += (sender, e) =>
			{
				index = list.IndexFromPoint(e.Location);

				if (e.Location.Y < list.Top)
					index = -1;

				//label.Text = index.ToString();

				if (index != lastIndex)
				{
					int topIndex = Math.Max(-1, Math.Min(list.TopIndex + e.Delta, list.Items.Count - 1));

					list.BeginUpdate();

					//while (list.SelectedItems.Count > 0)
					//	list.SetSelected(0, false);

					//list.SelectedItems.Count // ClearSelected();


					//bool select = (index > lastIndex);
					//int min = Math.Min(lastIndex, index);
					//int max = Math.Max(lastIndex, index);

					//for (int i = min; i < max; ++i)
					//	list.SetSelected(i, select);

					if (index > lastIndex)
					{
						for (int i = lastIndex + 1; i <= index; ++i)
							list.SetSelected(i, true);
					}
					else
					{
						for (int i = index + 1; i <= lastIndex; ++i)
							list.SetSelected(i, false);
					}

					this.RefreshLabel(label, action, index + 1);
					lastIndex = index;
					list.EndUpdate();
					list.TopIndex = topIndex;
				}
				else if (index == -1)
				{
					list.UnSelectAll();
					this.RefreshLabel(label, action, index + 1);
					this.undoButton.Manager.Form.Focus();
				}
			};

			//list.MouseLeave += (sender, e) =>
			//{
			//	index = -1;
			//	this.RefreshLabel(label, action, index + 1);
			//};
			//this.undoButton.Manager.Form.Focus();
			//list.Focus();
		}

		private void RefreshLabel(LabelControl label, string action, int count)
		{
			label.Text = String.Format("{0} {1} Action{2}", action, count, (count == 1) ? "" : "s");
		}

		private void DrawDropDown(BarButtonItem button, string action, IEnumerable<string> commands, UndoRedoCallback callback) // ToolStripItem
		{
			int width = 200;
			int listHeight = 181;
			int textHeight = 27;
			Rectangle bounds = button.Links[0].Bounds;
			int left = bounds.Left;
			int top = bounds.Top + bounds.Height;
			int index = -1;
			int lastIndex = index;

			//Rectangle pageGroupBounds = button.Manager.Form.RectangleToScreen((button.Manager.Form as DevExpress.XtraBars.Ribbon.RibbonControl).Controls.get ViewInfo.Panel.Groups[0].Bounds);

			PanelControl panel = new PanelControl()
			{
				Size = new Size(width, textHeight + listHeight),
				Padding = new Padding(0),
				Margin = new Padding(0),
				//BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles. BorderStyle.FixedSingle,
			};

			LabelControl label = new LabelControl()
			{
				Size = new Size(width, textHeight),
				Location = new Point(6, listHeight + 3),
				//TextAlign = ContentAlignment.MiddleCenter,
				Text = String.Format("{0} 1 Action", action),
				Padding = new Padding(0),
				Margin = new Padding(0),
				//AutoSizeMode = LabelAutoSizeMode.None
			};

			//label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

			ListBoxControl list = new ListBoxControl()
			{
				Size = new Size(width, listHeight),
				Location = new Point(1, 1),
				SelectionMode = SelectionMode.MultiExtended,
				//ScrollAlwaysVisible = true,
				Padding = new Padding(0),
				Margin = new Padding(0),
				BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder,
				Font = new Font(panel.Font.FontFamily, 8),
			};

			foreach (var item in commands)
				list.Items.Add(item);

			if (list.Items.Count == 0)
				return;

			list.ShowFocusRect = false;
			list.UnSelectAll();

			ToolStripControlHost toolHost = new ToolStripControlHost(panel) { Size = panel.Size, Margin = new Padding(0) };
			ToolStripDropDown toolDrop = new ToolStripDropDown() { Padding = new Padding(0) };
			toolDrop.Items.Add(toolHost);

			panel.Controls.Add(list);
			panel.Controls.Add(label);

			toolDrop.Show(this.undoButton.Manager.Form, new Point(left, top));
			this.RefreshLabel(label, action, index + 1);

			// *Note: These will be "up values" that will exist beyond the scope of this function
			list.Click += (sender, e) =>
			{
				toolDrop.Close();
				callback(index + 1);
				this.SetEnableProperty();
			};

			list.MouseMove += (sender, e) =>
			{
				index = list.IndexFromPoint(e.Location);

				if (e.Location.Y < list.Top)
					index = -1;

				//label.Text = index.ToString();

				if (index != lastIndex)
				{
					int topIndex = Math.Max(-1, Math.Min(list.TopIndex + e.Delta, list.Items.Count - 1));

					list.BeginUpdate();

					//while (list.SelectedItems.Count > 0)
					//	list.SetSelected(0, false);

					//list.SelectedItems.Count // ClearSelected();


					//bool select = (index > lastIndex);
					//int min = Math.Min(lastIndex, index);
					//int max = Math.Max(lastIndex, index);

					//for (int i = min; i < max; ++i)
					//	list.SetSelected(i, select);

					if (index > lastIndex)
					{
						for (int i = lastIndex + 1; i <= index; ++i)
							list.SetSelected(i, true);
					}
					else
					{
						for (int i = index + 1; i <= lastIndex; ++i)
							list.SetSelected(i, false);
					}

					this.RefreshLabel(label, action, index + 1);
					lastIndex = index;
					list.EndUpdate();
					list.TopIndex = topIndex;
				}
				else if (index == -1)
				{
					list.UnSelectAll();
					this.RefreshLabel(label, action, index + 1);
					this.undoButton.Manager.Form.Focus();
				}
			};

			//list.MouseLeave += (sender, e) =>
			//{
			//	index = -1;
			//	this.RefreshLabel(label, action, index + 1);
			//};
			//this.undoButton.Manager.Form.Focus();
			//list.Focus();
		}


		private void DrawDropDown2(BarSubItem button, string action, IEnumerable<string> commands, UndoRedoCallback callback) // ToolStripItem
		{
			int width = 200;
			int listHeight = 181;
			int textHeight = 29;

			Panel panel = new Panel()
			{
				Size = new Size(width, textHeight + listHeight),
				Padding = new Padding(0),
				Margin = new Padding(0),
				BorderStyle = BorderStyle.FixedSingle,
			};

			Label label = new Label()
			{
				Size = new Size(width, textHeight),
				Location = new Point(1, listHeight - 2),
				TextAlign = ContentAlignment.MiddleCenter,
				Text = String.Format("{0} 1 Action", action),
				Padding = new Padding(0),
				Margin = new Padding(0),
			};

			ListBox list = new ListBox()
			{
				Size = new Size(width, listHeight),
				Location = new Point(1, 1),
				SelectionMode = SelectionMode.MultiSimple,
				ScrollAlwaysVisible = true,
				Padding = new Padding(0),
				Margin = new Padding(0),
				BorderStyle = BorderStyle.None,
				Font = new Font(panel.Font.FontFamily, 9),
			};

			foreach (var item in commands)
				list.Items.Add(item);

			if (list.Items.Count == 0)
				return;

			list.SelectedIndex = 0;

			ToolStripControlHost toolHost = new ToolStripControlHost(panel) { Size = panel.Size, Margin = new Padding(0) };
			ToolStripDropDown toolDrop = new ToolStripDropDown() { Padding = new Padding(0) };
			toolDrop.Items.Add(toolHost);

			panel.Controls.Add(list);
			panel.Controls.Add(label);

			//toolDrop.Show(this.undoButton.Manager.Form, new Point(button.Bounds.Left + button.Owner.Left, button.Bounds.Bottom + button.Owner.Top));
			toolDrop.Show(this.undoButton.Manager.Form, new Point(220, 27));

			// *Note: These will be "up values" that will exist beyond the scope of this function
			int index = 1;
			int lastIndex = 1;

			list.Click += (sender, e) => { toolDrop.Close(); callback(index); };
			list.MouseMove += (sender, e) =>
			{
				index = Math.Max(1, list.IndexFromPoint(e.Location) + 1);

				if (lastIndex != index)
				{
					int topIndex = Math.Max(0, Math.Min(list.TopIndex + e.Delta, list.Items.Count - 1));

					list.BeginUpdate();
					list.ClearSelected();

					for (int i = 0; i < index; ++i)
						list.SelectedIndex = i;

					label.Text = String.Format("{0} {1} Action{2}", action, index, (index == 1) ? "" : "s");
					lastIndex = index;
					list.EndUpdate();
					list.TopIndex = topIndex;
				}
			};

			list.Focus();
		}

		private void AddHistory(UndoAction undoAction)
		{
			//if (this.position == this.history.Count - 1)
			//{
			//	this.history.Add(undoAction);
			//	this.position++;
			//}
			//else
			//{
			//	this.position++;
			//	this.history.Insert(this.position, undoAction);
			//}

			this.history.Insert(++this.position, undoAction);
			//this.position++;
			this.SetEnableProperty();
		}

		private void SetEnableProperty()
		{
			if (this.undoButton != null)
				this.undoButton.Enabled = (this.position >= 0);

			if (this.redoButton != null)
				this.redoButton.Enabled = (this.position < this.history.Count - 1);
		}
	}



	//class ObjectAction
	//{
	//	public ObjectAction(ObjectActionType actionType)
	//	{
	//		this.ActionType = actionType;
	//	}

	//	public ObjectActionType ActionType { get; private set; }
	//	public ChangePropertyValueSimpleObjectRequesterEventArgs PropertyChangeInfo { get; set; }
	//	public RelationForeignObjectSetEventArgs ForeignRelationSetInfo { get; set; }
	//	public SimpleObjectRequesterEventArgs SimpleObjectInfo { get; set; }
	//	public TransactionActionsEventArgs TransactionInfo { get; set; }

	//	public override string ToString()
	//	{
	//		string text = String.Empty;

	//		switch (this.ActionType)
	//		{
	//			case ObjectActionType.PropertyChange:
					
	//				if (this.PropertyChangeInfo.Value is String)
	//				{
	//					if (this.PropertyChangeInfo.Value == null)
	//					{
	//						text = "Set null";
	//					}
	//					else if (this.PropertyChangeInfo.OldValue != null)
	//					{
	//						string sufix = String.Empty;

	//						if ((this.PropertyChangeInfo.Value as String).Length > (this.PropertyChangeInfo.OldValue as String).Length)
	//						{
	//							text = "Add ";
	//							sufix = (this.PropertyChangeInfo.Value as String).Substring((this.PropertyChangeInfo.OldValue as String).Length);
	//						}
	//						else
	//						{
	//							text = "Delete ";
	//							sufix = (this.PropertyChangeInfo.OldValue as String).Substring((this.PropertyChangeInfo.Value as String).Length);
	//						}

	//						if (sufix.Trim().Length == 0)
	//							sufix = "'" + sufix + "'";

	//						text += sufix;
	//					}
	//					else
	//					{
	//						text = "Set '" + (this.PropertyChangeInfo.Value as String) + "'";
	//					}
	//				}
	//				else
	//				{
	//					text = this.PropertyChangeInfo.PropertyName + "=";
	//					text += (this.PropertyChangeInfo.Value != null) ? this.PropertyChangeInfo.Value.ToString() : "null"; 
	//				}

	//				break;

	//			case ObjectActionType.ForeignObjectSet:

	//				text = "Set " + this.ForeignRelationSetInfo.ObjectRelationPolicyModel.Name + "=";
	//				text += (this.ForeignRelationSetInfo.ForeignBusinessObject != null) ? this.ForeignRelationSetInfo.ForeignBusinessObject.Name : "null";

	//				break;

	//			case ObjectActionType.ObjectCreated:

	//				text = "Created " + this.SimpleObjectInfo.SimpleObject.ToString();

	//				break;

	//			case ObjectActionType.ObjectDeleted:

	//				text = "Deleted " + this.SimpleObjectInfo.SimpleObject.ToString();

	//				break;

	//			case ObjectActionType.Transaction:

	//				text = "Transaction " + this.TransactionInfo.Transaction.Key.ObjectID.ToString();

	//				break;

	//		}

	//		return text;
	//	}

	//}

	//enum ObjectActionType
	//{
	//	PropertyChange,
	//	ForeignObjectSet,
	//	ObjectCreated,
	//	ObjectDeleted,
	//	Transaction
	//}
}