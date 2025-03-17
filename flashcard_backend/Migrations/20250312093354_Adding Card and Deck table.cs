using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flashcard_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddingCardandDecktable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "decks",
                columns: table => new
                {
                    deck_id = table.Column<string>(type: "text", nullable: false, defaultValueSql: "'D' || nextval('deck_seq')"),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    deck_title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_public = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_decks", x => x.deck_id);
                    table.ForeignKey(
                        name: "FK_decks_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "flashcards",
                columns: table => new
                {
                    card_id = table.Column<string>(type: "text", nullable: false, defaultValueSql: "'C' || nextval('card_seq')"),
                    deck_id = table.Column<string>(type: "text", nullable: false),
                    front_content = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    back_content = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false),
                    DeckModelDeckId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flashcards", x => x.card_id);
                    table.ForeignKey(
                        name: "FK_flashcards_decks_DeckModelDeckId",
                        column: x => x.DeckModelDeckId,
                        principalTable: "decks",
                        principalColumn: "deck_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_decks_user_id",
                table: "decks",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_flashcards_DeckModelDeckId",
                table: "flashcards",
                column: "DeckModelDeckId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "flashcards");

            migrationBuilder.DropTable(
                name: "decks");
        }
    }
}
