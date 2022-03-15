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


        public IEnumerable<Dictionary<int, string>> BenchmarkData_List_LessRand_FirstItem()
        {
            //first item, simple entries
            yield return new Dictionary<int, string>() {
                { 1 , "4" }, //optional
                { 4 , "0" }, //optional
                { 5 , "5" }, //optional
                { 10 , "3" },
                { 26 , "False" },
                { 48 , "True" }, //optional
            };

            //first item, simple, required only
            yield return new Dictionary<int, string>() {
                { 2 , "1" },
                { 7 , "1" },
                { 8 , "2" },
                { 9 , "8" },
                { 10 , "3" },
                { 26 , "False" },
                { 46 , "False" },
            };

            //first item, simple, optional only
            yield return new Dictionary<int, string>() {
                { 1 , "4" },
                { 3 , "8" },
                { 4 , "0" },
                { 5 , "5" },
            };



            //first item, strings, required only
            yield return new Dictionary<int, string>() {
                { 13 , "laboriosam" },
                { 29 , "voluptatibus" },
                { 30 , "commodi" },
                { 32 , "mollitia" },
            };            

            //first item, strings, optional only
            yield return new Dictionary<int, string>() {
                { 47 , "nihil tempora" },
                { 33 , "False" },
                { 48 , "True" },
            };



            //first item, strings, optional only
            yield return new Dictionary<int, string>() {
                { 47 , "nihil tempora nemo" },
            };

            //first item, strings, required only
            yield return new Dictionary<int, string>() {
                { 13 , "exercitationem laboriosam aspernatur" },
            };
        }

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic")]
        [ArgumentsSource(nameof(BenchmarkData_List_LessRand_FirstItem))]
        public void Magic_first(Dictionary<int, string> dict) => Search.JsonSearch_EfMagic(dict);
        
        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media")]
        [ArgumentsSource(nameof(BenchmarkData_List_LessRand_FirstItem))]
        public void Media_first(Dictionary<int, string> dict) => Search.TableSearch_Media(dict);



        public IEnumerable<Dictionary<int, string>> BenchmarkData_List_LessRand_LastItem()
        {
            //last item, simple entries
            yield return new Dictionary<int, string>() {
                { 4 , "13" }, //optional
                { 6 , "7" }, //optional
                { 7 , "18" },
                { 10 , "3" },
                { 26 , "False" },
                { 33 , "True" }, //optional
                { 48 , "True" }, //optional
            };

            //last item, simple, required only
            yield return new Dictionary<int, string>() {
                { 2 , "0" },
                { 7 , "18" },
                { 8 , "3" },
                { 9 , "11" },
                { 10 , "5" },
                { 26 , "True" },
            };

            ////first item, simple, optional only
            //yield return new Dictionary<int, string>() {
            //    { 4 , "0" },
            //    { 5 , "5" },
            //};



            //last item, strings, required only
            yield return new Dictionary<int, string>() {
                { 13 , "quisquam" },
                { 29 , "Laboriosam" },
                { 30 , "accusamus" },
                { 32 , "autem" },
            };

            ////last item, strings, optional only
            //yield return new Dictionary<int, string>() {
            //    { 47 , "nihil tempora" },
            //    { 33 , "False" },
            //    { 48 , "True" },
            //};



            ////last item, strings, optional only
            //yield return new Dictionary<int, string>() {
            //    { 47 , "nihil tempora nemo" },
            //};

            //last item, strings, required only
            yield return new Dictionary<int, string>() {
                { 13 , "quo vero porro est" },
            };
        }

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic")]
        [ArgumentsSource(nameof(BenchmarkData_List_LessRand_LastItem))]
        public void Magic_last(Dictionary<int, string> dict) => Search.JsonSearch_EfMagic(dict);
        
        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media")]
        [ArgumentsSource(nameof(BenchmarkData_List_LessRand_LastItem))]
        public void Media_last(Dictionary<int, string> dict) => Search.TableSearch_Media(dict);








        public IEnumerable<Dictionary<int, string>> BenchmarkData_List_LessRand_Set1()
        {
            //first item, simple entries
            yield return new Dictionary<int, string>() {
                { 1 , "4" }, //optional
                { 4 , "0" }, //optional
                { 10 , "3" },
                { 26 , "False" },
            };

            //first item, simple, required only
            yield return new Dictionary<int, string>() {
                { 2 , "1" },
                { 7 , "1" },
                { 8 , "2" },
                { 26 , "False" },
                { 46 , "False" },
            };

            //first item, simple, optional only
            yield return new Dictionary<int, string>() {
                { 1 , "4" },
                { 3 , "8" },
                { 4 , "0" },
            };



            //first item, strings, required only
            yield return new Dictionary<int, string>() {
                { 13 , "laboriosam" },
                { 29 , "voluptatibus" },
            };

            //first item, strings, optional only
            yield return new Dictionary<int, string>() {
                { 47 , "nihil tempora" }
            };



            //first item, strings, optional only
            yield return new Dictionary<int, string>() {
                { 47 , "nihil tempora" },
            };

            //first item, strings, required only
            yield return new Dictionary<int, string>() {
                { 13 , "exercitationem laboriosam" },
            };
        }

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic")]
        [ArgumentsSource(nameof(BenchmarkData_List_LessRand_Set1))]
        public void Magic_set1(Dictionary<int, string> dict) => Search.JsonSearch_EfMagic(dict);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media")]
        [ArgumentsSource(nameof(BenchmarkData_List_LessRand_Set1))]
        public void Media_set1(Dictionary<int, string> dict) => Search.TableSearch_Media(dict);



        public IEnumerable<Dictionary<int, string>> BenchmarkData_List_LessRand_Set2()
        {
            //last item, simple entries
            yield return new Dictionary<int, string>() {
                { 4 , "13" }, //optional
                { 7 , "18" },
                { 10 , "3" },
                { 26 , "False" },
                { 48 , "True" }, //optional
            };

            //last item, simple, required only
            yield return new Dictionary<int, string>() {
                { 2 , "0" },
                { 7 , "18" },
                { 8 , "3" },
                { 9 , "11" },
            };

            ////first item, simple, optional only
            //yield return new Dictionary<int, string>() {
            //    { 4 , "0" },
            //    { 5 , "5" },
            //};



            //last item, strings, required only
            yield return new Dictionary<int, string>() {
                { 13 , "quisquam" },
                { 29 , "Laboriosam" },
            };

            ////last item, strings, optional only
            //yield return new Dictionary<int, string>() {
            //    { 47 , "nihil tempora" },
            //    { 33 , "False" },
            //    { 48 , "True" },
            //};



            ////last item, strings, optional only
            //yield return new Dictionary<int, string>() {
            //    { 47 , "nihil tempora nemo" },
            //};

            //last item, strings, required only
            yield return new Dictionary<int, string>() {
                { 13 , "quo vero porro" },
            };
        }

        [Benchmark]
        [BenchmarkCategory("json", "lessrand", "magic")]
        [ArgumentsSource(nameof(BenchmarkData_List_LessRand_Set2))]
        public void Magic_set2(Dictionary<int, string> dict) => Search.JsonSearch_EfMagic(dict);

        [Benchmark]
        [BenchmarkCategory("table", "lessrand", "media")]
        [ArgumentsSource(nameof(BenchmarkData_List_LessRand_Set2))]
        public void Media_set2(Dictionary<int, string> dict) => Search.TableSearch_Media(dict);

    }
}
