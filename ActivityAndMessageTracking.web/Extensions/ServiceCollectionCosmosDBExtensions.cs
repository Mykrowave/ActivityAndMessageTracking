using Activity.common.Communication.Repositories;
using Activity.common.cosmosdb.Interfaces;
using Activity.common.Repositories.Communication;
using Activity.data.cosmosdb.Cosmos;
using Activity.data.cosmosdb.Repositories.Communication;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityAndMessageTracking.web.Extensions
{
    public static class ServiceCollectionCosmosDbExtensions
    {

        public static IServiceCollection AddCosmosDb(this IServiceCollection services, Uri serviceEndpoint,
            string authKey, string databaseName, List<string> collectionNames)
        {
            var documentClient = new DocumentClient(serviceEndpoint, authKey, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            documentClient.OpenAsync().Wait();

            var cosmosDbClientFactory = new CosmosDbClientFactory(databaseName, collectionNames, documentClient);
            cosmosDbClientFactory.EnsureDbSetupAsync().Wait();

            //CosmosDbClient Factory
            services.AddSingleton<ICosmosDbClientFactory>(cosmosDbClientFactory);

            //CosmosDb Repositories
            services.AddScoped<ISentEmailActivityRepository, CosmosSentEmailActivityRepository>();
            services.AddScoped<IDownloadedFromCommunicationActivityRepository, CosmosDownloadedFromCommunicationActivityRepository>();



            return services;
        }



    }
}
