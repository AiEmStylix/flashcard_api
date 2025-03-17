using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace flashcard_backend.Migrations
{
    /// <inheritdoc />
    public partial class Changerelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_flashcards_decks_DeckModelDeckId",
                table: "flashcards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_flashcards",
                table: "flashcards");

            migrationBuilder.DropIndex(
                name: "IX_flashcards_DeckModelDeckId",
                table: "flashcards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_decks",
                table: "decks");

            migrationBuilder.DropColumn(
                name: "card_id",
                table: "flashcards");

            migrationBuilder.DropColumn(
                name: "back_content",
                table: "flashcards");

            migrationBuilder.DropColumn(
                name: "front_content",
                table: "flashcards");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "flashcards");

            migrationBuilder.DropColumn(
                name: "deck_id",
                table: "decks");

            migrationBuilder.DropColumn(
                name: "category_id",
                table: "decks");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "decks");

            migrationBuilder.RenameColumn(
                name: "DeckModelDeckId",
                table: "flashcards",
                newName: "image_url");

            migrationBuilder.RenameColumn(
                name: "order",
                table: "flashcards",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "deck_title",
                table: "decks",
                newName: "name");

            migrationBuilder.AlterColumn<string>(
                name: "password_hash",
                table: "users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<int>(
                name: "deck_id",
                table: "flashcards",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "flashcards",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "flashcards",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "back",
                table: "flashcards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "front",
                table: "flashcards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "decks",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "decks",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "decks",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_flashcards",
                table: "flashcards",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_decks",
                table: "decks",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_flashcards_deck_id",
                table: "flashcards",
                column: "deck_id");

            migrationBuilder.AddForeignKey(
                name: "FK_flashcards_decks_deck_id",
                table: "flashcards",
                column: "deck_id",
                principalTable: "decks",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_flashcards_decks_deck_id",
                table: "flashcards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_flashcards",
                table: "flashcards");

            migrationBuilder.DropIndex(
                name: "IX_flashcards_deck_id",
                table: "flashcards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_decks",
                table: "decks");

            migrationBuilder.DropColumn(
                name: "back",
                table: "flashcards");

            migrationBuilder.DropColumn(
                name: "front",
                table: "flashcards");

            migrationBuilder.DropColumn(
                name: "id",
                table: "decks");

            migrationBuilder.RenameColumn(
                name: "image_url",
                table: "flashcards",
                newName: "DeckModelDeckId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "flashcards",
                newName: "order");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "decks",
                newName: "deck_title");

            migrationBuilder.AlterColumn<string>(
                name: "password_hash",
                table: "users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "deck_id",
                table: "flashcards",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "flashcards",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "order",
                table: "flashcards",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "card_id",
                table: "flashcards",
                type: "text",
                nullable: false,
                defaultValueSql: "'C' || nextval('card_seq')");

            migrationBuilder.AddColumn<string>(
                name: "back_content",
                table: "flashcards",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "front_content",
                table: "flashcards",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "flashcards",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "decks",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "decks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "deck_id",
                table: "decks",
                type: "text",
                nullable: false,
                defaultValueSql: "'D' || nextval('deck_seq')");

            migrationBuilder.AddColumn<int>(
                name: "category_id",
                table: "decks",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "decks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_flashcards",
                table: "flashcards",
                column: "card_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_decks",
                table: "decks",
                column: "deck_id");

            migrationBuilder.CreateIndex(
                name: "IX_flashcards_DeckModelDeckId",
                table: "flashcards",
                column: "DeckModelDeckId");

            migrationBuilder.AddForeignKey(
                name: "FK_flashcards_decks_DeckModelDeckId",
                table: "flashcards",
                column: "DeckModelDeckId",
                principalTable: "decks",
                principalColumn: "deck_id");
        }
    }
}
