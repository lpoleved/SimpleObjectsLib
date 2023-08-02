using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;

namespace Simple.Objects
{
    public class SimpleObjectValidationResult : ValidationResult
    {
		public static new SimpleObjectValidationResult DefaultSuccessResult = new SimpleObjectValidationResult(null, true, null, null, TransactionRequestAction.Save);

		public SimpleObjectValidationResult(SimpleObject target, bool passed, string message, ValidationResult failedRuleResult, TransactionRequestAction failedValidationType)
            : base(passed, message)
        {
            this.Target = target;
            this.FailedRuleResult = failedRuleResult;
            this.FailedValidationType = failedValidationType;
        }

        public SimpleObject Target { get; private set; }
        public ValidationResult FailedRuleResult { get; private set; }

        public TransactionRequestAction FailedValidationType { get; private set; }
        //public IPropertyModel ErrorPropertyModel
        //{
        //    get
        //    {
        //        if (this.FailedRuleResult != null && !this.FailedRuleResult.Passed)
        //            return this.FailedRuleResult.PropertyModel;

        //        return null;
        //    }
        //}

        public bool IsValidationErrorFormShown { get; set; }

        public new string Message
		{
			get
			{
				if (base.Message.IsNullOrWhiteSpace() && this.FailedRuleResult != null)
					return this.FailedRuleResult.Message;

				return base.Message;
			}
		}

        public static SimpleObjectValidationResult GetDefaultPassedResult(SimpleObject target)
        {
            return new SimpleObjectValidationResult(target, true, "Passed", null, TransactionRequestAction.Save);
        }

        public override string ToString()
        {
            return String.Format("{0} {1} -> {2}", (this.Passed ? "Passed" : "Failed"), this.Message, this.Target);
        }
    }
}
