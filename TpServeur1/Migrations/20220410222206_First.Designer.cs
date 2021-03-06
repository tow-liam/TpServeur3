// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TpServeur1.Models;

namespace TpServeur1.Migrations
{
    [DbContext(typeof(TpContext))]
    [Migration("20220410222206_First")]
    partial class First
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.15");

            modelBuilder.Entity("TpServeur1.Models.Categorie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TpServeur1.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .HasColumnType("text");

                    b.Property<int>("ProduitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProduitId")
                        .IsUnique();

                    b.ToTable("Images");
                });

            modelBuilder.Entity("TpServeur1.Models.Panier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Paniers");
                });

            modelBuilder.Entity("TpServeur1.Models.Produit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategorieId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Marque")
                        .HasColumnType("text");

                    b.Property<string>("Nom")
                        .HasColumnType("text");

                    b.Property<int?>("PanierId")
                        .HasColumnType("int");

                    b.Property<int>("QteInventaire")
                        .HasColumnType("int");

                    b.Property<int?>("Taille")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategorieId");

                    b.HasIndex("PanierId");

                    b.ToTable("Produits");
                });

            modelBuilder.Entity("TpServeur1.Models.Image", b =>
                {
                    b.HasOne("TpServeur1.Models.Produit", "Produit")
                        .WithOne("Image")
                        .HasForeignKey("TpServeur1.Models.Image", "ProduitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produit");
                });

            modelBuilder.Entity("TpServeur1.Models.Produit", b =>
                {
                    b.HasOne("TpServeur1.Models.Categorie", "Categorie")
                        .WithMany("Produits")
                        .HasForeignKey("CategorieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TpServeur1.Models.Panier", null)
                        .WithMany("Produits")
                        .HasForeignKey("PanierId");

                    b.Navigation("Categorie");
                });

            modelBuilder.Entity("TpServeur1.Models.Categorie", b =>
                {
                    b.Navigation("Produits");
                });

            modelBuilder.Entity("TpServeur1.Models.Panier", b =>
                {
                    b.Navigation("Produits");
                });

            modelBuilder.Entity("TpServeur1.Models.Produit", b =>
                {
                    b.Navigation("Image");
                });
#pragma warning restore 612, 618
        }
    }
}
