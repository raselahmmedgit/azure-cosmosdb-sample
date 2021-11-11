using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab.LocalCosmosDbApp.ViewModels
{
    public class PersonViewModel
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [DisplayName("Age")]
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }

    }
}
