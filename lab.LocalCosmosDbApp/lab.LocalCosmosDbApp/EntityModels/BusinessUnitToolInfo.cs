using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.EntityModels
{
    public class BusinessUnitToolInfo : BaseEntiry
    {
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
        //public List<BusinessUnitToolInfoCustomField>? CustomFields { get; set; }
    }
    public class BusinessUnitToolInfoCustomField
    {
        //[Required(ErrorMessage = "Custom Field is required.")]
        //[StringLength(maximumLength: 200, ErrorMessage = "Cannot be more than 200 characters")]
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }

}
