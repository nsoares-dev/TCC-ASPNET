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
    public class PedidosController : Controller
    {
        private readonly FinalProject___NicolasContext _context;

        public PedidosController(FinalProject___NicolasContext context)
        {
            _context = context;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            var finalProject___NicolasContext = _context.Pedido.Include(p => p.Cliente).Include(p => p.EnderecoEntrega).Include(p => p.StatusEntrega);
            return View(await finalProject___NicolasContext.ToListAsync());
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Cliente)
                .Include(p => p.EnderecoEntrega)
                .Include(p => p.StatusEntrega)
                .FirstOrDefaultAsync(m => m.PedidoId == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        [Authorize(Roles = "Cliente, Administrator")]
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "NomeCliente", "NomeCliente");
            ViewData["EnderecoId"] = new SelectList(_context.Set<Endereco>(), "EnderecoId", "EnderecoId");
            ViewData["StatusEntregaId"] = new SelectList(_context.Set<StatusEntrega>(), "DescricaoStatus", "StatusEntregaId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Cliente, Administrator")]
        public async Task<IActionResult> Create([Bind("PedidoId,ClienteId,DataPedido,Total,EnderecoId,StatusEntregaId")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "NomeCliente", "NomeCliente", pedido.ClienteId);
            ViewData["EnderecoId"] = new SelectList(_context.Set<Endereco>(), "EnderecoId", "EnderecoId", pedido.EnderecoId);
            ViewData["StatusEntregaId"] = new SelectList(_context.Set<StatusEntrega>(), "DescricaoStatus", "StatusEntregaId", pedido.StatusEntregaId);
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "NomeCliente", "NomeCliente", pedido.ClienteId);
            ViewData["EnderecoId"] = new SelectList(_context.Set<Endereco>(), "EnderecoId", "EnderecoId", pedido.EnderecoId);
            ViewData["StatusEntregaId"] = new SelectList(_context.Set<StatusEntrega>(), "DescricaoStatus", "StatusEntregaId", pedido.StatusEntregaId);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("PedidoId,ClienteId,DataPedido,Total,EnderecoId,StatusEntregaId")] Pedido pedido)
        {
            if (id != pedido.PedidoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.PedidoId))
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
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "NomeCliente", "NomeCliente", pedido.ClienteId);
            ViewData["EnderecoId"] = new SelectList(_context.Set<Endereco>(), "EnderecoId", "EnderecoId", pedido.EnderecoId);
            ViewData["StatusEntregaId"] = new SelectList(_context.Set<StatusEntrega>(), "DescricaoStatus", "StatusEntregaId", pedido.StatusEntregaId);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Cliente)
                .Include(p => p.EnderecoEntrega)
                .Include(p => p.StatusEntrega)
                .FirstOrDefaultAsync(m => m.PedidoId == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedido.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedido.Any(e => e.PedidoId == id);
        }
    }
}
