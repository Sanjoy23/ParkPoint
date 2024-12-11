using Microsoft.EntityFrameworkCore;

public class ParkPointContext : DbContext
{
    public ParkPointContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<ParkingSlot> ParkingSlots{get; set;} = null!;
    public DbSet<Vehicle> Vehicles {get; set;} = null!;
}