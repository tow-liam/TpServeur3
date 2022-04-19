using System;
namespace TpServeur1.Config
{
    public class SMTPConfig
    {
        public int Port { get; set; }
        public string Serveur { get; set; }
        public string Utilisateur { get; set; }
        public string MotDePasse { get; set; }
    }
}
