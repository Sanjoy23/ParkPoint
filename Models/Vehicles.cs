using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Vehicles{
    [Key]
    public int Id { get; set; }
    public string? LicensePlate { get; set; }
    [ForeignKey("ParkingSlots")]
    public int SlotId { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime ExitTime { get; set; }
    public string? Status { get; set; }
}