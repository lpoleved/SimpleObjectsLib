using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Simple.Objects;

namespace Simple.Objects.Controls
{
    // TODO: In furure Image issue revidation , if will be necessery
    public class ImageManager
    {
        //private SimpleObjectManager objectManager = null;
        private static ImageManager instance = null;
        private static object lockObjectInstance = new object();

        public static string ImageSufixExpanded = "Expanded";

        public static ImageManager Instance
        {
            get { return GetInstance<ImageManager>(); }
        }

     //   public SimpleObjectManager ObjectManager
     //   {
     //       get { return this.objectManager; }

     //       set
     //       {
     //           if (this.objectManager != null)
     //           {
					////this.objectManager.PropertyValueChange -= new SimpleObjects.ChangePropertyValueSimpleObjectRequesterEventHandler(objectManager_PropertyValueChange);
     //           }

     //           this.objectManager = value;

     //           if (this.objectManager != null)
     //           {
					////this.objectManager.PropertyValueChange += new SimpleObjects.ChangePropertyValueSimpleObjectRequesterEventHandler(objectManager_PropertyValueChange);
     //           }
     //       }
     //   }

		//private void objectManager_PropertyValueChange(object sender, SimpleObjects.ChangePropertyValueSimpleObjectRequesterEventArgs e)
		//{
		//	if (e.PropertyName == SimpleObjectModel.PropertyModel.Status.Name)
		//	{

		//	}
		//}
        
        protected static T GetInstance<T>() where T : ImageManager, new()
        {
            lock (lockObjectInstance)
            {
                if (instance == null)
                {
                    instance = new T();
                }
            }

            return instance as T;
        }
    }
}
