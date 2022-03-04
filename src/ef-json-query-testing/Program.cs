using BenchmarkDotNet.Running;
using ef_json_query_testing;

public class Program
{
    public static async Task Main(string[] args)
    {
        using (var context = new EfTestDbContext())
        {
            //CreateBogusData.LoadAllData(context, 30, 5000, 5);

            var summary = BenchmarkRunner.Run<BenchmarkTests>();
        }
    }
}
