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

        public virtual DbSet<Categorie> Categories { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Produit> Produits { get; set; }
        public virtual DbSet<Panier> Paniers { get; set; }


    }
}
