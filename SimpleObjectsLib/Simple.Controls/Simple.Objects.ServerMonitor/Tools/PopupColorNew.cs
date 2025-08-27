using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraTab;
using DevExpress.XtraBars;

namespace Simple.Objects.ServerMonitor
{
    public class PopupColorNew : XtraUserControl, IPopupColorPickEdit {
		private PopupControlContainer _container;
		private FormMainOld main;

		public PopupColorNew(PopupControlContainer container, FormMainOld parent) {
            this._container = container;
            builder = CreatePopupColorEditBuilder();
            container.Controls.Add(builder.TabControl);
            Size _size = builder.CalcContentSize();
            this._container.Size = new Size(_size.Width + 3, _size.Height);
            this.main = parent;
        }

        protected PopupColorBuilderEx CreatePopupColorEditBuilder() {
            return new DemoPopupColorBuilderEx(this);
        }
        void IPopupColorEdit.ClosePopup() {
            _container.HidePopup();
        }
        Color IPopupColorEdit.Color {
            get { return (Color)PopupColorBuilder.ResultValue; }
        }
        ColorListBox IPopupColorEdit.CreateColorListBox() {
            return new ColorListBox();
        }
        object IPopupColorEdit.EditValue {
            get { return null; }
        }
        bool IPopupColorEdit.IsPopupOpen {
            get { return true; }
        }
        DevExpress.LookAndFeel.UserLookAndFeel IPopupColorEdit.LookAndFeel {
            get { return LookAndFeel; }
        }
        void IPopupColorEdit.OnColorChanged() {
            //main.CurrentRichTextBox.SelectionColor = (Color)PopupColorBuilder.ResultValue;
        }
        DevExpress.XtraEditors.Repository.RepositoryItemColorEdit IPopupColorEdit.Properties {
            get { return new DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit(); }
        }
        PopupColorBuilderEx builder;
        public PopupColorBuilder PopupColorBuilder { get { return builder as PopupColorBuilder; } }

        ColorEditTabControlBase IPopupColorEdit.CreateTabControl() {
            return new PopupColorPickEditForm.ColorPickEditTabControl(this) as ColorEditTabControlBase;
        }
        IWin32Window IPopupColorPickEdit.OwnerWindow {
            get { return null; }
        }
        void IPopupColorPickEdit.ClosePopup(PopupCloseMode mode) {
            if(this._container != null) this._container.HidePopup();
        }
        ColorPickEditBase IPopupColorPickEdit.OwnerEdit {
            get { return null; }
        }
        void IPopupColorPickEdit.SetSelectedColorItem(ColorItem item) {
        }

		public void BeforeShowColorDialog()
		{
			throw new NotImplementedException();
		}

		public void AfterShowColorDialog()
		{
			throw new NotImplementedException();
		}

		bool IPopupColorPickEdit.HasBorder { get { return false; } }
    }
}
