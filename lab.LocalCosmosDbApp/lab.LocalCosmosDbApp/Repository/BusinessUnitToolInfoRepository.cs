using lab.LocalCosmosDbApp.Config;
using lab.LocalCosmosDbApp.Data;
using lab.LocalCosmosDbApp.EntityModels;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.Repository
{
    public class BusinessUnitToolInfoRepository : IBusinessUnitToolInfoRepository
    {
        //private AppDbContext _context;
        private readonly AppDbConnectionConfig _appDbConnectionConfig;
        public readonly string _databaseId;
        public readonly string _collectionId;
        //public BusinessUnitToolInfoRepository()
        //{
        //    _context = new AppDbContext();
        //}
        //public BusinessUnitToolInfoRepository(AppDbContext context)
        //{
        //    _context = context;
        //}
        //public BusinessUnitToolInfoRepository(IDbContextFactory<AppDbContext> factory)
        //{
        //    _context = factory.CreateDbContext();
        //}
        public BusinessUnitToolInfoRepository(IOptions<AppDbConnectionConfig> appDbConnectionConfig)
        {
            _appDbConnectionConfig = appDbConnectionConfig.Value;
            _databaseId = _appDbConnectionConfig.DatabaseName;
            _collectionId = _appDbConnectionConfig.ContainerName;
        }

        public readonly ConnectionPolicy _connectionPolicy = new ConnectionPolicy
        {
            ConnectionMode = ConnectionMode.Direct,
            ConnectionProtocol = Protocol.Tcp,
            RequestTimeout = new TimeSpan(1, 0, 0),
            MaxConnectionLimit = 1000,
            RetryOptions = new RetryOptions
            {
                MaxRetryAttemptsOnThrottledRequests = 10,
                MaxRetryWaitTimeInSeconds = 60
            }
        };

        public async Task<BusinessUnitToolInfo> GetBusinessUnitToolInfoAsync(string id)
        {
            try
            {
                using (var client = new DocumentClient(new Uri(_appDbConnectionConfig.EndPointUrl), _appDbConnectionConfig.AuthKey))
                {
                    var link = UriFactory.CreateDocumentUri(_databaseId, _collectionId, id);

                    var query = client.CreateDocumentQuery<BusinessUnitToolInfo>(
                                link,
                                new FeedOptions()
                                {
                                    EnableCrossPartitionQuery = true
                                })
                            .AsDocumentQuery();

                    var results = new List<BusinessUnitToolInfo>();

                    while (query.HasMoreResults)
                    {
                        results.AddRange(await query.ExecuteNextAsync<BusinessUnitToolInfo>());
                    }

                    return results.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<BusinessUnitToolInfo>> GetBusinessUnitToolInfosAsync()
        {
            using (var client = new DocumentClient(new Uri(_appDbConnectionConfig.EndPointUrl), _appDbConnectionConfig.AuthKey))
            {
                var link = UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId);

                var query = client.CreateDocumentQuery<BusinessUnitToolInfo>(
                                    link,
                                    new FeedOptions()
                                    {
                                        EnableCrossPartitionQuery = true
                                    })
                                .AsDocumentQuery();

                var results = new List<BusinessUnitToolInfo>();
                
                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<BusinessUnitToolInfo>());
                }

                return results.AsEnumerable();
            }
        }

        public async Task<int> InsertBusinessUnitToolInfoAsync(List<BusinessUnitToolInfo> modelList)
        {
            try
            {
                if (modelList != null)
                {
                    //using (var client = new DocumentClient(new Uri(_appDbConnectionConfig.EndPointUrl), _appDbConnectionConfig.AuthKey))
                    //{
                    //    var link = UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId);
                    //    foreach (var item in modelList)
                    //    {
                    //        await client.CreateDocumentAsync(link, item);
                    //    }
                    //    return 1;
                    //}

                    using (var client = new DocumentClient(new Uri(_appDbConnectionConfig.EndPointUrl), _appDbConnectionConfig.AuthKey, _connectionPolicy))
                    {
                        var link = UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId);
                        foreach (var item in modelList)
                        {
                            await client.CreateDocumentAsync(link, item);
                        }
                        return 1;
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> InsertBusinessUnitToolInfoAsync(BusinessUnitToolInfo model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.Id))
                {
                    using (var client = new DocumentClient(new Uri(_appDbConnectionConfig.EndPointUrl), _appDbConnectionConfig.AuthKey))
                    {
                        var link = UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId);
                        await client.CreateDocumentAsync(link, model);
                        return 1;
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateBusinessUnitToolInfoAsync(BusinessUnitToolInfo model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.Id))
                {
                    using (var client = new DocumentClient(new Uri(_appDbConnectionConfig.EndPointUrl), _appDbConnectionConfig.AuthKey))
                    {
                        var link = UriFactory.CreateDocumentUri(_databaseId, _collectionId, model.Id);
                        await client.ReplaceDocumentAsync(link, model);
                        return 1;
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> DeleteBusinessUnitToolInfoAsync(BusinessUnitToolInfo model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.Id))
                {
                    using (var client = new DocumentClient(new Uri(_appDbConnectionConfig.EndPointUrl), _appDbConnectionConfig.AuthKey))
                    {
                        var link = UriFactory.CreateDocumentUri(_databaseId, _collectionId, model.Id);
                        await client.DeleteDocumentAsync(link, new RequestOptions()
                        { PartitionKey = new Microsoft.Azure.Documents.PartitionKey(model.Id) });
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<BusinessUnitToolInfo>> ExecuteSqlQuery(string sqlQuery)
        {
            using (var client = new DocumentClient(new Uri(_appDbConnectionConfig.EndPointUrl), _appDbConnectionConfig.AuthKey))
            {

                IDocumentQuery<BusinessUnitToolInfo> query = client.CreateDocumentQuery<BusinessUnitToolInfo>(UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId), sqlQuery,
                    new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true }).AsDocumentQuery();

                List<BusinessUnitToolInfo> results = new List<BusinessUnitToolInfo>();
                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<BusinessUnitToolInfo>());
                }

                return results.AsEnumerable();
            }

        }

        public async Task<IEnumerable<BusinessUnitToolInfo>> ExecuteSqlQueryToGetList(string sqlQuery)
        {
            using (var client = new DocumentClient(new Uri(_appDbConnectionConfig.EndPointUrl), _appDbConnectionConfig.AuthKey))
            {
                IDocumentQuery<BusinessUnitToolInfo> query = client.CreateDocumentQuery<BusinessUnitToolInfo>(UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId), sqlQuery,
                    new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true }).AsDocumentQuery();

                List<BusinessUnitToolInfo> results = new List<BusinessUnitToolInfo>();
                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<BusinessUnitToolInfo>());
                }

                return results.AsEnumerable();
            }
            
        }

        public async Task<BusinessUnitToolInfo> ExecuteSqlQueryToGet(string sqlQuery)
        {
            using (var client = new DocumentClient(new Uri(_appDbConnectionConfig.EndPointUrl), _appDbConnectionConfig.AuthKey))
            {
                IDocumentQuery<BusinessUnitToolInfo> query = client.CreateDocumentQuery<BusinessUnitToolInfo>(UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId), sqlQuery,
                new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true }).AsDocumentQuery();

                List<BusinessUnitToolInfo> results = new List<BusinessUnitToolInfo>();
                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<BusinessUnitToolInfo>());
                }

                return results.FirstOrDefault();

            }
        }
    }

    public interface IBusinessUnitToolInfoRepository
    {
        Task<BusinessUnitToolInfo> GetBusinessUnitToolInfoAsync(string id);
        Task<IEnumerable<BusinessUnitToolInfo>> GetBusinessUnitToolInfosAsync();
        Task<int> InsertBusinessUnitToolInfoAsync(List<BusinessUnitToolInfo> modelList);
        Task<int> InsertBusinessUnitToolInfoAsync(BusinessUnitToolInfo model);
        Task<int> UpdateBusinessUnitToolInfoAsync(BusinessUnitToolInfo model);
        Task<int> DeleteBusinessUnitToolInfoAsync(BusinessUnitToolInfo model);
        Task<IEnumerable<BusinessUnitToolInfo>> ExecuteSqlQueryToGetList(string sqlQuery);
        Task<BusinessUnitToolInfo> ExecuteSqlQueryToGet(string sqlQuery);
    }
}
