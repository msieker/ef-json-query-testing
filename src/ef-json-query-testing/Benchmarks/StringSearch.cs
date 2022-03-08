using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Bogus;

namespace ef_json_query_testing.Benchmarks
{
    public class StringSearch : BaseBenchmark
    {
        // string match should be a contains, so these should be different than the above
        public IEnumerable<object[]> BenchmarkData_StringFind()
        {
            var stringTypeField = Context.DynamicFields.First(m => m.DataType == DataTypes.StringValue);

            var faker = new Faker();
            var item = faker.PickRandom(Context.DynamicMediaInformation.Where(m => m.FieldId == stringTypeField.DynamicFieldId)).First();

            yield return new object[] { item.FieldId, item.Value };
        }

        [Benchmark]
        [BenchmarkCategory("json", "string", "raw")]
        [ArgumentsSource(nameof(BenchmarkData_StringFind))]
        public void Raw(int i, string str) => Search.JsonSearch_Raw(i, str);

        [Benchmark]
        [BenchmarkCategory("json", "string", "magic")]
        [ArgumentsSource(nameof(BenchmarkData_StringFind))]
        public void Magic(int i, string str) => Search.JsonSearch_EfMagic(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "string", "info")]
        [ArgumentsSource(nameof(BenchmarkData_StringFind))]
        public void Info(int i, string str) => Search.TableSearch_Info(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "string", "media")]
        [ArgumentsSource(nameof(BenchmarkData_StringFind))]
        public void Media(int i, string str) => Search.TableSearch_Media(i, str);
    }
}
