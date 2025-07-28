using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Collections;
using Simple;
using Simple.Modeling;

namespace Simple.Objects
{
	public abstract class EasyObjectModel<TObject, TModel, TObjectProperty> : EasyObjectModel, IEasyObjectModel
        where TObject : EasyObject
		where TModel : EasyObjectModel<TObject, TModel, TObjectProperty>, new()
		where TObjectProperty : PropertyModelBase, new()
	{
        //private ObjectImageModel imageModel = null;
        private static TModel instance = null;
		private static object lockObject = new object();

		public static readonly TModel Default = new TModel();
		public static readonly TObjectProperty ObjectProperty = new TObjectProperty();

		public EasyObjectModel()
        {
            this.ObjectType = typeof(TObject);
            this.Caption = typeof(TObject).Name.InsertSpaceOnUpperChange();

			if (instance == null)
				instance = this as TModel;
        }

        public static TModel Instance 
		{
			get
			{
				lock (lockObject)
				{
					if (instance == null)
						instance = new TModel();
				}

				return instance;
			}
		}

        PropertyModelCollection<PropertyModel> IEasyObjectModel.PropertyModels
		{
			get { return ObjectProperty.GetCollection(); }
		}

		//IImageModel IEasyObjectModel.ImageModel
		//{
		//	get { return this.ImageModel; }
		//}
	}

    public abstract class EasyObjectModel : ModelElement, IEasyObjectModel
    {
        public EasyObjectModel()
        {
            foreach (var item in this.GetPropertyModelCollection())
                if (item is PropertyModel)
                    (item as PropertyModel).Owner = this;
        }

        //public string Caption2
        //{ get; set; }
        public Type ObjectType { get; set; }

        //public ObjectImageModel ImageModel
        //{
        //    get { return this.imageModel; }

        //    protected set
        //    {
        //        this.imageModel = value;

        //        if (this.imageModel != null)
        //            this.imageModel.Owner = this;
        //    }
        //}

        protected abstract PropertyModelCollection<PropertyModel> GetPropertyModelCollection();

        PropertyModelCollection<PropertyModel> IEasyObjectModel.PropertyModels
        {
            get { return this.GetPropertyModelCollection(); }
        }

        //IImageModel IEasyObjectModel.ImageModel
        //{
        //    get { return this.ImageModel; }
        //}
    }

    public interface IEasyObjectModel : IModelElement
    {
        //string Caption2 { get; set; }
        Type ObjectType { get; set; }
        PropertyModelCollection<PropertyModel> PropertyModels { get; }
    }
}
