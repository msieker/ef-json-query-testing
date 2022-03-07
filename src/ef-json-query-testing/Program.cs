using BenchmarkDotNet.Running;
using ef_json_query_testing;

public class Program
{
    // run benchmarks using:
    // dotnet run -c Release
    public static async Task Main(string[] args)
    {
        await using var context = EfTestDbContext.Create(Console.WriteLine);
        CreateBogusData.LoadAllData(context);

        //var svc = new SearchService(context);

        //svc.MediaJsonSearch_RAW_SqlInterpolated(1, "7");

        //var summary = BenchmarkRunner.Run<BenchmarkTests>();
    }
}
