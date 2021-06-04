using Microsoft.EntityFrameworkCore.Migrations;

namespace ACEPSMVC.Migrations
{
    public partial class CreateTableUtilidadePublica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UtilidadePublica",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 250, nullable: false),
                    Url = table.Column<string>(maxLength: 250, nullable: true),
                    Imagem = table.Column<string>(maxLength: 250, nullable: false),
                    Conteudo = table.Column<string>(maxLength: 250, nullable: true),
                    DataCriacao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilidadePublica", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UtilidadePublica");
        }
    }
}
