using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TpServeur1.Models
{
    public class Commande
    {
        public int CommandeID { get; set; }
        public string UserGuid { get; set; }
        public List<ItemCommande> Items { get; set; }
    }
}
