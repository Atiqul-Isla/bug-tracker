using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bug_tracker_web.Migrations
{
    public partial class CommentFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Bugs_BugId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "BugId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Bugs_BugId",
                table: "Comments",
                column: "BugId",
                principalTable: "Bugs",
                principalColumn: "BugId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Bugs_BugId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "BugId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Bugs_BugId",
                table: "Comments",
                column: "BugId",
                principalTable: "Bugs",
                principalColumn: "BugId");
        }
    }
}
