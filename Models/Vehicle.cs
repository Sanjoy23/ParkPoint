using System.ComponentModel.DataAnnotations.Schema;

public class Vehicle{
    public int Id { get; set; }
    public string? LicensePlate { get; set; } = string.Empty;
    [ForeignKey("ParkingSlot")]
    public int SlotId { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime ExitTime { get; set; }
    public string? Status { get; set; } = string.Empty;
}