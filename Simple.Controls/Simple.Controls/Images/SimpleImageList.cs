using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;

namespace Simple.Controls
{
    public partial class SimpleImageList : Component
    {

        public SimpleImageList()
        {
            InitializeComponent();
        }

        public SimpleImageList(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public ImageList ImageList 
        { 
            get 
            { 
                return imageList; 
            } 
        }

        public void Load(object resources)
        {
            foreach (PropertyInfo property in resources.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
            {
                object propertyValue = property.GetValue(resources, null);
                
                if (propertyValue is Bitmap)
                {
                    this.LoadItem((Bitmap)propertyValue, property.Name);
                }
            }
        }

        private void LoadItem(Bitmap bitmap, string bitmapName) 
        { 
            imageList.Images.Add(bitmapName, bitmap); 
        }
    }
}
