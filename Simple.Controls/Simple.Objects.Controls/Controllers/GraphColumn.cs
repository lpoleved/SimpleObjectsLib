using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Modeling;
using Simple.Objects;

namespace Simple.Objects.Controls
{
    public class GraphColumn
    {
		private int index = 0;
		private string name = String.Empty;
        private string caption = String.Empty;
        private BindingDataType dataType = BindingDataType.String;
        private BindingEditorType editorType = BindingEditorType.TextEdit;

        private IGraphController? graphController = null;
        private bool isNew = true;


        public GraphColumn()
            : this(String.Empty)
        {
        }

		public GraphColumn(PropertyModel propertyModel)
			: this(propertyModel.PropertyName, propertyModel.Caption)
		{
		}

		public GraphColumn(PropertyModel propertyModel, string caption)
			: this(propertyModel.PropertyName, caption)
		{
		}

		public GraphColumn(string name)
            : this(name, name.InsertSpaceOnUpperChange())
        {
        }

        public GraphColumn(string name, string caption)
            : this(name, caption, BindingDataType.String)
        {
        }

        public GraphColumn(string name, string caption, BindingDataType dataType)
            : this(name, caption, dataType, BindingEditorType.TextEdit)
        {
        }

		public GraphColumn(string name, string caption, BindingDataType dataType, BindingEditorType editorType)
        {
            this.name = name;
            this.caption = caption;
            this.dataType = dataType;
            this.editorType = editorType;
        }

		public int Index
		{
			get { return this.index; }
			internal set { this.index = value; }
		}

        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.IsNew)
                {
                    this.name = value;
                }
                else
                {
                    throw new ArgumentException("Graph column name is key and cannot be changed.");
                }
            }
        }

        public string Caption
        {
            get { return this.caption; }
            set
            {
                this.caption = value;

                if (!this.isNew)
                    this.GraphController?.SetColumnCaption(this.Index, this.caption);
            }
        }

        public BindingDataType DataType
        {
            get { return this.dataType; }
            set
            {
                this.dataType = value;

                if (!this.isNew)
                    this.GraphController?.SetColumnDataType(this.Index, this.dataType);
            }
        }

        public BindingEditorType EditorType
        {
            get { return this.editorType; }
            set
            {
                this.editorType = value;

                if (!this.isNew)
                    this.GraphController?.SetColumnEditorType(this.Index, this.editorType);
            }
        }

        public bool Enabled
        {
            get 
            { 
                if (this.GraphController is not null)
                    return this.GraphController.GetColumnEnableProperty(this.Index);
                
                return false;
            }
            
            set { this.GraphController?.SetColumnEnableProperty(this.Index, value); }
        }

		public bool ReadOnly
		{
			get
			{
				if (this.GraphController is not null)
					return this.GraphController.GetColumnReadOnlyProperty(this.Index);

				return false;
			}

			set { this.GraphController?.SetColumnReadOnlyProperty(this.Index, value); }
		}

		public bool Visible
		{
			get 
            { 
                if (this.GraphController is not null)
                    return this.GraphController.GetColumnVisibleProperty(this.Index);

                return false;
            }
			
            set { this.GraphController?.SetColumnVisibleProperty(this.Index, value); }
		}
		
		public int Width
        {
			get 
            { 
                if (this.GraphController is not null)
                    return this.GraphController.GetColumnWidth(this.Index);

                return -1;
            }

			set { this.GraphController?.SetColumnWidth(this.Index, value); }
        }

		public bool ShowInCustomizationForm
		{
			get 
            { 
                if (this.GraphController is not null)
                    return this.GraphController.GetColumnShowInCustomizationFormProperty(this.Index);

                return false;
            }
			
            set { this.GraphController?.SetColumnShowInCustomizationFormProperty(this.Index, value); }
		}

        internal IGraphController? GraphController
        {
            get { return this.graphController; }
            set 
            {
                if (value != this.graphController)
                {
                    if (this.graphController != null)
						this.graphController.RemoveColumn(this.Index);

                    this.graphController = value;

					if (this.graphController != null)
					{
						int columnIndex = this.graphController.AddColumn(this.Name, this.Caption, this.DataType, this.EditorType);

						if (columnIndex >= 0)
							this.index = columnIndex;
					}
                }
            }
        }

        internal bool IsNew
        {
            get { return this.isNew; }
            set { this.isNew = value; }
        }
    }
}
