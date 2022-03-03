using BenchmarkDotNet.Attributes;
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
            yield return new object[] { 0, "asd" };
            yield return new object[] { 1, "aaaaaaaaaaaaaaaa" };
        }

        [Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NotMatch_MediaJsonSearch_RAW_SqlInterpolated(int i, string str) => _search.MediaJsonSearch_RAW_SqlInterpolated(i, str);

        [Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NotMatch_MediaTableSearch_OnlyContains(int i, string str) => _search.MediaTableSearch_OnlyContains(i, str);

        [Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NotMatch_MediaTableSearch_ContainsOrEquals(int i, string str) => _search.MediaTableSearch_ContainsOrEquals(i, str);




        public IEnumerable<object[]> BenchmarkData_Match()
        {
            var itemFirst = _context.DynamicMediaInformation.First();
            var itemLast = _context.DynamicMediaInformation.Last();

            yield return new object[] { itemFirst.FieldId, itemFirst.Value };
            yield return new object[] { itemLast.FieldId, itemLast.Value };
        }

        [Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_Match))]
        public void Benchmark_Match_MediaJsonSearch_RAW_SqlInterpolated(int i, string str) => _search.MediaJsonSearch_RAW_SqlInterpolated(i, str);

        [Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_Match))]
        public void Benchmark_Match_MediaTableSearch_OnlyContains(int i, string str) => _search.MediaTableSearch_OnlyContains(i, str);

        [Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_Match))]
        public void Benchmark_Match_MediaTableSearch_ContainsOrEquals(int i, string str) => _search.MediaTableSearch_ContainsOrEquals(i, str);

    }
}
