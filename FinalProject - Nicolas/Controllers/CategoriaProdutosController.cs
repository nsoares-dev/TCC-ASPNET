using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject___Nicolas.Data;
using FinalProject___Nicolas.Models;

namespace FinalProject___Nicolas.Controllers
{
    public class CategoriaProdutosController : Controller
    {
        private readonly FinalProject___NicolasContext _context;

        public CategoriaProdutosController(FinalProject___NicolasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoriaProduto.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaProduto = await _context.CategoriaProduto
                .FirstOrDefaultAsync(m => m.CategoriaProdutoId == id);
            if (categoriaProduto == null)
            {
                return NotFound();
            }

            return View(categoriaProduto);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriaProdutoId,NomeCategoria,DescricaoCategoria,Ativa")] CategoriaProduto categoriaProduto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriaProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaProduto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaProduto = await _context.CategoriaProduto.FindAsync(id);
            if (categoriaProduto == null)
            {
                return NotFound();
            }
            return View(categoriaProduto);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriaProdutoId,NomeCategoria,DescricaoCategoria,Ativa")] CategoriaProduto categoriaProduto)
        {
            if (id != categoriaProduto.CategoriaProdutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaProduto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaProdutoExists(categoriaProduto.CategoriaProdutoId))
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
            return View(categoriaProduto);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaProduto = await _context.CategoriaProduto
                .FirstOrDefaultAsync(m => m.CategoriaProdutoId == id);
            if (categoriaProduto == null)
            {
                return NotFound();
            }

            return View(categoriaProduto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriaProduto = await _context.CategoriaProduto.FindAsync(id);
            if (categoriaProduto != null)
            {
                _context.CategoriaProduto.Remove(categoriaProduto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaProdutoExists(int id)
        {
            return _context.CategoriaProduto.Any(e => e.CategoriaProdutoId == id);
        }
    }
}
