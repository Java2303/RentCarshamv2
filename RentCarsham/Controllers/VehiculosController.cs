using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentCarsham.Models;

namespace RentCarsham.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly RentCarshamContext _context;
        private readonly ILogger<VehiculosController> _logger;

        public VehiculosController(RentCarshamContext context, ILogger<VehiculosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Vehiculos
        public async Task<IActionResult> Index()
        {
            var rentCarshamContext = _context.Vehiculos.Include(v => v.Marca).Include(v => v.Modelo);
            return View(await rentCarshamContext.ToListAsync());
        }

        // GET: Vehiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos
                .Include(v => v.Marca)
                .Include(v => v.Modelo)
                .FirstOrDefaultAsync(m => m.VehiculoId == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // GET: Vehiculos/Create
        public IActionResult Create()
        {
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "Nombre");
            ViewData["ModeloId"] = new SelectList(_context.Modelos, "ModeloId", "Nombre");
            return View();
        }

        // POST: Vehiculos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehiculoId,MarcaId,ModeloId,Anio,PrecioPorDia,Disponible,Placa,Kilometraje")] Vehiculo vehiculo)
        {
            if (true)
            {
                _context.Add(vehiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "Nombre", vehiculo.MarcaId);
            ViewData["ModeloId"] = new SelectList(_context.Modelos, "ModeloId", "Nombre", vehiculo.ModeloId);

            return View(vehiculo);
        }

        // GET: Vehiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
            {
                return NotFound();
            }
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "Nombre", vehiculo.MarcaId);
            ViewData["ModeloId"] = new SelectList(_context.Modelos, "ModeloId", "Nombre", vehiculo.ModeloId);
            return View(vehiculo);
        }

        // POST: Vehiculos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehiculoId,MarcaId,ModeloId,Anio,PrecioPorDia,Disponible,Placa,Kilometraje")] Vehiculo vehiculo)
        {
            if (id != vehiculo.VehiculoId)
            {
                return NotFound();
            }

            if (true)
            {
                try
                {
                    _context.Update(vehiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiculoExists(vehiculo.VehiculoId))
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

            ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "Nombre", vehiculo.MarcaId);
            ViewData["ModeloId"] = new SelectList(_context.Modelos, "ModeloId", "Nombre", vehiculo.ModeloId);

            return View(vehiculo); 
        }


        // GET: Vehiculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos
                .Include(v => v.Marca)
                .Include(v => v.Modelo)
                .FirstOrDefaultAsync(m => m.VehiculoId == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // POST: Vehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo != null)
            {
                _context.Vehiculos.Remove(vehiculo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiculoExists(int id)
        {
            return _context.Vehiculos.Any(e => e.VehiculoId == id);
        }

        // Nuevas funciones para la API

        // GET: api/Vehiculos
        [HttpGet("api/Vehiculos")]
        public async Task<IActionResult> GetVehiculos(int page = 1, int pageSize = 10)
        {
            var vehiculos = await _context.Vehiculos
                .Include(v => v.Marca)
                .Include(v => v.Modelo)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(vehiculos);
        }

        // GET: api/Vehiculos/{id}
        [HttpGet("api/Vehiculos/{id}")]
        public async Task<IActionResult> GetVehiculo(int id)
        {
            var vehiculo = await _context.Vehiculos
                .Include(v => v.Marca)
                .Include(v => v.Modelo)
                .FirstOrDefaultAsync(v => v.VehiculoId == id);

            if (vehiculo == null)
            {
                return NotFound();
            }
            return Ok(vehiculo);
        }
    }
}
