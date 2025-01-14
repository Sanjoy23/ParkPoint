using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace ParkPoint.Controllers;

[Route("web-api/[controller]")]
[ApiController]
public class ParkingSoltsController : ControllerBase
{
    private readonly IParkingService _service;
    private readonly IDistributedCache _distributedCache;

    //private readonly IMemoryCache _memoryCache;

    public ParkingSoltsController(IParkingService service, IDistributedCache distributedCache)//IMemoryCache memoryCache)
    {
        _service = service;
        //_memoryCache = memoryCache;
        _distributedCache = distributedCache;
    }

    [HttpGet("slots")]
    //[ResponseCache(Location = ResponseCacheLocation.Any, Duration = 10000)] -- response cachig
    public async Task<IActionResult> getAllSlots()
    {
        var result = await _service.GetSlots();
        return Ok(result);
        //return await DistributedCache(); -- for distributed caching using redis
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
    [HttpPut("update-slot")]
    public async Task<IActionResult> UpdateParkingSlot(int Id, [FromBody] UpdateParkingSlotDto updateSlotDto)
    {
        //if(updateSlotDto == null) return BadRequest("Invalid data");
        bool result = await _service.UpdateParkingSlot(Id, updateSlotDto);
        if(result)
        {
            return Ok("Parking slot updated successfully.");
        }
        else {
            return NotFound("Parking slot not found.");
        }
    }

    // private async Task<IActionResult> MemoryCache()
    // {
    //     var cachedData = _memoryCache.Get<List<SlotDto>>("ParkingSlots");
    //     if(cachedData != null)
    //     {
    //         return Ok(cachedData);
    //     }

    //     var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
    //     cachedData = await _service.GetSlots();
    //     _memoryCache.Set("ParkingSlots",cachedData,expirationTime);
    //     return Ok(cachedData);
    // }

    private async Task<IActionResult> DistributedCache()
    {
        var cachedData = await _distributedCache.GetStringAsync("ParkingSlots");
        if(cachedData != null)
        {
            return Ok(cachedData);
        }
        var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
        var parkingSlots = await _service.GetSlots();
        cachedData = JsonConvert.SerializeObject(parkingSlots);
        var cacheOption = new DistributedCacheEntryOptions().SetAbsoluteExpiration(expirationTime);
        await _distributedCache.SetStringAsync("ParkingSlots", cachedData, cacheOption);
        return Ok(parkingSlots);
    }
}