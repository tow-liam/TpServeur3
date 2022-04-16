using System;
using System.Collections.Generic;
using System.Linq;

namespace TpServeur1.Models
{
    public static class DbInitializer
    {
        public static void Initialize(TpContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Categorie>() {
                    new Categorie{Id=1,Nom="Hockey"},
                    new Categorie{Id=2,Nom="Soccer"},
                    new Categorie{Id=3,Nom="Baseball"},
                    new Categorie{Id=4,Nom="Vélo"},
                };
                context.Categories.AddRange(categories);
            }
            context.SaveChanges();
        
            if (!context.Produits.Any())
            {
                var produits = new List<Produit>() {
                    new Produit{Nom="Gant d'hockey",Description="Noir et rouge",Marque="Bauer", Taille=Size.Medium, QteInventaire=5, CategorieId=1},
                    new Produit{Nom="Gant d'hockey",Description="Noir et rouge",Marque="Bauer", Taille=Size.Petit, QteInventaire=5, CategorieId=1},
                    new Produit{Nom="Gant d'hockey",Description="Blanc et or",Marque="CCM", Taille=Size.Medium, QteInventaire=5, CategorieId=1},
                    new Produit{Nom="Soulier à crampon",Description="Blanc et or",Marque="Nike", Taille=Size.Medium, QteInventaire=5, CategorieId=2},
                    new Produit{Nom="Soulier à crampon",Description="Noir et arc-en-ciel",Marque="Adidas", Taille=Size.Medium, QteInventaire=5, CategorieId=2},
                    new Produit{Nom="Gant de baseball",Description="Rouge",Marque="Adidas", Taille=Size.Medium, QteInventaire=5, CategorieId=3},
                    new Produit{Nom="Bâton de baseball",Description="Noir",Marque="Easton", QteInventaire=5, CategorieId=3},
                    new Produit{Nom="Vélo de route",Description="Vert",Marque="Giant", Taille=Size.Medium, QteInventaire=5, CategorieId=4},
                    new Produit{Nom="Vélo de montagne",Description="Bleu",Marque="Norco", Taille=Size.Medium, QteInventaire=5, CategorieId=4},
                };
                context.Produits.AddRange(produits);
            }
            
            context.SaveChanges();
        }
    }
}
