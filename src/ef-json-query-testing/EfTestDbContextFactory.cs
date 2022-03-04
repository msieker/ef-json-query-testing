using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class EfTestDbContextFactory : IDesignTimeDbContextFactory<EfTestDbContext>
{
    public EfTestDbContext CreateDbContext(string[] args)
    {
        return EfTestDbContext.Create();
    }
}
