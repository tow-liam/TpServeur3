using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TpServeur1.Models;

namespace TpServeur1.Controllers
{
    public class ItemPanierController : Controller
    {
        private readonly TpContext _context;

        public ItemPanierController(TpContext context)
        {
            _context = context;
        }

        // GET: ItemPanier
        public async Task<IActionResult> Index()
        {
            var itemPaniers = _context.ItemPanier.Include(i => i.Panier).Include(i => i.Produit);
            return View(await itemPaniers.ToListAsync());
        }

        // GET: ItemPanier/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPanier = await _context.ItemPanier
                .Include(i => i.Panier)
                .Include(i => i.Produit)
                .FirstOrDefaultAsync(m => m.ItemPanierID == id);
            if (itemPanier == null)
            {
                return NotFound();
            }

            return View(itemPanier);
        }

        // GET: ItemPanier/Create
        public IActionResult Create()
        {
            ViewData["PanierID"] = new SelectList(_context.Panier, "PanierID", "PanierID");
            ViewData["ProduitID"] = new SelectList(_context.Produits, "ProduitID", "ProduitID");
            return View();
        }

        // POST: ItemPanier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemPanierID,ProduitID,PanierID,Quantite")] ItemPanier itemPanier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemPanier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PanierID"] = new SelectList(_context.Panier, "PanierID", "PanierID", itemPanier.PanierID);
            ViewData["ProduitID"] = new SelectList(_context.Produits, "ProduitID", "ProduitID", itemPanier.ProduitID);
            return View(itemPanier);
        }

        // GET: ItemPanier/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPanier = await _context.ItemPanier.FindAsync(id);
            if (itemPanier == null)
            {
                return NotFound();
            }
            ViewData["PanierID"] = new SelectList(_context.Panier, "PanierID", "PanierID", itemPanier.PanierID);
            ViewData["ProduitID"] = new SelectList(_context.Produits, "ProduitID", "ProduitID", itemPanier.ProduitID);
            return View(itemPanier);
        }

        // POST: ItemPanier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemPanierID,ProduitID,PanierID,Quantite")] ItemPanier itemPanier)
        {
            if (id != itemPanier.ItemPanierID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemPanier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemPanierExists(itemPanier.ItemPanierID))
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
            ViewData["PanierID"] = new SelectList(_context.Panier, "PanierID", "PanierID", itemPanier.PanierID);
            ViewData["ProduitID"] = new SelectList(_context.Produits, "ProduitID", "ProduitID", itemPanier.ProduitID);
            return View(itemPanier);
        }

        // GET: ItemPanier/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPanier = await _context.ItemPanier
                .Include(i => i.Panier)
                .Include(i => i.Produit)
                .FirstOrDefaultAsync(m => m.ItemPanierID == id);
            if (itemPanier == null)
            {
                return NotFound();
            }

            return View(itemPanier);
        }

        // POST: ItemPanier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemPanier = await _context.ItemPanier.FindAsync(id);
            _context.ItemPanier.Remove(itemPanier);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Panier");
        }

        private bool ItemPanierExists(int id)
        {
            return _context.ItemPanier.Any(e => e.ItemPanierID == id);
        }
    }
}
