using System;

namespace lab.LocalCosmosDbApp.Config
{
    public class AppDbConnectionConfig
    {
        public static string Name = "AppDbConnectionConfig";
        public string EndPointUrl { get; set; }
        public string AuthKey { get; set; }
        public string DatabaseName { get; set; }
        public string ContainerName { get; set; }
        public string AspNetIdentityContainerName { get; set; }
        public bool IsDatabaseCreate { get; set; }
        public bool IsMasterDataInsert { get; set; }
    }
}
