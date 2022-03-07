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

                sqlStatement += $" AND JSON_VALUE([Details], {{{count}}}) ";
                count++;

                if (field.DataType == DataTypes.StringValue)
                {
                    var containsString = "%" + searchField.Value + "%";
                    parameters.Add(containsString);
                    sqlStatement += $" like {{{count}}}";
                }
                else
                {
                    parameters.Add(searchField.Value);
                    sqlStatement += $" = {{{count}}}";
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

        public List<Media_Dynamic> TableSearch_ViaInfoTable(int DynamicFieldId, string value)
        {
            var field = _context.DynamicFields.FirstOrDefault(f => f.DynamicFieldId == DynamicFieldId);

            if (field == null)
            {
                return new List<Media_Dynamic>();
            }

            //change search based on data type
            var ids = new List<int>();
            if (DataTypes.StringValue == field.DataType)
            {
                // contains search
                ids = _context.DynamicMediaInformation.Where(d => d.FieldId == DynamicFieldId && d.Value.Contains(value)).Select(d => d.MediaId).ToList();
            }
            else
            {
                // exact match search
                ids = _context.DynamicMediaInformation.Where(d => d.FieldId == DynamicFieldId && d.Value.Equals(value)).Select(d => d.MediaId).ToList();
            }

            return _context.Media_Dynamic.Include(m => m.DynamicMediaInformation).Where(m => ids.Contains(m.Media_DynamicId)).ToList();
        }

        public List<Media_Dynamic> TableSearch_ViaMediaTable(int DynamicFieldId, string value)
        {
            var field = _context.DynamicFields.FirstOrDefault(f => f.DynamicFieldId == DynamicFieldId);

            if (field == null)
            {
                return new List<Media_Dynamic>();
            }

            //change search based on data type
            if (DataTypes.StringValue == field.DataType)
            {
                // contains search
                return _context.Media_Dynamic.Where(d => d.DynamicMediaInformation.FirstOrDefault(i => i.FieldId == DynamicFieldId && i.Value.Contains(value)) != null).ToList();
            }
            else
            {
                // exact match search
                return _context.Media_Dynamic.Where(d => d.DynamicMediaInformation.FirstOrDefault(i => i.FieldId == DynamicFieldId && i.Value.Equals(value)) != null).ToList();
            }
        }

        //public List<Media_Dynamic> TableSearch_ViaInfoTable(Dictionary<int, string> searchFields)
        //{
        //    if (searchFields == null || searchFields.Count() == 0)
        //    {
        //        return new List<Media_Dynamic>();
        //    }
        //
        //    // for a set of dynamic media info with the same media id
        //    // there exists a match to each item in the given searchFields
        //
        //    var fieldList = _context.DynamicFields.ToList();
        //    IQueryable<IGrouping<int, DynamicMediaInformation>>? query = _context.DynamicMediaInformation.GroupBy(g => g.MediaId);
        //    foreach (var searchField in searchFields)
        //    {
        //        var field = fieldList.FirstOrDefault(f => f.DynamicFieldId == searchField.Key);
        //        if (field == null)
        //        {
        //            continue;
        //        }
        //
        //        if (field.DataType == DataTypes.StringValue)
        //        {
        //            query.Where(q => q.Where(g => g.FieldId == searchField.Key && g.Value.Contains(searchField.Value)) != null);
        //        }
        //        else
        //        {
        //            query.Where();
        //        }
        //    }
        //}

        
        //public List<Media_Dynamic> TableSearch_ViaMediaTable(Dictionary<int, string> searchFields)
        //{
        //    if (searchFields == null || searchFields.Count() == 0)
        //    {
        //        return new List<Media_Dynamic>();
        //    }
        //
        //    var fieldList = _context.DynamicFields.ToList();
        //    var query = _context.Media_Json;
        //    foreach (var searchField in searchFields)
        //    {
        //        var field = fieldList.FirstOrDefault(f => f.DynamicFieldId == searchField.Key);
        //        if (field == null)
        //        {
        //            continue;
        //        }
        //
        //        if (field.DataType == DataTypes.StringValue)
        //        {
        //            query.Where(q => q.);
        //        }
        //        else
        //        {
        //            query.Where();
        //        }
        //    }
        //
        //}

        #endregion
    }

    public interface ISearchService
    {
        List<Media_Json> JsonSearch(int DynamicFieldId, string value);

        List<DynamicMediaInformation> TableSearch(int DynamicFieldId, string value);
    }
}
