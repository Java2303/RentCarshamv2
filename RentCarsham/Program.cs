using Microsoft.EntityFrameworkCore;
using RentCarsham.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Agregar el contexto de la base de datos RentCarshamContext
builder.Services.AddDbContext<RentCarshamContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));

// Configuración para manejar referencias cíclicas en JSON
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true; // Opcional: JSON legible
    });

// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirCualquierOrigen",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

// Aplica la política de CORS antes de autorizar
app.UseCors("PermitirCualquierOrigen");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
