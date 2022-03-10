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
    public class StringSearch : BaseBenchmark
    {
        // string match should be a contains, so these should be different than the above
        private int i;
        private string str;

        [GlobalSetup]
        public async Task GlobalSetup()
        {
            var stringTypeField = await Context.DynamicFields.FirstAsync(m => m.DataType == DataTypes.StringValue);

            var faker = new Faker();
            var item = faker.PickRandom<DynamicMediaInformation>(Context.DynamicMediaInformation.Where(m => m.FieldId == stringTypeField.DynamicFieldId));

            i = item.FieldId;
            str = item.Value;
        }

        //[Benchmark]
        //[BenchmarkCategory("json", "string", "raw")]
        //public Task Raw() => Run(s=>s.JsonSearch_Raw(i, str));

        [Benchmark]
        [BenchmarkCategory("json", "string", "magic")]
        public Task Magic() => Run(s => s.JsonSearch_EfMagic(i, str));

        //[Benchmark]
        //[BenchmarkCategory("table", "string", "info")]
        //public Task Info() => Run(s => s.TableSearch_Info(i, str));

        [Benchmark]
        [BenchmarkCategory("table", "string", "media")]
        public Task Media() => Run(s => s.TableSearch_Media(i, str));
    }
}
