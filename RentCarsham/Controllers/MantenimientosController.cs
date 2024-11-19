using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentCarsham.Models;

namespace RentCarsham.Controllers
{
    public class MantenimientosController : Controller
    {
        private readonly RentCarshamContext _context;

        public MantenimientosController(RentCarshamContext context)
        {
            _context = context;
        }

        // GET: Mantenimientos
        public async Task<IActionResult> Index()
        {
            var rentCarshamContext = _context.Mantenimientos.Include(m => m.Vehiculo);
            return View(await rentCarshamContext.ToListAsync());
        }

        // GET: Mantenimientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mantenimiento = await _context.Mantenimientos
                .Include(m => m.Vehiculo)
                .FirstOrDefaultAsync(m => m.MantenimientoId == id);
            if (mantenimiento == null)
            {
                return NotFound();
            }

            return View(mantenimiento);
        }

        // GET: Mantenimientos/Create
        public IActionResult Create()
        {
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "VehiculoId");
            return View();
        }

        // POST: Mantenimientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MantenimientoId,VehiculoId,TipoMantenimiento,FechaMantenimiento,Costo,Descripcion")] Mantenimiento mantenimiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mantenimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "VehiculoId", mantenimiento.VehiculoId);
            return View(mantenimiento);
        }

        // GET: Mantenimientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mantenimiento = await _context.Mantenimientos.FindAsync(id);
            if (mantenimiento == null)
            {
                return NotFound();
            }
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "VehiculoId", mantenimiento.VehiculoId);
            return View(mantenimiento);
        }

        // POST: Mantenimientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MantenimientoId,VehiculoId,TipoMantenimiento,FechaMantenimiento,Costo,Descripcion")] Mantenimiento mantenimiento)
        {
            if (id != mantenimiento.MantenimientoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mantenimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MantenimientoExists(mantenimiento.MantenimientoId))
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
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "VehiculoId", mantenimiento.VehiculoId);
            return View(mantenimiento);
        }

        // GET: Mantenimientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mantenimiento = await _context.Mantenimientos
                .Include(m => m.Vehiculo)
                .FirstOrDefaultAsync(m => m.MantenimientoId == id);
            if (mantenimiento == null)
            {
                return NotFound();
            }

            return View(mantenimiento);
        }

        // POST: Mantenimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mantenimiento = await _context.Mantenimientos.FindAsync(id);
            if (mantenimiento != null)
            {
                _context.Mantenimientos.Remove(mantenimiento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MantenimientoExists(int id)
        {
            return _context.Mantenimientos.Any(e => e.MantenimientoId == id);
        }
    }
}
