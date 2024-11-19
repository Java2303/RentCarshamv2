using System;
using System.Collections.Generic;

namespace RentCarsham.Models;

public partial class Seguro
{
    public int SeguroId { get; set; }

    public int VehiculoId { get; set; }

    public string TipoSeguro { get; set; } = null!;

    public decimal Precio { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public virtual Vehiculo Vehiculo { get; set; } = null!;
}
