using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab.LocalCosmosDbApp.ViewModels
{
    public class PersonViewModel
    {
        [Key]
        public string Id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(200)]
        public string PersonName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(200)]
        public string EmailAddress { get; set; }

        [DisplayName("Date Of Birth")]
        [Required(ErrorMessage = "Date Of Birth is required")]
        public DateTime DateOfBirth { get; set; }

    }
}
