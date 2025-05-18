using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZumitoWeb.Data;
using ZumitoWeb.Models;
using System.Linq.Expressions;

namespace ZumitoWeb.Controllers
{
    public class RutasController : Controller
    {
        private readonly ZumitoWebContext _context;
        private Grafo grafo = new Grafo();

        public RutasController(ZumitoWebContext context)
        {
            _context = context;
        }

        // GET: Rutas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ruta.Include(r => r.Empleado);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rutas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ruta = await _context.Ruta
                .Include(r => r.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ruta == null)
            {
                return NotFound();
            }

            return View(ruta);
        }

        // GET: Rutas/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "Id", "Id");
            return View();
        }

        // POST: Rutas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Camino,Estado,EmpleadoId")] Ruta ruta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ruta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "Id", "Id", ruta.EmpleadoId);
            return View(ruta);
        }

        // GET: Rutas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ruta = await _context.Ruta.FindAsync(id);
            if (ruta == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "Id", "Id", ruta.EmpleadoId);
            return View(ruta);
        }

        // POST: Rutas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Camino,Estado,EmpleadoId")] Ruta ruta)
        {
            if (id != ruta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ruta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RutaExists(ruta.Id))
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
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "Id", "Id", ruta.EmpleadoId);
            return View(ruta);
        }

        // GET: Rutas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ruta = await _context.Ruta
                .Include(r => r.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ruta == null)
            {
                return NotFound();
            }

            return View(ruta);
        }

        // POST: Rutas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ruta = await _context.Ruta.FindAsync(id);
            if (ruta != null)
            {
                _context.Ruta.Remove(ruta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RutaExists(int id)
        {
            return _context.Ruta.Any(e => e.Id == id);
        }

        public async void CrearGrafos(int[] ids)
        {
            grafo.Nodos.Clear();
            grafo.Aristas.Clear();

            //Nodo origen
            grafo.AgregarNodo(new Nodo("0", 13.674378406007788, -89.23707583262862));
            
            var pedidos = await _context.Pedido.Include(p => p.Cliente).Where(p => ids.Contains(p.Id)).ToListAsync();
            foreach (var pedido in pedidos)
            {
                grafo.AgregarNodo(new Nodo(pedido.Id.ToString(), pedido.Cliente.Latitud, pedido.Cliente.Longitud));
            }

            grafo.ConstruirGrafoCompleto();

        }

        public static Dictionary<Nodo, double> Calcular(Grafo grafo, Nodo origen)
        {
            var distancias = new Dictionary<Nodo, double>();
            var visitados = new HashSet<Nodo>();
            var cola = new PriorityQueue<Nodo, double>();

            foreach (var nodo in grafo.Nodos)
            {
                distancias[nodo] = double.PositiveInfinity;
            }

            distancias[origen] = 0;
            cola.Enqueue(origen, 0);

            while (cola.Count > 0)
            {
                var actual = cola.Dequeue();

                if (visitados.Contains(actual))
                    continue;

                visitados.Add(actual);

                var aristasVecinas = grafo.Aristas.Where(a => a.Origen == actual);

                foreach (var arista in aristasVecinas)
                {
                    var destino = arista.Destino;
                    var nuevaDistancia = distancias[actual] + arista.Peso;

                    if (nuevaDistancia < distancias[destino])
                    {
                        distancias[destino] = nuevaDistancia;
                        cola.Enqueue(destino, nuevaDistancia);
                    }
                }
            }

            return distancias;
        }
    }
}
