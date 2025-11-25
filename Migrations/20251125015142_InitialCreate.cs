using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditosChevrolet.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SolicitudesCredito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroSolicitud = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AsesorId = table.Column<int>(type: "int", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinancieraId = table.Column<int>(type: "int", nullable: true),
                    MinutosReintento = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesCredito", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificacionesAsesor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsesorId = table.Column<int>(type: "int", nullable: false),
                    SolicitudCreditoId = table.Column<int>(type: "int", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Leido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificacionesAsesor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificacionesAsesor_SolicitudesCredito_SolicitudCreditoId",
                        column: x => x.SolicitudCreditoId,
                        principalTable: "SolicitudesCredito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespuestasCredito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudCreditoId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MontoAprobado = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Tasa = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    FechaRespuesta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JsonCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestasCredito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespuestasCredito_SolicitudesCredito_SolicitudCreditoId",
                        column: x => x.SolicitudCreditoId,
                        principalTable: "SolicitudesCredito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificacionesAsesor_SolicitudCreditoId",
                table: "NotificacionesAsesor",
                column: "SolicitudCreditoId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasCredito_SolicitudCreditoId",
                table: "RespuestasCredito",
                column: "SolicitudCreditoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesCredito_NumeroSolicitud",
                table: "SolicitudesCredito",
                column: "NumeroSolicitud",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificacionesAsesor");

            migrationBuilder.DropTable(
                name: "RespuestasCredito");

            migrationBuilder.DropTable(
                name: "SolicitudesCredito");
        }
    }
}
