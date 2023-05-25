using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bug_tracker_web.Migrations
{
    public partial class AddBugSeverityAndStatusColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BugStatus",
                table: "Bugs",
                newName: "BugStatusValue");

            migrationBuilder.RenameColumn(
                name: "BugSeverity",
                table: "Bugs",
                newName: "BugSeverityValue");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BugStatusValue",
                table: "Bugs",
                newName: "BugStatus");

            migrationBuilder.RenameColumn(
                name: "BugSeverityValue",
                table: "Bugs",
                newName: "BugSeverity");
        }
    }
}
