using BenchmarkDotNet.Attributes;
using Bogus;
using ef_json_query_testing.Enums;
using ef_json_query_testing.Models;

namespace ef_json_query_testing.Benchmarks
{
    public class StringBenchmarks : BaseBenchmark
    {
        // string match should be a contains, so these should be different than the above
        public IEnumerable<object[]> BenchmarkData_StringFind()
        {
            var stringTypeField = Context.DynamicFields.First(m => m.DataType == DataTypes.StringValue && m.IsRequired);

            var faker = new Faker();
            var item = faker.PickRandom<DynamicMediaInformation>(Context.DynamicMediaInformation.Where(m => m.FieldId == stringTypeField.DynamicFieldId));

            yield return new object[] { item.FieldId, item.Value };
        }

        [Benchmark]
        [BenchmarkCategory("json", "string", "raw")]
        [ArgumentsSource(nameof(BenchmarkData_StringFind))]
        public void JSON_Raw(int i, string str) => Search.JsonSearch_Raw(i, str);

        [Benchmark]
        [BenchmarkCategory("json", "string", "magic")]
        [ArgumentsSource(nameof(BenchmarkData_StringFind))]
        public void JSON_Magic(int i, string str) => Search.JsonSearch_EfMagic(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "string", "info")]
        [ArgumentsSource(nameof(BenchmarkData_StringFind))]
        public void Table_Info(int i, string str) => Search.TableSearch_Info(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "string", "media")]
        [ArgumentsSource(nameof(BenchmarkData_StringFind))]
        public void Table_Media(int i, string str) => Search.TableSearch_Media(i, str);
    }
}
