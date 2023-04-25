using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pojistenci.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypPojisteni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazev = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypPojisteni", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pojistenec",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vek = table.Column<int>(type: "int", nullable: false),
                    Registrovan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kraj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypPojisteniId = table.Column<int>(type: "int", nullable: false),
                    TypPjisteniId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pojistenec", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pojistenec_TypPojisteni_TypPjisteniId",
                        column: x => x.TypPjisteniId,
                        principalTable: "TypPojisteni",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pojistenec_TypPjisteniId",
                table: "Pojistenec",
                column: "TypPjisteniId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pojistenec");

            migrationBuilder.DropTable(
                name: "TypPojisteni");
        }
    }
}
