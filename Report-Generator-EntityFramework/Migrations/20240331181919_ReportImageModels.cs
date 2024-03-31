using Microsoft.EntityFrameworkCore.Migrations;

namespace Report_Generator_EntityFramework.Migrations
{
    public partial class ReportImageModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportImageModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ReportModelId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportImageModels");
        }
    }
}
