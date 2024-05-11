using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Report_Generator_EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class INITIALMIGRATION : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tittle = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<bool>(type: "INTEGER", nullable: false),
                    Kunde = table.Column<string>(type: "TEXT", nullable: false),
                    AvvikFraStandarder = table.Column<string>(type: "TEXT", nullable: false),
                    MotattDato = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Kommentarer = table.Column<string>(type: "TEXT", nullable: false),
                    UiaRegnr = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "KontrollertAvførtAvModels",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Department = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Position = table.Column<string>(type: "TEXT", nullable: false),
                    ReportModelID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KontrollertAvførtAvModels", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KontrollertAvførtAvModels_ReportModels_ReportModelID",
                        column: x => x.ReportModelID,
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

            migrationBuilder.CreateTable(
                name: "tests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReportModelId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tests_ReportModels_ReportModelId",
                        column: x => x.ReportModelId,
                        principalTable: "ReportModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestUtførtAvModels",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Department = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Position = table.Column<string>(type: "TEXT", nullable: false),
                    ReportModelID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestUtførtAvModels", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TestUtførtAvModels_ReportModels_ReportModelID",
                        column: x => x.ReportModelID,
                        principalTable: "ReportModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trykktestingModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TrykkflateMm = table.Column<decimal>(type: "TEXT", nullable: false),
                    PalastHastighetMPas = table.Column<decimal>(type: "TEXT", nullable: false),
                    TrykkfasthetMPa = table.Column<decimal>(type: "TEXT", nullable: false),
                    TrykkfasthetMPaNSE = table.Column<decimal>(type: "TEXT", nullable: false),
                    ReportModelId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trykktestingModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trykktestingModels_ReportModels_ReportModelId",
                        column: x => x.ReportModelId,
                        principalTable: "ReportModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "verktøies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReportModelId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_verktøies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_verktøies_ReportModels_ReportModelId",
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
                name: "IX_KontrollertAvførtAvModels_ReportModelID",
                table: "KontrollertAvførtAvModels",
                column: "ReportModelID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportImageModels_ReportModelId",
                table: "ReportImageModels",
                column: "ReportModelId");

            migrationBuilder.CreateIndex(
                name: "IX_tests_ReportModelId",
                table: "tests",
                column: "ReportModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TestUtførtAvModels_ReportModelID",
                table: "TestUtførtAvModels",
                column: "ReportModelID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trykktestingModels_ReportModelId",
                table: "trykktestingModels",
                column: "ReportModelId");

            migrationBuilder.CreateIndex(
                name: "IX_verktøies_ReportModelId",
                table: "verktøies",
                column: "ReportModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "concreteDensityModels");

            migrationBuilder.DropTable(
                name: "DataEtterKuttingOgSlipingModels");

            migrationBuilder.DropTable(
                name: "DataFraOppdragsgiverPrøverModels");

            migrationBuilder.DropTable(
                name: "KontrollertAvførtAvModels");

            migrationBuilder.DropTable(
                name: "ReportImageModels");

            migrationBuilder.DropTable(
                name: "tests");

            migrationBuilder.DropTable(
                name: "TestUtførtAvModels");

            migrationBuilder.DropTable(
                name: "trykktestingModels");

            migrationBuilder.DropTable(
                name: "verktøies");

            migrationBuilder.DropTable(
                name: "ReportModels");
        }
    }
}
