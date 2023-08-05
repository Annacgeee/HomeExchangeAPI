using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeExchangeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddHomeNumberToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomeNumbers",
                columns: table => new
                {
                    HomeNo = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeNumbers", x => x.HomeNo);
                });

            migrationBuilder.UpdateData(
                table: "Homes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 3, 22, 52, 10, 769, DateTimeKind.Local).AddTicks(2260));

            migrationBuilder.UpdateData(
                table: "Homes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 3, 22, 52, 10, 769, DateTimeKind.Local).AddTicks(2340));

            migrationBuilder.UpdateData(
                table: "Homes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 3, 22, 52, 10, 769, DateTimeKind.Local).AddTicks(2340));

            migrationBuilder.UpdateData(
                table: "Homes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 3, 22, 52, 10, 769, DateTimeKind.Local).AddTicks(2350));

            migrationBuilder.UpdateData(
                table: "Homes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 3, 22, 52, 10, 769, DateTimeKind.Local).AddTicks(2350));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeNumbers");

            migrationBuilder.UpdateData(
                table: "Homes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 30, 16, 9, 13, 133, DateTimeKind.Local).AddTicks(7570));

            migrationBuilder.UpdateData(
                table: "Homes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 30, 16, 9, 13, 133, DateTimeKind.Local).AddTicks(7660));

            migrationBuilder.UpdateData(
                table: "Homes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 30, 16, 9, 13, 133, DateTimeKind.Local).AddTicks(7660));

            migrationBuilder.UpdateData(
                table: "Homes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 30, 16, 9, 13, 133, DateTimeKind.Local).AddTicks(7670));

            migrationBuilder.UpdateData(
                table: "Homes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 30, 16, 9, 13, 133, DateTimeKind.Local).AddTicks(7680));
        }
    }
}
