namespace ef_json_query_testing.Benchmarks
{
    public static class TestValueConstants
    {
        public static Dictionary<int, string> first_both_bool_int = new Dictionary<int, string>() {
                { 1 , "4" }, //optional
                { 4 , "0" }, //optional
                { 5 , "5" }, //optional
                { 10 , "3" },
                { 26 , "False" },
                { 48 , "True" }, //optional
            };

        public static Dictionary<int, string> first_req_bool_int = new Dictionary<int, string>() {
                { 2 , "1" },
                { 7 , "1" },
                { 8 , "2" },
                { 9 , "8" },
                { 10 , "3" },
                { 26 , "False" },
                { 46 , "False" },
            };

        public static Dictionary<int, string> first_op_int = new Dictionary<int, string>() {
                { 1 , "4" },
                { 3 , "8" },
                { 4 , "0" },
                { 5 , "5" },
            };

        public static Dictionary<int, string> first_req_string = new Dictionary<int, string>() {
                { 13 , "laboriosam" },
                { 29 , "voluptatibus" },
                { 30 , "commodi" },
                { 32 , "mollitia" },
            };

        public static Dictionary<int, string> first_op_string = new Dictionary<int, string>() {
                { 47 , "nihil tempora" },
                { 33 , "False" },
                { 48 , "True" },
            };

        public static Dictionary<int, string> first_op_string_single = new Dictionary<int, string>() {
                { 47 , "nihil tempora nemo" },
            };

        public static Dictionary<int, string> first_req_string_single = new Dictionary<int, string>() {
                { 13 , "exercitationem laboriosam aspernatur" },
            };

        public static Dictionary<int, string> Last_both_int_bool = new Dictionary<int, string>() {
                { 4 , "13" }, //optional
                { 6 , "7" }, //optional
                { 7 , "18" },
                { 10 , "5" },
                { 26 , "True" },
                { 33 , "True" }, //optional
            };
        public static Dictionary<int, string> Last_req_int_bool = new Dictionary<int, string>() {
                { 2 , "0" },
                { 7 , "18" },
                { 8 , "3" },
                { 9 , "11" },
                { 10 , "5" },
                { 26 , "True" },
            };
        public static Dictionary<int, string> Last_req_string = new Dictionary<int, string>() {
                { 13 , "quisquam" },
                { 29 , "Laboriosam" },
                { 30 , "accusamus" },
                { 32 , "autem" },
            };
        public static Dictionary<int, string> Last_req_string_single = new Dictionary<int, string>() {
                { 13 , "quo vero porro est" },
            };

        public static Dictionary<int, string> set1_both_int_bool = new Dictionary<int, string>() {
                { 1 , "4" }, //optional
                { 4 , "0" }, //optional
                { 10 , "3" },
                { 26 , "False" },
            };
        public static Dictionary<int, string> set1_req_int_bool = new Dictionary<int, string>() {
                { 2 , "1" },
                { 7 , "1" },
                { 8 , "2" },
                { 26 , "False" },
                { 46 , "False" },
            };
        public static Dictionary<int, string> set1_op_int = new Dictionary<int, string>() {
                { 1 , "4" },
                { 3 , "8" },
                { 4 , "0" },
            };
        public static Dictionary<int, string> set1_req_string = new Dictionary<int, string>() {
                { 13 , "laboriosam" },
                { 29 , "voluptatibus" },
            };
        public static Dictionary<int, string> set1_op_string_single = new Dictionary<int, string>() {
                { 47 , "nihil tempora" }
            };
        public static Dictionary<int, string> set1_req_string_single = new Dictionary<int, string>() {
                { 13 , "exercitationem laboriosam" },
            };

        public static Dictionary<int, string> set2_both_int_bool = new Dictionary<int, string>() {
                { 4 , "13" }, //optional
                { 7 , "18" },
                { 10 , "3" },
                { 26 , "False" },
                { 48 , "True" }, //optional
            };
        public static Dictionary<int, string> set2_req_int = new Dictionary<int, string>() {
                { 2 , "0" },
                { 7 , "18" },
                { 8 , "3" },
                { 9 , "11" },
            };
        public static Dictionary<int, string> set2_req_string = new Dictionary<int, string>() {
                { 13 , "quisquam" },
                { 29 , "Laboriosam" },
            };
        public static Dictionary<int, string> set2_req_string_single = new Dictionary<int, string>() {
                { 13 , "quo vero porro" },
            };


        public static List<int>? ColumnCount_10 = new List<int>() { 3, 47, 48, 21, 39, 28, 13, 26, 37, 40 };
        public static List<int>? ColumnCount_25 = new List<int>() { 3, 47, 48, 21, 39, 28, 13, 26, 37, 40,
                                                                    4, 33, 38, 42, 31, 12, 19, 46, 16, 49,
                                                                    8, 18, 14, 36, 2 };
        public static List<int>? ColumnCount_All = null;
    }
}
