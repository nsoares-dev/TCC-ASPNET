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
    public class ProdutosController : Controller
    {
        private readonly FinalProject___NicolasContext _context;

        public ProdutosController(FinalProject___NicolasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Produto.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .Include(p => p.CategoriaProduto)
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        public IActionResult Create()
        {
            ViewData["CategoriaProdutoId"] = new SelectList(_context.Set<CategoriaProduto>(), "NomeCategoria", "NomeCategoria");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Vendedor, Administrator")]
        public async Task<IActionResult> Create([Bind("ProdutoId,ProdutoNome,ProdutoPreco,Estoque,Disponivel,PrecoDesconto,ImagemUrl,CategoriaProdutoId")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaProdutoId"] = new SelectList(_context.Set<CategoriaProduto>(), "NomeCategoria", "NomeCategoria", produto.CategoriaProdutoId);
            return View(produto);
        }

        [Authorize(Roles = "Vendedor, Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaProdutoId"] = new SelectList(_context.Set<CategoriaProduto>(), "NomeCategoria", "NomeCategoria", produto.CategoriaProdutoId);
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Vendedor, Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("ProdutoId,ProdutoNome,ProdutoPreco,Estoque,Disponivel,PrecoDesconto,ImagemUrl,CategoriaProdutoId")] Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.ProdutoId))
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
            ViewData["CategoriaProdutoId"] = new SelectList(_context.Set<CategoriaProduto>(), "NomeCategoria", "NomeCategoria", produto.CategoriaProdutoId);
            return View(produto);
        }

        [Authorize(Roles = "Vendedor, Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .Include(p => p.CategoriaProduto)
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Vendedor, Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produto.FindAsync(id);
            if (produto != null)
            {
                _context.Produto.Remove(produto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.ProdutoId == id);
        }
    }
}
