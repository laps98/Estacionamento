using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estacionamento.Context.Migrations
{
    /// <inheritdoc />
    public partial class criacaoDasTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "IdCarro",
                table: "Clientes");

            migrationBuilder.RenameTable(
                name: "Clientes",
                newName: "Cliente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MovimentacaoDeVeiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fluxo = table.Column<int>(type: "int", nullable: false),
                    DataDeEntrada = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataDeSaida = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacaoDeVeiculo", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TabelaDePreco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Periodo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaDePreco", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MovimentacaoDeCaixa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdMovimentacaoDeVeiculo = table.Column<int>(type: "int", nullable: true),
                    IdTabelaDePerco = table.Column<int>(type: "int", nullable: true),
                    Descricao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Fluxo = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TabelaDePrecoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacaoDeCaixa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentacaoDeCaixa_MovimentacaoDeVeiculo_IdMovimentacaoDeVe~",
                        column: x => x.IdMovimentacaoDeVeiculo,
                        principalTable: "MovimentacaoDeVeiculo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovimentacaoDeCaixa_TabelaDePreco_TabelaDePrecoId",
                        column: x => x.TabelaDePrecoId,
                        principalTable: "TabelaDePreco",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacaoDeCaixa_IdMovimentacaoDeVeiculo",
                table: "MovimentacaoDeCaixa",
                column: "IdMovimentacaoDeVeiculo");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacaoDeCaixa_TabelaDePrecoId",
                table: "MovimentacaoDeCaixa",
                column: "TabelaDePrecoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimentacaoDeCaixa");

            migrationBuilder.DropTable(
                name: "MovimentacaoDeVeiculo");

            migrationBuilder.DropTable(
                name: "TabelaDePreco");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.RenameTable(
                name: "Cliente",
                newName: "Clientes");

            migrationBuilder.AddColumn<string>(
                name: "IdCarro",
                table: "Clientes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "Id");
        }
    }
}
