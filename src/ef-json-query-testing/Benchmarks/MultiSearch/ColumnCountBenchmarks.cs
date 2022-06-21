using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef_json_query_testing.Benchmarks.MultiSearch
{
    public class ColumnCountBenchmarks : BaseBenchmark
    {


        [Benchmark]
        [BenchmarkCategory("table", "columncount", "first", "count10")]
        public void Media_count10_first_op_string() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.first_op_string, columnInclude: TestValueConstants.ColumnCount_10);

        [Benchmark]
        [BenchmarkCategory("table", "columncount", "first", "count10")]
        public void Media_count10_first_op_string_single() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.first_op_string_single, columnInclude: TestValueConstants.ColumnCount_10);

        [Benchmark]
        [BenchmarkCategory("table", "columncount", "first", "count10")]
        public void Media_count10_first_req_string() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.first_req_string, columnInclude: TestValueConstants.ColumnCount_10);

        [Benchmark]
        [BenchmarkCategory("table", "columncount", "first", "count10")]
        public void Media_count10_first_req_string_single() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.first_req_string_single, columnInclude: TestValueConstants.ColumnCount_10);






        [Benchmark]
        [BenchmarkCategory("table", "columncount", "set1", "count10")]
        public void Media_count10_set1_req_string() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.set1_req_string, columnInclude: TestValueConstants.ColumnCount_10);

        [Benchmark]
        [BenchmarkCategory("table", "columncount", "set1", "count10")]
        public void Media_count10_set1_op_string_single() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.set1_op_string_single, columnInclude: TestValueConstants.ColumnCount_10);

        [Benchmark]
        [BenchmarkCategory("table", "columncount", "set1", "count10")]
        public void Media_count10_set1_req_string_single() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.set1_req_string_single, columnInclude: TestValueConstants.ColumnCount_10);







        [Benchmark]
        [BenchmarkCategory("table", "columncount", "set2", "count10")]
        public void Media_count10_set2_req_string() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.set2_req_string, columnInclude: TestValueConstants.ColumnCount_10);

        [Benchmark]
        [BenchmarkCategory("table", "columncount", "set2", "count10")]
        public void Media_count10_set2_req_string_single() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.set2_req_string_single, columnInclude: TestValueConstants.ColumnCount_10);













        [Benchmark]
        [BenchmarkCategory("table", "columncount", "first", "count25")]
        public void Media_count25_first_op_string() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.first_op_string, columnInclude: TestValueConstants.ColumnCount_25);

        [Benchmark]
        [BenchmarkCategory("table", "columncount", "first", "count25")]
        public void Media_count25_first_op_string_single() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.first_op_string_single, columnInclude: TestValueConstants.ColumnCount_25);

        [Benchmark]
        [BenchmarkCategory("table", "columncount", "first", "count25")]
        public void Media_count25_first_req_string() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.first_req_string, columnInclude: TestValueConstants.ColumnCount_25);

        [Benchmark]
        [BenchmarkCategory("table", "columncount", "first", "count25")]
        public void Media_count25_first_req_string_single() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.first_req_string_single, columnInclude: TestValueConstants.ColumnCount_25);






        [Benchmark]
        [BenchmarkCategory("table", "columncount", "set1", "count25")]
        public void Media_count25_set1_req_string() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.set1_req_string, columnInclude: TestValueConstants.ColumnCount_25);

        [Benchmark]
        [BenchmarkCategory("table", "columncount", "set1", "count25")]
        public void Media_count25_set1_op_string_single() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.set1_op_string_single, columnInclude: TestValueConstants.ColumnCount_25);

        [Benchmark]
        [BenchmarkCategory("table", "columncount", "set1", "count25")]
        public void Media_count25_set1_req_string_single() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.set1_req_string_single, columnInclude: TestValueConstants.ColumnCount_25);







        [Benchmark]
        [BenchmarkCategory("table", "columncount", "set2", "count25")]
        public void Media_count25_set2_req_string() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.set2_req_string, columnInclude: TestValueConstants.ColumnCount_25);

        [Benchmark]
        [BenchmarkCategory("table", "columncount", "set2", "count25")]
        public void Media_count25_set2_req_string_single() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.set2_req_string_single, columnInclude: TestValueConstants.ColumnCount_25);














        [Benchmark]
        [BenchmarkCategory("table", "columncount", "first", "countall")]
        public void Media_countAll_first_op_string() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.first_op_string, columnInclude: TestValueConstants.ColumnCount_All);

        [Benchmark]
        [BenchmarkCategory("table", "columncount", "first", "countall")]
        public void Media_countAll_first_op_string_single() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.first_op_string_single, columnInclude: TestValueConstants.ColumnCount_All);

        [Benchmark]
        [BenchmarkCategory("table", "columncount", "first", "countall")]
        public void Media_countAll_first_req_string() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.first_req_string, columnInclude: TestValueConstants.ColumnCount_All);

        [Benchmark]
        [BenchmarkCategory("table", "columncount", "first", "countall")]
        public void Media_countAll_first_req_string_single() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.first_req_string_single, columnInclude: TestValueConstants.ColumnCount_All);






        [Benchmark]
        [BenchmarkCategory("table", "columncount", "set1", "countall")]
        public void Media_countAll_set1_req_string() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.set1_req_string, columnInclude: TestValueConstants.ColumnCount_All);

        [Benchmark]
        [BenchmarkCategory("table", "columncount", "set1", "countall")]
        public void Media_countAll_set1_op_string_single() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.set1_op_string_single, columnInclude: TestValueConstants.ColumnCount_All);

        [Benchmark]
        [BenchmarkCategory("table", "columncount", "set1", "countall")]
        public void Media_countAll_set1_req_string_single() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.set1_req_string_single, columnInclude: TestValueConstants.ColumnCount_All);







        [Benchmark]
        [BenchmarkCategory("table", "columncount", "set2", "countall")]
        public void Media_countAll_set2_req_string() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.set2_req_string, columnInclude: TestValueConstants.ColumnCount_All);

        [Benchmark]
        [BenchmarkCategory("table", "columncount", "set2", "countall")]
        public void Media_countAll_set2_req_string_single() => Search.TableSearch_Media_RestrictedColumns(TestValueConstants.set2_req_string_single, columnInclude: TestValueConstants.ColumnCount_All);


    }
}
