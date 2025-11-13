using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GPlanner.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DaillyPlanItems",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DayOfWeek = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaillyPlanItems", x => x.Date);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SchoolName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsNotified = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
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
                    table.ForeignKey(
                        name: "FK_UserTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ScheduledTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ActivityType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OriginalUserTaskId = table.Column<int>(type: "int", nullable: true),
                    DailyPlanDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledTasks_DaillyPlanItems_DailyPlanDate",
                        column: x => x.DailyPlanDate,
                        principalTable: "DaillyPlanItems",
                        principalColumn: "Date",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduledTasks_UserTasks_OriginalUserTaskId",
                        column: x => x.OriginalUserTaskId,
                        principalTable: "UserTasks",
                        principalColumn: "TaskId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "IsNotified", "Name", "SchoolName", "Username" },
                values: new object[] { 1, "2004-08-15", true, "Victor De Marez", "Sint- - Franciscuscollege", "victordemarez" });

            migrationBuilder.InsertData(
                table: "UserTasks",
                columns: new[] { "TaskId", "Date", "Description", "Priority", "Title", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prepare for the upcoming math exam on algebra and geometry.", 5, "Math Exam", 0, 1 },
                    { 2, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Annual health check-up appointment at the clinic.", 4, "Appointment with the doctor", 3, 1 },
                    { 3, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Submit the final report and presentation for the science project.", 3, "Science Project Submission", 4, 1 },
                    { 4, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "English spelling and grammar test.", 2, "English Toets", 1, 1 },
                    { 5, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Complete the history assignment on World War II.", 1, "History Assignment", 4, 1 },
                    { 6, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Routine dental check-up and cleaning.", 4, "Dentist Appointment", 3, 1 },
                    { 7, new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Write and submit the lab report for the recent chemistry experiment.", 3, "Chemistry Lab Report", 4, 1 },
                    { 8, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prepare for the physics test on motion and forces.", 2, "Physics Toets", 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTasks_DailyPlanDate",
                table: "ScheduledTasks",
                column: "DailyPlanDate");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTasks_OriginalUserTaskId",
                table: "ScheduledTasks",
                column: "OriginalUserTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTasks_UserId",
                table: "UserTasks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledTasks");

            migrationBuilder.DropTable(
                name: "DaillyPlanItems");

            migrationBuilder.DropTable(
                name: "UserTasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
