using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bug_tracker_web.Migrations
{
    public partial class SetComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Bugs_BugId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_BugId",
                table: "Comments",
                newName: "IX_Comments_BugId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Bugs_BugId",
                table: "Comments",
                column: "BugId",
                principalTable: "Bugs",
                principalColumn: "BugId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Bugs_BugId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_BugId",
                table: "Comment",
                newName: "IX_Comment_BugId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Bugs_BugId",
                table: "Comment",
                column: "BugId",
                principalTable: "Bugs",
                principalColumn: "BugId");
        }
    }
}
