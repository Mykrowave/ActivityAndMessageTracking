using System;
using System.Collections.Generic;
using System.Text;

namespace Activity.common.cosmosdb.Interfaces
{
    public interface ICosmosDbClientFactory
    {
        ICosmosDbClient GetClient(string collectionName);
    }
}
