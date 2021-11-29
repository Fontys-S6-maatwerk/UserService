using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace User_Service.Data.Migrations
{
    public partial class email : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "User",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("9c3b5697-e4ba-4c4a-b4c9-0dacd3b634e2"),
                columns: new[] { "CreatedAt", "LastUpdatedAt" },
                values: new object[] { new DateTime(2021, 11, 29, 14, 5, 47, 511, DateTimeKind.Utc).AddTicks(4210), new DateTime(2021, 11, 29, 14, 5, 47, 511, DateTimeKind.Utc).AddTicks(4210) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "User");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("9c3b5697-e4ba-4c4a-b4c9-0dacd3b634e2"),
                columns: new[] { "CreatedAt", "LastUpdatedAt" },
                values: new object[] { new DateTime(2021, 11, 2, 9, 1, 11, 392, DateTimeKind.Utc).AddTicks(4528), new DateTime(2021, 11, 2, 9, 1, 11, 392, DateTimeKind.Utc).AddTicks(4528) });
        }
    }
}
