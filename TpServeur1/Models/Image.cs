using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TpServeur1.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int ProduitId { get; set; }
        public Produit Produit { get; set; }
    }
}
