using BenchmarkDotNet.Attributes;
using Bogus;
using ef_json_query_testing.Enums;
using Microsoft.EntityFrameworkCore;

namespace ef_json_query_testing.Benchmarks
{
    public class FewFieldsBenchmarks : BaseBenchmark
    {
        public FewFieldsBenchmarks()
        {
            fewSearchFields = BenchmarkData_List_Few();
        }

        public Dictionary<int, string> fewSearchFields { get; set; }

        public Dictionary<int, string> BenchmarkData_List_Few()
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

                var intFields = values.Where(v => v.Field.DataType == DataTypes.IntValue && !v.Field.DynamicListTypeId.HasValue);
                var listIntFields = values.Where(v => v.Field.DataType == DataTypes.IntValue && v.Field.DynamicListTypeId.HasValue);
                var boolFields = values.Where(v => v.Field.DataType == DataTypes.BoolValue);
                var stringFields = values.Where(v => v.Field.DataType == DataTypes.StringValue && v.Value.Length > 15);

                // if each field doesnt have an available option, try again.
                // (this is based on the idea it's easier to just pick random again, than it is to search for all the matching criteria.)
                if (!intFields.Any() || !listIntFields.Any() || !boolFields.Any() || !stringFields.Any())
                {
                    continue;
                }
                else
                {
                    // all fields have an option, dont repeat the loop.
                    loopCount = maxloops;
                }

                // pick fields to search with, one of each type.
                var intField = faker.PickRandom(intFields);
                var listIntField = faker.PickRandom(listIntFields);
                var boolField = faker.PickRandom(boolFields);
                var stringField = faker.PickRandom(stringFields);

                // add search values to dict                
                list.Add(intField.FieldId, intField.Value);
                list.Add(listIntField.FieldId, listIntField.Value);
                list.Add(boolField.FieldId, boolField.Value);
                list.Add(stringField.FieldId, faker.PickRandom(stringField.Value.Split(' ')));
            }

            //throw if something wasnt picked.
            if (list.Count < 3)
            {
                throw new Exception("Didnt find a good test value.");
            }

            return list;
        }


        [Benchmark]
        [BenchmarkCategory("json", "fewfields", "raw")]
        public void Raw() => Search.JsonSearch_Raw(fewSearchFields);

        [Benchmark]
        [BenchmarkCategory("json", "fewfields", "magic")]
        public void Magic() => Search.JsonSearch_EfMagic(fewSearchFields);

        [Benchmark]
        [BenchmarkCategory("json", "fewfields", "indexed")]
        public void Indexed() => Search.JsonSearch_Indexed(fewSearchFields);

        [Benchmark]
        [BenchmarkCategory("table", "fewfields", "media")]
        public void Media() => Search.TableSearch_Media(fewSearchFields);
    }
}
