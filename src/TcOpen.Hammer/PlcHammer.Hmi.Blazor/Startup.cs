using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.JSInterop;
using PlcHammer.Hmi.Blazor.Data;
using PlcHammer.Hmi.Blazor.Security;
using PlcHammer.Hmi.Blazor.Shared;
using PlcHammerConnector;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TcoCore;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Data.Json;
using TcOpen.Inxton.Data.MongoDb;
using TcOpen.Inxton.Local.Security;
using TcOpen.Inxton.Local.Security.Blazor;
using TcOpen.Inxton.Local.Security.Blazor.Extension;
using TcOpen.Inxton.Local.Security.Blazor.Services;
using TcOpen.Inxton.Local.Security.Blazor.Users;
using TcOpen.Inxton.TcoCore.Blazor.Extensions;
using Vortex.Presentation.Blazor.Services;
using TcOpen.Inxton.Security;

namespace PlcHammer.Hmi.Blazor
{
    public class Startup
    {
<<<<<<< HEAD
        private BlazorRoleGroupManager roleGroupManager;
        private bool mongoDB = true;

=======
>>>>>>> dev
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static StringWriter logMessages = new StringWriter();

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            IRepository<UserData> userRepo;
            IRepository<GroupData> groupRepo;
            BlazorRoleGroupManager roleGroupManager;

            services.AddRazorPages();
            services.AddServerSideBlazor()
                .AddHubOptions(hub => hub.MaximumReceiveMessageSize = 100 * 1024 * 1024);

            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddVortexBlazorServices();

            services.AddTcoCoreExtensions();

            if (true)/*Mongo database*/
            {
                (userRepo, groupRepo) = SetUpMongoDatabase();
            }
            else /*Json repositories*/
            {
                (userRepo, groupRepo) = SetUpJsonRepositories();
            }

            roleGroupManager = new BlazorRoleGroupManager(groupRepo);
            Roles.Create(roleGroupManager);

<<<<<<< HEAD
            services.AddVortexBlazorSecurity(userRepo, groupRepo, roleGroupManager);

            services.AddTcoCoreExtensions();
            if (mongoDB)/*Mongo repositories for data*/
            {
                SetUpMongoDatabase();
            }
            else /*Json repositories for data*/
            {
                SetUpJsonRepositories();
            }

            // App setup
            TcOpen.Inxton.TcoAppDomain.Current.Builder
                .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter(new LoggerConfiguration()
                                        //.WriteTo.TextWriter(logMessages)
                                        .WriteTo.Console()        // This will write log into application console.  
                                        .WriteTo.Notepad()        // This will write logs to first instance of notepad program.
                                                                  // uncomment this to send logs over MQTT, to receive the data run MQTTTestClient from this solution.
                                                                  // .WriteTo.MQTT(new MQTTnet.Client.Options.MqttClientOptionsBuilder().WithTcpServer("broker.emqx.io").Build(), "fun_with_TcOpen_Hammer") 
                                        .Enrich.WithProperty("user",/*_signInManager.Context.User*/ TcOpen.Inxton.Local.Security.SecurityManager.Manager.Principal.Identity.Name)
                                        .Enrich.With(new Serilog.Enrichers.EnvironmentNameEnricher())
                                        .Enrich.With(new Serilog.Enrichers.EnvironmentUserNameEnricher())
                                        .Enrich.With(new Serilog.Enrichers.MachineNameEnricher())
                                        .MinimumLevel.Verbose())) // Sets the logger configuration (default reports only to console).
                .SetSecurity(SecurityManager.Manager.Service)
                .SetEditValueChangeLogging(Entry.PlcHammer.Connector);

