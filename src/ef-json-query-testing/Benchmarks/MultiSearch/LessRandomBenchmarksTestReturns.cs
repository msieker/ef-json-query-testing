namespace ef_json_query_testing.Benchmarks
{
    public class LessRandomBenchmarksTestReturns : BaseBenchmark
    {
        public void TestSearch()
        {
            var thing1 = Search.JsonSearch_Indexed(TestValueConstants.first_both_bool_int).Count;
            var thing2 = Search.TableSearch_Media(TestValueConstants.first_both_bool_int).Count;
            if (thing1 != thing2)
            {
                throw new Exception();
            }

            var thing3 = Search.JsonSearch_Indexed(TestValueConstants.first_req_bool_int).Count;
            var thing4 = Search.TableSearch_Media(TestValueConstants.first_req_bool_int).Count;
            if (thing3 != thing4)
            {
                throw new Exception();
            }

            var thing5 = Search.JsonSearch_Indexed(TestValueConstants.first_op_int).Count;
            var thing6 = Search.TableSearch_Media(TestValueConstants.first_op_int).Count;
            if (thing5 != thing6)
            {
                throw new Exception();
            }

            var thing7 = Search.JsonSearch_Indexed(TestValueConstants.first_req_string).Count;
            var thing8 = Search.TableSearch_Media(TestValueConstants.first_req_string).Count;
            if (thing7 != thing8)
            {
                throw new Exception();
            }

            var thing9 = Search.JsonSearch_Indexed(TestValueConstants.first_op_string).Count;
            var thing10 = Search.TableSearch_Media(TestValueConstants.first_op_string).Count;
            if (thing9 != thing10)
            {
                throw new Exception();
            }

            var thing11 = Search.JsonSearch_Indexed(TestValueConstants.first_op_string_single).Count;
            var thing12 = Search.TableSearch_Media(TestValueConstants.first_op_string_single).Count;
            if (thing11 != thing12)
            {
                throw new Exception();
            }

            var thing13 = Search.JsonSearch_Indexed(TestValueConstants.first_req_string_single).Count;
            var thing14 = Search.TableSearch_Media(TestValueConstants.first_req_string_single).Count;
            if (thing13 != thing14)
            {
                throw new Exception();
            }

            var thing15 = Search.JsonSearch_Indexed(TestValueConstants.Last_both_int_bool).Count;
            var thing16 = Search.TableSearch_Media(TestValueConstants.Last_both_int_bool).Count;
            if (thing15 != thing16)
            {
                throw new Exception();
            }

            var thing17 = Search.JsonSearch_Indexed(TestValueConstants.Last_req_int_bool).Count;
            var thing18 = Search.TableSearch_Media(TestValueConstants.Last_req_int_bool).Count;
            if (thing17 != thing18)
            {
                throw new Exception();
            }

            var thing19 = Search.JsonSearch_Indexed(TestValueConstants.Last_req_string).Count;
            var thing20 = Search.TableSearch_Media(TestValueConstants.Last_req_string).Count;
            if (thing19 != thing20)
            {
                throw new Exception();
            }

            var thing21 = Search.JsonSearch_Indexed(TestValueConstants.Last_req_string_single).Count;
            var thing22 = Search.TableSearch_Media(TestValueConstants.Last_req_string_single).Count;
            if (thing21 != thing22)
            {
                throw new Exception();
            }

            var thing23 = Search.JsonSearch_Indexed(TestValueConstants.set1_both_int_bool).Count;
            var thing24 = Search.TableSearch_Media(TestValueConstants.set1_both_int_bool).Count;
            if (thing23 != thing24)
            {
                throw new Exception();
            }

            var thing25 = Search.JsonSearch_Indexed(TestValueConstants.set1_req_int_bool).Count;
            var thing26 = Search.TableSearch_Media(TestValueConstants.set1_req_int_bool).Count;
            if (thing25 != thing26)
            {
                throw new Exception();
            }

            var thing27 = Search.JsonSearch_Indexed(TestValueConstants.set1_op_int).Count;
            var thing28 = Search.TableSearch_Media(TestValueConstants.set1_op_int).Count;
            if (thing27 != thing28)
            {
                throw new Exception();
            }

            var thing29 = Search.JsonSearch_Indexed(TestValueConstants.set1_req_string).Count;
            var thing30 = Search.TableSearch_Media(TestValueConstants.set1_req_string).Count;
            if (thing29 != thing30)
            {
                throw new Exception();
            }

            var thing31 = Search.JsonSearch_Indexed(TestValueConstants.set1_op_string_single).Count;
            var thing32 = Search.TableSearch_Media(TestValueConstants.set1_op_string_single).Count;
            if (thing31 != thing32)
            {
                throw new Exception();
            }

            var thing33 = Search.JsonSearch_Indexed(TestValueConstants.set1_req_string_single).Count;
            var thing34 = Search.TableSearch_Media(TestValueConstants.set1_req_string_single).Count;
            if (thing33 != thing34)
            {
                throw new Exception();
            }

            var thing35 = Search.JsonSearch_Indexed(TestValueConstants.set2_both_int_bool).Count;
            var thing36 = Search.TableSearch_Media(TestValueConstants.set2_both_int_bool).Count;
            if (thing35 != thing36)
            {
                throw new Exception();
            }

            var thing37 = Search.JsonSearch_Indexed(TestValueConstants.set2_req_int).Count;
            var thing38 = Search.TableSearch_Media(TestValueConstants.set2_req_int).Count;
            if (thing37 != thing38)
            {
                throw new Exception();
            }

            var thing39 = Search.JsonSearch_Indexed(TestValueConstants.set2_req_string).Count;
            var thing40 = Search.TableSearch_Media(TestValueConstants.set2_req_string).Count;
            if (thing39 != thing40)
            {
                throw new Exception();
            }

            var thing41 = Search.JsonSearch_Indexed(TestValueConstants.set2_req_string_single).Count;
            var thing42 = Search.TableSearch_Media(TestValueConstants.set2_req_string_single).Count;
            if (thing41 != thing42)
            {
                throw new Exception();
            }

        }
    }

}
