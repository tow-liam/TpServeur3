using System;
using System.Collections.Generic;

namespace TpServeur3.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public List<Produit> ListProduit { get; set; }
    }
}
