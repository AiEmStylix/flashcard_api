using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flashcard_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddingFlashcardanddecktable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image_url",
                table: "flashcards");

            migrationBuilder.DropColumn(
                name: "is_public",
                table: "decks");

            migrationBuilder.RenameColumn(
                name: "front",
                table: "flashcards",
                newName: "question");

            migrationBuilder.RenameColumn(
                name: "back",
                table: "flashcards",
                newName: "answer");

            migrationBuilder.AddColumn<DateTime>(
                name: "last_reviewed",
                table: "flashcards",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "decks",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "decks",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_reviewed",
                table: "flashcards");

            migrationBuilder.RenameColumn(
                name: "question",
                table: "flashcards",
                newName: "front");

            migrationBuilder.RenameColumn(
                name: "answer",
                table: "flashcards",
                newName: "back");

            migrationBuilder.AddColumn<string>(
                name: "image_url",
                table: "flashcards",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "decks",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "decks",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "is_public",
                table: "decks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
