using Microsoft.AspNetCore.Mvc;
using RentCarsham.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace RentCarsham.Controllers
{
    public class AccountController : Controller
    {
        private readonly RentCarshamContext _context;

        public AccountController(RentCarshamContext context)
        {
            _context = context;
        }

        // Vista de Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Acción de Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string telefono)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(telefono))
            {
                ViewBag.ErrorMessage = "El correo electrónico y el teléfono son obligatorios.";
                return View();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Telefono == telefono);

            if (usuario == null)
            {
                ViewBag.ErrorMessage = "Correo electrónico o teléfono incorrectos.";
                return View();
            }

            // Si el usuario existe, manejar la redirección según su rol
            if (usuario.Rol == "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            else if (usuario.Rol == "Empleado")
            {
                return RedirectToAction("Index", "Empleado");
            }
            else // Rol Cliente
            {
                return RedirectToAction("Index", "Cliente");
            }
        }

        // Vista de Registro
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Acción de Registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string nombre, string email, string telefono, string direccion, string documentoIdentidad, string rol)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(telefono) || string.IsNullOrEmpty(rol))
            {
                ViewBag.ErrorMessage = "Todos los campos son obligatorios.";
                return View();
            }

            // Verificar si el usuario ya existe
            var existingUser = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);

            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "El usuario con ese correo electrónico ya existe.";
                return View();
            }

            // Crear un nuevo usuario
            var nuevoUsuario = new Usuario
            {
                Nombre = nombre,
                Email = email,
                Telefono = telefono,
                Direccion = direccion,
                DocumentoIdentidad = documentoIdentidad,
                Rol = rol // Asignar el rol seleccionado
            };

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            // Redirigir al login después de registrarse
            return RedirectToAction("Login");
        }
    }
}