using System;
namespace TpServeur3.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int ProduitId { get; set; }
        public Produit Produit { get; set; }
    }
}
