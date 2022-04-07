using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TpServeur1.Models
{
    public class Panier
    {
        public int Id { get; set; }
        public List<Produit> Produits { get; set; }
    }
}
