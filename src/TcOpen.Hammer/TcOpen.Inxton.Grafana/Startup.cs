using System;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using TcOpenHammer.Grafana.API.Model.Mongo;

namespace TcOpenHammer.Grafana.API
{
    /// <summary>
    /// Defines dependencies and the way they are instiantied.
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(c =>
            {
                c.AddSimpleConsole(options =>
                {
                    options.ColorBehavior = Microsoft
                        .Extensions
                        .Logging
                        .Console
                        .LoggerColorBehavior
                        .Enabled;
                    options.SingleLine = true;
                    options.TimestampFormat = "[HH:mm:ss.fff]";
                });
                c.AddConfiguration(Configuration.GetSection("Logging"));
            });

            services.Configure<DatabaseSettings>(
                Configuration.GetSection(nameof(DatabaseSettings))
            );
            services.AddSingleton<DatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DatabaseSettings>>().Value
            );
            services.AddSingleton<MongoService>();
            services.AddSingleton<StationModesService>();
            services.AddSingleton<ProcessDataService>();
            services.AddSingleton<QueryService>();
            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo { Title = "TcOpenHammer.Grafana.API", Version = "v1" }
                );
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DataBackend v1")
                );
            }
            SetupCulture();
            app.UseRouting();
            app.UseAuthorization();
            // To get images from the .\images folder.
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void SetupCulture()
        {
            var cultureInfo = new CultureInfo("sk-SK");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
    }
}
