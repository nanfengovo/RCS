using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RCS.Migrations
{
    /// <inheritdoc />
    public partial class Added_wmsLocation_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WmsLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationCode = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    LocationType = table.Column<int>(type: "int", nullable: false),
                    ZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: false),
                    Column = table.Column<int>(type: "int", nullable: false),
                    Layer = table.Column<int>(type: "int", nullable: false),
                    LocationStatus = table.Column<int>(type: "int", nullable: false),
                    PhysicalSensorStatus = table.Column<int>(type: "int", nullable: true),
                    AssociatedDeviceCode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CarrierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LockingTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WmsLocations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WmsLocations_LocationCode",
                table: "WmsLocations",
                column: "LocationCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WmsLocations");
        }
    }
}
