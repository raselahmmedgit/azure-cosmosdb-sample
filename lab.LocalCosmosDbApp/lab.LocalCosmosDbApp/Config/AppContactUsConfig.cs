using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.Config
{
    public class AppContactUsConfig
    {
        public static string Name = "AppContactUsConfig";
        public string EmailAddress { get; set; }
        public string EmailAddressDisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberDisplayName { get; set; }
    }
}
