using ef_json_query_testing.Data.Models;
using ef_json_query_testing.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace ef_json_query_testing
{
    public class SearchService
    {
        private readonly EfTestDbContext _context;

        public SearchService(EfTestDbContext context)
        {
            _context = context;
        }


        #region JSON

        public List<Media_Json> MediaJsonSearch_JsonDocument(int DynamicFieldId, string value)
        {
            var field = _context.DynamicFields.FirstOrDefault(f => f.DynamicFieldId == DynamicFieldId);
            if (field == null)
            {
                return new List<Media_Json>();
            }

            return _context.Media_Json.Where(j => j.JsonDocument.RootElement.GetProperty(field.JsonName).ValueEquals(value)).ToList();
        }

        public List<Media_Json> MediaJsonSearch_JsonDocumentCombo(int DynamicFieldId, string value)
        {
            var field = _context.DynamicFields.FirstOrDefault(f => f.DynamicFieldId == DynamicFieldId);
            if (field == null)
            {
                return new List<Media_Json>();
            }

            return _context.Media_Json.Where(j => j.JsonDetails != null && j.JsonDetails.Contains(value) && j.JsonDocument.RootElement.GetProperty(field.JsonName).ValueEquals(value)).ToList();
        }

        public List<Media_Json> MediaJsonSearch_Dictionary(int DynamicFieldId, string value)
        {
            var field = _context.DynamicFields.FirstOrDefault(f => f.DynamicFieldId == DynamicFieldId);
            if (field == null)
            {
                return new List<Media_Json>();
            }

            return _context.Media_Json.Where( j => j.JsonDict.ContainsKey(field.JsonName) && j.GetJsonValue(field.JsonName).Equals(value)).ToList();
        }

        public List<Media_Json> MediaJsonSearch_RAW_SqlInterpolated(int DynamicFieldId, string value)
        {
            var field = _context.DynamicFields.FirstOrDefault(f => f.DynamicFieldId == DynamicFieldId);
            if (field == null)
            {
                return new List<Media_Json>();
            }

            // FromSqlInterpolated allows for use of string interpolation but it is handled in a way to avoid sql injection.
            return _context.Media_Json.FromSqlInterpolated($"SELECT * FROM [dbo].[Media_Json] WHERE JSON_VALUE([JsonDetails], '$.{field.JsonName}') = {value}").ToList();
        }


        #endregion


        #region Dynamic Table Store

        public List<DynamicMediaInformation> DynamicMediaSearch1(int DynamicFieldId, string value)
        {
            return _context.DynamicMediaInformation.Where(d => d.FieldId == DynamicFieldId && d.Value.Contains(value)).ToList();
        }

        public List<DynamicMediaInformation> DynamicMediaSearch2(int DynamicFieldId, string value)
        {
            var field = _context.DynamicFields.FirstOrDefault(f => f.DynamicFieldId == DynamicFieldId);

            if (field == null)
            {
                return new List<DynamicMediaInformation>();
            }

            //change search based on data type
            if (DataTypes.StringValue == field.DataType)
            {
                // contains search
                return _context.DynamicMediaInformation.Where(d => d.FieldId == DynamicFieldId && d.Value.Contains(value)).ToList();
            }
            else
            {
                // exact match search
                return _context.DynamicMediaInformation.Where(d => d.FieldId == DynamicFieldId && d.Value.Equals(value)).ToList();
            }
        }

        #endregion
    }
}
