using lab.LocalCosmosDbApp.DbContext;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.Repository
{
    public class BaseRepository<T> where T : class
    {
        public readonly string _databaseId;
        public readonly string _collectionId;
        public readonly DocumentClient _documentClient;
        
        public BaseRepository(string collectionId)
        {
            _documentClient = AppDbContext.GetDocumentClient();
            _databaseId = AppDbContext.GetDatabaseId();
            _collectionId = collectionId;

        }
        
        public async Task<T> GetItemByIdAsync(string id)
        {
            try
            {
                Document document = await _documentClient.ReadDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, id));
                return (T)(dynamic)document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<T> GetItemAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                IDocumentQuery<T> query = _documentClient.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId),
                new FeedOptions { MaxItemCount = -1 })
                .Where(predicate)
                .AsDocumentQuery();
                List<T> results = new List<T>();
                if (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<T>());
                }
                return results.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<T> GetItemLastAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                IDocumentQuery<T> query = _documentClient.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId),
                new FeedOptions { MaxItemCount = -1 })
                .Where(predicate)
                .AsDocumentQuery();
                List<T> results = new List<T>();
                if (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<T>());
                }
                return results.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<IEnumerable<T>> GetAllItemAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                IDocumentQuery<T> query = _documentClient.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId),
                new FeedOptions { MaxItemCount = -1 })
                .Where(predicate)
                .AsDocumentQuery();

                List<T> results = new List<T>();
                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<T>());
                }

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<IEnumerable<T>> GetAllItemAsync()
        {
            try
            {
                IDocumentQuery<T> query = _documentClient.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId),
                new FeedOptions { MaxItemCount = -1 })
                .AsDocumentQuery();

                List<T> results = new List<T>();
                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<T>());
                }

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Document> InsertItemAsync(T item)
        {
            try
            {
                return await _documentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId), item);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Document> UpdateItemAsync(string id, T item)
        {
            try
            {
                return await _documentClient.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, id), item);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteItemAsync(string id)
        {
            try
            {
                await _documentClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Initialize()
        {
            InsertCollectionIfNotExistsAsync().Wait();
        }

        private async Task InsertCollectionIfNotExistsAsync()
        {
            try
            {
                await _documentClient.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await _documentClient.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(_databaseId),
                        new DocumentCollection { Id = _collectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<T>> ExecuteSqlQuery(string sqlQuery)
        {
            IDocumentQuery<T> query = _documentClient.CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(_databaseId,_collectionId), sqlQuery,
                new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true }).AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }
            return results;
        }

        public async Task<IEnumerable<ReturnType>> ExecuteSqlQueryToGetList<ReturnType>(string sqlQuery) where ReturnType:class
        {
            IDocumentQuery<ReturnType> query = _documentClient.CreateDocumentQuery<ReturnType>(UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId), sqlQuery,
                new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true}).AsDocumentQuery();

            List<ReturnType> results = new List<ReturnType>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<ReturnType>());
            }
            return results;
        }

        public async Task<ReturnType> ExecuteSqlQueryToGetDocument<ReturnType>(string sqlQuery) where ReturnType : class
        {

            IDocumentQuery<ReturnType> query = _documentClient.CreateDocumentQuery<ReturnType>(UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId), sqlQuery,
                new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true }).AsDocumentQuery();

            List<ReturnType> results = new List<ReturnType>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<ReturnType>());
            }
            return results.FirstOrDefault();
        }
    }
}
