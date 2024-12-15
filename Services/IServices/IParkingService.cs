public interface IParkingService{
    Task<List<SlotDto>> GetSlots();
    Task<SlotDto>AddParkingSlot(string slotNumber, string type);
    Task<bool> UpdateParkingSlot(int Id, UpdateParkingSlotDto updateDto);
}