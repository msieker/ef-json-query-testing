using ef_json_query_testing.Enums;
using ef_json_query_testing.Indexing;
using ef_json_query_testing.Models;
using ef_json_query_testing.Translators;
using EntityFrameworkExtras.EFCore;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ef_json_query_testing
{
    public class SearchService : ISearchService
    {
        private readonly EfTestDbContext _context;

        private const int Take_Count = 100;
        private const int Max_String_Length = 500;

        public SearchService(EfTestDbContext context)
        {
            _context = context;
        }

        #region JSON

        public List<Media_Json> JsonSearch_Raw(int DynamicFieldId, string value)
        {
            var field = _context.DynamicFields.AsNoTracking().FirstOrDefault(f => f.DynamicFieldId == DynamicFieldId);
            if (field == null)
            {
                return new List<Media_Json>();
            }

            // FromSqlInterpolated allows for use of string interpolation but it is handled in a way to avoid sql injection.
            var jsonPath = $"$.\"{field.DynamicFieldId}\"";

            var query = new List<Media_Json>();
            if (field.DataType == DataTypes.StringValue)
            {
                var containsString = "%" + value + "%";
                return _context.Media_Json
                    .FromSqlInterpolated($"SELECT * FROM [dbo].[Media_Json] WHERE JSON_VALUE([Details], {jsonPath}) LIKE {containsString}")
                    .AsNoTracking()
                    .OrderBy(m => m.Media_JsonId)
                    .Take(Take_Count)
                    .ToList();
            }
            else
            {
                return _context.Media_Json
                    .FromSqlInterpolated($"SELECT * FROM [dbo].[Media_Json] WHERE JSON_VALUE([Details], {jsonPath}) = {value}")
                    .AsNoTracking()
                    .OrderBy(m => m.Media_JsonId)
                    .Take(Take_Count)
                    .ToList();
            }
        }


        public List<Media_Json> JsonSearch_Raw(Dictionary<int, string> searchFields)
        {
            if (searchFields == null || !searchFields.Any())
            {
                return new List<Media_Json>();
            }

            var fieldList = _context.DynamicFields.AsNoTracking().ToList();

            var sqlStatement = "SELECT * FROM [dbo].[Media_Json] WHERE 1=1 ";

            var count = 0;
            var parameters = new List<object>();
            var hasSearchField = false;
            foreach (var searchField in searchFields)
            {
                var field = fieldList.FirstOrDefault(f => f.DynamicFieldId == searchField.Key);
                if (field == null)
                {
                    continue;
                }

                hasSearchField = true;

                parameters.Add($"$.\"{field.DynamicFieldId}\"");
                sqlStatement += $" AND JSON_VALUE([Details], {{{count}}}) ";
                count++;

                if (field.DataType == DataTypes.StringValue)
                {
                    var containsString = "%" + searchField.Value + "%";
                    parameters.Add(containsString);
                    sqlStatement += $" LIKE {{{count}}}";
                }
                else
                {
                    parameters.Add(searchField.Value);
                    sqlStatement += $" = {{{count}}}";
                }

                count++;
            }

            if (hasSearchField)
            {
                return _context.Media_Json
                .FromSqlRaw(sqlStatement, parameters.ToArray())
                .AsNoTracking()
                .OrderBy(m => m.Media_JsonId)
                .Take(Take_Count)
                .ToList();
            }
            else
            {
                return new List<Media_Json>();
            }
        }



        public List<Media_Json> JsonSearch_EfMagic(int DynamicFieldId, string value)
        {
            var field = _context.DynamicFields
                .AsNoTracking()
                .FirstOrDefault(f => f.DynamicFieldId == DynamicFieldId);

            if (field == null)
            {
                return new List<Media_Json>();
            }

            var jsonPath = $"$.\"{field.DynamicFieldId}\"";
            if (field.DataType == DataTypes.StringValue)
            {
                return _context.Media_Json
                    .AsNoTracking()
                    .Where(m => EF.Functions.JsonValue(m.Details, jsonPath).Contains(value))
                    .OrderBy(m => m.Media_JsonId)
                    .Take(Take_Count)
                    .ToList();
            }
            else
            {
                return _context.Media_Json
                    .AsNoTracking()
                    .Where(m => EF.Functions.JsonValue(m.Details, jsonPath) == value)
                    .OrderBy(m => m.Media_JsonId)
                    .Take(Take_Count)
                    .ToList();
            }
        }

        public List<Media_Json> JsonSearch_EfMagic(Dictionary<int, string> searchFields)
        {
            if (searchFields == null || !searchFields.Any())
            {
                return new List<Media_Json>();
            }

            var fieldList = _context.DynamicFields.AsNoTracking().ToList();
            var query = _context.Media_Json.AsNoTracking().AsQueryable();
            var hasSearchField = false;
            foreach (var searchField in searchFields)
            {
                var field = fieldList.FirstOrDefault(f => f.DynamicFieldId == searchField.Key);
                if (field == null)
                {
                    continue;
                }
                hasSearchField = true;

                var jsonPath = $"$.\"{field.DynamicFieldId}\"";
                if (field.DataType == DataTypes.StringValue)
                {
                    query = query.Where(q => EF.Functions.JsonValue(q.Details, jsonPath).Contains(searchField.Value));
                }
                else
                {
                    query = query.Where(q => EF.Functions.JsonValue(q.Details, jsonPath) == searchField.Value);
                }
            }

            if (hasSearchField)
            {
                return query
                    .OrderBy(m => m.Media_JsonId)
                    .Take(Take_Count)
                    .ToList();
            }
            else
            {
                return new List<Media_Json>();
            }
        }


        public List<Media_Json> JsonSearch_Indexed(Dictionary<int, string> searchFields)
        {
            if (searchFields == null || !searchFields.Any())
            {
                return new List<Media_Json>();
            }

            var tableSearchFields = new List<SearchFields>();

            var fieldList = _context.DynamicFields.AsNoTracking().ToList();
            var hasSearchField = false;
            foreach (var searchField in searchFields)
            {
                var field = fieldList.FirstOrDefault(f => f.DynamicFieldId == searchField.Key);
                if (field == null)
                {
                    continue;
                }

                hasSearchField = true;

                var record = new SearchFields()
                {
                    fieldId = searchField.Key,
                    searchValue = searchField.Value,
                    valueType = field.DataType.GetSqlType(Max_String_Length)
                };

                tableSearchFields.Add(record);
            }

            if (hasSearchField)
            {
                var proc = new SearchJsonProcedure()
                {
                    searchFields = tableSearchFields
                };

                _context.Database.ExecuteStoredProcedure(proc);

                var q = _context.Media_Json
                    //.FromSqlRaw(proc.queryStatement)
                    .AsNoTracking()
                    .OrderBy(m => m.Media_JsonId)
                    .Take(Take_Count);

                return q.ToList();
            }
            else
            {
                return new List<Media_Json>();
            }
        }

        #endregion


        #region Dynamic Table Store

        public List<Media_Dynamic> TableSearch_Info(int DynamicFieldId, string value)
        {
            var field = _context.DynamicFields
                .AsNoTracking()
                .FirstOrDefault(f => f.DynamicFieldId == DynamicFieldId);

            if (field == null)
            {
                return new List<Media_Dynamic>();
            }

            //change search based on data type
            var ids = new List<int>();
            if (DataTypes.StringValue == field.DataType)
            {
                // contains search
                ids = _context.DynamicMediaInformation
                    .AsNoTracking()
                    .Where(d => d.FieldId == DynamicFieldId && d.Value.Contains(value))
                    .Select(d => d.MediaId)
                    .ToList();
            }
            else
            {
                // exact match search
                ids = _context.DynamicMediaInformation
                    .AsNoTracking()
                    .Where(d => d.FieldId == DynamicFieldId && d.Value.Equals(value))
                    .Select(d => d.MediaId)
                    .ToList();
            }

            return _context.Media_Dynamic
                .AsNoTracking()
                .Include(m => m.DynamicMediaInformation)
                .Where(m => ids.Contains(m.Media_DynamicId))
                .OrderBy(m => m.Media_DynamicId)
                .Take(Take_Count)
                .ToList();
        }

        public List<Media_Dynamic> TableSearch_Media(int DynamicFieldId, string value)
        {
            var field = _context.DynamicFields.AsNoTracking().FirstOrDefault(f => f.DynamicFieldId == DynamicFieldId);

            if (field == null)
            {
                return new List<Media_Dynamic>();
            }

            //change search based on data type
            if (DataTypes.StringValue == field.DataType)
            {
                // contains search
                return _context.Media_Dynamic
                    .AsNoTracking()
                    .Include(d => d.DynamicMediaInformation)
                    .Where(d => d.DynamicMediaInformation.FirstOrDefault(i => i.FieldId == DynamicFieldId && i.Value.Contains(value)) != null)
                    .OrderBy(m => m.Media_DynamicId)
                    .Take(Take_Count)
                    .ToList();
            }
            else
            {
                // exact match search
                return _context.Media_Dynamic
                    .AsNoTracking()
                    .Include(d => d.DynamicMediaInformation)
                    .Where(d => d.DynamicMediaInformation.FirstOrDefault(i => i.FieldId == DynamicFieldId && i.Value.Equals(value)) != null)
                    .OrderBy(m => m.Media_DynamicId)
                    .Take(Take_Count)
                    .ToList();
            }
        }


        public List<Media_Dynamic> TableSearch_Media(Dictionary<int, string> searchFields)
        {
            if (searchFields == null || !searchFields.Any())
            {
                return new List<Media_Dynamic>();
            }

            var fieldList = _context.DynamicFields.AsNoTracking().ToList();
            var query = _context.Media_Dynamic.AsNoTracking().Include(d => d.DynamicMediaInformation).AsQueryable();
            var hasSearchField = false;
            foreach (var searchField in searchFields)
            {
                var field = fieldList.FirstOrDefault(f => f.DynamicFieldId == searchField.Key);
                if (field == null)
                {
                    continue;
                }
                hasSearchField = true;

                var jsonPath = $"$.\"{field.DynamicFieldId}\"";
                if (field.DataType == DataTypes.StringValue)
                {
                    query = query.Where(m => m.DynamicMediaInformation.Any(i => i.FieldId == field.DynamicFieldId && i.Value.Contains(searchField.Value)));
                }
                else
                {
                    query = query.Where(m => m.DynamicMediaInformation.Any(i => i.FieldId == field.DynamicFieldId && i.Value == searchField.Value));
                }
            }

            if (hasSearchField)
            {
                return query.OrderBy(q => q.Media_DynamicId).Take(Take_Count).ToList();
            }
            else
            {
                return new List<Media_Dynamic>();
            }
        }

        #endregion
    }

    public interface ISearchService
    {
        List<Media_Json> JsonSearch_Raw(int DynamicFieldId, string value);
        List<Media_Json> JsonSearch_Raw(Dictionary<int, string> searchFields);

        List<Media_Json> JsonSearch_EfMagic(int DynamicFieldId, string value);
        List<Media_Json> JsonSearch_EfMagic(Dictionary<int, string> searchFields);

        List<Media_Json> JsonSearch_Indexed(Dictionary<int, string> searchFields);

        List<Media_Dynamic> TableSearch_Info(int DynamicFieldId, string value);

        List<Media_Dynamic> TableSearch_Media(int DynamicFieldId, string value);
        List<Media_Dynamic> TableSearch_Media(Dictionary<int, string> searchFields);
    }
}
