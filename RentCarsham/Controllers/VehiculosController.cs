using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentCarsham.Models;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.IO;


namespace RentCarsham.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly RentCarshamContext _context;
        private readonly ILogger<VehiculosController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VehiculosController(RentCarshamContext context, ILogger<VehiculosController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
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
        [HttpGet("Vehiculos/ReportePdf")]
        public async Task<IActionResult> GeneratePdfReport()
        {
            var vehiculos = await _context.Vehiculos
                .Include(v => v.Marca)
                .Include(v => v.Modelo)
                .ToListAsync();

            // Crear un documento PDF
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Fuentes
            XFont fontTitle = new XFont("Arial Black", 24, XFontStyle.Bold);
            XFont fontSubTitle = new XFont("Arial", 14, XFontStyle.Bold);
            XFont fontBody = new XFont("Arial", 12, XFontStyle.Regular);
            XFont fontTableHeader = new XFont("Arial", 12, XFontStyle.Bold);

            // Colores
            XBrush redBrush = XBrushes.Red;
            XBrush blackBrush = XBrushes.Black;
            XBrush grayBrush = XBrushes.LightGray;

            int yPoint = 50;

            // Agregar el logo
            string logoPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "logo.png");
            if (System.IO.File.Exists(logoPath))
            {
                XImage logo = XImage.FromFile(logoPath);
                gfx.DrawImage(logo, 40, 20, 120, 60);
                yPoint = 90;
            }

            // Título del reporte
            gfx.DrawString("Reporte de Vehículos", fontTitle, redBrush, new XPoint(200, yPoint));
            yPoint += 40;

            // Fecha actual
            string currentDate = DateTime.Now.ToString("dd/MM/yyyy");
            gfx.DrawString($"Fecha: {currentDate}", fontSubTitle, blackBrush, new XPoint(40, yPoint));
            yPoint += 30;

            // Encabezado de la tabla
            gfx.DrawRectangle(grayBrush, 40, yPoint, page.Width - 80, 25);
            gfx.DrawString("ID", fontTableHeader, redBrush, new XPoint(50, yPoint + 18));
            gfx.DrawString("Marca", fontTableHeader, redBrush, new XPoint(100, yPoint + 18));
            gfx.DrawString("Modelo", fontTableHeader, redBrush, new XPoint(200, yPoint + 18));
            gfx.DrawString("Año", fontTableHeader, redBrush, new XPoint(300, yPoint + 18));
            gfx.DrawString("Placa", fontTableHeader, redBrush, new XPoint(350, yPoint + 18));
            gfx.DrawString("Precio/Día", fontTableHeader, redBrush, new XPoint(450, yPoint + 18));
            gfx.DrawString("Kilometraje", fontTableHeader, redBrush, new XPoint(550, yPoint + 18));
            yPoint += 30;

            // Agregar los datos de la tabla
            foreach (var vehiculo in vehiculos)
            {
                gfx.DrawString(vehiculo.VehiculoId.ToString(), fontBody, blackBrush, new XPoint(50, yPoint));
                gfx.DrawString(vehiculo.Marca.Nombre, fontBody, blackBrush, new XPoint(100, yPoint));
                gfx.DrawString(vehiculo.Modelo.Nombre, fontBody, blackBrush, new XPoint(200, yPoint));
                gfx.DrawString(vehiculo.Anio.ToString(), fontBody, blackBrush, new XPoint(300, yPoint));
                gfx.DrawString(vehiculo.Placa, fontBody, blackBrush, new XPoint(350, yPoint));
                gfx.DrawString($"{vehiculo.PrecioPorDia:C}", fontBody, blackBrush, new XPoint(450, yPoint));
                gfx.DrawString($"{vehiculo.Kilometraje} km", fontBody, blackBrush, new XPoint(550, yPoint));

                // Línea de separación
                gfx.DrawLine(XPens.LightGray, 40, yPoint + 15, page.Width - 40, yPoint + 15);

                yPoint += 20;

                if (yPoint > page.Height - 50) // Salto de página
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    yPoint = 50;
                }
            }

            // Marca de agua
            gfx.TranslateTransform(page.Width / 2, page.Height / 2);
            gfx.RotateTransform(-45);
            gfx.DrawString("RentCarsham", new XFont("Arial", 50, XFontStyle.BoldItalic), new XSolidBrush(XColor.FromArgb(50, XColors.Gray)), new XPoint(0, 0), XStringFormats.Center);

            // Guardar en un stream
            using (var stream = new MemoryStream())
            {
                document.Save(stream, false);
                byte[] pdf = stream.ToArray();
                return File(pdf, "application/pdf", "ReporteVehiculosF1.pdf");
            }
        }

    }
}