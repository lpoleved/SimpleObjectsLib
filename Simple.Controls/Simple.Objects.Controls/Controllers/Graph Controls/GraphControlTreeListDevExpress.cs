using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using Simple.Controls.TreeList;
using Simple.Objects;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon; 

namespace Simple.Objects.Controls
{
    public class GraphControlTreeListDevExpress : IGraphControl, IDisposable
    {
        #region |   Private Members   |

        private IGraphController graphController;
        private SimpleTreeList? treeList;

		private FieldInfo? imageIndexFieldInfo = typeof(TreeListNode).GetField("imageIndex", BindingFlags.Instance | BindingFlags.NonPublic);
		private FieldInfo? selectImageIndexFieldInfo = typeof(TreeListNode).GetField("selectImageIndex", BindingFlags.Instance | BindingFlags.NonPublic);
		private FieldInfo? stateImageIndexFieldInfo = typeof(TreeListNode).GetField("stateImageIndex", BindingFlags.Instance | BindingFlags.NonPublic);
		private object? destinationNodeParentCandidate = null;

		#endregion |   Private Members   |

		#region |   Constructors and Initialization   |

		public GraphControlTreeListDevExpress(IGraphController graphController)
        {
            this.graphController = graphController;
        }

        #endregion |   Constructors and Initialization   |

        #region |   Public Properties   |

        public IGraphController GraphController
        {
            get { return this.graphController; }
        }

        public SimpleTreeList? TreeList
        {
            get { return this.treeList; }
            set
            {
                if (this.treeList != null)
                {
                    this.treeList.BeforeFocusNode -= new BeforeFocusNodeEventHandler(treeList_BeforeFocusNode);
                    this.treeList.FocusedNodeChanged -= new FocusedNodeChangedEventHandler(treeList_FocusedNodeChanged);
                    this.treeList.CellValueChanging -= new CellValueChangedEventHandler(treeList_CellValueChanging);
                    //this.treeList.ValidateNode -= new ValidateNodeEventHandler(treeList_ValidateNode);
                    this.treeList.BeforeExpand -= new BeforeExpandEventHandler(TreeList_BeforeExpand);
                    this.treeList.AfterExpand -= new NodeEventHandler(treeList_AfterExpand);
                    this.treeList.BeforeCollapse -= new BeforeCollapseEventHandler(treeList_BeforeCollapse);
                    this.treeList.AfterCollapse -= new NodeEventHandler(treeList_AfterCollapse);
                    this.treeList.BeforeCheckNode -= new CheckNodeEventHandler(treeList_BeforeCheckNode);
                    this.treeList.KeyUp -= new KeyEventHandler(treeList_KeyUp);
                    this.treeList.KeyDown -= new KeyEventHandler(treeList_KeyDown);
                    this.treeList.BeforeDragNode -= new BeforeDragNodeEventHandler(treeList_BeforeDragNode);
                    this.treeList.DragOver -= new DragEventHandler(treeList_DragOver);
                    this.treeList.DragDrop -= new DragEventHandler(treeList_DragDrop);
                    this.treeList.GiveFeedback -= new GiveFeedbackEventHandler(treeList_GiveFeedback);
					this.treeList.EndSorting -= new EventHandler(treeList_EndSorting);
				}

                this.treeList = value;

                if (this.treeList != null)
                {
                    this.treeList.BeforeFocusNode += new BeforeFocusNodeEventHandler(treeList_BeforeFocusNode);
                    this.treeList.FocusedNodeChanged += new FocusedNodeChangedEventHandler(treeList_FocusedNodeChanged);
                    this.treeList.CellValueChanging += new CellValueChangedEventHandler(treeList_CellValueChanging);
                    //this.treeList.ValidateNode += new ValidateNodeEventHandler(treeList_ValidateNode);
                    this.treeList.BeforeExpand += new BeforeExpandEventHandler(TreeList_BeforeExpand);
                    this.treeList.AfterExpand += new NodeEventHandler(treeList_AfterExpand);
                    this.treeList.BeforeCollapse += new BeforeCollapseEventHandler(treeList_BeforeCollapse);
                    this.treeList.AfterCollapse += new NodeEventHandler(treeList_AfterCollapse);
                    this.treeList.BeforeCheckNode += new CheckNodeEventHandler(treeList_BeforeCheckNode);
                    this.treeList.KeyUp += new KeyEventHandler(treeList_KeyUp);
                    this.treeList.KeyDown += new KeyEventHandler(treeList_KeyDown);
                    this.treeList.BeforeDragNode += new BeforeDragNodeEventHandler(treeList_BeforeDragNode);
                    this.treeList.DragOver += new DragEventHandler(treeList_DragOver);
                    this.treeList.DragDrop += new DragEventHandler(treeList_DragDrop);
					this.treeList.AfterDropNode += TreeList_AfterDropNode;
                    this.treeList.GiveFeedback += new GiveFeedbackEventHandler(treeList_GiveFeedback);
					this.treeList.EndSorting += new EventHandler(treeList_EndSorting);

                    this.treeList.CustomNodeImagesDraw = true;
                    this.treeList.UseExpandedImages = true;
                }
                else
				{
                    throw new ArgumentNullException("The TreeList cannot be null");
				}
            }
        }


