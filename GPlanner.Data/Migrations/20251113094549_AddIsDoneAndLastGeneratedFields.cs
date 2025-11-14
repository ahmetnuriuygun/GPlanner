using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPlanner.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDoneAndLastGeneratedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "ScheduledTasks",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastGenerated",
                table: "DaillyPlanItems",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "ScheduledTasks");

            migrationBuilder.DropColumn(
                name: "LastGenerated",
                table: "DaillyPlanItems");
        }
    }
}
