using Microsoft.AspNetCore.Mvc;
using SafeWatchAI.API.Models;

namespace SafeWatchAI.API.Controllers;

/// <summary>
/// RESTful API controller responsible for exposing vehicle monitoring events.
/// Provides endpoints for querying detection records captured by the SafeWatch AI
/// distributed surveillance system across residential zones.
/// </summary>
[ApiController]
[Route("api/vehicle-events")]
public class VehicleEventsController : ControllerBase
{
    /// <summary>
    /// Retrieves the complete list of vehicle detection events.
    /// Returns in-memory simulated data reflecting realistic monitoring patterns,
    /// including suspicious recurrence anomalies and authorized baseline traffic.
    /// </summary>
    /// <returns>A list of <see cref="VehicleEvent"/> objects representing detected events.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<VehicleEvent>> GetAllEvents()
    {
        var events = new List<VehicleEvent>
        {
            // =====================================================================
            // SUSPICIOUS PATTERN: Recurring plate "XYZ-9876" detected during
            // late-night hours in critical industrial/cargo zones.
            // High frequency + off-hours = elevated risk profile.
            // =====================================================================
            new VehicleEvent
            {
                Id = 1,
                Placa = "XYZ-9876",
                FechaHoraDeteccion = new DateTime(2025, 6, 28, 2, 15, 10),
                ZonaMonitoreada = "Sector Industrial C",
                FrecuenciaIngreso = 12,
                TipoVehiculo = "Camión Ligero",
                Estado = "SOSPECHOSO",
                IndiceRiesgo = "Alto"
            },
            new VehicleEvent
            {
                Id = 2,
                Placa = "XYZ-9876",
                FechaHoraDeteccion = new DateTime(2025, 6, 28, 3, 42, 55),
                ZonaMonitoreada = "Área de Carga",
                FrecuenciaIngreso = 12,
                TipoVehiculo = "Camión Ligero",
                Estado = "SOSPECHOSO",
                IndiceRiesgo = "Alto"
            },
            new VehicleEvent
            {
                Id = 3,
                Placa = "XYZ-9876",
                FechaHoraDeteccion = new DateTime(2025, 6, 29, 1, 58, 30),
                ZonaMonitoreada = "Sector Industrial C",
                FrecuenciaIngreso = 12,
                TipoVehiculo = "Camión Ligero",
                Estado = "SOSPECHOSO",
                IndiceRiesgo = "Alto"
            },

            // =====================================================================
            // NORMAL TRAFFIC: Authorized and detected vehicles with low frequency,
            // operating during standard daytime hours in residential zones.
            // =====================================================================
            new VehicleEvent
            {
                Id = 4,
                Placa = "ABC-1234",
                FechaHoraDeteccion = new DateTime(2025, 6, 29, 8, 30, 0),
                ZonaMonitoreada = "Entrada Norte-1",
                FrecuenciaIngreso = 2,
                TipoVehiculo = "Sedan",
                Estado = "AUTORIZADO",
                IndiceRiesgo = "Bajo"
            },
            new VehicleEvent
            {
                Id = 5,
                Placa = "DEF-5678",
                FechaHoraDeteccion = new DateTime(2025, 6, 29, 9, 15, 22),
                ZonaMonitoreada = "Entrada Norte-1",
                FrecuenciaIngreso = 1,
                TipoVehiculo = "SUV",
                Estado = "AUTORIZADO",
                IndiceRiesgo = "Bajo"
            },
            new VehicleEvent
            {
                Id = 6,
                Placa = "GHI-3456",
                FechaHoraDeteccion = new DateTime(2025, 6, 29, 14, 5, 45),
                ZonaMonitoreada = "Entrada Sur-2",
                FrecuenciaIngreso = 3,
                TipoVehiculo = "Sedan",
                Estado = "DETECTADO",
                IndiceRiesgo = "Bajo"
            },
            new VehicleEvent
            {
                Id = 7,
                Placa = "JKL-7890",
                FechaHoraDeteccion = new DateTime(2025, 6, 29, 17, 50, 10),
                ZonaMonitoreada = "Entrada Norte-1",
                FrecuenciaIngreso = 1,
                TipoVehiculo = "SUV",
                Estado = "AUTORIZADO",
                IndiceRiesgo = "Bajo"
            },

            // =====================================================================
            // MEDIUM RISK: Vehicle with moderate frequency in sensitive zones
            // during evening hours — warrants continued monitoring.
            // =====================================================================
            new VehicleEvent
            {
                Id = 8,
                Placa = "MNO-2468",
                FechaHoraDeteccion = new DateTime(2025, 6, 29, 22, 30, 0),
                ZonaMonitoreada = "Área de Carga",
                FrecuenciaIngreso = 5,
                TipoVehiculo = "Camión Ligero",
                Estado = "DETECTADO",
                IndiceRiesgo = "Medio"
            },
            new VehicleEvent
            {
                Id = 9,
                Placa = "PQR-1357",
                FechaHoraDeteccion = new DateTime(2025, 6, 30, 0, 10, 33),
                ZonaMonitoreada = "Sector Industrial C",
                FrecuenciaIngreso = 6,
                TipoVehiculo = "Sedan",
                Estado = "SOSPECHOSO",
                IndiceRiesgo = "Medio"
            },
            new VehicleEvent
            {
                Id = 10,
                Placa = "STU-8024",
                FechaHoraDeteccion = new DateTime(2025, 6, 29, 11, 20, 0),
                ZonaMonitoreada = "Entrada Sur-2",
                FrecuenciaIngreso = 1,
                TipoVehiculo = "SUV",
                Estado = "AUTORIZADO",
                IndiceRiesgo = "Bajo"
            }
        };

        return Ok(events);
    }
}
