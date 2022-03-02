using BenchmarkDotNet.Running;
using ef_json_query_testing;

public class Program
{
    public static async Task Main(string[] args)
    {
        using (var context = new EfTestDbContext())
        {
            //CreateBogusData.LoadData(context);

            var summary = BenchmarkRunner.Run<BenchmarkTests>();
        }
    }
}
