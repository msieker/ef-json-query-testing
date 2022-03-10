using System.Diagnostics;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using ef_json_query_testing;
using ef_json_query_testing.Benchmarks;

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
    public static async Task Main(string[] args)
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
        var sql100k_m = "Server=(LocalDb)\\MSSQLLocalDB;Initial Catalog=ef_testing_large_202203081505;Integrated Security=SSPI;Connection Timeout=5;";
        var sql100k_b = "Server=(LocalDb)\\MSSQLLocalDB;Initial Catalog=ef_testing_large;Integrated Security=SSPI;Connection Timeout=5;";

        EfTestDbContext.ResetDefault(sql100k_b, true, Console.WriteLine);
        var benchmark = new StringSearch();
        var sw = new Stopwatch();
        var data = benchmark.BenchmarkData_StringFind().First();
        Console.WriteLine("----- Raw -----");
        sw.Restart();
        benchmark.Raw((int)data[0], (string)data[1]);
        sw.Stop();
        Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}ms");
        
        Console.WriteLine("----- Magic -----");
        sw.Restart();
        benchmark.Magic((int)data[0], (string)data[1]);
        sw.Stop();
        Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}ms");
        
        Console.WriteLine("----- Info -----");
        sw.Restart();
        benchmark.Info((int)data[0], (string)data[1]);
        sw.Stop();
        Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}ms");
        
        Console.WriteLine("----- Media -----");
        sw.Restart();
        benchmark.Media((int)data[0], (string)data[1]);
        sw.Stop();
        Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}ms");
        //var config = DefaultConfig.Instance
        //    //.AddJob(Job.Default
        //    //    .WithId("500_tracking")
        //    //    .WithEnvironmentVariable("BENCHMARK_NOTRACKING", "1")
        //    //    .WithEnvironmentVariable("BENCHMARK_SQL_CONN", sql500))
        //    //.AddJob(Job.Default
        //    //    .WithId("500_notracking")
        //    //    .WithEnvironmentVariable("BENCHMARK_NOTRACKING", "0")
        //    //    .WithEnvironmentVariable("BENCHMARK_SQL_CONN", sql500))
        //    .AddJob(Job.Default
        //        .WithId("100k_m_tracking")
        //        .WithEnvironmentVariable("BENCHMARK_NOTRACKING", "1")
        //        .WithEnvironmentVariable("BENCHMARK_SQL_CONN", sql100k_m))
        //    .AddJob(Job.Default
        //        .WithId("100k_m_notracking")
        //        .WithEnvironmentVariable("BENCHMARK_NOTRACKING", "0")
        //        .WithEnvironmentVariable("BENCHMARK_SQL_CONN", sql100k_m))
        //    .AddJob(Job.Default
        //        .WithId("100k_b_tracking")
        //        .WithEnvironmentVariable("BENCHMARK_NOTRACKING", "1")
        //        .WithEnvironmentVariable("BENCHMARK_SQL_CONN", sql100k_b))
        //    .AddJob(Job.Default
        //        .WithId("100k_b_notracking")
        //        .WithEnvironmentVariable("BENCHMARK_NOTRACKING", "0")
        //        .WithEnvironmentVariable("BENCHMARK_SQL_CONN", sql100k_b)
        //    ).AddExporter(RPlotExporter.Default)
        //    .AddColumn(CategoriesColumn.Default);

        //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
        //    .Run(args, config);
    }

}
