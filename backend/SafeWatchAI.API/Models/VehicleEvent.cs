namespace SafeWatchAI.API.Models;

/// <summary>
/// Represents a vehicle detection event captured by the SafeWatch AI monitoring system.
/// Each instance records a single detection occurrence within a monitored residential zone,
/// including behavioral pattern indicators such as frequency and risk assessment.
/// </summary>
public class VehicleEvent
{
    /// <summary>
    /// Unique identifier for the audit record.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Alphanumeric license plate identification of the detected vehicle.
    /// </summary>
    public string Placa { get; set; } = string.Empty;

    /// <summary>
    /// Exact timestamp of the vehicle's appearance in the monitored zone.
    /// </summary>
    public DateTime FechaHoraDeteccion { get; set; }

    /// <summary>
    /// Specific sector where the event was captured
    /// (e.g., "Entrada Norte-1", "Sector Industrial C", "Área de Carga").
    /// </summary>
    public string ZonaMonitoreada { get; set; } = string.Empty;

    /// <summary>
    /// Correlated count of how many times the vehicle has appeared within a critical period.
    /// </summary>
    public int FrecuenciaIngreso { get; set; }

    /// <summary>
    /// Vehicle classification (e.g., "Sedan", "SUV", "Camión Ligero").
    /// </summary>
    public string TipoVehiculo { get; set; } = string.Empty;

    /// <summary>
    /// Operational status of the analysis.
    /// Valid business values: "DETECTADO", "SOSPECHOSO", "AUTORIZADO".
    /// </summary>
    public string Estado { get; set; } = string.Empty;

    /// <summary>
    /// Calculated criticality level (e.g., "Bajo", "Medio", "Alto").
    /// </summary>
    public string IndiceRiesgo { get; set; } = string.Empty;
}
