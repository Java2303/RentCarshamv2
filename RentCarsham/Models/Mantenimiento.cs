using System;
using System.Collections.Generic;

namespace RentCarsham.Models;

public partial class Mantenimiento
{
    public int MantenimientoId { get; set; }

    public int VehiculoId { get; set; }

    public string TipoMantenimiento { get; set; } = null!;

    public DateTime FechaMantenimiento { get; set; }

    public decimal Costo { get; set; }

    public string? Descripcion { get; set; }

    public virtual Vehiculo Vehiculo { get; set; } = null!;
}
