using System.ComponentModel.DataAnnotations.Schema;

namespace lab.LocalCosmosDbApp.ViewModels
{
    public class BaseViewModel
    {
        [NotMapped]
        public string User { get; set; }
        [NotMapped]
        public string Role { get; set; }
        [NotMapped]
        public string IsCardView { get; set; }
        [NotMapped]
        public string IsListView { get; set; }
    }
}
