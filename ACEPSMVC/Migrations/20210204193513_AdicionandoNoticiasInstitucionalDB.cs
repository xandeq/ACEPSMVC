using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ACEPSMVC.Migrations
{
    public partial class AdicionandoNoticiasInstitucionalDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Institucional",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institucional", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Noticias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subtitulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinhaFina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagemDestaque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagemInterna = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Noticias", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Institucional");

            migrationBuilder.DropTable(
                name: "Noticias");
        }
    }
}
