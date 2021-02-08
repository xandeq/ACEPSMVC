using Microsoft.EntityFrameworkCore.Migrations;

namespace ACEPSMVC.Migrations
{
    public partial class AdicionandoCampoTextoNoticias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Texto",
                table: "Noticias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Texto",
                table: "Noticias");
        }
    }
}
