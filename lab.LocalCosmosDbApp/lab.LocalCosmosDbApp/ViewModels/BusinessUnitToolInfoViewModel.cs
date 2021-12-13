using System;
using System.Collections.Generic;

namespace lab.LocalCosmosDbApp.ViewModels
{
    public class BusinessUnitToolInfoViewModel
    {
        public string Id { get; set; }
        public string BusinessUnitToolInfoId { get; set; }
        public string BU { get; set; }
        public string FiscalQuarter { get; set; }
        public string ToolIndex { get; set; }
        public string Tool { get; set; }
        public string Building { get; set; }
        public string Platform { get; set; }
        public string Products { get; set; }
        public string Applications { get; set; }
        public string ToolCount { get; set; }
        public string BaysInUse { get; set; }
        public string BaysNeeded { get; set; }
        public string InstalledChambers { get; set; }
        public string PossibleChambers { get; set; }
        public string BaySize { get; set; }

        public IEnumerable<BusinessUnitToolInfoCustomFieldViewModel>? CustomFields { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }

        public int TotalRecord { get; set; }
    }

    public class BusinessUnitToolInfoCustomFieldViewModel
    {
        //[Required(ErrorMessage = "Custom Field is required.")]
        //[StringLength(maximumLength: 200, ErrorMessage = "Cannot be more than 200 characters")]
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
