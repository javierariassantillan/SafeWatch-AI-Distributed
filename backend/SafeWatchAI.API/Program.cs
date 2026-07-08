using Microsoft.EntityFrameworkCore;
using SafeWatchAI.API.Data;
using SafeWatchAI.API.Models;

var builder = WebApplication.CreateBuilder(args);

// =============================================================================
// Service Registration
// =============================================================================

// Configure Controllers
builder.Services.AddControllers();

// Configure Swagger/OpenAPI for development documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS - SafeWatch AI Frontend Access Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("SafeWatchAllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure Database Connection - SQLite
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

var app = builder.Build();

// =============================================================================
// HTTP Request Pipeline Configuration
// =============================================================================

// Enable Swagger/OpenAPI only in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS middleware must be placed before Authorization and Controller mapping
app.UseCors("SafeWatchAllowFrontend");

app.UseAuthorization();

app.MapControllers();

// Seeding and Auto-creation of Database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();

    if (!context.VehicleEvents.Any())
    {
        var seedEvents = new List<VehicleEvent>
        {
            new VehicleEvent
            {
                Placa = "ABC-1234",
                FechaHoraDeteccion = new DateTime(2026, 7, 2, 2, 15, 0),
                ZonaMonitoreada = "Zona Residencial A",
                FrecuenciaIngreso = 4,
                TipoVehiculo = "Sedán",
                ColorVehiculo = "Negro",
                Camara = "CAM-10",
                Estado = "SOSPECHOSO",
                NivelRiesgo = "Medio",
                EsListaNegra = false,
                ImagenVehiculo = "vehicle-sedan-black.jpg"
            },
            new VehicleEvent
            {
                Placa = "ABC-1234", // Duplicate vehicle in the early morning at a different location
                FechaHoraDeteccion = new DateTime(2026, 7, 2, 2, 45, 0),
                ZonaMonitoreada = "Zona Bancaria B",
                FrecuenciaIngreso = 5,
                TipoVehiculo = "Sedán",
                ColorVehiculo = "Negro",
                Camara = "CAM-12",
                Estado = "SOSPECHOSO",
                NivelRiesgo = "Alto",
                EsListaNegra = false,
                ImagenVehiculo = "vehicle-sedan-black-2.jpg"
            },
            new VehicleEvent
            {
                Placa = "XYZ-9876", // Blacklisted vehicle loitering
                FechaHoraDeteccion = new DateTime(2026, 7, 2, 3, 0, 0),
                ZonaMonitoreada = "Sector Comercial C",
                FrecuenciaIngreso = 15,
                TipoVehiculo = "Camioneta",
                ColorVehiculo = "Gris",
                Camara = "CAM-05",
                Estado = "SOSPECHOSO",
                NivelRiesgo = "Critico",
                EsListaNegra = true,
                ImagenVehiculo = "vehicle-suv-grey.jpg"
            },
            new VehicleEvent
            {
                Placa = "KBP-5544", // Loitering motorcycle in dawn hours
                FechaHoraDeteccion = new DateTime(2026, 7, 2, 4, 10, 0),
                ZonaMonitoreada = "Entrada Principal",
                FrecuenciaIngreso = 9,
                TipoVehiculo = "Motocicleta",
                ColorVehiculo = "Rojo",
                Camara = "CAM-01",
                Estado = "SOSPECHOSO",
                NivelRiesgo = "Medio",
                EsListaNegra = false,
                ImagenVehiculo = "vehicle-moto-red.jpg"
            },
            new VehicleEvent
            {
                Placa = "PTY-8899", // Normal authorized vehicle for baseline
                FechaHoraDeteccion = new DateTime(2026, 7, 2, 9, 30, 0),
                ZonaMonitoreada = "Estacionamiento Interno",
                FrecuenciaIngreso = 1,
                TipoVehiculo = "Sedán",
                ColorVehiculo = "Blanco",
                Camara = "CAM-03",
                Estado = "AUTORIZADO",
                NivelRiesgo = "Bajo",
                EsListaNegra = false,
                ImagenVehiculo = "vehicle-sedan-white.jpg"
            }
        };

        context.VehicleEvents.AddRange(seedEvents);
        context.SaveChanges();
    }
}

app.Run();
