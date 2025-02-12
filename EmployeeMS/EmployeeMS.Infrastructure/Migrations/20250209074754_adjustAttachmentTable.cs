using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adjustAttachmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeImages");

            migrationBuilder.DropTable(
                name: "EmployeeImageTypes");

            migrationBuilder.CreateTable(
                name: "EmployeeFileTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFileTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeFileTypeId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeFiles_EmployeeFileTypes_EmployeeFileTypeId",
                        column: x => x.EmployeeFileTypeId,
                        principalTable: "EmployeeFileTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeFiles_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EmployeeFileTypes",
                columns: new[] { "Id", "CreationDate", "TypeName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 9, 9, 47, 53, 863, DateTimeKind.Local).AddTicks(3216), "profile_picture" },
                    { 2, new DateTime(2025, 2, 9, 9, 47, 53, 863, DateTimeKind.Local).AddTicks(3279), "id_picture" },
                    { 3, new DateTime(2025, 2, 9, 9, 47, 53, 863, DateTimeKind.Local).AddTicks(3282), "attachment" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFiles_EmployeeFileTypeId",
                table: "EmployeeFiles",
                column: "EmployeeFileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFiles_EmployeeId",
                table: "EmployeeFiles",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeFiles");

            migrationBuilder.DropTable(
                name: "EmployeeFileTypes");

            migrationBuilder.CreateTable(
                name: "EmployeeImageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeImageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeImageTypeId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeImages_EmployeeImageTypes_EmployeeImageTypeId",
                        column: x => x.EmployeeImageTypeId,
                        principalTable: "EmployeeImageTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeImages_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EmployeeImageTypes",
                columns: new[] { "Id", "CreationDate", "TypeName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 8, 16, 57, 36, 115, DateTimeKind.Local).AddTicks(894), "profile_picture" },
                    { 2, new DateTime(2025, 2, 8, 16, 57, 36, 115, DateTimeKind.Local).AddTicks(955), "id_picture" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeImages_EmployeeId",
                table: "EmployeeImages",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeImages_EmployeeImageTypeId",
                table: "EmployeeImages",
                column: "EmployeeImageTypeId");
        }
    }
}
