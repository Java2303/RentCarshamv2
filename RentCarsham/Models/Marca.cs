using System;
using System.Collections.Generic;

namespace RentCarsham.Models;

public partial class Marca
{
    public int MarcaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public virtual ICollection<Modelo> Modelos { get; set; } = new List<Modelo>();

    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
