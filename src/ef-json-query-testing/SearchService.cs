using ef_json_query_testing.Enums;
using ef_json_query_testing.Indexing;
using ef_json_query_testing.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ef_json_query_testing
{
    public partial class SearchService : ISearchService
    {
        private readonly EfTestDbContext _context;

        private const int Take_Count = 100;
        private const int Max_String_Length = 500;

        public SearchService(EfTestDbContext context)
        {
            _context = context;
        }

        #region JSON


        public List<Media_Json> JsonSearch_Indexed(Dictionary<int, string> searchFields, bool throwOnNoResults = true)
        {
            if (searchFields == null || !searchFields.Any())
            {
                return new List<Media_Json>();
            }

            var tableSearchFields = new List<SearchFields>();

            var fieldList = _context.DynamicFields.AsNoTracking().ToList();
            var hasSearchField = false;
            var searchValues = new List<string>();
            var sqlStatement = "SELECT * FROM [dbo].[Media_Json] WHERE 1=1 ";
            var count = 0;
            foreach (var searchField in searchFields)
            {
                var field = fieldList.FirstOrDefault(f => f.DynamicFieldId == searchField.Key);
                if (field == null)
                {
                    continue;
                }
                hasSearchField = true;

                searchValues.Add(field.DataType == DataTypes.StringValue ? "%" + searchField.Value + "%" : searchField.Value);

                var valueType = field.DataType.GetSqlType(Max_String_Length);
                var thing = $"$.\"{searchField.Key}\"";
                sqlStatement += " AND CONVERT(" + valueType + ", JSON_VALUE([Details], '" + thing + "'))";
                sqlStatement += field.DataType == DataTypes.StringValue ? " LIKE " : " = ";
                sqlStatement += "{" + count + "}";

                count++;
            }

            List<Media_Json> list;
            if (hasSearchField)
            {
                var q = _context.Media_Json
                    .FromSqlRaw(sqlStatement, searchValues.ToArray())
                    .AsNoTracking()
                    .OrderBy(m => m.Media_JsonId)
                    .Take(Take_Count);

                list = q.ToList();
            }
            else
            {
                list = new List<Media_Json>();
            }

            if (!list.Any() && throwOnNoResults)
            {
                throw new Exception("No items found");
            }

            return list;
        }

        public List<Media_Json> JsonSearch_Indexed_NoColumns(Dictionary<int, string> searchFields, bool throwOnNoResults = true)
        {
            if (searchFields == null || !searchFields.Any())
            {
                return new List<Media_Json>();
            }

            var tableSearchFields = new List<SearchFields>();

            var fieldList = _context.DynamicFields.AsNoTracking().ToList();
            var hasSearchField = false;
            var searchValues = new List<string>();
            var sqlColumns = " [Media_JsonId] ,[UploadDate] ,[OriginalFileName] ,[FilePath] ,[CreatedDate] ,[FileSize] ,[FileWidth] ,[FileHeight] ,[Description] ,[Hold], [Details] ";
            var sqlStatement = "SELECT " + sqlColumns + " FROM [dbo].[Media_Json] WHERE 1=1 ";
            var count = 0;
            foreach (var searchField in searchFields)
            {
                var field = fieldList.FirstOrDefault(f => f.DynamicFieldId == searchField.Key);
                if (field == null)
                {
                    continue;
                }
                hasSearchField = true;

                searchValues.Add(field.DataType == DataTypes.StringValue ? "%" + searchField.Value + "%" : searchField.Value);

                var valueType = field.DataType.GetSqlType(Max_String_Length);
                var thing = $"$.\"{searchField.Key}\"";
                sqlStatement += " AND CONVERT(" + valueType + ", JSON_VALUE([Details], '" + thing + "'))";
                sqlStatement += field.DataType == DataTypes.StringValue ? " LIKE " : " = ";
                sqlStatement += "{" + count + "}";

                count++;
            }

            List<Media_Json> list;
            if (hasSearchField)
            {
                var q = _context.Media_Json
                    .FromSqlRaw(sqlStatement, searchValues.ToArray())
                    .AsNoTracking()
                    .Select(q => new Media_Json()
                    {
                        Media_JsonId = q.Media_JsonId,
                        UploadDate = q.UploadDate,
                        OriginalFileName = q.OriginalFileName,
                        FilePath = q.FilePath,
                        CreatedDate = q.CreatedDate,
                        FileSize = q.FileSize,
                        FileWidth = q.FileWidth,
                        FileHeight = q.FileHeight,
                        Description = q.Description,
                        Hold = q.Hold
                    })
                    .OrderBy(m => m.Media_JsonId)
                    .Take(Take_Count);

                list = q.ToList();
            }
            else
            {
                list = new List<Media_Json>();
            }

            if (!list.Any() && throwOnNoResults)
            {
                throw new Exception("No items found");
            }

            return list;
        }

        #endregion


        #region Dynamic Table Store


        public List<Media_Dynamic> TableSearch_Media(Dictionary<int, string> searchFields, bool throwOnNoResults = true)
        {
            if (searchFields == null || !searchFields.Any())
            {
                return new List<Media_Dynamic>();
            }

            _context.Database.SetCommandTimeout(500);
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

                if (field.DataType == DataTypes.StringValue)
                {
                    query = query.Where(m => m.DynamicMediaInformation.Any(i => i.FieldId == field.DynamicFieldId && i.Value.Contains(searchField.Value)));
                }
                else if (field.DataType == DataTypes.BoolValue)
                {
                    var val = ConvertBoolean(searchField.Value);
                    query = query.Where(m => m.DynamicMediaInformation.Any(i => i.FieldId == field.DynamicFieldId && i.Value == val));
                }
                else
                {
                    query = query.Where(m => m.DynamicMediaInformation.Any(i => i.FieldId == field.DynamicFieldId && i.Value == searchField.Value));
                }
            }

            List<Media_Dynamic> list;
            if (hasSearchField)
            {
                list = query.OrderBy(q => q.Media_DynamicId).Take(Take_Count).ToList();
            }
            else
            {
                list = new List<Media_Dynamic>();
            }

            if (!list.Any() && throwOnNoResults)
            {
                throw new Exception("No items found");
            }

            return list;
        }

        public List<Media_Dynamic> TableSearch_Media_TwoQueries(Dictionary<int, string> searchFields, bool throwOnNoResults = true)
        {
            if (searchFields == null || !searchFields.Any())
            {
                return new List<Media_Dynamic>();
            }

            _context.Database.SetCommandTimeout(500);
            var fieldList = _context.DynamicFields.AsNoTracking().ToList();
            var query = _context.Media_Dynamic.AsNoTracking();
            var hasSearchField = false;
            foreach (var searchField in searchFields)
            {
                var field = fieldList.FirstOrDefault(f => f.DynamicFieldId == searchField.Key);
                if (field == null)
                {
                    continue;
                }
                hasSearchField = true;

                if (field.DataType == DataTypes.StringValue)
                {
                    query = query.Where(m => m.DynamicMediaInformation.Any(i => i.FieldId == field.DynamicFieldId && i.Value.Contains(searchField.Value)));
                }
                else if (field.DataType == DataTypes.BoolValue)
                {
                    var val = ConvertBoolean(searchField.Value);
                    query = query.Where(m => m.DynamicMediaInformation.Any(i => i.FieldId == field.DynamicFieldId && i.Value == val));
                }
                else
                {
                    query = query.Where(m => m.DynamicMediaInformation.Any(i => i.FieldId == field.DynamicFieldId && i.Value == searchField.Value));
                }
            }

            List<Media_Dynamic> list;
            if (hasSearchField)
            {
                var partialResults = query.Select(q => q.Media_DynamicId).OrderBy(q => q).ToList();
                list = _context.Media_Dynamic.AsNoTracking()
                                             .Include(d => d.DynamicMediaInformation)
                                             .Where(m => partialResults.Contains(m.Media_DynamicId))
                                             .OrderBy(q => q.Media_DynamicId)
                                             .Take(Take_Count)
                                             .ToList();
            }
            else
            {
                list = new List<Media_Dynamic>();
            }

            if (!list.Any() && throwOnNoResults)
            {
                throw new Exception("No items found");
            }

            return list;
        }

        public List<Media_Dynamic> TableSearch_Media_NoColumns(Dictionary<int, string> searchFields, bool throwOnNoResults = true)
        {
            if (searchFields == null || !searchFields.Any())
            {
                return new List<Media_Dynamic>();
            }

            _context.Database.SetCommandTimeout(500);
            var fieldList = _context.DynamicFields.AsNoTracking().ToList();
            var query = _context.Media_Dynamic.AsNoTracking().AsQueryable();
            var hasSearchField = false;
            foreach (var searchField in searchFields)
            {
                var field = fieldList.FirstOrDefault(f => f.DynamicFieldId == searchField.Key);
                if (field == null)
                {
                    continue;
                }
                hasSearchField = true;

                if (field.DataType == DataTypes.StringValue)
                {
                    query = query.Where(m => m.DynamicMediaInformation.Any(i => i.FieldId == field.DynamicFieldId && i.Value.Contains(searchField.Value)));
                }
                else if (field.DataType == DataTypes.BoolValue)
                {
                    var val = ConvertBoolean(searchField.Value);
                    query = query.Where(m => m.DynamicMediaInformation.Any(i => i.FieldId == field.DynamicFieldId && i.Value == val));
                }
                else
                {
                    query = query.Where(m => m.DynamicMediaInformation.Any(i => i.FieldId == field.DynamicFieldId && i.Value == searchField.Value));
                }
            }

            List<Media_Dynamic> list;
            if (hasSearchField)
            {
                list = query.OrderBy(q => q.Media_DynamicId).Take(Take_Count).ToList();
            }
            else
            {
                list = new List<Media_Dynamic>();
            }

            if (!list.Any() && throwOnNoResults)
            {
                throw new Exception("No items found");
            }

            return list;
        }


        string ConvertBoolean(string value)
        {
            if (bool.TryParse(value, out var parsed))
            {
                return parsed ? "1" : "0";
            }

            return value;
        }

        #endregion
    }

    public partial interface ISearchService
    {
        List<Media_Json> JsonSearch_Indexed(Dictionary<int, string> searchFields, bool throwOnNoResults = true);

        List<Media_Json> JsonSearch_Indexed_NoColumns(Dictionary<int, string> searchFields, bool throwOnNoResults = true);



        List<Media_Dynamic> TableSearch_Media(Dictionary<int, string> searchFields, bool throwOnNoResults = true);

        List<Media_Dynamic> TableSearch_Media_TwoQueries(Dictionary<int, string> searchFields, bool throwOnNoResults = true);

        List<Media_Dynamic> TableSearch_Media_NoColumns(Dictionary<int, string> searchFields, bool throwOnNoResults = true);
    }
}
