public interface IParkingSlotRepository{
    Task<List<ParkingSlot>> GetAllSlots();
    Task<SlotDto> AddParkingSlot(string slotNumber, string type);
    Task<bool> UpdateParkingSlotById(int Id, UpdateParkingSlotDto updateDto);
}