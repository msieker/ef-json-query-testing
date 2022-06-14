namespace ef_json_query_testing.Benchmarks
{
    public class LessRandomBenchmarksTestReturns : BaseBenchmark
    {
        public void TestSearch()
        {
            Compare(Search.JsonSearch_Indexed(TestValueConstants.first_both_bool_int), Search.TableSearch_Media(TestValueConstants.first_both_bool_int));

            Compare(Search.JsonSearch_Indexed(TestValueConstants.first_req_bool_int), Search.TableSearch_Media(TestValueConstants.first_req_bool_int));

            Compare(Search.JsonSearch_Indexed(TestValueConstants.first_op_int), Search.TableSearch_Media(TestValueConstants.first_op_int));

            Compare(Search.JsonSearch_Indexed(TestValueConstants.first_req_string), Search.TableSearch_Media(TestValueConstants.first_req_string));

            Compare(Search.JsonSearch_Indexed(TestValueConstants.first_op_string), Search.TableSearch_Media(TestValueConstants.first_op_string));

            Compare(Search.JsonSearch_Indexed(TestValueConstants.first_op_string_single), Search.TableSearch_Media(TestValueConstants.first_op_string_single));

            Compare(Search.JsonSearch_Indexed(TestValueConstants.first_req_string_single), Search.TableSearch_Media(TestValueConstants.first_req_string_single));



            Compare(Search.JsonSearch_Indexed(TestValueConstants.Last_both_int_bool), Search.TableSearch_Media(TestValueConstants.Last_both_int_bool));

            Compare(Search.JsonSearch_Indexed(TestValueConstants.Last_req_int_bool), Search.TableSearch_Media(TestValueConstants.Last_req_int_bool));

            Compare(Search.JsonSearch_Indexed(TestValueConstants.Last_req_string), Search.TableSearch_Media(TestValueConstants.Last_req_string));

            Compare(Search.JsonSearch_Indexed(TestValueConstants.Last_req_string_single), Search.TableSearch_Media(TestValueConstants.Last_req_string_single));



            Compare(Search.JsonSearch_Indexed(TestValueConstants.set1_both_int_bool), Search.TableSearch_Media(TestValueConstants.set1_both_int_bool));

            Compare(Search.JsonSearch_Indexed(TestValueConstants.set1_req_int_bool), Search.TableSearch_Media(TestValueConstants.set1_req_int_bool));

            Compare(Search.JsonSearch_Indexed(TestValueConstants.set1_op_int), Search.TableSearch_Media(TestValueConstants.set1_op_int));

            Compare(Search.JsonSearch_Indexed(TestValueConstants.set1_req_string), Search.TableSearch_Media(TestValueConstants.set1_req_string));

            Compare(Search.JsonSearch_Indexed(TestValueConstants.set1_op_string_single), Search.TableSearch_Media(TestValueConstants.set1_op_string_single));

            Compare(Search.JsonSearch_Indexed(TestValueConstants.set1_req_string_single), Search.TableSearch_Media(TestValueConstants.set1_req_string_single));



            Compare(Search.JsonSearch_Indexed(TestValueConstants.set2_both_int_bool), Search.TableSearch_Media(TestValueConstants.set2_both_int_bool));

            Compare(Search.JsonSearch_Indexed(TestValueConstants.set2_req_int), Search.TableSearch_Media(TestValueConstants.set2_req_int));

            Compare(Search.JsonSearch_Indexed(TestValueConstants.set2_req_string), Search.TableSearch_Media(TestValueConstants.set2_req_string));

            Compare(Search.JsonSearch_Indexed(TestValueConstants.set2_req_string_single), Search.TableSearch_Media(TestValueConstants.set2_req_string_single));


        }

        void Compare(List<Models.Media_Json>? jsonResults, List<Models.Media_Dynamic>? mediaResults)
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
                var jsonId = jsonResults[i].Media_JsonId;
                var mediaResultId = mediaResults[i].Media_DynamicId;

                if (jsonId != mediaResultId)
                {
                    throw new Exception();
                }
            }
        }
    }

}
