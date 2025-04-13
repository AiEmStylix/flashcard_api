using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flashcard_backend.Migrations
{
    /// <inheritdoc />
    public partial class removesomefieldindatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
