using System;
using System.Collections.Generic;

namespace RentCarsham.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? DocumentoIdentidad { get; set; }
    public virtual ICollection<Alquilere> Alquileres { get; set; } = new List<Alquilere>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public string? Rol { get; set; }
}