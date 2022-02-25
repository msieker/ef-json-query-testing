using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef_json_query_testing.Data.Models
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
        public int FileWidth { get; set; }
        public int FileHeight { get; set; }
        public string Description { get; set; } = "";
        public bool Hold { get; set; }

        // Relationships
        public DynamicMediaInformation DynamicMediaInformation { get; set; }
    }
}
