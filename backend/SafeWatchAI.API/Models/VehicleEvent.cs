namespace SafeWatchAI.API.Models;

/// <summary>
/// Represents a vehicle detection event captured by the SafeWatch AI monitoring system.
/// Each instance records a single detection occurrence within a monitored zone,
/// including behavioral indicators and risk assessment.
/// </summary>
public class VehicleEvent
{
    public int Id { get; set; }

    public string Placa { get; set; } = string.Empty;

    public DateTime FechaHoraDeteccion { get; set; }

    public string ZonaMonitoreada { get; set; } = string.Empty;

    public int FrecuenciaIngreso { get; set; }

    public string TipoVehiculo { get; set; } = string.Empty;

    public string ColorVehiculo { get; set; } = string.Empty;

    public string Camara { get; set; } = string.Empty;

    /// <summary>
    /// Valid values: DETECTADO, SOSPECHOSO, AUTORIZADO.
    /// </summary>
    public string Estado { get; set; } = string.Empty;

    /// <summary>
    /// Valid values: BAJO, MEDIO, ALTO, CRITICO.
    /// </summary>
    public string NivelRiesgo { get; set; } = string.Empty;

    public string IndiceRiesgo
    {
        get => NivelRiesgo;
        set => NivelRiesgo = value;
    }

    public bool EsListaNegra { get; set; }

    public string ImagenVehiculo { get; set; } = string.Empty;
}