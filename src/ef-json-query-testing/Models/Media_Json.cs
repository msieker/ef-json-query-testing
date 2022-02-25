using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ef_json_query_testing.Data.Models
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
        public int FileWidth { get; set; }
        public int FileHeight { get; set; }
        public string Description { get; set; } = "";
        public bool Hold { get; set; }


        public string? JsonDetails { get; set; }

        [NotMapped]
        public JsonDocument JsonDocument => JsonDetails != null ? JsonDocument.Parse(JsonDetails) : JsonDocument.Parse("");

        [NotMapped]
        public IDictionary<string, object> JsonDict => JsonSerializer.Deserialize<IDictionary<string, object>>(JsonDetails ?? "") ?? new Dictionary<string, object>();

        public object GetJsonValue(string jsonName)
        {
            JsonDict.TryGetValue(jsonName, out object? t);
            return t ?? "";
        }
    }
}
