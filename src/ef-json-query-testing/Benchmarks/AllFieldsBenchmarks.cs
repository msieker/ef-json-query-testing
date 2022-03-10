using BenchmarkDotNet.Attributes;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace ef_json_query_testing.Benchmarks
{
    public class AllFieldsBenchmarks : BaseBenchmark
    {
        public AllFieldsBenchmarks()
        {
            allSearchFields = BenchmarkData_List_All();
        }

        public Dictionary<int, string> allSearchFields { get; set; }

        public Dictionary<int, string> BenchmarkData_List_All()
        {
            // all values need to be in one object
            // select an object.
            var mediaIds = Context.Media_Dynamic.AsNoTracking().Select(m => m.Media_DynamicId);
            var faker = new Faker();

            var list = new Dictionary<int, string>();

            int id = faker.PickRandom<int>(mediaIds);
            var mediaInfo = Context.DynamicMediaInformation.AsNoTracking().Include(i => i.Field).Where(i => i.MediaId == id).ToList();

            foreach (var info in mediaInfo)
            {
                list.Add(info.FieldId, info.Value);
            }

            return list;
        }


        [Benchmark]
        [BenchmarkCategory("json", "allfields", "raw")]
        public void JSON_Raw() => Search.JsonSearch_Raw(allSearchFields);

        [Benchmark]
        [BenchmarkCategory("json", "allfields", "magic")]
        public void JSON_Magic() => Search.JsonSearch_EfMagic(allSearchFields);

        [Benchmark]
        [BenchmarkCategory("table", "allfields", "media")]
        public void Table_Media() => Search.TableSearch_Media(allSearchFields);
    }
}
