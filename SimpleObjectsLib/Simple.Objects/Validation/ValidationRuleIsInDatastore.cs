using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Modeling;

namespace Simple.Objects
{
    public class ValidationRuleIsInDatastore : ValidationRule
    {
		public ValidationRuleIsInDatastore(IPropertyModel propertyModel)
			: this(propertyModel, simpleObject => simpleObject)
        {
        }

		public ValidationRuleIsInDatastore(IPropertyModel propertyModel, Func<SimpleObject, SimpleObject> getValidationObject)
			: base("Is " + propertyModel + " In Datastore")
        {
            this.GetValidationObject = getValidationObject;
        }

        public IPropertyModel PropertyModel { get; protected set; }
        public Func<SimpleObject, SimpleObject> GetValidationObject { get; protected set; }

        public override ValidationResult Validate(SimpleObject simpleObject, IDictionary<SimpleObject, TransactionRequestAction> transactionRequests)
        {
            bool passed = false;
			PropertyValidationResult validationResult = null;

            SimpleObject targetSimpleObject = this.GetValidationObject(simpleObject);
            passed = targetSimpleObject != null && targetSimpleObject.IsNew ? false : true;

            if (passed)
            {
                validationResult = PropertyValidationResult.GetDefaultSuccessResult(this.PropertyModel);
            }
            else
            {
                ISimpleObjectModel objectModel = simpleObject.GetModel();
                validationResult = new PropertyValidationResult(false, objectModel.ObjectType.Name.InsertSpaceOnUpperChange() + " " + this.PropertyModel.PropertyName + " is not in datastore.", this.PropertyModel);
            }

            return validationResult;
        }
    }
}
