using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeWatchAI.API.Data;
using SafeWatchAI.API.Models;
using System.Text;
using System.Text.Json;

namespace SafeWatchAI.API.Controllers;

[ApiController]
[Route("api/vehicle-events")]
public class VehicleEventsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public VehicleEventsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleEvent>>> GetAllEvents()
    {
        var events = await _context.VehicleEvents
            .OrderByDescending(e => e.FechaHoraDeteccion)
            .ToListAsync();
        return Ok(events);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VehicleEvent>> GetById(int id)
    {
        var vehicleEvent = await _context.VehicleEvents
            .FirstOrDefaultAsync(e => e.Id == id);

        if (vehicleEvent is null)
            return NotFound(new { message = "Evento vehicular no encontrado." });

        return Ok(vehicleEvent);
    }

    [HttpGet("plate/{placa}")]
    public async Task<ActionResult<IEnumerable<VehicleEvent>>> GetByPlate(string placa)
    {
        var events = await _context.VehicleEvents
            .Where(e => e.Placa.ToLower() == placa.ToLower())
            .OrderByDescending(e => e.FechaHoraDeteccion)
            .ToListAsync();

        if (!events.Any())
            return NotFound(new { message = "No existen eventos para la placa indicada." });

        return Ok(events);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadVideo(IFormFile video)
    {
        if (video == null || video.Length == 0)
        {
            return BadRequest(new { message = "El archivo de video es obligatorio." });
        }

        // IA Simulada: Creación de evento sospechoso extraído del video
        var newEvent = new VehicleEvent
        {
            Placa = "PCR-7890",
            FechaHoraDeteccion = DateTime.Now,
            ZonaMonitoreada = "Perímetro Norte",
            FrecuenciaIngreso = 4,
            TipoVehiculo = "Hyundai Tucson Negro",
            ColorVehiculo = "Negro",
            Camara = "CAM-09",
            Estado = "SOSPECHOSO",
            IndiceRiesgo = "92",
            EsListaNegra = false,
            ImagenVehiculo = "vehicle-tucson.jpg"
        };

        _context.VehicleEvents.Add(newEvent);
        await _context.SaveChangesAsync();

        // Integración con Telegram: Notificar al cuerpo de seguridad
        using (var httpClient = new HttpClient())
        {
            var telegramUrl = "https://api.telegram.org/bot7725948190:AAGnK_zKxXWdfp05R1wX4p82Yd9M4L_zQwE/sendMessage";
            var payload = new
            {
                chat_id = "-4712958102",
                text = "🚨 [SafeWatch AI] ALERTA CRÍTICA: Vehículo sospechoso detectado por cámara. Placa: PCR-7890. Zona: Perímetro Norte."
            };

            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync(telegramUrl, content);
                if (!response.IsSuccessStatusCode)
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error de Telegram API: {response.StatusCode} - {errorMsg}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar mensaje a Telegram: {ex.Message}");
            }
        }

        return Ok(newEvent);
    }
}