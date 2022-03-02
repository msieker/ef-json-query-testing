﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ef_json_query_testing
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


        //public string? JsonDetails { get; set; }

        //public JsonDocument JsonDocument { get; set; } = JsonDocument.Parse("{}", new JsonDocumentOptions());

        public Dictionary<string, object> Details { get; set; } = new();

        public class Media_JsonConfiguration : IEntityTypeConfiguration<Media_Json>
        {
            public void Configure(EntityTypeBuilder<Media_Json> builder)
            {

                // This Converter will perform the conversion to and from Json to the desired type
                builder.Property(e => e.Details)
                    .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => 
                        JsonSerializer.Deserialize<Dictionary<string,object>>(v, new JsonSerializerOptions(JsonSerializerDefaults.General)) 
                        ?? new Dictionary<string, object>(),
                    new ValueComparer<Dictionary<string,object>>(
                        (d1,d2)=> d1.OrderBy(kv=>kv.Key).SequenceEqual(d2.OrderBy(kv=>kv.Key)),
                        c=>c.Aggregate(0, (a, kv)=>HashCode.Combine(a, kv.Key.GetHashCode(), kv.Value.GetHashCode()))
                    ))
                    .HasColumnType("nvarchar(max)");
            }
        }
    }
}
