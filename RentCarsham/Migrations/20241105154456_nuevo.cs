using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCarsham.Migrations
{
    /// <inheritdoc />
    public partial class nuevo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    MarcaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Pais = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Marcas__D5B1CD8BFFE75C72", x => x.MarcaId);
                });

            migrationBuilder.CreateTable(
                name: "Sucursales",
                columns: table => new
                {
                    SucursalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Telefono = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sucursal__6CB482E10B2C0321", x => x.SucursalId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Direccion = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    DocumentoIdentidad = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuarios__2B3DE7B86C910C35", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Modelos",
                columns: table => new
                {
                    ModeloId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Caja = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CapacidadPersonas = table.Column<int>(type: "int", nullable: false),
                    CapacidadMaletero = table.Column<int>(type: "int", nullable: false),
                    MarcaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Modelos__FA60529A21A7EC68", x => x.ModeloId);
                    table.ForeignKey(
                        name: "FK__Modelos__MarcaId__164452B1",
                        column: x => x.MarcaId,
                        principalTable: "Marcas",
                        principalColumn: "MarcaId");
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    VehiculoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarcaId = table.Column<int>(type: "int", nullable: false),
                    ModeloId = table.Column<int>(type: "int", nullable: false),
                    Anio = table.Column<int>(type: "int", nullable: false),
                    PrecioPorDia = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Disponible = table.Column<bool>(type: "bit", nullable: false),
                    Placa = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Kilometraje = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Vehiculo__AA088600294C68B2", x => x.VehiculoId);
                    table.ForeignKey(
                        name: "FK__Vehiculos__Marca__1A14E395",
                        column: x => x.MarcaId,
                        principalTable: "Marcas",
                        principalColumn: "MarcaId");
                    table.ForeignKey(
                        name: "FK__Vehiculos__Model__1B0907CE",
                        column: x => x.ModeloId,
                        principalTable: "Modelos",
                        principalColumn: "ModeloId");
                });

            migrationBuilder.CreateTable(
                name: "Alquileres",
                columns: table => new
                {
                    AlquilerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    VehiculoId = table.Column<int>(type: "int", nullable: false),
                    FechaAlquiler = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaDevolucion = table.Column<DateTime>(type: "datetime", nullable: false),
                    TotalPago = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Alquiler__F28020B57F83BCA9", x => x.AlquilerId);
                    table.ForeignKey(
                        name: "FK__Alquilere__Usuar__1DE57479",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId");
                    table.ForeignKey(
                        name: "FK__Alquilere__Vehic__1ED998B2",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "VehiculoId");
                });

            migrationBuilder.CreateTable(
                name: "Mantenimientos",
                columns: table => new
                {
                    MantenimientoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehiculoId = table.Column<int>(type: "int", nullable: false),
                    TipoMantenimiento = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    FechaMantenimiento = table.Column<DateTime>(type: "datetime", nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Mantenim__A62E61A2C6DE808C", x => x.MantenimientoId);
                    table.ForeignKey(
                        name: "FK__Mantenimi__Vehic__276EDEB3",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "VehiculoId");
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    ReservaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    VehiculoId = table.Column<int>(type: "int", nullable: false),
                    FechaReserva = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reservas__C39937633042D796", x => x.ReservaId);
                    table.ForeignKey(
                        name: "FK__Reservas__Usuari__2A4B4B5E",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId");
                    table.ForeignKey(
                        name: "FK__Reservas__Vehicu__2B3F6F97",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "VehiculoId");
                });

            migrationBuilder.CreateTable(
                name: "Seguros",
                columns: table => new
                {
                    SeguroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehiculoId = table.Column<int>(type: "int", nullable: false),
                    TipoSeguro = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Seguros__8B87D00A2DD844AD", x => x.SeguroId);
                    table.ForeignKey(
                        name: "FK__Seguros__Vehicul__24927208",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "VehiculoId");
                });

            migrationBuilder.CreateTable(
                name: "VehiculosSucursales",
                columns: table => new
                {
                    VehiculoId = table.Column<int>(type: "int", nullable: false),
                    SucursalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Vehiculo__ACC3CE2ECA68AD39", x => new { x.VehiculoId, x.SucursalId });
                    table.ForeignKey(
                        name: "FK__Vehiculos__Sucur__30F848ED",
                        column: x => x.SucursalId,
                        principalTable: "Sucursales",
                        principalColumn: "SucursalId");
                    table.ForeignKey(
                        name: "FK__Vehiculos__Vehic__300424B4",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "VehiculoId");
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    PagoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlquilerId = table.Column<int>(type: "int", nullable: false),
                    MontoPagado = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime", nullable: false),
                    MetodoPago = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pagos__F00B6138C59EB0A8", x => x.PagoId);
                    table.ForeignKey(
                        name: "FK__Pagos__AlquilerI__21B6055D",
                        column: x => x.AlquilerId,
                        principalTable: "Alquileres",
                        principalColumn: "AlquilerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alquileres_UsuarioId",
                table: "Alquileres",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Alquileres_VehiculoId",
                table: "Alquileres",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_VehiculoId",
                table: "Mantenimientos",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "UQ__Marcas__75E3EFCF6D3D6AAB",
                table: "Marcas",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_MarcaId",
                table: "Modelos",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_AlquilerId",
                table: "Pagos",
                column: "AlquilerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_UsuarioId",
                table: "Reservas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_VehiculoId",
                table: "Reservas",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Seguros_VehiculoId",
                table: "Seguros",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "UQ__Usuarios__A9D10534BBA585F7",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_MarcaId",
                table: "Vehiculos",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_ModeloId",
                table: "Vehiculos",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "UQ__Vehiculo__8310F99D214C4DC6",
                table: "Vehiculos",
                column: "Placa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehiculosSucursales_SucursalId",
                table: "VehiculosSucursales",
                column: "SucursalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mantenimientos");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Seguros");

            migrationBuilder.DropTable(
                name: "VehiculosSucursales");

            migrationBuilder.DropTable(
                name: "Alquileres");

            migrationBuilder.DropTable(
                name: "Sucursales");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Modelos");

            migrationBuilder.DropTable(
                name: "Marcas");
        }
    }
}
