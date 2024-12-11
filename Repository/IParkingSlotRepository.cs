public interface IParkingSlotRepository{
    Task<List<ParkingSlot>> GetAllSlots();
}