		#endregion |   Public Properties   |

		#region |   Public Methods   |

		public void SetEditMode(GraphEditMode editMode)
        {
            if (this.TreeList is null)
                return;
            
            switch (editMode)
            {
                case GraphEditMode.Select:
                        
                    this.TreeList.OptionsSimple.EditMode = SimpleTreeEditMode.Select;
                        
                    break;

                case GraphEditMode.SelectEdit:
                        
                    this.TreeList.OptionsSimple.EditMode = SimpleTreeEditMode.SelectEdit;
                        
                    break;

                case GraphEditMode.Edit:

                    this.TreeList.OptionsSimple.EditMode = SimpleTreeEditMode.Edit;

                    break;

                case GraphEditMode.ViewOnly:
                        
                    this.TreeList.OptionsSimple.EditMode = SimpleTreeEditMode.ViewOnly;
                        
                    break;

                default:
                        
                    this.TreeList.OptionsSimple.EditMode = SimpleTreeEditMode.Standard;
                        
                    break;
            }
        }

        public void SetLookAndFeelStyle(GraphLookAndFeelStyle lookAndFeelStyle)
        {
            if (this.TreeList != null)
                return;

            switch (lookAndFeelStyle)
            {
                case GraphLookAndFeelStyle.ExcelStyle:
                    
                    this.TreeList.OptionsSimple.LookAndFeelStyle = SimpleTreeLookAndFeelStyle.ExcelStyle;
                    
                    break;

                case GraphLookAndFeelStyle.ExplorerStyle:
                    
                    this.TreeList.OptionsSimple.LookAndFeelStyle = SimpleTreeLookAndFeelStyle.ExplorerStyle;

                    break;

                default:
                    
                    this.TreeList.OptionsSimple.LookAndFeelStyle = SimpleTreeLookAndFeelStyle.Standard;
                    
                    break;
            }
        }

        public void SetCanDragAndDrop(bool canDragAndDrop)
        {
            if (this.TreeList != null)
            {
                //this.TreeList.OptionsBehavior.DragNodes = canDragAndDrop;
                this.TreeList.OptionsDragAndDrop.DragNodesMode = (canDragAndDrop) ? DragNodesMode.Single : DragNodesMode.None;
                //this.TreeList.DragNodesMode = TreeListDragNodesMode.Advanced;
                this.TreeList.OptionsDragAndDrop.DropNodesMode = DropNodesMode.Advanced;
            }
        }

        public void SetImageList(ImageList imageList)
        {
            if (this.TreeList != null)
                this.TreeList.SelectImageList = imageList;
        }

        public void SetStateImageList(ImageList stateImageList)
        {
            if (this.TreeList != null)
                this.TreeList.StateImageList = stateImageList;
        }

        public void SetCheckBoxImageList(ImageList checkBoxImageList)
        {
            if (this.TreeList != null)
                this.TreeList.CheckBoxImageList = checkBoxImageList;
        }

        public void BeginUpdate()
        {
            this.TreeList?.BeginUnboundLoad();
        }

        public void EndUpdate()
        {
            this.TreeList?.EndUnboundLoad();
        }

