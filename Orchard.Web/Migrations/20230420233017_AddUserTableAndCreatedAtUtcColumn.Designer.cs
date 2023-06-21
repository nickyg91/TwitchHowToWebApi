﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Orchard.Data.Contexts;

#nullable disable

namespace Orchard.Web.Migrations
{
    [DbContext(typeof(OrchardDbContext))]
    [Migration("20230420233017_AddUserTableAndCreatedAtUtcColumn")]
    partial class AddUserTableAndCreatedAtUtcColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("orchard")
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Orchard.Data.Entities.Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAtUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at_utc")
                        .HasDefaultValueSql("timezone('UTC', now())");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .IsUnicode(true)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_basket");

                    b.ToTable("basket", "orchard");
                });

            modelBuilder.Entity("Orchard.Data.Entities.BasketFruit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer")
                        .HasColumnName("amount");

                    b.Property<int>("BasketId")
                        .HasColumnType("integer")
                        .HasColumnName("basket_id");

                    b.Property<DateTime>("CreatedAtUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at_utc")
                        .HasDefaultValueSql("timezone('UTC', now())");

                    b.Property<int>("FruitId")
                        .HasColumnType("integer")
                        .HasColumnName("fruit_id");

                    b.HasKey("Id")
                        .HasName("pk_basket_fruit_id");

                    b.HasIndex("BasketId");

                    b.HasIndex("FruitId");

                    b.ToTable("basket_fruit", "orchard");
                });

            modelBuilder.Entity("Orchard.Data.Entities.Fruit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAtUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at_utc")
                        .HasDefaultValueSql("timezone('UTC', now())");

                    b.Property<byte>("FruitShape")
                        .HasColumnType("smallint")
                        .HasColumnName("fruit_shape");

                    b.Property<byte>("FruitType")
                        .HasColumnType("smallint")
                        .HasColumnName("fruit_type");

                    b.HasKey("Id")
                        .HasName("pk_fruit_id");

                    b.ToTable("fruit", "orchard");
                });

            modelBuilder.Entity("Orchard.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birth_date");

                    b.Property<DateTime>("CreatedAtUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at_utc")
                        .HasDefaultValueSql("timezone('UTC', now())");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("email");

                    b.Property<Guid>("EmailVerificationGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("email_verification_guid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("first_name");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("hashed_password");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("last_name");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.ToTable("user", "orchard");
                });

            modelBuilder.Entity("Orchard.Data.Entities.BasketFruit", b =>
                {
                    b.HasOne("Orchard.Data.Entities.Basket", "Basket")
                        .WithMany("Fruit")
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_basket_basket_fruit");

                    b.HasOne("Orchard.Data.Entities.Fruit", "Fruit")
                        .WithMany()
                        .HasForeignKey("FruitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Basket");

                    b.Navigation("Fruit");
                });

            modelBuilder.Entity("Orchard.Data.Entities.Basket", b =>
                {
                    b.Navigation("Fruit");
                });
#pragma warning restore 612, 618
        }
    }
}
