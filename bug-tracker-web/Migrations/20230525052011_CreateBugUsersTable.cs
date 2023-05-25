using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bug_tracker_web.Migrations
{
    public partial class CreateBugUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BugUser_AspNetUsers_UserId",
                table: "BugUser");

            migrationBuilder.DropForeignKey(
                name: "FK_BugUser_Bugs_BugId",
                table: "BugUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BugUser",
                table: "BugUser");

            migrationBuilder.RenameTable(
                name: "BugUser",
                newName: "BugUsers");

            migrationBuilder.RenameIndex(
                name: "IX_BugUser_UserId",
                table: "BugUsers",
                newName: "IX_BugUsers_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BugUsers",
                table: "BugUsers",
                columns: new[] { "BugId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BugUsers_AspNetUsers_UserId",
                table: "BugUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BugUsers_Bugs_BugId",
                table: "BugUsers",
                column: "BugId",
                principalTable: "Bugs",
                principalColumn: "BugId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BugUsers_AspNetUsers_UserId",
                table: "BugUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BugUsers_Bugs_BugId",
                table: "BugUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BugUsers",
                table: "BugUsers");

            migrationBuilder.RenameTable(
                name: "BugUsers",
                newName: "BugUser");

            migrationBuilder.RenameIndex(
                name: "IX_BugUsers_UserId",
                table: "BugUser",
                newName: "IX_BugUser_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BugUser",
                table: "BugUser",
                columns: new[] { "BugId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BugUser_AspNetUsers_UserId",
                table: "BugUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BugUser_Bugs_BugId",
                table: "BugUser",
                column: "BugId",
                principalTable: "Bugs",
                principalColumn: "BugId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
