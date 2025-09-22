using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;

namespace Simple.Objects
{
    public class GraphObjectValidationResult : ValidationResult
    {
        public GraphObjectValidationResult(IPropertyValue target, bool passed, string message, ValidationResult failedRouleResult)
            : base(passed, message)
        {
            this.Target = target;
            this.FailedRuleResult = failedRouleResult;
        }

        public IPropertyValue Target { get; private set; }
        public ValidationResult FailedRuleResult { get; private set; }

        //public IPropertyModel ErrorPropertyModel
        //{
        //    get
        //    {
        //        if (this.FailedRuleResult != null && !this.FailedRuleResult.Passed)
        //            return this.FailedRuleResult.PropertyModel;

        //        return null;
        //    }
        //}

        public static GraphObjectValidationResult GetDefaultPassedResult(IPropertyValue target)
        {
            return new GraphObjectValidationResult(target, true, "Passed", null);
        }

        public override string ToString()
        {
            return String.Format("{0} {1} -> {2}", (this.Passed ? "Passed" : "Failed"), this.Message, this.Target);
        }
    }
}
