using Microsoft.EntityFrameworkCore.Migrations;

namespace FuDoKo.SmartHome.web.Data.Migrations
{
    public partial class UpdateNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Commands_DeviceConfigurations_DeviceConfigurationId",
                table: "Commands");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceConfigurations_Devices_DeviceId",
                table: "DeviceConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceConfigurations_Measures_MeasureId",
                table: "DeviceConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_Scripts_ConditionTypes_ConditionTypeId",
                table: "Scripts");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_SensorTypes_SensorTypeId",
                table: "Sensors");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersHaveControllers_Controllers_ControllerId",
                table: "UsersHaveControllers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersHaveControllers_AspNetUsers_UserId",
                table: "UsersHaveControllers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersHaveDevices_Devices_DeviceId",
                table: "UsersHaveDevices");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersHaveDevices_UsersHaveControllers_UsersHaveControllerId",
                table: "UsersHaveDevices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersHaveDevices",
                table: "UsersHaveDevices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersHaveControllers",
                table: "UsersHaveControllers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SensorTypes",
                table: "SensorTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceConfigurations",
                table: "DeviceConfigurations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConditionTypes",
                table: "ConditionTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "UsersHaveDevices",
                newName: "Users_Have_Devices");

            migrationBuilder.RenameTable(
                name: "UsersHaveControllers",
                newName: "Users_Have_Controllers");

            migrationBuilder.RenameTable(
                name: "SensorTypes",
                newName: "Sensor_Types");

            migrationBuilder.RenameTable(
                name: "DeviceConfigurations",
                newName: "Device_Configurations");

            migrationBuilder.RenameTable(
                name: "ConditionTypes",
                newName: "Condition_Types");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_UsersHaveDevices_UsersHaveControllerId",
                table: "Users_Have_Devices",
                newName: "IX_Users_Have_Devices_UsersHaveControllerId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersHaveDevices_DeviceId",
                table: "Users_Have_Devices",
                newName: "IX_Users_Have_Devices_DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersHaveControllers_UserId",
                table: "Users_Have_Controllers",
                newName: "IX_Users_Have_Controllers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersHaveControllers_ControllerId",
                table: "Users_Have_Controllers",
                newName: "IX_Users_Have_Controllers_ControllerId");

            migrationBuilder.RenameIndex(
                name: "IX_SensorTypes_TypeName",
                table: "Sensor_Types",
                newName: "IX_Sensor_Types_TypeName");

            migrationBuilder.RenameIndex(
                name: "IX_DeviceConfigurations_MeasureId",
                table: "Device_Configurations",
                newName: "IX_Device_Configurations_MeasureId");

            migrationBuilder.RenameIndex(
                name: "IX_DeviceConfigurations_DeviceId",
                table: "Device_Configurations",
                newName: "IX_Device_Configurations_DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_ConditionTypes_Type",
                table: "Condition_Types",
                newName: "IX_Condition_Types_Type");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users_Have_Devices",
                table: "Users_Have_Devices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users_Have_Controllers",
                table: "Users_Have_Controllers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sensor_Types",
                table: "Sensor_Types",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Device_Configurations",
                table: "Device_Configurations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Condition_Types",
                table: "Condition_Types",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_Users_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_Users_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Users_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_Users_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commands_Device_Configurations_DeviceConfigurationId",
                table: "Commands",
                column: "DeviceConfigurationId",
                principalTable: "Device_Configurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Device_Configurations_Devices_DeviceId",
                table: "Device_Configurations",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Device_Configurations_Measures_MeasureId",
                table: "Device_Configurations",
                column: "MeasureId",
                principalTable: "Measures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scripts_Condition_Types_ConditionTypeId",
                table: "Scripts",
                column: "ConditionTypeId",
                principalTable: "Condition_Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Sensor_Types_SensorTypeId",
                table: "Sensors",
                column: "SensorTypeId",
                principalTable: "Sensor_Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Have_Controllers_Controllers_ControllerId",
                table: "Users_Have_Controllers",
                column: "ControllerId",
                principalTable: "Controllers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Have_Controllers_Users_UserId",
                table: "Users_Have_Controllers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Have_Devices_Devices_DeviceId",
                table: "Users_Have_Devices",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Have_Devices_Users_Have_Controllers_UsersHaveControlle~",
                table: "Users_Have_Devices",
                column: "UsersHaveControllerId",
                principalTable: "Users_Have_Controllers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_Users_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_Users_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_Users_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_Users_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Commands_Device_Configurations_DeviceConfigurationId",
                table: "Commands");

            migrationBuilder.DropForeignKey(
                name: "FK_Device_Configurations_Devices_DeviceId",
                table: "Device_Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_Device_Configurations_Measures_MeasureId",
                table: "Device_Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_Scripts_Condition_Types_ConditionTypeId",
                table: "Scripts");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Sensor_Types_SensorTypeId",
                table: "Sensors");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Have_Controllers_Controllers_ControllerId",
                table: "Users_Have_Controllers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Have_Controllers_Users_UserId",
                table: "Users_Have_Controllers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Have_Devices_Devices_DeviceId",
                table: "Users_Have_Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Have_Devices_Users_Have_Controllers_UsersHaveControlle~",
                table: "Users_Have_Devices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users_Have_Devices",
                table: "Users_Have_Devices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users_Have_Controllers",
                table: "Users_Have_Controllers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sensor_Types",
                table: "Sensor_Types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Device_Configurations",
                table: "Device_Configurations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Condition_Types",
                table: "Condition_Types");

            migrationBuilder.RenameTable(
                name: "Users_Have_Devices",
                newName: "UsersHaveDevices");

            migrationBuilder.RenameTable(
                name: "Users_Have_Controllers",
                newName: "UsersHaveControllers");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Sensor_Types",
                newName: "SensorTypes");

            migrationBuilder.RenameTable(
                name: "Device_Configurations",
                newName: "DeviceConfigurations");

            migrationBuilder.RenameTable(
                name: "Condition_Types",
                newName: "ConditionTypes");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Have_Devices_UsersHaveControllerId",
                table: "UsersHaveDevices",
                newName: "IX_UsersHaveDevices_UsersHaveControllerId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Have_Devices_DeviceId",
                table: "UsersHaveDevices",
                newName: "IX_UsersHaveDevices_DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Have_Controllers_UserId",
                table: "UsersHaveControllers",
                newName: "IX_UsersHaveControllers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Have_Controllers_ControllerId",
                table: "UsersHaveControllers",
                newName: "IX_UsersHaveControllers_ControllerId");

            migrationBuilder.RenameIndex(
                name: "IX_Sensor_Types_TypeName",
                table: "SensorTypes",
                newName: "IX_SensorTypes_TypeName");

            migrationBuilder.RenameIndex(
                name: "IX_Device_Configurations_MeasureId",
                table: "DeviceConfigurations",
                newName: "IX_DeviceConfigurations_MeasureId");

            migrationBuilder.RenameIndex(
                name: "IX_Device_Configurations_DeviceId",
                table: "DeviceConfigurations",
                newName: "IX_DeviceConfigurations_DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_Condition_Types_Type",
                table: "ConditionTypes",
                newName: "IX_ConditionTypes_Type");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersHaveDevices",
                table: "UsersHaveDevices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersHaveControllers",
                table: "UsersHaveControllers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SensorTypes",
                table: "SensorTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceConfigurations",
                table: "DeviceConfigurations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConditionTypes",
                table: "ConditionTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commands_DeviceConfigurations_DeviceConfigurationId",
                table: "Commands",
                column: "DeviceConfigurationId",
                principalTable: "DeviceConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceConfigurations_Devices_DeviceId",
                table: "DeviceConfigurations",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceConfigurations_Measures_MeasureId",
                table: "DeviceConfigurations",
                column: "MeasureId",
                principalTable: "Measures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scripts_ConditionTypes_ConditionTypeId",
                table: "Scripts",
                column: "ConditionTypeId",
                principalTable: "ConditionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_SensorTypes_SensorTypeId",
                table: "Sensors",
                column: "SensorTypeId",
                principalTable: "SensorTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersHaveControllers_Controllers_ControllerId",
                table: "UsersHaveControllers",
                column: "ControllerId",
                principalTable: "Controllers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersHaveControllers_AspNetUsers_UserId",
                table: "UsersHaveControllers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersHaveDevices_Devices_DeviceId",
                table: "UsersHaveDevices",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersHaveDevices_UsersHaveControllers_UsersHaveControllerId",
                table: "UsersHaveDevices",
                column: "UsersHaveControllerId",
                principalTable: "UsersHaveControllers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
