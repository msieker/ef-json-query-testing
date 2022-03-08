using System.Diagnostics.CodeAnalysis;
using ef_json_query_testing;
using ef_json_query_testing.Translators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

[ExcludeFromCodeCoverage]
public class EfTestDbContext : DbContext
{
    private static readonly DbContextOptionsBuilder<EfTestDbContext> OptionsBuilder;
    private static readonly DbContextOptions<EfTestDbContext> Options;
    static EfTestDbContext()
    {

        OptionsBuilder = new DbContextOptionsBuilder<EfTestDbContext>();

        OptionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=ef_testing;Persist Security Info=False;Integrated Security=SSPI;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=5;");
        OptionsBuilder.UseJsonFunctions();

        Options = OptionsBuilder.Options;

    }

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
