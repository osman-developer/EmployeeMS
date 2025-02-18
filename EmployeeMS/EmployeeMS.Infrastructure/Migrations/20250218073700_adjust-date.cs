using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adjustdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmployeeFileTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 2, 18, 9, 36, 59, 423, DateTimeKind.Local).AddTicks(977));

            migrationBuilder.UpdateData(
                table: "EmployeeFileTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2025, 2, 18, 9, 36, 59, 423, DateTimeKind.Local).AddTicks(1031));

            migrationBuilder.UpdateData(
                table: "EmployeeFileTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2025, 2, 18, 9, 36, 59, 423, DateTimeKind.Local).AddTicks(1033));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmployeeFileTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 2, 17, 11, 39, 30, 623, DateTimeKind.Local).AddTicks(1959));

            migrationBuilder.UpdateData(
                table: "EmployeeFileTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2025, 2, 17, 11, 39, 30, 623, DateTimeKind.Local).AddTicks(2014));

            migrationBuilder.UpdateData(
                table: "EmployeeFileTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2025, 2, 17, 11, 39, 30, 623, DateTimeKind.Local).AddTicks(2016));
        }
    }
}
