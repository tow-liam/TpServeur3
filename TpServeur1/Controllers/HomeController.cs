using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TpServeur1.Models;

namespace TpServeur1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TpContext _context;

        public HomeController(ILogger<HomeController> logger, TpContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult APropos()
        {
            return View();
        }

        [Authorize]
        public IActionResult Avis()
        {
            return View();
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
