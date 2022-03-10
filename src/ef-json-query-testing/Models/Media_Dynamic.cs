namespace ef_json_query_testing.Models
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

            foreach (var info in DynamicMediaInformation)
            {
                jsonModel.Details.Add(info.Field.JsonName, info.Value);
            }

            return jsonModel;
        }
    }
}
