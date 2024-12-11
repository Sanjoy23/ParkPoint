using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

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
}