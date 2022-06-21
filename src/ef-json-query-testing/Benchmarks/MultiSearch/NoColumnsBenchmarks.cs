using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef_json_query_testing.Benchmarks
{
    public class NoColumnsBenchmarks : BaseBenchmark
    {

        [Benchmark]
        [BenchmarkCategory("json", "nocolumns", "first")]
        public void Indexed_noColumns_first_op_string() => Search.JsonSearch_Indexed_NoColumns(TestValueConstants.first_op_string);

        [Benchmark]
        [BenchmarkCategory("json", "nocolumns", "first")]
        public void Indexed_noColumns_first_op_string_single() => Search.JsonSearch_Indexed_NoColumns(TestValueConstants.first_op_string_single);

        [Benchmark]
        [BenchmarkCategory("json", "nocolumns", "first")]
        public void Indexed_noColumns_first_req_string() => Search.JsonSearch_Indexed_NoColumns(TestValueConstants.first_req_string);

        [Benchmark]
        [BenchmarkCategory("json", "nocolumns", "first")]
        public void Indexed_noColumns_first_req_string_single() => Search.JsonSearch_Indexed_NoColumns(TestValueConstants.first_req_string_single);


        [Benchmark]
        [BenchmarkCategory("json", "nocolumns", "set1")]
        public void Indexed_noColumns_set1_req_string() => Search.JsonSearch_Indexed_NoColumns(TestValueConstants.set1_req_string);

        [Benchmark]
        [BenchmarkCategory("json", "nocolumns", "set1")]
        public void Indexed_noColumns_set1_op_string_single() => Search.JsonSearch_Indexed_NoColumns(TestValueConstants.set1_op_string_single);

        [Benchmark]
        [BenchmarkCategory("json", "nocolumns", "set1")]
        public void Indexed_noColumns_set1_req_string_single() => Search.JsonSearch_Indexed_NoColumns(TestValueConstants.set1_req_string_single);


        [Benchmark]
        [BenchmarkCategory("json", "nocolumns", "set2")]
        public void Indexed_noColumns_set2_req_string() => Search.JsonSearch_Indexed_NoColumns(TestValueConstants.set2_req_string);

        [Benchmark]
        [BenchmarkCategory("json", "nocolumns", "set2")]
        public void Indexed_noColumns_set2_req_string_single() => Search.JsonSearch_Indexed_NoColumns(TestValueConstants.set2_req_string_single);














        [Benchmark]
        [BenchmarkCategory("table", "nocolumns", "first")]
        public void Media_noColumns_first_op_string() => Search.TableSearch_Media_NoColumns(TestValueConstants.first_op_string);

        [Benchmark]
        [BenchmarkCategory("table", "nocolumns", "first")]
        public void Media_noColumns_first_op_string_single() => Search.TableSearch_Media_NoColumns(TestValueConstants.first_op_string_single);

        [Benchmark]
        [BenchmarkCategory("table", "nocolumns", "first")]
        public void Media_noColumns_first_req_string() => Search.TableSearch_Media_NoColumns(TestValueConstants.first_req_string);

        [Benchmark]
        [BenchmarkCategory("table", "nocolumns", "first")]
        public void Media_noColumns_first_req_string_single() => Search.TableSearch_Media_NoColumns(TestValueConstants.first_req_string_single);


        [Benchmark]
        [BenchmarkCategory("table", "nocolumns", "set1")]
        public void Media_noColumns_set1_req_string() => Search.TableSearch_Media_NoColumns(TestValueConstants.set1_req_string);

        [Benchmark]
        [BenchmarkCategory("table", "nocolumns", "set1")]
        public void Media_noColumns_set1_op_string_single() => Search.TableSearch_Media_NoColumns(TestValueConstants.set1_op_string_single);

        [Benchmark]
        [BenchmarkCategory("table", "nocolumns", "set1")]
        public void Media_noColumns_set1_req_string_single() => Search.TableSearch_Media_NoColumns(TestValueConstants.set1_req_string_single);


        [Benchmark]
        [BenchmarkCategory("table", "nocolumns", "set2")]
        public void Media_noColumns_set2_req_string() => Search.TableSearch_Media_NoColumns(TestValueConstants.set2_req_string);

        [Benchmark]
        [BenchmarkCategory("table", "nocolumns", "set2")]
        public void Media_noColumns_set2_req_string_single() => Search.TableSearch_Media_NoColumns(TestValueConstants.set2_req_string_single);

    }
}
