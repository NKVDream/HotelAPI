using System.ComponentModel.DataAnnotations.Schema;

namespace H_Api.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomCapacity { get; set; }
        public int RoomLevel { get; set; }
        public int Price { get; set; }
        public bool Availability { get; set; }
        public int Floor { get; set; }
        public string Description { get; set; }

        public int ResponsibleEmployee { get; set; }


        [ForeignKey("RoomLevel")]
        public virtual Level Level { get; set; } 

        [ForeignKey("ResponsibleEmployee")]
        public virtual Employee Employee { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; } 
    }
}