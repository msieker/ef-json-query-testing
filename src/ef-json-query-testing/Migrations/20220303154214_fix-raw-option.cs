using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ef_json_query_testing
{
    public partial class fixrawoption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JsonDetails",
                table: "Media_Json");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Media_Json",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JsonDocument",
                table: "Media_Json",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "Media_Json");

            migrationBuilder.DropColumn(
                name: "JsonDocument",
                table: "Media_Json");

            migrationBuilder.AddColumn<string>(
                name: "JsonDetails",
                table: "Media_Json",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
