namespace H_Api.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
