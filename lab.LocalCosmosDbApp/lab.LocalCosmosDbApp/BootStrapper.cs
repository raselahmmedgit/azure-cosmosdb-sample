using DataTables.AspNet.AspNetCore;
using lab.LocalCosmosDbApp.Data;
using lab.LocalCosmosDbApp.Extensions;
using lab.LocalCosmosDbApp.Managers;
using lab.LocalCosmosDbApp.Repository;
using lab.LocalCosmosDbApp.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace lab.LocalCosmosDbApp
{
    public class BootStrapper
    {
        public static void Run(IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                services.AddRouting(options => options.LowercaseUrls = true);

                services.RegisterCustomRoute();

                // Add functionality to inject IOptions<T>
                services.AddOptions();

                services.AddCors();

                #region MemoryCache
                //services.AddDistributedMemoryCache();
                //services.AddMemoryCache();
                #endregion

                services.RegisterAutoMapper();
                services.RegisterDataTables();

                // Initializes Database and Master Data.
                InitializeDatabaseAndMasterDataAsync(configuration);

                //services.AddMvc(
                //   options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())
                //);
                //call this in case you need aspnet-user-authtype/aspnet-user-identity
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

                services.AddScoped<IEmailSenderManager, EmailSenderManager>();
                services.AddScoped<IPersonRepository, PersonRepository>();
                services.AddScoped<IPersonManager, PersonManager>();

                services.AddScoped<IToolInfoApproverSourceRepository, ToolInfoApproverSourceRepository>();
                services.AddScoped<IToolInfoApproverSourceManager, ToolInfoApproverSourceManager>();

            }
            catch (Exception)
            {
                throw;
            }

        }

        private static void InitializeDatabaseAndMasterDataAsync(IConfiguration configuration)
        {
            try
            {
                var isDatabaseCreate = configuration["AppDbConnectionConfig:IsDatabaseCreate"] == null ? true : bool.Parse(configuration["AppDbConnectionConfig:IsDatabaseCreate"].ToString());
                var isMasterDataInsert = configuration["AppDbConnectionConfig:IsMasterDataInsert"] == null ? true : bool.Parse(configuration["AppDbConnectionConfig:IsMasterDataInsert"].ToString());
                if (!isDatabaseCreate)
                {
                    using (var context = new AppDbContext())
                    {
                        if (AppDbContextInitializer.CreateDatabaseIfNotExists())
                        {
                            if (!isMasterDataInsert)
                            {
                                AppDbContextInitializer.MasterData();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
