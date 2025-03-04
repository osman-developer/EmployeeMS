using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ContractName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EmployeeContracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "ContractStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 3, 5, 0, 1, 20, 388, DateTimeKind.Local).AddTicks(229));

            migrationBuilder.UpdateData(
                table: "ContractStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2025, 3, 5, 0, 1, 20, 388, DateTimeKind.Local).AddTicks(233));

            migrationBuilder.UpdateData(
                table: "ContractStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2025, 3, 5, 0, 1, 20, 388, DateTimeKind.Local).AddTicks(235));

            migrationBuilder.UpdateData(
                table: "EmployeeContractTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 3, 5, 0, 1, 20, 388, DateTimeKind.Local).AddTicks(263));

            migrationBuilder.UpdateData(
                table: "EmployeeContractTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2025, 3, 5, 0, 1, 20, 388, DateTimeKind.Local).AddTicks(266));

            migrationBuilder.UpdateData(
                table: "EmployeeContractTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2025, 3, 5, 0, 1, 20, 388, DateTimeKind.Local).AddTicks(268));

            migrationBuilder.UpdateData(
                table: "EmployeeContractTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2025, 3, 5, 0, 1, 20, 388, DateTimeKind.Local).AddTicks(270));

            migrationBuilder.UpdateData(
                table: "EmployeeContractTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2025, 3, 5, 0, 1, 20, 388, DateTimeKind.Local).AddTicks(272));

            migrationBuilder.UpdateData(
                table: "EmployeeContractTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2025, 3, 5, 0, 1, 20, 388, DateTimeKind.Local).AddTicks(274));

            migrationBuilder.UpdateData(
                table: "EmployeeFileTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 3, 5, 0, 1, 20, 388, DateTimeKind.Local).AddTicks(5));

            migrationBuilder.UpdateData(
                table: "EmployeeFileTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2025, 3, 5, 0, 1, 20, 388, DateTimeKind.Local).AddTicks(56));

            migrationBuilder.UpdateData(
                table: "EmployeeFileTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2025, 3, 5, 0, 1, 20, 388, DateTimeKind.Local).AddTicks(58));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "EmployeeContracts");

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
    }
}
