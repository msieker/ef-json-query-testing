using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace ef_json_query_testing.Models
{
    public class Media_Json
    {
        public int Media_JsonId { get; set; }

        // Model Properties
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public string OriginalFileName { get; set; } = "";
        public string FilePath { get; set; } = "";
        public DateTime CreatedDate { get; set; }
        public int FileSize { get; set; }
        public int? FileWidth { get; set; }
        public int? FileHeight { get; set; }
        public string Description { get; set; } = "";
        public bool Hold { get; set; }


        public Dictionary<string, object> Details { get; set; } = new();


        public class Media_JsonConfiguration : IEntityTypeConfiguration<Media_Json>
        {
            public void Configure(EntityTypeBuilder<Media_Json> builder)
            {
                //TODO:  bad option for optimizing json?
                //builder.IsMemoryOptimized();

                // This Converter will perform the conversion to and from Json to the desired type
                builder.Property(e => e.Details)
                    .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v =>
                        JsonSerializer.Deserialize<Dictionary<string, object>>(v, new JsonSerializerOptions(JsonSerializerDefaults.General))
                        ?? new Dictionary<string, object>(),
                    new ValueComparer<Dictionary<string, object>>(
                        (d1, d2) => d2 != null && d1 != null && d1.OrderBy(kv => kv.Key).SequenceEqual(d2.OrderBy(kv => kv.Key)),
                        c => c.Aggregate(0, (a, kv) => HashCode.Combine(a, kv.Key.GetHashCode(), kv.Value.GetHashCode()))
                    ));
            }
        }
    }
}
