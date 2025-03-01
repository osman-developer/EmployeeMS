using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class datefieldfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "SigningDate",
                table: "EmployeeContracts",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "ContractStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6576));

            migrationBuilder.UpdateData(
                table: "ContractStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6579));

            migrationBuilder.UpdateData(
                table: "ContractStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6581));

            migrationBuilder.UpdateData(
                table: "EmployeeContractTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6607));

            migrationBuilder.UpdateData(
                table: "EmployeeContractTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6610));

            migrationBuilder.UpdateData(
                table: "EmployeeContractTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6612));

            migrationBuilder.UpdateData(
                table: "EmployeeContractTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6614));

            migrationBuilder.UpdateData(
                table: "EmployeeContractTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6616));

            migrationBuilder.UpdateData(
                table: "EmployeeContractTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6618));

            migrationBuilder.UpdateData(
                table: "EmployeeFileTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6334));

            migrationBuilder.UpdateData(
                table: "EmployeeFileTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6400));

            migrationBuilder.UpdateData(
                table: "EmployeeFileTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6402));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "SigningDate",
                table: "EmployeeContracts",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ContractStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3636));

            migrationBuilder.UpdateData(
                table: "ContractStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3640));

            migrationBuilder.UpdateData(
                table: "ContractStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3642));

            migrationBuilder.UpdateData(
                table: "EmployeeContractTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3666));

            migrationBuilder.UpdateData(
                table: "EmployeeContractTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3669));

            migrationBuilder.UpdateData(
                table: "EmployeeContractTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3671));

            migrationBuilder.UpdateData(
                table: "EmployeeContractTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3673));

            migrationBuilder.UpdateData(
                table: "EmployeeContractTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3675));

            migrationBuilder.UpdateData(
                table: "EmployeeContractTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2025, 3, 1, 14, 37, 1, 695, DateTimeKind.Local).AddTicks(3677));

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
        }
    }
}
