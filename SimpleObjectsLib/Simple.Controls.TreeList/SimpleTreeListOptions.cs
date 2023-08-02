using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using DevExpress.XtraTreeList;

namespace Simple.Controls.TreeList
{
    public class SimpleTreeListOptions : BaseTreeListOptions
    {
        bool showFocusedFrame, useTabKey, editable;
        SimpleTreeList treeList;
        SimpleTreeLookAndFeelStyle lookAndFeel;
        SimpleTreeEditMode editMode;
		DragNodesMode dragNodesMode;
		//SimpleTreeDeleteOption deleteOption = SimpleTreeDeleteOption.DeleteObject;
		bool autoDisableEditingColumns = true;
        bool validationDisabled = false;
        //bool nodeCheckingMode = false;
        bool showHorzLine = false;
        //bool allowRecursiveNodeChecking = false;
        //Color defaultForeColor;

        public SimpleTreeListOptions(SimpleTreeList treeList)
        {
            this.treeList = treeList;
			//this.defaultForeColor = this.treeList.Appearance.Row.ForeColor;
			this.EditMode = SimpleTreeEditMode.SelectEdit;
            this.LookAndFeelStyle = SimpleTreeLookAndFeelStyle.ExplorerStyle;
        }

        [Category("Options"), Browsable(true)] //, EditorBrowsable(EditorBrowsableState.Always), DefaultValue(SimpleTreeLookAndFeelStyle.ExplorerStyle)]
        [Description("Gets or sets an advanced look and feel style options to controls some extra visibility features.")]
        public SimpleTreeLookAndFeelStyle LookAndFeelStyle
        {
            get { return this.lookAndFeel; }
            set
            {
                this.lookAndFeel = value;
                this.RefreshLookAndFeel();
            }
        }

        [Category("Options"), Browsable(true)] //, DefaultValue(SimpleTreeEditMode.SelectEdit)]
        [Description("Gets or sets an advanced editor mode style options to controls some extra visibility features.")]
        public SimpleTreeEditMode EditMode
        {
            get { return this.editMode; }
            set
            {
                this.editMode = value;
                this.RefreshEditMode();
            }
        }

        //public SimpleTreeDeleteOption DeleteOption 
        //{ 
        //    get { return this.deleteOption; } 
        //    set { this.deleteOption = value; } 
        //}

        [Category("Options"), Browsable(true), DefaultValue(true)]
        [Description("Gets or sets whether a node binded to a data and node column exists in binding policy, column is enabled or disabled automaticaly.")]
        public bool AutoDisableEditingColumns 
        { 
            get { return this.autoDisableEditingColumns; } 
            set { this.autoDisableEditingColumns = value; } 
        }

        //[Category("Options"), Browsable(true), DefaultValue(false)]
        //[Description("Gets or sets whether child nodes are automatically checked/unchecked when a parent node is checked/unchecked and vice versa.")]
        //public bool AllowRecursiveNodeChecking
        //{
        //    get { return this.allowRecursiveNodeChecking; }
        //    set { this.allowRecursiveNodeChecking = value; }
        //}

        [Category("Options"), Browsable(true), DefaultValue(false)]
        [Description("Gets or sets value whether an binded node validation is disabled.")]
        public bool ValidationDisabled 
        { 
            get { return this.validationDisabled; } 
            set { this.validationDisabled = value; } 
        }

        public void EnableValidation() 
        { 
            this.validationDisabled = false; 
        }

        public void DisableValidation() 
        { 
            this.validationDisabled = true; 
        }

        //public bool NodeCheckingMode 
        //{ 
        //    get { return this.nodeCheckingMode; } 
        //    set { this.nodeCheckingMode = value; } 
        //}

