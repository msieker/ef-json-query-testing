using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class EfTestDbContextFactory : IDesignTimeDbContextFactory<EfTestDbContext>
{
    public EfTestDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EfTestDbContext>();
        optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Initial Catalog=ef_testing;Integrated Security=SSPI;Connection Timeout=5;");

        return new EfTestDbContext(optionsBuilder.Options);
    }
}
