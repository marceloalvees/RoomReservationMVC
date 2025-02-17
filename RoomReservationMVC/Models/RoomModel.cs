namespace RoomReservationMVC.Models
{
    public class RoomModel
    {
        public int id { get; set; }
        public required string name { get; set; }
        public int capacity { get; set; }
        public required string location { get; set; }
    }
}
