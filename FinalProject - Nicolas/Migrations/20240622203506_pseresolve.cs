using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject___Nicolas.Migrations
{
    /// <inheritdoc />
    public partial class pseresolve : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VendedorId",
                table: "Produtos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Vendedor",
                columns: table => new
                {
                    VendedorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeVendedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailVendedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefoneVendedor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPFCNPJVendedor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedor", x => x.VendedorId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_VendedorId",
                table: "Produtos",
                column: "VendedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Vendedor_VendedorId",
                table: "Produtos",
                column: "VendedorId",
                principalTable: "Vendedor",
                principalColumn: "VendedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Vendedor_VendedorId",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "Vendedor");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_VendedorId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "VendedorId",
                table: "Produtos");
        }
    }
}
