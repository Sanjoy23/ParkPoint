
public class ParkingService : IParkingService
{
    private readonly IParkingSlotRepository _repo;

    public ParkingService(IParkingSlotRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Slot>> GetSlots()
    {
        var allSlots = await _repo.GetAllSlots();
        return allSlots.Select(slot => new Slot
            {
                Id = slot.Id,
                SlotNumebr = slot.SlotNumber,
                Status = slot.Status,
                Type = slot.Type
            }).ToList();
    }
}