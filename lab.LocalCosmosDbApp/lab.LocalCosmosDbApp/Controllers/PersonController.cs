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

namespace lab.LocalCosmosDbApp.Controllers
{
    public class PersonController : BaseController
    {
        #region Global Variable Declaration
        private readonly ILogger<PersonController> _logger;
        private static IPersonManager _iPersonManager;
        #endregion

        #region Constructor
        public PersonController(IPersonManager iPersonManager)
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            _logger = loggerFactory.CreateLogger<PersonController>();
            _iPersonManager = iPersonManager;
        }
        #endregion

        #region Actions

        // GET: Person
        public async Task<IActionResult> Index()
        {
            try
            {
                var viewModelList = await _iPersonManager.GetAllAsync();
                return View(viewModelList);
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var viewModel = await _iPersonManager.GetByIdAsync(id);
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

        // GET: Person/Create
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

        // POST: Person/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonViewModel personViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _iPersonManager.InsertAsync(personViewModel);
                    return RedirectToAction(nameof(Index));
                }
                return View(personViewModel);
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var viewModel = await _iPersonManager.GetByIdAsync(id);
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

        // POST: Person/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, PersonViewModel personViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _iPersonManager.UpdateAsync(personViewModel);
                    return RedirectToAction(nameof(Index));
                }
                return View(personViewModel);
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var viewModel = await _iPersonManager.GetByIdAsync(id);
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

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var viewModel = await _iPersonManager.GetByIdAsync(id);
                if (viewModel == null)
                {
                    return NotFound();
                }

                await _iPersonManager.DeleteAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        private async Task<bool> PersonViewModelExists(string id)
        {
            var viewModel = await _iPersonManager.GetByIdAsync(id);
            return viewModel != null ? true : false;
        }

        #endregion
    }
}
