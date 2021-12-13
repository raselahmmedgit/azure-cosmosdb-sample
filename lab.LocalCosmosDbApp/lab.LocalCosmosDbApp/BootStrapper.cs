using DataTables.AspNet.AspNetCore;
using lab.LocalCosmosDbApp.Config;
using lab.LocalCosmosDbApp.Data;
using lab.LocalCosmosDbApp.Extensions;
using lab.LocalCosmosDbApp.Managers;
using lab.LocalCosmosDbApp.Repository;
using lab.LocalCosmosDbApp.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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

                //services.AddMvc(
                //   options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())
                //);
                //call this in case you need aspnet-user-authtype/aspnet-user-identity
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

                // Add our Config object so it can be injected
                services.Configure<AppDbConnectionConfig>(configuration.GetSection(AppDbConnectionConfig.Name));
                services.Configure<AppContactUsConfig>(configuration.GetSection(AppContactUsConfig.Name));
                services.Configure<AppEmailConfig>(configuration.GetSection(AppEmailConfig.Name));
                services.Configure<AppSmsConfig>(configuration.GetSection(AppSmsConfig.Name));
                services.Configure<AppConfig>(configuration.GetSection(AppConfig.Name));
                services.Configure<SeoConfig>(configuration.GetSection(SeoConfig.Name));
                //services.AddOptions<AppDbConnectionConfig>().Configure<IConfiguration>((settings, configuration) => {
                //    configuration.GetSection(AppDbConnectionConfig.Name).Bind(settings);
                //});

                services.AddDbContextFactory<AppDbContext>(
                   (IServiceProvider iServiceProvider, DbContextOptionsBuilder dbContextOptionsBuilder) =>
                   {
                       var appDbConnectionConfig = iServiceProvider
                           .GetRequiredService<IOptions<AppDbConnectionConfig>>()
                           .Value;

                       dbContextOptionsBuilder.UseCosmos(
                           appDbConnectionConfig.EndPointUrl,
                           appDbConnectionConfig.AuthKey,
                           appDbConnectionConfig.DatabaseName, dbOptions => {

                           /*dbOptions.ConnectionMode(ConnectionMode.Direct);
                           dbOptions.LimitToEndpoint();
                           dbOptions.Region(Regions.AustraliaCentral);
                           dbOptions.GatewayModeMaxConnectionLimit(32);
                           dbOptions.MaxRequestsPerTcpConnection(8);
                           dbOptions.MaxTcpConnectionsPerEndpoint(16);
                           dbOptions.IdleTcpConnectionTimeout(TimeSpan.FromMinutes(1));
                           dbOptions.OpenTcpConnectionTimeout(TimeSpan.FromMinutes(1));
                           dbOptions.RequestTimeout(TimeSpan.FromMinutes(1));*/

                           });
                   });

                services.AddScoped<IEmailSenderManager, EmailSenderManager>();

                services.AddScoped<IAppDbInitRepository, AppDbInitRepository>();
                services.AddScoped<IAppDbInitManager, AppDbInitManager>();

                services.AddScoped<IPersonRepository, PersonRepository>();
                services.AddScoped<IPersonManager, PersonManager>();

                services.AddScoped<IToolInfoApproverSourceRepository, ToolInfoApproverSourceRepository>();
                services.AddScoped<IToolInfoApproverSourceManager, ToolInfoApproverSourceManager>();

                services.AddScoped<IBusinessUnitToolInfoRepository, BusinessUnitToolInfoRepository>();
                services.AddScoped<IBusinessUnitToolInfoManager, BusinessUnitToolInfoManager>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
