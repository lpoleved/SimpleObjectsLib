using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using DevExpress.XtraEditors;
//using DevExpress.XtraBars.Ribbon;
using Simple;
using Simple.Objects;

namespace Simple.Objects.Controls
{
	public class GroupUserControl : GroupUserControl<Type>
	{
	}

	public partial class GroupUserControl<TKey> : UserControl where TKey : class
    {
        #region |   Private Members   |
        
        //private object bindingObject = null;
		//private Dictionary<TKey, UserControl> editPanelsByBindingObjectType = new Dictionary<TKey, UserControl>();
		private Dictionary<TKey, UserControl> userControlsByKey = new Dictionary<TKey, UserControl>();
		private Dictionary<TKey, Type> userControlTypesByKey = new Dictionary<TKey, Type>();
        private UserControl? activeUserControl = null;
        private Type? activeUserControlType = null;

        #endregion |   Private Members   |

        #region |   Constructors and Initialization   |

		public GroupUserControl()
        {
            InitializeComponent();
			//this.SetActivePropertyPanel(typeof(EmptyUserControl));
        }

		#endregion |   Constructors and Initialization   |

		#region |   Events   |

		public event GetEditPanelTypeEventHandler? GetUserControlType;

		#endregion |   Events   |

		#region |   Public Properties   |

		#endregion |   Public Properties   |

		#region |   Public Methods   |

		public void SetUserControlDefinition(TKey key, Type userControlType)
		{
			if (!userControlType.IsSubclassOf(typeof(SystemEditPanel)))
				throw new ArgumentException("Edit panel type must be inherited from PropertyPanel class.");
			
			this.userControlTypesByKey[key] = userControlType;
		}
		
		public void SetUserControl(TKey key)
		{
			UserControl? userControl = null;
			Type? userControlType = null;

			if (this.GetUserControlType != null)
			{
				userControlType = this.GetUserControlType(key);
			}
			else if (!this.userControlTypesByKey.TryGetValue(key, out userControlType))
			{ 
				userControlType = typeof(EmptyUserControl);
			}

			if (userControlType is not null && userControlType != this.activeUserControlType)
			{
				if (!this.userControlsByKey.ContainsKey(key))
				{
					userControl = Activator.CreateInstance(userControlType) as UserControl;
					this.userControlTypesByKey.Add(key, userControlType);

					if (userControl is not null)
						userControl.Parent = this;
				}
			}

			if (this.GetUserControlType != null)
			{
				userControlType = this.GetUserControlType(key);

				if (userControlType == null)
					userControlType = typeof(EmptyUserControl);

				if (!this.userControlsByKey.TryGetValue(key, out userControl))
				{
					userControl = Activator.CreateInstance(userControlType) as UserControl;
					this.userControlTypesByKey.Add(key, userControlType);

					userControl.Parent = this;
					userControl.Dock = DockStyle.Fill;
				}

				userControl.Dock = DockStyle.Fill;
				userControl.BringToFront();
				userControl.Visible = true;

				if (this.activeUserControl != null)
				{
					this.activeUserControl.Visible = false;
					this.activeUserControl.Dock = DockStyle.None;
				}

				this.activeUserControlType = userControlType;
				this.activeUserControl = userControl;
			}
		}

        #endregion |   Public Methods   |

        #region |   Private Methods   |


        #endregion |   Private Methods   |
	}

	#region |   Delegates   |

	public delegate Type GetUserControlTypeEventHandler<TKey>(TKey key);

	#endregion |   Delegates   |
}
