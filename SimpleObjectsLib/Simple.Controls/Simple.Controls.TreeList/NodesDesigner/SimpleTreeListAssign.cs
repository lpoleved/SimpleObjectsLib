//#if IncludeNodesEditorCode 

using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using System.Reflection;

namespace Simple.Controls.TreeList.Designer
{
    public class SimpleTreeListAssign
    {
        public static SimpleTreeList CreateTreeListControlAssign(SimpleTreeList editingTreeList)
        {
            SimpleTreeList treeList = Assembly.GetAssembly(editingTreeList.GetType()).CreateInstance(editingTreeList.GetType().ToString()) as SimpleTreeList;
            treeList.Dock = DockStyle.Fill;
            treeList.OptionsBehavior.PopulateServiceColumns = true;
            TreeListControlAssign(editingTreeList, treeList, true, true);
            return treeList;
        }

        public static void TreeListControlAssign(SimpleTreeList editingTreeList, SimpleTreeList treeList, bool setRepository, bool setStyles)
        {
            if (setRepository)
            {
                AssignEditors(editingTreeList, treeList);
                treeList.ExternalRepository = editingTreeList.ExternalRepository;
            }
            treeList.LookAndFeel.Assign(editingTreeList.LookAndFeel);
            treeList.SelectImageList = editingTreeList.SelectImageList;
            treeList.StateImageList = editingTreeList.StateImageList;
            treeList.CheckBoxImageList = editingTreeList.CheckBoxImageList;

            treeList.CustomCheckBoxDraw = editingTreeList.CustomCheckBoxDraw;
            treeList.CustomNodeImagesDraw = editingTreeList.CustomNodeImagesDraw;
            //treeList.ShowExtraChecked = editingTreeList.ShowExtraChecked;
            treeList.UseExpandedImages = editingTreeList.UseExpandedImages;

            treeList.Appearance.Assign(editingTreeList.Appearance);
            System.IO.MemoryStream str = new System.IO.MemoryStream();
            editingTreeList.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            treeList.RestoreLayoutFromStream(str);
            str.Close();
        }
        
        private static void AssignEditors(SimpleTreeList sourceTreeList, SimpleTreeList treeList)
        {
            foreach (DevExpress.XtraEditors.Repository.RepositoryItem item in sourceTreeList.RepositoryItems)
                treeList.RepositoryItems.Add(item.Clone() as DevExpress.XtraEditors.Repository.RepositoryItem);
        }
    }
}
//#endif