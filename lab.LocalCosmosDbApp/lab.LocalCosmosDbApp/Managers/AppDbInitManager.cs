using AutoMapper;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using lab.LocalCosmosDbApp.Config;
using lab.LocalCosmosDbApp.EntityModels;
using lab.LocalCosmosDbApp.Helpers;
using lab.LocalCosmosDbApp.Repository;
using lab.LocalCosmosDbApp.ViewModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.Managers
{
    public class AppDbInitManager : IAppDbInitManager
    {
        private readonly AppDbConnectionConfig _appDbConnectionConfig;
        private readonly IAppDbInitRepository _iAppDbInitRepository;

        public AppDbInitManager(IOptions<AppDbConnectionConfig> appDbConnectionConfig, IAppDbInitRepository iAppDbInitRepository)
        {
            _appDbConnectionConfig = appDbConnectionConfig.Value;
            _iAppDbInitRepository = iAppDbInitRepository;
        }

        public async Task<Result> InitDatabaseAndMasterDataAsync()
        {
            try
            {
                if (!_appDbConnectionConfig.IsDatabaseCreate)
                {
                    var isCreated = await CreateDatabaseIfNotExists();
                    if (isCreated)
                    {
                        if (!_appDbConnectionConfig.IsMasterDataInsert)
                        {
                            int saveChange = await CreateTableAndInsertMasterData();
                            if (saveChange > 0)
                            {
                                return Result.Ok(MessageHelper.Save);
                            }
                            else
                            {
                                return Result.Fail(MessageHelper.SaveFail);
                            }
                        }
                    }
                }

                return Result.Ok(MessageHelper.AlreadyDbExists);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CreateDatabaseIfNotExists()
        {
            try
            {
                var isCreateDatabase = await _iAppDbInitRepository.CreateDatabaseIfNotExists();
                return isCreateDatabase;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> CreateTableAndInsertMasterData()
        {
            try
            {
                #region Person
                var personList = new List<Person>();
                var person1 = new Person() { Id = "c10f6763-1c25-431f-9ceb-e8e3d7c8b88d", PersonName = "Rasel Ahmmed", EmailAddress = "raselahmmed@mail.com", DateOfBirth = new DateTime(1985, 12, 1) };
                var person2 = new Person() { Id = "e66a44e5-0072-40bc-95c6-ef76df6540eb", PersonName = "John Mia", EmailAddress = "johnmia@mail.com", DateOfBirth = new DateTime(1910, 8, 10) };
                personList.Add(person1);
                personList.Add(person2);
                #endregion

                #region Tool Info
                var toolInfoApproverSourceList = new List<ToolInfoApproverSource>();
                //001
                ToolInfoApproverSource toolInfoApproverSource1 = new ToolInfoApproverSource();
                toolInfoApproverSource1.Id = "4ebfed32-da3c-4269-8c10-a4d373b859df";
                toolInfoApproverSource1.Building = "Sample Building 001";
                toolInfoApproverSource1.BU = "Sample BU 001";
                toolInfoApproverSource1.KPU = "Sample KPU 001";
                toolInfoApproverSource1.BeginDate = new DateTime(2020, 9, 1);
                toolInfoApproverSource1.EndDate = new DateTime(2020, 9, 20);

                ToolProfile toolProfile1 = new ToolProfile();
                toolProfile1.ToolId = "b11dd969-5fcd-4d02-a727-fede48895308";
                toolProfile1.ToolName = "Glacier 001";
                toolProfile1.Bay = "Bay 001";
                toolProfile1.Lab = "Lab 001";
                toolProfile1.Room = "Room 001";
                toolProfile1.Initiator = "Demo Initiator 001";
                toolProfile1.ToolOwner = "John Doe 001";
                toolProfile1.SecondaryContact = "John Mia 001";
                toolProfile1.LabManager = "Lab Manager 001";

                EHSAssignment eHSAssignment1 = new EHSAssignment();
                eHSAssignment1.RegionSite = " RegionSite 001";
                eHSAssignment1.BuildingEnvironmental = " BuildingEnvironmental 001";
                eHSAssignment1.EnvironmentalAdditionalReviewerOne = "Good environment 001";
                eHSAssignment1.EnvironmentalAdditionalReviewerTwo = "Excellent Environment 001";
                eHSAssignment1.OccupationalSafety = "A+ Safety 001";
                eHSAssignment1.ChemAuthFacilities = "Chem is not available 001";
                eHSAssignment1.ProductSafety = "Prodduct Safety Level 5 001";
                eHSAssignment1.AdditionalEHSIH = string.Empty;

                toolInfoApproverSource1.ToolProfile = toolProfile1;
                toolInfoApproverSource1.EHSAssignment = eHSAssignment1;

                //002
                ToolInfoApproverSource toolInfoApproverSource2 = new ToolInfoApproverSource();
                toolInfoApproverSource2.Id = "58908831-6b91-412e-8cb1-1f5ef11069c3";
                toolInfoApproverSource2.Building = "Sample Building 002";
                toolInfoApproverSource2.BU = "Sample BU 002";
                toolInfoApproverSource2.KPU = "Sample KPU 002";
                toolInfoApproverSource2.BeginDate = new DateTime(2021, 11, 1);
                toolInfoApproverSource2.EndDate = new DateTime(2021, 11, 20);

                ToolProfile toolProfile2 = new ToolProfile();
                toolProfile2.ToolId = "3f52b23b-54e4-4560-8c7a-1d6703cac615";
                toolProfile2.ToolName = "Glacier 002";
                toolProfile2.Bay = "Bay 002";
                toolProfile2.Lab = "Lab 002";
                toolProfile2.Room = "Room 002";
                toolProfile2.Initiator = "Demo Initiator 002";
                toolProfile2.ToolOwner = "John Doe 002";
                toolProfile2.SecondaryContact = "John Mia 002";
                toolProfile2.LabManager = "Lab Manager 002";

                EHSAssignment eHSAssignment2 = new EHSAssignment();
                eHSAssignment2.RegionSite = " RegionSite 002";
                eHSAssignment2.BuildingEnvironmental = " BuildingEnvironmental 002";
                eHSAssignment2.EnvironmentalAdditionalReviewerOne = "Good environment 002";
                eHSAssignment2.EnvironmentalAdditionalReviewerTwo = "Excellent Environment 002";
                eHSAssignment2.OccupationalSafety = "A+ Safety 002";
                eHSAssignment2.ChemAuthFacilities = "Chem is not available 002";
                eHSAssignment2.ProductSafety = "Prodduct Safety Level 5 002";
                eHSAssignment2.AdditionalEHSIH = string.Empty;

                toolInfoApproverSource2.ToolProfile = toolProfile2;
                toolInfoApproverSource2.EHSAssignment = eHSAssignment2;

                toolInfoApproverSourceList.Add(toolInfoApproverSource1);
                toolInfoApproverSourceList.Add(toolInfoApproverSource2);

                #endregion

                var saveChange = await _iAppDbInitRepository.CreateTableAndInsertMasterData(personList, toolInfoApproverSourceList);

                return saveChange;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public interface IAppDbInitManager
    {
        Task<Result> InitDatabaseAndMasterDataAsync();
        Task<bool> CreateDatabaseIfNotExists();
        Task<int> CreateTableAndInsertMasterData();
    }
}
