using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef_json_query_testing
{
    public enum DataTypes
    {
        IntValue = 0, // exact match
        StringValue = 1, // contains match
        BoolValue = 2, // exact match
        DateTimeValue = 3, //??
        DecimalValue = 4, //??
        //ListSingleValue = 5, // exact
    }
}
