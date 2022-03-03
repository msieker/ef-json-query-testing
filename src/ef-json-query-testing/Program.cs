using BenchmarkDotNet.Running;
using ef_json_query_testing;

public class Program
{
    public static async Task Main(string[] args)
    {
        using (var context = new EfTestDbContext())
        {
            //CreateBogusData.LoadAllData(context);

            var svc = new SearchService(context);

            svc.MediaJsonSearch_RAW_SqlInterpolated(1, "7");

            //var summary = BenchmarkRunner.Run<BenchmarkTests>();
        }
    }
}
