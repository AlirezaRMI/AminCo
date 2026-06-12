using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class cahngedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Content",
                table: "AboutUs",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "Content",
                table: "ContactInfo",
                keyColumn: "Id",
                keyValue: 1L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Content",
                table: "AboutUs",
                columns: new[] { "Id", "Content", "CreatedAt", "ImageUrl", "IsActive", "LastUpdated", "Title", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1L, "متن پیش‌فرض درباره ما - بعداً ویرایش شود", new DateTime(2026, 6, 6, 16, 9, 11, 462, DateTimeKind.Utc).AddTicks(3362), null, false, new DateTime(2026, 6, 6, 16, 9, 11, 462, DateTimeKind.Utc).AddTicks(4915), "درباره ما", null, 0L });

            migrationBuilder.InsertData(
                schema: "Content",
                table: "ContactInfo",
                columns: new[] { "Id", "Address", "CreatedAt", "Email", "GoogleMapUrl", "IsActive", "Phone", "UpdatedAt", "UpdatedBy", "WorkingHours" },
                values: new object[] { 1L, "تهران، خیابان ولیعصر", new DateTime(2026, 6, 6, 16, 9, 11, 516, DateTimeKind.Utc).AddTicks(3805), "info@aminco.com", null, false, "021-12345678", null, 0L, null });
        }
    }
}
