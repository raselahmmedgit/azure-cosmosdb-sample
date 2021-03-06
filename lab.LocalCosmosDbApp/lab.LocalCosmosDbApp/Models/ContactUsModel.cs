using System.ComponentModel.DataAnnotations;

namespace lab.LocalCosmosDbApp.Models
{
    public class ContactUsModel : CaptchaModel
    {
        [Required]
        [EmailAddress]
        public string ContactEmail { get; set; }
        
        [Required] 
        public string ContactName { get; set; }
        
        [Required] 
        public string ContactPhone { get; set; }
        
        [Required]
        public string ContactSubject { get; set; }
        
        [Required] 
        public string ContactMessage { get; set; }

        [Required]
        public string RecipientEmail { get; set; }
    }
}
