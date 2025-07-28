using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Modeling;

namespace Simple.Objects
{
	//[DatastoreTable(DatastoreTableInfo.Folders)]
	public class FolderModel : SimpleObjectModel<Folder, FolderPropertyModel, FolderModel>, ISimpleObjectModel
	{
		public FolderModel() : base(SystemTablesBase.Folders)
		{
			this.UpdateValidationRules.Add(new ValidationRuleExistance(PropertyModel.Name));
			this.UpdateValidationRules.Add(new ValidationRuleUnique(PropertyModel.Name));
		}
	}

	public class FolderPropertyModel : SimpleObjectPropertyModelBase
	{
		public PM Name		  = new PM<string>(1);
		public PM Description = new PM<string>(2);
	}
}
