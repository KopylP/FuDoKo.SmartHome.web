using Microsoft.EntityFrameworkCore.Migrations;

namespace FuDoKo.SmartHome.web.Data.Migrations
{
    public partial class Firebase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "FirebaseToken",
                table: "Users",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirebaseToken",
                table: "Users");
        }
    }
}
