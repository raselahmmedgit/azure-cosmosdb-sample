using lab.LocalCosmosDbApp.Data;
using lab.LocalCosmosDbApp.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.Repository
{
    public class AppDbInitRepository : IAppDbInitRepository
    {
        private AppDbContext _context;
        //public AppDbInitRepository()
        //{
        //    _context = new AppDbContext();
        //}
        //public AppDbInitRepository(AppDbContext context)
        //{
        //    _context = context;
        //}
        public AppDbInitRepository(IDbContextFactory<AppDbContext> factory)
        {
            _context = factory.CreateDbContext();
        }

        public async Task<bool> CreateDatabaseIfNotExists()
        {
            return await _context.Database.EnsureCreatedAsync();
        }

        public async Task<int> CreateTableAndInsertMasterData(List<Person> personList, List<ToolInfoApproverSource> toolInfoApproverSourceList)
        {
            // Create an instance and save the entity to the database
            
            await _context.AddRangeAsync(personList);
            await _context.AddRangeAsync(toolInfoApproverSourceList);

            return await _context.SaveChangesAsync();
        }

    }

    public interface IAppDbInitRepository
    {
        Task<bool> CreateDatabaseIfNotExists();
        Task<int> CreateTableAndInsertMasterData(List<Person> personList, List<ToolInfoApproverSource> toolInfoApproverSourceList);
    }
}
