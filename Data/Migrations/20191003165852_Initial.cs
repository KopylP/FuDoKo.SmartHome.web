using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FuDoKo.SmartHome.web.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Users",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Users",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ConditionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 2, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Controllers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    MAC = table.Column<string>(maxLength: 16, nullable: false),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    InstalledDate = table.Column<DateTime>(nullable: false),
                    LastConnection = table.Column<DateTime>(nullable: false),
                    PublicKey = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Controllers", x => x.Id);
                    table.UniqueConstraint("AK_Controllers_Id_MAC", x => new { x.Id, x.MAC });
                });

            migrationBuilder.CreateTable(
                name: "Measures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MeasureName = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SensorTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypeName = table.Column<string>(maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Pin = table.Column<int>(maxLength: 2, nullable: false),
                    MAC = table.Column<string>(maxLength: 16, nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: false),
                    ControllerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Controllers_ControllerId",
                        column: x => x.ControllerId,
                        principalTable: "Controllers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersHaveControllers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ControllerId = table.Column<int>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersHaveControllers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersHaveControllers_Controllers_ControllerId",
                        column: x => x.ControllerId,
                        principalTable: "Controllers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersHaveControllers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Pin = table.Column<int>(maxLength: 2, nullable: false),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    Value = table.Column<int>(maxLength: 3, nullable: false),
                    SensorTypeId = table.Column<int>(nullable: false),
                    ControllerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensors_Controllers_ControllerId",
                        column: x => x.ControllerId,
                        principalTable: "Controllers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sensors_SensorTypes_SensorTypeId",
                        column: x => x.SensorTypeId,
                        principalTable: "SensorTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<int>(maxLength: 3, nullable: false),
                    DeviceId = table.Column<int>(nullable: false),
                    MeasureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceConfigurations_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceConfigurations_Measures_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersHaveDevices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsersHaveControllerId = table.Column<int>(nullable: false),
                    DeviceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersHaveDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersHaveDevices_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersHaveDevices_UsersHaveControllers_UsersHaveControllerId",
                        column: x => x.UsersHaveControllerId,
                        principalTable: "UsersHaveControllers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scripts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ConditionValue = table.Column<float>(nullable: false),
                    ConditionTypeId = table.Column<int>(maxLength: 2, nullable: false),
                    LastModificationDate = table.Column<DateTime>(nullable: false),
                    SensorId = table.Column<int>(nullable: false),
                    TimeFrom = table.Column<DateTime>(nullable: false),
                    TimeTo = table.Column<DateTime>(nullable: false),
                    Delta = table.Column<float>(maxLength: 4, nullable: false),
                    RepeatTimes = table.Column<int>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    Priority = table.Column<int>(maxLength: 2, nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scripts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scripts_ConditionTypes_ConditionTypeId",
                        column: x => x.ConditionTypeId,
                        principalTable: "ConditionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Scripts_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ScriptId = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    DeviceConfigurationId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commands_DeviceConfigurations_DeviceConfigurationId",
                        column: x => x.DeviceConfigurationId,
                        principalTable: "DeviceConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commands_Scripts_ScriptId",
                        column: x => x.ScriptId,
                        principalTable: "Scripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commands_DeviceConfigurationId",
                table: "Commands",
                column: "DeviceConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Commands_ScriptId",
                table: "Commands",
                column: "ScriptId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionTypes_Type",
                table: "ConditionTypes",
                column: "Type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Controllers_MAC",
                table: "Controllers",
                column: "MAC",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceConfigurations_DeviceId",
                table: "DeviceConfigurations",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceConfigurations_MeasureId",
                table: "DeviceConfigurations",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ControllerId",
                table: "Devices",
                column: "ControllerId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_MAC",
                table: "Devices",
                column: "MAC",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Measures_MeasureName",
                table: "Measures",
                column: "MeasureName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scripts_ConditionTypeId",
                table: "Scripts",
                column: "ConditionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Scripts_SensorId",
                table: "Scripts",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_ControllerId",
                table: "Sensors",
                column: "ControllerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_SensorTypeId",
                table: "Sensors",
                column: "SensorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SensorTypes_TypeName",
                table: "SensorTypes",
                column: "TypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersHaveControllers_ControllerId",
                table: "UsersHaveControllers",
                column: "ControllerId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersHaveControllers_UserId",
                table: "UsersHaveControllers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersHaveDevices_DeviceId",
                table: "UsersHaveDevices",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersHaveDevices_UsersHaveControllerId",
                table: "UsersHaveDevices",
                column: "UsersHaveControllerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commands");

            migrationBuilder.DropTable(
                name: "UsersHaveDevices");

            migrationBuilder.DropTable(
                name: "DeviceConfigurations");

            migrationBuilder.DropTable(
                name: "Scripts");

            migrationBuilder.DropTable(
                name: "UsersHaveControllers");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Measures");

            migrationBuilder.DropTable(
                name: "ConditionTypes");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "Controllers");

            migrationBuilder.DropTable(
                name: "SensorTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Users");
        }
    }
}
