using System;
using System.Collections.Generic;

namespace RentCarsham.Models;

public partial class Vehiculo
{
    public int VehiculoId { get; set; }

    public int MarcaId { get; set; }

    public int ModeloId { get; set; }

    public int Anio { get; set; }

    public decimal PrecioPorDia { get; set; }

    public bool Disponible { get; set; }

    public string Placa { get; set; } = null!;

    public int Kilometraje { get; set; }

    public virtual ICollection<Alquilere> Alquileres { get; set; } = new List<Alquilere>();

    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();

    public virtual Marca Marca { get; set; } = null!;

    public virtual Modelo Modelo { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual ICollection<Seguro> Seguros { get; set; } = new List<Seguro>();

    public virtual ICollection<Sucursale> Sucursals { get; set; } = new List<Sucursale>();
}