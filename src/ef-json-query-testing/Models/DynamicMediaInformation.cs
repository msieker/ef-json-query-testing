using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace ef_json_query_testing.Models
{
    public class DynamicMediaInformation
    {
        public DynamicMediaInformation()
        {

        }

        public DynamicMediaInformation(int mediaId, int fieldId, string value)
        {
            MediaId = mediaId;
            FieldId = fieldId;
            Value = value;
        }

        public int DynamicMediaInformationId { get; set; }


        // Model Properties
        public string Value { get; set; } = "";


        // Relationships
        [Required]
        public int MediaId { get; set; }
        public Media_Dynamic Media { get; set; } = null!;

        [Required]
        public int FieldId { get; set; }
        public DynamicField Field { get; set; } = null!;


        internal class Configuration : IEntityTypeConfiguration<DynamicMediaInformation>
        {
            public void Configure(EntityTypeBuilder<DynamicMediaInformation> builder)
            {
                builder.HasIndex(b => b.FieldId)
                    .IncludeProperties(i => new { i.Value, i.MediaId });
            }
        }
    }
}
