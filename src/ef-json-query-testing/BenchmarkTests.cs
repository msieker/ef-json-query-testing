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
        private readonly SearchService _search = new SearchService(new EfTestDbContext());

        public IEnumerable<object[]> BenchmarkData_NoMatch()
        {
            yield return new object[] { 0, "" };
            yield return new object[] { 0, "asd" };
            yield return new object[] { 1, "0" };
            yield return new object[] { 14, "0" };
        }

        [Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NoMatch_MediaJsonSearch_JsonDocument(int i, string str) => _search.MediaJsonSearch_JsonDocument(i, str);

        [Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NotMatch_MediaJsonSearch_JsonDocumentCombo(int i, string str) => _search.MediaJsonSearch_JsonDocumentCombo(i, str);

        [Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NotMatch_MediaJsonSearch_Dictionary(int i, string str) => _search.MediaJsonSearch_Dictionary(i, str);

        [Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NotMatch_MediaJsonSearch_RAW_SqlInterpolated(int i, string str) => _search.MediaJsonSearch_RAW_SqlInterpolated(i, str);

        [Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NotMatch_MediaTableSearch_OnlyContains(int i, string str) => _search.MediaTableSearch_OnlyContains(i, str);

        [Benchmark]
        [ArgumentsSource(nameof(BenchmarkData_NoMatch))]
        public void Benchmark_NotMatch_MediaTableSearch_ContainsOrEquals(int i, string str) => _search.MediaTableSearch_ContainsOrEquals(i, str);

    }
}
