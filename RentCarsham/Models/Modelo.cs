using System;
using System.Collections.Generic;

namespace RentCarsham.Models;

public partial class Modelo
{
    public int ModeloId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Caja { get; set; } = null!;

    public int CapacidadPersonas { get; set; }

    public int CapacidadMaletero { get; set; }

    public int MarcaId { get; set; }

    public virtual Marca Marca { get; set; } = null!;

    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
