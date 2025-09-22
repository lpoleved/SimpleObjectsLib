using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Modeling;

namespace Simple.Objects
{
	public class PropertyValidationResult : ValidationResult
	{
		public PropertyValidationResult(bool passed)
			: base(passed)
		{
		}

		public PropertyValidationResult(bool passed, string message)
			: base(passed, message)
		{
		}

		public PropertyValidationResult(bool passed, string message, IPropertyModel? errorPropertyModel)
			: base(passed, message)
		{
			this.ErrorPropertyModel = errorPropertyModel;
		}

		public IPropertyModel? ErrorPropertyModel { get; private set; }

		public static PropertyValidationResult GetDefaultSuccessResult(IPropertyModel? propertyModel)
		{
			return new PropertyValidationResult(true, "Passed", propertyModel);
		}
	}
}
