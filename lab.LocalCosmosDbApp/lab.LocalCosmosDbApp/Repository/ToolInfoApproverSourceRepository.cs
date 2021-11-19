using lab.LocalCosmosDbApp.Data;
using lab.LocalCosmosDbApp.EntityModels;
using lab.LocalCosmosDbApp.ViewModels;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.Repository
{
    public class ToolInfoApproverSourceRepository : IToolInfoApproverSourceRepository
    {
        private AppDbContext _context;
        //public ToolInfoApproverSourceRepository()
        //{
        //    _context = new AppDbContext();
        //}
        //public ToolInfoApproverSourceRepository(AppDbContext context)
        //{
        //    _context = context;
        //}
        public ToolInfoApproverSourceRepository(IDbContextFactory<AppDbContext> factory)
        {
            _context = factory.CreateDbContext();
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

        public async Task<IEnumerable<ToolInfoApproverSource>> GetToolInfoApproverSourcesAsync(ToolInfoApproverSourceSearch searchModel)
        {
            var toolInfoApproverSource = _context.ToolInfoApproverSource.AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.ToolInfoApproverSourceId))
            {
                toolInfoApproverSource = toolInfoApproverSource.Where(m => m.Building.Contains(searchModel.ToolInfoApproverSourceId));
            }
            if (!string.IsNullOrEmpty(searchModel.Building))
            {
                toolInfoApproverSource = toolInfoApproverSource.Where(m => m.Building.Contains(searchModel.Building));
            }
            if (!string.IsNullOrEmpty(searchModel.BU))
            {
                toolInfoApproverSource = toolInfoApproverSource.Where(m => m.BU.Contains(searchModel.BU));
            }
            if (!string.IsNullOrEmpty(searchModel.KPU))
            {
                toolInfoApproverSource = toolInfoApproverSource.Where(m => m.KPU.Contains(searchModel.KPU));
            }
            if (searchModel.BeginDate != default(DateTime))
            {
                toolInfoApproverSource = toolInfoApproverSource.Where(m => m.BeginDate >= searchModel.BeginDate);
            }
            if (searchModel.EndDate != default(DateTime))
            {
                toolInfoApproverSource = toolInfoApproverSource.Where(m => m.EndDate <= searchModel.EndDate);
            }
            if (!string.IsNullOrEmpty(searchModel.ToolId))
            {
                toolInfoApproverSource = toolInfoApproverSource.Where(m => m.ToolProfile.ToolId.Contains(searchModel.ToolId));
            }
            if (!string.IsNullOrEmpty(searchModel.ToolName))
            {
                toolInfoApproverSource = toolInfoApproverSource.Where(m => m.ToolProfile.ToolName.Contains(searchModel.ToolName));
            }
            if (!string.IsNullOrEmpty(searchModel.Bay))
            {
                toolInfoApproverSource = toolInfoApproverSource.Where(m => m.ToolProfile.Bay.Contains(searchModel.Bay));
            }
            if (!string.IsNullOrEmpty(searchModel.Lab))
            {
                toolInfoApproverSource = toolInfoApproverSource.Where(m => m.ToolProfile.Lab.Contains(searchModel.Lab));
            }
            if (!string.IsNullOrEmpty(searchModel.Room))
            {
                toolInfoApproverSource = toolInfoApproverSource.Where(m => m.ToolProfile.Room.Contains(searchModel.Room));
            }
            if (!string.IsNullOrEmpty(searchModel.Initiator))
            {
                toolInfoApproverSource = toolInfoApproverSource.Where(m => m.ToolProfile.Initiator.Contains(searchModel.Initiator));
            }
            if (!string.IsNullOrEmpty(searchModel.ToolOwner))
            {
                toolInfoApproverSource = toolInfoApproverSource.Where(m => m.ToolProfile.ToolOwner.Contains(searchModel.ToolOwner));
            }
            if (!string.IsNullOrEmpty(searchModel.SecondaryContact))
            {
                toolInfoApproverSource = toolInfoApproverSource.Where(m => m.ToolProfile.SecondaryContact.Contains(searchModel.SecondaryContact));
            }
            if (!string.IsNullOrEmpty(searchModel.LabManager))
            {
                toolInfoApproverSource = toolInfoApproverSource.Where(m => m.ToolProfile.LabManager.Contains(searchModel.LabManager));
            }
            if (!string.IsNullOrEmpty(searchModel.RegionSite))
            {
                toolInfoApproverSource = toolInfoApproverSource.Where(m => m.EHSAssignment.RegionSite.Contains(searchModel.RegionSite));
            }

            //Container container = _context.Database.GetCosmosClient().GetContainer("LabAssetDb", "LabAssetContainer");
            //var resultList = await QueryWithSqlParameters(container);

            return await toolInfoApproverSource.ToListAsync();
        }

        // <QueryWithSqlParameters>
        private async Task<IEnumerable<ToolInfoApproverSource>> QueryWithSqlParameters(Container container)
        {
            string sql = $"SELECT * FROM ToolInfoApproverSource c WHERE 0=0";

            //sql += $" AND LOWER(c.ToolInfoApproverSourceId) LIKE LOWER('%{searchModel.ToolInfoApproverSourceId}%') ";
            //sql += $" AND LOWER(c.Building) LIKE LOWER('%{searchModel.Building}%') ";
            //sql += $" AND LOWER(c.BU) LIKE LOWER('%{searchModel.BU}%') ";
            //sql += $" AND LOWER(c.KPU) LIKE LOWER('%{searchModel.KPU}%') ";

            //sql += $" AND LOWER(c.BeginDate) LIKE LOWER('%{searchModel.BeginDate}%') ";
            //sql += $" AND LOWER(c.EndDate) LIKE LOWER('%{searchModel.EndDate}%') ";


            //sql += $" AND LOWER(c.ToolProfile.ToolId) LIKE LOWER('%{searchModel.ToolId}%') ";
            //sql += $" AND LOWER(c.ToolProfile.ToolName) LIKE LOWER('%{searchModel.Bay}%') ";
            //sql += $" AND LOWER(c.ToolProfile.Bay) LIKE LOWER('%{searchModel.Bay}%') ";
            //sql += $" AND LOWER(c.ToolProfile.Lab) LIKE LOWER('%{searchModel.Lab}%') ";
            //sql += $" AND LOWER(c.ToolProfile.Room) LIKE LOWER('%{searchModel.Room}%') ";
            //sql += $" AND LOWER(c.ToolProfile.Initiator) LIKE LOWER('%{searchModel.Initiator}%') ";
            //sql += $" AND LOWER(c.ToolProfile.ToolOwner) LIKE LOWER('%{searchModel.ToolOwner}%') ";
            //sql += $" AND LOWER(c.ToolProfile.SecondaryContact) LIKE LOWER('%{searchModel.SecondaryContact}%') ";
            //sql += $" AND LOWER(c.ToolProfile.LabManager) LIKE LOWER('%{searchModel.LabManager}%') ";

            //sql += $" AND LOWER(c.EHSAssignment.RegionSite) LIKE LOWER('%{searchModel.RegionSite}%') ";
            //sql += $" AND LOWER(c.EHSAssignment.BuildingEnvironmental) LIKE LOWER('%{searchModel.BuildingEnvironmental}%') ";
            //sql += $" AND LOWER(c.EHSAssignment.EnvironmentalAdditionalReviewerOne) LIKE LOWER('%{searchModel.EnvironmentalAdditionalReviewerOne}%') ";
            //sql += $" AND LOWER(c.EHSAssignment.EnvironmentalAdditionalReviewerTwo) LIKE LOWER('%{searchModel.EnvironmentalAdditionalReviewerTwo}%') ";
            //sql += $" AND LOWER(c.EHSAssignment.OccupationalSafety) LIKE LOWER('%{searchModel.OccupationalSafety}%') ";
            //sql += $" AND LOWER(c.EHSAssignment.ChemAuthFacilities) LIKE LOWER('%{searchModel.ChemAuthFacilities}%') ";
            //sql += $" AND LOWER(c.EHSAssignment.ProductSafety) LIKE LOWER('%{searchModel.ProductSafety}%') ";
            //sql += $" AND LOWER(c.EHSAssignment.AdditionalEHSIH) LIKE LOWER('%{searchModel.AdditionalEHSIH}%') ";

            QueryDefinition query = new QueryDefinition(sql);

            List<ToolInfoApproverSource> results = new List<ToolInfoApproverSource>();
            using (FeedIterator<ToolInfoApproverSource> resultSetIterator = container.GetItemQueryIterator<ToolInfoApproverSource>(
                query,
                requestOptions: new QueryRequestOptions()
                {
                    PartitionKey = new PartitionKey("ToolInfoApproverSourceId")
                }))
            {
                while (resultSetIterator.HasMoreResults)
                {
                    FeedResponse<ToolInfoApproverSource> response = await resultSetIterator.ReadNextAsync();
                    results.AddRange(response);
                    if (response.Diagnostics != null)
                    {
                        Console.WriteLine($"\nQueryWithSqlParameters Diagnostics: {response.Diagnostics.ToString()}");
                    }
                }

            }

            return results;
        }
        // </QueryWithSqlParameters>
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
