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
    public class ListIntSearch :BaseBenchmark
    {
        private int i;
        private string str;
        [GlobalSetup]
        public async Task GlobalSetup()
        {
            var listTypeField = await Context.DynamicFields.FirstAsync(m => m.DynamicListTypeId.HasValue);

            var faker = new Faker();
            var item = faker.PickRandom<DynamicMediaInformation>(Context.DynamicMediaInformation.Where(m => m.FieldId == listTypeField.DynamicFieldId));

            i = item.FieldId;
            str=item.Value;
        }

        //[Benchmark]
        //[BenchmarkCategory("json", "listInt", "raw")]
        //public Task Raw() => Search.JsonSearch_Raw(i, str);

        [Benchmark]
        [BenchmarkCategory("json", "listInt", "magic")]
        public Task Magic() => Search.JsonSearch_EfMagic(i, str);

        //[Benchmark]
        //[BenchmarkCategory("table", "listInt", "info")]
        //public Task Info() => Search.TableSearch_Info(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "listInt", "media")]
        public Task Media() => Search.TableSearch_Media(i, str);
    }
}
