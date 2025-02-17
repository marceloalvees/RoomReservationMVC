namespace RoomReservationMVC.Models
{
    public class ReservationResponseModel
    {
        public int id { get; set; }
        public required string roomName { get; set; }
        public DateTime date { get; set; }
        public DateTime dateCancelation { get; set; }
        public EReservationStatus status { get; set; }
        public int capacity { get; set; }
    }
}
