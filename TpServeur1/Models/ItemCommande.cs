using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TpServeur1.Models
{
    public class ItemCommande
    {
        public int ItemCommandeID { get; set; }
        public int ProduitID { get; set; }
        public int CommandeID { get; set; }
        public int Quantite { get; set; }
        public double MontantUnitaire { get; set; }
        public Commande Commande { get; set; }
        public Produit Produit { get; set; }
    }
}
