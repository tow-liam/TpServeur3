using System;
namespace TpServeur3.Models
{
    public class Produit
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
        public string NomProduit { get; set; }
        public string Description { get; set; }
        public string Marque { get; set; }
        public Size? Taille { get; set; }
        public int QteInventaire { get; set; }
        public int CategorieId { get; set; }
        public Categorie Categorie { get; set; }
    }

    public enum Size
    {
        TresPetit,
        Petit,
        Medium,
        Grand,
        TresGrand
    }
}
