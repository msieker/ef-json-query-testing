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
        [BenchmarkCategory("json", "lessrand", "magic", "first")]
        public void Magic_first_both_bool_int() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 1 , "4" }, //optional
                { 4 , "0" }, //optional
                { 5 , "5" }, //optional
                { 10 , "3" },
                { 26 , "False" },
                { 48 , "True" }, //optional
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "first")]
        public void Indexed_first_both_bool_int() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 1 , "4" }, //optional
                { 4 , "0" }, //optional
                { 5 , "5" }, //optional
                { 10 , "3" },
                { 26 , "False" },
                { 48 , "True" }, //optional
            });

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "first")]
        public void Media_first_both_bool_int() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 1 , "4" }, //optional
                { 4 , "0" }, //optional
                { 5 , "5" }, //optional
                { 10 , "3" },
                { 26 , "False" },
                { 48 , "True" }, //optional
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "first")]
        public void Magic_first_req_bool_int() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 2 , "1" },
                { 7 , "1" },
                { 8 , "2" },
                { 9 , "8" },
                { 10 , "3" },
                { 26 , "False" },
                { 46 , "False" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "first")]
        public void Indexed_first_req_bool_int() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 2 , "1" },
                { 7 , "1" },
                { 8 , "2" },
                { 9 , "8" },
                { 10 , "3" },
                { 26 , "False" },
                { 46 , "False" },
            });

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "first")]
        public void Media_first_req_bool_int() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 2 , "1" },
                { 7 , "1" },
                { 8 , "2" },
                { 9 , "8" },
                { 10 , "3" },
                { 26 , "False" },
                { 46 , "False" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "first")]
        public void Magic_first_op_int() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 1 , "4" },
                { 3 , "8" },
                { 4 , "0" },
                { 5 , "5" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "first")]
        public void Indexed_first_op_int() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 1 , "4" },
                { 3 , "8" },
                { 4 , "0" },
                { 5 , "5" },
            });

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "first")]
        public void Media_first_op_int() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 1 , "4" },
                { 3 , "8" },
                { 4 , "0" },
                { 5 , "5" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "first")]
        public void Magic_first_req_string() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 13 , "laboriosam" },
                { 29 , "voluptatibus" },
                { 30 , "commodi" },
                { 32 , "mollitia" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "first")]
        public void Indexed_first_req_string() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 13 , "laboriosam" },
                { 29 , "voluptatibus" },
                { 30 , "commodi" },
                { 32 , "mollitia" },
            });

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "first")]
        public void Media_first_req_string() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 13 , "laboriosam" },
                { 29 , "voluptatibus" },
                { 30 , "commodi" },
                { 32 , "mollitia" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "first")]
        public void Magic_first_op_string() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 47 , "nihil tempora" },
                { 33 , "False" },
                { 48 , "True" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "first")]
        public void Indexed_first_op_string() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 47 , "nihil tempora" },
                { 33 , "False" },
                { 48 , "True" },
            });

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "first")]
        public void Media_first_op_string() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 47 , "nihil tempora" },
                { 33 , "False" },
                { 48 , "True" },
            });
        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "first")]
        public void Magic_first_op_string_single() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 47 , "nihil tempora nemo" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "first")]
        public void Indexed_first_op_string_single() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 47 , "nihil tempora nemo" },
            });

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "first")]
        public void Media_first_op_string_single() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 47 , "nihil tempora nemo" },
            });
        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "first")]
        public void Magic_first_req_string_single() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 13 , "exercitationem laboriosam aspernatur" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "first")]
        public void Indexed_first_req_string_single() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 13 , "exercitationem laboriosam aspernatur" },
            });

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "first")]
        public void Media_first_req_string_single() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 13 , "exercitationem laboriosam aspernatur" },
            });






        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "last")]
        public void Magic_last_both_int_bool() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 4 , "13" }, //optional
                { 6 , "7" }, //optional
                { 7 , "18" },
                { 10 , "3" },
                { 26 , "False" },
                { 33 , "True" }, //optional
                { 48 , "True" }, //optional
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "last")]
        public void Indexed_Last_both_int_bool() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 4 , "13" }, //optional
                { 6 , "7" }, //optional
                { 7 , "18" },
                { 10 , "3" },
                { 26 , "False" },
                { 33 , "True" }, //optional
                { 48 , "True" }, //optional
            });

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "last")]
        public void Media_last_both_int_bool() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 4 , "13" }, //optional
                { 6 , "7" }, //optional
                { 7 , "18" },
                { 10 , "3" },
                { 26 , "False" },
                { 33 , "True" }, //optional
                { 48 , "True" }, //optional
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "last")]
        public void Magic_last_req_int_bool() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 2 , "0" },
                { 7 , "18" },
                { 8 , "3" },
                { 9 , "11" },
                { 10 , "5" },
                { 26 , "True" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "last")]
        public void Indexed_Last_req_int_bool() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 2 , "0" },
                { 7 , "18" },
                { 8 , "3" },
                { 9 , "11" },
                { 10 , "5" },
                { 26 , "True" },
            });

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "last")]
        public void Media_last_req_int_bool() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 2 , "0" },
                { 7 , "18" },
                { 8 , "3" },
                { 9 , "11" },
                { 10 , "5" },
                { 26 , "True" },
            });


        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "last")]
        public void Magic_last_req_string() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 13 , "quisquam" },
                { 29 , "Laboriosam" },
                { 30 , "accusamus" },
                { 32 , "autem" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "last")]
        public void Indexed_Last_req_string() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 13 , "quisquam" },
                { 29 , "Laboriosam" },
                { 30 , "accusamus" },
                { 32 , "autem" },
            });

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "last")]
        public void Media_last_req_string() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 13 , "quisquam" },
                { 29 , "Laboriosam" },
                { 30 , "accusamus" },
                { 32 , "autem" },
            });


        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "last")]
        public void Magic_last_req_string_single() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 13 , "quo vero porro est" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "last")]
        public void Indexed_Last_req_string_single() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 13 , "quo vero porro est" },
            });

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "last")]
        public void Media_last_req_string_single() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 13 , "quo vero porro est" },
            });









        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "set1")]
        public void Magic_set1_both_int_bool() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 1 , "4" }, //optional
                { 4 , "0" }, //optional
                { 10 , "3" },
                { 26 , "False" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set1")]
        public void Indexed_set1_both_int_bool() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 1 , "4" }, //optional
                { 4 , "0" }, //optional
                { 10 , "3" },
                { 26 , "False" },
            });

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set1")]
        public void Media_set1_both_int_bool() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 1 , "4" }, //optional
                { 4 , "0" }, //optional
                { 10 , "3" },
                { 26 , "False" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "set1")]
        public void Magic_set1_req_int_bool() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 2 , "1" },
                { 7 , "1" },
                { 8 , "2" },
                { 26 , "False" },
                { 46 , "False" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set1")]
        public void Indexed_set1_req_int_bool() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 2 , "1" },
                { 7 , "1" },
                { 8 , "2" },
                { 26 , "False" },
                { 46 , "False" },
            });

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set1")]
        public void Media_set1_req_int_bool() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 2 , "1" },
                { 7 , "1" },
                { 8 , "2" },
                { 26 , "False" },
                { 46 , "False" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "set1")]
        public void Magic_set1_op_int() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 1 , "4" },
                { 3 , "8" },
                { 4 , "0" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set1")]
        public void Indexed_set1_op_int() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 1 , "4" },
                { 3 , "8" },
                { 4 , "0" },
            });

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set1")]
        public void Media_set1_op_int() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 1 , "4" },
                { 3 , "8" },
                { 4 , "0" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "set1")]
        public void Magic_set1_req_string() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 13 , "laboriosam" },
                { 29 , "voluptatibus" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set1")]
        public void Indexed_set1_req_string() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 13 , "laboriosam" },
                { 29 , "voluptatibus" },
            });

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set1")]
        public void Media_set1_req_string() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 13 , "laboriosam" },
                { 29 , "voluptatibus" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "set1")]
        public void Magic_set1_op_sring_single() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 47 , "nihil tempora" }
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set1")]
        public void Indexed_set1_op_sring_single() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 47 , "nihil tempora" }
            });


        //THIS ONE
        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set1", "miss")]
        public void Media_set1_op_sring_single() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 47 , "nihil tempora" }
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "set1")]
        public void Magic_set1_req_string_single() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 13 , "exercitationem laboriosam" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set1")]
        public void Indexed_set1_req_string_single() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 13 , "exercitationem laboriosam" },
            });

        //THIS ONE
        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set1", "miss")]
        public void Media_set1_req_string_single() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 13 , "exercitationem laboriosam" },
            });


         




        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "set2")]
        public void Magic_set2_both_int_bool() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 4 , "13" }, //optional
                { 7 , "18" },
                { 10 , "3" },
                { 26 , "False" },
                { 48 , "True" }, //optional
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set2")]
        public void Indexed_set2_both_int_bool() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 4 , "13" }, //optional
                { 7 , "18" },
                { 10 , "3" },
                { 26 , "False" },
                { 48 , "True" }, //optional
            });

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set2")]
        public void Media_set2_both_int_bool() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 4 , "13" }, //optional
                { 7 , "18" },
                { 10 , "3" },
                { 26 , "False" },
                { 48 , "True" }, //optional
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "set2")]
        public void Magic_set2_req_int() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 2 , "0" },
                { 7 , "18" },
                { 8 , "3" },
                { 9 , "11" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set2")]
        public void Indexed_set2_req_int() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 2 , "0" },
                { 7 , "18" },
                { 8 , "3" },
                { 9 , "11" },
            });

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set2")]
        public void Media_set2_req_int() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 2 , "0" },
                { 7 , "18" },
                { 8 , "3" },
                { 9 , "11" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "set2")]
        public void Magic_set2_req_string() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 13 , "quisquam" },
                { 29 , "Laboriosam" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set2")]
        public void Indexed_set2_req_string() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 13 , "quisquam" },
                { 29 , "Laboriosam" },
            });

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set2")]
        public void Media_set2_req_string() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 13 , "quisquam" },
                { 29 , "Laboriosam" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic", "set2")]
        public void Magic_set2_req_string_single() => Search.JsonSearch_EfMagic(new Dictionary<int, string>() {
                { 13 , "quo vero porro" },
            });

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "indexed", "set2")]
        public void Indexed_set2_req_string_single() => Search.JsonSearch_Indexed(new Dictionary<int, string>() {
                { 13 , "quo vero porro" },
            });

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media", "set2")]
        public void Media_set2_req_string_single() => Search.TableSearch_Media(new Dictionary<int, string>() {
                { 13 , "quo vero porro" },
            });

    }
}
