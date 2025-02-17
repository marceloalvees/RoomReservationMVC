namespace RoomReservationMVC.Models
{
    public class UpdateReservationModel
    {
        public int ReservationId { get; set; }
        public DateTime Date { get; set; }
        public EReservationStatus Status { get; set; }
    }
}
