using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class EfTestDbContextFactory : IDesignTimeDbContextFactory<EfTestDbContext>
{
    public EfTestDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EfTestDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=ef_testing;Persist Security Info=False;Integrated Security=SSPI;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

        return new EfTestDbContext(optionsBuilder.Options);
    }
}
