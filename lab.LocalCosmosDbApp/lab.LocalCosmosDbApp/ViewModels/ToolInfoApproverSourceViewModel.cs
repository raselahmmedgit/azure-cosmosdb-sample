using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.ViewModels
{
    public class ToolInfoApproverSourceViewModel
    {
        public ToolInfoApproverSourceViewModel()
        {
            ToolProfileViewModel = new ToolProfileViewModel();
            EHSAssignmentViewModel = new EHSAssignmentViewModel();
        }
        public string Id { get; set; }
        public string Building { get; set; }
        public string BU { get; set; }
        public string KPU { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public ToolProfileViewModel ToolProfileViewModel { get; set; }
        public EHSAssignmentViewModel EHSAssignmentViewModel { get; set; }
    }

    public class ToolProfileViewModel
    {
        public string ToolId { get; set; }
        public string ToolName { get; set; }
        public string Bay { get; set; }
        public string Lab { get; set; }
        public string Room { get; set; }
        public string Initiator { get; set; }
        public string ToolOwner { get; set; }
        public string SecondaryContact { get; set; }
        public string LabManager { get; set; }
    }

    public class EHSAssignmentViewModel
    {
        //public EHSAssignment()
        //{
        //    ManagementApprovers = new List<string>();
        //    BuildingEnvironmentalBackups = new List<string>();
        //    OccupationalSafetyBackups = new List<string>();
        //    ChemAuthFacilitiesBackups = new List<string>();
        //    ProductSafetyBackups = new List<string>();
        //    AdditionalEHSIHBackups = new List<string>();
        //}

        public string RegionSite { get; set; }

        //public List<string> ManagementApprovers { get; set; }
        public string BuildingEnvironmental { get; set; }
        //public List<string> BuildingEnvironmentalBackups { get; set; }
        public string EnvironmentalAdditionalReviewerOne { get; set; }
        public string EnvironmentalAdditionalReviewerTwo { get; set; }
        public string OccupationalSafety { get; set; }
        //public List<string> OccupationalSafetyBackups { get; set; }
        public string ChemAuthFacilities { get; set; }
        //public List<string> ChemAuthFacilitiesBackups { get; set; }
        public string ProductSafety { get; set; }
        //public List<string> ProductSafetyBackups { get; set; }
        public string AdditionalEHSIH { get; set; }
        //public List<string> AdditionalEHSIHBackups { get; set; }
    }
}
