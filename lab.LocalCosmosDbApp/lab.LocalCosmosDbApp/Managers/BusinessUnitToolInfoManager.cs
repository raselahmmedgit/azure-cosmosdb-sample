using AutoMapper;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using lab.LocalCosmosDbApp.EntityModels;
using lab.LocalCosmosDbApp.Helpers;
using lab.LocalCosmosDbApp.Repository;
using lab.LocalCosmosDbApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.Managers
{
    public class BusinessUnitToolInfoManager : IBusinessUnitToolInfoManager
    {
        private readonly IBusinessUnitToolInfoRepository _iBusinessUnitToolInfoRepository;
        private readonly IMapper _iMapper;

        public BusinessUnitToolInfoManager(IBusinessUnitToolInfoRepository iBusinessUnitToolInfoRepository
            , IMapper iMapper)
        {
            _iBusinessUnitToolInfoRepository = iBusinessUnitToolInfoRepository;
            _iMapper = iMapper;
        }
        
        public async Task<BusinessUnitToolInfoViewModel> GetBusinessUnitToolInfoAsync()
        {
            try
            {
                var dataIEnumerable = await _iBusinessUnitToolInfoRepository.GetBusinessUnitToolInfosAsync();
                var data = dataIEnumerable.FirstOrDefault();
                return _iMapper.Map<BusinessUnitToolInfo, BusinessUnitToolInfoViewModel>(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BusinessUnitToolInfoViewModel> GetBusinessUnitToolInfoAsync(string id)
        {
            try
            {
                var data = await _iBusinessUnitToolInfoRepository.GetBusinessUnitToolInfoAsync(id);
                return _iMapper.Map<BusinessUnitToolInfo, BusinessUnitToolInfoViewModel>(data);
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
                var viewModelIEnumerable = await this.GetBusinessUnitToolInfoBySqlQueryAsync();
                //var viewModelIEnumerable = _iMapper.Map<IEnumerable<BusinessUnitToolInfo>, IEnumerable<BusinessUnitToolInfoViewModel>>(modelIEnumerable);

                // Global filtering.
                // Filter is being manually applied due to in-memmory (IEnumerable) data.
                // If you want something rather easier, check IEnumerableExtensions Sample.

                int dataCount = viewModelIEnumerable.Count();
                int filteredDataCount = 0;
                IEnumerable<BusinessUnitToolInfoViewModel> dataPage;
                if (viewModelIEnumerable.Count() > 0 && request != null)
                {
                    var filteredData = String.IsNullOrWhiteSpace(request.Search.Value)
                    ? viewModelIEnumerable
                    : viewModelIEnumerable.Where(_item => _item.BU.Contains(request.Search.Value));

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

        public async Task<IEnumerable<BusinessUnitToolInfoViewModel>> GetBusinessUnitToolInfosAsync()
        {
            try
            {
                var data = await _iBusinessUnitToolInfoRepository.GetBusinessUnitToolInfosAsync();
                return _iMapper.Map<IEnumerable<BusinessUnitToolInfo>, IEnumerable<BusinessUnitToolInfoViewModel>>(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Result> InsertBusinessUnitToolInfoAsync(BusinessUnitToolInfoViewModel model)
        {
            try
            {
                var data = _iMapper.Map<BusinessUnitToolInfoViewModel, BusinessUnitToolInfo>(model);

                var saveChange = await _iBusinessUnitToolInfoRepository.InsertBusinessUnitToolInfoAsync(data);

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

        public async Task<Result> InsertBusinessUnitToolInfoAsync(List<BusinessUnitToolInfoViewModel> modelList)
        {
            try
            {
                var dataList = _iMapper.Map<List<BusinessUnitToolInfoViewModel>, List< BusinessUnitToolInfo>>(modelList);

                var saveChange = await _iBusinessUnitToolInfoRepository.InsertBusinessUnitToolInfoAsync(dataList);

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

        public async Task<Result> UpdateBusinessUnitToolInfoAsync(BusinessUnitToolInfoViewModel model)
        {
            try
            {
                var data = _iMapper.Map<BusinessUnitToolInfoViewModel, BusinessUnitToolInfo>(model);

                var saveChange = await _iBusinessUnitToolInfoRepository.UpdateBusinessUnitToolInfoAsync(data);

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

        public async Task<Result> DeleteBusinessUnitToolInfoAsync(string id)
        {
            try
            {
                var model = await GetBusinessUnitToolInfoAsync(id);
                if (model != null)
                {
                    var data = _iMapper.Map<BusinessUnitToolInfoViewModel, BusinessUnitToolInfo>(model);

                    var saveChange = await _iBusinessUnitToolInfoRepository.DeleteBusinessUnitToolInfoAsync(data);

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

        public async Task<Result> UploadBusinessUnitToolInfosAsync(IFormFile uploadFile)
        {
            try
            {
                var dataList = ImportHelper.ImportBusinessUnitToolInfoFromCSVFileNew(uploadFile);
                //var jsonList = JsonConvert.SerializeObject(dataList);
                return await this.InsertBusinessUnitToolInfoAsync(dataList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BusinessUnitToolInfoViewModel> GetBusinessUnitToolInfoBySqlQueryAsync(string id)
        {
            try
            {
                string sql = $"SELECT *"
                          + $" FROM c where c.businessUnitToolInfoId = \"{id}\"";

                var data = await _iBusinessUnitToolInfoRepository.ExecuteSqlQueryToGet(sql);

                return _iMapper.Map<BusinessUnitToolInfo, BusinessUnitToolInfoViewModel>(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<BusinessUnitToolInfoViewModel>> GetBusinessUnitToolInfoBySqlQueryAsync()
        {
            try
            {
                string sql = $"SELECT *"
                          + $" FROM c";

                var dataList = await _iBusinessUnitToolInfoRepository.ExecuteSqlQueryToGetList(sql);

                return _iMapper.Map< IEnumerable<BusinessUnitToolInfo>, IEnumerable<BusinessUnitToolInfoViewModel>>(dataList);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public interface IBusinessUnitToolInfoManager
    {
        Task<BusinessUnitToolInfoViewModel> GetBusinessUnitToolInfoAsync();
        Task<BusinessUnitToolInfoViewModel> GetBusinessUnitToolInfoAsync(string id);
        Task<DataTablesResponse> GetDataTablesResponseAsync(IDataTablesRequest request);
        Task<IEnumerable<BusinessUnitToolInfoViewModel>> GetBusinessUnitToolInfosAsync();
        Task<Result> InsertBusinessUnitToolInfoAsync(List<BusinessUnitToolInfoViewModel> modelList);
        Task<Result> InsertBusinessUnitToolInfoAsync(BusinessUnitToolInfoViewModel model);
        Task<Result> UpdateBusinessUnitToolInfoAsync(BusinessUnitToolInfoViewModel model);
        Task<Result> DeleteBusinessUnitToolInfoAsync(string id);
        Task<Result> UploadBusinessUnitToolInfosAsync(IFormFile uploadFile);
        Task<BusinessUnitToolInfoViewModel> GetBusinessUnitToolInfoBySqlQueryAsync(string id);
        Task<IEnumerable<BusinessUnitToolInfoViewModel>> GetBusinessUnitToolInfoBySqlQueryAsync();
    }
}
