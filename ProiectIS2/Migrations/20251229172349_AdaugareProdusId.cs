using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectIS2.Migrations
{
    /// <inheritdoc />
    public partial class AdaugareProdusId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NumarOrdine",
                table: "Comenzi",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "ProdusId",
                table: "Comenzi",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comenzi_ProdusId",
                table: "Comenzi",
                column: "ProdusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comenzi_Produse_ProdusId",
                table: "Comenzi",
                column: "ProdusId",
                principalTable: "Produse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comenzi_Produse_ProdusId",
                table: "Comenzi");

            migrationBuilder.DropIndex(
                name: "IX_Comenzi_ProdusId",
                table: "Comenzi");

            migrationBuilder.DropColumn(
                name: "ProdusId",
                table: "Comenzi");

            migrationBuilder.AlterColumn<string>(
                name: "NumarOrdine",
                table: "Comenzi",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
