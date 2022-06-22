using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ef_json_query_testing
{
    public partial class removejsonnamecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JsonName",
                table: "DynamicFields");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JsonName",
                table: "DynamicFields",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
