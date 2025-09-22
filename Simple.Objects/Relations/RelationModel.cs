using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Modeling;

namespace Simple.Objects
{
    public abstract class RelationModel : ModelElement, IRelationModel, IEquatable<IRelationModel>
	{
        public RelationModel()
        {
            //this.RelationKey = -1;
			this.CanBeNull = false;
			this.AutoGenerate = true;
        }

		public string DefinitionFieldName { get; set; } = String.Empty;
		public Type DefinitionObjectClassType { get; set; } = typeof(RelationPolicyModelBase);
		public int RelationKey { get; set; }
		public bool CanBeNull { get; set; }
		public bool AutoGenerate { get; set; }

		public string GetObjectTypeName(Type propertyType)
		{
			string result = propertyType.GetName();

			if (this.CanBeNull)
				result += "?";

			return result;
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
		bool IEquatable<IRelationModel>.Equals(IRelationModel? other)
		{
			return this.RelationKey == other?.RelationKey;
		}
	}

	public interface IRelationModel : IModelElement, IEquatable<IRelationModel>
	{
		int RelationKey { get; }
		bool CanBeNull { get; }
    }
}
