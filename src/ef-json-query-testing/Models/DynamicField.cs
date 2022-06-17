using ef_json_query_testing.Enums;

namespace ef_json_query_testing.Models
{
    public class DynamicField
    {
        public DynamicField()
        {

        }

        public DynamicField(string name)
        {
            DisplayName = name;
        }

        public int DynamicFieldId { get; set; }

        // Model Properties
        public string DisplayName { get; set; } = "";
        public bool IsQueryable { get; set; }
        public bool IsRequired { get; set; }
        public string Description { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        public DataTypes DataType { get; set; }


        // Relationships
        public int? DynamicListTypeId { get; set; }
        public DynamicListType? DynamicListType { get; set; }
    }
}
