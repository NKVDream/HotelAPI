namespace H_Api.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int GuestID { get; set; }
        public virtual Guest Guest { get; set; }
        public int RoomID { get; set; }
        public virtual Room Room { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

    }
}
