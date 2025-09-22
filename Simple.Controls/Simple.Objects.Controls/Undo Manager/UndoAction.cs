using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars.Ribbon;

namespace Simple.Objects.Controls
{
	public abstract class UndoAction
	{
		public RibbonPage? SelectedRibbonPage { get; set; }

		public abstract string GetText(UndoActionType actionType);
		public abstract void Undo();
		public abstract void Redo();

		public enum UndoActionType
		{
			Undo,
			Redo
		}
	}
}
