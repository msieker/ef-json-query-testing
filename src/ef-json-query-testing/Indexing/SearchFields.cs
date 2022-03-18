using EntityFrameworkExtras.EFCore;

namespace ef_json_query_testing.Indexing
{
    [UserDefinedTableType("udt_SearchFields")]
    public class SearchFields
    {
        [UserDefinedTableTypeColumn(1)]
        public int fieldId { get; set; }

        [UserDefinedTableTypeColumn(2)]
        public string searchValue { get; set; }

        [UserDefinedTableTypeColumn(3)]
        public string valueType { get; set; }
    }
}
