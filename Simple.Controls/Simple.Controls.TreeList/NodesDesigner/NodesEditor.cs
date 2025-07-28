//#if IncludeNodesEditorCode 

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.IO;
using System.Drawing;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Utils;
using DevExpress.XtraEditors;

namespace Simple.Controls.TreeList.Designer
{
    public class NodesEditor : UITypeEditor
    {
        private IWindowsFormsEditorService edSvc = null;
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object objValue)
        {
            if (context != null && context.Instance != null && provider != null)
            {
                edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (edSvc != null)
                {
                    try
                    {
                        SimpleTreeListNodesEditor editor = new SimpleTreeListNodesEditor(objValue);
                        if (!editor.TreeList.IsUnboundMode)
                        {
                            XtraMessageBox.Show("The DataSource property is not null...\r\nPlease set it to '(none)' before editing the Nodes collection.", "Warning");
                        }
                        else
                        {
                            if (edSvc.ShowDialog(editor) == DialogResult.OK)
                            {
                                editor.TreeList.FireChanged();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        XtraMessageBox.Show(e.Message);
                    }
                }
            }
            return objValue;
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                return UITypeEditorEditStyle.Modal;
            }
            return base.GetEditStyle(context);
        }
    }
}

//#endif