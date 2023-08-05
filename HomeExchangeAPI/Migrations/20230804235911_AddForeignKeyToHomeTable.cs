using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeExchangeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToHomeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HomeID",
                table: "HomeNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Homes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 4, 16, 59, 11, 297, DateTimeKind.Local).AddTicks(8680));

            migrationBuilder.UpdateData(
                table: "Homes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 4, 16, 59, 11, 297, DateTimeKind.Local).AddTicks(8720));

            migrationBuilder.UpdateData(
                table: "Homes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 4, 16, 59, 11, 297, DateTimeKind.Local).AddTicks(8720));

            migrationBuilder.UpdateData(
                table: "Homes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 4, 16, 59, 11, 297, DateTimeKind.Local).AddTicks(8730));

            migrationBuilder.UpdateData(
                table: "Homes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 4, 16, 59, 11, 297, DateTimeKind.Local).AddTicks(8730));

            migrationBuilder.CreateIndex(
                name: "IX_HomeNumbers_HomeID",
                table: "HomeNumbers",
                column: "HomeID");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeNumbers_Homes_HomeID",
                table: "HomeNumbers",
                column: "HomeID",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeNumbers_Homes_HomeID",
                table: "HomeNumbers");

            migrationBuilder.DropIndex(
                name: "IX_HomeNumbers_HomeID",
                table: "HomeNumbers");

            migrationBuilder.DropColumn(
                name: "HomeID",
                table: "HomeNumbers");

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
    }
}
