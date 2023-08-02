using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Modeling;

namespace Simple.Objects
{
    public class ManyToManyRelationModel : RelationModel, IManyToManyRelationModel
    {
		private Type firstObjectType, secondObjectType;

		public ManyToManyRelationModel()
		{
			this.FirstObjectCollectionModifier = AccessModifier.Public;
			this.SecondObjectCollectionModifier = AccessModifier.Public;
		}

		public Type FirstObjectType 
		{
			get { return this.firstObjectType; }
			set
			{
				this.firstObjectType = value;

				if (value != null && String.IsNullOrEmpty(this.FirstObjectCollectionName))
					this.FirstObjectCollectionName = value.Name + "s";
			}
		}

        public Type SecondObjectType 
		{
			get { return this.secondObjectType; }
			set
			{
				this.secondObjectType = value;

				if (value != null && String.IsNullOrEmpty(this.SecondObjectCollectionName))
					this.SecondObjectCollectionName = value.Name + "s";
			}
		}

		public string FirstObjectCollectionName { get; set; }
		public string FirstObjectCollectionSummary { get; set; }
		public AccessModifier FirstObjectCollectionModifier { get; set; }
		public string SecondObjectCollectionName { get; set; }
		public string SecondObjectCollectionSummary { get; set; }
		public AccessModifier SecondObjectCollectionModifier { get; set; }
	}

    public interface IManyToManyRelationModel : IRelationModel
    {
        Type FirstObjectType { get; }
		string FirstObjectCollectionName { get; }
		string FirstObjectCollectionSummary { get; }
		Type SecondObjectType { get; }
		string SecondObjectCollectionName { get; }

		string SecondObjectCollectionSummary { get; }
	}
}
