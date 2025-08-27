using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace Simple.Controls
{
	public partial class FormFind : XtraForm
	{
		private string lastFoundedText = String.Empty;
		private ITextFinder? textFinder = null;
		private System.Timers.Timer actionInfoTimer = new System.Timers.Timer(7000);
		private const string strFormCaprion = "Find";

		public FormFind()
		{
			InitializeComponent();

			this.actionInfoTimer.Elapsed += new System.Timers.ElapsedEventHandler(actionInfoTimer_Elapsed);
		}

		public ITextFinder? TextFinder 
		{
			get { return this.textFinder; }
			
			set
			{
				this.textFinder = value;
				this.lastFoundedText = String.Empty;
			}
		}

		private void buttonFindNext_Click(object sender, EventArgs e)
		{
			if (this.TextFinder != null && this.editorFindWhat.Text != null && this.editorFindWhat.Text.Trim().Length > 0)
			{
				Cursor? currentCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;

				this.actionInfoTimer.Stop();
				this.Text = strFormCaprion;
				
				object startNode =  this.TextFinder.FocusedNode;
				string textToFind = this.editorFindWhat.Text.Trim();
				
				bool isFind = this.TextFinder.FindNextText(startNode, textToFind, this.checkEditMatchCase.Checked);

				if (isFind)
				{
					this.lastFoundedText = textToFind;
					this.Text = strFormCaprion;
				}
				else
				{
					this.Text = strFormCaprion;
					this.Text += (this.lastFoundedText == textToFind) ? ":  No more occurrences found" : ":  The specified text was not found";

					this.lastFoundedText = String.Empty;
					this.actionInfoTimer.Start();
				}

				Cursor.Current = currentCursor;
			}
		}

		private void actionInfoTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() => this.OnActionInfoTimer_Elapsed(e)));
			}
			else
			{
				this.OnActionInfoTimer_Elapsed(e);
			}
		}

		private void OnActionInfoTimer_Elapsed(System.Timers.ElapsedEventArgs e)
		{
			this.Text = strFormCaprion;
		}


		//private void FindTreeListNode(TreeListNode startNode, int startColumnIndex, int startTextPosition, string textToFind, FindTextDirection findDirection)
		//{
		//	if (startNode != null)
		//	{
		//		textToFind = textToFind.Trim();
				
		//		for (int i = startColumnIndex; i < startNode.TreeList.Columns.Count; i++)
		//		{
		//			string columnText = startNode[i] as string;

		//			if (columnText != null)
		//			{
		//				int findTextPosition = columnText.Trim().IndexOf(textToFind, startTextPosition);

		//				if (findTextPosition >= 0)
		//				{
		//					startNode.TreeList.FocusedColumn = startNode.TreeList.Columns[i];
		//					startNode.TreeList.ShowEditor();

		//					if (startNode.TreeList.ActiveEditor is TextEdit)
		//					{
		//						(startNode.TreeList.ActiveEditor as TextEdit).SelectionStart = findTextPosition;
		//						(startNode.TreeList.ActiveEditor as TextEdit).SelectionLength = textToFind.Length;
		//					}
		//					else if (startNode.TreeList.ActiveEditor is DateEdit)
		//					{
		//						(startNode.TreeList.ActiveEditor as DateEdit).SelectionStart = findTextPosition;
		//						(startNode.TreeList.ActiveEditor as DateEdit).SelectionLength = textToFind.Length;
		//					}

		//					break;
		//				}
		//			}
		//		}

		//		// TODO: Iterate throught nodes
		//	}
		//}

		//private TreeListNode FindTreeListNode(TreeListNode node)
		//{
		//	if (node == null)
		//		return null;
		//	ObjectTreeListNode onode = node as ObjectTreeListNode;
		//	if (onode != null)
		//	{
		//		((ObjectTreeList)treeList).BuildChildNodes(onode);
		//	}
		//	if (node.Nodes.Count > 0)
		//		return node.Nodes[0];
		//	if (node.ParentNode != null)
		//	{
		//		TreeListNodes owner = node.ParentNode.Nodes;
		//		while (node == owner.LastNode)
		//		{
		//			if (owner == treeList.Nodes)
		//				return null;
		//			if (node.ParentNode == null)
		//				return null;
		//			node = node.ParentNode;

		//			owner = node.ParentNode == null ? treeList.Nodes : node.ParentNode.Nodes;
		//		}
		//		int index = owner.IndexOf(node);
		//		return owner[index + 1];
		//	}
		//	else
		//	{
		//		if (treeList.Nodes.LastNode == node)
		//			return null;
		//		else
		//		{
		//			int index = treeList.Nodes.IndexOf(node);
		//			return treeList.Nodes[index + 1];
		//		}
		//	}
		//}

		
	}

	//public enum FindTextDirection
	//{
	//	GetNext,
	//	GetPrevious
	//}
}
