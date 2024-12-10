using System.ComponentModel.DataAnnotations;

public class ParkingSlots{
    [Key]
    public int Id { get; set; }
    public string? SlotNumber { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
    public string? ReservedFor { get; set; }
}