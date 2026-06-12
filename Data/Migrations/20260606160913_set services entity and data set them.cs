using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class setservicesentityanddatasetthem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Services",
                schema: "Content",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.UpdateData(
                schema: "Content",
                table: "AboutUs",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "LastUpdated" },
                values: new object[] { new DateTime(2026, 6, 6, 16, 9, 11, 462, DateTimeKind.Utc).AddTicks(3362), new DateTime(2026, 6, 6, 16, 9, 11, 462, DateTimeKind.Utc).AddTicks(4915) });

            migrationBuilder.UpdateData(
                schema: "Content",
                table: "ContactInfo",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 6, 16, 9, 11, 516, DateTimeKind.Utc).AddTicks(3805));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Services",
                schema: "Content");

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
    }
}
