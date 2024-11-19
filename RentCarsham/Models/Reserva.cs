using System;
using System.Collections.Generic;

namespace RentCarsham.Models;

public partial class Reserva
{
    public int ReservaId { get; set; }

    public int UsuarioId { get; set; }

    public int VehiculoId { get; set; }

    public DateTime FechaReserva { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;

    public virtual Vehiculo Vehiculo { get; set; } = null!;
}
