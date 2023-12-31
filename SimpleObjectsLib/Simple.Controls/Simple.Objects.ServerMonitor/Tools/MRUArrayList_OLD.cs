//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Drawing;
//using DevExpress.XtraEditors;
//using DevExpress.XtraBars.Ribbon;
//using System.Collections;
//using System.Windows.Forms;
//using System.IO;

//namespace Simple.Objects.ServerMonitor
//{
//    public class MRUArrayList : ArrayList
//	{
//        public static string MRUFileName = "RibbonMRUFiles.ini";
//        public static string MRUFolderName = "RibbonMRUFolders.ini";

//        Control container;
//        int maxRecentFiles = 9;
//        Image imgChecked, imgUncheked, glyph;
//        public event EventHandler LabelClicked;
//        bool indexedList;
//        bool showDescription;

//		public MRUArrayList(Control cont, Image iChecked, Image iUnchecked, Image glyph, bool indexedList, bool showDescription) : this(cont, iChecked, iUnchecked, glyph)
//		{
//            this.indexedList = indexedList;
//            this.showDescription = showDescription;
//        }

//        public MRUArrayList(Control cont, Image iChecked, Image iUnchecked, Image glyph) : this(cont, iChecked, iUnchecked)
//		{
//            this.glyph = glyph;
//        }

//        public MRUArrayList(Control cont, Image iChecked, Image iUnchecked)
//            : base()
//		{
//            this.indexedList = true;
//            this.imgChecked = iChecked;
//            this.imgUncheked = iUnchecked;
//            this.container = cont;
//        }

//        public void InsertElement(object value)
//		{
//            string[] names = value.ToString().Split(',');
//            string name = names[0];
//            bool checkedLabel = false;

//			if (names.Length > 1)
//				checkedLabel = names[1].ToLower().Equals("true");

//			foreach (Control c in container.Controls)
//			{
//                AppMenuFileLabel ml = c as AppMenuFileLabel;

//				if (ml == null)
//                    continue;

//				if (ml.Tag.Equals(name))
//				{
//                    checkedLabel = ml.Checked;
//                    base.Remove(name);
//                    ml.LabelClick -= new EventHandler(OnLabelClick);
//                    ml.Dispose();
                    
//                    break;
//                }
//            }

//            bool access = true;

//			if (base.Count >= maxRecentFiles)
//                access = RemoveLastElement();

//			if (access)
//			{
//                base.Insert(0, name);

//				AppMenuFileLabel ml = new AppMenuFileLabel();
//                container.Controls.Add(ml);
//                ml.BringToFront();
//                ml.Tag = name;

//				if (showDescription)
//				{
//					ml.Text = GetFileName(name);
//					ml.Description = name;
//				}
//				else
//				{
//					ml.Text = GetFileName(name);
//				}

//                ml.Glyph = glyph;
//                ml.Checked = checkedLabel;
//                ml.AutoHeight = true;
//                ml.Dock = DockStyle.Top;
//                ml.Image = imgUncheked;
//                ml.SelectedImage = imgChecked;
//                ml.LabelClick += new EventHandler(OnLabelClick);

//				if (indexedList)
//                    SetElementsRange();
//            }
//        }


//		public bool RemoveLastElement()
//		{
//            for(int i = 0; i < container.Controls.Count; i++)
//			{
//                AppMenuFileLabel ml = container.Controls[i] as AppMenuFileLabel;

//				if (!ml.Checked)
//				{
//                    base.Remove(ml.Tag);

//					ml.LabelClick -= new EventHandler(OnLabelClick);
//                    ml.Dispose();

//					return true;
//                }
//            }

//            return false;
//        }

//        public bool GetLabelChecked(string name)
//		{
//            foreach(AppMenuFileLabel ml in container.Controls)
//                if(ml.Tag.Equals(name))
//					return ml.Checked;

//            return false;
//        }

//        public void Init(string fileName, string defaultItem)
//		{
//            if(!System.IO.File.Exists(fileName))
//			{
//                InsertElement(defaultItem);
                
//                return;
//            }

//            System.IO.StreamReader sr = System.IO.File.OpenText(fileName);
//            container.SuspendLayout();

//			for (string s = sr.ReadLine(); s != null; s = sr.ReadLine())
//                this.InsertElement(s);

//			sr.Close();
//            container.ResumeLayout();
//        }

//		private string GetFileName(object obj)
//		{
//			FileInfo fi = new FileInfo(obj.ToString());
//			return fi.Name;
//		}

//		private void SetElementsRange()
//		{
//			int i = 0;

//			foreach (Control c in container.Controls)
//			{
//				AppMenuFileLabel ml = c as AppMenuFileLabel;

//				if (ml == null)
//					continue;

//				ml.Caption = string.Format("&{0}", container.Controls.Count - i);
//				i++;
//			}
//		}

//		private void OnLabelClick(object sender, EventArgs e)
//		{
//			if (this.LabelClicked != null)
//				this.LabelClicked(((AppMenuFileLabel)sender).Tag.ToString(), e);
//		}
//	}
//}
