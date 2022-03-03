using BenchmarkDotNet.Attributes;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef_json_query_testing
{
    public class BenchmarkTests
    {
        private EfTestDbContext _context = new EfTestDbContext();
        private SearchService _search;

        public BenchmarkTests()
        {
            _search = new SearchService(_context);
        }

        public IEnumerable<object[]> BenchmarkData_NoMatch()
        {
            yield return new object[] { 1, "aaaaaaaaaaaaaaaa" };
        }

        //[Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NotMatch_JSON_RAW_SqlInterpolated(int i, string str) => _search.MediaJsonSearch_RAW_SqlInterpolated(i, str);

        //[Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NotMatch_Table_OnlyContains(int i, string str) => _search.MediaTableSearch_OnlyContains(i, str);

       // [Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NotMatch_Table_ContainsOrEquals(int i, string str) => _search.MediaTableSearch_ContainsOrEquals(i, str);







        public IEnumerable<object[]> BenchmarkData_IntMatch()
        {
            var intTypeField = _context.DynamicFields.First(m => !m.DynamicListTypeId.HasValue && m.DataType == DataTypes.IntValue);


            var faker = new Faker();
            var itemRand = faker.PickRandom(_context.DynamicMediaInformation.Where(m => m.FieldId == intTypeField.DynamicFieldId));
           
            foreach(var item in itemRand)
            {
                yield return new object[] { item.FieldId, item.Value };
            }
        }

        //[Benchmark(OperationsPerInvoke = 20)]
        [Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_IntMatch))]
        public void Benchmark_Int_JSON_RAW_SqlInterpolated(int i, string str) => _search.MediaJsonSearch_RAW_SqlInterpolated(i, str);

        //[Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_IntMatch))]
        public void Benchmark_Int_Table_OnlyContains(int i, string str) => _search.MediaTableSearch_OnlyContains(i, str);

        //[Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_IntMatch))]
        public void Benchmark_Int_Table_ContainsOrEquals(int i, string str) => _search.MediaTableSearch_ContainsOrEquals(i, str);








        public IEnumerable<object[]> BenchmarkData_ListMatch()
        {
            var listTypeField = _context.DynamicFields.First(m => m.DynamicListTypeId.HasValue);


            var faker = new Faker();
            var itemRand = faker.PickRandom(_context.DynamicMediaInformation.Where(m => m.FieldId == listTypeField.DynamicFieldId));
            
            foreach (var item in itemRand)
            {
                yield return new object[] { item.FieldId, item.Value };
            }
        }

        [Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_ListMatch))]
        public void Benchmark_ListInt_JSON_RAW_SqlInterpolated(int i, string str) => _search.MediaJsonSearch_RAW_SqlInterpolated(i, str);

        //[Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_ListMatch))]
        public void Benchmark_ListInt_Table_OnlyContains(int i, string str) => _search.MediaTableSearch_OnlyContains(i, str);

        //[Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_ListMatch))]
        public void Benchmark_ListInt_Table_ContainsOrEquals(int i, string str) => _search.MediaTableSearch_ContainsOrEquals(i, str);















        public IEnumerable<object[]> BenchmarkData_BoolMatch()
        {
            var boolTypeField = _context.DynamicFields.First(m => m.DataType == DataTypes.BoolValue);


            var faker = new Faker();
            var itemRand = faker.PickRandom(_context.DynamicMediaInformation.Where(m => m.FieldId == boolTypeField.DynamicFieldId));
            
            foreach (var item in itemRand)
            {
                yield return new object[] { item.FieldId, item.Value };
            }
        }

        [Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_BoolMatch))]
        public void Benchmark_Bool_JSON_RAW_SqlInterpolated(int i, string str) => _search.MediaJsonSearch_RAW_SqlInterpolated(i, str);

        //[Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_BoolMatch))]
        public void Benchmark_Bool_Table_OnlyContains(int i, string str) => _search.MediaTableSearch_OnlyContains(i, str);

        //[Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_BoolMatch))]
        public void Benchmark_Bool_Table_ContainsOrEquals(int i, string str) => _search.MediaTableSearch_ContainsOrEquals(i, str);














        public IEnumerable<object[]> BenchmarkData_StringMatch()
        {
            var stringTypeField = _context.DynamicFields.First(m => m.DataType == DataTypes.StringValue);


            var faker = new Faker();
            var itemRand = faker.PickRandom(_context.DynamicMediaInformation.Where(m => m.FieldId == stringTypeField.DynamicFieldId));
            
            foreach (var item in itemRand)
            {
                yield return new object[] { item.FieldId, item.Value };
            }
        }

        [Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_StringMatch))]
        public void Benchmark_String_JSON_RAW_SqlInterpolated(int i, string str) => _search.MediaJsonSearch_RAW_SqlInterpolated(i, str);

        //[Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_StringMatch))]
        public void Benchmark_String_Table_OnlyContains(int i, string str) => _search.MediaTableSearch_OnlyContains(i, str);

        //[Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_StringMatch))]
        public void Benchmark_String_Table_ContainsOrEquals(int i, string str) => _search.MediaTableSearch_ContainsOrEquals(i, str);
    }
}
