using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef_json_query_testing.Data.Models
{
    public class DynamicMediaInformation
    {
        public int DynamicMediaInformationId { get; set; }


        // Model Properties
        public string Value { get; set; } = "";


        // Relationships
        [Required]
        public int MediaId { get; set; }
        public Media_Dynamic Media { get; set; }

        [Required]
        public int FieldId { get; set; }
        public DynamicField Field { get; set; }
    }
}
