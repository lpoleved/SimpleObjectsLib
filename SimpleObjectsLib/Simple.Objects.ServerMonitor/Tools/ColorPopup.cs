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
	public class ColorPopup : XtraUserControl, IPopupColorPickEdit
	{
		XtraTabControl tabControl;
		Color fResultColor;
		BarItem itemFontColor;
		PopupControlContainer container;
		FormMain_OLD main;
		PopupColorBuilderEx builder;

		public ColorPopup(PopupControlContainer container, BarItem item, FormMain_OLD parent)
		{
			this.main = parent;
			this.container = container;
			this.builder = CreatePopupColorEditBuilder();
			this.itemFontColor = item;	
			this.fResultColor = Color.Empty;
			this.tabControl = CreateTabControl();
			this.tabControl.TabStop = false;
			this.tabControl.TabPages.AddRange(new XtraTabPage[] { new XtraTabPage(), new XtraTabPage(), new XtraTabPage()});
			for(int i = 0; i < tabControl.TabPages.Count; i++) SetTabPageProperties(i);
			tabControl.Dock = DockStyle.Fill;
			this.container.Controls.AddRange(new System.Windows.Forms.Control[] {tabControl});
			this.container.Size = CalcFormSize();
		}

		protected PopupColorBuilderEx CreatePopupColorEditBuilder()
		{
			return new DemoPopupColorBuilderEx(this);
		}

		public PopupColorBuilder PopupColorBuilder { get { return builder as PopupColorBuilder; } }

		public static void SetImageColor(BarItem item, Color clr)
		{
			int imIndex = item.ImageIndex;
			ImageCollection iml = item.Images as ImageCollection;
			Bitmap im = (Bitmap)iml.Images[imIndex];
			Graphics g = Graphics.FromImage(im);
			Rectangle r = new Rectangle(2, 12, 12, 3);
			g.FillRectangle(new SolidBrush(clr), r);
			if(imIndex == iml.Images.Count - 1) {
				iml.Images.RemoveAt(imIndex);
			}
            g.Dispose();
			iml.Images.Add(im);
			item.ImageIndex = iml.Images.Count - 1;
		}

		public Color ResultColor
		{ 
			get { return fResultColor; }

			set
			{ 
				fResultColor = value; 
				SetImageColor(itemFontColor, fResultColor);
			}
		}

		public Size CalcFormSize()
		{
			//Size size = ColorCellsControlViewInfo.BestSize;
			//size.Height = 113;
			//return tabControl.CalcSizeByPageClient(size);

			builder = CreatePopupColorEditBuilder();
			Size size = builder.CalcContentSize();
			size.Height = 113;
			return tabControl.CalcSizeByPageClient(size);
		}

		private XtraTabControl CreateTabControl() { return new XtraTabControl(); }
		private ColorListBox CreateColorListBox() { return new ColorListBox(); }

		private void SetTabPageProperties(int pageIndex)
		{
			XtraTabPage tabPage = this.tabControl.TabPages[pageIndex];
			ColorListBox colorBox = null;
			BaseControl control = null;

			switch (pageIndex)
			{
				case 0:
					tabPage.Text = Localizer.Active.GetLocalizedString(StringId.ColorTabCustom);
					control = new ColorCellsControl(null);
					DevExpress.XtraEditors.Repository.RepositoryItemColorEdit rItem = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
					rItem.ShowColorDialog = false;
					(control as ColorCellsControl).Properties = rItem;
					(control as ColorCellsControl).EnterColor += new EnterColorEventHandler(OnEnterColor);

					//control.Size = ColorCellsControlViewInfo.BestSize;
					builder = CreatePopupColorEditBuilder();
					control.Size = builder.CalcContentSize();

					break;
				
				case 1:
					tabPage.Text = Localizer.Active.GetLocalizedString(StringId.ColorTabWeb);
					colorBox = CreateColorListBox();
					colorBox.Items.AddRange(ColorListBoxViewInfo.WebColors);
					colorBox.EnterColor += new EnterColorEventHandler(OnEnterColor);
					control = colorBox;
					break;
				
				case 2:
					tabPage.Text = Localizer.Active.GetLocalizedString(StringId.ColorTabSystem);
					colorBox = CreateColorListBox();
					colorBox.Items.AddRange(ColorListBoxViewInfo.SystemColors);
					colorBox.EnterColor += new EnterColorEventHandler(OnEnterColor);
					control = colorBox;
					break;
			}

			control.Dock = DockStyle.Fill;
			control.BorderStyle = BorderStyles.NoBorder;
			control.LookAndFeel.ParentLookAndFeel = itemFontColor.Manager.GetController().LookAndFeel;
			tabPage.Controls.Add(control);
		}

		private void OnEnterColor(object sender, EnterColorEventArgs e)
		{
			ResultColor = e.Color;
			OnEnterColor(e.Color);
		}

		private void OnEnterColor(Color clr)
		{
			container.HidePopup();
			//main.CurrentRichTextBox.SelectionColor = clr;
		}
		private ColorCellsControl CellsControl
		{
			get { return ((ColorCellsControl)tabControl.TabPages[0].Controls[0]); }
		}

		private string CustomColorsName
		{
			get { return "CustomColors"; }
		}

		void IPopupColorEdit.ClosePopup()
		{
			container.HidePopup();
		}

		Color IPopupColorEdit.Color
		{
			get { return (Color)PopupColorBuilder.ResultValue; }
		}

		ColorListBox IPopupColorEdit.CreateColorListBox()
		{
			return new ColorListBox();
		}

		object IPopupColorEdit.EditValue
		{
			get { return null; }
		}

		bool IPopupColorEdit.IsPopupOpen
		{
			get { return true; }
		}

		DevExpress.LookAndFeel.UserLookAndFeel IPopupColorEdit.LookAndFeel
		{
			get { return LookAndFeel; }
		}

		void IPopupColorEdit.OnColorChanged()
		{
			main.CurrentRichTextBox.SelectionColor = (Color)PopupColorBuilder.ResultValue;
		}

		DevExpress.XtraEditors.Repository.RepositoryItemColorEdit IPopupColorEdit.Properties
		{
			get { return new DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit(); }
		}

		ColorEditTabControlBase IPopupColorEdit.CreateTabControl()
		{
			return new DevExpress.XtraEditors.PopupColorPickEditForm.ColorPickEditTabControl(this) as ColorEditTabControlBase;
		}

		void IPopupColorPickEdit.ClosePopup(PopupCloseMode mode)
		{
			if (this.container != null)
				this.container.HidePopup();
		}

		ColorPickEditBase IPopupColorPickEdit.OwnerEdit
		{
			get { return null; }
		}
		
		void IPopupColorPickEdit.SetSelectedColorItem(ColorItem item)
		{
		}

		bool IPopupColorPickEdit.HasBorder { get { return false; } }

		IWin32Window IPopupColorPickEdit.OwnerWindow
		{
			get { return null; }
		}
	}

	public class DemoPopupColorBuilderEx : PopupColorBuilderEx
	{
		public DemoPopupColorBuilderEx(IPopupColorPickEdit owner)
			: base(owner)
		{
		}

		protected override ColorEditTabControlBase CreateTabControl()
		{
			ColorEditTabControlBase tab = base.CreateTabControl();
			
			tab.Dock = DockStyle.Fill;
			
			return tab;
		}
	}
}
