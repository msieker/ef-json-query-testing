using BenchmarkDotNet.Attributes;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 *  [AnyCategoriesFilter("A", "1")] - should run any benchmark that has a matching filter
 *      - options: 
 *           srategy used: "json", "table"
 *           test type: "nomatch", "int", "listInt", "bool", "string", "many"
 *           method used: "raw", "magic", "info", "media"
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
        [BenchmarkCategory("json", "nomatch", "raw")]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NoMatch_JSON_Raw(int i, string str) => _search.JsonSearch_Raw(i, str);

        [Benchmark]
        [BenchmarkCategory("json", "nomatch", "magic")]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NoMatch_JSON_Magic(int i, string str) => _search.JsonSearch_EfMagic(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "nomatch", "info")]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NoMatch_Table_Info(int i, string str) => _search.TableSearch_Info(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "nomatch", "media")]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NoMatch_Table_Media(int i, string str) => _search.TableSearch_Media(i, str);







        public IEnumerable<object[]> BenchmarkData_IntMatch()
        {
            var intTypeField = _context.DynamicFields.First(m => !m.DynamicListTypeId.HasValue && m.DataType == DataTypes.IntValue);

            var faker = new Faker();
            var item = faker.PickRandom(_context.DynamicMediaInformation.Where(m => m.FieldId == intTypeField.DynamicFieldId)).FirstOrDefault();

            yield return new object[] { item.FieldId, item.Value };
        }

        //[Benchmark(OperationsPerInvoke = 20)]
        [Benchmark]
        [BenchmarkCategory("json", "int", "raw")]
        [ArgumentsSource(nameof(BenchmarkData_IntMatch))]
        public void Benchmark_Int_JSON_Raw(int i, string str) => _search.JsonSearch_Raw(i, str);

        [Benchmark]
        [BenchmarkCategory("json", "int", "magic")]
        [ArgumentsSource(nameof(BenchmarkData_IntMatch))]
        public void Benchmark_Int_JSON_Magic(int i, string str) => _search.JsonSearch_EfMagic(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "int", "info")]
        [ArgumentsSource(nameof(BenchmarkData_IntMatch))]
        public void Benchmark_Int_Table_Info(int i, string str) => _search.TableSearch_Info(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "int", "media")]
        [ArgumentsSource(nameof(BenchmarkData_IntMatch))]
        public void Benchmark_Int_Table_Media(int i, string str) => _search.TableSearch_Media(i, str);








        public IEnumerable<object[]> BenchmarkData_ListMatch()
        {
            var listTypeField = _context.DynamicFields.First(m => m.DynamicListTypeId.HasValue);

            var faker = new Faker();
            var item = faker.PickRandom(_context.DynamicMediaInformation.Where(m => m.FieldId == listTypeField.DynamicFieldId)).FirstOrDefault();

            yield return new object[] { item.FieldId, item.Value };
        }

        [Benchmark]
        [BenchmarkCategory("json", "listInt", "raw")]
        [ArgumentsSource(nameof(BenchmarkData_ListMatch))]
        public void Benchmark_ListInt_JSON_Raw(int i, string str) => _search.JsonSearch_Raw(i, str);

        [Benchmark]
        [BenchmarkCategory("json", "listInt", "magic")]
        [ArgumentsSource(nameof(BenchmarkData_ListMatch))]
        public void Benchmark_ListInt_JSON_Magic(int i, string str) => _search.JsonSearch_EfMagic(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "listInt", "info")]
        [ArgumentsSource(nameof(BenchmarkData_ListMatch))]
        public void Benchmark_ListInt_Table_Info(int i, string str) => _search.TableSearch_Info(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "listInt", "media")]
        [ArgumentsSource(nameof(BenchmarkData_ListMatch))]
        public void Benchmark_ListInt_Table_Media(int i, string str) => _search.TableSearch_Media(i, str);















        public IEnumerable<object[]> BenchmarkData_BoolMatch()
        {
            var boolTypeField = _context.DynamicFields.First(m => m.DataType == DataTypes.BoolValue);

            var faker = new Faker();
            var item = faker.PickRandom(_context.DynamicMediaInformation.Where(m => m.FieldId == boolTypeField.DynamicFieldId)).FirstOrDefault();

            yield return new object[] { item.FieldId, item.Value };
        }

        [Benchmark]
        [BenchmarkCategory("json", "bool", "raw")]
        [ArgumentsSource(nameof(BenchmarkData_BoolMatch))]
        public void Benchmark_Bool_JSON_Raw(int i, string str) => _search.JsonSearch_Raw(i, str);

        [Benchmark]
        [BenchmarkCategory("json", "bool", "magic")]
        [ArgumentsSource(nameof(BenchmarkData_BoolMatch))]
        public void Benchmark_Bool_JSON_Magic(int i, string str) => _search.JsonSearch_EfMagic(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "bool", "info")]
        [ArgumentsSource(nameof(BenchmarkData_BoolMatch))]
        public void Benchmark_Bool_Table_Info(int i, string str) => _search.TableSearch_Info(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "bool", "media")]
        [ArgumentsSource(nameof(BenchmarkData_BoolMatch))]
        public void Benchmark_Bool_Table_Media(int i, string str) => _search.TableSearch_Media(i, str);













        // string match should be a contains, so these should be different than the above
        public IEnumerable<object[]> BenchmarkData_StringFind()
        {
            var stringTypeField = _context.DynamicFields.First(m => m.DataType == DataTypes.StringValue);

            var faker = new Faker();
            var item = faker.PickRandom(_context.DynamicMediaInformation.Where(m => m.FieldId == stringTypeField.DynamicFieldId)).FirstOrDefault();

            yield return new object[] { item.FieldId, item.Value };
        }

        [Benchmark]
        [BenchmarkCategory("json", "string", "raw")]
        [ArgumentsSource(nameof(BenchmarkData_StringFind))]
        public void Benchmark_String_JSON_Raw(int i, string str) => _search.JsonSearch_Raw(i, str);

        [Benchmark]
        [BenchmarkCategory("json", "string", "magic")]
        [ArgumentsSource(nameof(BenchmarkData_StringFind))]
        public void Benchmark_String_JSON_Magic(int i, string str) => _search.JsonSearch_EfMagic(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "string", "info")]
        [ArgumentsSource(nameof(BenchmarkData_StringFind))]
        public void Benchmark_String_Table_Info(int i, string str) => _search.TableSearch_Info(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "string", "media")]
        [ArgumentsSource(nameof(BenchmarkData_StringFind))]
        public void Benchmark_String_Table_Media(int i, string str) => _search.TableSearch_Media(i, str);














        public IEnumerable<object[]> BenchmarkData_List()
        {
            var list = new List<object[]>();

            list.AddRange(BenchmarkData_NoMatch());
            list.AddRange(BenchmarkData_IntMatch());
            list.AddRange(BenchmarkData_ListMatch());
            list.AddRange(BenchmarkData_BoolMatch());
            list.AddRange(BenchmarkData_StringFind());

            return list;
        }

        [Benchmark]
        [BenchmarkCategory("json", "many", "raw")]
        [ArgumentsSource(nameof(BenchmarkData_StringFind))]
        public void Benchmark_String_JSON_Raw(Dictionary<int, string> searchFields) => _search.JsonSearch_Raw(searchFields);

        [Benchmark]
        [BenchmarkCategory("json", "many", "magic")]
        [ArgumentsSource(nameof(BenchmarkData_StringFind))]
        public void Benchmark_String_JSON_Magic(Dictionary<int, string> searchFields) => _search.JsonSearch_EfMagic(searchFields);

        [Benchmark]
        [BenchmarkCategory("table", "many", "media")]
        [ArgumentsSource(nameof(BenchmarkData_StringFind))]
        public void Benchmark_String_Table_Media(Dictionary<int, string> searchFields) => _search.TableSearch_Media(searchFields);
    }
}
