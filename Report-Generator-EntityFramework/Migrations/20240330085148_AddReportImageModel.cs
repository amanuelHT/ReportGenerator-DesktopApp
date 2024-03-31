using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Report_Generator_EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddReportImageModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportImageModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportModelId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportImageModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportImageModels_ReportModels_ReportModelId",
                        column: x => x.ReportModelId,
                        principalTable: "ReportModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportImageModels_ReportModelId",
                table: "ReportImageModels",
                column: "ReportModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportImageModels");
        }
    }
}
