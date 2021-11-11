using lab.LocalCosmosDbApp.EntityModels;
using lab.LocalCosmosDbApp.Helpers;
using lab.LocalCosmosDbApp.Repository;
using lab.LocalCosmosDbApp.Utility;
using lab.LocalCosmosDbApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.Managers
{
    public class PersonManager : IPersonManager
    {
        private readonly PersonRepository _personRepository;

        public PersonManager()
        {
            _personRepository = new PersonRepository();
        }

        public async Task<List<PersonViewModel>> GetAllAsync()
        {
            try
            {
                var models = await _personRepository.GetAllAsync();
                var viewModelList = AutoMapperConfiguration.Mapper.Map<List<Persons>, List<PersonViewModel>>(models);
                return viewModelList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PersonViewModel> GetByIdAsync(string id)
        {
            try
            {
                var model = await _personRepository.GetByIdAsync(id);
                var viewModel = AutoMapperConfiguration.Mapper.Map<Persons, PersonViewModel>(model);
                return viewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Result> InsertAsync(PersonViewModel viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    var model = AutoMapperConfiguration.Mapper.Map<PersonViewModel, Persons>(viewModel);
                    var document = await _personRepository.InsertAsync(model);

                    if (!string.IsNullOrEmpty(document.Id))
                    {
                        return Result.Ok(MessageHelper.Save);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Result.Fail(MessageHelper.SaveFail);
        }

        public async Task<Result> UpdateAsync(PersonViewModel viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    var model = AutoMapperConfiguration.Mapper.Map<PersonViewModel, Persons>(viewModel);
                    var document = await _personRepository.UpdateAsync(model.Id, model);

                    if (!string.IsNullOrEmpty(document.Id))
                    {
                        return Result.Ok(MessageHelper.Update);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Result.Fail(MessageHelper.UpdateFail);
        }

        public async Task<Result> DeleteAsync(PersonViewModel viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    var model = AutoMapperConfiguration.Mapper.Map<PersonViewModel, Persons>(viewModel);
                    var document = await _personRepository.DeleteAsync(model.Id);

                    if (!string.IsNullOrEmpty(document.Id))
                    {
                        return Result.Ok(MessageHelper.Delete);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Result.Fail(MessageHelper.DeleteFail);
        }
    }

    public interface IPersonManager
    {
        Task<List<PersonViewModel>> GetAllAsync();
        Task<PersonViewModel> GetByIdAsync(string id);
        Task<Result> InsertAsync(PersonViewModel viewModel);
        Task<Result> UpdateAsync(PersonViewModel viewModel);
        Task<Result> DeleteAsync(PersonViewModel viewModel);
    }
}
