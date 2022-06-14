using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef_json_query_testing.Benchmarks
{
    public class LessRandomBenchmarks : BaseBenchmark
    {

        // compare optional field search vs required field search
        // first item and last item search
        // item with least amount of optiona fields vs most options


        // longest json.


        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "first")]
        public void Indexed_first_both_bool_int() => Search.JsonSearch_Indexed(TestValueConstants.first_both_bool_int);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "first")]
        public void Media_first_both_bool_int() => Search.TableSearch_Media(TestValueConstants.first_both_bool_int);

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "first")]
        public void Indexed_first_req_bool_int() => Search.JsonSearch_Indexed(TestValueConstants.first_req_bool_int);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "first")]
        public void Media_first_req_bool_int() => Search.TableSearch_Media(TestValueConstants.first_req_bool_int);

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "first")]
        public void Indexed_first_op_int() => Search.JsonSearch_Indexed(TestValueConstants.first_op_int);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "first")]
        public void Media_first_op_int() => Search.TableSearch_Media(TestValueConstants.first_op_int);

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "first")]
        public void Indexed_first_req_string() => Search.JsonSearch_Indexed(TestValueConstants.first_req_string);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "first")]
        public void Media_first_req_string() => Search.TableSearch_Media(TestValueConstants.first_req_string);

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "first")]
        public void Indexed_first_op_string() => Search.JsonSearch_Indexed(TestValueConstants.first_op_string);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "first")]
        public void Media_first_op_string() => Search.TableSearch_Media(TestValueConstants.first_op_string);

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "first")]
        public void Indexed_first_op_string_single() => Search.JsonSearch_Indexed(TestValueConstants.first_op_string_single);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "first")]
        public void Media_first_op_string_single() => Search.TableSearch_Media(TestValueConstants.first_op_string_single);

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "first")]
        public void Indexed_first_req_string_single() => Search.JsonSearch_Indexed(TestValueConstants.first_req_string_single);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "first")]
        public void Media_first_req_string_single() => Search.TableSearch_Media(TestValueConstants.first_req_string_single);





        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "last")]
        public void Indexed_Last_both_int_bool() => Search.JsonSearch_Indexed(TestValueConstants.Last_both_int_bool);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "last")]
        public void Media_last_both_int_bool() => Search.TableSearch_Media(TestValueConstants.Last_both_int_bool);

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "last")]
        public void Indexed_Last_req_int_bool() => Search.JsonSearch_Indexed(TestValueConstants.Last_req_int_bool);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "last")]
        public void Media_last_req_int_bool() => Search.TableSearch_Media(TestValueConstants.Last_req_int_bool);


        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "last")]
        public void Indexed_Last_req_string() => Search.JsonSearch_Indexed(TestValueConstants.Last_req_string);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "last")]
        public void Media_last_req_string() => Search.TableSearch_Media(TestValueConstants.Last_req_string);


        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "last")]
        public void Indexed_Last_req_string_single() => Search.JsonSearch_Indexed(TestValueConstants.Last_req_string_single);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "last")]
        public void Media_last_req_string_single() => Search.TableSearch_Media(TestValueConstants.Last_req_string_single);









        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set1")]
        public void Indexed_set1_both_int_bool() => Search.JsonSearch_Indexed(TestValueConstants.set1_both_int_bool);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set1")]
        public void Media_set1_both_int_bool() => Search.TableSearch_Media(TestValueConstants.set1_both_int_bool);

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set1")]
        public void Indexed_set1_req_int_bool() => Search.JsonSearch_Indexed(TestValueConstants.set1_req_int_bool);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set1")]
        public void Media_set1_req_int_bool() => Search.TableSearch_Media(TestValueConstants.set1_req_int_bool);

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set1")]
        public void Indexed_set1_op_int() => Search.JsonSearch_Indexed(TestValueConstants.set1_op_int);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set1")]
        public void Media_set1_op_int() => Search.TableSearch_Media(TestValueConstants.set1_op_int);


        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set1")]
        public void Indexed_set1_req_string() => Search.JsonSearch_Indexed(TestValueConstants.set1_req_string);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set1")]
        public void Media_set1_req_string() => Search.TableSearch_Media(TestValueConstants.set1_req_string);

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set1")]
        public void Indexed_set1_op_string_single() => Search.JsonSearch_Indexed(TestValueConstants.set1_op_string_single);


        //THIS ONE
        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set1", "miss")]
        public void Media_set1_op_string_single() => Search.TableSearch_Media(TestValueConstants.set1_op_string_single);

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set1")]
        public void Indexed_set1_req_string_single() => Search.JsonSearch_Indexed(TestValueConstants.set1_req_string_single);

        //THIS ONE
        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set1", "miss")]
        public void Media_set1_req_string_single() => Search.TableSearch_Media(TestValueConstants.set1_req_string_single);







        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set2")]
        public void Indexed_set2_both_int_bool() => Search.JsonSearch_Indexed(TestValueConstants.set2_both_int_bool);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set2")]
        public void Media_set2_both_int_bool() => Search.TableSearch_Media(TestValueConstants.set2_both_int_bool);

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set2")]
        public void Indexed_set2_req_int() => Search.JsonSearch_Indexed(TestValueConstants.set2_req_int);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set2")]
        public void Media_set2_req_int() => Search.TableSearch_Media(TestValueConstants.set2_req_int);

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set2")]
        public void Indexed_set2_req_string() => Search.JsonSearch_Indexed(TestValueConstants.set2_req_string);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set2")]
        public void Media_set2_req_string() => Search.TableSearch_Media(TestValueConstants.set2_req_string);


        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set2")]
        public void Indexed_set2_req_string_single() => Search.JsonSearch_Indexed(TestValueConstants.set2_req_string_single);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set2")]
        public void Media_set2_req_string_single() => Search.TableSearch_Media(TestValueConstants.set2_req_string_single);

    }
}
