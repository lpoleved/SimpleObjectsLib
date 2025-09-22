using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects.SocketProtocol
{
	public enum SystemRequest
	{
		//GetProtocolVersion = 0,
		GetServerVersionInfo = 0,
		AuthenticateSession = 1,
		GetServerObjectModel = 2,
		//GetAllServerObjectModels_OLD = 3, // Remove this, too many bytes 12+k and is not security commpliant
		TransactionRequest = 3,
		GetObjectPropertyValues = 4,
		GetGraphElementsWithObjects = 5,
		GetGraphElementsWithObjectsNew = 6,
		GetSimpleObjectGraphElementByGraphKey = 7,
		GetOneToOneForeignObject = 8,
		GetOneToManyForeignObjectCollection = 9,
		GetGroupMembershipCollection = 10,
		DoesGraphElementHaveChildren = 11,
		
		GetObjectIdsTEMP = 12, 
		
		//GetObjectCacheRelationCollection = 10,
		//GetObjectCacheRelationCollectionWithTableId = 11,
		//GetObjectPropertyValues = 5,
		//GetAndLoadPorpertyValues = 7,
	}
}
