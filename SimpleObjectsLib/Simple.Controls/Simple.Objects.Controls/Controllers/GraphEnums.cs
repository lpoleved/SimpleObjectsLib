using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects.Controls
{
    public enum GraphEditMode
    {
        Standard,
        Select,
        SelectEdit,
        Edit,
        ViewOnly
    }

    public enum GraphLookAndFeelStyle
    {
        Standard,
        ExplorerStyle,
        ExcelStyle
    }

    [Serializable]
    public enum BindingDataType
    {
        Object,
        String,
        Boolean,
        Int32,
        Decimal,
        Image
    }

    [Serializable]
    public enum BindingEditorType
    {
        Default,
        TextEdit,
        ComboEdit,
        DateEdit,
        CheckEdit
    }

    public enum SaveButtonOption
    {
        CommitChanges,
        SaveFocusedNode
    }
}
