using System;

namespace lab.LocalCosmosDbApp.Config
{
    public class AppDbConnectionConfig
    {
        public string EndPointUrl { get; set; }
        public string AuthKey { get; set; }
        public string DatabaseId { get; set; }

        public string AspNetIdentityUsers { get; set; }
        public string AspNetIdentityRoles { get; set; }
        public string AspNetIdentityUserRoles { get; set; }
        public bool IsDatabaseCreate { get; set; }
        public bool IsTableCreate { get; set; }
        public bool IsMasterDataInsert { get; set; }
    }
}
