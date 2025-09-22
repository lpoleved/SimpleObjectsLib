using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects
{
    public class OneToOneRelationModel : OneToOneOrManyRelationModel, IOneToOneRelationModel
    {
		public string ForeignObjectName { get; set; }
		public string ForeignObjectSummary { get; set; }

		public override ObjectRelationType RelationType => ObjectRelationType.OneToOne;

		protected override void OnSetForeignObjectType()
		{
			base.OnSetForeignObjectType();

			if (this.ForeignObjectType != null && String.IsNullOrEmpty(this.ForeignObjectName))
				this.ForeignObjectName = this.ForeignObjectType.Name;
		}
    }

    public interface IOneToOneRelationModel : IOneToOneOrManyRelationModel
    {
		string ForeignObjectName { get; }
		string ForeignObjectSummary { get; }
	}
}
