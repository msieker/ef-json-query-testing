﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Bogus;

namespace ef_json_query_testing.Benchmarks
{
    public class BoolSearch : BaseBenchmark
    {
        public IEnumerable<object[]> BenchmarkData_BoolMatch()
        {
            var boolTypeField = Context.DynamicFields.First(m => m.DataType == DataTypes.BoolValue);

            var faker = new Faker();
            var item = faker.PickRandom(Context.DynamicMediaInformation.Where(m => m.FieldId == boolTypeField.DynamicFieldId)).First();

            yield return new object[] { item.FieldId, item.Value };
        }

        [Benchmark]
        [BenchmarkCategory("json", "bool", "raw")]
        [ArgumentsSource(nameof(BenchmarkData_BoolMatch))]
        public void Raw(int i, string str) => Search.JsonSearch_Raw(i, str);

        [Benchmark]
        [BenchmarkCategory("json", "bool", "magic")]
        [ArgumentsSource(nameof(BenchmarkData_BoolMatch))]
        public void Magic(int i, string str) => Search.JsonSearch_EfMagic(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "bool", "info")]
        [ArgumentsSource(nameof(BenchmarkData_BoolMatch))]
        public void Info(int i, string str) => Search.TableSearch_Info(i, str);

        [Benchmark]
        [BenchmarkCategory("table", "bool", "media")]
        [ArgumentsSource(nameof(BenchmarkData_BoolMatch))]
        public void Media(int i, string str) => Search.TableSearch_Media(i, str);
    }
}
