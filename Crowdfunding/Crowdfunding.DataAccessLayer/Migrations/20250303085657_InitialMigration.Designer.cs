﻿// <auto-generated />
using System;
using Crowdfunding.DataAccessLayer.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Crowdfunding.DataAccessLayer.Migrations
{
    [DbContext(typeof(CrowfundingDbContext))]
    [Migration("20250303085657_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Crowdfunding.DataAccessLayer.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f2642ccb-5641-4b4e-ac0c-f9645796d3fe"),
                            Description = "Technology"
                        },
                        new
                        {
                            Id = new Guid("981e82aa-f32a-4609-b351-a98e0b50cf1c"),
                            Description = "Art"
                        },
                        new
                        {
                            Id = new Guid("53fdb313-1a6a-4f93-83df-1141c0ee8ddd"),
                            Description = "Science"
                        });
                });

            modelBuilder.Entity("Crowdfunding.DataAccessLayer.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c024f42e-8f86-45c9-977a-7c71e7b14fb5"),
                            Date = new DateTime(2025, 3, 3, 10, 56, 57, 684, DateTimeKind.Local).AddTicks(320),
                            ProjectId = new Guid("b388180d-8633-45d3-8da1-0aaaff1e9ab8"),
                            Text = "This is a great project!",
                            UserId = new Guid("5b4af467-6906-4dea-a32d-8f8b427665fc")
                        },
                        new
                        {
                            Id = new Guid("2640f541-162a-4301-bc0f-ede9acb97720"),
                            Date = new DateTime(2025, 3, 3, 10, 56, 57, 684, DateTimeKind.Local).AddTicks(323),
                            ProjectId = new Guid("7eb7eb00-dd2c-44aa-aace-58880c417a1a"),
                            Text = "I love this project!",
                            UserId = new Guid("a4bcc309-4841-4e47-ac31-95b8358ec32b")
                        },
                        new
                        {
                            Id = new Guid("761bee88-1c72-48e6-b07a-5f42300921dc"),
                            Date = new DateTime(2025, 3, 3, 10, 56, 57, 684, DateTimeKind.Local).AddTicks(326),
                            ProjectId = new Guid("3540cc97-4218-419b-a1fc-82a91cd8fd14"),
                            Text = "This project is amazing!",
                            UserId = new Guid("731a4586-7efd-4b45-b640-fea9d9d267a3")
                        });
                });

            modelBuilder.Entity("Crowdfunding.DataAccessLayer.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b388180d-8633-45d3-8da1-0aaaff1e9ab8"),
                            CategoryId = new Guid("53fdb313-1a6a-4f93-83df-1141c0ee8ddd"),
                            CreationDate = new DateTime(2025, 3, 3, 10, 56, 57, 684, DateTimeKind.Local).AddTicks(269),
                            CreatorId = new Guid("5b4af467-6906-4dea-a32d-8f8b427665fc"),
                            Description = "Research on artificial intelligence.",
                            Name = "AI Research"
                        },
                        new
                        {
                            Id = new Guid("7eb7eb00-dd2c-44aa-aace-58880c417a1a"),
                            CategoryId = new Guid("981e82aa-f32a-4609-b351-a98e0b50cf1c"),
                            CreationDate = new DateTime(2025, 3, 3, 10, 56, 57, 684, DateTimeKind.Local).AddTicks(313),
                            CreatorId = new Guid("a4bcc309-4841-4e47-ac31-95b8358ec32b"),
                            Description = "An exhibition of modern art.",
                            Name = "Modern Art Exhibition"
                        },
                        new
                        {
                            Id = new Guid("3540cc97-4218-419b-a1fc-82a91cd8fd14"),
                            CategoryId = new Guid("53fdb313-1a6a-4f93-83df-1141c0ee8ddd"),
                            CreationDate = new DateTime(2025, 3, 3, 10, 56, 57, 684, DateTimeKind.Local).AddTicks(316),
                            CreatorId = new Guid("731a4586-7efd-4b45-b640-fea9d9d267a3"),
                            Description = "Exploring the depths of space.",
                            Name = "Space Exploration"
                        });
                });

            modelBuilder.Entity("Crowdfunding.DataAccessLayer.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5b4af467-6906-4dea-a32d-8f8b427665fc"),
                            Name = "John",
                            SecondName = "Doe"
                        },
                        new
                        {
                            Id = new Guid("a4bcc309-4841-4e47-ac31-95b8358ec32b"),
                            Name = "Jane",
                            SecondName = "Doe"
                        },
                        new
                        {
                            Id = new Guid("731a4586-7efd-4b45-b640-fea9d9d267a3"),
                            Name = "Alice",
                            SecondName = "Smith"
                        });
                });

            modelBuilder.Entity("Crowdfunding.DataAccessLayer.Entities.Vote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Votes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ed75d929-6a05-4add-aaee-50a9a75e86e1"),
                            ProjectId = new Guid("b388180d-8633-45d3-8da1-0aaaff1e9ab8"),
                            UserId = new Guid("5b4af467-6906-4dea-a32d-8f8b427665fc")
                        },
                        new
                        {
                            Id = new Guid("f32b811d-f3a0-4ce3-b128-0b55f681fcf2"),
                            ProjectId = new Guid("7eb7eb00-dd2c-44aa-aace-58880c417a1a"),
                            UserId = new Guid("a4bcc309-4841-4e47-ac31-95b8358ec32b")
                        },
                        new
                        {
                            Id = new Guid("c48dfb7c-f3b2-45d8-a543-70f6c2616fd2"),
                            ProjectId = new Guid("3540cc97-4218-419b-a1fc-82a91cd8fd14"),
                            UserId = new Guid("731a4586-7efd-4b45-b640-fea9d9d267a3")
                        });
                });

            modelBuilder.Entity("Crowdfunding.DataAccessLayer.Entities.Comment", b =>
                {
                    b.HasOne("Crowdfunding.DataAccessLayer.Entities.Project", "Project")
                        .WithMany("Comments")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Crowdfunding.DataAccessLayer.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Crowdfunding.DataAccessLayer.Entities.Project", b =>
                {
                    b.HasOne("Crowdfunding.DataAccessLayer.Entities.Category", "Category")
                        .WithMany("Projects")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Crowdfunding.DataAccessLayer.Entities.User", "Creator")
                        .WithMany("Projects")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Crowdfunding.DataAccessLayer.Entities.Vote", b =>
                {
                    b.HasOne("Crowdfunding.DataAccessLayer.Entities.Project", "Project")
                        .WithMany("Votes")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Crowdfunding.DataAccessLayer.Entities.User", "User")
                        .WithMany("Votes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Crowdfunding.DataAccessLayer.Entities.Category", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("Crowdfunding.DataAccessLayer.Entities.Project", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("Crowdfunding.DataAccessLayer.Entities.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Projects");

                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}
