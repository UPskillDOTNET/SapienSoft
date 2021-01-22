namespace Park1API.Models
{
    public enum Status { Available, Booked, Reserved }

    public class Slot
    {
        public int SlotId {get; set; }
        public string LocationCode { get; set; }
        public Status Status { get; set; }
    }
}
