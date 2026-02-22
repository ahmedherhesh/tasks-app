using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserForProjectAndTaskItemAndTaskTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ApplicationUserId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskTimes_AspNetUsers_UserId",
                table: "TaskTimes");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TaskTimes",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_TaskTimes_UserId",
                table: "TaskTimes",
                newName: "IX_TaskTimes_CreatedById");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Projects",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ApplicationUserId",
                table: "Projects",
                newName: "IX_Projects_CreatedById");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreatedById",
                table: "Tasks",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_CreatedById",
                table: "Projects",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_CreatedById",
                table: "Tasks",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTimes_AspNetUsers_CreatedById",
                table: "TaskTimes",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_CreatedById",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_CreatedById",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskTimes_AspNetUsers_CreatedById",
                table: "TaskTimes");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CreatedById",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "TaskTimes",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskTimes_CreatedById",
                table: "TaskTimes",
                newName: "IX_TaskTimes_UserId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Projects",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_CreatedById",
                table: "Projects",
                newName: "IX_Projects_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ApplicationUserId",
                table: "Projects",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTimes_AspNetUsers_UserId",
                table: "TaskTimes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
