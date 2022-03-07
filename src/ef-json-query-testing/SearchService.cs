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


        public List<Media_Json> JsonSearch(Dictionary<int, string> searchFields)
        {
            if (searchFields == null || searchFields.Count() == 0)
            {
                return new List<Media_Json>();
            }

            var fieldList = _context.DynamicFields.ToList();

            var sqlStatement = "SELECT * FROM [dbo].[Media_Json] WHERE 1=1 ";

            var count = 0;
            var parameters = new List<object>();
            foreach (var searchField in searchFields)
            {
                var field = fieldList.FirstOrDefault(f => f.DynamicFieldId == searchField.Key);
                if (field == null)
                {
                    continue;
                }

                parameters.Add(MakeJsonPath(field.JsonName));

                sqlStatement += $" AND JSON_VALUE([Details], {count}) ";
                count++;

                if (field.DataType == DataTypes.StringValue)
                {
                    var containsString = "%" + searchField.Value + "%";
                    parameters.Add(containsString);
                    sqlStatement += $" like {count}";
                }
                else
                {
                    parameters.Add(searchField.Value);
                    sqlStatement += $" = {count}";
                }

                count++;
            }

            return _context.Media_Json.FromSqlRaw(sqlStatement, parameters.ToArray()).ToList();
        }

        private string MakeJsonPath(string name)
        {
            return $"$.\"{name}\"";
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
