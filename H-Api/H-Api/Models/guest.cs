using System.ComponentModel.DataAnnotations.Schema;

namespace H_Api.Models
{
    public class Guest
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [Column("phone")]
        public string Phone { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}