using BenchmarkDotNet.Running;
using ef_json_query_testing;
//using ef_json_query_testing.Data.Seeders;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        using (var context = new EfTestDbContext())
        {
            //CreateBogusData.LoadMediaJson(context);
            var svc = new SearchService(context);

            svc.MediaJsonSearch_RAW_SqlInterpolated(1, "17");
            //var summary = BenchmarkRunner.Run<BenchmarkTests>();
        }
    }
}