        internal void RefreshEditMode()
        {
            Color foreColor;

            switch (this.editMode)
            {
                case SimpleTreeEditMode.Standard:

                    this.treeList.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
                    this.treeList.OptionsBehavior.Editable = this.editable;
                    this.treeList.OptionsNavigation.UseTabKey = this.useTabKey;
                    this.treeList.OptionsView.ShowHorzLines = this.showHorzLine;
                    this.treeList.OptionsDragAndDrop.DragNodesMode = this.dragNodesMode;
                    //this.treeList.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Empty;
                    this.treeList.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Empty;
                    this.treeList.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Empty;
                    this.treeList.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Empty;
                    //this.treeList.Appearance.HideSelectionRow.Options.UseForeColor = false;
                    this.treeList.Appearance.FocusedRow.Options.UseForeColor = false;
                    this.treeList.Appearance.FocusedCell.Options.UseBackColor = false;
                    this.treeList.Appearance.FocusedCell.Options.UseForeColor = false;
                    this.treeList.Appearance.HideSelectionRow.Options.UseForeColor = true;

                    foreColor = (this.treeList.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhiteOnViewMode) ? Color.WhiteSmoke : Color.Black; // this.treeList.Appearance.Row.ForeColor;

					this.treeList.Appearance.HideSelectionRow.ForeColor = foreColor; // foreColor;

					break;

                case SimpleTreeEditMode.Select:

                    this.showFocusedFrame = (this.treeList.OptionsView.FocusRectStyle != DrawFocusRectStyle.None);
                    this.treeList.OptionsSelection.EnableAppearanceFocusedCell = false;
                    this.treeList.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
                    this.editable = this.treeList.OptionsBehavior.Editable;
                    this.dragNodesMode = this.treeList.OptionsDragAndDrop.DragNodesMode;
                    this.treeList.OptionsDragAndDrop.DragNodesMode = DragNodesMode.None;
                    this.treeList.OptionsBehavior.Editable = false;
                    this.treeList.Appearance.FocusedRow.Options.UseForeColor = true;
                    this.treeList.Appearance.FocusedCell.Options.UseForeColor = true;
                    //this.treeList.Appearance.FocusedRow.ForeColor = foreColor;
                    //this.treeList.Appearance.FocusedCell.ForeColor = foreColor;

                    foreColor = this.treeList.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhite ? Color.WhiteSmoke : Color.Black;

                    this.treeList.Appearance.FocusedRow.Options.UseForeColor = true;
                    this.treeList.Appearance.FocusedCell.Options.UseBackColor = true;
                    this.treeList.Appearance.FocusedCell.Options.UseForeColor = true;
                    this.treeList.Appearance.HideSelectionRow.Options.UseForeColor = true;

                    this.treeList.Appearance.FocusedRow.ForeColor = foreColor;
                    this.treeList.Appearance.FocusedCell.ForeColor = foreColor; // FromArgb(((System.Byte)(243)), ((System.Byte)(243)), ((System.Byte)(243)));
                    this.treeList.Appearance.HideSelectionRow.ForeColor = foreColor;

                    this.treeList.OptionsSelection.EnableAppearanceFocusedRow = true;
                    this.treeList.OptionsSelection.EnableAppearanceFocusedCell = true;

                    if (this.treeList.DoesCurrentStyleSkinRequireFocusedNodeFontInversionWhenTreeListIsNotFocused)
                        this.treeList.Appearance.HideSelectionRow.ForeColor = (foreColor == Color.Black) ? Color.WhiteSmoke : Color.Black; // Color.Black;

                    break;

                case SimpleTreeEditMode.SelectEdit:

                    //this.DeleteOption = SimpleTreeDeleteOption.DeleteObject;
                    this.showFocusedFrame = this.treeList.OptionsView.FocusRectStyle != DrawFocusRectStyle.None;
                    //this.treeList.OptionsSelection.EnableAppearanceFocusedCell = false;
                    this.treeList.OptionsView.FocusRectStyle = DrawFocusRectStyle.None;
                    this.treeList.OptionsDragAndDrop.DragNodesMode = this.dragNodesMode;
                    this.useTabKey = this.treeList.OptionsNavigation.UseTabKey;
                    this.treeList.OptionsNavigation.UseTabKey = true;
                    this.editable = this.treeList.OptionsBehavior.Editable;
                    this.treeList.OptionsBehavior.Editable = true;
                    this.treeList.Appearance.FocusedCell.BackColor = this.treeList.Appearance.Row.BackColor;

                    foreColor = this.treeList.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhite ? Color.WhiteSmoke : Color.Black;

                    this.treeList.Appearance.FocusedRow.Options.UseForeColor = true;
                    this.treeList.Appearance.FocusedCell.Options.UseBackColor = true;
                    this.treeList.Appearance.FocusedCell.Options.UseForeColor = true;
                    this.treeList.Appearance.HideSelectionRow.Options.UseForeColor = true;

                    this.treeList.Appearance.FocusedRow.ForeColor = foreColor;
                    this.treeList.Appearance.FocusedCell.ForeColor = foreColor; // FromArgb(((System.Byte)(243)), ((System.Byte)(243)), ((System.Byte)(243)));
                    this.treeList.Appearance.HideSelectionRow.ForeColor = foreColor;

                    this.treeList.OptionsSelection.EnableAppearanceFocusedRow = true;
                    this.treeList.OptionsSelection.EnableAppearanceFocusedCell = true;

                    if (this.treeList.DoesCurrentStyleSkinRequireFocusedNodeFontInversionWhenTreeListIsNotFocused)
                        this.treeList.Appearance.HideSelectionRow.ForeColor = (foreColor == Color.Black) ? Color.WhiteSmoke : Color.Black; // Color.Black;

                    //if (this.treeList.DoesCurrentStyleSkinRequireFocusedNodeFontInversionWhenTreeListIsNotFocused)
                    //    this.treeList.Appearance.HideSelectionRow.ForeColor = (foreColor == Color.Black) ? Color.WhiteSmoke : Color.Black; // Color.Black;

                    //this.treeList.Appearance.FocusedRow.ForeColor = foreColor;
                    //this.treeList.Appearance.FocusedCell.ForeColor = foreColor;

                    //this.treeList.Appearance.FocusedRow.Options.UseForeColor = true;
                    //this.treeList.Appearance.FocusedCell.Options.UseForeColor = true;

                    break;

                case SimpleTreeEditMode.Edit:

                    //this.DeleteOption = SimpleTreeDeleteOption.DeleteObject;
                    this.showFocusedFrame = false;
                    //this.treeList.OptionsSelection.EnableAppearanceFocusedCell = false;
                    this.treeList.OptionsView.FocusRectStyle = DrawFocusRectStyle.None;
                    this.treeList.OptionsDragAndDrop.DragNodesMode = this.dragNodesMode;
                    this.useTabKey = this.treeList.OptionsNavigation.UseTabKey;
                    this.treeList.OptionsNavigation.UseTabKey = true;
                    this.editable = this.treeList.OptionsBehavior.Editable;
                    this.treeList.OptionsBehavior.Editable = true;
                    //this.treeList.Appearance.FocusedCell.BackColor = this.treeList.Appearance.Row.BackColor;

                    this.treeList.OptionsSelection.EnableAppearanceFocusedRow = false; // No Blue focused cell back color
                    this.treeList.OptionsSelection.EnableAppearanceFocusedCell = false;

                    this.treeList.Appearance.FocusedRow.Options.UseForeColor = false;
                    this.treeList.Appearance.FocusedCell.Options.UseBackColor = false;
                    this.treeList.Appearance.FocusedCell.Options.UseForeColor = false;
                    this.treeList.Appearance.HideSelectionRow.Options.UseForeColor = true;

					foreColor = (!this.treeList.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhite) ? Color.WhiteSmoke : this.treeList.Appearance.Row.ForeColor;

					//if (this.treeList.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhiteOnViewMode) //&& !this.treeList.DoesCurrentStyleSkinRequireFocusedNodeFontInversionWhenTreeListIsNotFocused)
					//	foreColor = Color.WhiteSmoke;

					//this.treeList.Appearance.FocusedRow.ForeColor = foreColor;
					//this.treeList.Appearance.FocusedCell.ForeColor = foreColor; // FromArgb(((System.Byte)(243)), ((System.Byte)(243)), ((System.Byte)(243)));
					this.treeList.Appearance.HideSelectionRow.ForeColor = foreColor; // foreColor;



					//this.treeList.Appearance.FocusedRow.ForeColor = foreColor;
					//this.treeList.Appearance.FocusedCell.ForeColor = foreColor;

					//this.treeList.Appearance.FocusedRow.Options.UseForeColor = true;
					//this.treeList.Appearance.FocusedCell.Options.UseForeColor = true;

					break;

                case SimpleTreeEditMode.ViewOnly:

                    this.showFocusedFrame = this.treeList.OptionsView.FocusRectStyle != DrawFocusRectStyle.None;
                    this.treeList.OptionsView.FocusRectStyle = DrawFocusRectStyle.None;
                    this.editable = this.treeList.OptionsBehavior.Editable;
                    this.treeList.OptionsBehavior.Editable = false;
                    this.dragNodesMode = this.treeList.OptionsDragAndDrop.DragNodesMode;
                    this.treeList.OptionsDragAndDrop.DragNodesMode = DragNodesMode.None;
                    this.treeList.OptionsSelection.EnableAppearanceFocusedRow = false;
                    this.treeList.OptionsSelection.EnableAppearanceFocusedCell = false;

                    //this.treeList.OptionsSelection.InvertSelection = true;
                    //this.treeList.OptionsBehavior.ImmediateEditor = false;
                    //this.treeList.OptionsBehavior.KeepSelectedOnClick = false;
                    //this.treeList.OptionsBehavior.ShowEditorOnMouseUp = false;
                    //this.treeList.OptionsBehavior.ImmediateEditor = false;

                    //this.treeList.OptionsSelection.EnableAppearanceFocusedCell = false;
                    //this.treeList.OptionsSelection.EnableAppearanceFocusedRow = false;
                    this.treeList.DisableValidation();

                    this.treeList.Appearance.HideSelectionRow.Options.UseForeColor = false;
                    this.treeList.Appearance.FocusedRow.Options.UseForeColor = false;
                    this.treeList.Appearance.FocusedCell.Options.UseBackColor = false;
                    this.treeList.Appearance.FocusedCell.Options.UseForeColor = false;

                    foreColor = (this.treeList.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhiteOnViewMode) ? Color.WhiteSmoke : Color.Black; // this.treeList.Appearance.Row.ForeColor;

					//if (this.treeList.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhiteOnViewMode) //&& !this.treeList.DoesCurrentStyleSkinRequireFocusedNodeFontInversionWhenTreeListIsNotFocused)
					//	foreColor = Color.WhiteSmoke;

					this.treeList.Appearance.HideSelectionRow.ForeColor = foreColor;

					//this.treeList.Appearance.SelectedRow.Options.UseForeColor = true;
					//this.treeList.Appearance.HideSelectionRow.Options.UseForeColor = true;
					//this.treeList.Appearance.FocusedRow.Options.UseForeColor = true;
					//this.treeList.Appearance.FocusedCell.Options.UseForeColor = true;

					//Color backColor = this.treeList.BackColor; // this.treeList.BackColor // this.treeList.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhite ? Color.Black : Color.White;

					//if (this.treeList.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhiteOnViewMode) //&& !this.treeList.DoesCurrentStyleSkinRequireFocusedNodeFontInversionWhenTreeListIsNotFocused)
					//    foreColor = Color.WhiteSmoke;

					//this.treeList.Appearance.FocusedRow.ForeColor = foreColor;
					//this.treeList.Appearance.FocusedRow.BackColor = backColor;
					//this.treeList.Appearance.HideSelectionRow.ForeColor = foreColor;
					//this.treeList.Appearance.HideSelectionRow.BackColor = backColor;
					//this.treeList.Appearance.FocusedCell.ForeColor = foreColor; // System.Drawing.Color.Black; // FromArgb(((System.Byte)(243)), ((System.Byte)(243)), ((System.Byte)(243)));
					//this.treeList.Appearance.FocusedCell.BackColor = backColor; // System.Drawing.Color.Black; // FromArgb(((System.Byte)(243)), ((System.Byte)(243)), ((System.Byte)(243)));

					break;
            }
        }

