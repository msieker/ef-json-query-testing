using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ef_json_query_testing.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            Environment.SetEnvironmentVariable("BENCHMARK_SQL_CONN", "Server=(LocalDb)\\MSSQLLocalDB;Initial Catalog=ef_testing_xunit;Integrated Security=SSPI;Connection Timeout=5;");

            Context = EfTestDbContext.Create(true);
            SearchService = new SearchService(Context);

            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
            
            var sqlFile = Path.Combine("Sql/Create_stp_Add_Json_Index.sql");
            Context.Database.ExecuteSqlRaw(File.ReadAllText(sqlFile));

            var udt = Path.Combine("Sql/Create_udt_SearchFields.sql");
            var pew = File.ReadAllText(udt);
            Context.Database.ExecuteSqlRaw(pew);

            //var stp = Path.Combine("Sql/Create_stp_Search_Json.sql");
            //var thing = File.ReadAllText(stp);
            //Context.Database.ExecuteSqlRaw(thing);

            TestDataSeed.LoadAll(Context);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
        }

        public EfTestDbContext Context { get; private set; }
        public ISearchService SearchService { get; private set; }
    }

    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
