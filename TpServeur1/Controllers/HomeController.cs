using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using TpServeur1.Config;
using TpServeur1.Models;

namespace TpServeur1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TpContext _context;
        private readonly SMTPConfig configSMTP;

        public HomeController(ILogger<HomeController> logger, TpContext context, IOptions<SMTPConfig> config)
        {
            _logger = logger;
            _context = context;
            configSMTP = config.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult APropos()
        {
            return View();
        }

        public IActionResult Reparations()
        {
            return File("~/Documents/Atelier.pdf", "application/pdf");
        }

        [Authorize]
        public IActionResult Avis()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult DonnezAvis(string prenom, string nom, string email, string review)
        {
            SmtpClient smtpClient = new SmtpClient(configSMTP.Serveur, configSMTP.Port);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(configSMTP.Utilisateur, configSMTP.MotDePasse);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(email, "Client Sport Suprême");
            mail.To.Add(new MailAddress("1453304@cstjean.qc.ca"));
            mail.Subject = "Nouvel avis de " + prenom + " " + nom;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = review;
            mail.BodyEncoding = System.Text.Encoding.UTF8;

            Evaluation evaluation = new Evaluation { Prenom = prenom, Nom = nom, Courriel = email, Avis = review };
            _context.Evaluations.Add(evaluation);
            _context.SaveChanges();
            smtpClient.Send(mail);
            mail.Dispose();
            return View("Merci");
        }

        public async Task<IActionResult> Produits(int? categorieId)
        {
            if (categorieId == null)
            {
                return NotFound();
            }

            var produits = _context.Produits.Include(p => p.Categorie).Include(p => p.Image).Where(m => m.CategorieId == categorieId);
            
            if (produits == null)
            {
                return NotFound();
            }

            return View(await produits.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
