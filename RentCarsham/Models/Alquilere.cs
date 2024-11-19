using System;
using System.Collections.Generic;

namespace RentCarsham.Models;

public partial class Alquilere
{
    public int AlquilerId { get; set; }

    public int UsuarioId { get; set; }

    public int VehiculoId { get; set; }

    public DateTime FechaAlquiler { get; set; }

    public DateTime FechaDevolucion { get; set; }

    public decimal TotalPago { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual Usuario Usuario { get; set; } = null!;

    public virtual Vehiculo Vehiculo { get; set; } = null!;
}
