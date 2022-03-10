using BenchmarkDotNet.Attributes;
using Bogus;
using ef_json_query_testing.Enums;
using Microsoft.EntityFrameworkCore;

namespace ef_json_query_testing.Benchmarks
{
    public class StringFieldsBenchmarks : BaseBenchmark
    {
        public StringFieldsBenchmarks()
        {
            stringSearchFields = BenchmarkData_List_Strings();
        }

        public Dictionary<int, string> stringSearchFields { get; set; }

        public Dictionary<int, string> BenchmarkData_List_Strings()
        {
            // all values need to be in one object
            // select an object.
            var mediaIds = Context.Media_Dynamic.AsNoTracking().Select(m => m.Media_DynamicId);
            var faker = new Faker();

            var list = new Dictionary<int, string>();
            var maxloops = 10;
            for (int loopCount = 0; loopCount < maxloops; loopCount++)
            {
                int id = faker.PickRandom<int>(mediaIds);
                var values = Context.DynamicMediaInformation.AsNoTracking().Include(i => i.Field).Where(i => i.MediaId == id).ToList();

                var requiredString = values.Where(v => v.Field.DataType == DataTypes.StringValue && v.Field.IsRequired);
                var optionalString = values.Where(v => v.Field.DataType == DataTypes.StringValue && !v.Field.IsRequired);

                // if each field doesnt have an available option, try again.
                // (this is based on the idea it's easier to just pick random again, than it is to search for all the matching criteria.)
                if (!requiredString.Any() || !optionalString.Any())
                {
                    continue;
                }
                else
                {
                    // all fields have an option, dont repeat the loop.
                    loopCount = maxloops;
                }

                // pick fields to search with, one of each type.
                var requiredField = faker.PickRandom(requiredString);
                var optionalField = faker.PickRandom(optionalString);

                // add search values to dict                
                list.Add(requiredField.FieldId, requiredField.Value);
                list.Add(optionalField.FieldId, optionalField.Value);
            }

            //throw if something wasnt picked.
            if (list.Count < 3)
            {
                throw new Exception("Didnt find a good test value.");
            }

            return list;
        }


        [Benchmark]
        [BenchmarkCategory("json", "stringfields", "raw")]
        public void Benchmark_StringFields_JSON_Raw() => Search.JsonSearch_Raw(stringSearchFields);

        [Benchmark]
        [BenchmarkCategory("json", "stringfields", "magic")]
        public void Benchmark_StringFields_JSON_Magic() => Search.JsonSearch_EfMagic(stringSearchFields);

        [Benchmark]
        [BenchmarkCategory("table", "stringfields", "media")]
        public void Benchmark_StringFields_Table_Media() => Search.TableSearch_Media(stringSearchFields);
    }
}
