using BenchmarkDotNet.Attributes;
using Bogus;
using ef_json_query_testing.Enums;
using ef_json_query_testing.Models;
using Microsoft.EntityFrameworkCore;

namespace ef_json_query_testing.Benchmarks
{
    public class StringFieldsBenchmarks : BaseBenchmark
    {
        public StringFieldsBenchmarks()
        {
            stringSearchFields_both = BenchmarkData_List_Strings_both();
            stringSearchFields_required = BenchmarkData_List_Strings_required();
            stringSearchFields_optional = BenchmarkData_List_Strings_optional();
        }

        public Dictionary<int, string> stringSearchFields_both { get; set; }

        public Dictionary<int, string> BenchmarkData_List_Strings_both()
        {
            var ids = Context.Media_Dynamic.AsNoTracking()
                .Where(m => m.DynamicMediaInformation
                             .Where(d => d.Field.DataType == DataTypes.StringValue)
                             .Select(d => d.Field.IsRequired)
                             .Distinct()
                             .Count() == 2)
                .Select(m => m.Media_DynamicId)
                .ToList();

            var faker = new Faker();
            var id = faker.PickRandom(ids);
            var mediaItem = Context.Media_Dynamic.AsNoTracking()
                .Include(m => m.DynamicMediaInformation)
                .ThenInclude(d => d.Field)
                .FirstOrDefault(m => m.Media_DynamicId == id);

            var requiredStrings = mediaItem.DynamicMediaInformation.Where(v => v.Field.IsRequired);
            var optionalStrings = mediaItem.DynamicMediaInformation.Where(v => !v.Field.IsRequired);

            // pick fields to search with, one of each type.
            var requiredField = faker.PickRandom(requiredStrings);
            var optionalField = faker.PickRandom(optionalStrings);

            // add search values to dict                
            var list = new Dictionary<int, string>
            {
                { requiredField.FieldId, requiredField.Value },
                { optionalField.FieldId, optionalField.Value }
            };

            return list;
        }


        [Benchmark]
        [BenchmarkCategory("json", "stringfields", "both", "raw")]
        public void JSON_Raw_both() => Search.JsonSearch_Raw(stringSearchFields_both);

        [Benchmark]
        [BenchmarkCategory("json", "stringfields", "both", "magic")]
        public void JSON_Magic_both() => Search.JsonSearch_EfMagic(stringSearchFields_both);

        [Benchmark]
        [BenchmarkCategory("table", "stringfields", "both", "media")]
        public void Table_Media_both() => Search.TableSearch_Media(stringSearchFields_both);


        public Dictionary<int, string> stringSearchFields_required { get; set; }

        public Dictionary<int, string> BenchmarkData_List_Strings_required()
        {
            var ids = Context.Media_Dynamic.AsNoTracking()
                .Where(m => m.DynamicMediaInformation
                             .Where(d => d.Field.DataType == DataTypes.StringValue && d.Field.IsRequired)
                             .Any())
                .Select(m => m.Media_DynamicId)
                .ToList();

            var faker = new Faker();
            var id = faker.PickRandom(ids);
            var mediaItem = Context.Media_Dynamic.AsNoTracking()
                .Include(m => m.DynamicMediaInformation)
                .ThenInclude(d => d.Field)
                .FirstOrDefault(m => m.Media_DynamicId == id);

            var requiredStrings = mediaItem.DynamicMediaInformation.Where(v => v.Field.IsRequired);
            //var optionalStrings = mediaItem.DynamicMediaInformation.Where(v => !v.Field.IsRequired);

            // pick fields to search with, one of each type.
            var requiredField = faker.PickRandom(requiredStrings);
            //var optionalField = faker.PickRandom(optionalStrings);

            // add search values to dict                
            var list = new Dictionary<int, string>
            {
                { requiredField.FieldId, requiredField.Value },
                //{ optionalField.FieldId, optionalField.Value }
            };

            return list;
        }


        [Benchmark]
        [BenchmarkCategory("json", "stringfields", "req", "raw")]
        public void JSON_Raw_req() => Search.JsonSearch_Raw(stringSearchFields_required);

        [Benchmark]
        [BenchmarkCategory("json", "stringfields", "req", "magic")]
        public void JSON_Magic_req() => Search.JsonSearch_EfMagic(stringSearchFields_required);

        [Benchmark]
        [BenchmarkCategory("table", "stringfields", "req", "media")]
        public void Table_Media_req() => Search.TableSearch_Media(stringSearchFields_required);




        public Dictionary<int, string> stringSearchFields_optional { get; set; }

        public Dictionary<int, string> BenchmarkData_List_Strings_optional()
        {
            var ids = Context.Media_Dynamic.AsNoTracking()
                .Where(m => m.DynamicMediaInformation
                             .Where(d => d.Field.DataType == DataTypes.StringValue && !d.Field.IsRequired)
                             .Any())
                .Select(m => m.Media_DynamicId)
                .ToList();

            var faker = new Faker();
            var id = faker.PickRandom(ids);
            var mediaItem = Context.Media_Dynamic.AsNoTracking()
                .Include(m => m.DynamicMediaInformation)
                .ThenInclude(d => d.Field)
                .FirstOrDefault(m => m.Media_DynamicId == id);

            //var requiredStrings = mediaItem.DynamicMediaInformation.Where(v => v.Field.IsRequired);
            var optionalStrings = mediaItem.DynamicMediaInformation.Where(v => !v.Field.IsRequired);

            // pick fields to search with, one of each type.
            //var requiredField = faker.PickRandom(requiredStrings);
            var optionalField = faker.PickRandom(optionalStrings);

            // add search values to dict                
            var list = new Dictionary<int, string>
            {
                //{ requiredField.FieldId, requiredField.Value },
                { optionalField.FieldId, optionalField.Value }
            };

            return list;
        }


        [Benchmark]
        [BenchmarkCategory("json", "stringfields", "op", "raw")]
        public void JSON_Raw_op() => Search.JsonSearch_Raw(stringSearchFields_optional);

        [Benchmark]
        [BenchmarkCategory("json", "stringfields", "op", "magic")]
        public void JSON_Magic_op() => Search.JsonSearch_EfMagic(stringSearchFields_optional);

        [Benchmark]
        [BenchmarkCategory("table", "stringfields", "op", "media")]
        public void Table_Media_op() => Search.TableSearch_Media(stringSearchFields_optional);
    }
}
