﻿using System;
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

namespace lab.LocalCosmosDbApp.Controllers
{
    public class ToolInfoApproverSourceController : BaseController
    {
        #region Global Variable Declaration
        private readonly ILogger<ToolInfoApproverSourceController> _logger;
        private static IToolInfoApproverSourceManager _iToolInfoApproverSourceManager;
        #endregion

        #region Constructor
        public ToolInfoApproverSourceController(IToolInfoApproverSourceManager iToolInfoApproverSourceManager)
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            _logger = loggerFactory.CreateLogger<ToolInfoApproverSourceController>();
            _iToolInfoApproverSourceManager = iToolInfoApproverSourceManager;
        }
        #endregion

        #region Actions

        // GET: ToolInfoApproverSource
        public async Task<IActionResult> Index()
        {
            try
            {
                var viewModelList = await _iToolInfoApproverSourceManager.GetToolInfoApproverSourcesAsync();
                return View(viewModelList);
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        [HttpGet]
        [ResponseCache(NoStore = true, Duration = 0)]
        public async Task<IActionResult> GetDataAsync(IDataTablesRequest request)
        {
            try
            {
                DataTablesResponse response = await _iToolInfoApproverSourceManager.GetDataTablesResponseAsync(request);
                return new DataTablesJsonResult(response, true);
            }
            catch (Exception ex)
            {
                return ErrorPartialView(ex);
            }
        }

        // GET: ToolInfoApproverSource/Details/5
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var viewModel = await _iToolInfoApproverSourceManager.GetToolInfoApproverSourceAsync(id);
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

        // GET: ToolInfoApproverSource/Create
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

        // POST: ToolInfoApproverSource/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToolInfoApproverSourceViewModel personViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _iToolInfoApproverSourceManager.InsertToolInfoApproverSourceAsync(personViewModel);
                    return RedirectToAction(nameof(Index));
                }
                return View(personViewModel);
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        // GET: ToolInfoApproverSource/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var viewModel = await _iToolInfoApproverSourceManager.GetToolInfoApproverSourceAsync(id);
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

        // POST: ToolInfoApproverSource/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ToolInfoApproverSourceViewModel personViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _iToolInfoApproverSourceManager.UpdateToolInfoApproverSourceAsync(personViewModel);
                    return RedirectToAction(nameof(Index));
                }
                return View(personViewModel);
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        // GET: ToolInfoApproverSource/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var viewModel = await _iToolInfoApproverSourceManager.GetToolInfoApproverSourceAsync(id);
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

        // POST: ToolInfoApproverSource/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var viewModel = await _iToolInfoApproverSourceManager.GetToolInfoApproverSourceAsync(id);
                if (viewModel == null)
                {
                    return NotFound();
                }

                await _iToolInfoApproverSourceManager.DeleteToolInfoApproverSourceAsync(viewModel.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        private async Task<bool> ToolInfoApproverSourceViewModelExists(string id)
        {
            var viewModel = await _iToolInfoApproverSourceManager.GetToolInfoApproverSourceAsync(id);
            return viewModel != null ? true : false;
        }

        #endregion
    }
}
