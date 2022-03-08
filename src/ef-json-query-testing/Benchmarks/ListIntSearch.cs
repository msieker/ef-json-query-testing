using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Bogus;

namespace ef_json_query_testing.Benchmarks
{
    public class ListIntSearch :BaseBenchmark
    {
        public IEnumerable<object[]> BenchmarkData_ListMatch()
        {
            var listTypeField = Context.DynamicFields.First(m => m.DynamicListTypeId.HasValue);

            var faker = new Faker();
            var item = faker.PickRandom(Context.DynamicMediaInformation.Where(m => m.FieldId == listTypeField.DynamicFieldId)).First();

            yield return new object[] { item.FieldId, item.Value };
        }

        [Benchmark]
        [BenchmarkCategory("json", "listInt", "raw")]
        [ArgumentsSource(nameof(BenchmarkData_ListMatch))]
        public void Raw(int i, string str) => Search.JsonSearch_Raw(i, str);

        [Benchmark]
        [BenchmarkCategory("json", "listInt", "magic")]
        [ArgumentsSource(nameof(BenchmarkData_ListMatch))]
        public void Magic(int i, string str) => Search.JsonSearch_EfMagic(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "listInt", "info")]
        [ArgumentsSource(nameof(BenchmarkData_ListMatch))]
        public void Info(int i, string str) => Search.TableSearch_Info(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "listInt", "media")]
        [ArgumentsSource(nameof(BenchmarkData_ListMatch))]
        public void Media(int i, string str) => Search.TableSearch_Media(i, str);
    }
}
