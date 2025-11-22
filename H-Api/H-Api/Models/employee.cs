using System.ComponentModel.DataAnnotations.Schema;

namespace H_Api.Models
{
    public class Employee
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("employee_role")]
        public int EmployeeRole { get; set; }

        [ForeignKey("EmployeeRole")]
        public virtual Role Role { get; set; }
        public virtual ICollection<Room> ResponsibleRooms { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}