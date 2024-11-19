using Microsoft.AspNetCore.Mvc;
using RentCarsham.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarsham.Controllers
{
    public class AdminController : Controller
    {
        private readonly RentCarshamContext _context;

        public AdminController(RentCarshamContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> VerUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return View(usuarios); // Ver todos los usuarios
        }

        // Ejemplo de una acción para gestionar reservas
        public IActionResult GestionarReservas()
        {
            return View(); // Vista para gestionar reservas (por ejemplo)
        }

        // Otras acciones que solo un admin podría realizar, como agregar coches, etc.
    }
}
