using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estacionamento.Context.Migrations
{
    /// <inheritdoc />
    public partial class MigrationVaga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdVaga",
                table: "MovimentacaoDeVeiculo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Vaga",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaga", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacaoDeVeiculo_IdVaga",
                table: "MovimentacaoDeVeiculo",
                column: "IdVaga");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentacaoDeVeiculo_Vaga_IdVaga",
                table: "MovimentacaoDeVeiculo",
                column: "IdVaga",
                principalTable: "Vaga",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentacaoDeVeiculo_Vaga_IdVaga",
                table: "MovimentacaoDeVeiculo");

            migrationBuilder.DropTable(
                name: "Vaga");

            migrationBuilder.DropIndex(
                name: "IX_MovimentacaoDeVeiculo_IdVaga",
                table: "MovimentacaoDeVeiculo");

            migrationBuilder.DropColumn(
                name: "IdVaga",
                table: "MovimentacaoDeVeiculo");
        }
    }
}
