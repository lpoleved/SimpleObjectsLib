//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Simple.Modeling;

//namespace Simple.Objects.Controls
//{
//	public class GraphColumn
//	{
//		private string name = String.Empty;
//		private string caption = String.Empty;
//		private BindingDataType dataType = BindingDataType.String;
//		private BindingEditorType editorType = BindingEditorType.TextEdit;

//		private SimpleGraphController graphController = null;
//		private bool isNew = true;


//		public GraphColumn()
//			: this(String.Empty)
//		{
//		}

//		public GraphColumn(string name)
//			: this(name, name)
//		{
//		}

//		public GraphColumn(string name, string caption)
//			: this(name, caption, BindingDataType.String)
//		{
//		}

//		public GraphColumn(IPropertyModel propertyModel)
//			: this(propertyModel.Name, propertyModel.Caption)
//		{
//		}

//		public GraphColumn(string name, string caption, BindingDataType dataType)
//			: this(name, caption, dataType, BindingEditorType.TextEdit)
//		{
//		}

//		public GraphColumn(string name, string caption, BindingDataType dataType, BindingEditorType editorType)
//		{
//			this.name = name;
//			this.caption = caption;
//			this.dataType = dataType;
//			this.editorType = editorType;
//		}

//		public string Name
//		{
//			get { return this.name; }
//			set
//			{
//				if (this.IsNew)
//				{
//					this.name = value;
//				}
//				else
//				{
//					throw new ArgumentException("Graph column name is key and cannot be changed.");
//				}
//			}
//		}

//		public string Caption
//		{
//			get { return this.caption; }
//			set
//			{
//				this.caption = value;

//				if (!this.isNew && this.GraphController != null)
//				{
//					this.GraphController.GraphControlSetColumnCaption(this.Name, this.caption);
//				}
//			}
//		}

//		public BindingDataType DataType
//		{
//			get { return this.dataType; }
//			set
//			{
//				this.dataType = value;

//				if (!this.isNew && this.GraphController != null)
//				{
//					this.GraphController.GraphControlSetColumnDataType(this.Name, this.dataType);
//				}
//			}
//		}

//		public BindingEditorType EditorType
//		{
//			get { return this.editorType; }
//			set
//			{
//				this.editorType = value;

//				if (!this.isNew && this.GraphController != null)
//				{
//					this.GraphController.GraphControlSetColumnEditorType(this.Name, this.editorType);
//				}
//			}
//		}

//		public bool Enabled
//		{
//			get { return this.graphController.GetGraphColumnEnableProperty(this); }
//			set { this.graphController.SetGraphColumnEnableProperty(this, value); }
//		}

//		public int Width
//		{
//			get { return this.graphController.GraphControlGetColumnWidth(this.Name); }
//			set { this.graphController.GraphControlSetColumnWidth(this.Name, value); }
//		}

//		internal SimpleGraphController GraphController
//		{
//			get { return this.graphController; }
//			set 
//			{
//				if (value != this.graphController)
//				{
//					if (this.graphController != null)
//					{
//						this.graphController.GraphControlRemoveColumn(this.Name);
//					}

//					this.graphController = value;

//					if (this.graphController != null)
//					{
//						this.graphController.GraphControlAddColumn(this.Name, this.Caption, this.DataType, this.EditorType);
//					}
//				}
//			}
//		}

//		internal bool IsNew
//		{
//			get { return this.isNew; }
//			set { this.isNew = value; }
//		}
//	}
//}
