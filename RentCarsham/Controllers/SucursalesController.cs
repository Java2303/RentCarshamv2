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
    public class SucursalesController : Controller
    {
        private readonly RentCarshamContext _context;

        public SucursalesController(RentCarshamContext context)
        {
            _context = context;
        }

        // GET: Sucursales
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sucursales.ToListAsync());
        }

        // GET: Sucursales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursale = await _context.Sucursales
                .FirstOrDefaultAsync(m => m.SucursalId == id);
            if (sucursale == null)
            {
                return NotFound();
            }

            return View(sucursale);
        }

        // GET: Sucursales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sucursales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SucursalId,Nombre,Direccion,Telefono")] Sucursale sucursale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sucursale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sucursale);
        }

        // GET: Sucursales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursale = await _context.Sucursales.FindAsync(id);
            if (sucursale == null)
            {
                return NotFound();
            }
            return View(sucursale);
        }

        // POST: Sucursales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SucursalId,Nombre,Direccion,Telefono")] Sucursale sucursale)
        {
            if (id != sucursale.SucursalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sucursale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SucursaleExists(sucursale.SucursalId))
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
            return View(sucursale);
        }

        // GET: Sucursales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursale = await _context.Sucursales
                .FirstOrDefaultAsync(m => m.SucursalId == id);
            if (sucursale == null)
            {
                return NotFound();
            }

            return View(sucursale);
        }

        // POST: Sucursales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sucursale = await _context.Sucursales.FindAsync(id);
            if (sucursale != null)
            {
                _context.Sucursales.Remove(sucursale);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SucursaleExists(int id)
        {
            return _context.Sucursales.Any(e => e.SucursalId == id);
        }
    }
}
