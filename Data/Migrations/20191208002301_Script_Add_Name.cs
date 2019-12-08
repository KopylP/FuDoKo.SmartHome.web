using Microsoft.EntityFrameworkCore.Migrations;

namespace FuDoKo.SmartHome.web.Data.Migrations
{
    public partial class Script_Add_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Scripts",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Scripts");
        }
    }
}
