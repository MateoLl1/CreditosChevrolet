using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditosChevrolet.Migrations
{
    /// <inheritdoc />
    public partial class AddPlazoMesesToRespuesta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlazoMeses",
                table: "RespuestasCredito",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlazoMeses",
                table: "RespuestasCredito");
        }
    }
}