        internal void RefreshLookAndFeel()
        {
            switch (this.lookAndFeel)
            {
                case SimpleTreeLookAndFeelStyle.Standard:

					this.treeList.OptionsView.ShowIndicator = true;
                    this.treeList.OptionsView.ShowVertLines = true;
                    this.treeList.OptionsView.ShowHorzLines = true;
                    this.treeList.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.Percent50;
                    this.treeList.OptionsSelection.EnableAppearanceFocusedRow = true;
                    this.treeList.OptionsSelection.EnableAppearanceFocusedCell = true;

                    break;

                case SimpleTreeLookAndFeelStyle.ExplorerStyle:

					this.treeList.OptionsView.ShowIndicator = false;
                    this.treeList.OptionsView.ShowVertLines = false;
                    this.treeList.OptionsView.ShowHorzLines = false;
                    this.treeList.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None;

					//if (this.treeList.DoesCurrentStyleSkinRequireFocusedNodeFontInversionWhenTreeListIsNotFocused)
					//{
					//	this.treeList.Appearance.FocusedCell.ForeColor = this.treeList.Appearance.Row.ForeColor;
					//	this.treeList.Appearance.HideSelectionRow.ForeColor = this.treeList.Appearance.Row.ForeColor;
					//}

					break;

                case SimpleTreeLookAndFeelStyle.ExcelStyle:

                    //Color foreColor = (!this.treeList.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhite && !this.treeList.DoesCurrentStyleSkinRequireFocusedNodeFontInversionWhenTreeListIsNotFocused) ? Color.WhiteSmoke : Color.Black;
                    //Color foreColor = (this.treeList.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhite) ? Color.WhiteSmoke : Color.Black;
                    //Color backColor = this.treeList.BackColor; // this.treeList.BackColor // this.treeList.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhite ? Color.Black : Color.White;

                    this.treeList.OptionsView.ShowIndicator = true;
                    this.treeList.OptionsView.ShowVertLines = false;
                    //this.treeList.OptionsView.ShowHorzLines = false;
                    this.treeList.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None;

					this.showFocusedFrame = (this.treeList.OptionsView.FocusRectStyle != DrawFocusRectStyle.None);
                    this.treeList.OptionsView.FocusRectStyle = DrawFocusRectStyle.None;
                    this.useTabKey = this.treeList.OptionsNavigation.UseTabKey;
                    this.treeList.OptionsNavigation.UseTabKey = true;
					//this.editable = this.treeList.OptionsBehavior.Editable;
					//this.treeList.OptionsBehavior.Editable = true;
                    this.showHorzLine = this.treeList.OptionsView.ShowHorzLines;
                    this.treeList.OptionsView.ShowHorzLines = true;

     //               this.treeList.OptionsSelection.EnableAppearanceFocusedRow = false; // No Blue focused cell back color
     //               this.treeList.OptionsSelection.EnableAppearanceFocusedCell = false;

     //               //this.treeList.Appearance.FocusedRow.Options.UseBackColor = true;
     //               this.treeList.Appearance.HideSelectionRow.Options.UseForeColor = this.treeList.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhite;
     //               //this.treeList.Appearance.HideSelectionRow.Options.UseBackColor = this.treeList.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhite;
     //               this.treeList.Appearance.FocusedRow.ForeColor = foreColor;
     //               //this.treeList.Appearance.FocusedCell.ForeColor = foreColor;

     //               if (this.treeList.DoesCurrentStyleSkinRequireFocusedNodeFontInversionWhenTreeListIsNotFocused)
					//{
     //                   this.treeList.Appearance.FocusedCell.ForeColor = foreColor; // this.treeList.Appearance.Row.ForeColor;
     //                   this.treeList.Appearance.HideSelectionRow.ForeColor = foreColor; // this.treeList.Appearance.Row.ForeColor; //= Color.Black
     //               }

					//this.SetAdvancedFocusedRowForeColor();


					//this.treeList.Appearance.HideSelectionRow.Options.UseForeColor = true;
					//this.treeList.Appearance.HideSelectionRow.Options.UseBackColor = true;

					//if (this.treeList.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhiteOnViewMode)
					//    foreColor = Color.WhiteSmoke;

					//if (this.treeList.DoesCurrentStyleSkinRequireFocusedNodeForeColorWhite)
					//this.treeList.Appearance.FocusedCell.ForeColor = this.treeList.ForeColor;

					//this.treeList.Appearance.FocusedRow.ForeColor = foreColor;
					//this.treeList.Appearance.FocusedRow.BackColor = backColor;
					//this.treeList.Appearance.FocusedCell.ForeColor = foreColor; // System.Drawing.Color.Black; // FromArgb(((System.Byte)(243)), ((System.Byte)(243)), ((System.Byte)(243)));
					//this.treeList.Appearance.FocusedCell.BackColor = backColor; // System.Drawing.Color.Black; // FromArgb(((System.Byte)(243)), ((System.Byte)(243)), ((System.Byte)(243)));
					//this.treeList.Appearance.HideSelectionRow.ForeColor = foreColor;
					//this.treeList.Appearance.HideSelectionRow.BackColor = backColor;


					//this.treeList.Appearance.FocusedRow.Options.UseForeColor = true;
					//this.treeList.Appearance.FocusedRow.Options.UseBackColor = true;
					//this.treeList.Appearance.FocusedCell.Options.UseBackColor = true;
					//this.treeList.Appearance.FocusedCell.Options.UseForeColor = false;
					//this.treeList.Appearance.HideSelectionRow.ForeColor = this.treeList.Appearance.Row.ForeColor; // Color.Black; // System.Drawing.Color.Empty;
					//this.treeList.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Empty;
					//this.treeList.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Empty;
					//this.treeList.Appearance.FocusedRow.BackColor = System.Drawing.Color.White;
					//this.treeList.Appearance.FocusedCell.ForeColor = System.Drawing.Color.WhiteSmoke; // FromArgb(((System.Byte)(243)), ((System.Byte)(243)), ((System.Byte)(243)));
					//this.treeList.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.WhiteSmoke;
					//this.treeList.Appearance.FocusedRow.ForeColor = System.Drawing.Color.WhiteSmoke;
					//this.treeList.Appearance.FocusedCell.ForeColor = System.Drawing.Color.WhiteSmoke; // FromArgb(((System.Byte)(243)), ((System.Byte)(243)), ((System.Byte)(243)));

					break;
            }

            this.SetAdvancedHideSelectionRowForeColor();
        }

