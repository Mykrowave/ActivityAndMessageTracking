using Activity.common.Communication.Repositories;
using Activity.common.cosmosdb.Interfaces;
using Activity.common.DomainModels.Communication;
using Activity.common.Repositories;
using Activity.common.SearchFilters.Communication;
using Activity.data.cosmosdb.Cosmos;
using Activity.data.cosmosdb.Extensions;
using Activity.data.cosmosdb.Helpers;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Activity.data.cosmosdb.Repositories.Communication
{
    public class CosmosSentEmailActivityRepository : CosmosActivityRepository<SentCampaignEmailActivity>, ISentEmailActivityRepository
    {
        public CosmosSentEmailActivityRepository(ICosmosDbClientFactory cosmosDbClientFactory) 
            : base(cosmosDbClientFactory)
        {
        }

        public override string CollectionName => "communicationActivity";

        public async Task<List<SentCampaignEmailActivity>> Query(string tenant, SentCampaignEmailActivityFilter filter)
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
                        PartitionKey = new PartitionKey($"{tenant}-{nameof(SentCampaignEmailActivity)}")
                    });

                var results = new List<SentCampaignEmailActivity>();

                if (documents != null && documents.Count >= 1)
                {
                    documents.ForEach(doc =>
                        results.Add(JsonConvert.DeserializeObject<SentCampaignEmailActivity>(doc.ToString()))
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

        /// <summary>
        /// An implicit SQL Query Builder for CosmosDB SQL API. A better approach would be to refactor in order to benefit from LINQ Query
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private CosmosSQLQuery _CreateCosmosSQLQuery(String tenant, SentCampaignEmailActivityFilter filter)
        {

            CosmosSQLQuery query = new CosmosSQLQuery();
            query.Parameters.Add("@startDate", filter.Start.ToUniversalTime().ToString());
            query.Parameters.Add("@endDate", filter.End.ToUniversalTime().ToString());
            query.Parameters.Add("@activityType", nameof(SentCampaignEmailActivity));
            query.Parameters.Add("@tenant", tenant);

            query.Query = $"SELECT * FROM {CollectionName} as t ";
            // Required Fields
            query.Query += $" WHERE t.{nameof(SentCampaignEmailActivity.ActionDate).ToCamelCase()} >= @startDate ";
            query.Query += $" AND t.{nameof(SentCampaignEmailActivity.ActionDate).ToCamelCase()} <= @endDate ";
            query.Query += $" AND t.{nameof(SentCampaignEmailActivity.ActivityType).ToCamelCase()} <= @activityType ";
            query.Query += $" AND t.{nameof(SentCampaignEmailActivity.Tenant).ToCamelCase()} = @tenant ";

            // if UserId is set
            if (!String.IsNullOrEmpty(filter.ActionUserId))
            {
                query.Query += $" AND t.{nameof(SentCampaignEmailActivity.ActionUserId).ToCamelCase()} = '@actionUserId' ";
                query.Parameters.Add("@actionUserId", filter.ActionUserId);
            }

            // TODO: Add Recipient Filter query

            // TODO: Add Subject Filter (with "Like")




            return query;
        }

        

        
    }
    
}
