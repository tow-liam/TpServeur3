using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TpServeur1.Migrations
{
    public partial class ImgFixReal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Produits_ProduitId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ProduitId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ProduitId",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Produits",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Images",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Images",
                type: "varbinary(4000)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produits_ImageId",
                table: "Produits",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produits_Images_ImageId",
                table: "Produits",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produits_Images_ImageId",
                table: "Produits");

            migrationBuilder.DropIndex(
                name: "IX_Produits_ImageId",
                table: "Produits");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Produits");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "ProduitId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProduitId",
                table: "Images",
                column: "ProduitId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Produits_ProduitId",
                table: "Images",
                column: "ProduitId",
                principalTable: "Produits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
