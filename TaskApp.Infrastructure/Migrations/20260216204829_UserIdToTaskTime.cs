using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserIdToTaskTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TaskTimes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskTimes_UserId",
                table: "TaskTimes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTimes_AspNetUsers_UserId",
                table: "TaskTimes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskTimes_AspNetUsers_UserId",
                table: "TaskTimes");

            migrationBuilder.DropIndex(
                name: "IX_TaskTimes_UserId",
                table: "TaskTimes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TaskTimes");
        }
    }
}
