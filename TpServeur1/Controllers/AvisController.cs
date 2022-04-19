using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TpServeur1.Config;
using TpServeur1.Models;

namespace TpServeur1.Controllers
{
    public class AvisController : Controller
    {
        private readonly TpContext _context;
        private readonly SMTPConfig configSMTP;

        public AvisController(TpContext context, IOptions<SMTPConfig> config)
        {
            _context = context;
            configSMTP = config.Value;
        }

        [Authorize(Roles = "Administrateur")]
        // GET: Avis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Avis.ToListAsync());
        }

        [Authorize(Roles = "Administrateur")]
        // GET: Avis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avis = await _context.Avis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avis == null)
            {
                return NotFound();
            }

            return View(avis);
        }

        [Authorize]
        // GET: Avis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Avis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Prenom,Nom,Courriel,Commentaire")] Avis avis)
        {
            if (ModelState.IsValid)
            {
                SmtpClient smtpClient = new SmtpClient(configSMTP.Serveur, configSMTP.Port);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(configSMTP.Utilisateur, configSMTP.MotDePasse);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;

                MailMessage mail = new MailMessage();
                //mail.From = new MailAddress(avis.Courriel, "Client Sport Suprême");
                mail.From = new MailAddress(configSMTP.Utilisateur, "Client Sport Suprême");
                mail.To.Add(new MailAddress("1453304@cstjean.qc.ca"));
                mail.Subject = "Nouvel avis de " + avis.Prenom + " " + avis.Nom;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = avis.Commentaire;
                mail.BodyEncoding = System.Text.Encoding.UTF8;

                _context.Add(avis);
                await _context.SaveChangesAsync();
                smtpClient.Send(mail);
                mail.Dispose();
                return RedirectToAction("Merci", "Home");
            }
            return View(avis);
        }

        [Authorize(Roles = "Administrateur")]
        // GET: Avis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avis = await _context.Avis.FindAsync(id);
            if (avis == null)
            {
                return NotFound();
            }
            return View(avis);
        }

        [Authorize(Roles = "Administrateur")]
        // POST: Avis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Prenom,Nom,Courriel,Commentaire")] Avis avis)
        {
            if (id != avis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvisExists(avis.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(avis);
        }

        [Authorize(Roles = "Administrateur")]
        // GET: Avis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avis = await _context.Avis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avis == null)
            {
                return NotFound();
            }

            return View(avis);
        }

        [Authorize(Roles = "Administrateur")]
        // POST: Avis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avis = await _context.Avis.FindAsync(id);
            _context.Avis.Remove(avis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvisExists(int id)
        {
            return _context.Avis.Any(e => e.Id == id);
        }
    }
}
