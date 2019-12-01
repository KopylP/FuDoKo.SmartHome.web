using Microsoft.EntityFrameworkCore.Migrations;

namespace FuDoKo.SmartHome.web.Data.Migrations
{
    public partial class ScriptaddComplited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RepeatTimes",
                table: "Scripts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "Complited",
                table: "Scripts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complited",
                table: "Scripts");

            migrationBuilder.AlterColumn<int>(
                name: "RepeatTimes",
                table: "Scripts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
