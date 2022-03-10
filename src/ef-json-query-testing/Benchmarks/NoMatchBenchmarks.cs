using BenchmarkDotNet.Attributes;

namespace ef_json_query_testing.Benchmarks
{
    public class NoMatchBenchmarks : BaseBenchmark
    {
        public IEnumerable<object[]> BenchmarkData_NoMatch()
        {
            yield return new object[] { 1, "aaaaaaaaaaaaaaaa" };
        }

        [Benchmark]
        [BenchmarkCategory("json", "nomatch", "raw")]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NoMatch_JSON_Raw(int i, string str) => Search.JsonSearch_Raw(i, str);

        [Benchmark]
        [BenchmarkCategory("json", "nomatch", "magic")]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NoMatch_JSON_Magic(int i, string str) => Search.JsonSearch_EfMagic(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "nomatch", "info")]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NoMatch_Table_Info(int i, string str) => Search.TableSearch_Info(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "nomatch", "media")]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NoMatch_Table_Media(int i, string str) => Search.TableSearch_Media(i, str);
    }
}
