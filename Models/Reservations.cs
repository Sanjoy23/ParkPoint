public class Reservations
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SlotId { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public DateTime EntryTime { get; set; }
    public DateTime ExitTime { get; set; }
    public Boolean Paid { get; set; }
}