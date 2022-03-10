using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ef_json_query_testing
{
    public partial class addIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DynamicMediaInformation_FieldId",
                table: "DynamicMediaInformation");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicMediaInformation_FieldId",
                table: "DynamicMediaInformation",
                column: "FieldId")
                .Annotation("SqlServer:Include", new[] { "Value", "MediaId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DynamicMediaInformation_FieldId",
                table: "DynamicMediaInformation");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicMediaInformation_FieldId",
                table: "DynamicMediaInformation",
                column: "FieldId");
        }
    }
}
