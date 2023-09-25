using Microsoft.Extensions.DependencyInjection;

using TcoCore.TcoDiagnosticsAlternative.LoggingToDb;

namespace TcoCore.TcoDiagnosticsAlternative.Services
{
    public static class LibraryServiceConfiguration
    {
        public static void AddLibraryServices(this IServiceCollection services)
        {
            services.AddSingleton<IMongoLogger, MongoLogger>();
            // Register other services
        }
    }
}
