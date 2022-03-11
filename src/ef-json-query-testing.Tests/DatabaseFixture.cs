using System;
using Xunit;
using Xunit.Abstractions;

namespace ef_json_query_testing.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            Environment.SetEnvironmentVariable("BENCHMARK_SQL_CONN", "Server=(LocalDb)\\MSSQLLocalDB;Initial Catalog=ef_testing_xunit;Integrated Security=SSPI;Connection Timeout=5;");

            Context = EfTestDbContext.Create(true);
            SearchService = new SearchService(Context);


            Context.Database.EnsureCreated();
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
