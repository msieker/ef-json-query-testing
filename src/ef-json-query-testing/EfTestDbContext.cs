using System.Diagnostics.CodeAnalysis;
using ef_json_query_testing.Data.Models;
using Microsoft.EntityFrameworkCore;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=.");
        }
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
