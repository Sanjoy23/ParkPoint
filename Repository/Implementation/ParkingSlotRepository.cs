
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

public class ParkingSlotRepository : IParkingSlotRepository
{
    private readonly ParkPointContext _context;

    public ParkingSlotRepository(ParkPointContext context)
    {
        _context = context;
    }

    public async Task<List<ParkingSlot>> GetAllSlots()
    {
        return await _context.ParkingSlots.ToListAsync();
    }
}