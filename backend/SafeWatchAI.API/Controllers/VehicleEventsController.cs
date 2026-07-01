using Microsoft.AspNetCore.Mvc;
using SafeWatchAI.API.Models;
using SafeWatchAI.API.Services;

namespace SafeWatchAI.API.Controllers;

[ApiController]
[Route("api/vehicle-events")]
public class VehicleEventsController : ControllerBase
{
    private readonly VehicleEventService _vehicleEventService;

    public VehicleEventsController(VehicleEventService vehicleEventService)
    {
        _vehicleEventService = vehicleEventService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<VehicleEvent>> GetAllEvents()
    {
        var events = _vehicleEventService.GetAll();
        return Ok(events);
    }

    [HttpGet("{id:int}")]
    public ActionResult<VehicleEvent> GetById(int id)
    {
        var vehicleEvent = _vehicleEventService.GetById(id);

        if (vehicleEvent is null)
            return NotFound(new { message = "Evento vehicular no encontrado." });

        return Ok(vehicleEvent);
    }

    [HttpGet("plate/{placa}")]
    public ActionResult<IEnumerable<VehicleEvent>> GetByPlate(string placa)
    {
        var events = _vehicleEventService.GetByPlate(placa);

        if (!events.Any())
            return NotFound(new { message = "No existen eventos para la placa indicada." });

        return Ok(events);
    }
}