using ef_json_query_testing.Models;
using EntityFrameworkExtras.EFCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef_json_query_testing.Indexing
{
    [StoredProcedure("stp_Json_Search")]
    public class SearchJsonProcedure
    {
        [StoredProcedureParameter(SqlDbType.Udt, ParameterName = "searchFields")]
        public List<SearchFields> SearchFields { get; set; }

        // take, skip

        //[StoredProcedureParameter(SqlDbType.Structured, Direction = ParameterDirection.Output)]
        //public List<Media_Json>? Media { get; set; }
    }
}
