﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using flashcard_backend.DatabaseContext;

#nullable disable

namespace flashcard_backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250312093354_Adding Card and Deck table")]
    partial class AddingCardandDecktable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("flashcard_backend.Models.CardModel", b =>
                {
                    b.Property<string>("CardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasColumnName("card_id")
                        .HasDefaultValueSql("'C' || nextval('card_seq')");

                    b.Property<string>("BackContent")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("back_content");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("DeckId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("deck_id");

                    b.Property<string>("DeckModelDeckId")
                        .HasColumnType("text");

                    b.Property<string>("FrontContent")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("front_content");

                    b.Property<int>("Ordering")
                        .HasColumnType("integer")
                        .HasColumnName("order");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("CardId");

                    b.HasIndex("DeckModelDeckId");

                    b.ToTable("flashcards");
                });

            modelBuilder.Entity("flashcard_backend.Models.DeckModel", b =>
                {
                    b.Property<string>("DeckId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasColumnName("deck_id")
                        .HasDefaultValueSql("'D' || nextval('deck_seq')");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("DeckTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("deck_title");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean")
                        .HasColumnName("is_public");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("DeckId");

                    b.HasIndex("UserId");

                    b.ToTable("decks");
                });

            modelBuilder.Entity("flashcard_backend.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("password_hash");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("flashcard_backend.Models.CardModel", b =>
                {
                    b.HasOne("flashcard_backend.Models.DeckModel", null)
                        .WithMany("Cards")
                        .HasForeignKey("DeckModelDeckId");
                });

            modelBuilder.Entity("flashcard_backend.Models.DeckModel", b =>
                {
                    b.HasOne("flashcard_backend.Models.UserModel", "User")
                        .WithMany("Decks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("flashcard_backend.Models.DeckModel", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("flashcard_backend.Models.UserModel", b =>
                {
                    b.Navigation("Decks");
                });
#pragma warning restore 612, 618
        }
    }
}