        public int AddColumn(string columnName, string caption, BindingDataType dataType, BindingEditorType editorType)
        {
            if (this.TreeList is null)
                return -1;
            
            TreeListColumn column = this.TreeList.Columns.Add();
            
            column.Name = columnName;
            column.FieldName = columnName;
            column.Caption = caption;
            column.VisibleIndex = this.TreeList.Columns.Count - 1;
            column.Format.FormatType = DevExpress.Utils.FormatType.Custom;
            column.Format.FormatString = "\\";

            // TODO: editor type implementation based on BindingDataType dataType and BindingEditorType editorType

			return column.ColumnIndex;
        }

		public void SetColumnCaption(int columnIndex, string caption)
        {
            if (this.TreeList is null)
                return;
            
            TreeListColumn column = this.TreeList.Columns[columnIndex];
            
            column.Caption = caption;
        }

		public void SetColumnDataType(int columnIndex, BindingDataType dataType)
        {
            TreeListColumn column = this.TreeList.Columns[columnIndex];
            // TODO: 
        }

		public void SetColumnEditorType(int columnIndex, BindingEditorType editorType)
        {
            TreeListColumn column = this.TreeList.Columns[columnIndex];
            // TODO: 
        }

		public int GetColumnWidth(int columnIndex)
        {
            if (this.TreeList is null)
                return -1;
            
            return this.TreeList.Columns[columnIndex].Width;
        }

		public void SetColumnWidth(int columnIndex, int width)
        {
            if (this.TreeList is null)
                return;

            this.TreeList.Columns[columnIndex].Width = width;
        }

		public void RemoveColumn(int columnIndex)
        {
            if (this.TreeList is null)
                return;

            TreeListColumn column = this.TreeList.Columns[columnIndex];
            
            this.TreeList.Columns.Remove(column);
        }

		public void SetColumnError(int columnIndex, string errorMessage)
        {
            if (this.TreeList is null)
                return;

            TreeListColumn column = this.TreeList.Columns[columnIndex];
            
            this.TreeList.SetColumnError(column, errorMessage, DevExpress.XtraEditors.DXErrorProvider.ErrorType.Default);
        }

        public void ResetColumnErrors()
        {
            if (this.TreeList is null)
                return;

            foreach (TreeListColumn column in this.TreeList.Columns)
                this.TreeList.SetColumnError(column, "", DevExpress.XtraEditors.DXErrorProvider.ErrorType.None);
        }

        public void BestFitColumns(bool applyAutoWidth)
        {
            if (this.TreeList is null)
                return;

            this.TreeList.OptionsView.AutoWidth = applyAutoWidth;
            this.TreeList.BestFitColumns(applyAutoWidth);
        }

		public void RemoveColumnSorting()
		{
			this.TreeList?.ClearSorting();
		}

        public object? AddNode(object parentNode, object nodeTag)
        {
            if (this.TreeList is null)
                return null;

            return this.TreeList.AppendNode(null, parentNode as TreeListNode, nodeTag);
        }

        public object? GetGraphNodeTag(object node)
        {
            if (node is TreeListNode treeListNode)
                return treeListNode.Tag;

            return default;
        }

        public void SetNodeHasChildrenProperty(object node, bool value)
        {
            if (node is TreeListNode treeListNode)
                treeListNode.HasChildren = value;
        }

        public void SetNodeCheckedProperty(object node, bool value)
        {
            if (node is TreeListNode treeListNode)
                treeListNode.Checked = value;
        }

        public void RemoveNode(object node)
        {
            if (node is TreeListNode treeListNode)
                this.TreeList?.DeleteNode(treeListNode);
        }

		public object? GetCellEditValue(object node, int columnIndex)
        {
            if (node is TreeListNode treeListNode)
			    return treeListNode.GetValue(columnIndex);

            return null;
        }

        public void SetCellEditValue(object node, int columnIndex, object? value)
        {
            if (node is TreeListNode treeListNode)
                treeListNode.SetValue(columnIndex, value);
        }

        public void MoveNode(object sourceNode, object? destinationNode)
        {
            this.TreeList?.MoveNode(sourceNode as TreeListNode, destinationNode as TreeListNode, false);
        }

        public void ExpandNode(object node)
        {
            if (node is TreeListNode treeListNode)
                treeListNode.Expanded = true;
        }

