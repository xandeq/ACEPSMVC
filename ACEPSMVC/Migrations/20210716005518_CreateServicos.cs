using Microsoft.EntityFrameworkCore.Migrations;

namespace ACEPSMVC.Migrations
{
    public partial class CreateServicos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Servicos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 250, nullable: false),
                    Logo = table.Column<string>(maxLength: 100, nullable: true),
                    Website = table.Column<string>(maxLength: 250, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Endereco = table.Column<string>(maxLength: 250, nullable: true),
                    Observacao = table.Column<string>(nullable: true),
                    DataCriacao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Servicos");
        }
    }
}
