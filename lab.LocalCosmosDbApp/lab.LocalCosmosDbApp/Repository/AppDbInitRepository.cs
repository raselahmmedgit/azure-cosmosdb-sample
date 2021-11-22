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

        public bool CreateDatabaseIfNotExists()
        {
            return _context.Database.EnsureCreated();
        }

        public int CreateTableAndInsertMasterData(List<Person> personList, List<ToolInfoApproverSource> toolInfoApproverSourceList)
        {
            // Create an instance and save the entity to the database
            
            _context.AddRange(personList);
            _context.AddRange(toolInfoApproverSourceList);

            return _context.SaveChanges();
        }

    }

    public interface IAppDbInitRepository
    {
        bool CreateDatabaseIfNotExists();
        int CreateTableAndInsertMasterData(List<Person> personList, List<ToolInfoApproverSource> toolInfoApproverSourceList);
    }
}
