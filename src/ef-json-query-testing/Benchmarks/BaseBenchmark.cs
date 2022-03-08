using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace ef_json_query_testing.Benchmarks
{
    public class BaseBenchmark
    {
        protected EfTestDbContext Context = EfTestDbContext.Create(false);
        protected SearchService Search;

        public BaseBenchmark()
        {
            Search = new SearchService(Context);
            Randomizer.Seed = new Random(42);
        }
    }
}
