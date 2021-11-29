using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace User_Service.Data.Migrations
{
    public partial class fieldnames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("9c3b5697-e4ba-4c4a-b4c9-0dacd3b634e2"));

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "User",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "User",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "User",
                newName: "Name");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Email", "LastUpdatedAt", "Name", "Surname" },
                values: new object[] { new Guid("9c3b5697-e4ba-4c4a-b4c9-0dacd3b634e2"), new DateTime(2021, 11, 29, 14, 5, 47, 511, DateTimeKind.Utc).AddTicks(4210), null, new DateTime(2021, 11, 29, 14, 5, 47, 511, DateTimeKind.Utc).AddTicks(4210), "harry", "janssen" });
        }
    }
}
