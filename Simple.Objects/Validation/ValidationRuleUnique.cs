using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Modeling;

namespace Simple.Objects
{
    public class ValidationRuleUnique : ValidationRule
    {
        public ValidationRuleUnique(IPropertyModel? propertyModel)
            : this(propertyModel, String.Empty)
        {
        }
        
        public ValidationRuleUnique(IPropertyModel? propertyModel, string errorSufix)
            : base(String.Format("Unique{0}", (propertyModel != null) ? " " + propertyModel!.PropertyName : String.Empty))
        {
            this.PropertyModel = propertyModel;
            this.ErrorSuffix = errorSufix;
        }

        public ValidationRuleUnique(IPropertyModel? propertyModel, int validationGraphKey)
            : this(propertyModel)
        {
            this.ValidationGraphKey = validationGraphKey;
        }

        public ValidationRuleUnique(IPropertyModel? propertyModel, IEnumerable<SimpleObject> collection)
            : this(propertyModel)
        {
            List<IEnumerable<SimpleObject>> targetMultipleCollections = new List<IEnumerable<SimpleObject>>();
            
            targetMultipleCollections.Add(collection);
            this.MultipleCollections = targetMultipleCollections;
        }

        public ValidationRuleUnique(IPropertyModel? propertyModel, IEnumerable<IEnumerable<SimpleObject>> multipleCollections)
            : this(propertyModel)
        {
            this.MultipleCollections = multipleCollections;
        }

        public ValidationRuleUnique(IPropertyModel? propertyModel, Func<SimpleObject, IEnumerable<SimpleObject>> getValidationCollection)
            : this(propertyModel, getValidationCollection, String.Empty)
        {
        }

        public ValidationRuleUnique(IPropertyModel? propertyModel, Func<SimpleObject, IEnumerable<SimpleObject>> getValidationCollection, string? errorSuffix)
            : this(propertyModel, getValidationCollection, objectComparer: null, errorSuffix)
        {
        }

		public ValidationRuleUnique(IPropertyModel? propertyModel, Func<SimpleObject, IEnumerable<SimpleObject>> getValidationCollection, Func<SimpleObject, SimpleObject, bool>? objectComparer)
			: this(propertyModel, getValidationCollection, objectComparer, String.Empty)
		{
		}

		public ValidationRuleUnique(IPropertyModel? propertyModel, Func<SimpleObject, IEnumerable<SimpleObject>> getValidationCollection, Func<SimpleObject, SimpleObject, bool>? objectComparer, string? errorSuffix)
			: this(propertyModel)
		{
			this.ObjectComparer = objectComparer;
			this.GetValidationCollection = getValidationCollection;
			this.ErrorSuffix = errorSuffix;
		}
		
        public ValidationRuleUnique(IPropertyModel? propertyModel, Func<SimpleObject, IEnumerable<IEnumerable<SimpleObject>>> getValidationMultipleCollection)
            : this(propertyModel)
        {
            this.GetValidationMultipleCollection = getValidationMultipleCollection;
        }

        public IPropertyModel? PropertyModel { get; protected set; }
        public int ValidationGraphKey { get; protected set; }
        public IEnumerable<IEnumerable<SimpleObject>>? MultipleCollections { get; protected set; }
        public Func<SimpleObject, SimpleObject, bool>? ObjectComparer { get; protected set; }
        public Func<SimpleObject, IEnumerable<SimpleObject>>? GetValidationCollection { get; protected set; }
        public Func<SimpleObject, IEnumerable<IEnumerable<SimpleObject>>>? GetValidationMultipleCollection { get; protected set; }
        public string? ErrorSuffix { get; protected set; }

