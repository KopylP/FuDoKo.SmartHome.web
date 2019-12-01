using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FuDoKo.SmartHome.web.Data.Migrations
{
    public partial class Commandupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Commands");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeSpan",
                table: "Commands",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSpan",
                table: "Commands");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Commands",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
