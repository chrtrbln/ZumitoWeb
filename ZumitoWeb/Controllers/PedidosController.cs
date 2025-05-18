using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZumitoWeb.Data;
using ZumitoWeb.Models;
using ZumitoWeb.ViewModels;

namespace ZumitoWeb.Controllers
{
    public class PedidosController : Controller
    {
        private readonly ZumitoWebContext _context;

        public PedidosController(ZumitoWebContext context)
        {
            _context = context;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pedido.Include(p => p.Cliente).Include(p => p.Ruta);
            return View(await applicationDbContext.ToListAsync());
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
                .Include(p => p.Ruta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            //ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nombre");
            ViewBag.Clientes = new SelectList(_context.Cliente.ToList(), "Id", "Nombre");

            var productosDto = _context.Producto
                .Select(p => new { id = p.Id, nombre = p.Nombre, precio = p.Precio })
                .ToList();

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            ViewBag.ProductosJson = JsonSerializer.Serialize(productosDto);

            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrearPedidoViewModel model)
        {
            double total = 0;
            foreach (var p in model.Productos)
            {
                total = total + (p.Precio*p.Cantidad);
            }
            
            var pedido = new Pedido
            {
                ClienteId = model.ClienteId,
                Fecha = model.Fecha,
                CostoTotal = total
            };

            _context.Pedido.Add(pedido);
            await _context.SaveChangesAsync();

            foreach (var prod in model.Productos)
            {
                _context.ProductoPedido.Add(new ProductoPedido
                {
                    PedidoId = pedido.Id,
                    ProductoId = prod.ProductoId,
                    Cantidad = prod.Cantidad
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Id", pedido.ClienteId);
            ViewData["RutaId"] = new SelectList(_context.Set<Ruta>(), "Id", "Id", pedido.RutaId);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Estado,CostoTotal,RutaId,ClienteId")] Pedido pedido)
        {
            if (id != pedido.Id)
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
                    if (!PedidoExists(pedido.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Id", pedido.ClienteId);
            ViewData["RutaId"] = new SelectList(_context.Set<Ruta>(), "Id", "Id", pedido.RutaId);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Cliente)
                .Include(p => p.Ruta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
            return _context.Pedido.Any(e => e.Id == id);
        }

        public IActionResult PedidosRuta()
        {
            return View();
        }
    }
}
