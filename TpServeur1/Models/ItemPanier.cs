using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TpServeur1.Models
{
    public class ItemPanier
    {
        public int ItemPanierID { get; set; }
        public int ProduitID { get; set; }
        public int PanierID { get; set; }
        public int Quantite { get; set; }

        public Panier Panier { get; set; }
        public Produit Produit { get; set; }
    }
}
