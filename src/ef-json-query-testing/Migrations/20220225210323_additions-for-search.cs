using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ef_json_query_testing
{
    public partial class additionsforsearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicFields_DynamicListItems_DynamicListTypeId",
                table: "DynamicFields");

            migrationBuilder.AlterColumn<int>(
                name: "DynamicListTypeId",
                table: "DynamicFields",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "JsonName",
                table: "DynamicFields",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicFields_DynamicListItems_DynamicListTypeId",
                table: "DynamicFields",
                column: "DynamicListTypeId",
                principalTable: "DynamicListItems",
                principalColumn: "DynamicListItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicFields_DynamicListItems_DynamicListTypeId",
                table: "DynamicFields");

            migrationBuilder.DropColumn(
                name: "JsonName",
                table: "DynamicFields");

            migrationBuilder.AlterColumn<int>(
                name: "DynamicListTypeId",
                table: "DynamicFields",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicFields_DynamicListItems_DynamicListTypeId",
                table: "DynamicFields",
                column: "DynamicListTypeId",
                principalTable: "DynamicListItems",
                principalColumn: "DynamicListItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
