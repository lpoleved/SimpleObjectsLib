using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Simple.Objects;

namespace Simple.Objects.Controls
{
	[Serializable]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	public class EditPanelInfoAttribute : Attribute 
	{
		public EditPanelInfoAttribute(Type objectType, params int[] objectSubTypes)
		{
			if (!typeof(SimpleObject).IsAssignableFrom(objectType))
			{
				throw new ArgumentException("Edit Panel Object Type must be inherited from SimpleObject.");
			}
			else
			{
				this.ObjectType = objectType;
				this.ObjectSubTypes = objectSubTypes;
			}
		}

		public Type ObjectType { get; private set; }
		public int[] ObjectSubTypes { get; private set; }
	}
}
