
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class Program
{
    public static async Task Main(string[] args)
    {

        //Build Config
        var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();


        try
        {
            var host = CreateHostBuilder(args).Build();
            //Initalize DB
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                { 
                    //testing load data
                    //CreateBogusData.LoadData(scope.ServiceProvider.GetService<DentalExpressDbContext>());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred seeding the DB.");
                }
            }

            host.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Web Host terminated unexpectedly");
        }
        finally
        {
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
