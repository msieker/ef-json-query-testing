using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ef_json_query_testing
{
    public partial class Drop_And_Create_udt_and_stp_search : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var udt = Path.Combine("Sql/Create_udt_SearchFields.sql");
            migrationBuilder.Sql(File.ReadAllText(udt));

            var stp = Path.Combine("Sql/Create_stp_Search_Json.sql");
            migrationBuilder.Sql(File.ReadAllText(stp));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var stp = Path.Combine("Sql/Drop_stp_Search_Json.sql");
            migrationBuilder.Sql(File.ReadAllText(stp));

            var udt = Path.Combine("Sql/Drop_udt_SearchFields.sql");
            migrationBuilder.Sql(File.ReadAllText(udt));
        }
    }
}
