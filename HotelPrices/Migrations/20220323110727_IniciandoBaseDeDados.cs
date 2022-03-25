using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelPrices.Migrations
{
    public partial class IniciandoBaseDeDados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HotelQuarto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    NumeroAdultos = table.Column<int>(nullable: false),
                    CheckIn = table.Column<DateTime>(nullable: false),
                    CheckOut = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelQuarto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotelQuartoTarifa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelQuartoId = table.Column<int>(nullable: false),
                    Condicao = table.Column<string>(nullable: true),
                    ValorMedio = table.Column<string>(nullable: true),
                    ValorTotal = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelQuartoTarifa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelQuartoTarifa_HotelQuarto_HotelQuartoId",
                        column: x => x.HotelQuartoId,
                        principalTable: "HotelQuarto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelQuartoTarifa_HotelQuartoId",
                table: "HotelQuartoTarifa",
                column: "HotelQuartoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelQuartoTarifa");

            migrationBuilder.DropTable(
                name: "HotelQuarto");
        }
    }
}
