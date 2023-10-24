using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estacionamento.Context.Migrations
{
    /// <inheritdoc />
    public partial class MigrationAdm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Administrador",
                table: "Usuario",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Administrador",
                table: "Usuario");
        }
    }
}
