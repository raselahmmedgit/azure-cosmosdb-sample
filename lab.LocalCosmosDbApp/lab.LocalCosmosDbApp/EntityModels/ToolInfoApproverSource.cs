using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.EntityModels
{
    public class ToolInfoApproverSource
    {
        public ToolInfoApproverSource()
        {
            ToolProfile = new ToolProfile();
            EHSAssignment = new EHSAssignment();
        }
        public string Id { get; set; }
        public string ToolInfoApproverSourceId { get; set; }
        public string Building { get; set; }
        public string BU { get; set; }
        public string KPU { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public ToolProfile ToolProfile { get; set; }
        public EHSAssignment EHSAssignment { get; set; }
    }

    public class ToolProfile
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

    public class EHSAssignment
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
