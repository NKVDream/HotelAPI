using System.ComponentModel.DataAnnotations.Schema;

namespace H_Api.Models
{
    public class Reservation
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("guest")]
        public int GuestID { get; set; }
        public virtual Guest Guest { get; set; }

        [Column("room")]
        public int RoomID { get; set; }
        public virtual Room Room { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("employee")]
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
    }
}