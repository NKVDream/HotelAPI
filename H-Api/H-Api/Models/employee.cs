using System.ComponentModel.DataAnnotations.Schema;

namespace H_Api.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int EmployeeRole { get; set; }

        [ForeignKey("EmployeeRole")]
        public virtual Role Role { get; set; }
    }
}