        public void CollapseNode(object node)
        {
            if (node is TreeListNode treeListNode)
                treeListNode.Expanded = false;
        }

        public void ExpandAll()
        {
            this.TreeList?.ExpandAll();
        }

        public void CollapseAll()
        {
            this.TreeList?.CollapseAll();
        }

        public object? GetParentNode(object node)
        {
            if (node is TreeListNode treeListNode)
                return treeListNode.ParentNode;

            return null;
        }

        public ICollection? GetChildNodes(object node)
        {
            if (node is TreeListNode treeListNode)
                return treeListNode.Nodes;

            return default;
        }

        public int GetChildNodesCount(object node)
        {
            if (node is TreeListNode treeListNode)
                return treeListNode.Nodes.Count;

            return -1;
        }

        public object? GetFocusedNode()
        {
            if (this.TreeList is null)
                return null;

            return this.TreeList.FocusedNode;
        }

        public void SetFocusedNode(object node)
        {
            if (this.TreeList != null && node is TreeListNode treeListNode)
                this.TreeList.FocusedNode = treeListNode;
        }

		public void SetFocusedColumn(int columnIndex)
        {
            TreeListColumn? column = this.TreeList.Columns[columnIndex];
            
            if (column is not null)
                this.TreeList.FocusedColumn = column;
        }

        public void Focus()
        {
            this.TreeList?.Focus();
        }

        public void CloseEditor()
        {
            this.TreeList?.CloseEditor();
        }

        public void ShowEditor()
        {
            if (this.TreeList is not null)
            {
                this.TreeList.OptionsBehavior.Editable = true;
                this.TreeList.ShowEditor();
            }
        }

        public void SetNodeImageIndex(object node, int imageIndex)
        {
            if (node is TreeListNode treeListNode)
            {
                this.imageIndexFieldInfo?.SetValue(treeListNode, imageIndex);
                this.selectImageIndexFieldInfo?.SetValue(treeListNode, imageIndex);
                this.TreeList?.RefreshNode(treeListNode);
            }
			//treeListNode.ImageIndex = imageIndex;
			//treeListNode.SelectImageIndex = imageIndex;
        }

        public void SetNodeExpandedImageIndex(object node, int imageIndex)
        {
            if (node is SimpleTreeListNode simpleTreeListNode)
                simpleTreeListNode.ExpandedImageIndex = imageIndex;
        }

        public void SetNodeStateImageIndex(object node, int imageIndex)
        {
            if (node is TreeListNode treeListNode)
            {
                this.stateImageIndexFieldInfo?.SetValue(treeListNode, imageIndex);
                this.TreeList?.RefreshNode(treeListNode);
            }

			//treeListNode.StateImageIndex = imageIndex;
        }

        public void SetNodeCheckBoxImageIndex(object node, int imageIndex)
        {
            if (node is SimpleTreeListNode simpleTreeListNode)
                simpleTreeListNode.CheckBoxImageIndex = imageIndex;
        }

        public void SetCheckBoxMode(bool checkBoxMode)
        {
            if (this.TreeList is null)
                return;

            this.TreeList.CustomCheckBoxDraw = checkBoxMode;

            if (checkBoxMode)
            {
                this.TreeList.OptionsView.CheckBoxStyle = DefaultNodeCheckBoxStyle.Check;
                this.TreeList.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
            }
            else
			{
                this.TreeList.OptionsView.CheckBoxStyle = DefaultNodeCheckBoxStyle.Default;
                this.TreeList.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Default;
            }
        }

        public void SetRecursiveNodeChecking(bool allowRecursiveNodeChecking)
		{
            if (this.TreeList != null) 
                this.TreeList.OptionsBehavior.AllowRecursiveNodeChecking = allowRecursiveNodeChecking;
        }

        public void SetOrderIndex(object node, int orderIndex)
		{
            if (this.TreeList != null && node is TreeListNode treeListNode)
                this.TreeList.SetNodeIndex(treeListNode, orderIndex);
		}

		public bool GetColumnEnableProperty(int columnIndex)
        {
            if (this.TreeList != null)
                return false;
            
            TreeListColumn? column = this.TreeList.Columns[columnIndex];

            if (column is not null)
                return column.OptionsColumn.AllowEdit;
                
            return false;
        }

