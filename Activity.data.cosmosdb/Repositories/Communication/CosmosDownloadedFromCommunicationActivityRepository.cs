using Activity.common.cosmosdb.Interfaces;
using Activity.common.DomainModels.Communication;
using Activity.common.Repositories.Communication;
using Activity.common.SearchFilters.Communication;
using Activity.data.cosmosdb.Cosmos;
using Activity.data.cosmosdb.Extensions;
using Activity.data.cosmosdb.Helpers;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Activity.data.cosmosdb.Repositories.Communication
{
    public class CosmosDownloadedFromCommunicationActivityRepository : CosmosActivityRepository<DownloadedFromCommunicationActivity>, IDownloadedFromCommunicationActivityRepository
    {
        public CosmosDownloadedFromCommunicationActivityRepository(ICosmosDbClientFactory cosmosDbClientFactory) 
            : base(cosmosDbClientFactory)
        {
        }

        public override string CollectionName => "communicationActivity";

        public async Task<List<DownloadedFromCommunicationActivity>> Query(string tenant, DownloadedFromCommunicationActivityFilter filter)
        {
            CosmosSQLQuery query = _CreateCosmosSQLQuery(tenant, filter);

            try
            {
                var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
                var documents = await cosmosDbClient.Query(
                    query.Query,
                    query.Parameters,
                    new FeedOptions
                    {
                        PartitionKey = new PartitionKey($"{tenant}-{nameof(DownloadedFromCommunicationActivity)}")
                    });

                var results = new List<DownloadedFromCommunicationActivity>();

                if (documents != null && documents.Count >= 1)
                {
                    documents.ForEach(doc =>
                        results.Add(JsonConvert.DeserializeObject<DownloadedFromCommunicationActivity>(doc.ToString()))
                    );
                }

                return results;

            }
            catch (Exception e)
            {
                // TODO: Create Specific Exceptions to handle problem cases
                throw e;
            }

            throw new NotImplementedException();
        }

        private CosmosSQLQuery _CreateCosmosSQLQuery(String tenant, DownloadedFromCommunicationActivityFilter filter)
        {

            CosmosSQLQuery query = SearchFilterHelper.CreateBaseSqlQuery(tenant, CollectionName, filter);

            // if UserId is set
            if (!String.IsNullOrEmpty(filter.ActionUserId))
            {
                query.Query += $" AND t.{nameof(SentCampaignEmailActivity.ActionUserId).ToCamelCase()} = '@actionUserId' ";
                query.Parameters.Add("@actionUserId", filter.ActionUserId);
            }

            // TODO: Implement FileType Filter

            

            return query;
        }
    }
}
