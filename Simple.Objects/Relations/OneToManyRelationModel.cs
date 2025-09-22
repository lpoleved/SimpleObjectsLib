using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects
{
    public class OneToManyRelationModel : OneToOneOrManyRelationModel, IOneToManyRelationModel
    {

		public override ObjectRelationType RelationType => ObjectRelationType.OneToMany;
		public string ForeignCollectionName { get; set; }
		public string ForeignCollectionSummary { get; set; }

		//public Func<SimpleObject, Predicate<SimpleObject>> GetForeignCollectionSelectorByPrimaryObject { get; set; }

		protected override void OnSetForeignObjectType()
		{
			base.OnSetForeignObjectType();

			if (this.ForeignObjectType != null && String.IsNullOrEmpty(this.ForeignCollectionName))
				this.ForeignCollectionName = this.ForeignObjectType.Name + "s";
		}
	}

    public interface IOneToManyRelationModel : IOneToOneOrManyRelationModel
    {
		string ForeignCollectionName { get; }
		string ForeignCollectionSummary { get; }
		//Func<SimpleObject, Predicate<SimpleObject>> GetForeignCollectionSelectorByPrimaryObject { get; }
	}
}
