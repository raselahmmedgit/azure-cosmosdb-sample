using lab.LocalCosmosDbApp.EntityModels;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.Repository
{
    public class PersonRepository : BaseRepository<Persons>
    {
        public PersonRepository() : base(typeof(Persons).Name)
        {

        }
        
        public async Task<Persons> GetByIdAsync(string id)
        {
            try
            {
                var model = _documentClient.CreateDocumentQuery<Persons>(
                UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId),
                new FeedOptions { MaxItemCount = -1 }).Where(x => x.Id == id).FirstOrDefault();

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Persons>> GetAllAsync()
        {
            try
            {
                var modelList = _documentClient.CreateDocumentQuery<Persons>(
                UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId),
                new FeedOptions { MaxItemCount = -1 }).ToList();

                return modelList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Document> InsertAsync(Persons person)
        {
            try
            {
                Document document = new Document();
                if (person != null)
                {
                    person.Id = Guid.NewGuid().ToString();
                    await InsertItemAsync(person);
                    document.Id = person.Id;
                }
                return document;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Document> UpdateAsync(string id, Persons person)
        {
            try
            {
                Document document = new Document();

                if (person != null)
                {
                    await UpdateItemAsync(id, person);
                    document.Id = person.Id;
                }
                return document;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Document> DeleteAsync(string id)
        {
            try
            {
                Document document = new Document();

                var person = await base.GetItemByIdAsync(id);

                if (person != null)
                {
                    await DeleteAsync(person.Id);
                    document.Id = person.Id;
                }
                return document;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
