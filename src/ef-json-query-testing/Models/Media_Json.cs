using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace ef_json_query_testing.Models
{
    public class Media_Json : IEquatable<Media_Json?>
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


        public override bool Equals(object? obj)
        {
            return Equals(obj as Media_Json);
        }

        public bool Equals(Media_Json? other)
        {
            return other != null &&
                   Media_JsonId == other.Media_JsonId &&
                   UploadDate == other.UploadDate &&
                   OriginalFileName == other.OriginalFileName &&
                   FilePath == other.FilePath &&
                   CreatedDate == other.CreatedDate &&
                   FileSize == other.FileSize &&
                   FileWidth == other.FileWidth &&
                   FileHeight == other.FileHeight &&
                   Description == other.Description &&
                   Hold == other.Hold &&
                   EqualityComparer<Dictionary<string, object>>.Default.Equals(Details, other.Details);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Media_JsonId);
            hash.Add(UploadDate);
            hash.Add(OriginalFileName);
            hash.Add(FilePath);
            hash.Add(CreatedDate);
            hash.Add(FileSize);
            hash.Add(FileWidth);
            hash.Add(FileHeight);
            hash.Add(Description);
            hash.Add(Hold);
            hash.Add(Details);
            return hash.ToHashCode();
        }


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