		public void SetColumnEnableProperty(int columnIndex, bool enabled)
        {
            if (this.TreeList is null)
                return;

            TreeListColumn? column = this.TreeList.Columns[columnIndex];
            
            if (column is not null)
                column.OptionsColumn.AllowEdit = enabled;
        }

		public bool GetColumnVisibleProperty(int columnIndex)
		{
			TreeListColumn column = this.TreeList.Columns[columnIndex];
			
            return column.Visible;
		}

		public void SetColumnVisibleProperty(int columnIndex, bool enabled)
		{
            if (this.TreeList is null)
                return;

            TreeListColumn? column = this.TreeList.Columns[columnIndex];
            
            if (column is not null)
                column.Visible = enabled;
		}

		public bool GetColumnShowInCustomizationFormProperty(int columnIndex)
		{
            TreeListColumn? column = this.TreeList.Columns[columnIndex];

            if (column is not null)
                return column.OptionsColumn.ShowInCustomizationForm;

            return false;
		}

		public void SetColumnShowInCustomizationFormProperty(int columnIndex, bool value)
		{
            if (this.TreeList is null)
                return;

            TreeListColumn? column = this.TreeList.Columns[columnIndex];

            if (column is not null)
                column.OptionsColumn.ShowInCustomizationForm = value;
		}

		public int GetSortedColumnCount()
		{
			if (this.TreeList is not null)
                return this.TreeList.SortedColumnCount;

            return -1;
		}
		
		public void SetButtonEnableProperty(Component button, bool enabled)
        {
            if (button is BarItem barItem)
            {
                barItem.Enabled = enabled;
            }
            else if (button is Control control)
            {
                control.Enabled = enabled;
            }
            else
            {
                throw new ArgumentException("Button type " + button.GetType() + " is not supported");
            }
        }

        public object? GetGraphElement(object node)
        {
            if (node is TreeListNode treeListNode && treeListNode.Tag is GraphElementNodeTag graphElementNodeTag)
                return graphElementNodeTag.GraphElement;

            return null;
        }

		public Component? GetEditorComponent(int columnIndex)
        {
            if (this.TreeList is null)
                return null;

            TreeListColumn column = this.TreeList.Columns[columnIndex];

            if (column is not null)
            {
                Component editorComponent = column.ColumnEdit;

                if (editorComponent is BaseEdit baseEdit)
                    editorComponent = baseEdit.Properties;

                return editorComponent;
            }

            return null;
        }

        public void ClearNodes()
        {
            this.TreeList?.ClearNodes();
        }

        public Form? FindForm()
        {
            if (this.TreeList is null)
                return null;

            return this.TreeList.FindForm();
        }

       
        #endregion |   Public Methods   |
  
        #region |   TreeList Events   |

        private void treeList_BeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            bool canFocus = true;
            
            this.GraphController.BeforeNodeIsFocused(e.Node, e.OldNode, ref canFocus);
            e.CanFocus = canFocus;
        }

