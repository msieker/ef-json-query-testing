using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static void RegisterEfTestingDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add framework services.
            services.AddDbContext<EfTestDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
