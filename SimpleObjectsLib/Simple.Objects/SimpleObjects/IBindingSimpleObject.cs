using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;

namespace Simple.Objects
{
    // TODO: Is IBindingSimpleObject really needed?. Use SimpleObject instead?
    public interface IBindingSimpleObject : IBindingObject
    {
		//event RelationForeignObjectSetEventHandler RelationForeignObjectSet;
        event BeforeChangePropertyValueSimpleObjectRequesterEventHandler BeforePropertyValueChange;
        event ChangePropertyValueSimpleObjectRequesterEventHandler PropertyValueChange;

        //bool Save();
        void RequestDelete();

        void SetPropertyValue(int propertyIndex, object value, ChangeContainer changeContainer, object requester);

        SimpleObject GetOneToOnePrimaryObject(int oneToOneRelationKey);
		bool SetOneToOnePrimaryObject(SimpleObject foreignObject, int oneToOneRelationKey, object requester);

		SimpleObject GetOneToOneForeignObject(int oneToOneRelationKey);
		bool SetOneToOneForeignObject(SimpleObject foreignObject, int oneToOneRelationKey, object requester);

		SimpleObject GetOneToManyPrimaryObject(int oneToManyRelationKey);
		bool SetOneToManyPrimaryObject(SimpleObject foreignObject, int oneToManyRelationKey, object requester);

        SimpleObjectCollection GetOneToManyForeignObjectCollection(int oneToManyRelationKey);

        SimpleObjectCollection GetGroupMemberCollection(int manyToManyRelationKey);


        //object this[int propertyIndex] { get; set; }

        //object GetPropertyValue(int propertyIndex);
        //T GetPropertyValue<T>(int propertyIndex);
        ////object GetOldPropertyValue(int propertyIndex);
        ////T GetOldPropertyValue<T>(int propertyIndex);
        //void SetPropertyValue(int propertyIndex, object value, object requester);
        //IEnumerable<int> ChangedPropertyIndexes
        //{ get; }

        ISimpleObjectModel GetModel();
        ////object GetObject();
        //string GetImageName();

        ////IDictionary<int, object> GetPropertyValueByIndexDictionary();
        ////IDictionary<string, object> GetPropertyValueByNameDictionary();
        ////IDictionary<int, object> GetOldPropertyValueByIndexDictionary();
        ////IDictionary<string, object> GetOldPropertyValueByNameDictionary();
        ////IDictionary<int, object> GetChangedPropertyValueByIndexDictionary();
        ////IDictionary<string, object> GetChangedPropertyValueByNameDictionary();
        ////IDictionary<int, object> GetChangedOldPropertyValueByIndexDictionary();
        ////IDictionary<string, object> GetChangedOldPropertyValueByNameDictionary();

        //void AcceptChanges(object requester);
        //void RejectChanges(object requester);

        //bool IsReadOnly { get; }
    }
}
