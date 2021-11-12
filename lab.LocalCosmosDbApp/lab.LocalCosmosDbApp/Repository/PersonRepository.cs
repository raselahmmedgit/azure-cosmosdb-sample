using lab.LocalCosmosDbApp.Data;
using lab.LocalCosmosDbApp.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private AppDbContext _context;
        public PersonRepository()
        {
            _context = new AppDbContext();
        }
        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Person> GetPersonAsync(string id)
        {
            return await _context.Person.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Person>> GetPersonsAsync()
        {
            return await _context.Person.ToListAsync();
        }

        public async Task<int> InsertOrUpdatetPersonAsync(Person model)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                await _context.Person.AddAsync(model);
            }
            else
            {
                _context.Person.Update(model);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> InsertPersonAsync(Person model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.Id))
                {
                    await _context.Person.AddAsync(model);
                }
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdatePersonAsync(Person model)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                _context.Person.Update(model);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeletePersonAsync(Person model)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                _context.Person.Remove(model);
            }
            return await _context.SaveChangesAsync();
        }

    }

    public interface IPersonRepository
    {
        Task<Person> GetPersonAsync(string id);
        Task<IEnumerable<Person>> GetPersonsAsync();
        Task<int> InsertOrUpdatetPersonAsync(Person model);
        Task<int> InsertPersonAsync(Person model);
        Task<int> UpdatePersonAsync(Person model);
        Task<int> DeletePersonAsync(Person model);
    }
}
