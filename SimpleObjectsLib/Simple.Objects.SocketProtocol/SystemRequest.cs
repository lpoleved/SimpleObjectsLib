using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Objects.SocketProtocol
{
	public enum SystemRequest
	{
		GetProtocolVersion = 0,
		AuthenticateSession = 1,
		GetServerObjectModel = 2,
		GetAllServerObjectModels = 3, // Remove this, too many bytes 12+k and is not security commpliant
		TransactionRequest = 4,
		GetObjectPropertyValues = 5,
		GetGraphElementsWithObjects = 6,
		GetSimpleObjectGraphElementByGraphKey = 7,
		GetOneToOneForeignObject = 8,
		GetOneToManyForeignObjectCollection = 9,
		GetGroupMembershipCollection = 10,
		GetObjectIdsTEMP = 100,
		//GetObjectCacheRelationCollection = 10,
		//GetObjectCacheRelationCollectionWithTableId = 11,
		//GetObjectPropertyValues = 5,
		//GetAndLoadPorpertyValues = 7,
	}
}
