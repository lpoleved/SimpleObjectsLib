using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Modeling;

namespace Simple.Objects
{
    public class EasyObjectValidationResult : ValidationResult
    {
        public EasyObjectValidationResult(EasyObject target, bool passed, string message, ValidationResult failedRuleResult)
            : base(passed, message)
        {
            this.Target = target;
            this.FailedRuleResult = failedRuleResult;
        }

        public EasyObject Target { get; private set; }
        public ValidationResult FailedRuleResult { get; private set; } 
		
		public static EasyObjectValidationResult GetDefaultPassedResult(EasyObject target)
        {
            return new EasyObjectValidationResult(target, true, "Passed", null);
        }

        public override string ToString()
        {
            return String.Format("{0} {1} -> {2}", (this.Passed ? "Passed" : "Failed"), this.Message, this.Target);
        }
    }
}
