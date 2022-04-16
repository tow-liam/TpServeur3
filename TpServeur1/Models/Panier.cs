using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TpServeur1.Models
{
    public class Panier
    {
        public int Id { get; set; }
        public string UserGuid { get; set; }
        public List<ItemPanier> ItemPanier { get; set; }
    }
}
