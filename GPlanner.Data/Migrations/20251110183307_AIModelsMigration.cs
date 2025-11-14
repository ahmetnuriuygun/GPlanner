using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPlanner.Data.Migrations
{
    /// <inheritdoc />
    public partial class AIModelsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTasks_DaillyPlanItems_DailyPlanDate",
                table: "ScheduledTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTasks_UserTasks_OriginalUserTaskId",
                table: "ScheduledTasks");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledTasks_OriginalUserTaskId",
                table: "ScheduledTasks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Users",
                type: "datetime(6)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DailyPlanItemDate",
                table: "ScheduledTasks",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "BirthDate",
                value: new DateTime(2007, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTasks_DailyPlanItemDate",
                table: "ScheduledTasks",
                column: "DailyPlanItemDate");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledTasks_DaillyPlanItems_DailyPlanDate",
                table: "ScheduledTasks",
                column: "DailyPlanDate",
                principalTable: "DaillyPlanItems",
                principalColumn: "Date");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledTasks_DaillyPlanItems_DailyPlanItemDate",
                table: "ScheduledTasks",
                column: "DailyPlanItemDate",
                principalTable: "DaillyPlanItems",
                principalColumn: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTasks_DaillyPlanItems_DailyPlanDate",
                table: "ScheduledTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTasks_DaillyPlanItems_DailyPlanItemDate",
                table: "ScheduledTasks");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledTasks_DailyPlanItemDate",
                table: "ScheduledTasks");

            migrationBuilder.DropColumn(
                name: "DailyPlanItemDate",
                table: "ScheduledTasks");

            migrationBuilder.AlterColumn<string>(
                name: "BirthDate",
                table: "Users",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldMaxLength: 20)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "BirthDate",
                value: "2004-08-15");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTasks_OriginalUserTaskId",
                table: "ScheduledTasks",
                column: "OriginalUserTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledTasks_DaillyPlanItems_DailyPlanDate",
                table: "ScheduledTasks",
                column: "DailyPlanDate",
                principalTable: "DaillyPlanItems",
                principalColumn: "Date",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledTasks_UserTasks_OriginalUserTaskId",
                table: "ScheduledTasks",
                column: "OriginalUserTaskId",
                principalTable: "UserTasks",
                principalColumn: "TaskId");
        }
    }
}
