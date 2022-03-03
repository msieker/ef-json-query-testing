﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ef_json_query_testing
{
    public class Media_Dynamic
    {
        public int Media_DynamicId { get; set; }

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

        // Relationships
        public ICollection<DynamicMediaInformation> DynamicMediaInformation { get; set; } = new List<DynamicMediaInformation>();

        // methods
        public Media_Json GetMediaJsonCopy()
        {
            var jsonModel = new Media_Json();
            jsonModel.UploadDate = UploadDate;
            jsonModel.OriginalFileName = OriginalFileName;
            jsonModel.FilePath = FilePath;
            jsonModel.CreatedDate = CreatedDate;
            jsonModel.FileSize = FileSize;
            jsonModel.FileWidth = FileWidth;
            jsonModel.FileHeight = FileHeight;
            jsonModel.Description = Description;
            jsonModel.Hold = Hold;

            var dict = new Dictionary<string, object>();
            foreach (var info in DynamicMediaInformation)
            {
                dict.Add(info.Field.JsonName, info.Value);
                jsonModel.Details.Add(info.Field.JsonName, info.Value);
            }

            jsonModel.JsonDocument = JsonDocument.Parse(JsonSerializer.Serialize(dict));

            return jsonModel;
        }
    }
}
