using AutoMapper;
using lab.LocalCosmosDbApp.EntityModels;
using lab.LocalCosmosDbApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab.LocalCosmosDbApp.Utility
{
    public class DefaultMappingProfile : Profile
    {
        public DefaultMappingProfile()
        {
            CreateMap<Persons, PersonViewModel>();
            CreateMap<PersonViewModel, Persons>();
        }
    }

}
