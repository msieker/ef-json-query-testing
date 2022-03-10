using System.ComponentModel.DataAnnotations;

namespace ef_json_query_testing.Models
{
    public class DynamicListItem
    {
        public DynamicListItem()
        {

        }

        public DynamicListItem(string name, int typeId)
        {
            DisplayName = name;
            DynamicListTypeId = typeId;
        }

        public int DynamicListItemId { get; set; }

        // Model Properties
        public string DisplayName { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Relationships
        [Required]
        public int DynamicListTypeId { get; set; }
        public DynamicListType? DynamicListType { get; set; }
    }
}
