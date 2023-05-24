using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bug_tracker_web.Migrations
{
    public partial class CommentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BugCreatedBy",
                table: "Bugs");

            migrationBuilder.AlterColumn<int>(
                name: "BugSeverity",
                table: "Bugs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BugId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Bugs_BugId",
                        column: x => x.BugId,
                        principalTable: "Bugs",
                        principalColumn: "BugId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_BugId",
                table: "Comment",
                column: "BugId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.AlterColumn<string>(
                name: "BugSeverity",
                table: "Bugs",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "BugCreatedBy",
                table: "Bugs",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");
        }
    }
}
