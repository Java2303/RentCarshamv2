using System;
using System.Collections.Generic;

namespace RentCarsham.Models;

public partial class Sucursale
{
    public int SucursalId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string? Telefono { get; set; }

    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
