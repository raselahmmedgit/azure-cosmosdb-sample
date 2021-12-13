using System;

namespace lab.LocalCosmosDbApp.ViewModels
{
    public class ObjectRowViewModel
    {
        public string ColumnName { get; set; }
        public decimal ColumnValue { get; set; }
        public DateTime ColumnDate { get; set; }
    }
}
