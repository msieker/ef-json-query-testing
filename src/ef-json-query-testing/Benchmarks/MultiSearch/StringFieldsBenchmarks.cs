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
            stringSearchFields_required2 = BenchmarkData_List_Strings_required2();
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
        public void Raw_both() => Search.JsonSearch_Raw(stringSearchFields_both);

        [Benchmark]
        [BenchmarkCategory("json", "stringfields", "both", "magic")]
        public void Magic_both() => Search.JsonSearch_EfMagic(stringSearchFields_both);

        [Benchmark]
        [BenchmarkCategory("json", "stringfields", "both", "indexed")]
        public void Indexed_both() => Search.JsonSearch_Indexed(stringSearchFields_both);

        [Benchmark]
        [BenchmarkCategory("table", "stringfields", "both", "media")]
        public void Media_both() => Search.TableSearch_Media(stringSearchFields_both);








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
        public void Raw_req() => Search.JsonSearch_Raw(stringSearchFields_required);

        [Benchmark]
        [BenchmarkCategory("json", "stringfields", "req", "magic")]
        public void Magic_req() => Search.JsonSearch_EfMagic(stringSearchFields_required);

        [Benchmark]
        [BenchmarkCategory("json", "stringfields", "req", "indexed")]
        public void Indexed_req() => Search.JsonSearch_Indexed(stringSearchFields_required);

        [Benchmark]
        [BenchmarkCategory("table", "stringfields", "req", "media")]
        public void Media_req() => Search.TableSearch_Media(stringSearchFields_required);








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
        public void Raw_op() => Search.JsonSearch_Raw(stringSearchFields_optional);

        [Benchmark]
        [BenchmarkCategory("json", "stringfields", "op", "magic")]
        public void Magic_op() => Search.JsonSearch_EfMagic(stringSearchFields_optional);

        [Benchmark]
        [BenchmarkCategory("json", "stringfields", "op", "indexed")]
        public void Indexed_op() => Search.JsonSearch_Indexed(stringSearchFields_optional);

        [Benchmark]
        [BenchmarkCategory("table", "stringfields", "op", "media")]
        public void Media_op() => Search.TableSearch_Media(stringSearchFields_optional);








        public Dictionary<int, string> stringSearchFields_required2 { get; set; }

        public Dictionary<int, string> BenchmarkData_List_Strings_required2()
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
            var requiredFields = faker.PickRandom(requiredStrings, 3);
            //var optionalField = faker.PickRandom(optionalStrings);

            // add search values to dict                
            var list = new Dictionary<int, string>();
            foreach (var field in requiredFields)
            {
                var min = field.Value.Length / 3;
                var max = (field.Value.Length - min) * 2;

                //get a chunk in the middle to search on. otherwise just take the string.
                if (min > 0 && max > 0 && min < field.Value.Length && max < field.Value.Length && min < max)
                {
                    list.Add(field.FieldId, field.Value.Substring(min, min));
                }
                else
                {
                    list.Add(field.FieldId, field.Value);
                }
            }

            return list;
        }


        [Benchmark]
        [BenchmarkCategory("json", "stringfields", "req2", "raw")]
        public void Raw_req2() => Search.JsonSearch_Raw(stringSearchFields_required2);

        [Benchmark]
        [BenchmarkCategory("json", "stringfields", "req2", "magic")]
        public void Magic_req2() => Search.JsonSearch_EfMagic(stringSearchFields_required2);

        [Benchmark]
        [BenchmarkCategory("json", "stringfields", "req2", "indexed")]
        public void Indexed_req2() => Search.JsonSearch_Indexed(stringSearchFields_required2);

        [Benchmark]
        [BenchmarkCategory("table", "stringfields", "req2", "media")]
        public void Media_req2() => Search.TableSearch_Media(stringSearchFields_required2);
    }
}