        private void SetAdvancedHideSelectionRowForeColor()
        {
            string activeSkinName = this.treeList.LookAndFeel.ActiveSkinName;

            if (this.editMode == SimpleTreeEditMode.Select || this.editMode == SimpleTreeEditMode.SelectEdit)
            {
                if (activeSkinName == "Visual Studio 2013 Dark")
                {
                    this.treeList.Appearance.HideSelectionRow.ForeColor = Color.FromArgb(51, 153, 255); //Color.DodgerBlue;
                }
                else if (activeSkinName == "Metropolis Dark" || activeSkinName == "Office 2016 Black")
                {
                    this.treeList.Appearance.HideSelectionRow.ForeColor = Color.FromArgb(247, 138, 9); //Color.Orange;
                }
                //else if (activeSkinName == "Black")
                //{
                //    this.treeList.Appearance.FocusedCell.ForeColor = Color.Black;
                //}
            }
        }
    }

    public enum SimpleTreeEditMode
    {
        Standard,
        Select,
        SelectEdit,
        Edit,
        ViewOnly
    }

    public enum SimpleTreeLookAndFeelStyle
    {
        Standard,
        ExplorerStyle,
        ExcelStyle
    }

    //public enum SimpleTreeDeleteOption
    //{
    //    DeleteObject,
    //    DeleteNodeOnly
    //}
}