            // Initialize logger
            Entry.PlcHammer.TECH_MAIN._app._logger.StartLoggingMessages(TcoCore.eMessageCategory.Info);
=======
            services.AddVortexBlazorSecurity(userRepo, roleGroupManager);
>>>>>>> dev
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            Entry.PlcHammer.Connector.BuildAndStart();
        }

        private static void SetUpRepositories(IRepository<PlainStation001_ProductionData> processRecipiesRepository,
                                             IRepository<PlainStation001_ProductionData> processTraceabiltyRepository,
                                             IRepository<PlainStation001_TechnologicalSettings> technologyDataRepository)
        {
            Entry.PlcHammer.TECH_MAIN._app._station001._processRecipies.InitializeRepository(processRecipiesRepository);
            Entry.PlcHammer.TECH_MAIN._app._station001._processRecipies.InitializeRemoteDataExchange();

            Entry.PlcHammer.TECH_MAIN._app._station001._processTraceabilty.InitializeRepository(processTraceabiltyRepository);
            Entry.PlcHammer.TECH_MAIN._app._station001._processTraceabilty.InitializeRemoteDataExchange();

            Entry.PlcHammer.TECH_MAIN._app._station001._technologicalDataManager.InitializeRepository(technologyDataRepository);
            Entry.PlcHammer.TECH_MAIN._app._station001._technologicalDataManager.InitializeRemoteDataExchange();
        }

        private static (IRepository<UserData> userRepo, IRepository<GroupData> groupRepo) SetUpJsonRepositories()
        {
            var executingAssemblyFile = new FileInfo(Assembly.GetExecutingAssembly().Location);
            var repositoryDirectory = Path.GetFullPath($"{executingAssemblyFile.Directory}..\\..\\..\\..\\..\\JSONREPOS\\");

            if (!Directory.Exists(repositoryDirectory))
            {
                Directory.CreateDirectory(repositoryDirectory);
            }

            /*Data*/
            var processRecipiesRepository = new JsonRepository<PlainStation001_ProductionData>(new JsonRepositorySettings<PlainStation001_ProductionData>(Path.Combine(repositoryDirectory, "ProcessSettings")));
            var processTraceabiltyRepository = new JsonRepository<PlainStation001_ProductionData>(new JsonRepositorySettings<PlainStation001_ProductionData>(Path.Combine(repositoryDirectory, "Traceability")));
            var technologyDataRepository = new JsonRepository<PlainStation001_TechnologicalSettings>(new JsonRepositorySettings<PlainStation001_TechnologicalSettings>(Path.Combine(repositoryDirectory, "TechnologicalSettings")));

            SetUpRepositories(processRecipiesRepository, processTraceabiltyRepository, technologyDataRepository);

            /*Security*/
            IRepository<UserData> userRepo = new JsonRepository<UserData>(new JsonRepositorySettings<UserData>(Path.Combine(repositoryDirectory, "UsersBlazor")));
            IRepository<GroupData> groupRepo = new JsonRepository<GroupData>(new JsonRepositorySettings<GroupData>(Path.Combine(repositoryDirectory, "GroupsBlazor")));
            return (userRepo, groupRepo);
        }

        private static (IRepository<UserData> userRepo, IRepository<GroupData> groupRepo) SetUpMongoDatabase()
        {
            var mongoUri = "mongodb://localhost:27017";
            var databaseName = "Hammer";

            /*Data*/
            var processRecipiesRepository = new MongoDbRepository<PlainStation001_ProductionData>(new MongoDbRepositorySettings<PlainStation001_ProductionData>(mongoUri, databaseName, "ProcessSettings"));
            var processTraceabiltyRepository = new MongoDbRepository<PlainStation001_ProductionData>(new MongoDbRepositorySettings<PlainStation001_ProductionData>(mongoUri, databaseName, "Traceability"));
            var technologyDataRepository = new MongoDbRepository<PlainStation001_TechnologicalSettings>(new MongoDbRepositorySettings<PlainStation001_TechnologicalSettings>(mongoUri, databaseName, "TechnologicalSettings"));

            SetUpRepositories(processRecipiesRepository, processTraceabiltyRepository, technologyDataRepository);

            /*Security*/
            IRepository<UserData> userRepo = new MongoDbRepository<UserData>(new MongoDbRepositorySettings<UserData>(mongoUri, "HammerBlazor", "Users"));
            IRepository<GroupData> groupRepo = new MongoDbRepository<GroupData>(new MongoDbRepositorySettings<GroupData>(mongoUri, "HammerBlazor", "Groups"));
            return (userRepo, groupRepo);
        }
    }
}
