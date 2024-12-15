
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SQLitePCL;

public class ParkingService : IParkingService
{
    private readonly IParkingSlotRepository _repo;

    public ParkingService(IParkingSlotRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<SlotDto>> GetSlots()
    {
        var allSlots = await _repo.GetAllSlots();
        return allSlots.Select(slot => new SlotDto
        {
            Id = slot.Id,
            SlotNumebr = slot.SlotNumber,
            Status = slot.Status,
            Type = slot.Type
        }).ToList();
    }

    public async Task<SlotDto> AddParkingSlot(string slotNumber, string type)
    {
        var result  = await _repo.AddParkingSlot(slotNumber, type);
        if(result != null)
        {
            return result;
        }
        else{
            return new SlotDto();
        }
    }

    public async Task<bool> UpdateParkingSlot(int Id, UpdateParkingSlotDto updateDto)
    {
        bool result = await _repo.UpdateParkingSlotById(Id, updateDto);
        return result;
    }
}