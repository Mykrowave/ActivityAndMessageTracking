using Activity.common.DomainModels;
using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Activity.common.cosmosdb.Interfaces
{
    public interface IDocumentCollectionContext<in T> where T : BaseActivity
    {
        string CollectionName { get; }
        string GenerateId(T entity);
        //PartitionKey ResolvePartitionKey(string entityId); 
    }
}
