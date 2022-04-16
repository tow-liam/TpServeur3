using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace TpServeur1.Migrations
{
    public partial class AddPanier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produits_Paniers_PanierId",
                table: "Produits");

            migrationBuilder.DropIndex(
                name: "IX_Produits_PanierId",
                table: "Produits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paniers",
                table: "Paniers");

            migrationBuilder.DropColumn(
                name: "PanierId",
                table: "Produits");

            migrationBuilder.RenameTable(
                name: "Paniers",
                newName: "Panier");

            migrationBuilder.AddColumn<double>(
                name: "Prix",
                table: "Produits",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "UserGuid",
                table: "Panier",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Panier",
                table: "Panier",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Commande",
                columns: table => new
                {
                    CommandeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserGuid = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commande", x => x.CommandeID);
                });

            migrationBuilder.CreateTable(
                name: "ItemPanier",
                columns: table => new
                {
                    ItemPanierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ProduitID = table.Column<int>(type: "int", nullable: false),
                    PanierID = table.Column<int>(type: "int", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPanier", x => x.ItemPanierID);
                    table.ForeignKey(
                        name: "FK_ItemPanier_Panier_PanierID",
                        column: x => x.PanierID,
                        principalTable: "Panier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPanier_Produits_ProduitID",
                        column: x => x.ProduitID,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemCommande",
                columns: table => new
                {
                    ItemCommandeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ProduitID = table.Column<int>(type: "int", nullable: false),
                    CommandeID = table.Column<int>(type: "int", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    MontantUnitaire = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCommande", x => x.ItemCommandeID);
                    table.ForeignKey(
                        name: "FK_ItemCommande_Commande_CommandeID",
                        column: x => x.CommandeID,
                        principalTable: "Commande",
                        principalColumn: "CommandeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemCommande_Produits_ProduitID",
                        column: x => x.ProduitID,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemCommande_CommandeID",
                table: "ItemCommande",
                column: "CommandeID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCommande_ProduitID",
                table: "ItemCommande",
                column: "ProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPanier_PanierID",
                table: "ItemPanier",
                column: "PanierID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPanier_ProduitID",
                table: "ItemPanier",
                column: "ProduitID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemCommande");

            migrationBuilder.DropTable(
                name: "ItemPanier");

            migrationBuilder.DropTable(
                name: "Commande");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Panier",
                table: "Panier");

            migrationBuilder.DropColumn(
                name: "Prix",
                table: "Produits");

            migrationBuilder.DropColumn(
                name: "UserGuid",
                table: "Panier");

            migrationBuilder.RenameTable(
                name: "Panier",
                newName: "Paniers");

            migrationBuilder.AddColumn<int>(
                name: "PanierId",
                table: "Produits",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paniers",
                table: "Paniers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produits_PanierId",
                table: "Produits",
                column: "PanierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produits_Paniers_PanierId",
                table: "Produits",
                column: "PanierId",
                principalTable: "Paniers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
