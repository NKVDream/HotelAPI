using System.ComponentModel.DataAnnotations.Schema;

namespace H_Api.Models
{
    public class Role
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("role_name")]
        public string RoleName { get; set; }

        [Column("description")]
        public string Description { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
