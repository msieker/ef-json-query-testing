using BenchmarkDotNet.Attributes;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 *  [AnyCategoriesFilter("A", "1")] - should run any benchmark that has a matching filter
 *      - options: "json", "table", "nomatch", "int", "listInt", "bool", "string"
 */

namespace ef_json_query_testing
{
    [CategoriesColumn]
    //[AnyCategoriesFilter("string")]
    public class BenchmarkTests
    {
        private EfTestDbContext _context = EfTestDbContext.Create();
        private SearchService _search;

        public BenchmarkTests()
        {
            _search = new SearchService(_context);
        }

        public IEnumerable<object[]> BenchmarkData_NoMatch()
        {
            yield return new object[] { 1, "aaaaaaaaaaaaaaaa" };
        }

        [Benchmark]
        [BenchmarkCategory("json", "nomatch")]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NotMatch_JSON(int i, string str) => _search.JsonSearch(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "nomatch")]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NotMatch_Table(int i, string str) => _search.TableSearch_ViaInfoTable(i, str);







        public IEnumerable<object[]> BenchmarkData_IntMatch()
        {
            var intTypeField = _context.DynamicFields.First(m => !m.DynamicListTypeId.HasValue && m.DataType == DataTypes.IntValue);

            var faker = new Faker();
            var item = faker.PickRandom(_context.DynamicMediaInformation.Where(m => m.FieldId == intTypeField.DynamicFieldId)).FirstOrDefault();

            yield return new object[] { item.FieldId, item.Value };
        }

        //[Benchmark(OperationsPerInvoke = 20)]
        [Benchmark]
        [BenchmarkCategory("json", "int")]
        [ArgumentsSource(nameof(BenchmarkData_IntMatch))]
        public void Benchmark_Int_JSON(int i, string str) => _search.JsonSearch(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "int")]
        [ArgumentsSource(nameof(BenchmarkData_IntMatch))]
        public void Benchmark_Int_Table(int i, string str) => _search.TableSearch_ViaInfoTable(i, str);








        public IEnumerable<object[]> BenchmarkData_ListMatch()
        {
            var listTypeField = _context.DynamicFields.First(m => m.DynamicListTypeId.HasValue);

            var faker = new Faker();
            var item = faker.PickRandom(_context.DynamicMediaInformation.Where(m => m.FieldId == listTypeField.DynamicFieldId)).FirstOrDefault();

            yield return new object[] { item.FieldId, item.Value };
        }

        [Benchmark]
        [BenchmarkCategory("json", "listInt")]
        [ArgumentsSource(nameof(BenchmarkData_ListMatch))]
        public void Benchmark_ListInt_JSON(int i, string str) => _search.JsonSearch(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "listInt")]
        [ArgumentsSource(nameof(BenchmarkData_ListMatch))]
        public void Benchmark_ListInt_Table(int i, string str) => _search.TableSearch_ViaInfoTable(i, str);















        public IEnumerable<object[]> BenchmarkData_BoolMatch()
        {
            var boolTypeField = _context.DynamicFields.First(m => m.DataType == DataTypes.BoolValue);

            var faker = new Faker();
            var item = faker.PickRandom(_context.DynamicMediaInformation.Where(m => m.FieldId == boolTypeField.DynamicFieldId)).FirstOrDefault();

            yield return new object[] { item.FieldId, item.Value };
        }

        [Benchmark]
        [BenchmarkCategory("json", "bool")]
        [ArgumentsSource(nameof(BenchmarkData_BoolMatch))]
        public void Benchmark_Bool_JSON(int i, string str) => _search.JsonSearch(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "bool")]
        [ArgumentsSource(nameof(BenchmarkData_BoolMatch))]
        public void Benchmark_Bool_Table(int i, string str) => _search.TableSearch_ViaInfoTable(i, str);













        // string match should be a contains, so these should be different than the above
        public IEnumerable<object[]> BenchmarkData_StringFind()
        {
            var stringTypeField = _context.DynamicFields.First(m => m.DataType == DataTypes.StringValue);

            var faker = new Faker();
            var item = faker.PickRandom(_context.DynamicMediaInformation.Where(m => m.FieldId == stringTypeField.DynamicFieldId)).FirstOrDefault();

            yield return new object[] { item.FieldId, item.Value };
        }

        [Benchmark]
        [BenchmarkCategory("json", "string")]
        [ArgumentsSource(nameof(BenchmarkData_StringFind))]
        public void Benchmark_String_JSON(int i, string str) => _search.JsonSearch(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "string")]
        [ArgumentsSource(nameof(BenchmarkData_StringFind))]
        public void Benchmark_String_Table(int i, string str) => _search.TableSearch_ViaInfoTable(i, str);
    }
}
