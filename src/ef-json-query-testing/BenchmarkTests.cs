using BenchmarkDotNet.Attributes;
using Bogus;
using Microsoft.EntityFrameworkCore;
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
    [AnyCategoriesFilter("many")]
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














        public IEnumerable<object> BenchmarkData_List()
        {
            // all values need to be in one object
            // select an object.
            var mediaIds = _context.Media_Dynamic.AsNoTracking().Select(m => m.Media_DynamicId);
            var faker = new Faker();

            var list = new Dictionary<int, string>();
            var maxloops = 10;
            for (int loopCount = 0; loopCount < maxloops; loopCount++)
            {
                int id = faker.PickRandom<int>(mediaIds);
                var values = _context.DynamicMediaInformation.AsNoTracking().Include(i => i.Field).Where(i => i.MediaId == id).ToList();

                var intFields = values.Where(v => v.Field.DataType == DataTypes.IntValue && !v.Field.DynamicListTypeId.HasValue);
                var listIntFields = values.Where(v => v.Field.DataType == DataTypes.IntValue && v.Field.DynamicListTypeId.HasValue);
                var boolFields = values.Where(v => v.Field.DataType == DataTypes.BoolValue);
                var stringFields = values.Where(v => v.Field.DataType == DataTypes.StringValue && v.Value.Length > 15);

                // if each field doesnt have an available option, try again.
                // (this is based on the idea it's easier to just pick random again, than it is to search for all the matching criteria.)
                if (intFields.Count() == 0 || listIntFields.Count() == 0 || boolFields.Count() == 0 || stringFields.Count() == 0)
                {
                    continue;
                }
                else
                {
                    // all fields have an option, dont repeat the loop.
                    loopCount = maxloops;
                }

                // pick fields to search with, one of each type.
                var intField = faker.PickRandom(intFields);
                var listIntField = faker.PickRandom(listIntFields);
                var boolField = faker.PickRandom(boolFields);
                var stringField = faker.PickRandom(stringFields);                

                // add search values to dict                
                list.Add(intField.FieldId, intField.Value);
                list.Add(listIntField.FieldId, listIntField.Value);
                list.Add(boolField.FieldId, boolField.Value);
                list.Add(stringField.FieldId, faker.PickRandom(stringField.Value.Split(' ')));
            }

            //throw if something wasnt picked.
            if (list.Count < 3)
            {
                throw new Exception("Didnt find a good test value.");
            }

            yield return list;
        }

        [ParamsSource(nameof(BenchmarkData_List))]
        public Dictionary<int, string> searchFields { get; set; }



        [Benchmark]
        [BenchmarkCategory("json", "many", "raw")]
        public void Benchmark_Many_JSON_Raw() => _search.JsonSearch_Raw(searchFields);

        [Benchmark]
        [BenchmarkCategory("json", "many", "magic")]
        public void Benchmark_Many_JSON_Magic() => _search.JsonSearch_EfMagic(searchFields);

        [Benchmark]
        [BenchmarkCategory("table", "many", "media")]
        public void Benchmark_Many_Table_Media() => _search.TableSearch_Media(searchFields);
    }
}
