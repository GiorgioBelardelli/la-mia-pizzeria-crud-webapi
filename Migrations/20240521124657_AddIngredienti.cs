using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    public partial class AddIngredienti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pizzas_Categorie_CategoriaId",
                table: "pizzas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pizzas",
                table: "pizzas");

            migrationBuilder.RenameTable(
                name: "pizzas",
                newName: "Pizzas");

            migrationBuilder.RenameIndex(
                name: "IX_pizzas_CategoriaId",
                table: "Pizzas",
                newName: "IX_Pizzas_CategoriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pizzas",
                table: "Pizzas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Ingredienti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredienti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientiPizza",
                columns: table => new
                {
                    IngredientiId = table.Column<int>(type: "int", nullable: false),
                    PizzeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientiPizza", x => new { x.IngredientiId, x.PizzeId });
                    table.ForeignKey(
                        name: "FK_IngredientiPizza_Ingredienti_IngredientiId",
                        column: x => x.IngredientiId,
                        principalTable: "Ingredienti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientiPizza_Pizzas_PizzeId",
                        column: x => x.PizzeId,
                        principalTable: "Pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientiPizza_PizzeId",
                table: "IngredientiPizza",
                column: "PizzeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_Categorie_CategoriaId",
                table: "Pizzas",
                column: "CategoriaId",
                principalTable: "Categorie",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_Categorie_CategoriaId",
                table: "Pizzas");

            migrationBuilder.DropTable(
                name: "IngredientiPizza");

            migrationBuilder.DropTable(
                name: "Ingredienti");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pizzas",
                table: "Pizzas");

            migrationBuilder.RenameTable(
                name: "Pizzas",
                newName: "pizzas");

            migrationBuilder.RenameIndex(
                name: "IX_Pizzas_CategoriaId",
                table: "pizzas",
                newName: "IX_pizzas_CategoriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pizzas",
                table: "pizzas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_pizzas_Categorie_CategoriaId",
                table: "pizzas",
                column: "CategoriaId",
                principalTable: "Categorie",
                principalColumn: "Id");
        }
    }
}
