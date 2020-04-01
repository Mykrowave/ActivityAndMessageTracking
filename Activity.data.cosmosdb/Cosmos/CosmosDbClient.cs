using Activity.common.cosmosdb.Interfaces;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Activity.data.cosmosdb.Cosmos
{
    public class CosmosDbClient : ICosmosDbClient
    {
        private readonly string _databaseName;
        private readonly string _collectionName;
        private readonly IDocumentClient _documentClient;

        public CosmosDbClient(string databaseName, string collectionName, IDocumentClient documentClient)
        {
            _databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
            _collectionName = collectionName ?? throw new ArgumentNullException(nameof(collectionName));
            _documentClient = documentClient ?? throw new ArgumentNullException(nameof(documentClient));
        }

        
        public async Task<Document> ReadDocumentAsync(string documentId, RequestOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _documentClient.ReadDocumentAsync(
                UriFactory.CreateDocumentUri(_databaseName, _collectionName, documentId), options, cancellationToken);
        }

        public async Task<Document> CreateDocumentAsync(object document, RequestOptions options = null,
            bool disableAutomaticIdGeneration = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _documentClient.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName), document, options,
                disableAutomaticIdGeneration, cancellationToken);
        }

        public async Task<Document> ReplaceDocumentAsync(string documentId, object document,
            RequestOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _documentClient.ReplaceDocumentAsync(
                UriFactory.CreateDocumentUri(_databaseName, _collectionName, documentId), document, options,
                cancellationToken);
        }

        public async Task<Document> DeleteDocumentAsync(string documentId, RequestOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _documentClient.DeleteDocumentAsync(
                UriFactory.CreateDocumentUri(_databaseName, _collectionName, documentId), options, cancellationToken);
        }

        public async Task<List<Document>> Query(string query, Dictionary<string, string> queryParameters = null, 
            FeedOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {

            SqlParameterCollection paramCollection = new SqlParameterCollection();
            foreach (var p in queryParameters)
            {
                paramCollection.Add(new SqlParameter(p.Key, p.Value));
            }

            SqlQuerySpec querySpec = queryParameters == null ?
                new SqlQuerySpec(query) :
                new SqlQuerySpec(query, paramCollection);

            var docQuery = _documentClient.CreateDocumentQuery<Document>(
                UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName),
                querySpec,
                options).AsDocumentQuery();

            List<Document> results = new List<Document>();
            while (docQuery.HasMoreResults)
            {
                foreach (Document d in await docQuery.ExecuteNextAsync<Document>())
                {
                    results.Add(d);
                }
            }

            return results;
        }

        
    }
}
