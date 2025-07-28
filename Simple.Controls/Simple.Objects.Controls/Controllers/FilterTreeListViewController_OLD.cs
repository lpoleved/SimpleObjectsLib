//using System;
//using DevExpress.ExpressApp;
//using DevExpress.XtraTreeList;
//using DevExpress.ExpressApp.Actions;
//using DevExpress.XtraTreeList.Nodes;
//using DevExpress.ExpressApp.SystemModule;
//using DevExpress.ExpressApp.Editors;
//using DevExpress.ExpressApp.Win.Controls;
////using DevExpress.ExpressApp.TreeListEditors.Win;

//namespace Simple.Objects.Controls
//{
//	public partial class FilterTreeListViewController : ViewController<ListView>
//	{
//		DevExpress.XtraTreeList.TreeList treeList;
//		TreeListNode startNode;
//		TreeListNode currentNode;
//		ParametrizedAction fullTextFilterAction;
//		public FilterTreeListViewController()
//		{
//			TargetViewId = "TestTreeObject_ListView";
//		}
//		protected override void OnViewControlsCreated()
//		{
//			base.OnViewControlsCreated();
//			fullTextFilterAction = Frame.GetController<FilterController>().FullTextFilterAction;
//			fullTextFilterAction.Executed += FullTextFilterAction_Executed;
//			//TreeListEditor treeListEditor = View.Editor as TreeListEditor;
//			//if (treeListEditor != null)
//			//{
//			//	treeList = treeListEditor.TreeList;
//			//	treeList = treeListEditor.TreeList;
//			//	treeList.KeyDown += treeList_KeyDown;
//			//	treeList.Disposed += treeList_Disposed;
//			//}
//		}
//		void FullTextFilterAction_Executed(object sender, ActionBaseEventArgs e)
//		{
//			if (startNode == null)
//			{
//				startNode = treeList.Nodes.FirstNode;
//				currentNode = startNode;
//				treeList.FocusedNode = null;
//			}
//			TreeListSearch(fullTextFilterAction.Value as string);
//		}
//		void treeList_Disposed(object sender, EventArgs e)
//		{
//			DevExpress.XtraTreeList.TreeList treeList = (DevExpress.XtraTreeList.TreeList)sender;

//			treeList.KeyDown -= treeList_KeyDown;
//			treeList.Disposed -= treeList_Disposed;
//		}
//		void treeList_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
//		{
//			if (e.KeyCode == System.Windows.Forms.Keys.F3)
//			{
//				TreeListSearch(fullTextFilterAction.Value as string);
//			}
//		}
//		protected virtual void TreeListSearch(string value)
//		{
//			if (!string.IsNullOrEmpty(value))
//			{
//				DoSearch(value);
//			}
//		}
//		protected void DoSearch(string textToSearch)
//		{
//			while (currentNode != null)
//			{
//				string value = currentNode[View.ObjectTypeInfo.DefaultMember.BindingName] as string;
//				if (value != null)
//				{
//					if (value.Contains(textToSearch))
//					{
//						treeList.FocusedNode = currentNode;
//						currentNode = GetNextTreeListNode(currentNode);
//						if (currentNode == null)
//							currentNode = startNode;
//						return;
//					}
//					currentNode = GetNextTreeListNode(currentNode);
//					if (currentNode == null)
//					{
//						currentNode = startNode;
//						break;
//					}
//				}
//			}
//		}
//		private TreeListNode GetNextTreeListNode(TreeListNode node)
//		{
//			if (node == null)
//				return null;
//			ObjectTreeListNode onode = node as ObjectTreeListNode;
//			if (onode != null)
//			{
//				((ObjectTreeList)treeList).BuildChildNodes(onode);
//			}
//			if (node.Nodes.Count > 0)
//				return node.Nodes[0];
//			if (node.ParentNode != null)
//			{
//				TreeListNodes owner = node.ParentNode.Nodes;
//				while (node == owner.LastNode)
//				{
//					if (owner == treeList.Nodes)
//						return null;
//					if (node.ParentNode == null)
//						return null;
//					node = node.ParentNode;

//					owner = node.ParentNode == null ? treeList.Nodes : node.ParentNode.Nodes;
//				}
//				int index = owner.IndexOf(node);
//				return owner[index + 1];
//			}
//			else
//			{
//				if (treeList.Nodes.LastNode == node)
//					return null;
//				else
//				{
//					int index = treeList.Nodes.IndexOf(node);
//					return treeList.Nodes[index + 1];
//				}
//			}
//		}
//		protected override void OnDeactivated()
//		{
//			fullTextFilterAction.Executed -= FullTextFilterAction_Executed;
//			fullTextFilterAction = null;
//			treeList = null;
//			currentNode = null;
//			startNode = null;
//			base.OnDeactivated();
//		}
//	}
//}