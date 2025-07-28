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
		GetSimpleObjectGraphElementByGraphKey = 6,
		GetOneToOneForeignObject = 7,
		GetOneToManyForeignObjectCollection = 8,
		GetGroupMembershipCollection = 9,
		DoesGraphElementHaveChildren = 10,
		
		GetObjectIdsTEMP = 11, 
		
		//GetObjectCacheRelationCollection = 10,
		//GetObjectCacheRelationCollectionWithTableId = 11,
		//GetObjectPropertyValues = 5,
		//GetAndLoadPorpertyValues = 7,
	}
}
