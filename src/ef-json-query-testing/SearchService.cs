using System.Runtime.CompilerServices;
using ef_json_query_testing.Translators;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ef_json_query_testing
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> AsMaybeNoTracking<T>(this DbSet<T> q, bool noTrackingFlag) where T: class
        {
            return noTrackingFlag ? q.AsNoTracking() : q.AsQueryable();
        }
        public static IQueryable<T> AsMaybeNoTracking<T>(this IQueryable<T> q, bool noTrackingFlag) where T : class
        {
            return noTrackingFlag ? q.AsNoTracking() : q.AsQueryable();
        }

        public static IQueryable<T> AsMaybeSplitQuery<T>(this DbSet<T> q, bool splitQueryFlag) where T : class
        {
            return splitQueryFlag ? q.AsSplitQuery() : q.AsQueryable();
        }
        public static IQueryable<T> AsMaybeSplitQuery<T>(this IQueryable<T> q, bool splitQueryFlag) where T : class
        {
            return splitQueryFlag ? q.AsSplitQuery() : q.AsQueryable();
        }
    }
    public class SearchService : ISearchService
    {
        private readonly EfTestDbContext _context;
        private readonly bool _doNoTracking = false;
        private readonly bool _doSplitQuery = false;
        private readonly string _benchmarkTag;
        public SearchService(EfTestDbContext context)
        {
            _benchmarkTag = Environment.GetEnvironmentVariable("BENCHMARK_NAME") ?? "";
            _doNoTracking = Environment.GetEnvironmentVariable("BENCHMARK_NOTRACKING") == "1";
            _doSplitQuery = Environment.GetEnvironmentVariable("BENCHMARK_SPLITQUERY") == "1";
            _context = context;
        }


        #region JSON

        public async Task<List<Media_Json>> JsonSearch_Raw(int DynamicFieldId, string value, int take = 50, int skip = 0, [CallerMemberName] string? callerMember = null, [CallerFilePath] string? callerPath = null)
        {
            var field = await _context.DynamicFields
                .TagWith($"{_benchmarkTag}: {callerPath}:{callerMember}")
                .TagWithCallSite()
                .AsMaybeNoTracking(_doNoTracking)
                .FirstOrDefaultAsync(f => f.DynamicFieldId == DynamicFieldId);
            if (field == null)
            {
                return new List<Media_Json>();
            }

            // FromSqlInterpolated allows for use of string interpolation but it is handled in a way to avoid sql injection.
            var jsonPath = $"$.\"{field.JsonName}\"";
            IQueryable<Media_Json> query;
            if (field.DataType == DataTypes.StringValue)
            {
                var containsString = "%" + value + "%";
                query = _context.Media_Json.FromSqlInterpolated($"SELECT * FROM [dbo].[Media_Json] WHERE JSON_VALUE([Details], {jsonPath}) like {containsString}");
            }
            else
            {
                query = _context.Media_Json.FromSqlInterpolated($"SELECT * FROM [dbo].[Media_Json] WHERE JSON_VALUE([Details], {jsonPath}) = {value}");
            }

            return await query
                .TagWith($"{_benchmarkTag}: {callerPath}:{callerMember}")
                .TagWithCallSite()
                .AsMaybeNoTracking(_doNoTracking)
                .OrderBy(q=>q.OriginalFileName)
                .Skip(skip).Take(take)
                .ToListAsync();
        }


        public async Task<List<Media_Json>> JsonSearch_Raw_CharIndex(Dictionary<int, string> searchFields, int take = 50, int skip = 0, [CallerMemberName] string? callerMember = null, [CallerFilePath] string? callerPath = null)
        {
            if (!searchFields.Any())
            {
                return new List<Media_Json>();
            }

            var fieldList = await _context.DynamicFields
                .TagWith($"{_benchmarkTag}: {callerPath}:{callerMember}")
                .TagWithCallSite()
                .AsMaybeNoTracking(_doNoTracking)
                .ToListAsync();

            var sqlStatement = "SELECT * FROM [dbo].[Media_Json] WHERE 1=1 ";

            var count = 0;
            var parameters = new List<object>();
            var keys = searchFields.Keys.ToList();
            var fields = fieldList.Where(f => keys.Contains(f.DynamicFieldId))
                .Select(f => new { f.JsonName, f.DataType, Value = searchFields[f.DynamicFieldId]});
            foreach (var field in fields)
            {
                
                parameters.Add($"$.\"{field.JsonName}\"");

                if (field.DataType == DataTypes.StringValue)
                {
                    sqlStatement += $" AND CHARINDEX(JSON_VALUE([Details], {{{count++}}}), {{{count++}}}) > 0 ";
                    parameters.Add(field.Value);
                }
                else
                {
                    sqlStatement += $" AND JSON_VALUE([Details], {{{count++}}}) = {{{count++}}}";
                    parameters.Add(field.Value);
                }
            }

            return await _context.Media_Json.FromSqlRaw(sqlStatement, parameters.ToArray())
                .TagWithCallSite()
                .TagWith($"{_benchmarkTag}: {callerPath}:{callerMember}")
                .AsMaybeNoTracking(_doNoTracking)
                .OrderBy(q => q.OriginalFileName)
                .Skip(skip).Take(take)
                .ToListAsync();
        }

        public async Task<List<Media_Json>> JsonSearch_Raw_Like(Dictionary<int, string> searchFields, int take = 50, int skip = 0, [CallerMemberName] string? callerMember = null, [CallerFilePath] string? callerPath = null)
        {
            if (!searchFields.Any())
            {
                return new List<Media_Json>();
            }

            var fieldList = await _context.DynamicFields
                .AsMaybeNoTracking(_doNoTracking)
                .TagWith($"{_benchmarkTag}: {callerPath}:{callerMember}")
                .TagWithCallSite()
                .ToListAsync();

            var sqlStatement = "SELECT * FROM [dbo].[Media_Json] WHERE 1=1 ";

            var count = 0;
            var parameters = new List<object>();
            var keys = searchFields.Keys.ToList();
            var fields = fieldList.Where(f => keys.Contains(f.DynamicFieldId))
                .Select(f => new { f.JsonName, f.DataType, Value = searchFields[f.DynamicFieldId] });
            foreach (var field in fields)
            {

                parameters.Add($"$.\"{field.JsonName}\"");

                if (field.DataType == DataTypes.StringValue)
                {
                    sqlStatement += $" AND JSON_VALUE([Details], {{{count++}}}) like {{{count++}}}";
                    parameters.Add($"%{field.Value}%");
                }
                else
                {
                    sqlStatement += $" AND JSON_VALUE([Details], {{{count++}}}) = {{{count++}}}";
                    parameters.Add(field.Value);
                }
            }

            return await _context.Media_Json.FromSqlRaw(sqlStatement, parameters.ToArray())
                .TagWith($"{_benchmarkTag}: {callerPath}:{callerMember}")
                .TagWithCallSite()
                .AsMaybeNoTracking(_doNoTracking)
                .OrderBy(q => q.OriginalFileName)
                .Skip(skip).Take(take)
                .ToListAsync();
        }



        public async Task<List<Media_Json>> JsonSearch_EfMagic(int DynamicFieldId, string value, int take = 50, int skip = 0, [CallerMemberName] string? callerMember = null, [CallerFilePath] string? callerPath = null)
        {
            var field = await  _context.DynamicFields
                .TagWith($"{_benchmarkTag}: {callerPath}:{callerMember}")
                .TagWithCallSite()
                .AsMaybeNoTracking(_doNoTracking)
                .FirstOrDefaultAsync(f => f.DynamicFieldId == DynamicFieldId);
            if (field == null)
            {
                return new List<Media_Json>();
            }

            // FromSqlInterpolated allows for use of string interpolation but it is handled in a way to avoid sql injection.
            var jsonPath = $"$.\"{field.JsonName}\"";
            IQueryable<Media_Json> query;
            if (field.DataType == DataTypes.StringValue)
            {
                query = _context.Media_Json.Where(m => EF.Functions.JsonValue(m.Details, jsonPath).Contains(value));
            }
            else
            {
                query = _context.Media_Json.Where(m => EF.Functions.JsonValue(m.Details, jsonPath) == value);
            }

            return await query
                .TagWith($"{_benchmarkTag}: {callerPath}:{callerMember}")
                .TagWithCallSite()
                .AsMaybeNoTracking(_doNoTracking)
                .OrderBy(q => q.OriginalFileName)
                .Skip(skip).Take(take)
                .ToListAsync();
        }

        public async Task<List<Media_Json>> JsonSearch_EfMagic(Dictionary<int, string> searchFields, int take = 50, int skip = 0, [CallerMemberName] string? callerMember = null, [CallerFilePath] string? callerPath = null)
        {
            if (!searchFields.Any())
            {
                return new List<Media_Json>();
            }

            var fieldList = await _context.DynamicFields
                .TagWith($"{_benchmarkTag}: {callerPath}:{callerMember}")
                .TagWithCallSite()
                .AsMaybeNoTracking(_doNoTracking)
                .ToListAsync();
            var query = _context.Media_Json.AsMaybeNoTracking(_doNoTracking).AsQueryable();
            
            foreach (var (key, value) in searchFields)
            {
                var field = fieldList.FirstOrDefault(f => f.DynamicFieldId == key);
                if (field == null)
                {
                    continue;
                }
                var jsonPath = $"$.\"{field.JsonName}\"";
                if (field.DataType == DataTypes.StringValue)
                {
                    query = query.Where(q => EF.Functions.JsonValue(q.Details, jsonPath).Contains(value));
                }
                else
                {
                    query = query.Where(q => EF.Functions.JsonValue(q.Details, jsonPath) == value);
                }
            }

            return await query
                .TagWith($"{_benchmarkTag}: {callerPath}:{callerMember}")
                .TagWithCallSite()
                .OrderBy(q => q.OriginalFileName)
                .Skip(skip).Take(take)
                .ToListAsync();
        }

        #endregion


        #region Dynamic Table Store

        public async Task<List<Media_Dynamic>> TableSearch_Info(int DynamicFieldId, string value, int take = 50, int skip = 0, [CallerMemberName] string? callerMember = null, [CallerFilePath] string? callerPath = null)
        {
            var field = await _context.DynamicFields
                .TagWith($"{_benchmarkTag}: {callerPath}:{callerMember}")
                .TagWithCallSite()
                .AsMaybeNoTracking(_doNoTracking)
                .FirstOrDefaultAsync(f => f.DynamicFieldId == DynamicFieldId);

            if (field == null)
            {
                return new List<Media_Dynamic>();
            }

            //change search based on data type
            var idQuery = _context.DynamicMediaInformation
                .AsMaybeNoTracking(_doNoTracking);
            if (DataTypes.StringValue == field.DataType)
            {
                // contains search
                idQuery = idQuery.Where(d => d.FieldId == DynamicFieldId && d.Value.Contains(value));
            }
            else
            {
                // exact match search
                idQuery = idQuery.Where(d => d.FieldId == DynamicFieldId && d.Value.Equals(value));
            }

            var ids = await idQuery
                .OrderBy(q=>q.FieldId)
                .Select(d => d.MediaId)
                .TagWith($"{_benchmarkTag}: {callerPath}:{callerMember}")
                .TagWithCallSite()
                .Skip(skip).Take(take)
                .ToListAsync();

            return await _context.Media_Dynamic
                .TagWith($"{_benchmarkTag}: {callerPath}:{callerMember}")
                .TagWithCallSite()
                .AsMaybeNoTracking(_doNoTracking)
                .Include(m => m.DynamicMediaInformation)
                .Where(m => ids.Contains(m.Media_DynamicId)).ToListAsync();
        }

        public async Task<List<Media_Dynamic>> TableSearch_Media(int DynamicFieldId, string value, int take = 50, int skip = 0, [CallerMemberName] string? callerMember = null, [CallerFilePath] string? callerPath = null)
        {
            var field = await _context.DynamicFields
                .TagWith($"{_benchmarkTag}: {callerPath}:{callerMember}")
                .TagWithCallSite()
                .AsMaybeNoTracking(_doNoTracking)
                .FirstOrDefaultAsync(f => f.DynamicFieldId == DynamicFieldId);

            if (field == null)
            {
                return new List<Media_Dynamic>();
            }

            var query = _context.Media_Dynamic
                .AsMaybeNoTracking(_doNoTracking)
                .AsMaybeSplitQuery(_doSplitQuery)
                .Include(d => d.DynamicMediaInformation)
                .AsQueryable();
            //change search based on data type
            if (DataTypes.StringValue == field.DataType)
            {
                // contains search
                query = query.Where(d => d.DynamicMediaInformation.Any(i => i.FieldId == DynamicFieldId && i.Value.Contains(value)));
            }
            else
            {
                // exact match search
                query = query.Where(d => d.DynamicMediaInformation.Any(i => i.FieldId == DynamicFieldId && i.Value.Equals(value)));
            }

            return await query
                .TagWith($"{_benchmarkTag}: {callerPath}:{callerMember}")
                .TagWithCallSite()
                .OrderBy(q => q.OriginalFileName)
                .Skip(skip).Take(take)
                .ToListAsync();
        }


        public async Task<List<Media_Dynamic>> TableSearch_Media(Dictionary<int, string> searchFields, int take = 50, int skip = 0, [CallerMemberName] string? callerMember = null, [CallerFilePath] string? callerPath = null)
        {
            if (!searchFields.Any())
            {
                return new List<Media_Dynamic>();
            }

            var fieldList = await _context.DynamicFields
                .TagWith($"{_benchmarkTag}: {callerPath}:{callerMember}")
                .TagWithCallSite()
                .AsMaybeNoTracking(_doNoTracking)
                .ToListAsync();
            var query = _context.Media_Dynamic.Include(d => d.DynamicMediaInformation)
                .AsMaybeNoTracking(_doSplitQuery)
                .AsMaybeNoTracking(_doNoTracking)
                .AsQueryable();
            foreach (var (key, value) in searchFields)
            {
                var field = fieldList.FirstOrDefault(f => f.DynamicFieldId == key);
                if (field == null)
                {
                    continue;
                }
                var jsonPath = $"$.\"{field.JsonName}\"";
                if (field.DataType == DataTypes.StringValue)
                {
                    query = query.Where(m => m.DynamicMediaInformation.Any(i => i.FieldId == field.DynamicFieldId && i.Value.Contains(value)));
                }
                else
                {
                    query = query.Where(m => m.DynamicMediaInformation.Any(i => i.FieldId == field.DynamicFieldId && i.Value == value));
                }
            }

            return await query
                .TagWith($"{_benchmarkTag}: {callerPath}:{callerMember}")
                .TagWithCallSite()
                .OrderBy(q => q.OriginalFileName)
                .Skip(skip).Take(take)
                .ToListAsync();
        }

        #endregion
    }

    public interface ISearchService
    {
        Task<List<Media_Json>> JsonSearch_Raw(int DynamicFieldId, string value, int take=50, int skip=0, [CallerMemberName] string? callerMember = null, [CallerFilePath] string? callerPath = null );
        Task<List<Media_Json>> JsonSearch_Raw_Like(Dictionary<int, string> searchFields, int take = 50, int skip = 0, [CallerMemberName] string? callerMember = null, [CallerFilePath] string? callerPath = null);
        Task<List<Media_Json>> JsonSearch_Raw_CharIndex(Dictionary<int, string> searchFields, int take = 50, int skip = 0, [CallerMemberName] string? callerMember = null, [CallerFilePath] string? callerPath = null);

        Task<List<Media_Json>> JsonSearch_EfMagic(int DynamicFieldId, string value, int take = 50, int skip = 0, [CallerMemberName] string? callerMember = null, [CallerFilePath] string? callerPath = null);
        Task<List<Media_Json>> JsonSearch_EfMagic(Dictionary<int, string> searchFields, int take = 50, int skip = 0, [CallerMemberName] string? callerMember = null, [CallerFilePath] string? callerPath = null);


        Task<List<Media_Dynamic>> TableSearch_Info(int DynamicFieldId, string value, int take = 50, int skip = 0, [CallerMemberName] string? callerMember = null, [CallerFilePath] string? callerPath = null);
        Task<List<Media_Dynamic>> TableSearch_Media(int DynamicFieldId, string value, int take = 50, int skip = 0, [CallerMemberName] string? callerMember = null, [CallerFilePath] string? callerPath = null);

        Task<List<Media_Dynamic>> TableSearch_Media(Dictionary<int, string> searchFields, int take = 50, int skip = 0, [CallerMemberName] string? callerMember = null, [CallerFilePath] string? callerPath = null);
    }
}
