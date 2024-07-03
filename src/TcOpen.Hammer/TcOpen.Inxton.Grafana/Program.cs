using Grafana.Backend.Queries;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TcOpenHammer.Grafana.API
{
    /// <summary>
    /// Entry point of the program. Creating an instance of <see cref="Startup"/>
    /// </summary>
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
