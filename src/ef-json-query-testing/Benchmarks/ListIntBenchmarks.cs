using BenchmarkDotNet.Attributes;
using Bogus;
using ef_json_query_testing.Models;

namespace ef_json_query_testing.Benchmarks
{
    public class ListIntBenchmarks : BaseBenchmark
    {
        public IEnumerable<object[]> BenchmarkData_ListMatch()
        {
            var listTypeField = Context.DynamicFields.First(m => m.DynamicListTypeId.HasValue);

            var faker = new Faker();
            var item = faker.PickRandom<DynamicMediaInformation>(Context.DynamicMediaInformation.Where(m => m.FieldId == listTypeField.DynamicFieldId));

            yield return new object[] { item.FieldId, item.Value };
        }

        [Benchmark]
        [BenchmarkCategory("json", "listInt", "raw")]
        [ArgumentsSource(nameof(BenchmarkData_ListMatch))]
        public void Benchmark_ListInt_JSON_Raw(int i, string str) => Search.JsonSearch_Raw(i, str);

        [Benchmark]
        [BenchmarkCategory("json", "listInt", "magic")]
        [ArgumentsSource(nameof(BenchmarkData_ListMatch))]
        public void Benchmark_ListInt_JSON_Magic(int i, string str) => Search.JsonSearch_EfMagic(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "listInt", "info")]
        [ArgumentsSource(nameof(BenchmarkData_ListMatch))]
        public void Benchmark_ListInt_Table_Info(int i, string str) => Search.TableSearch_Info(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "listInt", "media")]
        [ArgumentsSource(nameof(BenchmarkData_ListMatch))]
        public void Benchmark_ListInt_Table_Media(int i, string str) => Search.TableSearch_Media(i, str);
    }
}
