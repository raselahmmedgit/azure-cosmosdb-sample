using AutoMapper;
using lab.LocalCosmosDbApp.EntityModels;
using lab.LocalCosmosDbApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab.LocalCosmosDbApp.Utility
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Person, PersonViewModel>();
            CreateMap<PersonViewModel, Person>();

            CreateMap<ToolInfoApproverSource, ToolInfoApproverSourceViewModel>();
            CreateMap<ToolInfoApproverSourceViewModel, ToolInfoApproverSource>();

            CreateMap<ToolProfile, ToolProfileViewModel>();
            CreateMap<ToolProfileViewModel, ToolProfile>();

            CreateMap<EHSAssignment, EHSAssignmentViewModel>();
            CreateMap<EHSAssignmentViewModel, EHSAssignment>();

            CreateMap<BusinessUnitToolInfo, BusinessUnitToolInfoViewModel>();
            CreateMap<BusinessUnitToolInfoViewModel, BusinessUnitToolInfo>();

            CreateMap<BusinessUnitToolInfoViewModel, BusinessUnitToolInfoCsvModel>();
            CreateMap<BusinessUnitToolInfoCsvModel, BusinessUnitToolInfoViewModel>();
        }
    }

}
