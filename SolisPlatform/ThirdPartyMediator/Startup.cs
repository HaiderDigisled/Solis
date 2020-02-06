using Data.Contracts;
using Data.Contracts.GoodWee;
using Data.Contracts.GrowWatt;
using Data.Contracts.SunGrow;
using Data.Repository;
using Data.Repository.GoodWee;
using Data.Repository.GrowWatt;
using Data.Repository.SunGrow;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Miscellaneous.Foundation;
using Services;
using Services.Mediator;
using System.IO;

namespace ThirdPartyMediator
{
    public class Startup
    {
        IConfigurationRoot Configuration;
        public Startup()
        {
            var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(Configuration["ConnectionStrings:HangfireConnection"], new SqlServerStorageOptions
            {
                //CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                //SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                //QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                UsePageLocksOnDequeue = true,
                DisableGlobalLocks = true
            }));

            Settings.Database.Connection = Configuration["ConnectionStrings:HangfireConnection"];
            Settings.FailureAlerts.InternalRecipients = Configuration["Alerts:Failure:InternalRecipients"];
            // service DI
            services.AddScoped<IServiceHandler, ServiceHandler>();

            // handlers DI
            services.AddScoped<IDailyJob, DailyJob>();

            // repository DI
            services.AddScoped<IGraphRepository, GraphRepository>();
            services.AddScoped<IVendorRepository, VendorRepository>();
            services.AddScoped<ISunGrowRepository, SunGrowRepository>();
            services.AddScoped<IGrowWattRepository, GrowWattRepository>();
            services.AddScoped<IGoodWeeRepository, GoodWeeRepository>();
            services.AddScoped<IMiscRepository, MiscRepository>();
            
            // Add the processing server as IHostedService
            services.AddHangfireServer();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHangfireDashboard();
            app.UseMvc();
        }
    }
}
