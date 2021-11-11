using lab.LocalCosmosDbApp.EntityModels;
using lab.LocalCosmosDbApp.Repository;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.DbContext
{
    public class AppDbContext
    {
        private static string _databaseId = null;
        private static DocumentClient _documentClient;

        public AppDbContext()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _databaseId = configuration.GetValue<string>("AppDbConnectionConfig:DatabaseId");
        }

        private static void Initialize()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _documentClient = new DocumentClient(new Uri(configuration.GetValue<string>("AppDbConnectionConfig:EndPointUrl")), configuration.GetValue<string>("AppDbConnectionConfig:AuthKey"));

            if (configuration.GetValue<string>("AppDbConnectionConfig:IsDatabaseCreate") != null && configuration.GetValue<bool>("AppDbConnectionConfig:IsDatabaseCreate"))
            {
                CreateDatabaseIfNotExistsAsync().Wait();
            }
            if (configuration.GetValue<string>("AppDbConnectionConfig:IsTableCreate") != null && configuration.GetValue<bool>("AppDbConnectionConfig:IsTableCreate"))
            {
                CreateIdentityUsersAndRoles().Wait();
                CreateTablesIfNotExists().Wait();
            }
            if (configuration.GetValue<string>("AppDbConnectionConfig:IsDemoDataInsert") != null && configuration.GetValue<bool>("AppDbConnectionConfig:IsDemoDataInsert"))
            {
                //InsertDemolDataAsync().Wait();
            }
        }
        
        public static DocumentClient GetDocumentClient()
        {
            if (_documentClient == null)
                Initialize();
            return _documentClient;
        }
        public static void CreateDocumentClient()
        {
            if (_documentClient == null)
                Initialize();
        }
        public static string GetDatabaseId()
        {
            return _databaseId;
        }
        
        private static async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {                
                await _documentClient.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(_databaseId));
                
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await _documentClient.CreateDatabaseAsync(new Database { Id = _databaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private static async Task CreateIdentityUsersAndRoles()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            try
            {
                // Does the Collection exist?
                await _documentClient.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(_databaseId, configuration.GetValue<string>("AppDbConnectionConfig:AspNetIdentityUsers")), new RequestOptions { OfferThroughput = 1000 });                                
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    DocumentCollection collection = new DocumentCollection() { Id = configuration.GetValue<string>("AppDbConnectionConfig:AspNetIdentityUsers") };
                    await _documentClient.CreateDocumentCollectionAsync(UriFactory.CreateDatabaseUri(_databaseId), collection, new RequestOptions { OfferThroughput = 1000 });                    
                }
            }

            try
            {
                // Does the Collection exist?
                await _documentClient.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(_databaseId, configuration.GetValue<string>("AppDbConnectionConfig:AspNetIdentityRoles")), new RequestOptions { OfferThroughput = 1000 });
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    DocumentCollection collection = new DocumentCollection() { Id = configuration.GetValue<string>("AppDbConnectionConfig:AspNetIdentityRoles") };
                    await _documentClient.CreateDocumentCollectionAsync(UriFactory.CreateDatabaseUri(_databaseId), collection, new RequestOptions { OfferThroughput = 1000 });
                }
            }
        }

        private static async Task CreateTablesIfNotExists()
        {
            new PersonRepository().Initialize();

            await Task.Yield();
        }
      
        private static async Task InsertDemolDataAsync()
        {
            #region Person
            PersonRepository personRepository = new PersonRepository();
            var personExist = await personRepository.GetByIdAsync("3116bb7d-ad48-4c5e-8630-a2aa046a9e64");
            if (personExist == null)
            {
                Persons person = new Persons
                {
                    Id = "3116bb7d-ad48-4c5e-8630-a2aa046a9e64",
                    FirstName = "Rasel",
                    Age = 35
                };
                await personRepository.InsertAsync(person);
            }
            #endregion
        }
    }
}