        public override ValidationResult Validate(SimpleObject simpleObject, IDictionary<SimpleObject, TransactionRequestAction>? transactionRequests)
        {
			PropertyValidationResult validationResult = PropertyValidationResult.GetDefaultSuccessResult(this.PropertyModel);
            SimpleObject? inputSimpleObject = this.GetValidationSimpleObject(simpleObject);

			//if (inputSimpleObject != null && inputSimpleObject.IsValidationTest && this.SkipIfTest)
			//	return validationResult;

            if (inputSimpleObject is null)
				return new PropertyValidationResult(false, "Unique property validation failed: input object is null");

			object ? inputSimpleObjectPropertyValue = (inputSimpleObject != null && this.PropertyModel != null && this.PropertyModel.PropertyName != "Name") ? inputSimpleObject[this.PropertyModel.PropertyName] 
                                                                                                                                                             : inputSimpleObject?.GetName();
            IEnumerable<IEnumerable<SimpleObject>>? targetMultipleCollections = null;

            if (this.GetValidationMultipleCollection != null)
            {
                targetMultipleCollections = this.GetValidationMultipleCollection(simpleObject);
            }
            else if (this.GetValidationCollection != null)
            {
                IEnumerable<SimpleObject> validationCollection = this.GetValidationCollection(simpleObject);
                List<IEnumerable<SimpleObject>> validationCollections = new List<IEnumerable<SimpleObject>>();
                
                validationCollections.Add(validationCollection);
                targetMultipleCollections = validationCollections;
            }
            else if (this.MultipleCollections != null)
            {
                targetMultipleCollections = this.MultipleCollections;
            }
            else if (this.ValidationGraphKey > 0 && inputSimpleObject != null)
            {
                targetMultipleCollections = from graphElement in (inputSimpleObject as SimpleObject).GraphElements
                                            where graphElement.Graph.GraphKey == this.ValidationGraphKey
                                            select graphElement.GetNeighbours();
            }
            else if (inputSimpleObject != null && inputSimpleObject.GetModel().MustHaveAtLeastOneGraphElement)
            {
                targetMultipleCollections = from graphElement in (inputSimpleObject as SimpleObject).GraphElements
                                            select graphElement.GetNeighbours();
            }
            else
            {
                SimpleObjectCollection<SimpleObject>? validationList = (simpleObject.Manager.GetObjectCache(simpleObject.GetModel().TableInfo.TableId))?.GetObjectCollection<SimpleObject>();

                if (validationList != null)
                {
                    List<IEnumerable<SimpleObject>> validationCollections = new List<IEnumerable<SimpleObject>>();
                    
                    validationCollections.Add(validationList);
                    targetMultipleCollections = validationCollections;
                }
                else if (inputSimpleObject != null) // validationList will never be null
                {
                    targetMultipleCollections = from graphElement in (inputSimpleObject as SimpleObject).GraphElements
                                                select graphElement.GetNeighbours();
                }
            }

            if (targetMultipleCollections != null)
            {
                foreach (IEnumerable<SimpleObject> validationCollection in targetMultipleCollections)
                {
                    foreach (SimpleObject item in validationCollection)
                    {
                        SimpleObject? validationSimpleObject = this.GetValidationSimpleObject(item);
                        // Check unique value only for different type of objects
                        if (validationSimpleObject != null && validationSimpleObject != inputSimpleObject) // && validationSimpleObject.GetType() != inputSimpleObject.GetType())
                        {
                            IPropertyModel? propertyModel = (this.PropertyModel != null) ? validationSimpleObject.GetModel().PropertyModels[this.PropertyModel.PropertyName] : null;
                            bool isEqueal = false;

                            if (this.ObjectComparer != null)
                            {
                                isEqueal = this.ObjectComparer(item, inputSimpleObject!);
                            }
                            else
                            {
                                object? validationSimpleObjectPropertyValue = null;

                                if (propertyModel != null)
                                    validationSimpleObjectPropertyValue = (propertyModel.PropertyIndex != validationSimpleObject.GetModel().NamePropertyModel.PropertyIndex) ? validationSimpleObject[propertyModel.PropertyIndex]
                                                                                                                                                                             : validationSimpleObject.GetName();
                                else // if (this.PropertyModel?.PropertyName == "Name")
                                    validationSimpleObjectPropertyValue = validationSimpleObject.GetName();

                                isEqueal = Comparison.IsEqual(validationSimpleObjectPropertyValue, inputSimpleObjectPropertyValue, trimBeforeStringComparison: true);
                            }

                            if (isEqueal)
                            {
                                ISimpleObjectModel objectModel = simpleObject.GetModel();
                                string propertyCaption = (this.PropertyModel != null) ? " " + this.PropertyModel.Caption : String.Empty;
                                string errorMessage = String.Empty;  // objectModel.ObjectType.Name.InsertSpaceOnUpperChange() + propertyCaption;

                                if (this.ErrorSuffix.IsNullOrEmpty())
                                {
                                    errorMessage = objectModel.Description + propertyCaption + " must be unique within graph neighbours";

                                    if (item is GraphElement graphElement)
                                        errorMessage += $" ({graphElement.Graph.Name?.InsertSpaceOnUpperChange()} Graph)";

                                    errorMessage += ".";
                                }
                                else
                                {
                                    errorMessage = this.ErrorSuffix ?? String.Empty;
                                }

                                validationResult = new PropertyValidationResult(false, errorMessage, this.PropertyModel);

                                break;
                            }
                        }
                    }

                    if (!validationResult.Passed)
                        break;
                }
            }
            else
			{
                validationResult = new PropertyValidationResult(false, "Unique property validation failed: Target collection is null; SimpleObject:" + simpleObject.GetType().Name + this.ErrorSuffix);
            }

            return validationResult;
        }

        public SimpleObject? GetValidationSimpleObject(SimpleObject simpleObject) => (simpleObject is GraphElement graphElement) ? graphElement.SimpleObject 
                                                                                                                                 : simpleObject;
    }
}
