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
    public class SegurosController : Controller
    {
        private readonly RentCarshamContext _context;

        public SegurosController(RentCarshamContext context)
        {
            _context = context;
        }

        // GET: Seguros
        public async Task<IActionResult> Index()
        {
            var rentCarshamContext = _context.Seguros.Include(s => s.Vehiculo);
            return View(await rentCarshamContext.ToListAsync());
        }

        // GET: Seguros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seguro = await _context.Seguros
                .Include(s => s.Vehiculo)
                .FirstOrDefaultAsync(m => m.SeguroId == id);
            if (seguro == null)
            {
                return NotFound();
            }

            return View(seguro);
        }

        // GET: Seguros/Create
        public IActionResult Create()
        {
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "VehiculoId");
            return View();
        }

        // POST: Seguros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SeguroId,VehiculoId,TipoSeguro,Precio,FechaInicio,FechaFin")] Seguro seguro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seguro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "VehiculoId", seguro.VehiculoId);
            return View(seguro);
        }

        // GET: Seguros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seguro = await _context.Seguros.FindAsync(id);
            if (seguro == null)
            {
                return NotFound();
            }
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "VehiculoId", seguro.VehiculoId);
            return View(seguro);
        }

        // POST: Seguros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SeguroId,VehiculoId,TipoSeguro,Precio,FechaInicio,FechaFin")] Seguro seguro)
        {
            if (id != seguro.SeguroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seguro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeguroExists(seguro.SeguroId))
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
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "VehiculoId", seguro.VehiculoId);
            return View(seguro);
        }

        // GET: Seguros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seguro = await _context.Seguros
                .Include(s => s.Vehiculo)
                .FirstOrDefaultAsync(m => m.SeguroId == id);
            if (seguro == null)
            {
                return NotFound();
            }

            return View(seguro);
        }

        // POST: Seguros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seguro = await _context.Seguros.FindAsync(id);
            if (seguro != null)
            {
                _context.Seguros.Remove(seguro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeguroExists(int id)
        {
            return _context.Seguros.Any(e => e.SeguroId == id);
        }
    }
}