        private void treeList_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            this.GraphController.FocusedNodeIsChanged(e.Node, e.OldNode);
        }

        private void treeList_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            this.GraphController.CellValueIsChanged(e.Node, e.Column.AbsoluteIndex, e.Value);
        }

        private void treeList_KeyUp(object? sender, KeyEventArgs e)
        {
            // Restore old node value on ESCAPE key press
            if (e.KeyCode == Keys.Escape && this.TreeList?.FocusedNode != null && this.TreeList.FocusedColumn != null)
            {
                object cellValue = this.TreeList.FocusedNode[this.TreeList.FocusedColumn];
               
                this.GraphController.CellValueIsChanged(this.TreeList.FocusedNode, this.TreeList.FocusedColumn.AbsoluteIndex, cellValue);
            }
        }

        private void treeList_KeyDown(object? sender, KeyEventArgs e)
        {
            this.GraphController.OnKeyDown(sender, e);
        }


        private void treeList_BeforeDragNode(object sender, BeforeDragNodeEventArgs e)
        {
            bool canDrag = this.GraphController.CanDragNode(e.Node);
        }

        //private void treeList_DragLeave(object sender, EventArgs e)
        //{
        //    //throw new NotImplementedException();
        //}

        private void treeList_DragOver(object sender, DragEventArgs e)
        {
            DXDragEventArgs args = this.TreeList.GetDXDragEventArgs(e);
            //object dragNode = e.Data.GetData(typeof(SimpleTreeListNode));
            //         Point p = this.TreeList.PointToClient(new Point(e.X, e.Y));
            //         object targetNode = this.TreeList.CalcHitInfo(p).Node;

            //e.Effect = this.GraphController.GetDragDropEffect(dragNode, targetNode);

            e.Effect = DragDropEffects.None;

            if (args.DragInsertPosition != DragInsertPosition.None)
            {
                TreeListNode? newParentNode = this.GetDragDropParentNode(args.TargetNode, args.DragInsertPosition);
				bool canChangeParent = this.GraphController.CanNodeChangeParent(args.Node, newParentNode);

                if (canChangeParent)
                {
                    e.Effect = DragDropEffects.Move;
					this.destinationNodeParentCandidate = newParentNode;
				}
			}
        }

		private TreeListNode? GetDragDropParentNode(TreeListNode targetNode, DragInsertPosition dragInsertPosition)
		{
			TreeListNode? parentNode = targetNode;

			if (dragInsertPosition == DragInsertPosition.Before || dragInsertPosition == DragInsertPosition.After)
				parentNode = targetNode?.ParentNode;

			return parentNode;
		}

		private void treeList_DragDrop(object sender, DragEventArgs e)
        {
			DXDragEventArgs args = this.TreeList.GetDXDragEventArgs(e);

			if (args.DragInsertPosition == DragInsertPosition.None && this.destinationNodeParentCandidate == null) // In case when node can be moved to root (last under all elements in root position, if thex ecists) we need to manualy move node.
				this.TreeList?.MoveNode(args.Node, destinationNode: null);                                         // TreeList AfterDropNode event is not fired

			this.GraphController.DragDrop(args.Node, this.destinationNodeParentCandidate);
		}

		private void TreeList_AfterDropNode(object sender, AfterDropNodeEventArgs e)
		{
            //if (this.TreeList is null)
            //    return;
            
            if (e.Node is not null)
            {
				int nodeIndex = this.TreeList!.GetNodeIndex(e.Node);
				
                this.GraphController.AfterDropNode(e.Node, nodeIndex);
            }
		}


        private void treeList_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            //e.UseDefaultCursors = false;
        }

        private void treeList_BeforeCollapse(object sender, BeforeCollapseEventArgs e)
        {
            bool canCollapse = true;
            
            this.GraphController.BeforeNodeIsCollapsed(e.Node, ref canCollapse);

            e.CanCollapse = canCollapse;
            
            //e.CanCollapse = this.lastValidationNodeResult.IsValid;

            //if (!e.CanCollapse)
            //{
            //    // Allow collapsing nodes but prohibit colapsing any of the parent error nodes.

            //    GraphElement failedGraphElement = this.GetGraphElement(this.lastValidationFailedNode);
            //    GraphElement graphElementToBeExpanded = this.GetGraphElement(e.Node);

            //    e.CanCollapse = !failedGraphElement.HasAsParent(graphElementToBeExpanded);
            //}
        }

        private void TreeList_BeforeExpand(object sender, BeforeExpandEventArgs e)
        {
            this.GraphController.BeforeNodeIsExpanded(e.Node);
        }
        
        private void treeList_AfterExpand(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            this.GraphController.NodeIsExpanded(e.Node);
        }

        private void treeList_AfterCollapse(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            this.GraphController.NodeIsCollapsed(e.Node);
        }

        private void treeList_BeforeCheckNode(object sender, CheckNodeEventArgs e)
        {
            bool canCheck = e.CanCheck;
            bool checkValue = (e.State == CheckState.Checked);
            this.GraphController.BeforeNodeCheckStateIsChanged(e.Node, checkValue, ref canCheck);

            e.CanCheck = canCheck;
        }

		private void treeList_EndSorting(object sender, EventArgs e)
		{
			this.GraphController.SortedColumnCountIsChanged();
		}

        #endregion |   TreeList Events   |

        #region |   Dispose   |

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public void Dispose() => this.Dispose(true);

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        public void Dispose(bool disposing)
        {
            this.treeList?.Dispose();
        }

        #endregion |   Dispose   |
    }
}
