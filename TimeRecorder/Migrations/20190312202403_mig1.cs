using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeRecorder.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectDTOs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Argb = table.Column<uint>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDTOs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecordingDTOs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordingDTOs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordingDTOs_ProjectDTOs_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDTOs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagDTOs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TagValue = table.Column<string>(nullable: true),
                    RecordingDTOId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagDTOs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagDTOs_RecordingDTOs_RecordingDTOId",
                        column: x => x.RecordingDTOId,
                        principalTable: "RecordingDTOs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordingDTOs_ProjectId",
                table: "RecordingDTOs",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TagDTOs_RecordingDTOId",
                table: "TagDTOs",
                column: "RecordingDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_TagDTOs_TagValue",
                table: "TagDTOs",
                column: "TagValue",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagDTOs");

            migrationBuilder.DropTable(
                name: "RecordingDTOs");

            migrationBuilder.DropTable(
                name: "ProjectDTOs");
        }
    }
}
