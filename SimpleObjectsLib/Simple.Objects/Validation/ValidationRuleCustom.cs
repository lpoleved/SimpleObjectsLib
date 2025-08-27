using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Modeling;

namespace Simple.Objects
{
    public class ValidationRuleCustom : ValidationRule
    {
        public ValidationRuleCustom(Func<SimpleObject, IDictionary<SimpleObject, TransactionRequestAction>, bool> getCustomValidation, string errorMessageBody)
            : this(null, getCustomValidation, String.Empty, errorMessageBody)
        {
        }

        public ValidationRuleCustom(Func<SimpleObject, IDictionary<SimpleObject, TransactionRequestAction>, bool> getCustomValidation, string errorMessagePreffix, string errorMessageBody)
            : this(null, getCustomValidation, errorMessagePreffix, errorMessageBody)
        {
        }

        public ValidationRuleCustom(Func<SimpleObject, IDictionary<SimpleObject, TransactionRequestAction>, bool> getCustomValidation, string errorMessagePreffix, string errorMessageBody, string errorMessageSuffix)
            : this(null, getCustomValidation, errorMessagePreffix, errorMessageBody, errorMessageSuffix)
        {
        }

        public ValidationRuleCustom(IPropertyModel propertyModel, Func<SimpleObject, IDictionary<SimpleObject, TransactionRequestAction>, bool> getCustomValidation, string errorMessageBody)
            : this(propertyModel, getCustomValidation, String.Empty, errorMessageBody)
        {
        }

        public ValidationRuleCustom(IPropertyModel propertyModel, Func<SimpleObject, IDictionary<SimpleObject, TransactionRequestAction>, bool> getCustomValidation, string errorMessagePreffix, string errorMessageBody)
            : this(propertyModel, getCustomValidation, errorMessagePreffix, errorMessageBody, String.Empty)
        {
        }

        public ValidationRuleCustom(IPropertyModel propertyModel, Func<SimpleObject, IDictionary<SimpleObject, TransactionRequestAction>, bool> getCustomValidation, string errorMessagePreffix, string errorMessageBody, string errorMessageSuffix)
        {
            this.PropertyModel = propertyModel;
            this.GetCustomValidation = getCustomValidation;
            this.ErrorMessagePrefix = errorMessagePreffix;
            this.ErrorMessageBody = errorMessageBody;
            this.ErrorMessageSuffix = errorMessageSuffix;
        }

        public IPropertyModel PropertyModel { get; protected set; }
        public Func<SimpleObject, IDictionary<SimpleObject, TransactionRequestAction>?, bool> GetCustomValidation { get; protected set; }
        public string ErrorMessagePrefix { get; protected set; }
        public string ErrorMessageBody { get; protected set; }
        public string ErrorMessageSuffix { get; protected set; }

        public override ValidationResult Validate(SimpleObject simpleObject, IDictionary<SimpleObject, TransactionRequestAction>? transactionRequests)
        {
			PropertyValidationResult result;

			//if (simpleObject.IsValidationTest && this.SkipIfTest)
			//	return PropertyValidationResult.GetDefaultSuccessResult(this.PropertyModel);

			bool passed = this.GetCustomValidation(simpleObject, transactionRequests);

            if (passed)
            {
				result = PropertyValidationResult.GetDefaultSuccessResult(this.PropertyModel);
            }
			else
			{
				string objectName = (this.PropertyModel != null && this.PropertyModel.Owner != null && this.PropertyModel.Owner is ISimpleObjectModel simpleObjectModel) ? simpleObjectModel.ObjectCaption : String.Empty;
				string propertyCaption = this.PropertyModel != null ? this.PropertyModel.Caption : String.Empty;
				string errorMessage = this.ErrorMessagePrefix != null ? this.ErrorMessagePrefix : objectName + "'s";

				errorMessage += this.ErrorMessageBody != null ? this.ErrorMessageBody : " " + propertyCaption;
				errorMessage += this.ErrorMessageSuffix != null ? this.ErrorMessageSuffix : "";

				result = new PropertyValidationResult(false, errorMessage, this.PropertyModel);
			}

            return result;
        }
    }
}
