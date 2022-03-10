using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ef_json_query_testing
{
    public partial class fixListTypeOnFieldsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicFields_DynamicListItems_DynamicListTypeId",
                table: "DynamicFields");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicFields_DynamicListTypes_DynamicListTypeId",
                table: "DynamicFields",
                column: "DynamicListTypeId",
                principalTable: "DynamicListTypes",
                principalColumn: "DynamicListTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicFields_DynamicListTypes_DynamicListTypeId",
                table: "DynamicFields");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicFields_DynamicListItems_DynamicListTypeId",
                table: "DynamicFields",
                column: "DynamicListTypeId",
                principalTable: "DynamicListItems",
                principalColumn: "DynamicListItemId");
        }
    }
}
