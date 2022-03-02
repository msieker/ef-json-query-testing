using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ef_json_query_testing
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DynamicListTypes",
                columns: table => new
                {
                    DynamicListTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicListTypes", x => x.DynamicListTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Media_Dynamic",
                columns: table => new
                {
                    Media_DynamicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OriginalFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: false),
                    FileWidth = table.Column<int>(type: "int", nullable: false),
                    FileHeight = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hold = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media_Dynamic", x => x.Media_DynamicId);
                });

            migrationBuilder.CreateTable(
                name: "Media_Json",
                columns: table => new
                {
                    Media_JsonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OriginalFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: false),
                    FileWidth = table.Column<int>(type: "int", nullable: false),
                    FileHeight = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hold = table.Column<bool>(type: "bit", nullable: false),
                    JsonDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media_Json", x => x.Media_JsonId);
                });

            migrationBuilder.CreateTable(
                name: "DynamicListItems",
                columns: table => new
                {
                    DynamicListItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DynamicListTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicListItems", x => x.DynamicListItemId);
                    table.ForeignKey(
                        name: "FK_DynamicListItems_DynamicListTypes_DynamicListTypeId",
                        column: x => x.DynamicListTypeId,
                        principalTable: "DynamicListTypes",
                        principalColumn: "DynamicListTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicFields",
                columns: table => new
                {
                    DynamicFieldId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsQueryable = table.Column<bool>(type: "bit", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    DynamicListTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicFields", x => x.DynamicFieldId);
                    table.ForeignKey(
                        name: "FK_DynamicFields_DynamicListItems_DynamicListTypeId",
                        column: x => x.DynamicListTypeId,
                        principalTable: "DynamicListItems",
                        principalColumn: "DynamicListItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicMediaInformation",
                columns: table => new
                {
                    DynamicMediaInformationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaId = table.Column<int>(type: "int", nullable: false),
                    FieldId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicMediaInformation", x => x.DynamicMediaInformationId);
                    table.ForeignKey(
                        name: "FK_DynamicMediaInformation_DynamicFields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "DynamicFields",
                        principalColumn: "DynamicFieldId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DynamicMediaInformation_Media_Dynamic_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media_Dynamic",
                        principalColumn: "Media_DynamicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFields_DynamicListTypeId",
                table: "DynamicFields",
                column: "DynamicListTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicListItems_DynamicListTypeId",
                table: "DynamicListItems",
                column: "DynamicListTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicMediaInformation_FieldId",
                table: "DynamicMediaInformation",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicMediaInformation_MediaId",
                table: "DynamicMediaInformation",
                column: "MediaId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicMediaInformation");

            migrationBuilder.DropTable(
                name: "Media_Json");

            migrationBuilder.DropTable(
                name: "DynamicFields");

            migrationBuilder.DropTable(
                name: "Media_Dynamic");

            migrationBuilder.DropTable(
                name: "DynamicListItems");

            migrationBuilder.DropTable(
                name: "DynamicListTypes");
        }
    }
}
