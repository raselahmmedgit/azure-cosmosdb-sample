using AutoMapper;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using lab.LocalCosmosDbApp.EntityModels;
using lab.LocalCosmosDbApp.Helpers;
using lab.LocalCosmosDbApp.Repository;
using lab.LocalCosmosDbApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.Managers
{
    public class ToolInfoApproverSourceManager : IToolInfoApproverSourceManager
    {
        private readonly IToolInfoApproverSourceRepository _iToolInfoApproverSourceRepository;
        private readonly IMapper _iMapper;

        public ToolInfoApproverSourceManager(IToolInfoApproverSourceRepository iToolInfoApproverSourceRepository
            , IMapper iMapper)
        {
            _iToolInfoApproverSourceRepository = iToolInfoApproverSourceRepository;
            _iMapper = iMapper;
        }
        
        public async Task<ToolInfoApproverSourceViewModel> GetToolInfoApproverSourceAsync()
        {
            try
            {
                var dataIEnumerable = await _iToolInfoApproverSourceRepository.GetToolInfoApproverSourcesAsync();
                var data = dataIEnumerable.FirstOrDefault();
                return _iMapper.Map<ToolInfoApproverSource, ToolInfoApproverSourceViewModel>(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ToolInfoApproverSourceViewModel> GetToolInfoApproverSourceAsync(string id)
        {
            try
            {
                var data = await _iToolInfoApproverSourceRepository.GetToolInfoApproverSourceAsync(id);
                return _iMapper.Map<ToolInfoApproverSource, ToolInfoApproverSourceViewModel>(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DataTablesResponse> GetDataTablesResponseAsync(IDataTablesRequest request)
        {
            try
            {
                var modelIEnumerable = await _iToolInfoApproverSourceRepository.GetToolInfoApproverSourcesAsync();
                var viewModelIEnumerable = _iMapper.Map<IEnumerable<ToolInfoApproverSource>, IEnumerable<ToolInfoApproverSourceViewModel>>(modelIEnumerable);

                // Global filtering.
                // Filter is being manually applied due to in-memmory (IEnumerable) data.
                // If you want something rather easier, check IEnumerableExtensions Sample.

                int dataCount = viewModelIEnumerable.Count();
                int filteredDataCount = 0;
                IEnumerable<ToolInfoApproverSourceViewModel> dataPage;
                if (viewModelIEnumerable.Count() > 0 && request != null)
                {
                    var filteredData = String.IsNullOrWhiteSpace(request.Search.Value)
                    ? viewModelIEnumerable
                    : viewModelIEnumerable.Where(_item => _item.Building.Contains(request.Search.Value));

                    dataCount = filteredData.Count();

                    // Paging filtered data.
                    // Paging is rather manual due to in-memmory (IEnumerable) data.
                    dataPage = filteredData.Skip(request.Start).Take(request.Length);

                    filteredDataCount = filteredData.Count();
                }
                else
                {
                    var filteredData = viewModelIEnumerable;

                    dataCount = filteredData.Count();

                    dataPage = filteredData;

                    filteredDataCount = filteredData.Count();
                }

                // Response creation. To create your response you need to reference your request, to avoid
                // request/response tampering and to ensure response will be correctly created.
                var response = DataTablesResponse.Create(request, dataCount, filteredDataCount, dataPage);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ToolInfoApproverSourceViewModel>> GetToolInfoApproverSourcesAsync()
        {
            try
            {
                var data = await _iToolInfoApproverSourceRepository.GetToolInfoApproverSourcesAsync();
                return _iMapper.Map<IEnumerable<ToolInfoApproverSource>, IEnumerable<ToolInfoApproverSourceViewModel>>(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ToolInfoApproverSource>> GetToolInfoApproverSourcesAsync(ToolInfoApproverSourceSearch searchModel)
        {
            try
            {
                var modelList = await _iToolInfoApproverSourceRepository.GetToolInfoApproverSourcesAsync(searchModel);
                return modelList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ToolInfoApproverSource>> GetToolInfoApproverSourcesWithSqlAsync(ToolInfoApproverSourceSearch searchModel)
        {
            try
            {
                var modelList = await _iToolInfoApproverSourceRepository.GetToolInfoApproverSourcesWithSqlAsync(searchModel);
                return modelList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> InsertOrUpdatetToolInfoApproverSourceAsync(ToolInfoApproverSourceViewModel model)
        {
            var data = _iMapper.Map<ToolInfoApproverSourceViewModel, ToolInfoApproverSource>(model);
            return await _iToolInfoApproverSourceRepository.InsertOrUpdatetToolInfoApproverSourceAsync(data);
        }

        public async Task<Result> InsertToolInfoApproverSourceAsync(ToolInfoApproverSourceViewModel model)
        {
            try
            {
                var data = _iMapper.Map<ToolInfoApproverSourceViewModel, ToolInfoApproverSource>(model);

                var saveChange = await _iToolInfoApproverSourceRepository.InsertToolInfoApproverSourceAsync(data);

                if (saveChange > 0)
                {
                    return Result.Ok(MessageHelper.Save);
                }
                else
                {
                    return Result.Fail(MessageHelper.SaveFail);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Result> UpdateToolInfoApproverSourceAsync(ToolInfoApproverSourceViewModel model)
        {
            try
            {
                var data = _iMapper.Map<ToolInfoApproverSourceViewModel, ToolInfoApproverSource>(model);

                var saveChange = await _iToolInfoApproverSourceRepository.UpdateToolInfoApproverSourceAsync(data);

                if (saveChange > 0)
                {
                    return Result.Ok(MessageHelper.Update);
                }
                else
                {
                    return Result.Fail(MessageHelper.UpdateFail);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Result> DeleteToolInfoApproverSourceAsync(string id)
        {
            try
            {
                var model = await GetToolInfoApproverSourceAsync(id);
                if (model != null)
                {
                    var data = _iMapper.Map<ToolInfoApproverSourceViewModel, ToolInfoApproverSource>(model);

                    var saveChange = await _iToolInfoApproverSourceRepository.DeleteToolInfoApproverSourceAsync(data);

                    if (saveChange > 0)
                    {
                        return Result.Ok(MessageHelper.Delete);
                    }
                    else
                    {
                        return Result.Fail(MessageHelper.DeleteFail);
                    }
                }
                else
                {
                    return Result.Fail(MessageHelper.DeleteFail);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public interface IToolInfoApproverSourceManager
    {
        Task<ToolInfoApproverSourceViewModel> GetToolInfoApproverSourceAsync();
        Task<ToolInfoApproverSourceViewModel> GetToolInfoApproverSourceAsync(string id);
        Task<DataTablesResponse> GetDataTablesResponseAsync(IDataTablesRequest request);
        Task<IEnumerable<ToolInfoApproverSourceViewModel>> GetToolInfoApproverSourcesAsync();
        Task<IEnumerable<ToolInfoApproverSource>> GetToolInfoApproverSourcesAsync(ToolInfoApproverSourceSearch searchModel);
        Task<IEnumerable<ToolInfoApproverSource>> GetToolInfoApproverSourcesWithSqlAsync(ToolInfoApproverSourceSearch searchModel);
        Task<int> InsertOrUpdatetToolInfoApproverSourceAsync(ToolInfoApproverSourceViewModel model);
        Task<Result> InsertToolInfoApproverSourceAsync(ToolInfoApproverSourceViewModel model);
        Task<Result> UpdateToolInfoApproverSourceAsync(ToolInfoApproverSourceViewModel model);
        Task<Result> DeleteToolInfoApproverSourceAsync(string id);
    }
}
