using Microsoft.EntityFrameworkCore.Migrations;

namespace FuDoKo.SmartHome.web.Data.Migrations
{
    public partial class SensorFloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Value",
                table: "Sensors",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "Sensors",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(float),
                oldMaxLength: 3);
        }
    }
}
