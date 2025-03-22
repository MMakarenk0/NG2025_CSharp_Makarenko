using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Crowdfunding.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { new Guid("53fdb313-1a6a-4f93-83df-1141c0ee8ddd"), "Science" },
                    { new Guid("981e82aa-f32a-4609-b351-a98e0b50cf1c"), "Art" },
                    { new Guid("f2642ccb-5641-4b4e-ac0c-f9645796d3fe"), "Technology" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "SecondName" },
                values: new object[,]
                {
                    { new Guid("5b4af467-6906-4dea-a32d-8f8b427665fc"), "John", "Doe" },
                    { new Guid("731a4586-7efd-4b45-b640-fea9d9d267a3"), "Alice", "Smith" },
                    { new Guid("a4bcc309-4841-4e47-ac31-95b8358ec32b"), "Jane", "Doe" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CategoryId", "CreationDate", "CreatorId", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("3540cc97-4218-419b-a1fc-82a91cd8fd14"), new Guid("53fdb313-1a6a-4f93-83df-1141c0ee8ddd"), new DateTime(2025, 3, 3, 10, 56, 57, 684, DateTimeKind.Local).AddTicks(316), new Guid("731a4586-7efd-4b45-b640-fea9d9d267a3"), "Exploring the depths of space.", "Space Exploration" },
                    { new Guid("7eb7eb00-dd2c-44aa-aace-58880c417a1a"), new Guid("981e82aa-f32a-4609-b351-a98e0b50cf1c"), new DateTime(2025, 3, 3, 10, 56, 57, 684, DateTimeKind.Local).AddTicks(313), new Guid("a4bcc309-4841-4e47-ac31-95b8358ec32b"), "An exhibition of modern art.", "Modern Art Exhibition" },
                    { new Guid("b388180d-8633-45d3-8da1-0aaaff1e9ab8"), new Guid("53fdb313-1a6a-4f93-83df-1141c0ee8ddd"), new DateTime(2025, 3, 3, 10, 56, 57, 684, DateTimeKind.Local).AddTicks(269), new Guid("5b4af467-6906-4dea-a32d-8f8b427665fc"), "Research on artificial intelligence.", "AI Research" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Date", "ProjectId", "Text", "UserId" },
                values: new object[,]
                {
                    { new Guid("2640f541-162a-4301-bc0f-ede9acb97720"), new DateTime(2025, 3, 3, 10, 56, 57, 684, DateTimeKind.Local).AddTicks(323), new Guid("7eb7eb00-dd2c-44aa-aace-58880c417a1a"), "I love this project!", new Guid("a4bcc309-4841-4e47-ac31-95b8358ec32b") },
                    { new Guid("761bee88-1c72-48e6-b07a-5f42300921dc"), new DateTime(2025, 3, 3, 10, 56, 57, 684, DateTimeKind.Local).AddTicks(326), new Guid("3540cc97-4218-419b-a1fc-82a91cd8fd14"), "This project is amazing!", new Guid("731a4586-7efd-4b45-b640-fea9d9d267a3") },
                    { new Guid("c024f42e-8f86-45c9-977a-7c71e7b14fb5"), new DateTime(2025, 3, 3, 10, 56, 57, 684, DateTimeKind.Local).AddTicks(320), new Guid("b388180d-8633-45d3-8da1-0aaaff1e9ab8"), "This is a great project!", new Guid("5b4af467-6906-4dea-a32d-8f8b427665fc") }
                });

            migrationBuilder.InsertData(
                table: "Votes",
                columns: new[] { "Id", "ProjectId", "UserId" },
                values: new object[,]
                {
                    { new Guid("c48dfb7c-f3b2-45d8-a543-70f6c2616fd2"), new Guid("3540cc97-4218-419b-a1fc-82a91cd8fd14"), new Guid("731a4586-7efd-4b45-b640-fea9d9d267a3") },
                    { new Guid("ed75d929-6a05-4add-aaee-50a9a75e86e1"), new Guid("b388180d-8633-45d3-8da1-0aaaff1e9ab8"), new Guid("5b4af467-6906-4dea-a32d-8f8b427665fc") },
                    { new Guid("f32b811d-f3a0-4ce3-b128-0b55f681fcf2"), new Guid("7eb7eb00-dd2c-44aa-aace-58880c417a1a"), new Guid("a4bcc309-4841-4e47-ac31-95b8358ec32b") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProjectId",
                table: "Comments",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CategoryId",
                table: "Projects",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatorId",
                table: "Projects",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_ProjectId",
                table: "Votes",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserId",
                table: "Votes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
