using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TpServeur1.Models;

namespace TpServeur1.Controllers
{
    
    public class ProduitController : Controller
    {
        private readonly TpContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProduitController(TpContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Administrateur")]
        // GET: Produit
        public async Task<IActionResult> Index()
        {
            var tpContext = _context.Produits.Include(p => p.Categorie).Include(p => p.Image);
            return View(await tpContext.ToListAsync());
        }

        // GET: Produit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits
                .Include(p => p.Categorie).Include(p => p.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }

        [Authorize(Roles = "Administrateur")]
        // GET: Produit/Create
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id");
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id");
            return View();
        }

        [Authorize(Roles = "Administrateur")]
        // POST: Produit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Description,Marque,Taille,QteInventaire,CategorieId")] Produit produit)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files.SingleOrDefault();
                    Image img = new Image()
                    {
                        Nom = file.FileName,
                        ContentType = file.ContentType
                    };

                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);

                    img.ImageData = ms.ToArray();
                    ms.Close();
                    ms.Dispose();

                    _context.Add(img);
                    _context.SaveChanges();

                    produit.ImageId = img.Id;
                }
                _context.Add(produit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", produit.CategorieId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", produit.ImageId);
            return View(produit);
        }

        [Authorize(Roles = "Administrateur")]
        // GET: Produit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits.FindAsync(id);
            if (produit == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", produit.CategorieId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", produit.ImageId);
            return View(produit);
        }

        [Authorize(Roles = "Administrateur")]
        // POST: Produit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description,Marque,Taille,QteInventaire,CategorieId, ImageId")] Produit produit)
        {
            if (id != produit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Request.Form.Files.Count > 0)
                    {
                        var file = Request.Form.Files.SingleOrDefault();
                        Image img = new Image()
                        {
                            Nom = file.FileName,
                            ContentType = file.ContentType
                        };

                        MemoryStream ms = new MemoryStream();
                        file.CopyTo(ms);

                        img.ImageData = ms.ToArray();
                        ms.Close();
                        ms.Dispose();

                        _context.Add(img);

                        if (produit.ImageId != null)
                        {
                            var imageASupprimer = await _context.Images.FindAsync(produit.ImageId);
                            produit.ImageId = null;
                            _context.SaveChanges();
                            _context.Remove(imageASupprimer);
                        }
                        _context.SaveChanges();

                        produit.ImageId = img.Id;
                    }
                    _context.Update(produit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduitExists(produit.Id))
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
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Id", produit.CategorieId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Id", produit.ImageId);
            return View(produit);
        }

        [Authorize(Roles = "Administrateur")]
        // GET: Produit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits
                .Include(p => p.Categorie).Include(p => p.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }

        [Authorize(Roles = "Administrateur")]
        // POST: Produit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            _context.Produits.Remove(produit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduitExists(int id)
        {
            return _context.Produits.Any(e => e.Id == id);
        }

        [Authorize]
        public async Task<IActionResult> AjoutPanier(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var current_User = _userManager.GetUserAsync(HttpContext.User).Result;
            string current_User_Id = (current_User != null) ? "" + current_User.Id : "";

            var panier = _context.Panier.FirstOrDefault(x => x.UserGuid == current_User_Id);

            if (panier == null)
            {
                panier = new Panier()
                {
                    UserGuid = current_User_Id,
                    ItemPanier = new List<ItemPanier>()
                };
                _context.Add(panier);
                await _context.SaveChangesAsync();
            }
            var item = _context.ItemPanier.FirstOrDefault(x => x.PanierID == panier.Id && x.ProduitID == id.Value);

            if (item == null)
            {
                item = new ItemPanier() { PanierID = panier.Id, ProduitID = id.Value, Quantite = 1 };
                _context.Add(item);
            }
            else
            {
                item.Quantite++;
                _context.Update(item);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
