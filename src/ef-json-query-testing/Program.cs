using ef_json_query_testing.Data.Seeders;

public class Program
{
    public static async Task Main(string[] args)
    {
        using (var context = new EfTestDbContext())
        {
            CreateBogusData.LoadData(context);
        }
    }
}
