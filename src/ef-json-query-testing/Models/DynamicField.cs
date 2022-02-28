using ef_json_query_testing.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef_json_query_testing.Data.Models
{
    public class DynamicField
    {
        public DynamicField()
        {

        }

        public DynamicField(string name)
        {
            DisplayName = name;

            //create a unique json prop name with a guid and add simplified display name for human readability.
            JsonName = MakeJsonName();
        }

        public int DynamicFieldId { get; set; }

        // Model Properties
        public string DisplayName { get; set; } = "";
        public string JsonName { get; set; } = "";
        public bool IsQueryable { get; set; }
        public bool IsRequired { get; set; }
        public string Description { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        public DataTypes DataType { get; set; }


        // Relationships
        public int? DynamicListTypeId { get; set; }
        public DynamicListItem? DynamicListType { get; set; }

        // methods

        public string MakeJsonName()
        {
            return Guid.NewGuid().ToString() + "__" + string.Concat(Array.FindAll(DisplayName.ToCharArray(), char.IsLetterOrDigit));
        }
    }
}
