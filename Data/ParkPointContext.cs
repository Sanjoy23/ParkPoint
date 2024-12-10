using Microsoft.EntityFrameworkCore;

public class ParkPointContext : DbContext
{
    public ParkPointContext(DbContextOptions options) : base(options)
    {
    }
    DbSet<ParkingSlot> ParkingSlots{get; set;} = null!;
    DbSet<Vehicle> Vehicles {get; set;} = null!;
}