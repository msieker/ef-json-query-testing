using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ef_json_query_testing
{
    public partial class Alter_stp_Add_Json_Index : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFile = Path.Combine("Sql/Alter_v1_stp_add_json_index.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sqlFile = Path.Combine("Sql/Undo_Alter_v1_stp_add_json_index.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }
    }
}
