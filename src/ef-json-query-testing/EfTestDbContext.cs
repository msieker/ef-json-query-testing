using System.Diagnostics.CodeAnalysis;
using ef_json_query_testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

[ExcludeFromCodeCoverage]
public class EfTestDbContext : DbContext
{
    public EfTestDbContext(DbContextOptions<EfTestDbContext> options) : base(options)
    {
    }

    public EfTestDbContext()
    {
        //For Migrations
        //dotnet ef --startup-project ../ef-json-query-testing migrations add Initial
        //Apply changes
        //dotnet ef --startup-project ../ef-json-query-testing database update
    }

    public static EfTestDbContext Create(Action<string>? logger = null)
    {
        var options = new DbContextOptionsBuilder<EfTestDbContext>();

        options.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Initial Catalog=ef_testing_202203041105;Integrated Security=SSPI;Connection Timeout=5;");

        if (logger != null)
        {
            options.LogTo(logger, minimumLevel: LogLevel.Information).EnableSensitiveDataLogging(true);
        }

        return new EfTestDbContext(options.Options);
    }

    public DbSet<DynamicField> DynamicFields { get; set; }
    public DbSet<DynamicListType> DynamicListTypes { get; set; }
    public DbSet<DynamicListItem> DynamicListItems { get; set; }
    public DbSet<DynamicMediaInformation> DynamicMediaInformation { get; set; }
    public DbSet<Media_Dynamic> Media_Dynamic { get; set; }
    public DbSet<Media_Json> Media_Json { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Perform the base configuration items, we do our stuff AFTER so we can configure it
        base.OnModelCreating(builder);

        //applies all configurations implementing IEntityTypeConfiguration in a given assembly.
        builder.ApplyConfigurationsFromAssembly(typeof(DynamicField).Assembly);
    }
}
