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
    [StoredProcedure("[dbo].[stp_Search_Json]")]
    public class SearchJsonProcedure
    {
        [StoredProcedureParameter(SqlDbType.Udt)]
        public List<SearchFields>? searchFields { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Direction = ParameterDirection.Output)]
        public string queryStatement { get; set; }
    }
}
