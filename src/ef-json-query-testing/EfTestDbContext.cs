using System.Diagnostics.CodeAnalysis;
using ef_json_query_testing.Models;
using ef_json_query_testing.Translators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

[ExcludeFromCodeCoverage]
public class EfTestDbContext : DbContext
{

    private static DbContextOptionsBuilder<EfTestDbContext> OptionsBuilder;
    private static DbContextOptions<EfTestDbContext> Options;

    private static PooledDbContextFactory<EfTestDbContext> Factory;

    static EfTestDbContext()
    {
        var connStr = Environment.GetEnvironmentVariable("BENCHMARK_SQL_CONN") ?? "Server=localhost;Initial Catalog=ef_testing_index_large;Persist Security Info=False;Integrated Security=SSPI;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=5;";
        OptionsBuilder = new DbContextOptionsBuilder<EfTestDbContext>();

        OptionsBuilder.UseSqlServer(connStr);
        OptionsBuilder.UseJsonFunctions();

        Options = OptionsBuilder.Options;

        Factory = new PooledDbContextFactory<EfTestDbContext>(Options);

    }

    public static void ResetDefault(string connStr, bool useLogging = false, Action<string>? logger = null, LogLevel minimumLevel = LogLevel.Information)
    {
        OptionsBuilder = new DbContextOptionsBuilder<EfTestDbContext>();

        OptionsBuilder.UseSqlServer(connStr);
        OptionsBuilder.UseJsonFunctions();

        if (useLogging)
        {
            OptionsBuilder.LogTo(logger ?? Console.WriteLine, minimumLevel).EnableSensitiveDataLogging(true);
        }

        Options = OptionsBuilder.Options;
        Factory = new PooledDbContextFactory<EfTestDbContext>(Options);
    }
    public EfTestDbContext(DbContextOptions<EfTestDbContext> options) : base(options)
    {
    }

    //public EfTestDbContext()
    //{
    //    //For Migrations
    //    //dotnet ef --startup-project ../ef-json-query-testing migrations add Initial
    //    //Apply changes
    //    //dotnet ef --startup-project ../ef-json-query-testing database update
    //}

    public static EfTestDbContext FromFactory() => Factory.CreateDbContext();

    public static EfTestDbContext Create(bool useLogging = true, Action<string>? logger = null, LogLevel minimumLevel = LogLevel.Information)
    {
        if (!useLogging) return new EfTestDbContext(Options);

        var options = new DbContextOptionsBuilder<EfTestDbContext>(Options);
        options.LogTo(logger ?? Console.WriteLine, minimumLevel).EnableSensitiveDataLogging(true);
        return new EfTestDbContext(options.Options);
    }

    public DbSet<DynamicField> DynamicFields => Set<DynamicField>();
    public DbSet<DynamicListType> DynamicListTypes => Set<DynamicListType>();
    public DbSet<DynamicListItem> DynamicListItems => Set<DynamicListItem>();
    public DbSet<DynamicMediaInformation> DynamicMediaInformation => Set<DynamicMediaInformation>();
    public DbSet<Media_Dynamic> Media_Dynamic => Set<Media_Dynamic>();
    public DbSet<Media_Json> Media_Json => Set<Media_Json>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Perform the base configuration items, we do our stuff AFTER so we can configure it
        base.OnModelCreating(builder);

        //applies all configurations implementing IEntityTypeConfiguration in a given assembly.
        builder.ApplyConfigurationsFromAssembly(typeof(DynamicField).Assembly);
    }
}
