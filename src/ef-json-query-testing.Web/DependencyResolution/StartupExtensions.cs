using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class StartupExtensions
    {
        public static void RegisterEfTestingServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Setup Services
            //services.AddTransient<IAdminDataService, AdminDataService>();
        }
    }
}