using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Modeling;

namespace Simple.Objects
{
	//public class ValidationRuleExistence<T> : ValidationRuleExistence where T : SimpleObject
	//{
	//	public new Func<T, bool> GetValidationExistence { get; protected set; }

	//	public ValidationRuleExistence(IPropertyModel propertyModel, Func<T, bool> getValidationExistence)
	//		: base(propertyModel, null)
	//	{
	//		this.GetValidationExistence = getValidationExistence;
	//	}

	//	public ValidationRuleExistence(IPropertyModel propertyModel, Func<T, bool> getValidationExistence, string errorMessage)
	//		: base(propertyModel, null, errorMessage)
	//	{
	//		this.GetValidationExistence = getValidationExistence;
	//	}

	//	protected override Func<SimpleObject, bool> GetValidationExistenceMethod
	//	{
	//		get { return (Func<SimpleObject, bool>)this.GetValidationExistence; }
	//	}
	//}
	
	public class ValidationRuleExistance : ValidationRule
    {
        private bool validateByPropertyModel;

        public ValidationRuleExistance(IPropertyModel propertyModel)
            : base(propertyModel.PropertyName + " Existence")
        {
            this.PropertyModel = propertyModel;
            this.validateByPropertyModel = true;
			//this.PropertyOrItemName = String.Empty;
//            base.GetValidation = this.ValidatePropertyExistenceByPropertyModel;
        }

		public ValidationRuleExistance(IPropertyModel propertyModel, Func<SimpleObject, IDictionary<SimpleObject, TransactionRequestAction>, bool> getValidationExistence)
			: this(propertyModel, getValidationExistence, String.Empty)
        {
        }

		public ValidationRuleExistance(IPropertyModel propertyModel, Func<SimpleObject, IDictionary<SimpleObject, TransactionRequestAction>, bool> getValidationExistence, string errorMessage)
			: base(propertyModel + " Existence")
        {
			this.PropertyModel = propertyModel;
            this.GetValidationExistence = getValidationExistence;
//            base.GetValidation = this.ValidatePropertyExistenceByValidationExistenceDelegate;
            this.ErrorMessage = errorMessage;
            this.validateByPropertyModel = false;
        }

        public IPropertyModel PropertyModel { get; protected set; }
		//public string PropertyOrItemName { get; protected set; }
        public Func<SimpleObject, IDictionary<SimpleObject, TransactionRequestAction>, bool> GetValidationExistence { get; protected set; }
        public string ErrorMessage { get; protected set; }

		public PropertyValidationResult ValidatePropertyExistenceByPropertyModel(SimpleObject simpleObject, IDictionary<SimpleObject, TransactionRequestAction> transactionRequests)
        {
            bool passed = false;
			PropertyValidationResult validationResult = PropertyValidationResult.GetDefaultSuccessResult(this.PropertyModel);

			//if (simpleObject.IsValidationTest && this.SkipIfTest)
			//	return validationResult;

			object? propertyValue = simpleObject[this.PropertyModel.PropertyName];
            passed = !Comparison.IsEmpty(propertyValue);

            if (!passed)
            {
                ISimpleObjectModel objectModel = simpleObject.GetModel();
                validationResult = new PropertyValidationResult(false, objectModel.ObjectType.Name.InsertSpaceOnUpperChange() + " " + this.PropertyModel.Caption + " is empty.", this.PropertyModel);
            }

            return validationResult;
        }

        public PropertyValidationResult ValidatePropertyExistenceByValidationExistenceDelegate(SimpleObject simpleObject, IDictionary<SimpleObject, TransactionRequestAction> transactionRequests)
        {
            bool passed = false;
			PropertyValidationResult validationResult = PropertyValidationResult.GetDefaultSuccessResult(this.PropertyModel);

            passed = this.GetValidationExistence(simpleObject, transactionRequests);

            if (!passed)
            {
                ISimpleObjectModel objectModel = simpleObject.GetModel();
				string message = String.IsNullOrEmpty(this.ErrorMessage) ? objectModel.ObjectType.Name.InsertSpaceOnUpperChange() + " " + this.PropertyModel.Caption + " is empty." : this.ErrorMessage;

                validationResult = new PropertyValidationResult(false, message, this.PropertyModel);
            }

            return validationResult;
        }

        public override ValidationResult Validate(SimpleObject simpleObject, IDictionary<SimpleObject, TransactionRequestAction> transactionRequests)
        {
            if (this.validateByPropertyModel)
                return this.ValidatePropertyExistenceByPropertyModel(simpleObject, transactionRequests);
            else
                return this.ValidatePropertyExistenceByValidationExistenceDelegate(simpleObject, transactionRequests);
        }
    }
}
