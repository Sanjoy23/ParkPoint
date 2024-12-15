using Microsoft.AspNetCore.Mvc;

namespace ParkPoint.Controllers;

[Route("web-api/[controller]")]
[ApiController]
public class ParkingSoltsController : ControllerBase
{
    private readonly IParkingService _service;
    public ParkingSoltsController(IParkingService service)
    {
        _service = service;
    }

    [HttpGet("Slots")]
    public async Task<IActionResult> getAllSlots()
    {
        var result = await _service.GetSlots();
        return Ok(result);
    }

    [HttpPost("add-slot")]
    public async Task<IActionResult> AddParkingSlot(string slotNumber, string type)
    {
        try
        {
            var result = await _service.AddParkingSlot(slotNumber, type);
            if (result != null)
            {
                return Ok(result);
            }
            else return Conflict("Slot already exists");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}