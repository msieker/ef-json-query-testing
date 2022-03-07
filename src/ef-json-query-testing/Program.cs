using BenchmarkDotNet.Running;
using ef_json_query_testing;

public class Program
{
    // run benchmarks using:
    // dotnet run -c Release
    public static async Task Main(string[] args)
    {
        await using var context = EfTestDbContext.Create(Console.WriteLine);
        //CreateBogusData.LoadAllData(context);

        var svc = new SearchService(context);

        //var result = svc.JsonSearch(4, "6");
        //var result1 = svc.JsonSearch(4, "6");
        var result = svc.JsonSearch(new Dictionary<int, string>() { { 4, "6" }, {24, "Suscipit" } });
        Console.WriteLine(result.Count);
        //var summary = BenchmarkRunner.Run<BenchmarkTests>();
    }
}
