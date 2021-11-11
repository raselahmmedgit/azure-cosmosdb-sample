using lab.LocalCosmosDbApp.Extensions;
using lab.LocalCosmosDbApp.Managers;
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

                //services.AddMvc(
                //   options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())
                //);
                //call this in case you need aspnet-user-authtype/aspnet-user-identity
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

                services.AddScoped<IEmailSenderManager, EmailSenderManager>();
                services.AddScoped<IPersonManager, PersonManager>();

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
