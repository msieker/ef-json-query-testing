﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef_json_query_testing
{
    public class DynamicListType
    {
        public DynamicListType()
        {

        }

        public DynamicListType(string name)
        {
            DisplayName = name;
        }

        public int DynamicListTypeId { get; set; }

        // Model Properties
        public string DisplayName { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Relationships
    }
}
