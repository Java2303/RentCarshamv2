using System;
using System.Collections.Generic;

namespace RentCarsham.Models;

public partial class Pago
{
    public int PagoId { get; set; }

    public int AlquilerId { get; set; }

    public decimal MontoPagado { get; set; }

    public DateTime FechaPago { get; set; }

    public string? MetodoPago { get; set; }

    public virtual Alquilere Alquiler { get; set; } = null!;
}
