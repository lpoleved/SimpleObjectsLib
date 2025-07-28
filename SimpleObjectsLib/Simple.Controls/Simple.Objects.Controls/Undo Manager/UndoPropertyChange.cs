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
using Simple.Objects;

namespace Simple.Objects.Controls
{
	public class UndoPropertyChange : UndoAction
	{
		public UndoPropertyChange(ChangePropertyValuePertyModelSimpleObjectChangeContainerContextRequesterEventArgs propertyChangeInfo)
		{
			this.PropertyChangeInfo = propertyChangeInfo;
		}

		public ChangePropertyValuePertyModelSimpleObjectChangeContainerContextRequesterEventArgs PropertyChangeInfo { get; private set; }

		public override string GetText(UndoActionType actionType)
		{
			string text = String.Empty;
			string? valueDiff = String.Empty;

			if ((this.PropertyChangeInfo.OldPropertyValue == null && actionType == UndoActionType.Undo) || (this.PropertyChangeInfo.PropertyValue == null && actionType == UndoActionType.Redo))
			{
				text = "Set ";
				valueDiff = "null";
			}
			else if (this.PropertyChangeInfo.PropertyValue is String)
			{
				string? value = this.PropertyChangeInfo.PropertyValue as String;
				string? oldValue = this.PropertyChangeInfo.OldPropertyValue as String;

				if (actionType == UndoActionType.Undo)
				{
					if (value != null)
					{
						if (value.Length > oldValue?.Length)
						{
							// new text is inserted
							text = "Delete ";
							valueDiff = value.Substring(oldValue.Length);
						}
						else
						{
							// old text is deleted
							text = "Add ";
							valueDiff = oldValue?.Substring(value.Length);
						}
					}
					else
					{
						text = "Set ";
						valueDiff = oldValue;
					}
				}
				else // if (this.ActionType == UndoType.Redo) 
				{
					if (oldValue != null)
					{
						if (value?.Length > oldValue.Length)
						{
							// new text is inserted
							text = "Add ";
							valueDiff = value.Substring(oldValue.Length);
						}
						else
						{
							// old text is deleted
							text = "Delete ";
							valueDiff = oldValue.Substring(value?.Length ?? 0);
						}
					}
					else
					{
						text = "Set ";
						valueDiff = value;
					}
				}
			}
			else
			{
				text = "Set ";
				valueDiff = (actionType == UndoActionType.Undo) ? this.PropertyChangeInfo.OldPropertyValue?.ToString() :
																  this.PropertyChangeInfo.PropertyValue?.ToString();
			}

			if (valueDiff?.Trim().Length == 0)
				valueDiff = "'" + valueDiff + "'";

			text += valueDiff;

			return text;
		}

		public override void Undo()
		{
			if (this.PropertyChangeInfo.PropertyModel != null)
				this.PropertyChangeInfo.SimpleObject.SetPropertyValue(this.PropertyChangeInfo.PropertyModel, this.PropertyChangeInfo.OldPropertyValue, requester: this);
		}

		public override void Redo()
		{
			if (this.PropertyChangeInfo.PropertyModel != null)
				this.PropertyChangeInfo.SimpleObject.SetPropertyValue(this.PropertyChangeInfo.PropertyModel, this.PropertyChangeInfo.PropertyValue, requester: this);
		}
	}
}
