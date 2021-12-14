using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab.LocalCosmosDbApp.ViewModels;
using Microsoft.Extensions.Logging;
using lab.LocalCosmosDbApp.Managers;
using DataTables.AspNet.Core;
using DataTables.AspNet.AspNetCore;
using lab.LocalCosmosDbApp.Exceptions;
using lab.LocalCosmosDbApp.Helpers;

namespace lab.LocalCosmosDbApp.Controllers
{
    public class BusinessUnitToolInfoController : BaseController
    {
        #region Global Variable Declaration
        private readonly ILogger<BusinessUnitToolInfoController> _logger;
        private static IBusinessUnitToolInfoManager _iBusinessUnitToolInfoManager;
        #endregion

        #region Constructor
        public BusinessUnitToolInfoController(IBusinessUnitToolInfoManager iBusinessUnitToolInfoManager)
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            _logger = loggerFactory.CreateLogger<BusinessUnitToolInfoController>();
            _iBusinessUnitToolInfoManager = iBusinessUnitToolInfoManager;
        }
        #endregion

        #region Actions

        // GET: BusinessUnitToolInfo
        public async Task<IActionResult> Index()
        {
            try
            {
                //var viewModelList = await _iBusinessUnitToolInfoManager.GetBusinessUnitToolInfosAsync();
                //return View(viewModelList);

                return View("DataTable");
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        [HttpGet]
        [ResponseCache(NoStore = true, Duration = 0)]
        public async Task<IActionResult> GetDataAjax(IDataTablesRequest request)
        {
            try
            {
                DataTablesResponse response = await _iBusinessUnitToolInfoManager.GetDataTablesResponseAsync(request);
                return new DataTablesJsonResult(response, true);
            }
            catch (Exception ex)
            {
                return ErrorPartialView(ex);
            }
        }

        // GET: BusinessUnitToolInfo/Details/5
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var viewModel = await _iBusinessUnitToolInfoManager.GetBusinessUnitToolInfoAsync(id);
                if (viewModel == null)
                {
                    return NotFound();
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        // GET: BusinessUnitToolInfo/Create
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        // POST: BusinessUnitToolInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BusinessUnitToolInfoViewModel personViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _iBusinessUnitToolInfoManager.InsertBusinessUnitToolInfoAsync(personViewModel);
                    return RedirectToAction(nameof(Index));
                }
                return View(personViewModel);
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        // GET: BusinessUnitToolInfo/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var viewModel = await _iBusinessUnitToolInfoManager.GetBusinessUnitToolInfoAsync(id);
                if (viewModel == null)
                {
                    return NotFound();
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        // POST: BusinessUnitToolInfo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, BusinessUnitToolInfoViewModel personViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _iBusinessUnitToolInfoManager.UpdateBusinessUnitToolInfoAsync(personViewModel);
                    return RedirectToAction(nameof(Index));
                }
                return View(personViewModel);
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        // GET: BusinessUnitToolInfo/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var viewModel = await _iBusinessUnitToolInfoManager.GetBusinessUnitToolInfoAsync(id);
                if (viewModel == null)
                {
                    return NotFound();
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        // POST: BusinessUnitToolInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var viewModel = await _iBusinessUnitToolInfoManager.GetBusinessUnitToolInfoAsync(id);
                if (viewModel == null)
                {
                    return NotFound();
                }

                await _iBusinessUnitToolInfoManager.DeleteBusinessUnitToolInfoAsync(viewModel.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        private async Task<bool> BusinessUnitToolInfoViewModelExists(string id)
        {
            var viewModel = await _iBusinessUnitToolInfoManager.GetBusinessUnitToolInfoAsync(id);
            return viewModel != null ? true : false;
        }

        [HttpGet]
        [ResponseCache(NoStore = true, Duration = 0)]
        public async Task<IActionResult> AddAjax()
        {
            try
            {
                var viewModel = new BusinessUnitToolInfoViewModel();
                if (viewModel != null)
                {
                    return PartialView("~/Views/BusinessUnitToolInfo/_AddOrEdit.cshtml", viewModel);
                }
                else
                {
                    return ErrorPartialView(ExceptionHelper.ExceptionErrorMessageForNullObject());
                }
            }
            catch (Exception ex)
            {
                return ErrorPartialView(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditAjax(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var viewModel = await _iBusinessUnitToolInfoManager.GetBusinessUnitToolInfoBySqlQueryAsync(id);
                    if (viewModel != null)
                    {
                        //return PartialView("_AddOrEdit", viewModel);
                        return PartialView("~/Views/BusinessUnitToolInfo/_AddOrEdit.cshtml", viewModel);
                    }
                    else
                    {
                        return ErrorPartialView(ExceptionHelper.ExceptionErrorMessageForNullObject());
                    }
                }
                else
                {
                    return ErrorPartialView(ExceptionHelper.ExceptionErrorMessageForNullObject());
                }
            }
            catch (Exception ex)
            {
                return ErrorPartialView(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetailsAjax(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var viewModel = await _iBusinessUnitToolInfoManager.GetBusinessUnitToolInfoBySqlQueryAsync(id);
                    if (viewModel != null)
                    {
                        //return PartialView("_Details", viewModel);
                        return PartialView("~/Views/BusinessUnitToolInfo/_Details.cshtml", viewModel);
                    }
                    else
                    {
                        return ErrorPartialView(ExceptionHelper.ExceptionErrorMessageForNullObject());
                    }
                }
                else
                {
                    return ErrorPartialView(ExceptionHelper.ExceptionErrorMessageForNullObject());
                }
            }
            catch (Exception ex)
            {
                return ErrorPartialView(ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAjax(BusinessUnitToolInfoViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(viewModel.Id)) //add
                    {
                        viewModel.Id = Guid.NewGuid().ToString();
                        _result = await _iBusinessUnitToolInfoManager.InsertBusinessUnitToolInfoAsync(viewModel);
                    }
                    else if (!string.IsNullOrEmpty(viewModel.Id)) //edit
                    {
                        _result = await _iBusinessUnitToolInfoManager.UpdateBusinessUnitToolInfoAsync(viewModel);
                    }
                }
                else
                {
                    _result = Result.Fail(ExceptionHelper.ModelStateErrorFirstFormat(ModelState));
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(LogMessageHelper.FormateMessageForException(ex, "SaveAjax[POST]"));
                _result = Result.Fail(MessageHelper.UnhandledError);
            }

            return JsonResult(_result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAjax(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    _result = await _iBusinessUnitToolInfoManager.DeleteBusinessUnitToolInfoAsync(id);
                }
                else
                {
                    _result = Result.Fail(MessageHelper.DeleteFail);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LogMessageHelper.FormateMessageForException(ex, "DeleteAjax[POST]"));
                _result = Result.Fail(MessageHelper.UnhandledError);
            }

            return JsonResult(_result);
        }


        #endregion
    }
}
