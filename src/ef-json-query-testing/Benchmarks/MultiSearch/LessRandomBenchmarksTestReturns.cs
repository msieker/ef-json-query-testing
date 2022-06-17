using ef_json_query_testing.Models;

namespace ef_json_query_testing.Benchmarks
{
    public class LessRandomBenchmarksTestReturns : BaseBenchmark
    {
        public void TestSearch()
        {
            TestSearch((a) => Search.JsonSearch_Indexed(a), (b) => Search.TableSearch_Media(b));
            TestSearch((a) => Search.JsonSearch_Indexed(a), (b) => Search.TableSearch_Media_SplitQuery(b));
            TestSearch((a) => Search.JsonSearch_Indexed(a), (b) => Search.TableSearch_Media_TwoQueries(b));
            int it = 0;
        }


        public void TestSearch(Func<Dictionary<int, string>, List<Media_Json>> jsonSearch, Func<Dictionary<int, string>, List<Media_Dynamic>> dynamicSearch)
        {
            Compare(jsonSearch(TestValueConstants.first_both_bool_int), dynamicSearch(TestValueConstants.first_both_bool_int));

            Compare(jsonSearch(TestValueConstants.first_req_bool_int), dynamicSearch(TestValueConstants.first_req_bool_int));

            Compare(jsonSearch(TestValueConstants.first_op_int), dynamicSearch(TestValueConstants.first_op_int));

            Compare(jsonSearch(TestValueConstants.first_req_string), dynamicSearch(TestValueConstants.first_req_string));

            Compare(jsonSearch(TestValueConstants.first_op_string), dynamicSearch(TestValueConstants.first_op_string));

            Compare(jsonSearch(TestValueConstants.first_op_string_single), dynamicSearch(TestValueConstants.first_op_string_single));

            Compare(jsonSearch(TestValueConstants.first_req_string_single), dynamicSearch(TestValueConstants.first_req_string_single));



            Compare(jsonSearch(TestValueConstants.Last_both_int_bool), dynamicSearch(TestValueConstants.Last_both_int_bool));

            Compare(jsonSearch(TestValueConstants.Last_req_int_bool), dynamicSearch(TestValueConstants.Last_req_int_bool));

            Compare(jsonSearch(TestValueConstants.Last_req_string), dynamicSearch(TestValueConstants.Last_req_string));

            Compare(jsonSearch(TestValueConstants.Last_req_string_single), dynamicSearch(TestValueConstants.Last_req_string_single));



            Compare(jsonSearch(TestValueConstants.set1_both_int_bool), dynamicSearch(TestValueConstants.set1_both_int_bool));

            Compare(jsonSearch(TestValueConstants.set1_req_int_bool), dynamicSearch(TestValueConstants.set1_req_int_bool));

            Compare(jsonSearch(TestValueConstants.set1_op_int), dynamicSearch(TestValueConstants.set1_op_int));

            Compare(jsonSearch(TestValueConstants.set1_req_string), dynamicSearch(TestValueConstants.set1_req_string));

            Compare(jsonSearch(TestValueConstants.set1_op_string_single), dynamicSearch(TestValueConstants.set1_op_string_single));

            Compare(jsonSearch(TestValueConstants.set1_req_string_single), dynamicSearch(TestValueConstants.set1_req_string_single));



            Compare(jsonSearch(TestValueConstants.set2_both_int_bool), dynamicSearch(TestValueConstants.set2_both_int_bool));

            Compare(jsonSearch(TestValueConstants.set2_req_int), dynamicSearch(TestValueConstants.set2_req_int));

            Compare(jsonSearch(TestValueConstants.set2_req_string), dynamicSearch(TestValueConstants.set2_req_string));

            Compare(jsonSearch(TestValueConstants.set2_req_string_single), dynamicSearch(TestValueConstants.set2_req_string_single));


        }


        void Compare(List<Media_Json>? jsonResults, List<Media_Dynamic>? mediaResults)
        {
            if (jsonResults == null || mediaResults == null || !jsonResults.Any() || !mediaResults.Any())
            {
                throw new Exception();
            }

            if (jsonResults.Count != mediaResults.Count)
            {
                throw new Exception();
            }

            for (int i = 0; i < jsonResults.Count; i++)
            {
                if (!jsonResults[i].CheckMatch(mediaResults[i]))
                {
                    throw new Exception();
                }
            }
        }
    }

}
