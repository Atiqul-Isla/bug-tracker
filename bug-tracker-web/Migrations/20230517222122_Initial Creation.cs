using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bug_tracker_web.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ProjectType = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ProjectDescription = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    ProjectVersion = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    ProjectCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectCreatedBy = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectID);
                });

            migrationBuilder.CreateTable(
                name: "Bugs",
                columns: table => new
                {
                    BugId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    BugName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    BugStatus = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    BugSeverity = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    BugCreatedBy = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    BugCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BugDescription = table.Column<string>(type: "nvarchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bugs", x => x.BugId);
                    table.ForeignKey(
                        name: "FK_Bugs_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_ProjectID",
                table: "Bugs",
                column: "ProjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bugs");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
