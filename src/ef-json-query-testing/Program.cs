using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using ef_json_query_testing;

public class Program
{
    // run benchmarks using:
    // dotnet run -c Release
    public static async Task Main(string[] args)
    {
        //for running with a debugger:
        //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());
        
        
        //await using var context = EfTestDbContext.Create(true);
        //CreateBogusData.LoadAllData(context);

        //var svc = new SearchService(context);
        
        //var raw = svc.JsonSearch_Raw(27, "46503058185780657558627466918");
        //var magic = svc.JsonSearch_EfMagic(27, "46503058185780657558627466918");
        //var info = svc.TableSearch_Info(27, "46503058185780657558627466918");
        //var media = svc.TableSearch_Media(27, "46503058185780657558627466918");
        
        //var fields = new Dictionary<int, string>() {
        //    { 30, "delectus" },
        //    { 13, "explicabo" },
        //    { 19, "dolores" }
        //};
        
        //var raw_many = svc.JsonSearch_Raw(fields);
        //var magic_many = svc.JsonSearch_EfMagic(fields);
        //var media_many = svc.TableSearch_Media(fields);


        var summary = BenchmarkRunner.Run<BenchmarkTests>();

    }
}
