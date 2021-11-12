using lab.LocalCosmosDbApp.Data;
using lab.LocalCosmosDbApp.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.Repository
{
    public class ToolInfoApproverSourceRepository : IToolInfoApproverSourceRepository
    {
        private AppDbContext _context;
        public ToolInfoApproverSourceRepository()
        {
            _context = new AppDbContext();
        }
        public ToolInfoApproverSourceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ToolInfoApproverSource> GetToolInfoApproverSourceAsync(string id)
        {
            return await _context.ToolInfoApproverSource.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ToolInfoApproverSource>> GetToolInfoApproverSourcesAsync()
        {
            return await _context.ToolInfoApproverSource.ToListAsync();
        }

        public async Task<int> InsertOrUpdatetToolInfoApproverSourceAsync(ToolInfoApproverSource model)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                await _context.ToolInfoApproverSource.AddAsync(model);
            }
            else
            {
                _context.ToolInfoApproverSource.Update(model);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> InsertToolInfoApproverSourceAsync(ToolInfoApproverSource model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.Id))
                {
                    await _context.ToolInfoApproverSource.AddAsync(model);
                }
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateToolInfoApproverSourceAsync(ToolInfoApproverSource model)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                _context.ToolInfoApproverSource.Update(model);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteToolInfoApproverSourceAsync(ToolInfoApproverSource model)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                _context.ToolInfoApproverSource.Remove(model);
            }
            return await _context.SaveChangesAsync();
        }

    }

    public interface IToolInfoApproverSourceRepository
    {
        Task<ToolInfoApproverSource> GetToolInfoApproverSourceAsync(string id);
        Task<IEnumerable<ToolInfoApproverSource>> GetToolInfoApproverSourcesAsync();
        Task<int> InsertOrUpdatetToolInfoApproverSourceAsync(ToolInfoApproverSource model);
        Task<int> InsertToolInfoApproverSourceAsync(ToolInfoApproverSource model);
        Task<int> UpdateToolInfoApproverSourceAsync(ToolInfoApproverSource model);
        Task<int> DeleteToolInfoApproverSourceAsync(ToolInfoApproverSource model);
    }
}
