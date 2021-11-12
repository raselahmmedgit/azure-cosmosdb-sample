using Microsoft.AspNetCore.Identity;

namespace lab.LocalCosmosDbApp.Identity
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole() { }

        public bool IsActive { get; set; }
    }
}
