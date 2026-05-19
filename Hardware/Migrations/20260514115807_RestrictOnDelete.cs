using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hardware.Migrations
{
    /// <inheritdoc />
    public partial class RestrictOnDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Complects_ComplectId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceNames_DeviceNameId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceProviders_DeviceProviderId",
                table: "Devices");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Complects_ComplectId",
                table: "Devices",
                column: "ComplectId",
                principalTable: "Complects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceNames_DeviceNameId",
                table: "Devices",
                column: "DeviceNameId",
                principalTable: "DeviceNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceProviders_DeviceProviderId",
                table: "Devices",
                column: "DeviceProviderId",
                principalTable: "DeviceProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Complects_ComplectId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceNames_DeviceNameId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceProviders_DeviceProviderId",
                table: "Devices");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Complects_ComplectId",
                table: "Devices",
                column: "ComplectId",
                principalTable: "Complects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceNames_DeviceNameId",
                table: "Devices",
                column: "DeviceNameId",
                principalTable: "DeviceNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceProviders_DeviceProviderId",
                table: "Devices",
                column: "DeviceProviderId",
                principalTable: "DeviceProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
