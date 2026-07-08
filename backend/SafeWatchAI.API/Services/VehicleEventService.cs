using SafeWatchAI.API.Models;

namespace SafeWatchAI.API.Services;

public class VehicleEventService
{
    private readonly List<VehicleEvent> _events = new()
    {
        new VehicleEvent
        {
            Id = 1,
            Placa = "XYZ-9876",
            FechaHoraDeteccion = new DateTime(2025, 6, 28, 2, 15, 10),
            ZonaMonitoreada = "Sector Industrial C",
            FrecuenciaIngreso = 12,
            TipoVehiculo = "Camión Ligero",
            ColorVehiculo = "Gris",
            Camara = "CAM-08",
            Estado = "SOSPECHOSO",
            IndiceRiesgo = "Alto",
            EsListaNegra = true,
            ImagenVehiculo = "vehicle-truck.jpg"
        },
        new VehicleEvent
        {
            Id = 2,
            Placa = "ABC-1234",
            FechaHoraDeteccion = new DateTime(2025, 6, 29, 8, 30, 0),
            ZonaMonitoreada = "Entrada Norte-1",
            FrecuenciaIngreso = 2,
            TipoVehiculo = "Sedán",
            ColorVehiculo = "Blanco",
            Camara = "CAM-01",
            Estado = "AUTORIZADO",
            IndiceRiesgo = "Bajo",
            EsListaNegra = false,
            ImagenVehiculo = "vehicle-sedan.jpg"
        },
        new VehicleEvent
        {
            Id = 3,
            Placa = "PQR-1357",
            FechaHoraDeteccion = new DateTime(2025, 6, 30, 0, 10, 33),
            ZonaMonitoreada = "Sector Industrial C",
            FrecuenciaIngreso = 6,
            TipoVehiculo = "Sedán",
            ColorVehiculo = "Negro",
            Camara = "CAM-08",
            Estado = "SOSPECHOSO",
            IndiceRiesgo = "Medio",
            EsListaNegra = false,
            ImagenVehiculo = "vehicle-black.jpg"
        }
    };

    public IEnumerable<VehicleEvent> GetAll()
    {
        return _events.OrderByDescending(e => e.FechaHoraDeteccion);
    }

    public VehicleEvent? GetById(int id)
    {
        return _events.FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<VehicleEvent> GetByPlate(string placa)
    {
        return _events
            .Where(e => e.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(e => e.FechaHoraDeteccion);
    }
}