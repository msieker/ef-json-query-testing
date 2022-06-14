using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ef_json_query_testing
{
    public partial class Drop_And_Create_stp_Add_Json_Index : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFile = Path.Combine("Sql/Create_stp_Add_Json_Index.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sqlFile = Path.Combine("Sql/Drop_stp_Add_Json_Index.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }
    }
}
