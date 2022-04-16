using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TpServeur1.Models
{
    public class TpContext : DbContext
    {
        public TpContext(DbContextOptions<TpContext> options) : base(options)
        {

        }

        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Panier> Panier { get; set; }
        public DbSet<ItemPanier> ItemPanier { get; set; }
        public DbSet<Commande> Commande { get; set; }
        public DbSet<ItemCommande> ItemCommande { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
