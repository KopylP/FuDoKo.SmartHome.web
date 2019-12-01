using Microsoft.EntityFrameworkCore.Migrations;

namespace FuDoKo.SmartHome.web.Data.Migrations
{
    public partial class EndCommand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "End",
                table: "Commands",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End",
                table: "Commands");
        }
    }
}
