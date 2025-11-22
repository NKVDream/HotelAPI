using System.ComponentModel.DataAnnotations.Schema;

namespace H_Api.Models
{
    public class Room
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("room_capacity")]
        public int RoomCapacity { get; set; }

        [Column("room_level")]
        public int RoomLevel { get; set; }

        [Column("price")]
        public int Price { get; set; }

        [Column("availability")]
        public bool Availability { get; set; }

        [Column("floor")]
        public int Floor { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("responsible_employee")]
        public int ResponsibleEmployee { get; set; }

        [ForeignKey("RoomLevel")]
        public virtual Level Level { get; set; }

        [ForeignKey("ResponsibleEmployee")]
        public virtual Employee Employee { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}