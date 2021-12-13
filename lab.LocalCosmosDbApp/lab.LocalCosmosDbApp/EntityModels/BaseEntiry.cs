using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.EntityModels
{
    public class BaseEntiry
    {
        public string Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public bool IsDeleted {get;set;}
        public DateTime? DeletedDateTime { get; set; }

        [NotMapped]
        public int TotalRecord { get; set; }
    }
}
