using System.Diagnostics;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using ef_json_query_testing;
using ef_json_query_testing.Benchmarks;
using Mawosoft.Extensions.BenchmarkDotNet;
using Perfolizer.Horology;

public class Program
{

    //private class Config : ManualConfig
    //{

    //    public Config()
    //    {
    //        var sql500 = "Server=(LocalDb)\\MSSQLLocalDB;Initial Catalog=ef_testing_202203071330;Integrated Security=SSPI;Connection Timeout=5;";
    //        var sql100k_m = "Server=(LocalDb)\\MSSQLLocalDB;Initial Catalog=ef_testing_large_202203081505;Integrated Security=SSPI;Connection Timeout=5;";
    //        var sql100k_b = "Server=(LocalDb)\\MSSQLLocalDB;Initial Catalog=ef_testing_large;Integrated Security=SSPI;Connection Timeout=5;";
    //        DefaultConfig.Instance
    //        AddLogger()

    //        AddJob(Job.Default
    //            .WithId("500_tracking")
    //            .WithEnvironmentVariable("BENCHMARK_NOTRACKING", "1")
    //            .WithEnvironmentVariable("BENCHMARK_SQL_CONN", sql500)
    //        );

    //        AddJob(Job.Default
    //            .WithId("500_notracking")
    //            .WithEnvironmentVariable("BENCHMARK_NOTRACKING", "0")
    //            .WithEnvironmentVariable("BENCHMARK_SQL_CONN", sql500)
    //        );

    //        AddJob(Job.Default
    //            .WithId("100k_m_tracking")
    //            .WithEnvironmentVariable("BENCHMARK_NOTRACKING", "1")
    //            .WithEnvironmentVariable("BENCHMARK_SQL_CONN", sql100k_m)
    //        );

    //        AddJob(Job.Default
    //            .WithId("100k_m_notracking")
    //            .WithEnvironmentVariable("BENCHMARK_NOTRACKING", "0")
    //            .WithEnvironmentVariable("BENCHMARK_SQL_CONN", sql100k_m)
    //        );

    //        AddJob(Job.Default
    //            .WithId("100k_b_tracking")
    //            .WithEnvironmentVariable("BENCHMARK_NOTRACKING", "1")
    //            .WithEnvironmentVariable("BENCHMARK_SQL_CONN", sql100k_b)
    //        );

    //        AddJob(Job.Default
    //            .WithId("100k_b_notracking")
    //            .WithEnvironmentVariable("BENCHMARK_NOTRACKING", "0")
    //            .WithEnvironmentVariable("BENCHMARK_SQL_CONN", sql100k_b)
    //        );
    //    }
    //}

    // run benchmarks using:
    // dotnet run -c Release
    public async static Task Main(string[] args)
    {
        //        await using var context = EfTestDbContext.Create(true);
        //CreateBogusData.LoadAllData(context);

        //var svc = new SearchService(context);

        //svc.MediaJsonSearch_RAW_SqlInterpolated(1, "7");
        //var result1 = svc.JsonSearch_Raw(new Dictionary<int, string>() { { 4, "6" }, { 24, "Suscipit" } });
        //var result2 = svc.JsonSearch_EfMagic(new Dictionary<int, string>() { { 4, "6" }, { 24, "Suscipit" } });
        //Console.WriteLine($"{result1.Count}, {result2.Count}");
        //var summary = BenchmarkRunner.Run<BenchmarkTests>();

        var sql500 = "Server=(LocalDb)\\MSSQLLocalDB;Initial Catalog=ef_testing_202203071330;Integrated Security=SSPI;Connection Timeout=5;";
        var sql100k_m = "Server=localhost;Initial Catalog=ef_testing_large_m;Integrated Security=SSPI;Connection Timeout=5;";
        var sql100k_b = "Server=localhost;Initial Catalog=ef_testing_large_b;Integrated Security=SSPI;Connection Timeout=5;";



        EfTestDbContext.ResetDefault(sql100k_m, true, Console.WriteLine);
        var benchmark = new StringSearch();
        await benchmark.GlobalSetup();
        var sw = new Stopwatch();

        Console.WriteLine("----- Magic -----");
        sw.Restart();
        await benchmark.Magic();
        sw.Stop();
        Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}ms");

        Console.WriteLine("----- Media -----");
        sw.Restart();
        await benchmark.Media();
        sw.Stop();
        Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}ms");

        var connStrs = new Dictionary<string, string>{
            {"sql100k_m", sql100k_m},
            {"sql100k_b", sql100k_b},
        };
        var tracking = new[] { "tracking" };
        var splitQuery = new[] { "nosplit" };

        var product =
            from c in connStrs.Keys
            from t in tracking
            from s in splitQuery
            select new
            {
                name = $"{c}_{t}_{s}",
                connStr = connStrs[c],
                notracking = t == "notracking" ? "1" : "0",
                split = s == "split" ? "1" : "0"
            };


        var config = DefaultConfig.Instance
            .AddExporter(RPlotExporter.Default)
            .AddColumn(CategoriesColumn.Default)
            .AddColumn(StatisticColumn.P95)
            .AddDiagnoser(MemoryDiagnoser.Default)
            .ReplaceColumnCategory(new JobColumnSelectionProvider("-EnvironmentVariables", false));

        foreach (var c in product)
        {
            config.AddJob(Job.Default
                .WithId(c.name)
                .WithMinInvokeCount(10)
                .WithMinIterationTime(new TimeInterval(1.5, TimeUnit.Second))
                .WithEnvironmentVariable("BENCHMARK_NAME", c.name)
                .WithEnvironmentVariable("BENCHMARK_NOTRACKING", c.notracking)
                .WithEnvironmentVariable("BENCHMARK_SQL_CONN", c.connStr)
                .WithEnvironmentVariable("BENCHMARK_SPLITQUERY", c.split)
            );
        }

        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
            .Run(args, config);
        //return Task.CompletedTask;
    }


}
