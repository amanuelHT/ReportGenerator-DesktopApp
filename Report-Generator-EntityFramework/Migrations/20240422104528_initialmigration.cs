using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Report_Generator_EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tittle = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<bool>(type: "INTEGER", nullable: false),
                    Kunde = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "concreteDensityModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Dato = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MasseILuft = table.Column<double>(type: "REAL", nullable: false),
                    MasseIVannbad = table.Column<double>(type: "REAL", nullable: false),
                    Pw = table.Column<double>(type: "REAL", nullable: false),
                    V = table.Column<double>(type: "REAL", nullable: false),
                    Densitet = table.Column<double>(type: "REAL", nullable: false),
                    ReportModelId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_concreteDensityModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_concreteDensityModels_ReportModels_ReportModelId",
                        column: x => x.ReportModelId,
                        principalTable: "ReportModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataEtterKuttingOgSlipingModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IvannbadDato = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TestDato = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Overflatetilstand = table.Column<string>(type: "TEXT", nullable: false),
                    Dm = table.Column<double>(type: "REAL", nullable: false),
                    Prøvetykke = table.Column<double>(type: "REAL", nullable: false),
                    DmPrøvetykkeRatio = table.Column<double>(type: "REAL", nullable: false),
                    TrykkfasthetMPa = table.Column<double>(type: "REAL", nullable: false),
                    FasthetSammenligning = table.Column<string>(type: "TEXT", nullable: false),
                    FørSliping = table.Column<double>(type: "REAL", nullable: false),
                    EtterSliping = table.Column<double>(type: "REAL", nullable: false),
                    MmTilTopp = table.Column<double>(type: "REAL", nullable: false),
                    ReportModelId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataEtterKuttingOgSlipingModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataEtterKuttingOgSlipingModels_ReportModels_ReportModelId",
                        column: x => x.ReportModelId,
                        principalTable: "ReportModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataFraOppdragsgiverPrøverModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Datomottatt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Overdekningoppgitt = table.Column<string>(type: "TEXT", nullable: false),
                    Dmax = table.Column<string>(type: "TEXT", nullable: false),
                    KjerneImax = table.Column<int>(type: "INTEGER", nullable: false),
                    KjerneImin = table.Column<int>(type: "INTEGER", nullable: false),
                    OverflateOK = table.Column<string>(type: "TEXT", nullable: false),
                    OverflateUK = table.Column<string>(type: "TEXT", nullable: false),
                    ReportModelId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataFraOppdragsgiverPrøverModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataFraOppdragsgiverPrøverModels_ReportModels_ReportModelId",
                        column: x => x.ReportModelId,
                        principalTable: "ReportModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportImageModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    ReportModelId = table.Column<Guid>(type: "TEXT", nullable: false)
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
                name: "IX_concreteDensityModels_ReportModelId",
                table: "concreteDensityModels",
                column: "ReportModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DataEtterKuttingOgSlipingModels_ReportModelId",
                table: "DataEtterKuttingOgSlipingModels",
                column: "ReportModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DataFraOppdragsgiverPrøverModels_ReportModelId",
                table: "DataFraOppdragsgiverPrøverModels",
                column: "ReportModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportImageModels_ReportModelId",
                table: "ReportImageModels",
                column: "ReportModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "concreteDensityModels");

            migrationBuilder.DropTable(
                name: "DataEtterKuttingOgSlipingModels");

            migrationBuilder.DropTable(
                name: "DataFraOppdragsgiverPrøverModels");

            migrationBuilder.DropTable(
                name: "ReportImageModels");

            migrationBuilder.DropTable(
                name: "ReportModels");
        }
    }
}
