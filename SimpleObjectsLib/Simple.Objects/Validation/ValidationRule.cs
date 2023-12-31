﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Modeling;

namespace Simple.Objects
{
    public class ValidationRule : IValidationRule //: ModelBase
    {
		private Func<SimpleObject, IDictionary<SimpleObject, TransactionRequestAction>, ValidationResult> validator = null;

		public ValidationRule()
            : this(String.Empty)
        {
        }

		public ValidationRule(string name)
			: this(name, null)
		{
		}

		public ValidationRule(string name, Func<SimpleObject, IDictionary<SimpleObject, TransactionRequestAction>, ValidationResult> validator)
        {
            this.Name = name;
            //this.getValidation = getValidation;
            this.SkipIfTest = true;
			this.validator = validator;
        }

        public string Name { get; private set; }
        public bool SkipIfTest { get; set; }

        public virtual ValidationResult Validate(SimpleObject simpleObject, IDictionary<SimpleObject, TransactionRequestAction> transactionActions)
        {
			return this.validator(simpleObject, transactionActions);
        }
    }

    public interface IValidationRule //: IModel
    {
        string Name { get; }
        //Func<SimpleObject, ValidationRuleResult> GetValidation { get; }
        ValidationResult Validate(SimpleObject simpleObject, IDictionary<SimpleObject, TransactionRequestAction> transactionActions);
        bool SkipIfTest { get; }
    }
}
