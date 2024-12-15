
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

public class ParkingSlotRepository : IParkingSlotRepository
{
    private readonly ParkPointContext _context;

    public ParkingSlotRepository(ParkPointContext context)
    {
        _context = context;
    }

    public async Task<SlotDto> AddParkingSlot(string slotNumber, string type)
    {
        var existingSlot = await _context.ParkingSlots.FirstOrDefaultAsync(x => x.SlotNumber == slotNumber);
        
        if(existingSlot != null) return new SlotDto();
        
        var newSlot = new ParkingSlot
        {
            SlotNumber = slotNumber,
            Type = type,
            Status = "Vacant"
        };
        _context.ParkingSlots.Add(newSlot);
        await _context.SaveChangesAsync();

        var slotDto = new SlotDto
        {
            Id = newSlot.Id,
            SlotNumebr = newSlot.SlotNumber,
            Type = newSlot.Type,
            Status = newSlot.Status
        };
        return slotDto;
    }

    public async Task<List<ParkingSlot>> GetAllSlots()
    {
        return await _context.ParkingSlots.ToListAsync();
    }
}