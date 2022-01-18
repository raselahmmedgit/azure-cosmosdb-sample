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
    public class PersonManager : IPersonManager
    {
        private readonly IPersonRepository _iPersonRepository;
        private readonly IMapper _iMapper;

        public PersonManager(IPersonRepository iPersonRepository
            , IMapper iMapper)
        {
            _iPersonRepository = iPersonRepository;
            _iMapper = iMapper;
        }
        
        public async Task<PersonViewModel> GetPersonAsync()
        {
            try
            {
                var dataIEnumerable = await _iPersonRepository.GetPersonsAsync();
                var data = dataIEnumerable.FirstOrDefault();
                return _iMapper.Map<Person, PersonViewModel>(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PersonViewModel> GetPersonAsync(string id)
        {
            try
            {
                var data = await _iPersonRepository.GetPersonAsync(id);
                return _iMapper.Map<Person, PersonViewModel>(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DataTablesResponse> GetDataTablesResponseAsync(IDataTablesRequest request, string currentUserName, string currentUserRoleName)
        {
            try
            {
                var modelIEnumerable = await _iPersonRepository.GetPersonsAsync();
                var viewModelIEnumerable = _iMapper.Map<IEnumerable<Person>, IEnumerable<PersonViewModel>>(modelIEnumerable);

                var dataList = viewModelIEnumerable.ToList();

                // Global filtering.
                // Filter is being manually applied due to in-memmory (IEnumerable) data.
                // If you want something rather easier, check IEnumerableExtensions Sample.

                int totalRecords = dataList.Count();
                int totalRecordsFiltered = 0;
                List<PersonViewModel> data;
                if (dataList.Count() > 0 && request != null)
                {
                    var filteredData = String.IsNullOrWhiteSpace(request.Search.Value)
                    ? dataList
                    : dataList.Where(_item => _item.PersonName.Contains(request.Search.Value)).ToList();

                    // Paging filtered data.
                    // Paging is rather manual due to in-memmory (IEnumerable) data.
                    data = filteredData.Skip(request.Start).Take(request.Length).ToList();

                    totalRecordsFiltered = filteredData.Count();
                }
                else
                {
                    var filteredData = dataList;

                    data = filteredData;

                    totalRecordsFiltered = filteredData.Count();
                }

                data.ForEach(x => {
                    x.User = currentUserName;
                    x.Role = currentUserRoleName;
                    x.IsCardView = "true";
                    x.IsListView = "false";
                });

                // Response creation. To create your response you need to reference your request, to avoid
                // request/response tampering and to ensure response will be correctly created.
                var response = DataTablesResponse.Create(request, totalRecords, totalRecordsFiltered, data);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<PersonViewModel>> GetPersonsAsync()
        {
            try
            {
                var data = await _iPersonRepository.GetPersonsAsync();
                return _iMapper.Map<IEnumerable<Person>, IEnumerable<PersonViewModel>>(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> InsertOrUpdatetPersonAsync(PersonViewModel model)
        {
            var data = _iMapper.Map<PersonViewModel, Person>(model);
            return await _iPersonRepository.InsertOrUpdatetPersonAsync(data);
        }

        public async Task<Result> InsertPersonAsync(PersonViewModel model)
        {
            try
            {
                var data = _iMapper.Map<PersonViewModel, Person>(model);

                var saveChange = await _iPersonRepository.InsertPersonAsync(data);

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

        public async Task<Result> UpdatePersonAsync(PersonViewModel model)
        {
            try
            {
                var data = _iMapper.Map<PersonViewModel, Person>(model);

                var saveChange = await _iPersonRepository.UpdatePersonAsync(data);

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

        public async Task<Result> DeletePersonAsync(string id)
        {
            try
            {
                var model = await GetPersonAsync(id);
                if (model != null)
                {
                    var data = _iMapper.Map<PersonViewModel, Person>(model);

                    var saveChange = await _iPersonRepository.DeletePersonAsync(data);

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

    public interface IPersonManager
    {
        Task<PersonViewModel> GetPersonAsync();
        Task<PersonViewModel> GetPersonAsync(string id);
        Task<DataTablesResponse> GetDataTablesResponseAsync(IDataTablesRequest request, string currentUserName, string currentUserRoleName);
        Task<IEnumerable<PersonViewModel>> GetPersonsAsync();
        Task<int> InsertOrUpdatetPersonAsync(PersonViewModel model);
        Task<Result> InsertPersonAsync(PersonViewModel model);
        Task<Result> UpdatePersonAsync(PersonViewModel model);
        Task<Result> DeletePersonAsync(string id);
    }
}
