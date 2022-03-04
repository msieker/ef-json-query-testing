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

        public List<Media_Json> JsonSearch(int DynamicFieldId, string value)
        {
            var field = _context.DynamicFields.FirstOrDefault(f => f.DynamicFieldId == DynamicFieldId);
            if (field == null)
            {
                return new List<Media_Json>();
            }
                       
            // FromSqlInterpolated allows for use of string interpolation but it is handled in a way to avoid sql injection.
            var jsonPath = $"$.\"{field.JsonName}\"";

            if (field.DataType == DataTypes.StringValue)
            {
                var containsString = "%" + value + "%";
                return _context.Media_Json.FromSqlInterpolated($"SELECT * FROM [dbo].[Media_Json] WHERE JSON_VALUE([Details], {jsonPath}) like {containsString}").ToList();
            }
            else
            {
                return _context.Media_Json.FromSqlInterpolated($"SELECT * FROM [dbo].[Media_Json] WHERE JSON_VALUE([Details], {jsonPath}) = {value}").ToList();
            }
        }


        #endregion


        #region Dynamic Table Store

        public List<DynamicMediaInformation> TableSearch(int DynamicFieldId, string value)
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
        List<Media_Json> JsonSearch(int DynamicFieldId, string value);

        List<DynamicMediaInformation> TableSearch(int DynamicFieldId, string value);
    }
}
