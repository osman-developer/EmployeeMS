using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class contractsConfiguring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Employees",
                newName: "HireDate");

            migrationBuilder.CreateTable(
                name: "ContractStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeContractTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeContractTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ContractTypeId = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractStatusId = table.Column<int>(type: "int", nullable: false),
                    SigningDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ContractTerms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeContracts_ContractStatuses_ContractStatusId",
                        column: x => x.ContractStatusId,
                        principalTable: "ContractStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeContracts_EmployeeContractTypes_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalTable: "EmployeeContractTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeContracts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ContractStatuses",
                columns: new[] { "Id", "CreationDate", "StatusName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3636), "Active" },
                    { 2, new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3640), "Terminated" },
                    { 3, new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3642), "On Leave" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeContractTypes",
                columns: new[] { "Id", "ContractTypeName", "CreationDate" },
                values: new object[,]
                {
                    { 1, "Full-time", new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3666) },
                    { 2, "Part-time", new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3669) },
                    { 3, "Temporary ", new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3671) },
                    { 4, "Contract", new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3673) },
                    { 5, "Internship", new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3675) },
                    { 6, "Freelance / Consultant", new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3677) }
                });

            migrationBuilder.UpdateData(
                table: "EmployeeFileTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3412));

            migrationBuilder.UpdateData(
                table: "EmployeeFileTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3471));

            migrationBuilder.UpdateData(
                table: "EmployeeFileTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3474));

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_ContractStatusId",
                table: "EmployeeContracts",
                column: "ContractStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_ContractTypeId",
                table: "EmployeeContracts",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_EmployeeId",
                table: "EmployeeContracts",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeContracts");

            migrationBuilder.DropTable(
                name: "ContractStatuses");

            migrationBuilder.DropTable(
                name: "EmployeeContractTypes");

            migrationBuilder.RenameColumn(
                name: "HireDate",
                table: "Employees",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "Employees",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Employees",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "EmployeeFileTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 2, 26, 15, 3, 31, 389, DateTimeKind.Local).AddTicks(3293));

            migrationBuilder.UpdateData(
                table: "EmployeeFileTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2025, 2, 26, 15, 3, 31, 389, DateTimeKind.Local).AddTicks(3357));

            migrationBuilder.UpdateData(
                table: "EmployeeFileTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2025, 2, 26, 15, 3, 31, 389, DateTimeKind.Local).AddTicks(3359));
        }
    }
}
