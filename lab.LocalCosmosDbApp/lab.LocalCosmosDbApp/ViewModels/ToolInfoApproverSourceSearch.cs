using System;

namespace lab.LocalCosmosDbApp.ViewModels
{
    public class ToolInfoApproverSourceSearch
    {
        public string ToolInfoApproverSourceId { get; set; }
        public string Building { get; set; }
        public string BU { get; set; }
        public string KPU { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        //ToolProfile
        public string ToolId { get; set; }
        public string ToolName { get; set; }
        public string Bay { get; set; }
        public string Lab { get; set; }
        public string Room { get; set; }
        public string Initiator { get; set; }
        public string ToolOwner { get; set; }
        public string SecondaryContact { get; set; }
        public string LabManager { get; set; }

        //EHSAssignment
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
