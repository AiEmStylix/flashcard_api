using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flashcard_backend.Migrations
{
    /// <inheritdoc />
    public partial class addingadminuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "full_name", "last_login", "password_hash", "role", "username" },
                values: new object[] { 1, new DateTime(2025, 4, 22, 5, 21, 51, 780, DateTimeKind.Utc).AddTicks(8581), "admin@test123.com", "Admin", null, "$2a$13$Yeaw/4AugEcdNFDPRgslmukitVawMJbBq0EvR6W3hfHjlNuF/Cgme", 1, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
