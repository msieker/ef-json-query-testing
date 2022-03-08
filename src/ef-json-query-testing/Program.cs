using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Running;
using ef_json_query_testing;

public class Program
{

    private class Config : ManualConfig
    {
        public Config()
        {
            
        }
    }

    // run benchmarks using:
    // dotnet run -c Release
    public static async Task Main(string[] args)
    {
        await using var context = EfTestDbContext.Create(true);
        //CreateBogusData.LoadAllData(context);

        var svc = new SearchService(context);

        //svc.MediaJsonSearch_RAW_SqlInterpolated(1, "7");
        //var result1 = svc.JsonSearch_Raw(new Dictionary<int, string>() { { 4, "6" }, { 24, "Suscipit" } });
        //var result2 = svc.JsonSearch_EfMagic(new Dictionary<int, string>() { { 4, "6" }, { 24, "Suscipit" } });
        //Console.WriteLine($"{result1.Count}, {result2.Count}");
        //var summary = BenchmarkRunner.Run<BenchmarkTests>();

        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
            .Run(args, DefaultConfig.Instance
                .AddExporter(RPlotExporter.Default)
                .AddColumn(CategoriesColumn.Default));
    }
}
