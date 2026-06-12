using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class setnewentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partners",
                schema: "Content",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                });

            migrationBuilder.UpdateData(
                schema: "Content",
                table: "AboutUs",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "LastUpdated" },
                values: new object[] { new DateTime(2026, 6, 6, 15, 28, 44, 827, DateTimeKind.Utc).AddTicks(492), new DateTime(2026, 6, 6, 15, 28, 44, 827, DateTimeKind.Utc).AddTicks(1483) });

            migrationBuilder.UpdateData(
                schema: "Content",
                table: "ContactInfo",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 6, 15, 28, 44, 867, DateTimeKind.Utc).AddTicks(6846));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Partners",
                schema: "Content");

            migrationBuilder.UpdateData(
                schema: "Content",
                table: "AboutUs",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "LastUpdated" },
                values: new object[] { new DateTime(2026, 5, 13, 12, 6, 30, 265, DateTimeKind.Utc).AddTicks(3985), new DateTime(2026, 5, 13, 12, 6, 30, 265, DateTimeKind.Utc).AddTicks(5456) });

            migrationBuilder.UpdateData(
                schema: "Content",
                table: "ContactInfo",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 13, 12, 6, 30, 323, DateTimeKind.Utc).AddTicks(6854));
        }
    }
}
