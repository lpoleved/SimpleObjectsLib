using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using Simple;
using Simple.Modeling;
using Simple.Objects;

namespace Simple.Objects.Controls
{
    public partial class SystemEditPanel : XtraUserControl, IBindingControl
	{
        #region |   Constructors and Initialization   |

		public SystemEditPanel()
        {
            InitializeComponent();
        }

        #endregion |   Constructors and Initialization   |

        #region |   Public Properties   |

        [Category("Simple"), DefaultValue(null), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object? BindingObject { get; private set; }

		public object? Context { get; set; }

        #endregion |   Public Properties   |

		#region |   Public Methods   |

		public void InitializeEditors()
		{
			this.OnInitializeEditors();
		}

		public void SetBindingObject(object? bindingObject)
		{
			if (bindingObject != this.BindingObject)
			{
				object? oldBindingObject = this.BindingObject;
				
				this.BindingObject = bindingObject;
				this.OnBindingObjectChange(bindingObject, oldBindingObject);
			}
		}

		public void RefreshBindingObject(object? requester = null)
		{
			this.OnRefreshBindingObject(requester);
		}

        #endregion |   Public Methods   |

        #region |   Protected Virtual Methods   |

		protected virtual void OnInitializeEditors()
		{
		}

        protected virtual void OnBindingObjectChange(object? bindingObject, object? oldBindingObject)
        {
			this.RefreshBindingObject();
		}

		protected virtual void OnRefreshBindingObject(object? requester)
		{
		}
		
		#endregion |   Protected Virtual Methods   |
	}
}
