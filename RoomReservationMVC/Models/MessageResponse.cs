namespace RoomReservationMVC.Models
{
    public class MessageResponse<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public T? data { get; set; } = default;
    }
}
