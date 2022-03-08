using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace ef_json_query_testing.Benchmarks
{
    public class ManySearch : BaseBenchmark
    {
        public ManySearch()
        {
            searchFields = BenchmarkData_List();
        }
        public Dictionary<int, string> BenchmarkData_List()
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

                var intFields = values.Where(v => v.Field.DataType == DataTypes.IntValue && !v.Field.DynamicListTypeId.HasValue).ToList();
                var listIntFields = values.Where(v => v.Field.DataType == DataTypes.IntValue && v.Field.DynamicListTypeId.HasValue).ToList();
                var boolFields = values.Where(v => v.Field.DataType == DataTypes.BoolValue).ToList();
                var stringFields = values.Where(v => v.Field.DataType == DataTypes.StringValue && v.Value.Length > 15).ToList();

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

        //[ParamsSource(nameof(BenchmarkData_List))]
        public Dictionary<int, string> searchFields { get; set; }



        [Benchmark]
        [BenchmarkCategory("json", "many", "raw_charindex")]
        public void CharIndex() => Search.JsonSearch_Raw_CharIndex(searchFields);

        [Benchmark]
        [BenchmarkCategory("json", "many", "raw_like")]
        public void Like() => Search.JsonSearch_Raw_CharIndex(searchFields);

        [Benchmark]
        [BenchmarkCategory("json", "many", "magic")]
        public void Magic() => Search.JsonSearch_EfMagic(searchFields);

        [Benchmark]
        [BenchmarkCategory("table", "many", "media")]
        public void Media() => Search.TableSearch_Media(searchFields);
    }
}
