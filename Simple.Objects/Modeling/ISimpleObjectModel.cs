using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Modeling;
using Simple.Datastore;

namespace Simple.Objects
{
	public interface ISimpleObjectModel : IModelElement
	{
		//int TableId { get; }

		Type ObjectType { get; }
		PropertyModelCollection<PropertyModel> PropertyModels { get; }
		IPropertyModel GetPropertyModel(int propertyIndex);

		IPropertyModel IdPropertyModel { get; }
		IPropertyModel? NamePropertyModel { get; }
		IPropertyModel? DescriptionPropertyModel { get; }
		IPropertyModel? SubTypePropertyModel { get; }
		IPropertyModel? PreviousIdPropertyModel { get; }
		IPropertyModel? OrderIndexPropertyModel { get; }
		//IPropertyModel ActionSetOrderIndexPropertyModel { get; }

		int[] PropertyIndexes { get; }
		int[] IndexedPropertyIndexes { get; }
		int[] SerializablePropertyIndexes { get; }
		int[] StorablePropertyIndexes { get; }
		int[] TransactionActionPropertyIndexes { get; }
		int TransactionActionLogPropertySequenceId { get; }
		string[] StorableFieldNameIndexes { get; }

		SerializationModel SerializationModel { get; }
		//IPropertyModel[] StorablePropertyModelSequence { get; }
		//int[] StorablePropertyIndexSequence { get; }
		//int StorablePropertySequenceId { get; }
		//int[] PublicPropertyIndexSequence { get; }

		//int[] StorablePropertyIndexes { get; }
		//int StorablePropertyIndexSequenceId { get; }
		//IPropertyModel[] StorablePropertyModels { get; }

		//int TableId { get; }
		//string TableName { get; }
		TableInfo TableInfo { get; }
		bool ReuseIds { get; }
		long MinId { get; }
		string ObjectCaption { get; }
		bool IsStorable { get; }
		//bool IsSortable { get; }
		SortingModel SortingModel { get; }
		bool FetchAllRecords { get; }
		IList<IValidationRule> UpdateValidationRules { get; }
		IList<IValidationRule> DeleteValidationRules { get; }
		bool DeleteSimpleObjectOnLastGraphElementDelete { get; }
		bool DeleteAllSimpleObjectGraphElementsOnOneGraphElementDelete { get; }
		bool MustHaveAtLeastOneGraphElement { get; }
		int MustHaveGraphElementGraphKey { get; }

		// Consider remove this
		int SortableOneToManyRelationKey { get; }
		IDictionary<int, IModelElement> SubTypes { get; }
		IObjectRelationModel RelationModel { get; }
		//IList<GraphElementCreatedAction> GraphElementCreatedActions { get; }
	}
}
