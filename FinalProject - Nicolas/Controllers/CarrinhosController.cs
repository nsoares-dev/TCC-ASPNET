using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject___Nicolas.Data;
using FinalProject___Nicolas.Models;
using Microsoft.AspNetCore.Authorization;


namespace FinalProject___Nicolas.Controllers
{
    [Authorize]
    public class CarrinhosController : Controller
    {
        private readonly FinalProject___NicolasContext _context;

        public CarrinhosController(FinalProject___NicolasContext context)
        {
            _context = context;
        }

        // GET: Carrinhos
        public async Task<IActionResult> Index()
        {
            var finalProject___NicolasContext = _context.Carrinho.Include(c => c.Cliente);
            return View(await finalProject___NicolasContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrinho = await _context.Carrinho
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.CarrinhoId == id);
            if (carrinho == null)
            {
                return NotFound();
            }

            return View(carrinho);
        }

        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "ClienteId", "NomeCliente");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarrinhoId,ClienteId,TotalCarrinho")] Carrinho carrinho)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carrinho);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "ClienteId", "NomeCliente", carrinho.ClienteId);
            return View(carrinho);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrinho = await _context.Carrinho.FindAsync(id);
            if (carrinho == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "ClienteId", "NomeCliente", carrinho.ClienteId);
            return View(carrinho);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarrinhoId,ClienteId,TotalCarrinho")] Carrinho carrinho)
        {
            if (id != carrinho.CarrinhoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrinho);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarrinhoExists(carrinho.CarrinhoId))
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
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "ClienteId", "NomeCliente", carrinho.ClienteId);
            return View(carrinho);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrinho = await _context.Carrinho
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.CarrinhoId == id);
            if (carrinho == null)
            {
                return NotFound();
            }

            return View(carrinho);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carrinho = await _context.Carrinho.FindAsync(id);
            if (carrinho != null)
            {
                _context.Carrinho.Remove(carrinho);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarrinhoExists(int id)
        {
            return _context.Carrinho.Any(e => e.CarrinhoId == id);
        }
    }
}
