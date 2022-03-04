using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ef_json_query_testing
{
    public partial class fixrawoption2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JsonDocument",
                table: "Media_Json");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JsonDocument",
                table: "Media_Json",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
