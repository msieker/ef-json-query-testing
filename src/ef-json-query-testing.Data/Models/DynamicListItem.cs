using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef_json_query_testing.Data.Models
{
    public class DynamicListItem
    {
        public int DynamicListItemId { get; set; }

        // Model Properties
        public string DisplayName { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Relationships
        [Required]
        public int DynamicListTypeId { get; set; }
        public DynamicListType DynamicListType { get; set; }
    }
}
