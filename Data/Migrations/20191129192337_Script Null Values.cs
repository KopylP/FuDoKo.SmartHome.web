using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FuDoKo.SmartHome.web.Data.Migrations
{
    public partial class ScriptNullValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeTo",
                table: "Scripts",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "SensorId",
                table: "Scripts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "Delta",
                table: "Scripts",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(float),
                oldMaxLength: 4);

            migrationBuilder.AlterColumn<int>(
                name: "ConditionTypeId",
                table: "Scripts",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeTo",
                table: "Scripts",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SensorId",
                table: "Scripts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Delta",
                table: "Scripts",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(float),
                oldMaxLength: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ConditionTypeId",
                table: "Scripts",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 2,
                oldNullable: true);
        }
    }
}
