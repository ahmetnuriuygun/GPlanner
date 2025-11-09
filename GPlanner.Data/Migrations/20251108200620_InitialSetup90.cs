using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GPlanner.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetup90 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserTasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTasks", x => x.TaskId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "UserTasks",
                columns: new[] { "TaskId", "Date", "Description", "Priority", "Title", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Create slides for MAUI and ASP.NET API demonstration.", 5, "Prepare Project Defense Slides", 3, 1 },
                    { 2, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Replace mock data with actual HTTP GET requests to the API.", 4, "Integrate API calls into ViewModels", 0, 1 },
                    { 3, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Check prices for holiday travel.", 3, "Book flight tickets", 4, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTasks");
        }
    }
}
