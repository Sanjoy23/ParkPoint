public interface IParkingService{
    Task<List<Slot>> GetSlots();
}