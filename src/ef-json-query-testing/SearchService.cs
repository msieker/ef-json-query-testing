using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ef_json_query_testing
{
    public class SearchService : ISearchService
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

            JsonElement throwaway;
            return _context.Media_Json.Where(j => j.JsonDocument.RootElement.TryGetProperty(field.JsonName, out throwaway) && j.JsonDocument.RootElement.GetProperty(field.JsonName).ValueEquals(value)).ToList();
        }

        public List<Media_Json> MediaJsonSearch_RAW_SqlInterpolated(int DynamicFieldId, string value)
        {
            var field = _context.DynamicFields.FirstOrDefault(f => f.DynamicFieldId == DynamicFieldId);
            if (field == null)
            {
                return new List<Media_Json>();
            }

            var jsonPath = $"$.\"{field.JsonName}\"";
            // FromSqlInterpolated allows for use of string interpolation but it is handled in a way to avoid sql injection.
            var results = _context.Media_Json.FromSqlInterpolated($"SELECT * FROM [dbo].[Media_Json] WHERE JSON_VALUE([Details], {jsonPath}) = {value}").ToList();
            var results2 = _context.Media_Json.FromSqlInterpolated($"SELECT * FROM [dbo].[Media_Json] WHERE FileWidth IS NOT NULL");
            return results;
        }


        #endregion


        #region Dynamic Table Store

        public List<DynamicMediaInformation> MediaTableSearch_OnlyContains(int DynamicFieldId, string value)
        {
            return _context.DynamicMediaInformation.Where(d => d.FieldId == DynamicFieldId && d.Value.Contains(value)).ToList();
        }

        public List<DynamicMediaInformation> MediaTableSearch_ContainsOrEquals(int DynamicFieldId, string value)
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

    public interface ISearchService
    {
        List<Media_Json> MediaJsonSearch_JsonDocument(int DynamicFieldId, string value);
        List<Media_Json> MediaJsonSearch_RAW_SqlInterpolated(int DynamicFieldId, string value);

        List<DynamicMediaInformation> MediaTableSearch_OnlyContains(int DynamicFieldId, string value);
        List<DynamicMediaInformation> MediaTableSearch_ContainsOrEquals(int DynamicFieldId, string value);
    }
}
