using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TpServeur1.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public List<Produit> Produits { get; set; }
    }
}
