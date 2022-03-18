using EntityFrameworkExtras.EFCore;

namespace ef_json_query_testing.Indexing
{
    [UserDefinedTableType("SearchFields")]
    public class SearchFields
    {
        [UserDefinedTableTypeColumn(1)]
        public int FieldId { get; set; }

        [UserDefinedTableTypeColumn(2)]
        public string SearchValue { get; set; } = "";

        [UserDefinedTableTypeColumn(3)]
        public string ValueType { get; set; } = "";
    }
}
