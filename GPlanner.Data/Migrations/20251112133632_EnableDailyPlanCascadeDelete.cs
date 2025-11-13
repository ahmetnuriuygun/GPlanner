using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPlanner.Data.Migrations
{
    /// <inheritdoc />
    public partial class EnableDailyPlanCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTasks_DaillyPlanItems_DailyPlanDate",
                table: "ScheduledTasks");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledTasks_DaillyPlanItems_DailyPlanDate",
                table: "ScheduledTasks",
                column: "DailyPlanDate",
                principalTable: "DaillyPlanItems",
                principalColumn: "Date",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTasks_DaillyPlanItems_DailyPlanDate",
                table: "ScheduledTasks");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledTasks_DaillyPlanItems_DailyPlanDate",
                table: "ScheduledTasks",
                column: "DailyPlanDate",
                principalTable: "DaillyPlanItems",
                principalColumn: "Date");
        }
    }
}
