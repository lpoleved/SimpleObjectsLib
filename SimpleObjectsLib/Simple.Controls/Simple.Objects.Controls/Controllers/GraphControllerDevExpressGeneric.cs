using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Simple;
using DevExpress.XtraBars;

namespace Simple.Objects.Controls
{
	public class GraphControllerDevExpress<TGraphKey, TManyToManyRelationKey> : GraphControllerDevExpress
	{
		[Category("Graph"), DefaultValue(0)]
		public new TGraphKey GraphKey
		{
			get { return Conversion.TryChangeType<TGraphKey>(base.GraphKey); }
			set { base.GraphKey = (value is not null) ? Conversion.TryChangeType<int>(value) : 0; }
		}

		[Category("Group Membership"), DefaultValue(0)]
		public new TManyToManyRelationKey ManyToManyRelationKey
		{
			get { return Conversion.TryChangeType<TManyToManyRelationKey>(base.ManyToManyRelationKey); }
			set { base.ManyToManyRelationKey = (value is not null) ? Conversion.TryChangeType<int>(value) : 0; }
		}

		public void SetGoToGraphButton(TGraphKey graphKey, BarItem button)
		{
			if (graphKey is not null)
				base.SetGoToGraphButton(Conversion.TryChangeType<int>(graphKey), button);
		}
	}
}
