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
		public UndoPropertyChange(ChangePropertyValueSimpleObjectRequesterEventArgs propertyChangeInfo)
		{
			this.PropertyChangeInfo = propertyChangeInfo;
		}

		public ChangePropertyValueSimpleObjectRequesterEventArgs PropertyChangeInfo { get; private set; }

		public override string GetText(UndoActionType actionType)
		{
			string text = String.Empty;
			string? valueDiff = String.Empty;

			if ((this.PropertyChangeInfo.OldValue == null && actionType == UndoActionType.Undo) || (this.PropertyChangeInfo.Value == null && actionType == UndoActionType.Redo))
			{
				text = "Set ";
				valueDiff = "null";
			}
			else if (this.PropertyChangeInfo.Value is String)
			{
				string? value = this.PropertyChangeInfo.Value as String;
				string? oldValue = this.PropertyChangeInfo.OldValue as String;

				if (actionType == UndoActionType.Undo)
				{
					if (value != null)
					{
						if (value.Length > oldValue.Length)
						{
							// new text is inserted
							text = "Delete ";
							valueDiff = value.Substring(oldValue.Length);
						}
						else
						{
							// old text is deleted
							text = "Add ";
							valueDiff = oldValue.Substring(value.Length);
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
						if (value.Length > oldValue.Length)
						{
							// new text is inserted
							text = "Add ";
							valueDiff = value.Substring(oldValue.Length);
						}
						else
						{
							// old text is deleted
							text = "Delete ";
							valueDiff = oldValue.Substring(value.Length);
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
				valueDiff = (actionType == UndoActionType.Undo) ? this.PropertyChangeInfo.OldValue.ToString() :
																  this.PropertyChangeInfo.Value.ToString();
			}

			if (valueDiff.Trim().Length == 0)
				valueDiff = "'" + valueDiff + "'";

			text += valueDiff;

			return text;
		}

		public override void Undo()
		{
			this.PropertyChangeInfo.SimpleObject.SetPropertyValue(this.PropertyChangeInfo.PropertyModel, this.PropertyChangeInfo.OldValue, requester: this);
		}

		public override void Redo()
		{
			this.PropertyChangeInfo.SimpleObject.SetPropertyValue(this.PropertyChangeInfo.PropertyModel, this.PropertyChangeInfo.Value, requester: this);
		}
	}
}
