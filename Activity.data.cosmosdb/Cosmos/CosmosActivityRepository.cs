using Activity.common.cosmosdb.Interfaces;
using Activity.common.DomainModels;
using Activity.common.Exceptions;
using Activity.common.Repositories;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Activity.data.cosmosdb.Cosmos
{
    public abstract class CosmosActivityRepository<T> : IActivityRepository<T>, IDocumentCollectionContext<T> where T : BaseActivity
    {
        protected readonly ICosmosDbClientFactory _cosmosDbClientFactory;

        protected CosmosActivityRepository(ICosmosDbClientFactory cosmosDbClientFactory)
        {
            _cosmosDbClientFactory = cosmosDbClientFactory;
        }
        //public async Task<T> GetByIdAsync(string id)
        //{
        //    try
        //    {
        //        var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
        //        var document = await cosmosDbClient.ReadDocumentAsync(id, new RequestOptions
        //        {
        //            PartitionKey = ResolvePartitionKey(id)
        //        });

        //        return JsonConvert.DeserializeObject<T>(document.ToString());
        //    }
        //    catch (DocumentClientException e)
        //    {
        //        if (e.StatusCode == HttpStatusCode.NotFound)
        //        {
        //            throw new EntityNotFoundException();
        //        }

        //        throw;
        //    }
        //}
        public async Task<T> AddAsync(T entity)
        {
            try
            {
                entity.Id = GenerateId(entity);
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                var document = await cosmosDbClient.CreateDocumentAsync(entity);
                return JsonConvert.DeserializeObject<T>(document.ToString());
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new EntityAlreadyExistsException();
                }

                throw;
            }
        }
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            List<Task<T>> entityTasks = new List<Task<T>>();

            foreach(var task in entities)
            {
                entityTasks.Add(AddAsync(task));
            }

            return await Task.WhenAll<T>(entityTasks);
        }
        public async Task UpdateAsync(T entity)
        {
            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                await cosmosDbClient.ReplaceDocumentAsync(entity.Id, entity, new RequestOptions
                {
                    PartitionKey = ResolvePartitionKey(entity.Id),
                });
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException();
                }

                throw;
            }
        }
        public async Task DeleteAsync(T entity)
        {
            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                await cosmosDbClient.DeleteDocumentAsync(entity.Id, new RequestOptions
                {
                    PartitionKey = ResolvePartitionKey(entity.Id)
                });
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException();
                }

                throw;
            }
        }
        


        public abstract string CollectionName { get; }
        public virtual string GenerateId(T entity) => Guid.NewGuid().ToString();
        public virtual PartitionKey ResolvePartitionKey(string entityId) => null;

        
    }
}
