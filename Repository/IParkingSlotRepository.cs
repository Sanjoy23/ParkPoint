public interface IParkingSlotRepository{
    Task<List<ParkingSlot>> GetAllSlots();
    Task<SlotDto> AddParkingSlot(string slotNumber, string type);
}