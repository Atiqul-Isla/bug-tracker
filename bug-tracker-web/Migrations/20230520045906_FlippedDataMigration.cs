using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bug_tracker_web.Migrations
{
    public partial class FlippedDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Bugs_BugID",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_BugID",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "BugID",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "ProjectID",
                table: "Bugs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_ProjectID",
                table: "Bugs",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Projects_ProjectID",
                table: "Bugs",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ProjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_Projects_ProjectID",
                table: "Bugs");

            migrationBuilder.DropIndex(
                name: "IX_Bugs_ProjectID",
                table: "Bugs");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "Bugs");

            migrationBuilder.AddColumn<int>(
                name: "BugID",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_BugID",
                table: "Projects",
                column: "BugID");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Bugs_BugID",
                table: "Projects",
                column: "BugID",
                principalTable: "Bugs",
                principalColumn: "BugId");
        }
    }
}
