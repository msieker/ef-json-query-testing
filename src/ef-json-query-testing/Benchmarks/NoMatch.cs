using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace ef_json_query_testing.Benchmarks
{
    public class NoMatch : BaseBenchmark
    {
        private int i = 1;
        private string str = "aaaaaaaaaaaaaaa";
        
        //[Benchmark]
        //[BenchmarkCategory("json", "nomatch", "raw")]
        //public Task Raw() => Search.JsonSearch_Raw(i, str);

        [Benchmark]
        [BenchmarkCategory("json", "nomatch", "magic")]
        public Task Magic() => Search.JsonSearch_EfMagic(i, str);

        //[Benchmark]
        //[BenchmarkCategory("table", "nomatch", "info")]
        //public Task Info() => Search.TableSearch_Info(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "nomatch", "media")]
        public Task Media() => Search.TableSearch_Media(i, str);
    }
}
